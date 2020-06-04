using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
	public class Tool_CharacodeEditor : Form
	{
		public bool FileOpen = false;

		public string FilePath = "";

		public byte[] fileBytes = new byte[0];

		public List<string> CharacterList = new List<string>();

		public int CharacterCount = 0;

		private IContainer components = null;

		private ListBox ListBox1;

		private Button button2;

		private Button button1;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem fileToolStripMenuItem;

		private ToolStripMenuItem newCharacodeFileToolStripMenuItem;

		private ToolStripMenuItem openCharacodeToolStripMenuItem;

		private ToolStripMenuItem saveToolStripMenuItem;

		private ToolStripMenuItem saveAsToolStripMenuItem;

		private ToolStripMenuItem closeToolStripMenuItem;

		private TextBox textBox1;

		public Tool_CharacodeEditor()
		{
			InitializeComponent();
		}

		public void NewFile()
		{
			fileBytes = new byte[0];
			FilePath = "";
			ListBox1.Items.Clear();
			CharacterList = new List<string>();
			CharacterCount = 0;
			FileOpen = true;
		}

		public void OpenFile(string path = "")
		{
			OpenFileDialog o = new OpenFileDialog();
			o.DefaultExt = "xfbin";

            if(path == "") o.ShowDialog();
            else o.FileName = path;

            if (!(o.FileName != "") || !File.Exists(o.FileName))
            {
                return;
            }

            ListBox1.Items.Clear();
            FilePath = o.FileName;
            fileBytes = File.ReadAllBytes(FilePath);

            // Check for NUCC in header
            if (!(fileBytes.Length > 0x44 && Main.b_ReadString(fileBytes, 0, 4) == "NUCC"))
            {
                MessageBox.Show("Not a valid .xfbin file.");
                return;
            }

			if (XfbinParser.GetNameList(fileBytes)[0] == "characode")
			{
                int fileStart = XfbinParser.GetFileSectionIndex(fileBytes);
                CharacterCount = Main.b_ReadInt(fileBytes, fileStart + 0x1C);

				CharacterList = new List<string>();
				for (int x = 0; x < CharacterCount; x++)
				{
                    string character = Main.b_ReadString(fileBytes, fileStart + 0x20 + (x * 8));
                    CharacterList.Add(character);
					ListBox1.Items.Add((x + 1).ToString("X2") + " = " + character);
				}

                FileOpen = true;
                if (this.Visible) MessageBox.Show("Characode contains " + CharacterCount + " character IDs.");
			}
			else
			{
				MessageBox.Show("Please select a valid characode file.");
				FilePath = "";
				fileBytes = new byte[0];
				FileOpen = false;
			}
		}

		public void AddID(string ID)
		{
			CharacterList.Add(ID);
			ListBox1.Items.Add((CharacterCount + 1).ToString("X2") + " = " + ID);
			ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
			CharacterCount++;
		}

		public void RemoveID(int Index)
		{
			CharacterList.RemoveAt(Index);
			if (ListBox1.SelectedIndex > 0)
			{
				ListBox1.SelectedIndex--;
			}
			else
			{
				ListBox1.ClearSelected();
			}
			ListBox1.Items.RemoveAt(Index);
			CharacterCount--;
			UpdateList();
		}

		public byte[] ConvertToFile()
		{
            byte[] actual = new byte[0];
            int startOfFile = XfbinParser.GetFileSectionIndex(fileBytes);
            for (int x = 0; x < startOfFile + 0x20; x++) actual = Main.b_AddBytes(actual, new byte[] { fileBytes[x] });

            actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(CharacterCount), startOfFile + 0x20 - 0x4);
            actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes((CharacterCount * 8) + 0x4), startOfFile + 0x20 - 0x8, 1);
            actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes((CharacterCount * 8) + 0x8), startOfFile + 0x20 - 0x8 - 0xC, 1);

            for (int x = 0; x < CharacterCount; x++)
			{
                actual = Main.b_AddBytes(actual, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
                actual = Main.b_ReplaceString(actual, CharacterList[x], startOfFile + 0x20 + (0x8 * x));
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
				99,
				0,
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

            actual = Main.b_AddBytes(actual, finalBytes);
            return actual;
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
                if (this.Visible) MessageBox.Show("File saved to " + FilePath + ".");
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
			CharacterList.Clear();
			ListBox1.Items.Clear();
			ListBox1.Items.Add("No file loaded...");
			CharacterCount = 0;
			FilePath = "";
			fileBytes = new byte[0];
			FileOpen = false;
		}

		public void UpdateList()
		{
			for (int x = 0; x < CharacterCount; x++)
			{
				string character = CharacterList[x];
				ListBox1.Items[x] = (x + 1).ToString("X2") + " = " + character;
			}
		}

		public void ExitTool()
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				if (textBox1.Text != "" && !CharacterList.Contains(textBox1.Text))
				{
					AddID(textBox1.Text);
					textBox1.Text = "";
				}
				else if (textBox1.Text == "")
				{
					MessageBox.Show("ID to add is empty!");
				}
				else
				{
					MessageBox.Show("ID already exists in characode.");
				}
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
					MessageBox.Show("No ID selected.");
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
					CloseFile();
					OpenFile();
				}
			}
			else
			{
				CloseFile();
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
				DialogResult msg = MessageBox.Show("Are you sure you want to discard this file?", "", MessageBoxButtons.OKCancel);
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
			ExitTool();
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCharacodeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCharacodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListBox1
            // 
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.Items.AddRange(new object[] {
            "No file loaded..."});
            this.ListBox1.Location = new System.Drawing.Point(13, 29);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(273, 329);
            this.ListBox1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 390);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(274, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Remove selected ID";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 364);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add new ID";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(298, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCharacodeFileToolStripMenuItem,
            this.openCharacodeToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newCharacodeFileToolStripMenuItem
            // 
            this.newCharacodeFileToolStripMenuItem.Name = "newCharacodeFileToolStripMenuItem";
            this.newCharacodeFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newCharacodeFileToolStripMenuItem.Text = "New";
            this.newCharacodeFileToolStripMenuItem.Click += new System.EventHandler(this.newCharacodeFileToolStripMenuItem_Click);
            // 
            // openCharacodeToolStripMenuItem
            // 
            this.openCharacodeToolStripMenuItem.Name = "openCharacodeToolStripMenuItem";
            this.openCharacodeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openCharacodeToolStripMenuItem.Text = "Open";
            this.openCharacodeToolStripMenuItem.Click += new System.EventHandler(this.openCharacodeToolStripMenuItem_Click);
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 366);
            this.textBox1.MaxLength = 8;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            // 
            // Tool_CharacodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 423);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ListBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Tool_CharacodeEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Characode Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
