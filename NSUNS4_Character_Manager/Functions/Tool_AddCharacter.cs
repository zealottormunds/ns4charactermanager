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
    public partial class Tool_AddCharacter : Form
    {
        Main mf;
        public Tool_AddCharacter(Main m)
        {
            InitializeComponent();
            mf = m;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string character = w_chara.Text;
            string basechar = w_basechar.Text;
            string model = w_model.Text;
            int page = (int)w_page.Value;
            int pos = (int)w_pos.Value;
            int charaindex = -1;

            // Check if characode already exists
            Tool_CharacodeEditor cc = new Tool_CharacodeEditor();
            cc.OpenFile(mf.chaPath);
            bool exists = false;
            for(int x = 0; x < cc.CharacterCount; x++)
            {
                if(character == cc.CharacterList[x])
                {
                    exists = true;
                    x = cc.CharacterCount;
                }
            }

            if(exists)
            {
                MessageBox.Show("Characode ID already exists.");
                return;
            }

            // Add characode
            cc.AddID(character);
            charaindex = cc.CharacterCount;

            // Open DPP
            Tool_DuelPlayerParamEditor dpp = new Tool_DuelPlayerParamEditor();
            dpp.OpenFile(mf.dppPath);

            // Find base character's dpp entry
            int basedpp = -1;
            for(int x = 0; x < dpp.EntryCount; x++)
            {
                if(basechar == dpp.CharaList[x])
                {
                    basedpp = x;
                    x = dpp.EntryCount;
                }
            }
            if(basedpp == -1)
            {
                MessageBox.Show("Base character not found in duelPlayerParam.");
                return;
            }

            // Add dpp entry
            dpp.listBox1.SelectedIndex = basedpp;
            dpp.AddEntry();
            int indexdpp = dpp.EntryCount - 1;
            dpp.CharaList[indexdpp] = character;

            // Set costume
            if (model != "") dpp.CostumeList[indexdpp][0] = model;
            else dpp.CostumeList[indexdpp][0] = character;

            // Set binPath and binName
            dpp.BinPath[indexdpp] = "Z:/param/player/Converter/bin/" + character + "prm_bas.bin";
            dpp.BinName[indexdpp] = character + "prm_bas";

            // Open PSP
            Tool_PlayerSettingParamEditor psp = new Tool_PlayerSettingParamEditor();
            psp.OpenFile(mf.pspPath);

            int pspIndex = -1;
            for (int x = 0; x < psp.EntryCount; x++)
            {
                string thischaracter = psp.CharacterList[x];
                if (basechar == thischaracter.Substring(0, basechar.Length))
                {
                    pspIndex = x;
                    x = psp.EntryCount;
                }
            }
            if (pspIndex == -1)
            {
                MessageBox.Show("Base character not found in playerSettingParam.");
                return;
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
            psp.OptValueA[pspIndex] = 0;
            psp.CharacodeList[pspIndex] = BitConverter.GetBytes(charaindex);

            // Open roster tool
            Tool_RosterEditor csp = new Tool_RosterEditor();
            csp.OpenFile(mf.cspPath);

            // Find roster ID
            int rosterId = -1;
            for (int x = 0; x < csp.EntryCount; x++)
            {
                //if (basechar == csp.CharacterList[x].Substring(0, basechar.Length))
                if(csp.PageList[x] == 2 && csp.PositionList[x] == 0x18)
                {
                    MessageBox.Show(csp.CharacterList[x]);
                }

                if(csp.CharacterList[x].Contains(basechar))
                {
                    rosterId = x;
                    x = csp.EntryCount;
                }
            }
            if (rosterId == -1)
            {
                MessageBox.Show("Base character not found in characterSelectParam.");
                return;
            }

            // Copy roster entry
            csp.ListBox1.SelectedIndex = rosterId;
            csp.AddEntry();
            rosterId = csp.EntryCount - 1;

            // Set roster name as psp name
            csp.CharacterList[rosterId] = characterPspName;
            csp.PageList[rosterId] = page;
            csp.PositionList[rosterId] = pos;

            // Set costume
            int costumecount = 0;
            int thispage = csp.PageList[rosterId];
            int thispos = csp.PositionList[rosterId];
            for (int x = 0; x < csp.EntryCount - 1; x++)
            {
                if (csp.PageList[x] == thispage && csp.PositionList[x] == thispos)
                {
                    int thiscos = csp.CostumeList[x];
                    if (thiscos > costumecount) costumecount = thiscos;
                }
            }
            costumecount = costumecount + 1;
            csp.CostumeList[rosterId] = costumecount;

            // Save files
            cc.SaveFile();
            dpp.SaveFile();
            psp.SaveFile();
            csp.SaveFile();

            MessageBox.Show("Finished adding character. Please add an entry to SkillCustomizeParam and SpSkillCustomizeParam manually for your character to work. Otherwise, it will crash when picked.");
        }
    }
}
