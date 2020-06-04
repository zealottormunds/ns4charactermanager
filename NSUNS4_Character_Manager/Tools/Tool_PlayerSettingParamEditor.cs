using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
	public class Tool_PlayerSettingParamEditor : Form
	{
		public bool FileOpen = false;
		public string FilePath = "";
        public byte[] FileBytes;

		public List<byte[]> PresetList = new List<byte[]>();
		public List<byte[]> CharacodeList = new List<byte[]>();
		public List<int> OptValueA = new List<int>();
		public List<string> CharacterList = new List<string>();
		public List<int> OptValueB = new List<int>();
		public List<int> OptValueC = new List<int>();
		public List<string> c_cha_a_List = new List<string>();
		public List<string> c_cha_b_List = new List<string>();
		public List<int> OptValueD = new List<int>();
		public List<int> OptValueE = new List<int>();

		public int EntryCount = 0;

		private IContainer components = null;
		public ListBox ListBox1;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem newToolStripMenuItem;
		private ToolStripMenuItem openToolStripMenuItem;
		private ToolStripMenuItem saveToolStripMenuItem;
		private ToolStripMenuItem saveAsToolStripMenuItem;
		private ToolStripMenuItem closeToolStripMenuItem;
		private Button AddButton;
		private Button RemoveButton;
		private Label label1;
		private NumericUpDown pid0;
		private NumericUpDown pid1;
    	private Label label2;
		private NumericUpDown cid1;
		private NumericUpDown cid0;
		private Label label3;
		private TextBox cname;
		private TextBox cchaa;
		private Label label4;
		private TextBox cchab;
		private Label label5;
		private Button SaveButton;
		private NumericUpDown opta;
		private Label label6;
		private NumericUpDown optb;
		private Label label7;
		private NumericUpDown optc;
		private Label label8;
		private NumericUpDown opte;
		private Label label9;
		private NumericUpDown optd;
		private Label label10;

		public Tool_PlayerSettingParamEditor()
		{
			InitializeComponent();
		}

		public void ClearFile()
		{
            FileBytes = new byte[0];
            PresetList = new List<byte[]>();
			CharacodeList = new List<byte[]>();
			OptValueA = new List<int>();
			CharacterList = new List<string>();
			OptValueB = new List<int>();
			OptValueC = new List<int>();
			c_cha_a_List = new List<string>();
			c_cha_b_List = new List<string>();
			OptValueD = new List<int>();
			OptValueE = new List<int>();
			EntryCount = 0;
			ListBox1.Items.Clear();
		}

		public void NewFile()
		{
            FileBytes = new byte[0];
            FileOpen = true;
			FilePath = "";
			ClearFile();
		}

		public void CloseFile()
		{
			ClearFile();
			FileOpen = false;
			FilePath = "";
		}

		public void OpenFile(string basepath = "")
		{
			OpenFileDialog o = new OpenFileDialog();
			o.DefaultExt = "xfbin";

            if(basepath != "")
            {
                o.FileName = basepath;
            }
            else
            {
                o.ShowDialog();
            }

			if (!(o.FileName != "") || !File.Exists(o.FileName))
			{
				return;
			}

			ClearFile();
			FileOpen = true;
			FilePath = o.FileName;

			FileBytes = File.ReadAllBytes(FilePath);
			EntryCount = FileBytes[304] + FileBytes[305] * 0x100 + FileBytes[306] * 0x10000 + FileBytes[307] * 0x1000000;
			for (int x2 = 0; x2 < EntryCount; x2++)
			{
				long _ptr = 316 + 0x38 * x2;
				byte[] PresetID = new byte[4]
				{
					FileBytes[_ptr],
					FileBytes[_ptr + 1],
					0,
					0
				};
				byte[] CharacodeID = new byte[4]
				{
					FileBytes[_ptr + 4],
					FileBytes[_ptr + 5],
					0,
					0
				};
				int OptA = FileBytes[_ptr + 8] + FileBytes[_ptr + 9] * 0x100 + FileBytes[_ptr + 10] * 0x10000 + FileBytes[_ptr + 11] * 0x1000000;
				string CharacterID = "";
				long _ptrCharacter3 = FileBytes[_ptr + 16] + FileBytes[_ptr + 17] * 0x100 + FileBytes[_ptr + 18] * 0x10000 + FileBytes[_ptr + 19] * 0x1000000;
				for (int a3 = 0; a3 < 16; a3++)
				{
					if (FileBytes[_ptr + 16 + _ptrCharacter3 + a3] != 0)
					{
						string str = CharacterID;
						char c = (char)FileBytes[_ptr + 16 + _ptrCharacter3 + a3];
						CharacterID = str + c;
					}
					else
					{
						a3 = 16;
					}
				}
				int OptB = FileBytes[_ptr + 24] + FileBytes[_ptr + 25] * 0x100 + FileBytes[_ptr + 26] * 0x10000 + FileBytes[_ptr + 27] * 0x1000000;
				int OptC = FileBytes[_ptr + 28] + FileBytes[_ptr + 29] * 0x100 + FileBytes[_ptr + 30] * 0x10000 + FileBytes[_ptr + 31] * 0x1000000;
				string c_cha_a = "";
				_ptrCharacter3 = FileBytes[_ptr + 32] + FileBytes[_ptr + 33] * 0x100 + FileBytes[_ptr + 34] * 0x10000 + FileBytes[_ptr + 35] * 0x1000000;
				for (int a2 = 0; a2 < 16; a2++)
				{
					if (FileBytes[_ptr + 32 + _ptrCharacter3 + a2] != 0)
					{
						string str2 = c_cha_a;
						char c = (char)FileBytes[_ptr + 32 + _ptrCharacter3 + a2];
						c_cha_a = str2 + c;
					}
					else
					{
						a2 = 16;
					}
				}
				string c_cha_b = "";
				_ptrCharacter3 = FileBytes[_ptr + 40] + FileBytes[_ptr + 41] * 0x100 + FileBytes[_ptr + 42] * 0x10000 + FileBytes[_ptr + 43] * 0x1000000;
				for (int a = 0; a < 16; a++)
				{
					if (FileBytes[_ptr + 40 + _ptrCharacter3 + a] != 0)
					{
						string str3 = c_cha_b;
						char c = (char)FileBytes[_ptr + 40 + _ptrCharacter3 + a];
						c_cha_b = str3 + c;
					}
					else
					{
						a = 16;
					}
				}
				int OptD = FileBytes[_ptr + 48] + FileBytes[_ptr + 49] * 0x100 + FileBytes[_ptr + 50] * 0x10000 + FileBytes[_ptr + 51] * 0x1000000;
				int OptE = FileBytes[_ptr + 52] + FileBytes[_ptr + 53] * 0x100 + FileBytes[_ptr + 54] * 0x10000 + FileBytes[_ptr + 55] * 0x1000000;
				PresetList.Add(PresetID);
				CharacodeList.Add(CharacodeID);
				OptValueA.Add(OptA);
				CharacterList.Add(CharacterID);
				OptValueB.Add(OptB);
				OptValueC.Add(OptC);
				c_cha_a_List.Add(c_cha_a);
				c_cha_b_List.Add(c_cha_b);
				OptValueD.Add(OptD);
				OptValueE.Add(OptE);
			}
			for (int x = 0; x < EntryCount; x++)
			{
				string NewItem = "Preset: " + PresetList[x][0].ToString("X2") + " " + PresetList[x][1].ToString("X2") + ", Characode: " + CharacodeList[x][0].ToString("X2") + " " + CharacodeList[x][1].ToString("X2") + ", Name: " + CharacterList[x];
				ListBox1.Items.Add(NewItem);
			}
		}

        public void AddID()
        {
            // Generate new preset ID
            int maxPid = 0;
            for (int pid = 0; pid < EntryCount; pid++)
            {
                int thisPid = ((int)PresetList[pid][0] * 0x1) + ((int)PresetList[pid][1] * 0x100);
                if (thisPid > maxPid) maxPid = thisPid;
            }
            maxPid++;
            byte[] PresetID = BitConverter.GetBytes(maxPid);

            byte[] CharacodeID = new byte[4]
            {
                (byte)cid0.Value,
                (byte)cid1.Value,
                0,
                0
            };

            int OptA = (int)opta.Value;
            string CharacterID = cname.Text;
            int OptB = (int)optb.Value;
            int OptC = (int)optc.Value;
            string c_cha_a = cchaa.Text;
            string c_cha_b = cchab.Text;
            int OptD = (int)optd.Value;
            int OptE = (int)opte.Value;

			PresetList.Add(PresetID);
			CharacodeList.Add(CharacodeID);
			OptValueA.Add(OptA);
			CharacterList.Add(CharacterID);
			OptValueB.Add(OptB);
			OptValueC.Add(OptC);
			c_cha_a_List.Add(c_cha_a);
			c_cha_b_List.Add(c_cha_b);
			OptValueD.Add(OptD);
			OptValueE.Add(OptE);

			int x = EntryCount;
			string NewItem = "Preset: " + PresetList[x][0].ToString("X2") + " " + PresetList[x][1].ToString("X2") + ", Characode: " + CharacodeList[x][0].ToString("X2") + " " + CharacodeList[x][1].ToString("X2") + ", Name: " + CharacterList[x];
			ListBox1.Items.Add(NewItem);
			EntryCount++;
			ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
            if (this.Visible) MessageBox.Show("Entry added.");
		}

		public void RemoveID(int Index)
		{
			if (ListBox1.Items.Count > Index)
			{
				if (ListBox1.SelectedIndex > 0)
				{
					ListBox1.SelectedIndex--;
				}
				else
				{
					ListBox1.ClearSelected();
				}

				PresetList.RemoveAt(Index);
				CharacodeList.RemoveAt(Index);
				OptValueA.RemoveAt(Index);
				CharacterList.RemoveAt(Index);
				OptValueB.RemoveAt(Index);
				OptValueC.RemoveAt(Index);
				c_cha_a_List.RemoveAt(Index);
				c_cha_b_List.RemoveAt(Index);
				OptValueD.RemoveAt(Index);
				OptValueE.RemoveAt(Index);
				ListBox1.Items.RemoveAt(Index);
				EntryCount--;

				MessageBox.Show("Entry deleted.");
			}
			else
			{
				MessageBox.Show("No item to delete...");
			}
		}

		public void SaveID()
		{
			int x = ListBox1.SelectedIndex;
			if (x > -1)
			{
				byte[] PresetID = new byte[4]
				{
					(byte)pid0.Value,
					(byte)pid1.Value,
					0,
					0
				};
				byte[] CharacodeID = new byte[4]
				{
					(byte)cid0.Value,
					(byte)cid1.Value,
					0,
					0
				};

				int OptA = (int)opta.Value;
				string CharacterID = cname.Text;
				int OptB = (int)optb.Value;
				int OptC = (int)optc.Value;
				string c_cha_a = cchaa.Text;
				string c_cha_b = cchab.Text;
				int OptD = (int)optd.Value;
				int OptE = (int)opte.Value;
				PresetList[x] = PresetID;
				CharacodeList[x] = CharacodeID;
				OptValueA[x] = OptA;
				CharacterList[x] = CharacterID;
				OptValueB[x] = OptB;
				OptValueC[x] = OptC;
				c_cha_a_List[x] = c_cha_a;
				c_cha_b_List[x] = c_cha_b;
				OptValueD[x] = OptD;
				OptValueE[x] = OptE;

				string NewItem = "Preset: " + PresetList[x][0].ToString("X2") + " " + PresetList[x][1].ToString("X2") + ", Characode: " + CharacodeList[x][0].ToString("X2") + " " + CharacodeList[x][1].ToString("X2") + ", Name: " + CharacterList[x];
				ListBox1.Items[x] = NewItem;
				MessageBox.Show("Entry saved.");
			}
			else
			{
				MessageBox.Show("No entry selected.");
			}
		}

        public byte[] ConvertToFile()
        {
            byte[] actual = new byte[0];

            actual = Main.b_AddBytes(actual, FileBytes, 0, 0, XfbinParser.GetFileSectionIndex(FileBytes) + 0x2C);
            actual = Main.b_AddBytes(actual, new byte[EntryCount * 0x38]);
            actual = Main.b_AddBytes(actual, new byte[0xC]);

            for(int x = 0; x < EntryCount; x++)
            {
                int entry = XfbinParser.GetFileSectionIndex(FileBytes) + 0x38 + (0x38 * x);

                int characterPointer = actual.Length - (entry + 0x10);
                actual = Main.b_AddString(actual, CharacterList[x]);
                actual = Main.b_AddBytes(actual, new byte[0x8 - CharacterList[x].Length]);
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(characterPointer), entry + 0x10);

                if (x != 0)
                {
                    int cha_a_pointer = actual.Length - (entry + 0x20);
                    actual = Main.b_AddString(actual, c_cha_a_List[x]);
                    actual = Main.b_AddBytes(actual, new byte[0x10 - c_cha_a_List[x].Length]);
                    actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(cha_a_pointer), entry + 0x20);

                    int cha_b_pointer = actual.Length - (entry + 0x28);
                    actual = Main.b_AddString(actual, c_cha_b_List[x]);
                    actual = Main.b_AddBytes(actual, new byte[0x10 - c_cha_b_List[x].Length]);
                    actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(cha_b_pointer), entry + 0x28);
                }
            }

            for (int x = 0; x < EntryCount; x++)
            {
                int entry = XfbinParser.GetFileSectionIndex(FileBytes) + 0x38 + (0x38 * x);
                actual = Main.b_ReplaceBytes(actual, PresetList[x], entry);
                actual = Main.b_ReplaceBytes(actual, CharacodeList[x], entry + 0x4);
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueA[x]), entry + 0x8);
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueB[x]), entry + 0x18);
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueC[x]), entry + 0x1C);
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueD[x]), entry + 0x30);
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueE[x]), entry + 0x34);
            }

            int fileSize = actual.Length - (XfbinParser.GetFileSectionIndex(FileBytes) + 0x28);
            int startFile = XfbinParser.GetFileSectionIndex(FileBytes) + 0x24;
            actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(fileSize), startFile, 1);
            actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(fileSize + 0x4), startFile - 0xC, 1);
            actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(EntryCount), startFile + 0x8);
            actual = Main.b_ReplaceBytes(actual, new byte[] { 0x8 }, startFile + 0xC);
            actual = Main.b_AddBytes(actual, new byte[] { 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x02, 0x00, 0x79, 0x18, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 });

            return actual;
        }

		public byte[] __ConvertToFile()
		{
			List<byte> file = new List<byte>();

			byte[] header = new byte[316]
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
				232,
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
				35,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				32,
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
				112,
				108,
				97,
				121,
				101,
				114,
				83,
				101,
				116,
				116,
				105,
				110,
				103,
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
				112,
				108,
				97,
				121,
				101,
				114,
				83,
				101,
				116,
				116,
				105,
				110,
				103,
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
				0,
				119,
				244,
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
				119,
				240,
				232,
				3,
				0,
				0,
				64,
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
			};
			for (int x4 = 0; x4 < header.Length; x4++)
			{
				file.Add(header[x4]);
			}

			for (int x3 = 0; x3 < EntryCount * 56; x3++)
			{
				file.Add(0);
			}
			List<int> namePointer = new List<int>();
			List<int> chaaPointer = new List<int>();
			List<int> chabPointer = new List<int>();
			for (int x2 = 0; x2 < EntryCount; x2++)
			{
				namePointer.Add(file.Count);
				int nameLength3 = CharacterList[x2].Length;
				for (int a17 = 0; a17 < nameLength3; a17++)
				{
					file.Add((byte)CharacterList[x2][a17]);
				}
				for (int a16 = nameLength3; a16 < 16; a16++)
				{
					file.Add(0);
				}
				chaaPointer.Add(file.Count);
				nameLength3 = c_cha_a_List[x2].Length;
				for (int a15 = 0; a15 < nameLength3; a15++)
				{
					file.Add((byte)c_cha_a_List[x2][a15]);
				}
				for (int a14 = nameLength3; a14 < 16; a14++)
				{
					file.Add(0);
				}
				chabPointer.Add(file.Count);
				nameLength3 = c_cha_b_List[x2].Length;
				for (int a13 = 0; a13 < nameLength3; a13++)
				{
					file.Add((byte)c_cha_b_List[x2][a13]);
				}
				for (int a12 = nameLength3; a12 < 16; a12++)
				{
					file.Add(0);
				}
				for (int a11 = 0; a11 < 4; a11++)
				{
					file.Add(0);
				}
				for (int a10 = 0; a10 < 4; a10++)
				{
					file[0x13C + 0x38 * x2 + a10] = PresetList[x2][a10];
				}
				for (int a9 = 0; a9 < 4; a9++)
				{
					file[0x13C + 0x38 * x2 + 4 + a9] = CharacodeList[x2][a9];
				}
				byte[] o_a = BitConverter.GetBytes(OptValueA[x2]);
				for (int a8 = 0; a8 < 4; a8++)
				{
					file[0x13C + 0x38 * x2 + 8 + a8] = o_a[a8];
				}
				int newPointer3 = namePointer[x2] - 316 - 0x38 * x2 - 16;
				byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);
				for (int a7 = 0; a7 < 4; a7++)
				{
					file[0x13C + 0x38 * x2 + 16 + a7] = ptrBytes3[a7];
				}
				byte[] o_b = BitConverter.GetBytes(OptValueB[x2]);
				for (int a6 = 0; a6 < 4; a6++)
				{
					file[0x13C + 0x38 * x2 + 24 + a6] = o_b[a6];
				}
				byte[] o_c = BitConverter.GetBytes(OptValueC[x2]);
				for (int a5 = 0; a5 < 4; a5++)
				{
					file[0x13C + 0x38 * x2 + 28 + a5] = o_c[a5];
				}
				newPointer3 = chaaPointer[x2] - 316 - 0x38 * x2 - 32;
				ptrBytes3 = BitConverter.GetBytes(newPointer3);
				for (int a4 = 0; a4 < 4; a4++)
				{
					file[0x13C + 0x38 * x2 + 32 + a4] = ptrBytes3[a4];
				}
				newPointer3 = chabPointer[x2] - 316 - 0x38 * x2 - 40;
				ptrBytes3 = BitConverter.GetBytes(newPointer3);
				for (int a3 = 0; a3 < 4; a3++)
				{
					file[0x13C + 0x38 * x2 + 40 + a3] = ptrBytes3[a3];
				}
				byte[] o_d = BitConverter.GetBytes(OptValueD[x2]);
				for (int a2 = 0; a2 < 4; a2++)
				{
					file[0x13C + 0x38 * x2 + 48 + a2] = o_d[a2];
				}
				byte[] o_e = BitConverter.GetBytes(OptValueE[x2]);
				for (int a = 0; a < 4; a++)
				{
					file[0x13C + 0x38 * x2 + 52 + a] = o_e[a];
				}
			}
			int FileSize3 = file.Count - 300;
			byte[] sizeBytes3 = BitConverter.GetBytes(FileSize3);
			int FileSize2 = file.Count - 288 + 4;
			byte[] sizeBytes2 = BitConverter.GetBytes(FileSize2);
			for (int a20 = 0; a20 < 4; a20++)
			{
				file[296 + a20] = sizeBytes3[3 - a20];
			}
			for (int a19 = 0; a19 < 4; a19++)
			{
				file[284 + a19] = sizeBytes3[3 - a19];
			}
			byte[] countBytes = BitConverter.GetBytes(EntryCount);
			for (int a18 = 0; a18 < 4; a18++)
			{
				file[304 + a18] = countBytes[a18];
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

		private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int x = ListBox1.SelectedIndex;
			if (x > -1 && x < ListBox1.Items.Count)
			{
				pid0.Value = PresetList[x][0];
				pid1.Value = PresetList[x][1];
				cid0.Value = CharacodeList[x][0];
				cid1.Value = CharacodeList[x][1];
				opta.Value = OptValueA[x];
				cname.Text = CharacterList[x];
				optb.Value = OptValueB[x];
				optc.Value = OptValueC[x];
				cchaa.Text = c_cha_a_List[x];
				cchab.Text = c_cha_b_List[x];
				optd.Value = OptValueD[x];
				opte.Value = OptValueE[x];
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				DialogResult msg = MessageBox.Show("Are you sure you want to create a new file?", "", MessageBoxButtons.OKCancel);
				if (msg == DialogResult.OK)
				{
					CloseFile();
					NewFile();
				}
			}
			else
			{
				CloseFile();
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

		private void AddButton_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				AddID();
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void RemoveButton_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				RemoveID(ListBox1.SelectedIndex);
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				SaveID();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pid0 = new System.Windows.Forms.NumericUpDown();
            this.pid1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cid1 = new System.Windows.Forms.NumericUpDown();
            this.cid0 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cname = new System.Windows.Forms.TextBox();
            this.cchaa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cchab = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.opta = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.optb = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.optc = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.opte = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.optd = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pid0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cid0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optd)).BeginInit();
            this.SuspendLayout();
            // 
            // ListBox1
            // 
            this.ListBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.ItemHeight = 15;
            this.ListBox1.Location = new System.Drawing.Point(12, 30);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(374, 499);
            this.ListBox1.TabIndex = 0;
            this.ListBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(607, 24);
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
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
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
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(395, 538);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(200, 30);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Create new entry with this data";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(12, 538);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(374, 30);
            this.RemoveButton.TabIndex = 3;
            this.RemoveButton.Text = "Remove selected entry";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(392, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Preset ID (Example: 7B 01)";
            // 
            // pid0
            // 
            this.pid0.Hexadecimal = true;
            this.pid0.Location = new System.Drawing.Point(395, 53);
            this.pid0.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.pid0.Name = "pid0";
            this.pid0.Size = new System.Drawing.Size(97, 20);
            this.pid0.TabIndex = 6;
            // 
            // pid1
            // 
            this.pid1.Hexadecimal = true;
            this.pid1.Location = new System.Drawing.Point(498, 53);
            this.pid1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.pid1.Name = "pid1";
            this.pid1.Size = new System.Drawing.Size(97, 20);
            this.pid1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(392, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Characode ID (Example: C8 00)";
            // 
            // cid1
            // 
            this.cid1.Hexadecimal = true;
            this.cid1.Location = new System.Drawing.Point(498, 100);
            this.cid1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.cid1.Name = "cid1";
            this.cid1.Size = new System.Drawing.Size(97, 20);
            this.cid1.TabIndex = 8;
            // 
            // cid0
            // 
            this.cid0.Hexadecimal = true;
            this.cid0.Location = new System.Drawing.Point(395, 100);
            this.cid0.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.cid0.Name = "cid0";
            this.cid0.Size = new System.Drawing.Size(97, 20);
            this.cid0.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(394, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Character ID (Example: 2nrt00)";
            // 
            // cname
            // 
            this.cname.Location = new System.Drawing.Point(396, 193);
            this.cname.MaxLength = 15;
            this.cname.Name = "cname";
            this.cname.Size = new System.Drawing.Size(200, 20);
            this.cname.TabIndex = 11;
            // 
            // cchaa
            // 
            this.cchaa.Location = new System.Drawing.Point(395, 336);
            this.cchaa.MaxLength = 15;
            this.cchaa.Name = "cchaa";
            this.cchaa.Size = new System.Drawing.Size(200, 20);
            this.cchaa.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Identifier A for c_cha (Optional!)";
            // 
            // cchab
            // 
            this.cchab.Location = new System.Drawing.Point(395, 385);
            this.cchab.MaxLength = 15;
            this.cchab.Name = "cchab";
            this.cchab.Size = new System.Drawing.Size(200, 20);
            this.cchab.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(393, 368);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Identifier B for c_cha (Optional!)";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(395, 506);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(200, 26);
            this.SaveButton.TabIndex = 16;
            this.SaveButton.Text = "Save selected entry";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // opta
            // 
            this.opta.Hexadecimal = true;
            this.opta.Location = new System.Drawing.Point(396, 148);
            this.opta.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.opta.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.opta.Name = "opta";
            this.opta.Size = new System.Drawing.Size(199, 20);
            this.opta.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(393, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Costume Slot ID:";
            // 
            // optb
            // 
            this.optb.Hexadecimal = true;
            this.optb.Location = new System.Drawing.Point(397, 239);
            this.optb.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.optb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.optb.Name = "optb";
            this.optb.Size = new System.Drawing.Size(199, 20);
            this.optb.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(394, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Optional Value B";
            // 
            // optc
            // 
            this.optc.Hexadecimal = true;
            this.optc.Location = new System.Drawing.Point(397, 287);
            this.optc.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.optc.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.optc.Name = "optc";
            this.optc.Size = new System.Drawing.Size(199, 20);
            this.optc.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(394, 271);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Optional Value C";
            // 
            // opte
            // 
            this.opte.Hexadecimal = true;
            this.opte.Location = new System.Drawing.Point(396, 480);
            this.opte.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.opte.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.opte.Name = "opte";
            this.opte.Size = new System.Drawing.Size(199, 20);
            this.opte.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(393, 464);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Optional Value E";
            // 
            // optd
            // 
            this.optd.Hexadecimal = true;
            this.optd.Location = new System.Drawing.Point(396, 432);
            this.optd.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.optd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.optd.Name = "optd";
            this.optd.Size = new System.Drawing.Size(199, 20);
            this.optd.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(393, 416);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Optional Value D";
            // 
            // Tool_PlayerSettingParamEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 577);
            this.Controls.Add(this.opte);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.optd);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.optc);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.optb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.opta);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.cchab);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cchaa);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cid1);
            this.Controls.Add(this.cid0);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pid1);
            this.Controls.Add(this.pid0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.ListBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Tool_PlayerSettingParamEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Player Setting Param Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pid0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cid0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
