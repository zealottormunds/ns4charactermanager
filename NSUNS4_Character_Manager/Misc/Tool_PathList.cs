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

namespace NSUNS4_Character_Manager.Misc
{
    public partial class Tool_PathList : Form
    {
        public Tool_PathList()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        void OpenFile()
        {
            //CloseFile();
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();

            if (o.FileName == "" || File.Exists(o.FileName) == false) return;

            //filePath = o.FileName;
            byte[] fileBytes = File.ReadAllBytes(o.FileName);

            int fileStart = XfbinParser.GetFileSectionIndex(fileBytes);
            List<string> paths = XfbinParser.GetPathList(fileBytes);
            //paths.Sort();
            for(int x = 0; x < paths.Count; x++) listBox1.Items.Add(paths[x]);

            List<string> namepaths = XfbinParser.GetNameList(fileBytes);
            for (int x = 0; x < namepaths.Count; x++) listBox2.Items.Add(namepaths[x]);
        }
    }
}
