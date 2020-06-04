using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_Character_Manager
{
    public partial class Tool_SkillCustomizeParamEditor : Form
    {
        public Tool_SkillCustomizeParamEditor()
        {
            InitializeComponent();
        }

        bool fileOpen = false;
        string filePath = "";
        int fileStart = 0;
        byte[] fileBytes;
        byte[] header = new byte[0];
        public int entryCount = 0;

        public List<int> charaList = new List<int>();
        public List<float[]> valueList = new List<float[]>();
        public List<string[]> jyuList = new List<string[]>();

        void OpenFile(string path = "")
        {
            OpenFileDialog o = new OpenFileDialog();
            o.DefaultExt = "xfbin";

            if (path == "")
            {
                o.ShowDialog();
            }
            else
            {
                o.FileName = path;
            }

            if (!(o.FileName != "") || !File.Exists(o.FileName))
            {
                return;
            }

            filePath = o.FileName;
            fileOpen = true;
            byte[] fileBytes = File.ReadAllBytes(filePath);

            fileStart = XfbinParser.GetFileSectionIndex(fileBytes);
            header = Main.b_AddBytes(header, fileBytes, 0, fileStart, 0x38);
            fileStart = fileStart + 0x28;

            entryCount = Main.b_ReadInt(fileBytes, fileStart + 0x4);

            for(int x = 0; x < entryCount; x++)
            {

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
    }
}
