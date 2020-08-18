using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_Character_Manager.Tools
{
    public partial class Tool_MovesetCoder : Form
    {
        public Tool_MovesetCoder()
        {
            InitializeComponent();

            // FUNCT
            for(int x = 0; x < Program.ME_LIST.Length; x++) t_function.Items.Add(x.ToString("X2") + " = " + Program.ME_LIST[x]);

            // COND
            for(int x = 0; x < Program.COND.Length; x++) t_condition.Items.Add(Program.COND[x]);
            for(int x = Program.COND.Length; x < 256; x++) t_condition.Items.Add("0x" + t_condition.Items.Count.ToString("X2").PadLeft(2, '0') + " = ???");

            // DMG COND
            for (int x = 0; x < Program.DMGCOND.Length; x++) t_dmgcond.Items.Add(Program.DMGCOND[x]);
            for (int x = Program.DMGCOND.Length; x < 256; x++) t_dmgcond.Items.Add("0x" + t_dmgcond.Items.Count.ToString("X2").PadLeft(2, '0') + " = ???");

            // HIT EFF
            for (int x = 0; x < Program.HITEFF.Length; x++) t_hiteffect.Items.Add(Program.HITEFF[x]);
            for (int x = Program.HITEFF.Length; x < 256; x++) t_hiteffect.Items.Add("0x" + t_hiteffect.Items.Count.ToString("X2").PadLeft(2, '0') + " = ???");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileOpen) CloseFile(true);
            else CloseFile();

            OpenFile();
        }

        bool fileOpen = false;
        string filePath = "";
        byte[] fileBytes;

        List<byte[]> verSection = new List<byte[]>();
        List<int> anmCount = new List<int>();
        List<List<byte[]>> anmSection = new List<List<byte[]>>();

        List<int> verList = new List<int>();
        List<int> verLength = new List<int>();
        List<List<byte[]>> plAnmList = new List<List<byte[]>>();
        List<List<List<byte[]>>> movementList = new List<List<List<byte[]>>>();

        void OpenFile()
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();

            if (o.FileName == "" || File.Exists(o.FileName) == false) return;

            filePath = o.FileName;
            fileBytes = File.ReadAllBytes(filePath);

            // Find all ver sections
            int actualver = 0;
            while(actualver != -1)
            {
                actualver = XfbinParser.FindString(fileBytes, "ver0.000", actualver + 1);
                //MessageBox.Show(actualver.ToString("X2"));
                if (actualver != -1)
                {
                    verList.Add(actualver);
                    verLength.Add(Main.b_ReadIntRev(fileBytes, actualver - 4));
                }
            }

            // Add all ver section byte data
            for(int x = 0; x < verList.Count; x++)
            {
                List<byte> actualSection = new List<byte>();
                int begin = verList[x];
                int end = verLength[x];

                for(int y = 0; y < end; y++)
                {
                    actualSection.Add(fileBytes[begin + y]);
                }

                verSection.Add(actualSection.ToArray());
                //File.WriteAllBytes(filePath + "_" + x.ToString(), actualSection.ToArray());
            }

            // List all anm sections
            string[] sectionnames =
            {
                "Awakening",
                "Base",
                "Jutsu",
                "Ultimate Jutsu",
                "Effect",
                "Expansion A",
                "Expansion B",
                "Expansion C"
            };

            for(int a = 0; a < verList.Count; a++)
            {
                listBox1.Items.Add(sectionnames[a]);

                byte[] actualSection = verSection[a];
                int anmSectionCount = actualSection[0x30];
                int start = 0x40;
                int index = 0x40;

                plAnmList.Add(new List<byte[]>());
                movementList.Add(new List<List<byte[]>>()); //

                //anmSection.Add(new List<byte[]>());
                //anmCount.Add(actualSection[0x30]);

                for (int x = 0; x < anmSectionCount; x++)
                {
                    // Add this pl_anm's header to plAnmList
                    List<byte> planmheader = new List<byte>();
                    for (int y = 0; y < 0xD4; y++)
                    {
                        planmheader.Add(actualSection[start + y]);
                    }
                    //MessageBox.Show(Main.b_ReadString(planmheader.ToArray(), 0));
                    plAnmList[a].Add(planmheader.ToArray());
                    movementList[a].Add(new List<byte[]>());

                    index = start + 0x50;
                    byte m_movcount = actualSection[index];
                    //MessageBox.Show("ANM " + x.ToString() + " has " + m_movcount.ToString() + " sections");

                    index = start + 0xD4;

                    // Add each movement section of this pl_anm to the master list
                    for (int y = 0; y < m_movcount; y++)
                    {
                        List<byte> movementsection = new List<byte>();

                        // Default movement section length is 0x40
                        int sectionLength = 0x40;

                        int function = actualSection[index + 0x22] * 0x1 + actualSection[index + 0x23] * 0x100;
                        
                        switch(function)
                        {
                            case 0x83:
                                if (index + 0x40 < actualSection.Length)
                                {
                                    string str = Main.b_ReadString(actualSection, index + 0x40);
                                    if (str == "SPSKILL_END") sectionLength = 0xA0;
                                }
                                break;
                            case 0xC1:
                            case 0xC3:
                            case 0xC6:
                            case 0xC8:
                            case 0xCA:
                            case 0xD1:
                            case 0xD3:
                            case 0xD5:
                            case 0xD7:
                            case 0xD9:
                                sectionLength = 0xA0;
                                break;
                            case 0xA0:
                            case 0xA1:
                            case 0xA2:
                            case 0xA3:
                            case 0xA4:
                            case 0xA5:
                                if (index + 0x40 < actualSection.Length)
                                {
                                    string str = Main.b_ReadString(actualSection, index + 0x40);
                                    if (str.Length > 7 && str.Substring(0, 7) == "SKL_ATK") sectionLength = 0xA0;
                                }
                                break;
                        }

                        // If there's a D (from DAMAGE_ID) in section + 0x40, length is 0xA0
                        if (index + 0x40 < actualSection.Length)
                        {
                            string str = Main.b_ReadString(actualSection, index + 0x40);
                            if(str.Length > 3 && (str.Substring(0, 3) == "DMG" || str.Substring(0, 3) == "DAM"))
                            {
                                sectionLength = 0xA0;
                            }

                            /*char act1 = (char)actualSection[index + 0x40];
                            if (!Char.IsDigit(act1) && Char.IsUpper(act1))
                            {
                                byte byte1 = actualSection[index + 0x40 + 0x2A]; // 20 
                                byte byte2 = actualSection[index + 0x40 + 0x2C]; // 22

                                if(byte1 == 0x0 && byte2 == 0x0)
                                {

                                }
                            }*/
                        }

                        // If the first letter of the hitbox is caps, then it's a special 0x60 section
                        // char act = (char)actualSection[index];
                        // if (actualSection[index] != 0x0 && Char.IsUpper(act) && !Char.IsDigit(act)) sectionLength = 0x40;

                        //MessageBox.Show("Movement " + y.ToString() + " of ANM " + x.ToString() + " is " + sectionLength.ToString("X2") + " bytes long");
                        for (int z = 0; z < sectionLength; z++) movementsection.Add(actualSection[z + index]);
                        index = index + sectionLength;

                        // Add to master list
                        movementList[a][x].Add(movementsection.ToArray());
                    }

                    start = index;
                }
            }

            fileOpen = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int actualIndex = listBox1.SelectedIndex;

            if (fileOpen == false || actualIndex == -1) return;

            ReadAnmList(actualIndex);
        }

        public void ReadAnmList(int sectionindex)
        {
            anm_list.Items.Clear();
            int anmSectionCount = plAnmList[sectionindex].Count;

            for (int x = 0; x < anmSectionCount; x++)
            {
                int index = 0x00;

                string m_planm = Main.b_ReadString(plAnmList[sectionindex][x], index);
                if (m_planm == "") m_planm = "[EMPTY]";

                anm_list.Items.Add(m_planm);
            }
        }

        private void anm_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            int actualIndex = anm_list.SelectedIndex;
            int currentVer = listBox1.SelectedIndex;

            if (fileOpen == false || actualIndex == -1) return;

            t_planm.Text = "";
            t_anm.Text = "";
            t_loadsection.Value = 0;
            t_1cmn.Checked = false;
            t_flag1.Checked = false;
            t_flag2.Checked = false;
            t_flag3.Checked = false;
            t_flag4.Checked = false;
            t_prevanm1.Text = "";
            t_prevanm2.Text = "";
            t_prevanm3.Text = "";
            t_distance.Text = "";
            t_direction.Value = 0;
            t_condition.SelectedIndex = -1;
            t_length.Value = 0;
            t_btnpress.Value = 0;

            t_hitboxid.Text = "";
            t_dmgid.Text = "";
            t_hiteffect.SelectedIndex = -1;
            t_dmgcond.SelectedIndex = -1;
            t_function.SelectedIndex = -1;
            t_timing.Value = 0;
            t_param1.Value = 0;
            t_param2.Value = 0;
            t_dmgamount.Value = 0;
            t_pushamount.Value = 0;
            t_vertpushamount.Value = 0;
            t_hits.Value = 0;

            ReadPlAnm(currentVer, actualIndex);
        }

        List<byte> attackData = new List<byte>();
        List<byte[]> movSection = new List<byte[]>();
        public void ReadPlAnm(int sectionindex, int planm)
        {
            byte[] actualSection = plAnmList[sectionindex][planm];

            // Clear old animations
            attackData.Clear();
            mov_list.Items.Clear();

            int index = 0;
            int start = 0;

            index = start + 0x00;
            string m_planm = Main.b_ReadString(actualSection, index);
            if (m_planm == "") m_planm = "[EMPTY]";
            t_planm.Text = m_planm;

            index = start + 0x20;
            string m_anim = Main.b_ReadString(actualSection, index);
            t_anm.Text = m_anim;

            index = start + 0x50;
            byte m_movcount = actualSection[index];

            index = start + 0x56;
            byte sectionload = actualSection[index];
            t_loadsection.Value = sectionload;

            index = start + 0x58;
            byte cubeman = actualSection[index];
            t_1cmn.Checked = (cubeman == 1);

            index = start + 0x5A;
            bool flag1 = (actualSection[index] == 2);
            t_flag1.Checked = flag1;

            index = start + 0x5C;
            bool flag2 = (actualSection[index] == 1);
            t_flag2.Checked = flag2;

            index = start + 0x5E;
            bool flag3 = (actualSection[index] == 1);
            t_flag3.Checked = flag3;

            index = start + 0x60;
            bool flag4 = (actualSection[index] == 1);
            t_flag4.Checked = flag4;

            index = start + 0x62;
            // distance

            index = start + 0x68;
            byte direction = actualSection[index];
            t_direction.Value = direction;

            index = start + 0x6C;
            byte condition = actualSection[index];
            t_condition.SelectedIndex = condition;
            //MessageBox.Show(index.ToString("X2"));

            index = start + 0x6E;
            int atttime = actualSection[index] * 0x1 + actualSection[index + 0x1] * 0x100;
            t_length.Value = atttime;

            index = start + 0x70;
            int btnpress = actualSection[index] * 0x1 + actualSection[index + 0x1] * 0x100;
            t_btnpress.Value = btnpress;

            index = start + 0x74;
            string planm1 = Main.b_ReadString(actualSection, index);
            t_prevanm1.Text = planm1;

            index = start + 0x94;
            string planm2 = Main.b_ReadString(actualSection, index);
            t_prevanm2.Text = planm2;

            index = start + 0xB4;
            string planm3 = Main.b_ReadString(actualSection, index);
            t_prevanm3.Text = planm3;

            for(int x = 0; x < movementList[sectionindex][planm].Count; x++)
            {
                mov_list.Items.Add("Section " + x.ToString());
            }
        }

        private void mov_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            int actualindex = mov_list.SelectedIndex;
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;

            if (fileOpen == false || actualindex == -1) return;

            t_hitboxid.Text = "";
            t_dmgid.Text = "";
            t_hiteffect.SelectedIndex = -1;
            t_dmgcond.SelectedIndex = -1;
            t_function.SelectedIndex = -1;
            t_timing.Value = 0;
            t_param1.Value = 0;
            t_param2.Value = 0;
            t_param3.Value = 0;
            t_dmgamount.Value = 0;
            t_pushamount.Value = 0;
            t_vertpushamount.Value = 0;
            t_hits.Value = 0;

            ReadMovementSection(movementList[actualver][actualanm][actualindex]);
        }
        
        public void ReadMovementSection(byte[] section)
        {
            int len = section.Length;
            int index = 0;

            string hit = Main.b_ReadString(section, index);
            t_hitboxid.Text = hit;

            index = 0x20;
            byte timing = Main.b_ReadByteArray(section, index, 1)[0];
            t_timing.Value = timing;

            index = 0x22;
            int function = section[index] * 0x1 + section[index + 1] * 0x100;
            if (function > t_function.Items.Count) function = 0;
            t_function.SelectedIndex = function;

            index = 0x24;
            byte param1 = Main.b_ReadByteArray(section, index, 1)[0];
            t_param1.Value = param1;

            index = 0x26;
            byte param2 = Main.b_ReadByteArray(section, index, 1)[0];
            t_param2.Value = param2;

            index = 0x2C;
            float param3 = Main.b_ReadFloat(section, index);
            t_param3.Value = (decimal)param3;

            if (len == 0xA0)
            {
                index = 0x40;
                string damageid = Main.b_ReadString(section, index);
                t_dmgid.Text = damageid;

                index = 0x82;
                int selectedhit = section[index];
                if (selectedhit == 65535) selectedhit = -1;
                t_hiteffect.SelectedIndex = selectedhit;

                index = 0x86;
                int selectedmg = section[index];
                if (selectedmg == 65535) selectedmg = -1;
                t_dmgcond.SelectedIndex = selectedmg;

                index = 0x88;
                t_dmgamount.Value = (decimal)Main.b_ReadFloat(section, index);

                index = 0x90;
                t_pushamount.Value = (decimal)Main.b_ReadFloat(section, index);

                index = 0x94;
                t_vertpushamount.Value = (decimal)Main.b_ReadFloat(section, index);

                index = 0x98;
                t_hits.Value = section[index];
            }
        }

        private void t_secadd_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;

            if (fileOpen == false || actualver == -1 || actualanm == -1) return;

            byte[] newsec = new byte[0x40];
            movementList[actualver][actualanm].Add(newsec);

            // Replace movement count
            plAnmList[actualver][actualanm][0x50] = (byte)movementList[actualver][actualanm].Count;

            mov_list.Items.Add("Section " + mov_list.Items.Count.ToString());
        }

        private void t_secdup_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;
            int actualsec = mov_list.SelectedIndex;

            if (fileOpen == false || actualver == -1 || actualanm == -1 || actualsec == -1) return;

            byte[] newsec = movementList[actualver][actualanm][actualsec];
            movementList[actualver][actualanm].Add(newsec.ToList().ToArray());

            // Replace movement count
            plAnmList[actualver][actualanm][0x50] = (byte)movementList[actualver][actualanm].Count;

            mov_list.Items.Add("Section " + mov_list.Items.Count.ToString());
        }

        private void t_secdel_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;
            int actualsec = mov_list.SelectedIndex;

            if (fileOpen == false || actualver == -1 || actualanm == -1 || actualsec == -1) return;

            if (actualsec == mov_list.Items.Count - 1) mov_list.SelectedIndex--;
            int newselection = mov_list.SelectedIndex;

            movementList[actualver][actualanm].RemoveAt(actualsec);
            mov_list.Items.RemoveAt(actualsec);
            mov_list.SelectedIndex = newselection;

            // Replace movement count
            plAnmList[actualver][actualanm][0x50] = (byte)movementList[actualver][actualanm].Count;
        }

        private void t_saveanm_Click(object sender, EventArgs e)
        {
            int ver = listBox1.SelectedIndex;
            int anm = anm_list.SelectedIndex;
            if (fileOpen == false || ver == -1 || anm == -1) return;

            // Replace pl_anm
            plAnmList[ver][anm] = Main.b_ReplaceBytes(plAnmList[ver][anm], new byte[0x20], 0);
            plAnmList[ver][anm] = Main.b_ReplaceString(plAnmList[ver][anm], t_planm.Text, 0);

            // Replace animation
            plAnmList[ver][anm] = Main.b_ReplaceBytes(plAnmList[ver][anm], new byte[0x20], 0x20);
            plAnmList[ver][anm] = Main.b_ReplaceString(plAnmList[ver][anm], t_anm.Text, 0x20);

            // Replace movement count
            plAnmList[ver][anm][0x50] = (byte)movementList[ver][anm].Count;

            // Replace loading section
            plAnmList[ver][anm][0x56] = (byte)t_loadsection.Value;

            // Replace cubeman
            plAnmList[ver][anm][0x58] = Convert.ToByte(t_1cmn.Checked);

            // Replace flags
            byte flg1 = 2;
            
            if (t_flag1.Checked == false) flg1 = 0;
            plAnmList[ver][anm][0x5A] = flg1;
            plAnmList[ver][anm][0x5C] = Convert.ToByte(t_flag2.Checked);
            plAnmList[ver][anm][0x5E] = Convert.ToByte(t_flag3.Checked);
            plAnmList[ver][anm][0x60] = Convert.ToByte(t_flag4.Checked);

            // Replace distance
            // code here

            // Replace direction
            plAnmList[ver][anm][0x68] = (byte)t_direction.Value;

            // Replace conditions and timing
            plAnmList[ver][anm][0x6C] = (byte)t_condition.SelectedIndex;

            byte[] lengthbytes = BitConverter.GetBytes((int)t_length.Value);
            plAnmList[ver][anm][0x6E] = lengthbytes[0];
            plAnmList[ver][anm][0x6F] = lengthbytes[1];

            byte[] btnbytes = BitConverter.GetBytes((int)t_btnpress.Value);
            plAnmList[ver][anm][0x70] = btnbytes[0];
            plAnmList[ver][anm][0x71] = btnbytes[1];

            // Replace prev pl_anms
            plAnmList[ver][anm] = Main.b_ReplaceBytes(plAnmList[ver][anm], new byte[0x20], 0x74);
            plAnmList[ver][anm] = Main.b_ReplaceString(plAnmList[ver][anm], t_prevanm1.Text, 0x74);

            plAnmList[ver][anm] = Main.b_ReplaceBytes(plAnmList[ver][anm], new byte[0x20], 0x94);
            plAnmList[ver][anm] = Main.b_ReplaceString(plAnmList[ver][anm], t_prevanm2.Text, 0x94);

            plAnmList[ver][anm] = Main.b_ReplaceBytes(plAnmList[ver][anm], new byte[0x20], 0xB4);
            plAnmList[ver][anm] = Main.b_ReplaceString(plAnmList[ver][anm], t_prevanm3.Text, 0xB4);

            anm_list.Items[anm] = t_planm.Text;
        }

        private void t_savesec_Click(object sender, EventArgs e)
        {
            int ind = mov_list.SelectedIndex;
            int ver = listBox1.SelectedIndex;
            int anm = anm_list.SelectedIndex;
            if (fileOpen == false || ind == -1 || ver == -1 || anm == -1) return;

            // Replace hitbox id
            movementList[ver][anm][ind] = Main.b_ReplaceBytes(movementList[ver][anm][ind], new byte[0x20], 0);
            movementList[ver][anm][ind] = Main.b_ReplaceString(movementList[ver][anm][ind], t_hitboxid.Text, 0);

            // Replace timing
            movementList[ver][anm][ind][0x20] = (byte)t_timing.Value;

            // Replace function
            byte[] funct_bytes = BitConverter.GetBytes(t_function.SelectedIndex);
            movementList[ver][anm][ind][0x22] = funct_bytes[0];
            movementList[ver][anm][ind][0x23] = funct_bytes[1];

            // Replace parameters
            movementList[ver][anm][ind][0x24] = (byte)t_param1.Value;
            movementList[ver][anm][ind][0x26] = (byte)t_param2.Value;

            // Param 3
            byte[] floatparam3 = BitConverter.GetBytes(Convert.ToSingle(t_param3.Value));
            movementList[ver][anm][ind] = Main.b_ReplaceBytes(movementList[ver][anm][ind], floatparam3, 0x2C);

            int function = t_function.SelectedIndex;
            int newLength = 0x40;

            switch (function)
            {
                case 0x83:
                    if (t_dmgid.Text == "SPSKILL_END") newLength = 0xA0;
                    break;
                case 0xC1:
                case 0xC3:
                case 0xC6:
                case 0xC8:
                case 0xCA:
                case 0xD1:
                case 0xD3:
                case 0xD5:
                case 0xD7:
                case 0xD9:
                    newLength = 0xA0;
                    break;
                case 0xA0:
                case 0xA1:
                case 0xA2:
                case 0xA3:
                case 0xA4:
                case 0xA5:
                    if (t_dmgid.Text.Length > 7 && t_dmgid.Text.Substring(0, 7) == "SKL_ATK") newLength = 0xA0;
                    break;
            }

            // Transform to 0x40 section if there's no damage id
            if (t_dmgid.Text == "" && newLength == 0x40)
            {
                if (movementList[ver][anm][ind].Length > 0x40)
                {
                    int addbytes = 0xA0 - movementList[ver][anm][ind].Length;
                    byte[] newMovement = new byte[0x40];
                    
                    for(int x = 0; x < 0x40; x++)
                    {
                        newMovement[x] = movementList[ver][anm][ind][x];
                    }

                    movementList[ver][anm][ind] = newMovement;
                }
            }
            else
            {
                // Otherwise, transform to 0xA0 section and add data
                if (movementList[ver][anm][ind].Length < 0xA0)
                {
                    int addbytes = 0xA0 - movementList[ver][anm][ind].Length;
                    movementList[ver][anm][ind] = Main.b_AddBytes(movementList[ver][anm][ind], new byte[addbytes]);
                }

                // Replace damage_id
                movementList[ver][anm][ind] = Main.b_ReplaceBytes(movementList[ver][anm][ind], new byte[0x20], 0x40);
                movementList[ver][anm][ind] = Main.b_ReplaceString(movementList[ver][anm][ind], t_dmgid.Text, 0x40);

                // Replace hit effect and sound
                movementList[ver][anm][ind][0x82] = (byte)t_hiteffect.SelectedIndex;

                // Replace damage condition
                movementList[ver][anm][ind][0x86] = (byte)t_dmgcond.SelectedIndex;

                // Replace damage amount
                byte[] dmgamount = BitConverter.GetBytes(Convert.ToSingle(t_dmgamount.Value));
                movementList[ver][anm][ind] = Main.b_ReplaceBytes(movementList[ver][anm][ind], dmgamount, 0x88);

                // Replace push away amount
                byte[] pushaway = BitConverter.GetBytes(Convert.ToSingle(t_pushamount.Value));
                movementList[ver][anm][ind] = Main.b_ReplaceBytes(movementList[ver][anm][ind], pushaway, 0x90);

                // Replace rise push away amount
                byte[] rise = BitConverter.GetBytes(Convert.ToSingle(t_vertpushamount.Value));
                movementList[ver][anm][ind] = Main.b_ReplaceBytes(movementList[ver][anm][ind], rise, 0x94);

                // Replace hit count
                movementList[ver][anm][ind][0x98] = (byte)t_hits.Value;
            }
        }

        public byte[] GenerateFile()
        {
            byte[] newBytes = new byte[0];

            newBytes = Main.b_AddBytes(newBytes, fileBytes, 0, 0, verList[0]);

            int verCount = plAnmList.Count;

            for (int x = 0; x < verCount; x++)
            {
                int countLength = newBytes.Length;

                // Add header of ver
                byte[] header = new byte[0x40];
                header[0x00] = 0x76;
                header[0x01] = 0x65;
                header[0x02] = 0x72;
                header[0x03] = 0x30;
                header[0x04] = 0x2E;
                header[0x05] = 0x30;
                header[0x06] = 0x30;
                header[0x07] = 0x30;
                header[0x30] = (byte)plAnmList[x].Count;
                newBytes = Main.b_AddBytes(newBytes, header);

                // Add each pl_anm
                for (int y = 0; y < plAnmList[x].Count; y++)
                {
                    newBytes = Main.b_AddBytes(newBytes, plAnmList[x][y]);

                    for (int z = 0; z < movementList[x][y].Count; z++)
                    {
                        newBytes = Main.b_AddBytes(newBytes, movementList[x][y][z]);
                    }
                }

                int totalLen = newBytes.Length - countLength;
                newBytes = Main.b_ReplaceBytes(newBytes, BitConverter.GetBytes(totalLen), countLength - 0x04, 1);
                newBytes = Main.b_ReplaceBytes(newBytes, BitConverter.GetBytes(totalLen + 4), countLength - 0x10, 1);

                int start = verList[x];
                int end = start + verLength[x];
                int next = fileBytes.Length;

                if (x < verCount - 1) next = verList[x + 1];

                int totalBytesToAdd = next - end;
                byte[] toaddempty = new byte[totalBytesToAdd];

                int actual = newBytes.Length;
                newBytes = Main.b_AddBytes(newBytes, toaddempty);

                for (int y = 0; y < totalBytesToAdd; y++)
                {
                    newBytes[actual + y] = fileBytes[end + y];
                }
            }

            return newBytes;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(filePath + ".backup")) File.Delete(filePath + ".backup");
            File.Copy(filePath, filePath + ".backup");
            File.WriteAllBytes(filePath, GenerateFile());
            MessageBox.Show("File saved to " + filePath);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.ShowDialog();

            if(s.FileName != "")
            {
                filePath = s.FileName;
                File.WriteAllBytes(filePath, GenerateFile());
                MessageBox.Show("File saved to " + filePath);
            }

            //byte[] bottomheader = new byte[0x10];
            //bottomheader[7] = 0x6;
            //bottomheader[9] = 0x63;
            //newBytes = Main.b_AddBytes(newBytes, bottomheader);
        }

        private void t_add_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;

            if (fileOpen == false || actualver == -1) return;

            byte[] newsec = new byte[0xD4];
            plAnmList[actualver].Add(newsec);
            movementList[actualver].Add(new List<byte[]>());

            anm_list.Items.Add("[EMPTY]");
        }

        private void t_dup_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;

            if (fileOpen == false || actualver == -1 || actualanm == -1) return;

            byte[] newsec = plAnmList[actualver][actualanm].ToList().ToArray();
            plAnmList[actualver].Add(newsec);
            movementList[actualver].Add(movementList[actualver][actualanm].ToList());

            /*for (int x = 0; x < movementList[actualver][actualanm].Count; x++)
            {
                byte[] mov = new byte[movementList[actualver][actualanm][x].Length];
                for (int y = 0; y < movementList[actualver][actualanm][x].Length; y++)
                {
                    mov[y] = movementList[actualver][actualanm][x][y];
                }
                movementList[actualver][actualanm].Add(mov);
            }*/

            anm_list.Items.Add(Main.b_ReadString(newsec, 0));
        }

        private void t_del_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;

            if (fileOpen == false || actualver == -1 || actualanm == -1) return;

            if (listBox1.SelectedIndex == listBox1.Items.Count - 1) listBox1.SelectedIndex--;
            int newsel = listBox1.SelectedIndex;
            listBox1.SelectedIndex = newsel;

            anm_list.Items.RemoveAt(actualanm);
            plAnmList[actualver].RemoveAt(actualanm);
            movementList[actualver].RemoveAt(actualanm);
        }

        void CloseFile(bool message = false)
        {
            if(message)
            {
                DialogResult r = MessageBox.Show("Are you sure you want to close this file?", "", MessageBoxButtons.YesNo);

                if (r == DialogResult.No) return;
            }

            fileOpen = false;
            filePath = "";
            listBox1.Items.Clear();
            anm_list.Items.Clear();
            mov_list.Items.Clear();

            fileBytes = new byte[0];

            verSection.Clear();
            anmCount.Clear();
            anmSection.Clear();
            verList.Clear();
            verLength.Clear();
            plAnmList.Clear();
            movementList.Clear();

            t_planm.Text = "";
            t_anm.Text = "";
            t_loadsection.Value = 0;
            t_1cmn.Checked = false;
            t_flag1.Checked = false;
            t_flag2.Checked = false;
            t_flag3.Checked = false;
            t_flag4.Checked = false;
            t_prevanm1.Text = "";
            t_prevanm2.Text = "";
            t_prevanm3.Text = "";
            t_distance.Text = "";
            t_direction.Value = 0;
            t_condition.SelectedIndex = -1;
            t_length.Value = 0;
            t_btnpress.Value = 0;

            t_hitboxid.Text = "";
            t_dmgid.Text = "";
            t_hiteffect.SelectedIndex = -1;
            t_dmgcond.SelectedIndex = -1;
            t_function.SelectedIndex = -1;
            t_timing.Value = 0;
            t_param1.Value = 0;
            t_param2.Value = 0;
            t_dmgamount.Value = 0;
            t_pushamount.Value = 0;
            t_vertpushamount.Value = 0;
            t_hits.Value = 0;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fileOpen) CloseFile(true);
        }

        void SwitchPlAnmAndMove(int actualver, int actualanm, int switchnum)
        {
            // Switch pl anm
            byte[] thisplanm = plAnmList[actualver][actualanm];
            plAnmList[actualver][actualanm] = plAnmList[actualver][switchnum];
            plAnmList[actualver][switchnum] = thisplanm;

            string planmname = anm_list.Items[actualanm].ToString();
            anm_list.Items[actualanm] = anm_list.Items[switchnum].ToString();
            anm_list.Items[switchnum] = planmname;

            // Switch movementList
            List<byte[]> anmmov = movementList[actualver][actualanm];
            movementList[actualver][actualanm] = movementList[actualver][switchnum];
            movementList[actualver][switchnum] = anmmov;

            anm_list.SelectedIndex = switchnum;
        }

        private void b_moveuppl_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;

            if (fileOpen == false || actualver == -1 || actualanm == -1) return;

            if(actualanm != 0)
            {
                int switchnum = actualanm - 1;

                SwitchPlAnmAndMove(actualver, actualanm, switchnum);
            }
        }

        private void b_movedownpl_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;

            if (fileOpen == false || actualver == -1 || actualanm == -1) return;

            if (actualanm != anm_list.Items.Count - 1)
            {
                int switchnum = actualanm + 1;

                SwitchPlAnmAndMove(actualver, actualanm, switchnum);
            }
        }

        void SwitchMove(int actualver, int actualanm, int actualmov, int switchmov)
        {
            // Switch movementList
            byte[] mov = movementList[actualver][actualanm][actualmov];
            movementList[actualver][actualanm][actualmov] = movementList[actualver][actualanm][switchmov];
            movementList[actualver][actualanm][switchmov] = mov;

            mov_list.SelectedIndex = switchmov;
        }

        private void b_moveupmov_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;
            int actualmov = mov_list.SelectedIndex;

            if (fileOpen == false || actualver == -1 || actualanm == -1) return;

            if (actualmov != 0)
            {
                int switchnum = actualmov - 1;

                SwitchMove(actualver, actualanm, actualmov, switchnum);
            }
        }

        private void b_movedownmov_Click(object sender, EventArgs e)
        {
            int actualver = listBox1.SelectedIndex;
            int actualanm = anm_list.SelectedIndex;
            int actualmov = mov_list.SelectedIndex;

            if (fileOpen == false || actualver == -1 || actualanm == -1) return;

            if (actualmov < mov_list.Items.Count - 1)
            {
                int switchnum = actualmov + 1;

                SwitchMove(actualver, actualanm, actualmov, switchnum);
            }
        }

        private void setCubemanToEveryANMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ver = listBox1.SelectedIndex;
            if (ver == -1) return;

            for(int x = 0; x < plAnmList[ver].Count; x++)
            {
                plAnmList[ver][x][0x58] = 1;
            }

            if (plAnmList[ver].Count > 0) t_1cmn.Checked = true;
        }

        private void setCubemanOffInAllPlanmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ver = listBox1.SelectedIndex;
            if (ver == -1) return;

            for (int x = 0; x < plAnmList[ver].Count; x++)
            {
                plAnmList[ver][x][0x58] = 0;
            }

            if(plAnmList[ver].Count > 0) t_1cmn.Checked = false;
        }
    }
}
