using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
	public class Tool_DuelPlayerParamEditor : Form
	{
		public bool FileOpen = false;
		public string FilePath = "";
		public int EntryCount = 0;
		public List<string> BinPath = new List<string>();
		public List<string> BinName = new List<string>();
		public List<byte[]> Data = new List<byte[]>();
		public List<string> CharaList = new List<string>();
		public List<string[]> CostumeList = new List<string[]>();
		public List<string[]> AwkCostumeList = new List<string[]>();
		public List<string> DefaultAssist1 = new List<string>();
		public List<string> DefaultAssist2 = new List<string>();
		public List<string> AwkAction = new List<string>();
		public List<string[]> ItemList = new List<string[]>();
		public List<byte[]> ItemCount = new List<byte[]>();
        public List<string> Partner = new List<string>();

        private IContainer components = null;
		public ListBox listBox1;
		private Button button1;
		private Button button2;
		private Button button3;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem newToolStripMenuItem;
		private ToolStripMenuItem openToolStripMenuItem;
		private ToolStripMenuItem saveToolStripMenuItem;
		private ToolStripMenuItem saveAsToolStripMenuItem;
		private ToolStripMenuItem closeToolStripMenuItem;
		private Label label1;
		private TextBox w_characodeid;
		private Button b_costumeids;
		private Button b_awkcostumeids;
		private TextBox w_awkaction;
		private Label label2;
		private TextBox w_defaultassist1;
		private Label label3;
		private TextBox w_defaultassist2;
		private Label label4;
		private TextBox w_item1;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label label8;
		private NumericUpDown w_itemc1;
		private NumericUpDown w_itemc2;
		private TextBox w_item2;
		private NumericUpDown w_itemc3;
		private TextBox w_item3;
		private NumericUpDown w_itemc4;
        private TextBox w_charaprmbas;
        private Label label9;
        private TextBox w_partner;
        private Label label10;
        private TextBox w_item4;

		public Tool_DuelPlayerParamEditor()
		{
			InitializeComponent();
		}

		public void NewFile()
		{
			FileOpen = true;
			FilePath = "";
			EntryCount = 0;
			BinPath.Clear();
			BinName.Clear();
			Data.Clear();
			CharaList.Clear();
			CostumeList.Clear();
			AwkCostumeList.Clear();
			DefaultAssist1.Clear();
			DefaultAssist2.Clear();
			AwkAction.Clear();
			ItemList.Clear();
			ItemCount.Clear();
            Partner.Clear();
            listBox1.ClearSelected();
			listBox1.Items.Clear();
			EntryCount = 1;
			BinPath.Add("Z:/param/player/Converter/bin/1newprm_bas.bin");
			BinName.Add("1newprm_bas");
			Data.Add(new byte[760]
			{
				50,
				110,
				114,
				116,
				0,
				0,
				0,
				0,
				50,
				110,
				114,
				116,
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
				100,
				110,
				114,
				107,
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
				100,
				110,
				114,
				100,
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
				50,
				110,
				114,
				113,
				0,
				0,
				0,
				0,
				50,
				110,
				114,
				113,
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
				50,
				110,
				114,
				113,
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
				50,
				110,
				114,
				113,
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
				168,
				192,
				1,
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
				45,
				1,
				0,
				0,
				0,
				0,
				0,
				0,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				255,
				0,
				0,
				0,
				0,
				50,
				115,
				107,
				114,
				0,
				0,
				0,
				0,
				50,
				107,
				107,
				115,
				0,
				0,
				0,
				0,
				160,
				0,
				148,
				0,
				148,
				0,
				40,
				0,
				45,
				0,
				110,
				0,
				0,
				0,
				0,
				66,
				0,
				0,
				200,
				66,
				0,
				0,
				128,
				63,
				0,
				0,
				128,
				63,
				0,
				0,
				128,
				63,
				0,
				0,
				128,
				63,
				0,
				0,
				128,
				63,
				0,
				0,
				128,
				63,
				0,
				0,
				128,
				63,
				65,
				87,
				65,
				75,
				69,
				95,
				50,
				78,
				82,
				71,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				32,
				66,
				70,
				0,
				14,
				0,
				25,
				0,
				15,
				0,
				0,
				0,
				0,
				63,
				66,
				65,
				84,
				84,
				76,
				69,
				95,
				73,
				84,
				69,
				77,
				49,
				53,
				48,
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
				2,
				0,
				66,
				65,
				84,
				84,
				76,
				69,
				95,
				73,
				84,
				69,
				77,
				57,
				48,
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
				2,
				0,
				66,
				65,
				84,
				84,
				76,
				69,
				95,
				73,
				84,
				69,
				77,
				57,
				57,
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
				2,
				0,
				66,
				65,
				84,
				84,
				76,
				69,
				95,
				73,
				84,
				69,
				77,
				49,
				52,
				52,
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
				2,
				0,
				0,
				0,
				0,
				66,
				0,
				0,
				200,
				66,
				70,
				0,
				14,
				0,
				25,
				0,
				15,
				0,
				0,
				0,
				0,
				63,
				0,
				0,
				0,
				63,
				0,
				0,
				64,
				63,
				102,
				102,
				230,
				63,
				0,
				0,
				160,
				64,
				0,
				0,
				0,
				64,
				0,
				0,
				128,
				63,
				0,
				0,
				128,
				63,
				0,
				0,
				112,
				65,
				0,
				0,
				0,
				64,
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
				0,
				0,
				0,
				0,
				205,
				204,
				204,
				61,
				205,
				204,
				204,
				61,
				154,
				153,
				153,
				62,
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
				0
			});
			CharaList.Add("1new");
			string[] costumes = new string[20];
			for (int x4 = 0; x4 < 20; x4++)
			{
				costumes[x4] = "";
			}
			CostumeList.Add(costumes);
			string[] awkcostumes = new string[20];
			for (int x3 = 0; x3 < 20; x3++)
			{
				awkcostumes[x3] = "";
			}
			AwkCostumeList.Add(awkcostumes);
			DefaultAssist1.Add("");
			DefaultAssist2.Add("");
			AwkAction.Add("");
			string[] items = new string[4];
			for (int x2 = 0; x2 < 4; x2++)
			{
				items[x2] = "";
			}
			ItemList.Add(items);
			byte[] itemc = new byte[4];
			for (int x = 0; x < 4; x++)
			{
				itemc[x] = 0;
			}
			ItemCount.Add(itemc);
            Partner.Add("");
            listBox1.Items.Add(BinName[0]);
		}

		public void OpenFile(string basepath = "")
		{
			OpenFileDialog o = new OpenFileDialog();
			o.DefaultExt = "xfbin";

            if(basepath == "")
            {
                o.ShowDialog();
            }
            else
            {
                o.FileName = basepath;
            }

			if (!(o.FileName != "") || !File.Exists(o.FileName))
			{
				return;
			}
			FileOpen = true;

			listBox1.Items.Clear();
			EntryCount = 0;
			BinPath.Clear();
			BinName.Clear();
			Data.Clear();
			CharaList.Clear();
			CostumeList.Clear();
			AwkCostumeList.Clear();
			DefaultAssist1.Clear();
			DefaultAssist2.Clear();
			AwkAction.Clear();
			ItemList.Clear();
			ItemCount.Clear();
            Partner.Clear();
            FilePath = o.FileName;
			byte[] FileBytes = File.ReadAllBytes(FilePath);
			EntryCount = Main.b_byteArrayToIntRev(Main.b_ReadByteArray(FileBytes, 36, 4)) - 1;
            if (this.Visible) MessageBox.Show("This file contains " + EntryCount.ToString("X2") + " entries.");
			int Index3 = 128;
			for (int x3 = 0; x3 < EntryCount; x3++)
			{
				string path = Main.b_ReadString(FileBytes, Index3);
				BinPath.Add(path);
				Index3 = Index3 + path.Length + 1;
			}
			Index3++;
			for (int x2 = 0; x2 < EntryCount + 2; x2++)
			{
				string name = Main.b_ReadString(FileBytes, Index3);
				BinName.Add(name);
				Index3 = Index3 + name.Length + 1;
			}
			BinName.RemoveAt(1);
			BinName.RemoveAt(1);
			int StartOfFile = 68 + Main.b_byteArrayToIntRev(Main.b_ReadByteArray(FileBytes, 16, 4));
			for (int x = 0; x < EntryCount; x++)
			{
				List<byte> data = new List<byte>();
				for (int y = 0; y < 760; y++)
				{
					data.Add(FileBytes[StartOfFile + 760 * x + 48 * x + y]);
				}
				Data.Add(data.ToArray());
				int _ptr = StartOfFile + 760 * x + 48 * x;
				string characodeid = Main.b_ReadString(FileBytes, _ptr);
				string[] costumeid = new string[20];
				for (int c2 = 0; c2 < 20; c2++)
				{
					costumeid[c2] = "";
					string cid = Main.b_ReadString(FileBytes, _ptr + 8 + 8 * c2);
					if (cid != "")
					{
						costumeid[c2] = cid;
					}
				}
				string[] awkcostumeid = new string[20];
				for (int c = 0; c < 20; c++)
				{
					awkcostumeid[c] = "";
					string awkcid = Main.b_ReadString(FileBytes, _ptr + 168 + 8 * c);
					if (awkcid != "")
					{
						awkcostumeid[c] = awkcid;
					}
				}
				string defAssist3 = Main.b_ReadString(FileBytes, _ptr + 420);
				string defAssist2 = Main.b_ReadString(FileBytes, _ptr + 428);
				string awkaction = Main.b_ReadString(FileBytes, _ptr + 484);
				string[] itemlist = new string[4];
				byte[] itemcount = new byte[4];
				for (int i = 0; i < 4; i++)
				{
					itemlist[i] = "";
					itemcount[i] = 0;
					string item = Main.b_ReadString(FileBytes, _ptr + 516 + 32 * i);
					byte count = FileBytes[_ptr + 546 + 32 * i];
					if (item != "")
					{
						itemlist[i] = item;
						itemcount[i] = count;
					}
				}
                string partner = Main.b_ReadString(FileBytes, _ptr + 328);
                CharaList.Add(characodeid);
				CostumeList.Add(costumeid);
				AwkCostumeList.Add(awkcostumeid);
				DefaultAssist1.Add(defAssist3);
				DefaultAssist2.Add(defAssist2);
				AwkAction.Add(awkaction);
				ItemList.Add(itemlist);
				ItemCount.Add(itemcount);
                Partner.Add(partner);
                listBox1.Items.Add(BinName[x]);
			}
			Index3++;
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
			NewFile();
			FileOpen = false;
			FilePath = "";
		}

		public void AddEntry()
		{
			int actualEntry = EntryCount;
			EntryCount++;
			BinPath.Add("Z:/param/player/Converter/bin/" + w_charaprmbas.Text + "prm_bas.bin");
			BinName.Add(w_charaprmbas.Text + "prm_bas");
			if (listBox1.SelectedIndex != -1)
			{
				Data.Add(Data[listBox1.SelectedIndex]);
			}
			else
			{
				Data.Add(new byte[760]
				{
					50,
					110,
					114,
					116,
					0,
					0,
					0,
					0,
					50,
					110,
					114,
					116,
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
					100,
					110,
					114,
					107,
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
					100,
					110,
					114,
					100,
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
					50,
					110,
					114,
					113,
					0,
					0,
					0,
					0,
					50,
					110,
					114,
					113,
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
					50,
					110,
					114,
					113,
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
					50,
					110,
					114,
					113,
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
					168,
					192,
					1,
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
					45,
					1,
					0,
					0,
					0,
					0,
					0,
					0,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					255,
					0,
					0,
					0,
					0,
					50,
					115,
					107,
					114,
					0,
					0,
					0,
					0,
					50,
					107,
					107,
					115,
					0,
					0,
					0,
					0,
					160,
					0,
					148,
					0,
					148,
					0,
					40,
					0,
					45,
					0,
					110,
					0,
					0,
					0,
					0,
					66,
					0,
					0,
					200,
					66,
					0,
					0,
					128,
					63,
					0,
					0,
					128,
					63,
					0,
					0,
					128,
					63,
					0,
					0,
					128,
					63,
					0,
					0,
					128,
					63,
					0,
					0,
					128,
					63,
					0,
					0,
					128,
					63,
					65,
					87,
					65,
					75,
					69,
					95,
					50,
					78,
					82,
					71,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					32,
					66,
					70,
					0,
					14,
					0,
					25,
					0,
					15,
					0,
					0,
					0,
					0,
					63,
					66,
					65,
					84,
					84,
					76,
					69,
					95,
					73,
					84,
					69,
					77,
					49,
					53,
					48,
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
					2,
					0,
					66,
					65,
					84,
					84,
					76,
					69,
					95,
					73,
					84,
					69,
					77,
					57,
					48,
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
					2,
					0,
					66,
					65,
					84,
					84,
					76,
					69,
					95,
					73,
					84,
					69,
					77,
					57,
					57,
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
					2,
					0,
					66,
					65,
					84,
					84,
					76,
					69,
					95,
					73,
					84,
					69,
					77,
					49,
					52,
					52,
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
					2,
					0,
					0,
					0,
					0,
					66,
					0,
					0,
					200,
					66,
					70,
					0,
					14,
					0,
					25,
					0,
					15,
					0,
					0,
					0,
					0,
					63,
					0,
					0,
					0,
					63,
					0,
					0,
					64,
					63,
					102,
					102,
					230,
					63,
					0,
					0,
					160,
					64,
					0,
					0,
					0,
					64,
					0,
					0,
					128,
					63,
					0,
					0,
					128,
					63,
					0,
					0,
					112,
					65,
					0,
					0,
					0,
					64,
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
					0,
					0,
					0,
					0,
					205,
					204,
					204,
					61,
					205,
					204,
					204,
					61,
					154,
					153,
					153,
					62,
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
					0
				});
			}
			CharaList.Add(w_characodeid.Text);

			string[] costumes = new string[20];
			for (int x = 0; x < 20; x++)
			{
                costumes[x] = CostumeList[listBox1.SelectedIndex][x];
			}
			CostumeList.Add(costumes);

			string[] awkcostumes = new string[20];
			for (int x = 0; x < 20; x++)
			{
                awkcostumes[x] = AwkCostumeList[listBox1.SelectedIndex][x];
			}
			AwkCostumeList.Add(awkcostumes);

			DefaultAssist1.Add(w_defaultassist1.Text);
			DefaultAssist2.Add(w_defaultassist2.Text);
			AwkAction.Add(w_awkaction.Text);
			ItemList.Add(new string[4]
			{
				w_item1.Text,
				w_item2.Text,
				w_item3.Text,
				w_item4.Text
			});
			ItemCount.Add(new byte[4]
			{
				(byte)w_itemc1.Value,
				(byte)w_itemc2.Value,
				(byte)w_itemc3.Value,
				(byte)w_itemc4.Value
			});
            Partner.Add(w_partner.Text);
            listBox1.Items.Add(BinName[actualEntry]);
			listBox1.SelectedIndex = actualEntry;
		}

		public void RemoveEntry()
		{
			if (listBox1.Items.Count > 1)
			{
				int x = listBox1.SelectedIndex;
				if (x != -1)
				{
					if (listBox1.SelectedIndex > 0)
					{
						listBox1.SelectedIndex--;
					}
					else
					{
						listBox1.ClearSelected();
					}
					EntryCount--;
					BinPath.RemoveAt(x);
					BinName.RemoveAt(x);
					Data.RemoveAt(x);
					CharaList.RemoveAt(x);
					CostumeList.RemoveAt(x);
					AwkCostumeList.RemoveAt(x);
					DefaultAssist1.RemoveAt(x);
					DefaultAssist2.RemoveAt(x);
					AwkAction.RemoveAt(x);
					ItemList.RemoveAt(x);
					ItemCount.RemoveAt(x);
                    Partner.RemoveAt(x);
                    listBox1.Items.RemoveAt(x);
				}
				else
				{
					MessageBox.Show("No entry selected...");
				}
			}
			else
			{
				MessageBox.Show("You can't remove the last entry of this file.");
			}
		}

		public void EditEntry()
		{
			int x = listBox1.SelectedIndex;
			if (x != -1)
			{
				BinPath[x] = "Z:/param/player/Converter/bin/" + w_charaprmbas.Text + "prm_bas.bin";
				BinName[x] = w_charaprmbas.Text + "prm_bas";
				CharaList[x] = w_characodeid.Text;
				DefaultAssist1[x] = w_defaultassist1.Text;
				DefaultAssist2[x] = w_defaultassist2.Text;
				AwkAction[x] = w_awkaction.Text;
				ItemList[x] = new string[4]
				{
					w_item1.Text,
					w_item2.Text,
					w_item3.Text,
					w_item4.Text
				};
				ItemCount[x] = new byte[4]
				{
					(byte)w_itemc1.Value,
					(byte)w_itemc2.Value,
					(byte)w_itemc3.Value,
					(byte)w_itemc4.Value
				};
                Partner[x] = w_partner.Text;
                listBox1.Items[x] = BinName[x];
			}
			else
			{
				MessageBox.Show("No entry selected...");
			}
		}

		public byte[] ConvertToFile()
		{
            // Build the header
			int totalLength4 = 0;

            byte[] fileBytes36 = new byte[0];
			fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[127]
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
				73,
				216,
				0,
				0,
				0,
				3,
				0,
				121,
				20,
				2,
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
				219,
				0,
				0,
				39,
				47,
				0,
				0,
				0,
				221,
				0,
				0,
				10,
				71,
				0,
				0,
				0,
				221,
				0,
				0,
				10,
				92,
				0,
				0,
				3,
				104,
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
				0
			});

            int PtrNucc = fileBytes36.Length;
			fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[1]);

            for (int x6 = 0; x6 < EntryCount; x6++)
			{
				fileBytes36 = Main.b_AddString(fileBytes36, BinPath[x6]);
				fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[1]);
			}

            int PtrPath = fileBytes36.Length;
			fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[1]);

            for (int x5 = 0; x5 < 1; x5++)
			{
				fileBytes36 = Main.b_AddString(fileBytes36, BinName[x5]);
				fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[1]);
			}

            fileBytes36 = Main.b_AddString(fileBytes36, "Page0");
			fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[1]);
			fileBytes36 = Main.b_AddString(fileBytes36, "index");
			fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[1]);

            for (int x4 = 1; x4 < EntryCount; x4++)
			{
				fileBytes36 = Main.b_AddString(fileBytes36, BinName[x4]);
				fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[1]);
			}

            int PtrName = fileBytes36.Length;
			totalLength4 = PtrName;
			int AddedBytes = 0;

            while (fileBytes36.Length % 4 != 0)
			{
				AddedBytes++;
				fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[1]);
			}

            // Build bin1
            totalLength4 = fileBytes36.Length;
			fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[48]
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
				3
			});

            for (int x3 = 1; x3 < EntryCount; x3++)
			{
				int actualEntry = x3 - 1;
				fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[4]
				{
					0,
					0,
					0,
					1
				});
				byte[] xbyte = BitConverter.GetBytes(2 + actualEntry);
				byte[] ybyte = BitConverter.GetBytes(4 + actualEntry);
				fileBytes36 = Main.b_AddBytes(fileBytes36, xbyte, 1);
				fileBytes36 = Main.b_AddBytes(fileBytes36, ybyte, 1);
			}

			int PtrSection = fileBytes36.Length;
			fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[16]
			{
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
				3
			});
			for (int x2 = 1; x2 < EntryCount; x2++)
			{
				int actualEntry2 = x2 - 1;
				fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[4]);
				byte[] xbyte2 = BitConverter.GetBytes(4 + actualEntry2);
				fileBytes36 = Main.b_AddBytes(fileBytes36, xbyte2, 1);
				fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[4]
				{
					0,
					0,
					0,
					2
				});
				fileBytes36 = Main.b_AddBytes(fileBytes36, new byte[4]
				{
					0,
					0,
					0,
					3
				});
			}

			totalLength4 = fileBytes36.Length;

			int PathLength = PtrPath - 127;
			int NameLength = PtrName - PtrPath;
			int Section1Length = PtrSection - PtrName - AddedBytes;
			int FullLength = totalLength4 - 68 + 40;
			int ReplaceIndex8 = 16;
			byte[] buffer8 = BitConverter.GetBytes(FullLength);
			fileBytes36 = Main.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 36;
			buffer8 = BitConverter.GetBytes(EntryCount + 1);
			fileBytes36 = Main.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 40;
			buffer8 = BitConverter.GetBytes(PathLength);
			fileBytes36 = Main.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 44;
			buffer8 = BitConverter.GetBytes(EntryCount + 3);
			fileBytes36 = Main.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 48;
			buffer8 = BitConverter.GetBytes(NameLength);
			fileBytes36 = Main.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 52;
			buffer8 = BitConverter.GetBytes(EntryCount + 3);
			fileBytes36 = Main.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 56;
			buffer8 = BitConverter.GetBytes(Section1Length);
			fileBytes36 = Main.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 60;
			buffer8 = BitConverter.GetBytes(EntryCount * 4);
			fileBytes36 = Main.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			for (int x = 0; x < EntryCount; x++)
			{
				fileBytes36 = ((x != 0) ? Main.b_AddBytes(fileBytes36, new byte[48]
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
					99,
					0,
					0,
					0,
					0,
					2,
					252,
					0,
					0,
					0,
					1,
					0,
					99,
					0,
					0,
					0,
					0,
					2,
					248
				}) : Main.b_AddBytes(fileBytes36, new byte[40]
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
					99,
					0,
					0,
					0,
					0,
					2,
					252,
					0,
					0,
					0,
					1,
					0,
					99,
					0,
					0,
					0,
					0,
					2,
					248
				}));
				fileBytes36 = Main.b_AddBytes(fileBytes36, Data[x].ToArray());
				int _ptr = 68 + FullLength + 48 * x + 760 * x;
				fileBytes36 = Main.b_ReplaceString(fileBytes36, CharaList[x], _ptr, 8);
				for (int i = 0; i < 20; i++)
				{
					fileBytes36 = Main.b_ReplaceString(fileBytes36, CostumeList[x][i], _ptr + 8 + 8 * i, 8);
					fileBytes36 = Main.b_ReplaceString(fileBytes36, AwkCostumeList[x][i], _ptr + 168 + 8 * i, 8);
				}
				fileBytes36 = Main.b_ReplaceString(fileBytes36, DefaultAssist1[x], _ptr + 420, 8);
				fileBytes36 = Main.b_ReplaceString(fileBytes36, DefaultAssist2[x], _ptr + 428, 8);
				fileBytes36 = Main.b_ReplaceString(fileBytes36, AwkAction[x], _ptr + 484, 16);
				fileBytes36 = Main.b_ReplaceString(fileBytes36, ItemList[x][0], _ptr + 516, 16);
				fileBytes36[_ptr + 546] = ItemCount[x][0];
				fileBytes36 = Main.b_ReplaceString(fileBytes36, ItemList[x][1], _ptr + 548, 16);
				fileBytes36[_ptr + 578] = ItemCount[x][1];
				fileBytes36 = Main.b_ReplaceString(fileBytes36, ItemList[x][2], _ptr + 580, 16);
				fileBytes36[_ptr + 610] = ItemCount[x][2];
				fileBytes36 = Main.b_ReplaceString(fileBytes36, ItemList[x][3], _ptr + 612, 16);
				fileBytes36[_ptr + 642] = ItemCount[x][3];
                fileBytes36 = Main.b_ReplaceString(fileBytes36, Partner[x], _ptr + 328);
            }
			return Main.b_AddBytes(fileBytes36, new byte[20]
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
			});
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

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileOpen)
			{
				DialogResult msg = MessageBox.Show("Are you sure you want to open a new file?", "", MessageBoxButtons.OKCancel);
				if (msg == DialogResult.OK)
				{
					CloseFile();
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
				EditEntry();
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
				RemoveEntry();
			}
			else
			{
				MessageBox.Show("No file loaded...");
			}
		}

		private void b_costumeids_Click(object sender, EventArgs e)
		{
			int x = listBox1.SelectedIndex;
			if (x != -1)
			{
				Tool_DuelPlayerParamEditor_Costumes t = new Tool_DuelPlayerParamEditor_Costumes(CostumeList[x].ToArray(), this, x);
				t.ShowDialog();
			}
			else
			{
				MessageBox.Show("No entry selected...");
			}
		}

		private void b_awkcostumeids_Click(object sender, EventArgs e)
		{
			int x = listBox1.SelectedIndex;
			if (x != -1)
			{
				Tool_DuelPlayerParamEditor_Costumes t = new Tool_DuelPlayerParamEditor_Costumes(AwkCostumeList[x].ToArray(), this, x, 0);
				t.ShowDialog();
			}
			else
			{
				MessageBox.Show("No entry selected...");
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int x = listBox1.SelectedIndex;
			if (x != -1)
			{
                w_charaprmbas.Text = BinName[x].Substring(0, BinName[x].Length - 7);
                w_characodeid.Text = CharaList[x];
				w_defaultassist1.Text = DefaultAssist1[x];
				w_defaultassist2.Text = DefaultAssist2[x];
				w_awkaction.Text = AwkAction[x];
				w_item1.Text = ItemList[x][0];
				w_itemc1.Value = ItemCount[x][0];
				w_item2.Text = ItemList[x][1];
				w_itemc2.Value = ItemCount[x][1];
				w_item3.Text = ItemList[x][2];
				w_itemc3.Value = ItemCount[x][2];
				w_item4.Text = ItemList[x][3];
				w_itemc4.Value = ItemCount[x][3];
                w_partner.Text = Partner[x];
            }
			else
			{
                w_charaprmbas.Text = "";
				w_characodeid.Text = "";
				w_defaultassist1.Text = "";
				w_defaultassist2.Text = "";
				w_awkaction.Text = "";
				w_item1.Text = "";
				w_itemc1.Value = 0m;
				w_item2.Text = "";
				w_itemc2.Value = 0m;
				w_item3.Text = "";
				w_itemc3.Value = 0m;
				w_item4.Text = "";
				w_itemc4.Value = 0m;
                w_partner.Text = "";
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.w_characodeid = new System.Windows.Forms.TextBox();
            this.b_costumeids = new System.Windows.Forms.Button();
            this.b_awkcostumeids = new System.Windows.Forms.Button();
            this.w_awkaction = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.w_defaultassist1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.w_defaultassist2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.w_item1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.w_itemc1 = new System.Windows.Forms.NumericUpDown();
            this.w_itemc2 = new System.Windows.Forms.NumericUpDown();
            this.w_item2 = new System.Windows.Forms.TextBox();
            this.w_itemc3 = new System.Windows.Forms.NumericUpDown();
            this.w_item3 = new System.Windows.Forms.TextBox();
            this.w_itemc4 = new System.Windows.Forms.NumericUpDown();
            this.w_item4 = new System.Windows.Forms.TextBox();
            this.w_charaprmbas = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.w_partner = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.w_itemc1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_itemc2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_itemc3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_itemc4)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(13, 39);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(293, 439);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(313, 486);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(352, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add new entry with this data (using the selected entry as a base)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(313, 456);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(352, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Save selected entry";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 486);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(293, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Remove selected entry";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(674, 24);
            this.menuStrip1.TabIndex = 4;
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
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Characode ID";
            // 
            // w_characodeid
            // 
            this.w_characodeid.Location = new System.Drawing.Point(312, 91);
            this.w_characodeid.MaxLength = 8;
            this.w_characodeid.Name = "w_characodeid";
            this.w_characodeid.Size = new System.Drawing.Size(353, 20);
            this.w_characodeid.TabIndex = 6;
            // 
            // b_costumeids
            // 
            this.b_costumeids.Location = new System.Drawing.Point(312, 118);
            this.b_costumeids.Name = "b_costumeids";
            this.b_costumeids.Size = new System.Drawing.Size(353, 23);
            this.b_costumeids.TabIndex = 7;
            this.b_costumeids.Text = "Edit costume ids";
            this.b_costumeids.UseVisualStyleBackColor = true;
            this.b_costumeids.Click += new System.EventHandler(this.b_costumeids_Click);
            // 
            // b_awkcostumeids
            // 
            this.b_awkcostumeids.Location = new System.Drawing.Point(312, 147);
            this.b_awkcostumeids.Name = "b_awkcostumeids";
            this.b_awkcostumeids.Size = new System.Drawing.Size(353, 23);
            this.b_awkcostumeids.TabIndex = 8;
            this.b_awkcostumeids.Text = "Edit awakening costume ids";
            this.b_awkcostumeids.UseVisualStyleBackColor = true;
            this.b_awkcostumeids.Click += new System.EventHandler(this.b_awkcostumeids_Click);
            // 
            // w_awkaction
            // 
            this.w_awkaction.Location = new System.Drawing.Point(312, 199);
            this.w_awkaction.MaxLength = 15;
            this.w_awkaction.Name = "w_awkaction";
            this.w_awkaction.Size = new System.Drawing.Size(353, 20);
            this.w_awkaction.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(239, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Awakening action (optional, from conditionparam)";
            // 
            // w_defaultassist1
            // 
            this.w_defaultassist1.Location = new System.Drawing.Point(313, 340);
            this.w_defaultassist1.MaxLength = 8;
            this.w_defaultassist1.Name = "w_defaultassist1";
            this.w_defaultassist1.Size = new System.Drawing.Size(352, 20);
            this.w_defaultassist1.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Default assist 1:";
            // 
            // w_defaultassist2
            // 
            this.w_defaultassist2.Location = new System.Drawing.Point(313, 384);
            this.w_defaultassist2.MaxLength = 8;
            this.w_defaultassist2.Name = "w_defaultassist2";
            this.w_defaultassist2.Size = new System.Drawing.Size(352, 20);
            this.w_defaultassist2.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(312, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Default assist 2:";
            // 
            // w_item1
            // 
            this.w_item1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.w_item1.Location = new System.Drawing.Point(313, 246);
            this.w_item1.MaxLength = 15;
            this.w_item1.Name = "w_item1";
            this.w_item1.Size = new System.Drawing.Size(128, 23);
            this.w_item1.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Item 1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(498, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Item 2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(498, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Item 4";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(312, 274);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Item 3";
            // 
            // w_itemc1
            // 
            this.w_itemc1.Location = new System.Drawing.Point(447, 246);
            this.w_itemc1.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.w_itemc1.Name = "w_itemc1";
            this.w_itemc1.Size = new System.Drawing.Size(48, 20);
            this.w_itemc1.TabIndex = 24;
            // 
            // w_itemc2
            // 
            this.w_itemc2.Location = new System.Drawing.Point(615, 246);
            this.w_itemc2.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.w_itemc2.Name = "w_itemc2";
            this.w_itemc2.Size = new System.Drawing.Size(48, 20);
            this.w_itemc2.TabIndex = 26;
            // 
            // w_item2
            // 
            this.w_item2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.w_item2.Location = new System.Drawing.Point(501, 246);
            this.w_item2.MaxLength = 15;
            this.w_item2.Name = "w_item2";
            this.w_item2.Size = new System.Drawing.Size(107, 23);
            this.w_item2.TabIndex = 25;
            // 
            // w_itemc3
            // 
            this.w_itemc3.Location = new System.Drawing.Point(447, 291);
            this.w_itemc3.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.w_itemc3.Name = "w_itemc3";
            this.w_itemc3.Size = new System.Drawing.Size(48, 20);
            this.w_itemc3.TabIndex = 28;
            // 
            // w_item3
            // 
            this.w_item3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.w_item3.Location = new System.Drawing.Point(313, 291);
            this.w_item3.MaxLength = 15;
            this.w_item3.Name = "w_item3";
            this.w_item3.Size = new System.Drawing.Size(128, 23);
            this.w_item3.TabIndex = 27;
            // 
            // w_itemc4
            // 
            this.w_itemc4.Location = new System.Drawing.Point(615, 291);
            this.w_itemc4.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.w_itemc4.Name = "w_itemc4";
            this.w_itemc4.Size = new System.Drawing.Size(48, 20);
            this.w_itemc4.TabIndex = 30;
            // 
            // w_item4
            // 
            this.w_item4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.w_item4.Location = new System.Drawing.Point(501, 291);
            this.w_item4.MaxLength = 15;
            this.w_item4.Name = "w_item4";
            this.w_item4.Size = new System.Drawing.Size(107, 23);
            this.w_item4.TabIndex = 29;
            // 
            // w_charaprmbas
            // 
            this.w_charaprmbas.Location = new System.Drawing.Point(312, 50);
            this.w_charaprmbas.MaxLength = 8;
            this.w_charaprmbas.Name = "w_charaprmbas";
            this.w_charaprmbas.Size = new System.Drawing.Size(353, 20);
            this.w_charaprmbas.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(313, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Character ID for prmbas";
            // 
            // w_partner
            // 
            this.w_partner.Location = new System.Drawing.Point(313, 427);
            this.w_partner.MaxLength = 8;
            this.w_partner.Name = "w_partner";
            this.w_partner.Size = new System.Drawing.Size(352, 20);
            this.w_partner.TabIndex = 34;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(312, 411);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Partner:";
            // 
            // Tool_DuelPlayerParamEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 521);
            this.Controls.Add(this.w_partner);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.w_charaprmbas);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.w_itemc4);
            this.Controls.Add(this.w_item4);
            this.Controls.Add(this.w_itemc3);
            this.Controls.Add(this.w_item3);
            this.Controls.Add(this.w_itemc2);
            this.Controls.Add(this.w_item2);
            this.Controls.Add(this.w_itemc1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.w_item1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.w_defaultassist2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.w_defaultassist1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.w_awkaction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.b_awkcostumeids);
            this.Controls.Add(this.b_costumeids);
            this.Controls.Add(this.w_characodeid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Tool_DuelPlayerParamEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Duel Player Param Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.w_itemc1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_itemc2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_itemc3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_itemc4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
