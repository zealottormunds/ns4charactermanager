using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
	public class Tool_RosterEditor : Form
	{
		public bool FileOpen = false;

        public byte[] fileBytes = new byte[] { };

		public string FilePath = "";

		public int EntryCount = 0;

		public List<string> CharacterList = new List<string>();
		public List<int> PageList = new List<int>();
		public List<int> PositionList = new List<int>();
		public List<int> CostumeList = new List<int>();
		public List<string> ChaList = new List<string>();
		public List<string> AccessoryList = new List<string>();
        public List<string> NewIdList = new List<string>();
		public List<byte[]> GibberishBytes = new List<byte[]>();

		private IContainer components = null;

		private ListBox ListBox1;

		private Button button1;

		private Button button2;

		private Label label1;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem fileToolStripMenuItem;

		private ToolStripMenuItem newToolStripMenuItem;

		private ToolStripMenuItem openToolStripMenuItem;

		private ToolStripMenuItem saveToolStripMenuItem;

		private ToolStripMenuItem saveAsToolStripMenuItem;

		private ToolStripMenuItem closeToolStripMenuItem;

		private TextBox t_charid;

		private Label label2;

		private NumericUpDown t_rosterpage;

		private NumericUpDown t_positionroster;

		private Label label3;

		private NumericUpDown t_costid;

		private Label label4;

		private TextBox t_accs;

		private Label label5;

		private Button button3;

		private TextBox t_chaid;

		private Label label6;
        private TextBox t_unkid;
        private Label label7;
        private Button button4;

		public Tool_RosterEditor()
		{
			InitializeComponent();
		}

		public void NewFile()
		{
			FilePath = "";
			FileOpen = true;
            fileBytes = new byte[] { };
			EntryCount = 0;
			CharacterList = new List<string>();
			PageList = new List<int>();
			PositionList = new List<int>();
			CostumeList = new List<int>();
			ChaList = new List<string>();
			AccessoryList = new List<string>();
            NewIdList = new List<string>();
			GibberishBytes = new List<byte[]>();
			ListBox1.ClearSelected();
			ListBox1.Items.Clear();
			t_accs.Clear();
			t_charid.Clear();
			t_chaid.Clear();
			t_costid.Value = 0m;
			t_positionroster.Value = 0m;
			t_rosterpage.Value = 0m;
		}

		public void OpenFile()
		{
			NewFile();
			FileOpen = false;
			OpenFileDialog o = new OpenFileDialog();
			o.DefaultExt = "xfbin";
			o.ShowDialog();
			if (o.FileName != "" && File.Exists(o.FileName))
			{
				FilePath = o.FileName;
				FileOpen = true;
				byte[] FileBytes = File.ReadAllBytes(FilePath);
                fileBytes = FileBytes;
				EntryCount = BitConverter.ToInt16(Main.b_ReadByteArray(FileBytes, 308, 4), 0);
				for (int x = 0; x < EntryCount; x++)
				{
					byte[] a = Main.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x, 4);
					int NamePointer2 = Main.b_byteArrayToInt(Main.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x, 4));
					string Name = Main.b_ReadString(FileBytes, NamePointer2 + 0xD8 * x + 320);
					int PageCount = Main.b_byteArrayToInt(Main.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 8, 4));
					int Position = Main.b_byteArrayToInt(Main.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 12, 4));
					int CostumeID = Main.b_byteArrayToInt(Main.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 16, 4));
					int ChaPointer = Main.b_byteArrayToInt(Main.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 24, 4));
					string Cha = Main.b_ReadString(FileBytes, ChaPointer + 0xD8 * x + 0x140 + 24);
					NamePointer2 = Main.b_byteArrayToInt(Main.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 32, 4));
					string Accessory = Main.b_ReadString(FileBytes, NamePointer2 + 0x20 + 0x140 + 0xD8 * x);
                    NamePointer2 = Main.b_byteArrayToInt(Main.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 40, 4));
                    string NewId = Main.b_ReadString(FileBytes, NamePointer2 + 0x28 + 0x140 + 0xD8 * x);
					byte[] gib = Main.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 48, 168);
					CharacterList.Add(Name);
					PageList.Add(PageCount);
					PositionList.Add(Position);
					CostumeList.Add(CostumeID);
					ChaList.Add(Cha);
					AccessoryList.Add(Accessory);
                    NewIdList.Add(NewId);
					GibberishBytes.Add(gib);
					ListBox1.Items.Add("Page: " + PageCount + ", Pos: " + Position.ToString("X2") + ", Char: " + Name + ", Cost: " + CostumeID);
				}
			}
		}

        public byte[] ConvertToFile()
        {
            byte[] actual = new byte[] { };

            int fileStart = XfbinParser.GetFileSectionIndex(fileBytes) + 0x28;
            actual = Main.b_AddBytes(actual, fileBytes, 0, 0, fileStart + 0x10);
            actual = Main.b_AddBytes(actual, new byte[0xD8 * EntryCount]);

            for(int x = 0; x < EntryCount; x++)
            {
                int entryIndex = fileStart + 0x10 + (x * 0xD8);
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(actual.Length - entryIndex), entryIndex);
                actual = Main.b_AddString(actual, CharacterList[x]);
                actual = Main.b_AddBytes(actual, new byte[0x8 - CharacterList[x].Length]);
                
                if(ChaList[x] != "")
                {
                    int entryChaIndex = entryIndex + 0x18;
                    actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(actual.Length - entryChaIndex), entryChaIndex);
                    actual = Main.b_AddString(actual, ChaList[x]);
                    actual = Main.b_AddBytes(actual, new byte[0x10 - ChaList[x].Length]);
                }

                if(AccessoryList[x] != "")
                {
                    int entryAccIndex = entryIndex + 0x20;
                    actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(actual.Length - entryAccIndex), entryAccIndex);
                    actual = Main.b_AddString(actual, AccessoryList[x]);
                    actual = Main.b_AddBytes(actual, new byte[0x10 - AccessoryList[x].Length]);
                }

                if(NewIdList[x] != "")
                {
                    int entryIdIndex = entryIndex + 0x28;
                    actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(actual.Length - entryIdIndex), entryIdIndex);
                    actual = Main.b_AddString(actual, NewIdList[x]);
                    actual = Main.b_AddBytes(actual, new byte[0x8 - NewIdList[x].Length]);
                }

                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(PageList[x]), 0x140 + 0xD8 * x + 8);
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(PositionList[x]), 0x140 + 0xD8 * x + 12);
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(CostumeList[x]), 0x140 + 0xD8 * x + 16);

                // Add positions and all that crap
                actual = Main.b_ReplaceBytes(actual, GibberishBytes[x], 0x140 + 0xD8 * x + 48);
            }

            int totalSize = actual.Length - fileStart;

            // Add EOF
            actual = Main.b_AddBytes(actual, new byte[] { 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x02, 0x00, 0x79, 0x18, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 });

            // Fix sizes
            actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes((short)(EntryCount)), fileStart + 0x2C - 0x28);
            actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(totalSize), fileStart + 0x24 - 0x28, 1);
            actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(totalSize + 4), fileStart + 0x18 - 0x28, 1);

            return actual;
        }

		public byte[] C_onvertToFile()
		{
			byte[] fileBytes11 = Main.b_AddBytes(new byte[0], new byte[320]
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
				236,
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
				37,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				34,
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
				99,
				104,
				97,
				114,
				97,
				99,
				116,
				101,
				114,
				83,
				101,
				108,
				101,
				99,
				116,
				80,
				97,
				114,
				97,
				109,
				46,
				98,
				105,
				110,
				0,
				0,
				99,
				104,
				97,
				114,
				97,
				99,
				116,
				101,
				114,
				83,
				101,
				108,
				101,
				99,
				116,
				80,
				97,
				114,
				97,
				109,
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
				1,
				2,
				220,
				0,
				0,
				0,
				1,
				0,
				121,
				0,
				0,
				0,
				1,
				2,
				216,
				232,
				3,
				0,
				0,
				23,
				1,
				0,
				0,
				8,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});

			for (int x2 = 0; x2 < EntryCount; x2++)
			{
				fileBytes11 = Main.b_AddBytes(fileBytes11, new byte[216]
				{
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
					31,
					5,
					152,
					194,
					41,
					220,
					147,
					66,
					41,
					188,
					161,
					195,
					102,
					230,
					164,
					66,
					41,
					220,
					147,
					66,
					41,
					188,
					161,
					195,
					31,
					5,
					152,
					194,
					41,
					220,
					147,
					66,
					41,
					188,
					161,
					195,
					102,
					230,
					164,
					66,
					41,
					220,
					147,
					66,
					41,
					188,
					161,
					195,
					31,
					5,
					152,
					194,
					133,
					107,
					183,
					66,
					205,
					236,
					159,
					195,
					102,
					230,
					164,
					66,
					133,
					107,
					183,
					66,
					205,
					236,
					159,
					195,
					154,
					153,
					29,
					65,
					102,
					198,
					174,
					67,
					92,
					143,
					234,
					64,
					133,
					139,
					174,
					67,
					246,
					40,
					44,
					65,
					236,
					209,
					172,
					67,
					0,
					0,
					128,
					63,
					205,
					204,
					228,
					64,
					51,
					51,
					211,
					191,
					133,
					235,
					33,
					64,
					246,
					40,
					156,
					64,
					113,
					61,
					74,
					191,
					51,
					51,
					149,
					65,
					82,
					184,
					137,
					66,
					20,
					174,
					199,
					62,
					143,
					194,
					69,
					64,
					246,
					40,
					156,
					64,
					215,
					163,
					112,
					191,
					51,
					51,
					149,
					65,
					82,
					184,
					137,
					66,
					20,
					174,
					199,
					62,
					143,
					194,
					69,
					64,
					246,
					40,
					156,
					64,
					215,
					163,
					112,
					191
				});
			}

			for (int x = 0; x < EntryCount; x++)
			{
				fileBytes11 = Main.b_ReplaceBytes(fileBytes11, BitConverter.GetBytes(PageList[x]), 0x140 + 0xD8 * x + 8);
				fileBytes11 = Main.b_ReplaceBytes(fileBytes11, BitConverter.GetBytes(PositionList[x]), 0x140 + 0xD8 * x + 12);
				fileBytes11 = Main.b_ReplaceBytes(fileBytes11, BitConverter.GetBytes(CostumeList[x]), 0x140 + 0xD8 * x + 16);
				int namePointer = fileBytes11.Length;
				fileBytes11 = Main.b_AddString(fileBytes11, CharacterList[x], 16);
				int chaPointer = fileBytes11.Length;
				if (ChaList[x] == "")
				{
					chaPointer = 0;
				}
				else
				{
					fileBytes11 = Main.b_AddString(fileBytes11, ChaList[x], 16);
				}
				int accPointer = fileBytes11.Length;
				if (AccessoryList[x] == "")
				{
					accPointer = 0;
				}
				else
				{
					fileBytes11 = Main.b_AddString(fileBytes11, AccessoryList[x], 16);
				}
				fileBytes11 = Main.b_ReplaceBytes(fileBytes11, BitConverter.GetBytes(namePointer - 0x140 - 0xD8 * x), 0x140 + 0xD8 * x);
				fileBytes11 = ((chaPointer == 0) ? Main.b_ReplaceBytes(fileBytes11, BitConverter.GetBytes(0), 0x140 + 0xD8 * x + 24) : Main.b_ReplaceBytes(fileBytes11, BitConverter.GetBytes(chaPointer - 0x140 - 0xD8 * x - 24), 0x140 + 0xD8 * x + 24));
				fileBytes11 = ((accPointer == 0) ? Main.b_ReplaceBytes(fileBytes11, BitConverter.GetBytes(0), 0x140 + 0xD8 * x + 32) : Main.b_ReplaceBytes(fileBytes11, BitConverter.GetBytes(accPointer - 0x140 - 0xD8 * x - 32), 0x140 + 0xD8 * x + 32));
				fileBytes11 = Main.b_ReplaceBytes(fileBytes11, GibberishBytes[x], 0x140 + 0xD8 * x + 48);
			}
			int OverallSize = fileBytes11.Length - 304;
			byte[] size3 = BitConverter.GetBytes(OverallSize);
			Array.Reverse(size3);
			byte[] size2 = BitConverter.GetBytes(OverallSize + 4);
			Array.Reverse(size2);
			fileBytes11 = Main.b_ReplaceBytes(fileBytes11, size3, 300);
			fileBytes11 = Main.b_ReplaceBytes(fileBytes11, size2, 288);
			fileBytes11 = Main.b_ReplaceBytes(fileBytes11, BitConverter.GetBytes(EntryCount), 308);
			return Main.b_AddBytes(fileBytes11, new byte[20]
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
			});
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

		public void CloseFile()
		{
			NewFile();
			FileOpen = false;
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

		public void AddEntry()
		{
			string Name = t_charid.Text;
			int PageCount = (int)t_rosterpage.Value;
			int Position = (int)t_positionroster.Value;
			int CostumeID = (int)t_costid.Value;
			string Cha = t_chaid.Text;
			string Accessory = t_accs.Text;
            string NewId = t_unkid.Text;
			CharacterList.Add(Name);
			PageList.Add(PageCount);
			PositionList.Add(Position);
			CostumeList.Add(CostumeID);
			ChaList.Add(Cha);
			AccessoryList.Add(Accessory);
            NewIdList.Add(NewId);

            if (ListBox1.SelectedIndex != -1) GibberishBytes.Add(GibberishBytes[ListBox1.SelectedIndex]);
            else GibberishBytes.Add(new byte[168]);

			ListBox1.Items.Add("Page: " + PageCount + ", Pos: " + Position.ToString("X2") + ", Char: " + Name + ", Cost: " + CostumeID);
			EntryCount++;
            ListBox1.SelectedIndex = EntryCount - 1;
			MessageBox.Show("Entry added.");
		}

		public void RemoveEntry(int Index)
		{
			if (Index != -1)
			{
				CharacterList.RemoveAt(Index);
				PageList.RemoveAt(Index);
				PositionList.RemoveAt(Index);
				CostumeList.RemoveAt(Index);
				ChaList.RemoveAt(Index);
				AccessoryList.RemoveAt(Index);
                NewIdList.RemoveAt(Index);
				GibberishBytes.RemoveAt(Index);
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
				MessageBox.Show("Entry deleted.");
			}
		}

		public void EditEntry(int x)
		{
			if (x != -1)
			{
				string Name = t_charid.Text;
				int PageCount = (int)t_rosterpage.Value;
				int Position = (int)t_positionroster.Value;
				int CostumeID = (int)t_costid.Value;
				string Cha = t_chaid.Text;
				string Accessory = t_accs.Text;
                string NewId = t_unkid.Text;
				CharacterList[x] = Name;
				PageList[x] = PageCount;
				PositionList[x] = Position;
				CostumeList[x] = CostumeID;
				ChaList[x] = Cha;
				AccessoryList[x] = Accessory;
                NewIdList[x] = NewId;
				ListBox1.Items[x] = "Page: " + PageCount + ", Pos: " + Position.ToString("X2") + ", Char: " + Name + ", Cost: " + CostumeID;
				MessageBox.Show("Entry updated.");
			}
		}

		public void OpenPositionEditor()
		{
			int x = ListBox1.SelectedIndex;
			if (x != -1)
			{
				Tool_RosterEditor_PositionData t = new Tool_RosterEditor_PositionData(this, GibberishBytes[x], x);
				t.ShowDialog();
			}
			else
			{
				MessageBox.Show("No entry selected...");
			}
		}

		private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int x = ListBox1.SelectedIndex;
			if (x > -1 && x < ListBox1.Items.Count)
			{
				t_charid.Text = CharacterList[x];
				t_rosterpage.Value = PageList[x];
				t_positionroster.Value = PositionList[x];
				t_costid.Value = CostumeList[x];
				t_chaid.Text = ChaList[x];
				t_accs.Text = AccessoryList[x];
                t_unkid.Text = NewIdList[x];
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				if (FileOpen)
				{
					DialogResult msg = MessageBox.Show("Are you sure you want to open another file?", "", MessageBoxButtons.OKCancel);
					if (msg == DialogResult.OK)
					{
						OpenFile();
					}
				}
			}
			else
			{
				OpenFile();
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
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
				DialogResult msg = MessageBox.Show("Are you sure you want to close this file?", "", MessageBoxButtons.OKCancel);
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

		private void button3_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				EditEntry(ListBox1.SelectedIndex);
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				AddEntry();
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
				RemoveEntry(ListBox1.SelectedIndex);
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				OpenPositionEditor();
			}
			else
			{
				MessageBox.Show("No file loaded...");
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.t_charid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.t_rosterpage = new System.Windows.Forms.NumericUpDown();
            this.t_positionroster = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.t_costid = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.t_accs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.t_chaid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.t_unkid = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t_rosterpage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_positionroster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_costid)).BeginInit();
            this.SuspendLayout();
            // 
            // ListBox1
            // 
            this.ListBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.ItemHeight = 15;
            this.ListBox1.Location = new System.Drawing.Point(13, 26);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(387, 394);
            this.ListBox1.TabIndex = 0;
            this.ListBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 429);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(207, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Create new slot with this data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 429);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(388, 33);
            this.button2.TabIndex = 1;
            this.button2.Text = "Remove selected slot";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(406, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Character ID (Example: 2nrt00)";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(628, 24);
            this.menuStrip1.TabIndex = 3;
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
            this.newToolStripMenuItem.Enabled = false;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close File";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // t_charid
            // 
            this.t_charid.Location = new System.Drawing.Point(409, 45);
            this.t_charid.MaxLength = 15;
            this.t_charid.Name = "t_charid";
            this.t_charid.Size = new System.Drawing.Size(207, 20);
            this.t_charid.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(406, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Roster page:";
            // 
            // t_rosterpage
            // 
            this.t_rosterpage.Location = new System.Drawing.Point(409, 93);
            this.t_rosterpage.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.t_rosterpage.Name = "t_rosterpage";
            this.t_rosterpage.Size = new System.Drawing.Size(207, 20);
            this.t_rosterpage.TabIndex = 7;
            // 
            // t_positionroster
            // 
            this.t_positionroster.Hexadecimal = true;
            this.t_positionroster.Location = new System.Drawing.Point(409, 139);
            this.t_positionroster.Maximum = new decimal(new int[] {
            27,
            0,
            0,
            0});
            this.t_positionroster.Name = "t_positionroster";
            this.t_positionroster.Size = new System.Drawing.Size(207, 20);
            this.t_positionroster.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(406, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Position on roster:";
            // 
            // t_costid
            // 
            this.t_costid.Hexadecimal = true;
            this.t_costid.Location = new System.Drawing.Point(409, 185);
            this.t_costid.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.t_costid.Name = "t_costid";
            this.t_costid.Size = new System.Drawing.Size(207, 20);
            this.t_costid.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Costume ID:";
            // 
            // t_accs
            // 
            this.t_accs.Location = new System.Drawing.Point(409, 273);
            this.t_accs.MaxLength = 15;
            this.t_accs.Name = "t_accs";
            this.t_accs.Size = new System.Drawing.Size(207, 20);
            this.t_accs.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Accessory (Optional. Example: 5nrtacc1)";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(409, 390);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(207, 33);
            this.button3.TabIndex = 14;
            this.button3.Text = "Save selected slot";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // t_chaid
            // 
            this.t_chaid.Location = new System.Drawing.Point(409, 230);
            this.t_chaid.MaxLength = 15;
            this.t_chaid.Name = "t_chaid";
            this.t_chaid.Size = new System.Drawing.Size(207, 20);
            this.t_chaid.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(406, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Cha ID (Optional)";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(409, 351);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(207, 33);
            this.button4.TabIndex = 17;
            this.button4.Text = "Edit position data";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // t_unkid
            // 
            this.t_unkid.Location = new System.Drawing.Point(409, 320);
            this.t_unkid.MaxLength = 15;
            this.t_unkid.Name = "t_unkid";
            this.t_unkid.Size = new System.Drawing.Size(207, 20);
            this.t_unkid.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(406, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Charsel file (Optional. Example: onrk)";
            // 
            // Tool_RosterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 474);
            this.Controls.Add(this.t_unkid);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.t_chaid);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.t_accs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.t_costid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.t_positionroster);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.t_rosterpage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.t_charid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ListBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Tool_RosterEditor";
            this.Text = "Character Roster Manager (CharacterSelectParam.xfbin)";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t_rosterpage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_positionroster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_costid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
