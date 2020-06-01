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

		public void OpenFile()
		{
			OpenFileDialog o = new OpenFileDialog();
			o.DefaultExt = "xfbin";
			o.ShowDialog();
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
                MessageBox.Show("Characode contains " + CharacterCount + " character IDs.");
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
			ListBox1 = new System.Windows.Forms.ListBox();
			button2 = new System.Windows.Forms.Button();
			button1 = new System.Windows.Forms.Button();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newCharacodeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openCharacodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			textBox1 = new System.Windows.Forms.TextBox();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			ListBox1.FormattingEnabled = true;
			ListBox1.Items.AddRange(new object[1]
			{
				"No file loaded..."
			});
			ListBox1.Location = new System.Drawing.Point(13, 29);
			ListBox1.Name = "ListBox1";
			ListBox1.Size = new System.Drawing.Size(273, 329);
			ListBox1.TabIndex = 0;
			button2.Location = new System.Drawing.Point(12, 390);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(274, 23);
			button2.TabIndex = 2;
			button2.Text = "Remove selected ID";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			button1.Location = new System.Drawing.Point(119, 364);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(167, 23);
			button1.TabIndex = 3;
			button1.Text = "Add new ID";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				fileToolStripMenuItem
			});
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(298, 24);
			menuStrip1.TabIndex = 4;
			menuStrip1.Text = "menuStrip1";
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5]
			{
				newCharacodeFileToolStripMenuItem,
				openCharacodeToolStripMenuItem,
				saveToolStripMenuItem,
				saveAsToolStripMenuItem,
				closeToolStripMenuItem
			});
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			fileToolStripMenuItem.Text = "File";
			newCharacodeFileToolStripMenuItem.Name = "newCharacodeFileToolStripMenuItem";
			newCharacodeFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			newCharacodeFileToolStripMenuItem.Text = "New";
			newCharacodeFileToolStripMenuItem.Click += new System.EventHandler(newCharacodeFileToolStripMenuItem_Click);
			openCharacodeToolStripMenuItem.Name = "openCharacodeToolStripMenuItem";
			openCharacodeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			openCharacodeToolStripMenuItem.Text = "Open";
			openCharacodeToolStripMenuItem.Click += new System.EventHandler(openCharacodeToolStripMenuItem_Click);
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
			textBox1.Location = new System.Drawing.Point(13, 366);
			textBox1.MaxLength = 8;
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(100, 20);
			textBox1.TabIndex = 5;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(298, 423);
			base.Controls.Add(textBox1);
			base.Controls.Add(button1);
			base.Controls.Add(button2);
			base.Controls.Add(ListBox1);
			base.Controls.Add(menuStrip1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MainMenuStrip = menuStrip1;
			base.Name = "Tool_CharacodeEditor";
			Text = "Characode Editor";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
