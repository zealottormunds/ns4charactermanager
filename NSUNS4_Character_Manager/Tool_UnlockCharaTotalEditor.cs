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
			ListBox1 = new System.Windows.Forms.ListBox();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			button1 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
			SuspendLayout();
			ListBox1.FormattingEnabled = true;
			ListBox1.Items.AddRange(new object[1]
			{
				"No file loaded..."
			});
			ListBox1.Location = new System.Drawing.Point(12, 34);
			ListBox1.Name = "ListBox1";
			ListBox1.Size = new System.Drawing.Size(343, 342);
			ListBox1.TabIndex = 0;
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				fileToolStripMenuItem
			});
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(367, 24);
			menuStrip1.TabIndex = 1;
			menuStrip1.Text = "menuStrip1";
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5]
			{
				newToolStripMenuItem,
				openToolStripMenuItem,
				saveToolStripMenuItem,
				saveAsToolStripMenuItem,
				closeToolStripMenuItem
			});
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			fileToolStripMenuItem.Text = "File";
			newToolStripMenuItem.Name = "newToolStripMenuItem";
			newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			newToolStripMenuItem.Text = "New";
			newToolStripMenuItem.Click += new System.EventHandler(newCharacodeFileToolStripMenuItem_Click);
			openToolStripMenuItem.Name = "openToolStripMenuItem";
			openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			openToolStripMenuItem.Text = "Open";
			openToolStripMenuItem.Click += new System.EventHandler(openCharacodeToolStripMenuItem_Click);
			saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			saveToolStripMenuItem.Text = "Save";
			saveToolStripMenuItem.Click += new System.EventHandler(saveToolStripMenuItem_Click);
			saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			saveAsToolStripMenuItem.Text = "Save As...";
			saveAsToolStripMenuItem.Click += new System.EventHandler(saveAsToolStripMenuItem_Click);
			closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			closeToolStripMenuItem.Text = "Close File";
			closeToolStripMenuItem.Click += new System.EventHandler(closeToolStripMenuItem_Click);
			button1.Location = new System.Drawing.Point(12, 410);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(343, 22);
			button1.TabIndex = 2;
			button1.Text = "Add unlock section";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			button2.Location = new System.Drawing.Point(12, 438);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(343, 23);
			button2.TabIndex = 3;
			button2.Text = "Remove selected unlock section";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(9, 387);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(207, 13);
			label1.TabIndex = 5;
			label1.Text = "Preset ID for new unlock: (Example: 7B01)";
			numericUpDown1.Hexadecimal = true;
			numericUpDown1.Location = new System.Drawing.Point(220, 384);
			numericUpDown1.Maximum = new decimal(new int[4]
			{
				255,
				0,
				0,
				0
			});
			numericUpDown1.Name = "numericUpDown1";
			numericUpDown1.Size = new System.Drawing.Size(63, 20);
			numericUpDown1.TabIndex = 6;
			numericUpDown2.Hexadecimal = true;
			numericUpDown2.Location = new System.Drawing.Point(289, 384);
			numericUpDown2.Maximum = new decimal(new int[4]
			{
				255,
				0,
				0,
				0
			});
			numericUpDown2.Name = "numericUpDown2";
			numericUpDown2.Size = new System.Drawing.Size(66, 20);
			numericUpDown2.TabIndex = 7;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(367, 474);
			base.Controls.Add(numericUpDown2);
			base.Controls.Add(numericUpDown1);
			base.Controls.Add(label1);
			base.Controls.Add(button2);
			base.Controls.Add(button1);
			base.Controls.Add(ListBox1);
			base.Controls.Add(menuStrip1);
			base.MainMenuStrip = menuStrip1;
			base.Name = "Tool_UnlockCharaTotalEditor";
			Text = "Unlock Chara Total Editor";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
