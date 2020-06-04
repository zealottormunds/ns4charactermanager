using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
    public partial class Tool_AddCostume : Form
    {
        Main mf;

        public Tool_AddCostume(Main m)
        {
            mf = m;
            InitializeComponent();
        }

        public int AddCostume()
        {
            string character = w_base.Text;
            string model = w_model.Text;

            // Open DPP
            Tool_DuelPlayerParamEditor dpp = new Tool_DuelPlayerParamEditor();
            dpp.OpenFile(mf.dppPath);

            // Find DPP index
            int dppIndex = -1;
            for (int x = 0; x < dpp.EntryCount; x++)
            {
                string thischaracter = dpp.CharaList[x];
                if (thischaracter == character)
                {
                    dppIndex = x;
                    x = dpp.EntryCount;
                }
            }
            if (dppIndex == -1)
            {
                if(this.Visible) MessageBox.Show("Base character not found in duelPlayerParam.");
                return 1;
            }

            // Find null costume and add ours
            int dpp_costId = -1;
            dpp.listBox1.SelectedIndex = dppIndex;
            for (int x = 0; x < 20; x++)
            {
                if (dpp.CostumeList[dppIndex][x] == "")
                {
                    dpp.CostumeList[dppIndex][x] = model;
                    dpp_costId = x;
                    x = 20;
                }
            }
            if (dpp_costId == -1)
            {
                if(this.Visible) MessageBox.Show("This character's costumes are full.");
                return 2;
            }

            // Open PSP
            Tool_PlayerSettingParamEditor psp = new Tool_PlayerSettingParamEditor();
            psp.OpenFile(mf.pspPath);

            // Find PSP entry
            int pspIndex = -1;
            for (int x = 0; x < psp.EntryCount; x++)
            {
                string thischaracter = psp.CharacterList[x];
                if (character == thischaracter.Substring(0, character.Length))
                {
                    pspIndex = x;
                    x = psp.EntryCount;
                }
            }
            if (pspIndex == -1)
            {
                if(this.Visible) MessageBox.Show("Base character not found in playerSettingParam.");
                return 3;
            }

            // Create psp entry for our costume
            psp.ListBox1.SelectedIndex = pspIndex;
            psp.AddID();
            pspIndex = psp.ListBox1.Items.Count - 1;

            // Set a new name (this will find an unused number, like 3obt00, 3obt01, 3obt02, until a number is not used)
            int maxNum = 0;
            for (int x = 0; x < psp.EntryCount; x++)
            {
                if (psp.CharacterList[x].Substring(0, character.Length) == character)
                {
                    int actualNum = int.Parse(psp.CharacterList[x].Substring(psp.CharacterList[x].Length - 2, 2));
                    if (actualNum > maxNum) maxNum = actualNum;
                }
            }
            string characterPspName = character;
            maxNum = maxNum + 1;

            if (maxNum < 0xF) characterPspName += "0" + maxNum.ToString("X2");
            else characterPspName += maxNum.ToString("X2");

            psp.CharacterList[pspIndex] = characterPspName;
            psp.OptValueA[pspIndex] = dpp_costId;

            // Open roster tool
            Tool_RosterEditor csp = new Tool_RosterEditor();
            csp.OpenFile(mf.cspPath);

            // Find roster ID
            int rosterId = -1;
            for (int x = 0; x < csp.EntryCount; x++)
            {
                if (character == csp.CharacterList[x].Substring(0, character.Length))
                {
                    rosterId = x;
                    x = csp.EntryCount;
                }
            }
            if (rosterId == -1)
            {
                if (this.Visible) MessageBox.Show("Base character not found in characterSelectParam.");
                return 4;
            }

            // Copy roster entry
            csp.ListBox1.SelectedIndex = rosterId;
            csp.AddEntry();
            rosterId = csp.EntryCount - 1;

            // Set roster name as psp name
            csp.CharacterList[rosterId] = characterPspName;

            // Set as last costume
            int costumecount = 0;
            int thispage = csp.PageList[rosterId];
            int thispos = csp.PositionList[rosterId];
            for (int x = 0; x < csp.EntryCount; x++)
            {
                if (csp.PageList[x] == thispage && csp.PositionList[x] == thispos)
                {
                    int thiscos = csp.CostumeList[x];
                    if (thiscos > costumecount) costumecount = thiscos;
                }
                /*if (csp.CharacterList[x].Substring(0, character.Length) == character)
                {
                    int thiscost = csp.CostumeList[x];
                    if (thiscost > costumecount) costumecount = thiscost;
                }*/
            }
            costumecount = costumecount + 1;
            csp.CostumeList[rosterId] = costumecount;

            // Save files
            dpp.SaveFile();
            psp.SaveFile();
            csp.SaveFile();

            if(this.Visible) MessageBox.Show("Added costume of base character " + character + " with model " + model +
                " in roster page " + csp.PageList[rosterId].ToString("X2") + " and position " +
                csp.PositionList[rosterId].ToString("X2") + ", as the costume id " + csp.CostumeList[rosterId].ToString("X2"));

            return 0;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            AddCostume();
        }
    }
}
