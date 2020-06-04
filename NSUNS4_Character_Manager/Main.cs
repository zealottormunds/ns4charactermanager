using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_Character_Manager
{
	public class Main : Form
	{
		private IContainer components = null;
		private Button button1;
		private Button button2;
		private Button button3;
		private Button button4;
		private Button button5;
		private Button button6;
		private Button button7;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem optionsToolStripMenuItem;
		private ToolStripMenuItem setDefaultPathsToolStripMenuItem;
		private ToolStripMenuItem aboutToolStripMenuItem;
		private ToolStripMenuItem setCharacodebinxfbinDefaultPathToolStripMenuItem;
		private ToolStripMenuItem setDuelPlayerParamDefaultPathToolStripMenuItem;
		private ToolStripMenuItem setPlayerSettingParamDefaultPathToolStripMenuItem;
		private ToolStripMenuItem pathToUnlockCharaTotalToolStripMenuItem;
		private ToolStripMenuItem pathToCharacterSelectParamToolStripMenuItem;
		private Label label1;
        private Button button8;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem addCostumeToolStripMenuItem;
        private ToolStripMenuItem addNewCharacterToolStripMenuItem;
        private Label label2;

        public string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "\\config.txt";
        public string datawin32Path = "[null]";
        public string chaPath = "[null]";
        public string dppPath = "[null]";
        public string pspPath = "[null]";
        public string unlPath = "[null]";
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private ToolStripMenuItem pathToDatawin32ToolStripMenuItem;
        private Label label3;
        private Button button13;
        public string cspPath = "[null]";

        public Main()
		{
			InitializeComponent();
           
            if(File.Exists(ConfigPath) == false)
            {
                CreateConfig();
            }
            else
            {
                LoadConfig();
            }
		}

        void CreateConfig()
        {
            List<string> cfg = new List<string>();
            cfg.Add("[null]");
            cfg.Add("[null]");
            cfg.Add("[null]");
            cfg.Add("[null]");
            cfg.Add("[null]");
            cfg.Add("[null]");
            File.WriteAllLines(ConfigPath, cfg.ToArray());
            MessageBox.Show("Config file created.");
        }

        void SaveConfig()
        {
            List<string> cfg = new List<string>();
            cfg.Add(datawin32Path);
            cfg.Add(chaPath);
            cfg.Add(dppPath);
            cfg.Add(pspPath);
            cfg.Add(unlPath);
            cfg.Add(cspPath);
            File.WriteAllLines(ConfigPath, cfg.ToArray());
            MessageBox.Show("Config file saved.");
        }

        void LoadConfig()
        {
            string[] cfg = File.ReadAllLines(ConfigPath);
            if (cfg.Length > 0) datawin32Path = cfg[0];
            if (cfg.Length > 1) chaPath = cfg[1];
            if (cfg.Length > 2) dppPath = cfg[2];
            if (cfg.Length > 3) pspPath = cfg[3];
            if (cfg.Length > 4) unlPath = cfg[4];
            if (cfg.Length > 5) cspPath = cfg[5];
            MessageBox.Show("Loaded paths.");
        }

        // Add costume
        private void addCostumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dppPath == "[null]" || pspPath == "[null]" || cspPath == "[null]")
            {
                MessageBox.Show("Please select your default paths (dpp, psp and csp) to use this function.");
                return;
            }

            Tool_AddCostume add = new Tool_AddCostume(this);
            add.ShowDialog();
        }

        private void addNewCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dppPath == "[null]" || pspPath == "[null]" || cspPath == "[null]" || chaPath == "[null]")
            {
                MessageBox.Show("Please select your default paths (dpp, psp, csp and characode) to use this function.");
                return;
            }

            Tool_AddCharacter add = new Tool_AddCharacter(this);
            add.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
		{
			Tool_CharacodeEditor characodeeditor = new Tool_CharacodeEditor();
			characodeeditor.ShowDialog();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			Tool_UnlockCharaTotalEditor unlockCharaTotalEditor = new Tool_UnlockCharaTotalEditor();
			unlockCharaTotalEditor.ShowDialog();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Tool_PlayerSettingParamEditor playerSettingParamEditor = new Tool_PlayerSettingParamEditor();
			playerSettingParamEditor.ShowDialog();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Tool_RosterEditor tool_RosterEditor = new Tool_RosterEditor();
			tool_RosterEditor.ShowDialog();
		}

		public static byte[] b_ReadByteArray(byte[] actual, int index, int count)
		{
			List<byte> a = new List<byte>();
			for (int x = 0; x < count; x++)
			{
				a.Add(actual[index + x]);
			}
			return a.ToArray();
		}

		public static int b_byteArrayToInt(byte[] actual)
		{
			int a = 0;
			return actual[0] + actual[1] * 256 + actual[2] * 65536 + actual[3] * 16777216;
		}

		public static int b_byteArrayToIntRev(byte[] actual)
		{
			int a = 0;
			return actual[3] + actual[2] * 256 + actual[1] * 65536 + actual[0] * 16777216;
		}

        public static int b_ReadInt(byte[] fileBytes, int index)
        {
            return Main.b_byteArrayToInt(Main.b_ReadByteArray(fileBytes, index, 4));
        }

        public static int b_ReadIntRev(byte[] fileBytes, int index)
        {
            return Main.b_byteArrayToIntRev(Main.b_ReadByteArray(fileBytes, index, 4));
        }

		public static float b_ReadFloat(byte[] actual, int index)
		{
			float a = -1f;
			return BitConverter.ToSingle(actual, index);
		}

		public static string b_ReadString(byte[] actual, int index, int count = -1)
		{
			string a = "";
			if (count == -1)
			{
				for (int x2 = index; x2 < actual.Length; x2++)
				{
					if (actual[x2] != 0)
					{
						string str = a;
						char c = (char)actual[x2];
						a = str + c;
					}
					else
					{
						x2 = actual.Length;
					}
				}
			}
			else
			{
				for (int x = index; x < count; x++)
				{
					string str2 = a;
					char c = (char)actual[x];
					a = str2 + c;
				}
			}
			return a;
		}

		public static byte[] b_ReplaceBytes(byte[] actual, byte[] bytesToReplace, int Index, int Invert = 0)
		{
			if (Invert == 0)
			{
				for (int x2 = 0; x2 < bytesToReplace.Length; x2++)
				{
					actual[Index + x2] = bytesToReplace[x2];
				}
			}
			else
			{
				for (int x = 0; x < bytesToReplace.Length; x++)
				{
					actual[Index + x] = bytesToReplace[bytesToReplace.Length - 1 - x];
				}
			}
			return actual;
		}

		public static byte[] b_ReplaceString(byte[] actual, string str, int Index, int Count = -1)
		{
			if (Count == -1)
			{
				for (int x2 = 0; x2 < str.Length; x2++)
				{
					actual[Index + x2] = (byte)str[x2];
				}
			}
			else
			{
				for (int x = 0; x < Count; x++)
				{
					if (str.Length > x)
					{
						actual[Index + x] = (byte)str[x];
					}
					else
					{
						actual[Index + x] = 0;
					}
				}
			}
			return actual;
		}

		public static byte[] b_AddBytes(byte[] actual, byte[] bytesToAdd, int Reverse = 0, int index = 0, int count = -1)
		{
			List<byte> a = actual.ToList();
			if (Reverse == 0)
			{
                if (count == -1) count = bytesToAdd.Length;
                for (int x = index; x < index + count; x++)
				{
					a.Add(bytesToAdd[x]);
				}
			}
			else
			{
                if (count == -1) count = bytesToAdd.Length;
                for (int x = index; x < index + count; x++)
				{
					a.Add(bytesToAdd[bytesToAdd.Length - 1 - x]);
				}
			}
			return a.ToArray();
		}

		public static byte[] b_AddInt(byte[] actual, int _num)
		{
			List<byte> a = actual.ToList();
			byte[] b = BitConverter.GetBytes(_num);
			for (int x = 0; x < 4; x++)
			{
				a.Add(b[x]);
			}
			return a.ToArray();
		}

		public static byte[] b_AddString(byte[] actual, string _str, int count = -1)
		{
			List<byte> a = actual.ToList();
			for (int x2 = 0; x2 < _str.Length; x2++)
			{
				a.Add((byte)_str[x2]);
			}
			for (int x = _str.Length; x < count; x++)
			{
				a.Add(0);
			}
			return a.ToArray();
		}

		public static byte[] b_AddFloat(byte[] actual, float f)
		{
			List<byte> a = actual.ToList();
			byte[] floatBytes = BitConverter.GetBytes(f);
			for (int x = 0; x < 4; x++)
			{
				a.Add(floatBytes[x]);
			}
			return a.ToArray();
		}

        public static int b_FindBytes(byte[] actual, byte[] bytes, int index = 0)
        { 
            int actualIndex = index;
            byte[] actualBytes = new byte[bytes.Length];
            bool found = false;
            bool f = false;

            int foundIndex = -1;

            for(int a = actualIndex; a < (actual.Length - bytes.Length); a++)
            {
                f = true;

                for(int x = 0; x < bytes.Length; x++)
                {
                    actualBytes[x] = actual[a + x];

                    if(actualBytes[x] != bytes[x])
                    {
                        x = bytes.Length;
                        f = false;
                    }
                }

                if (f)
                {
                    found = true;
                    foundIndex = a;
                    a = actual.Length;
                }
            }

            /*while (found == false && actualIndex < (actual.Length - bytes.Length))
            {
                for(int x = 0; x < bytes.Length; x++)
                {
                    actualBytes[x] = actual[actualIndex + x];

                    if(actualBytes[x] != bytes[x])
                    {
                        x = bytes.Length;
                        actualIndex = actualIndex + 1;
                    }
                }

                if(actualBytes == bytes)
                {
                    found = true;
                }
            }*/

            return foundIndex;
        }

        public static List<int> b_FindBytesList(byte[] actual, byte[] bytes, int index = 0)
        {
            int actualIndex = index;
            List<int> indexes = new List<int>();

            int lastFound = 0;
            while(lastFound != -1)
            {
                lastFound = b_FindBytes(actual, bytes, actualIndex);
                if (lastFound != -1)
                {
                    actualIndex = lastFound + 1;
                    indexes.Add(lastFound);
                }
            }

            return indexes;
        }

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Naruto: Storm 4 Character Manager made by Zealot Tormunds.");
		}

        public void SetPath(string f)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();

            switch (f)
            {
                case "characode":
                    chaPath = o.FileName;
                    break;
                case "dpp":
                    dppPath = o.FileName;
                    break;
                case "psp":
                    pspPath = o.FileName;
                    break;
                case "unlock":
                    unlPath = o.FileName;
                    break;
                case "csp":
                    cspPath = o.FileName;
                    break;
            }

            SaveConfig();
        }

        private void pathToDatawin32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog c = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            c.IsFolderPicker = true;
            
            if(c.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                datawin32Path = c.FileName;
            }
        }

        private void setCharacodebinxfbinDefaultPathToolStripMenuItem_Click(object sender, EventArgs e)
		{
            SetPath("characode");
		}

        private void setDuelPlayerParamDefaultPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPath("dpp");
        }

        private void setPlayerSettingParamDefaultPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPath("psp");
        }

        private void pathToUnlockCharaTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPath("unlock");
        }

        private void pathToCharacterSelectParamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPath("csp");
        }

        private void Main_Load(object sender, EventArgs e)
		{
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Tool_DuelPlayerParamEditor t = new Tool_DuelPlayerParamEditor();
			t.ShowDialog();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDefaultPathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathToDatawin32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCharacodebinxfbinDefaultPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDuelPlayerParamDefaultPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPlayerSettingParamDefaultPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathToUnlockCharaTotalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathToCharacterSelectParamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCostumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(213, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Character ID Manager\r\n(Characode.bin.xfbin)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(213, 36);
            this.button2.TabIndex = 2;
            this.button2.Text = "PlayerSettingParam Editor";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(233, 109);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(217, 59);
            this.button3.TabIndex = 3;
            this.button3.Text = "Character Roster Manager\r\n(CharacterSelectParam.xfbin)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(14, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(213, 36);
            this.button4.TabIndex = 4;
            this.button4.Text = "DuelPlayerParam Editor";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(233, 46);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(217, 59);
            this.button5.TabIndex = 5;
            this.button5.Text = "Character Unlocks Editor\r\n(UnlockCharaTotal.xfbin)";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(14, 357);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(436, 30);
            this.button6.TabIndex = 6;
            this.button6.Text = "Import (.ns4) file";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(233, 393);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(217, 30);
            this.button7.TabIndex = 7;
            this.button7.Text = "Export (.ns4) costume";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(464, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDefaultPathsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // setDefaultPathsToolStripMenuItem
            // 
            this.setDefaultPathsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pathToDatawin32ToolStripMenuItem,
            this.setCharacodebinxfbinDefaultPathToolStripMenuItem,
            this.setDuelPlayerParamDefaultPathToolStripMenuItem,
            this.setPlayerSettingParamDefaultPathToolStripMenuItem,
            this.pathToUnlockCharaTotalToolStripMenuItem,
            this.pathToCharacterSelectParamToolStripMenuItem});
            this.setDefaultPathsToolStripMenuItem.Name = "setDefaultPathsToolStripMenuItem";
            this.setDefaultPathsToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.setDefaultPathsToolStripMenuItem.Text = "Set default paths for mod installs";
            // 
            // pathToDatawin32ToolStripMenuItem
            // 
            this.pathToDatawin32ToolStripMenuItem.Name = "pathToDatawin32ToolStripMenuItem";
            this.pathToDatawin32ToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.pathToDatawin32ToolStripMenuItem.Text = "Path to data_win32";
            this.pathToDatawin32ToolStripMenuItem.Click += new System.EventHandler(this.pathToDatawin32ToolStripMenuItem_Click);
            // 
            // setCharacodebinxfbinDefaultPathToolStripMenuItem
            // 
            this.setCharacodebinxfbinDefaultPathToolStripMenuItem.Name = "setCharacodebinxfbinDefaultPathToolStripMenuItem";
            this.setCharacodebinxfbinDefaultPathToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.setCharacodebinxfbinDefaultPathToolStripMenuItem.Text = "Path to characode";
            this.setCharacodebinxfbinDefaultPathToolStripMenuItem.Click += new System.EventHandler(this.setCharacodebinxfbinDefaultPathToolStripMenuItem_Click);
            // 
            // setDuelPlayerParamDefaultPathToolStripMenuItem
            // 
            this.setDuelPlayerParamDefaultPathToolStripMenuItem.Name = "setDuelPlayerParamDefaultPathToolStripMenuItem";
            this.setDuelPlayerParamDefaultPathToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.setDuelPlayerParamDefaultPathToolStripMenuItem.Text = "Path to duelPlayerParam";
            this.setDuelPlayerParamDefaultPathToolStripMenuItem.Click += new System.EventHandler(this.setDuelPlayerParamDefaultPathToolStripMenuItem_Click);
            // 
            // setPlayerSettingParamDefaultPathToolStripMenuItem
            // 
            this.setPlayerSettingParamDefaultPathToolStripMenuItem.Name = "setPlayerSettingParamDefaultPathToolStripMenuItem";
            this.setPlayerSettingParamDefaultPathToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.setPlayerSettingParamDefaultPathToolStripMenuItem.Text = "Path to playerSettingParam";
            this.setPlayerSettingParamDefaultPathToolStripMenuItem.Click += new System.EventHandler(this.setPlayerSettingParamDefaultPathToolStripMenuItem_Click);
            // 
            // pathToUnlockCharaTotalToolStripMenuItem
            // 
            this.pathToUnlockCharaTotalToolStripMenuItem.Name = "pathToUnlockCharaTotalToolStripMenuItem";
            this.pathToUnlockCharaTotalToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.pathToUnlockCharaTotalToolStripMenuItem.Text = "Path to unlockCharaTotal...";
            this.pathToUnlockCharaTotalToolStripMenuItem.Click += new System.EventHandler(this.pathToUnlockCharaTotalToolStripMenuItem_Click);
            // 
            // pathToCharacterSelectParamToolStripMenuItem
            // 
            this.pathToCharacterSelectParamToolStripMenuItem.Name = "pathToCharacterSelectParamToolStripMenuItem";
            this.pathToCharacterSelectParamToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.pathToCharacterSelectParamToolStripMenuItem.Text = "Path to characterSelectParam";
            this.pathToCharacterSelectParamToolStripMenuItem.Click += new System.EventHandler(this.pathToCharacterSelectParamToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCostumeToolStripMenuItem,
            this.addNewCharacterToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // addCostumeToolStripMenuItem
            // 
            this.addCostumeToolStripMenuItem.Name = "addCostumeToolStripMenuItem";
            this.addCostumeToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.addCostumeToolStripMenuItem.Text = "Add new costume (Experimental)";
            this.addCostumeToolStripMenuItem.Click += new System.EventHandler(this.addCostumeToolStripMenuItem_Click);
            // 
            // addNewCharacterToolStripMenuItem
            // 
            this.addNewCharacterToolStripMenuItem.Name = "addNewCharacterToolStripMenuItem";
            this.addNewCharacterToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.addNewCharacterToolStripMenuItem.Text = "Add new character (Experimental)";
            this.addNewCharacterToolStripMenuItem.Click += new System.EventHandler(this.addNewCharacterToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "For players:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "For modders";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(14, 174);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(213, 36);
            this.button8.TabIndex = 11;
            this.button8.Text = "Spcload Editor";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(14, 218);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(213, 36);
            this.button9.TabIndex = 12;
            this.button9.Text = "SkillCustomizeParam Editor";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(233, 218);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(217, 36);
            this.button10.TabIndex = 13;
            this.button10.Text = "SpSkillCustomizeParam Editor";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Enabled = false;
            this.button11.Location = new System.Drawing.Point(233, 174);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(217, 36);
            this.button11.TabIndex = 14;
            this.button11.Text = "Player_Icon Editor";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Enabled = false;
            this.button12.Location = new System.Drawing.Point(14, 393);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(213, 30);
            this.button12.TabIndex = 16;
            this.button12.Text = "Export (.ns4) character";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Misc tools";
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(14, 293);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(436, 30);
            this.button13.TabIndex = 18;
            this.button13.Text = "Xfbin Texture Replacer";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 435);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naruto: Storm 4 Character Manager";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void button8_Click(object sender, EventArgs e)
        {
            Tool_SpcloadEditor s = new Tool_SpcloadEditor();
            s.ShowDialog();
        }

        // Export .ns4 character
        private void button7_Click(object sender, EventArgs e)
        {
            Functions.Tool_ExportCostume ec = new Functions.Tool_ExportCostume();
            ec.ShowDialog();
        }

        // Import .ns4
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.ShowDialog();

            int errors = 0;
            int total = o.FileNames.Length;

            for(int a = 0; a < o.FileNames.Length; a++)
            {
                byte[] fileBytes = File.ReadAllBytes(o.FileNames[a]);
                string filetype = Main.b_ReadString(fileBytes, 0);
                
                switch(filetype)
                {
                    default:
                        MessageBox.Show("Error importing " + o.FileNames[a] + ": not a valid .ns4 file.");
                        break;
                    case "NS4CS":
                        ImportCostume(fileBytes, o.FileNames[a]);
                        break;
                }
            }

            MessageBox.Show("Finished importing files (" + (total - errors).ToString() + " without errors out of " + total.ToString() + ")");
        }

        void ImportCostume(byte[] fileBytes, string filepath)
        {
            int actualindex = 6;
            string basechar = "";
            string costname = "";
            byte[] costfile = new byte[0];

            basechar = Main.b_ReadString(fileBytes, actualindex);
            actualindex = actualindex + basechar.Length + 1;

            costname = Main.b_ReadString(fileBytes, actualindex);
            actualindex = actualindex + costname.Length + 1;

            int fileLen = Main.b_ReadInt(fileBytes, actualindex);
            actualindex = actualindex + 4;

            costfile = Main.b_ReadByteArray(fileBytes, actualindex, fileLen);
            File.WriteAllBytes(datawin32Path + "/spc/" + costname + "bod1.xfbin", costfile);

            Tool_AddCostume t = new Tool_AddCostume(this);
            t.w_base.Text = basechar;
            t.w_model.Text = costname;
            int ret = t.AddCostume();

            switch(ret)
            {
                case 1:
                    MessageBox.Show("Error importing " + filepath + ": base character " + basechar + " not found in duelPlayerParam.");
                    break;
                case 2:
                    MessageBox.Show("Error importing " + filepath + ": base character " + basechar + " has its costume list full.");
                    break;
                case 3:
                    MessageBox.Show("Error importing " + filepath + ": base character " + basechar + " not found in playerSettingParam.");
                    break;
                case 4:
                    MessageBox.Show("Error importing " + filepath + ": base character " + basechar + " not found in characterSelectParam.");
                    break;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Misc.Tool_TextureReplacer t = new Misc.Tool_TextureReplacer();
            t.ShowDialog();
        }
    }
}
