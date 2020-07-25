using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
	public class Tool_UnlockCharaTotalEditor : Form
	{
		public bool FileOpen = false;

		public string FilePath = "";

		public byte[] FileBytes = new byte[0];

		public List<List<byte>> EntryList = new List<List<byte>>();

		public int EntryCount = 0;

		private IContainer components = null;

		private ListBox ListBox1;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem fileToolStripMenuItem;

		private ToolStripMenuItem newToolStripMenuItem;

		private ToolStripMenuItem openToolStripMenuItem;

		private ToolStripMenuItem saveToolStripMenuItem;

		private ToolStripMenuItem saveAsToolStripMenuItem;

		private ToolStripMenuItem closeToolStripMenuItem;

		private Button button1;

		private Button button2;

		private Label label1;

		private NumericUpDown numericUpDown1;

		private NumericUpDown numericUpDown2;

		public Tool_UnlockCharaTotalEditor()
		{
			InitializeComponent();
		}

		public void NewFile()
		{
			FileBytes = new byte[0];
			FilePath = "";
			EntryList = new List<List<byte>>();
			EntryCount = 0;
			ListBox1.Items.Clear();
			FileOpen = true;
		}

		public void OpenFile()
		{
			if (FileOpen)
			{
				CloseFile();
			}
			OpenFileDialog o = new OpenFileDialog();
			o.DefaultExt = "xfbin";
			o.ShowDialog();
			if (!(o.FileName != "") || !File.Exists(o.FileName))
			{
				return;
			}
			FileOpen = true;
			ListBox1.Items.Clear();
			FilePath = o.FileName;
			FileBytes = File.ReadAllBytes(FilePath);
			EntryCount = (FileBytes.Length - 300 - 20) / 12;
			EntryList = new List<List<byte>>();
			for (int x = 0; x < EntryCount; x++)
			{
				List<byte> character = new List<byte>();
				for (int a = 0; a < 12; a++)
				{
					byte b = FileBytes[300 + x * 12 + a];
					character.Add(b);
				}
				EntryList.Add(character);
				string toAdd = "";
				for (int c = 0; c < 12; c++)
				{
					toAdd = toAdd + character[c].ToString("X2") + " ";
				}
				ListBox1.Items.Add(toAdd);
			}
			MessageBox.Show("UnlockCharaTotal contains " + EntryCount + " unlock sections.");
		}

		public void AddID()
		{
			byte[] presetID = new byte[2]
			{
				(byte)numericUpDown1.Value,
				(byte)numericUpDown2.Value
			};
			byte[] sectionBytes = new byte[12]
			{
				presetID[0],
				presetID[1],
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0
			};
			EntryList.Add(sectionBytes.ToList());
			string toAdd = "";
			for (int c = 0; c < 12; c++)
			{
				toAdd = toAdd + sectionBytes[c].ToString("X2") + " ";
			}
			ListBox1.Items.Add(toAdd);
			ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
			EntryCount++;
		}

		public void RemoveID(int Index)
		{
			EntryList.RemoveAt(Index);
			if (ListBox1.SelectedIndex > 0)
			{
				ListBox1.SelectedIndex--;
			}
			else
			{
				ListBox1.ClearSelected();
			}
			ListBox1.Items.RemoveAt(Index);
			EntryCount--;
		}

		public byte[] ConvertToFile()
		{
			List<byte> file = new List<byte>();
			byte[] header = new byte[300]
			{
				78,
				85,
				67,
				67,
				0,
				0,
				0,
				121,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				228,
				0,
				0,
				0,
				3,
				0,
				121,
				0,
				0,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				59,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				33,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				30,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				48,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				0,
				110,
				117,
				99,
				99,
				67,
				104,
				117,
				110,
				107,
				78,
				117,
				108,
				108,
				0,
				110,
				117,
				99,
				99,
				67,
				104,
				117,
				110,
				107,
				66,
				105,
				110,
				97,
				114,
				121,
				0,
				110,
				117,
				99,
				99,
				67,
				104,
				117,
				110,
				107,
				80,
				97,
				103,
				101,
				0,
				110,
				117,
				99,
				99,
				67,
				104,
				117,
				110,
				107,
				73,
				110,
				100,
				101,
				120,
				0,
				0,
				98,
				105,
				110,
				95,
				108,
				101,
				47,
				120,
				54,
				52,
				47,
				117,
				110,
				108,
				111,
				99,
				107,
				67,
				104,
				97,
				114,
				97,
				84,
				111,
				116,
				97,
				108,
				46,
				98,
				105,
				110,
				0,
				0,
				117,
				110,
				108,
				111,
				99,
				107,
				67,
				104,
				97,
				114,
				97,
				84,
				111,
				116,
				97,
				108,
				0,
				80,
				97,
				103,
				101,
				48,
				0,
				105,
				110,
				100,
				101,
				120,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				3,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				3,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				3,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				121,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				121,
				0,
				0,
				0,
				0,
				5,
				252,
				0,
				0,
				0,
				1,
				0,
				121,
				0,
				0,
				0,
				0,
				5,
				248,
				233,
				3,
				0,
				0
			};
			for (int x3 = 0; x3 < header.Length; x3++)
			{
				file.Add(header[x3]);
			}
			byte[] Size3 = BitConverter.GetBytes(EntryCount * 12 + 8);
			Array.Reverse(Size3);
			file[280] = Size3[0];
			file[281] = Size3[1];
			file[282] = Size3[2];
			file[283] = Size3[3];
			Size3 = BitConverter.GetBytes(EntryCount * 12 + 4);
			Array.Reverse(Size3);
			file[292] = Size3[0];
			file[293] = Size3[1];
			file[294] = Size3[2];
			file[295] = Size3[3];
			Size3 = BitConverter.GetBytes(EntryCount);
			file[288] = Size3[0];
			file[289] = Size3[1];
			file[290] = Size3[2];
			file[291] = Size3[3];
			for (int x2 = 0; x2 < EntryCount; x2++)
			{
				for (int a = 0; a < 12; a++)
				{
					file.Add(EntryList[x2][a]);
				}
			}
			byte[] finalBytes = new byte[20]
			{
				0,
				0,
				0,
				8,
				0,
				0,
				0,
				2,
				0,
				121,
				24,
				0,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				0
			};
			for (int x = 0; x < finalBytes.Length; x++)
			{
				file.Add(finalBytes[x]);
			}
			return file.ToArray();
		}

		public void SaveFile()
		{
			if (FilePath != "")
			{
				if (File.Exists(FilePath + ".backup"))
				{
					File.Delete(FilePath + ".backup");
				}
				File.Copy(FilePath, FilePath + ".backup");
				File.WriteAllBytes(FilePath, ConvertToFile());
				MessageBox.Show("File saved to " + FilePath + ".");
			}
			else
			{
				SaveFileAs();
			}
		}

		public void SaveFileAs()
		{
			SaveFileDialog s = new SaveFileDialog();
			s.ShowDialog();
			if (!(s.FileName != ""))
			{
				return;
			}
			if (s.FileName == FilePath)
			{
				if (File.Exists(FilePath + ".backup"))
				{
					File.Delete(FilePath + ".backup");
				}
				File.Copy(FilePath, FilePath + ".backup");
			}
			else
			{
				FilePath = s.FileName;
			}
			File.WriteAllBytes(FilePath, ConvertToFile());
			MessageBox.Show("File saved to " + FilePath + ".");
		}

		public void CloseFile()
		{
			EntryList.Clear();
			ListBox1.Items.Clear();
			ListBox1.Items.Add("No file loaded...");
			EntryCount = 0;
			FilePath = "";
			FileBytes = new byte[0];
			FileOpen = false;
		}

		public void ExitTool()
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				AddID();
				numericUpDown1.Value = 0m;
				numericUpDown2.Value = 0m;
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				if (ListBox1.SelectedIndex != -1 && ListBox1.Items.Count > 0)
				{
					RemoveID(ListBox1.SelectedIndex);
				}
				else
				{
					MessageBox.Show("No section selected.");
				}
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void newCharacodeFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				DialogResult msg = MessageBox.Show("Are you sure you want to create a new file?", "", MessageBoxButtons.OKCancel);
				if (msg == DialogResult.OK)
				{
					NewFile();
				}
			}
			else
			{
				NewFile();
			}
		}

		private void openCharacodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				DialogResult msg = MessageBox.Show("Are you sure you want to open another file?", "", MessageBoxButtons.OKCancel);
				if (msg == DialogResult.OK)
				{
					OpenFile();
				}
			}
			else
			{
				OpenFile();
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				SaveFile();
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				SaveFileAs();
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				DialogResult msg = MessageBox.Show("Are you sure you want to close the actual file?", "", MessageBoxButtons.OKCancel);
				if (msg == DialogResult.OK)
				{
					CloseFile();
				}
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				DialogResult msg = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.OKCancel);
				if (msg == DialogResult.OK)
				{
					ExitTool();
				}
			}
			else
			{
				ExitTool();
			}
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
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // ListBox1
            // 
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.Items.AddRange(new object[] {
            "No file loaded..."});
            this.ListBox1.Location = new System.Drawing.Point(12, 34);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(343, 342);
            this.ListBox1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(367, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newCharacodeFileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openCharacodeToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.closeToolStripMenuItem.Text = "Close File";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 410);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(343, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add unlock section";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 438);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(343, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Remove selected unlock section";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 387);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Preset ID for new unlock: (Example: 7B01)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Hexadecimal = true;
            this.numericUpDown1.Location = new System.Drawing.Point(220, 384);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(63, 20);
            this.numericUpDown1.TabIndex = 6;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Hexadecimal = true;
            this.numericUpDown2.Location = new System.Drawing.Point(289, 384);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(66, 20);
            this.numericUpDown2.TabIndex = 7;
            // 
            // Tool_UnlockCharaTotalEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 474);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ListBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Tool_UnlockCharaTotalEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unlock Chara Total Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
