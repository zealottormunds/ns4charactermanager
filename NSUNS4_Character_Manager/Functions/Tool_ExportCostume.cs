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

namespace NSUNS4_Character_Manager.Functions
{
    public partial class Tool_ExportCostume : Form
    {
        public Tool_ExportCostume()
        {
            InitializeComponent();
        }

        byte[] fileBytes = new byte[0];
        byte[] costumexfbin = new byte[0];

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();

            if(o.FileName != "" && File.Exists(o.FileName))
            {
                costumexfbin = new byte[0];
                costumexfbin = File.ReadAllBytes(o.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (w_base.Text == "")
            {
                MessageBox.Show("Please input the base character of the costume.");
                return;
            }

            if (w_model.Text == "")
            {
                MessageBox.Show("Please input the model name.");
                return;
            }

            if(costumexfbin.Length == 0)
            {
                MessageBox.Show("Please select your .xfbin model file.");
                return;
            }

            fileBytes = new byte[0];
            fileBytes = Main.b_AddString(fileBytes, "NS4CS");
            fileBytes = Main.b_AddBytes(fileBytes, new byte[1]);

            fileBytes = Main.b_AddString(fileBytes, w_base.Text);
            fileBytes = Main.b_AddBytes(fileBytes, new byte[1]);

            fileBytes = Main.b_AddString(fileBytes, w_model.Text);
            fileBytes = Main.b_AddBytes(fileBytes, new byte[1]);

            fileBytes = Main.b_AddInt(fileBytes, costumexfbin.Length);
            fileBytes = Main.b_AddBytes(fileBytes, costumexfbin);

            SaveFileDialog s = new SaveFileDialog();
            s.DefaultExt = ".ns4";
            s.AddExtension = true;
            s.ShowDialog();

            if(s.FileName != "")
            {
                File.WriteAllBytes(s.FileName, fileBytes);
                MessageBox.Show("Costume exported as .ns4 file.");
            }
        }
    }
}
