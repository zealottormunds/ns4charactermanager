using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
        private Label label2;

		public Main()
		{
			InitializeComponent();
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

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Naruto: Storm 4 Character Manager made by Zealot Tormunds.");
		}

		private void setCharacodebinxfbinDefaultPathToolStripMenuItem_Click(object sender, EventArgs e)
		{
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
            this.setCharacodebinxfbinDefaultPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDuelPlayerParamDefaultPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPlayerSettingParamDefaultPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathToUnlockCharaTotalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathToCharacterSelectParamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
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
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(14, 243);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(436, 30);
            this.button6.TabIndex = 6;
            this.button6.Text = "Import (.ns4) character";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(14, 280);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(436, 30);
            this.button7.TabIndex = 7;
            this.button7.Text = "Export (.ns4) character";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
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
            this.setCharacodebinxfbinDefaultPathToolStripMenuItem,
            this.setDuelPlayerParamDefaultPathToolStripMenuItem,
            this.setPlayerSettingParamDefaultPathToolStripMenuItem,
            this.pathToUnlockCharaTotalToolStripMenuItem,
            this.pathToCharacterSelectParamToolStripMenuItem});
            this.setDefaultPathsToolStripMenuItem.Enabled = false;
            this.setDefaultPathsToolStripMenuItem.Name = "setDefaultPathsToolStripMenuItem";
            this.setDefaultPathsToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.setDefaultPathsToolStripMenuItem.Text = "Set default paths for mod installs";
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
            // 
            // setPlayerSettingParamDefaultPathToolStripMenuItem
            // 
            this.setPlayerSettingParamDefaultPathToolStripMenuItem.Name = "setPlayerSettingParamDefaultPathToolStripMenuItem";
            this.setPlayerSettingParamDefaultPathToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.setPlayerSettingParamDefaultPathToolStripMenuItem.Text = "Path to playerSettingParam";
            // 
            // pathToUnlockCharaTotalToolStripMenuItem
            // 
            this.pathToUnlockCharaTotalToolStripMenuItem.Name = "pathToUnlockCharaTotalToolStripMenuItem";
            this.pathToUnlockCharaTotalToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.pathToUnlockCharaTotalToolStripMenuItem.Text = "Path to unlockCharaTotal...";
            // 
            // pathToCharacterSelectParamToolStripMenuItem
            // 
            this.pathToCharacterSelectParamToolStripMenuItem.Name = "pathToCharacterSelectParamToolStripMenuItem";
            this.pathToCharacterSelectParamToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.pathToCharacterSelectParamToolStripMenuItem.Text = "Path to characterSelectParam";
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
            this.label1.Location = new System.Drawing.Point(14, 224);
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
            this.button8.Size = new System.Drawing.Size(436, 41);
            this.button8.TabIndex = 11;
            this.button8.Text = "Spcload Editor";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 324);
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
    }
}
