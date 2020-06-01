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

		private ListBox ListBox1;

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

		public void OpenFile()
		{
			OpenFileDialog o = new OpenFileDialog();
			o.DefaultExt = "xfbin";
			o.ShowDialog();
			if (!(o.FileName != "") || !File.Exists(o.FileName))
			{
				return;
			}
			ClearFile();
			FileOpen = true;
			FilePath = o.FileName;
			byte[] FileBytes = File.ReadAllBytes(FilePath);
			EntryCount = FileBytes[304] + FileBytes[305] * 256 + FileBytes[306] * 65536 + FileBytes[307] * 16777216;
			for (int x2 = 0; x2 < EntryCount; x2++)
			{
				long _ptr = 316 + 56 * x2;
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
				int OptA = FileBytes[_ptr + 8] + FileBytes[_ptr + 9] * 256 + FileBytes[_ptr + 10] * 65536 + FileBytes[_ptr + 11] * 16777216;
				string CharacterID = "";
				long _ptrCharacter3 = FileBytes[_ptr + 16] + FileBytes[_ptr + 17] * 256 + FileBytes[_ptr + 18] * 65536 + FileBytes[_ptr + 19] * 16777216;
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
				int OptB = FileBytes[_ptr + 24] + FileBytes[_ptr + 25] * 256 + FileBytes[_ptr + 26] * 65536 + FileBytes[_ptr + 27] * 16777216;
				int OptC = FileBytes[_ptr + 28] + FileBytes[_ptr + 29] * 256 + FileBytes[_ptr + 30] * 65536 + FileBytes[_ptr + 31] * 16777216;
				string c_cha_a = "";
				_ptrCharacter3 = FileBytes[_ptr + 32] + FileBytes[_ptr + 33] * 256 + FileBytes[_ptr + 34] * 65536 + FileBytes[_ptr + 35] * 16777216;
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
				_ptrCharacter3 = FileBytes[_ptr + 40] + FileBytes[_ptr + 41] * 256 + FileBytes[_ptr + 42] * 65536 + FileBytes[_ptr + 43] * 16777216;
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
				int OptD = FileBytes[_ptr + 48] + FileBytes[_ptr + 49] * 256 + FileBytes[_ptr + 50] * 65536 + FileBytes[_ptr + 51] * 16777216;
				int OptE = FileBytes[_ptr + 52] + FileBytes[_ptr + 53] * 256 + FileBytes[_ptr + 54] * 65536 + FileBytes[_ptr + 55] * 16777216;
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
			MessageBox.Show("Entry added.");
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
					file[316 + 56 * x2 + a10] = PresetList[x2][a10];
				}
				for (int a9 = 0; a9 < 4; a9++)
				{
					file[316 + 56 * x2 + 4 + a9] = CharacodeList[x2][a9];
				}
				byte[] o_a = BitConverter.GetBytes(OptValueA[x2]);
				for (int a8 = 0; a8 < 4; a8++)
				{
					file[316 + 56 * x2 + 8 + a8] = o_a[a8];
				}
				int newPointer3 = namePointer[x2] - 316 - 56 * x2 - 16;
				byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);
				for (int a7 = 0; a7 < 4; a7++)
				{
					file[316 + 56 * x2 + 16 + a7] = ptrBytes3[a7];
				}
				byte[] o_b = BitConverter.GetBytes(OptValueB[x2]);
				for (int a6 = 0; a6 < 4; a6++)
				{
					file[316 + 56 * x2 + 24 + a6] = o_b[a6];
				}
				byte[] o_c = BitConverter.GetBytes(OptValueC[x2]);
				for (int a5 = 0; a5 < 4; a5++)
				{
					file[316 + 56 * x2 + 28 + a5] = o_c[a5];
				}
				newPointer3 = chaaPointer[x2] - 316 - 56 * x2 - 32;
				ptrBytes3 = BitConverter.GetBytes(newPointer3);
				for (int a4 = 0; a4 < 4; a4++)
				{
					file[316 + 56 * x2 + 32 + a4] = ptrBytes3[a4];
				}
				newPointer3 = chabPointer[x2] - 316 - 56 * x2 - 40;
				ptrBytes3 = BitConverter.GetBytes(newPointer3);
				for (int a3 = 0; a3 < 4; a3++)
				{
					file[316 + 56 * x2 + 40 + a3] = ptrBytes3[a3];
				}
				byte[] o_d = BitConverter.GetBytes(OptValueD[x2]);
				for (int a2 = 0; a2 < 4; a2++)
				{
					file[316 + 56 * x2 + 48 + a2] = o_d[a2];
				}
				byte[] o_e = BitConverter.GetBytes(OptValueE[x2]);
				for (int a = 0; a < 4; a++)
				{
					file[316 + 56 * x2 + 52 + a] = o_e[a];
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
			ListBox1 = new System.Windows.Forms.ListBox();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			AddButton = new System.Windows.Forms.Button();
			RemoveButton = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			pid0 = new System.Windows.Forms.NumericUpDown();
			pid1 = new System.Windows.Forms.NumericUpDown();
			label2 = new System.Windows.Forms.Label();
			cid1 = new System.Windows.Forms.NumericUpDown();
			cid0 = new System.Windows.Forms.NumericUpDown();
			label3 = new System.Windows.Forms.Label();
			cname = new System.Windows.Forms.TextBox();
			cchaa = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			cchab = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			SaveButton = new System.Windows.Forms.Button();
			opta = new System.Windows.Forms.NumericUpDown();
			label6 = new System.Windows.Forms.Label();
			optb = new System.Windows.Forms.NumericUpDown();
			label7 = new System.Windows.Forms.Label();
			optc = new System.Windows.Forms.NumericUpDown();
			label8 = new System.Windows.Forms.Label();
			opte = new System.Windows.Forms.NumericUpDown();
			label9 = new System.Windows.Forms.Label();
			optd = new System.Windows.Forms.NumericUpDown();
			label10 = new System.Windows.Forms.Label();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pid0).BeginInit();
			((System.ComponentModel.ISupportInitialize)pid1).BeginInit();
			((System.ComponentModel.ISupportInitialize)cid1).BeginInit();
			((System.ComponentModel.ISupportInitialize)cid0).BeginInit();
			((System.ComponentModel.ISupportInitialize)opta).BeginInit();
			((System.ComponentModel.ISupportInitialize)optb).BeginInit();
			((System.ComponentModel.ISupportInitialize)optc).BeginInit();
			((System.ComponentModel.ISupportInitialize)opte).BeginInit();
			((System.ComponentModel.ISupportInitialize)optd).BeginInit();
			SuspendLayout();
			ListBox1.Font = new System.Drawing.Font("Consolas", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			ListBox1.FormattingEnabled = true;
			ListBox1.ItemHeight = 15;
			ListBox1.Location = new System.Drawing.Point(12, 30);
			ListBox1.Name = "ListBox1";
			ListBox1.Size = new System.Drawing.Size(374, 499);
			ListBox1.TabIndex = 0;
			ListBox1.SelectedIndexChanged += new System.EventHandler(ListBox1_SelectedIndexChanged);
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				fileToolStripMenuItem
			});
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(607, 24);
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
			newToolStripMenuItem.Click += new System.EventHandler(newToolStripMenuItem_Click);
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
			AddButton.Location = new System.Drawing.Point(395, 538);
			AddButton.Name = "AddButton";
			AddButton.Size = new System.Drawing.Size(200, 30);
			AddButton.TabIndex = 2;
			AddButton.Text = "Create new entry with this data";
			AddButton.UseVisualStyleBackColor = true;
			AddButton.Click += new System.EventHandler(AddButton_Click);
			RemoveButton.Location = new System.Drawing.Point(12, 538);
			RemoveButton.Name = "RemoveButton";
			RemoveButton.Size = new System.Drawing.Size(374, 30);
			RemoveButton.TabIndex = 3;
			RemoveButton.Text = "Remove selected entry";
			RemoveButton.UseVisualStyleBackColor = true;
			RemoveButton.Click += new System.EventHandler(RemoveButton_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(392, 37);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(134, 13);
			label1.TabIndex = 5;
			label1.Text = "Preset ID (Example: 7B 01)";
			pid0.Hexadecimal = true;
			pid0.Location = new System.Drawing.Point(395, 53);
			pid0.Maximum = new decimal(new int[4]
			{
				255,
				0,
				0,
				0
			});
			pid0.Name = "pid0";
			pid0.Size = new System.Drawing.Size(97, 20);
			pid0.TabIndex = 6;
			pid1.Hexadecimal = true;
			pid1.Location = new System.Drawing.Point(498, 53);
			pid1.Maximum = new decimal(new int[4]
			{
				255,
				0,
				0,
				0
			});
			pid1.Name = "pid1";
			pid1.Size = new System.Drawing.Size(97, 20);
			pid1.TabIndex = 6;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(392, 84);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(156, 13);
			label2.TabIndex = 7;
			label2.Text = "Characode ID (Example: C8 00)";
			cid1.Hexadecimal = true;
			cid1.Location = new System.Drawing.Point(498, 100);
			cid1.Maximum = new decimal(new int[4]
			{
				255,
				0,
				0,
				0
			});
			cid1.Name = "cid1";
			cid1.Size = new System.Drawing.Size(97, 20);
			cid1.TabIndex = 8;
			cid0.Hexadecimal = true;
			cid0.Location = new System.Drawing.Point(395, 100);
			cid0.Maximum = new decimal(new int[4]
			{
				255,
				0,
				0,
				0
			});
			cid0.Name = "cid0";
			cid0.Size = new System.Drawing.Size(97, 20);
			cid0.TabIndex = 9;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(394, 176);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(152, 13);
			label3.TabIndex = 10;
			label3.Text = "Character ID (Example: 2nrt00)";
			cname.Location = new System.Drawing.Point(396, 193);
			cname.MaxLength = 15;
			cname.Name = "cname";
			cname.Size = new System.Drawing.Size(200, 20);
			cname.TabIndex = 11;
			cchaa.Location = new System.Drawing.Point(395, 336);
			cchaa.MaxLength = 15;
			cchaa.Name = "cchaa";
			cchaa.Size = new System.Drawing.Size(200, 20);
			cchaa.TabIndex = 13;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(393, 319);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(156, 13);
			label4.TabIndex = 12;
			label4.Text = "Identifier A for c_cha (Optional!)";
			cchab.Location = new System.Drawing.Point(395, 385);
			cchab.MaxLength = 15;
			cchab.Name = "cchab";
			cchab.Size = new System.Drawing.Size(200, 20);
			cchab.TabIndex = 15;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(393, 368);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(156, 13);
			label5.TabIndex = 14;
			label5.Text = "Identifier B for c_cha (Optional!)";
			SaveButton.Location = new System.Drawing.Point(395, 506);
			SaveButton.Name = "SaveButton";
			SaveButton.Size = new System.Drawing.Size(200, 26);
			SaveButton.TabIndex = 16;
			SaveButton.Text = "Save selected entry";
			SaveButton.UseVisualStyleBackColor = true;
			SaveButton.Click += new System.EventHandler(SaveButton_Click);
			opta.Hexadecimal = true;
			opta.Location = new System.Drawing.Point(396, 148);
			opta.Maximum = new decimal(new int[4]
			{
				-1,
				0,
				0,
				0
			});
			opta.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			opta.Name = "opta";
			opta.Size = new System.Drawing.Size(199, 20);
			opta.TabIndex = 18;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(393, 132);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(86, 13);
			label6.TabIndex = 17;
			label6.Text = "Costume Slot ID:";
			optb.Hexadecimal = true;
			optb.Location = new System.Drawing.Point(397, 239);
			optb.Maximum = new decimal(new int[4]
			{
				-1,
				0,
				0,
				0
			});
			optb.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			optb.Name = "optb";
			optb.Size = new System.Drawing.Size(199, 20);
			optb.TabIndex = 20;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(394, 223);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(86, 13);
			label7.TabIndex = 19;
			label7.Text = "Optional Value B";
			optc.Hexadecimal = true;
			optc.Location = new System.Drawing.Point(397, 287);
			optc.Maximum = new decimal(new int[4]
			{
				-1,
				0,
				0,
				0
			});
			optc.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			optc.Name = "optc";
			optc.Size = new System.Drawing.Size(199, 20);
			optc.TabIndex = 22;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(394, 271);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(86, 13);
			label8.TabIndex = 21;
			label8.Text = "Optional Value C";
			opte.Hexadecimal = true;
			opte.Location = new System.Drawing.Point(396, 480);
			opte.Maximum = new decimal(new int[4]
			{
				-1,
				0,
				0,
				0
			});
			opte.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			opte.Name = "opte";
			opte.Size = new System.Drawing.Size(199, 20);
			opte.TabIndex = 26;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(393, 464);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(86, 13);
			label9.TabIndex = 25;
			label9.Text = "Optional Value E";
			optd.Hexadecimal = true;
			optd.Location = new System.Drawing.Point(396, 432);
			optd.Maximum = new decimal(new int[4]
			{
				-1,
				0,
				0,
				0
			});
			optd.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				-2147483648
			});
			optd.Name = "optd";
			optd.Size = new System.Drawing.Size(199, 20);
			optd.TabIndex = 24;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(393, 416);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(87, 13);
			label10.TabIndex = 23;
			label10.Text = "Optional Value D";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(607, 577);
			base.Controls.Add(opte);
			base.Controls.Add(label9);
			base.Controls.Add(optd);
			base.Controls.Add(label10);
			base.Controls.Add(optc);
			base.Controls.Add(label8);
			base.Controls.Add(optb);
			base.Controls.Add(label7);
			base.Controls.Add(opta);
			base.Controls.Add(label6);
			base.Controls.Add(SaveButton);
			base.Controls.Add(cchab);
			base.Controls.Add(label5);
			base.Controls.Add(cchaa);
			base.Controls.Add(label4);
			base.Controls.Add(cname);
			base.Controls.Add(label3);
			base.Controls.Add(cid1);
			base.Controls.Add(cid0);
			base.Controls.Add(label2);
			base.Controls.Add(pid1);
			base.Controls.Add(pid0);
			base.Controls.Add(label1);
			base.Controls.Add(RemoveButton);
			base.Controls.Add(AddButton);
			base.Controls.Add(ListBox1);
			base.Controls.Add(menuStrip1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MainMenuStrip = menuStrip1;
			base.Name = "Tool_PlayerSettingParamEditor";
			Text = "Player Setting Param Editor";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pid0).EndInit();
			((System.ComponentModel.ISupportInitialize)pid1).EndInit();
			((System.ComponentModel.ISupportInitialize)cid1).EndInit();
			((System.ComponentModel.ISupportInitialize)cid0).EndInit();
			((System.ComponentModel.ISupportInitialize)opta).EndInit();
			((System.ComponentModel.ISupportInitialize)optb).EndInit();
			((System.ComponentModel.ISupportInitialize)optc).EndInit();
			((System.ComponentModel.ISupportInitialize)opte).EndInit();
			((System.ComponentModel.ISupportInitialize)optd).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
