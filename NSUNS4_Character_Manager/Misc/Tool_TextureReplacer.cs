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
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using System.Drawing.Imaging;

namespace NSUNS4_Character_Manager.Misc
{
    public partial class Tool_TextureReplacer : Form
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        Image blackImage;
        public Tool_TextureReplacer()
        {
            InitializeComponent();
            blackImage = pictureBox1.Image;
            //AllocConsole();
        }

        public byte[] header_565 = new byte[]{
            0x44, 0x44, 0x53, 0x20, 0x7C, 0x00, 0x00, 0x00, 0x07, 0x10, 0x08, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02,
            0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00,
            0x00, 0x00, 0x00, 0xF8, 0x00, 0x00, 0xE0, 0x07, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00
        };

        public byte[] header_dxt1 = new byte[]
        {
            0x44, 0x44, 0x53, 0x20, 0x7C, 0x00, 0x00, 0x00, 0x07, 0x10, 0x08, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x44, 0x58, 0x54, 0x31, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        public byte[] header_dxt35 = new byte[]
        {
            0x44, 0x44, 0x53, 0x20, 0x7C, 0x00, 0x00, 0x00, 0x07, 0x10, 0x08, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x44, 0x58, 0x54, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        public byte[] header_dxt35m = new byte[]
        {
            0x44, 0x44, 0x53, 0x20, 0x7C, 0x00, 0x00, 0x00, 0x07, 0x10, 0x0A, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x44, 0x58, 0x54, 0x33, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x10, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        bool fileOpen = false;
        byte[] fileBytes = new byte[0];
        string filePath = "";

        List<int> ntp3Indices = new List<int>();
        List<int> ntp3Size = new List<int>();
        List<int> gidxCount = new List<int>();
        List<List<int>> gidxHeaderSizes = new List<List<int>>();
        List<List<int>> gidxIndices = new List<List<int>>();
        List<List<int>> gidxSizes = new List<List<int>>();
        List<byte[]> ntp3Headers = new List<byte[]>();
        List<List<byte[]>> gidxHeaders = new List<List<byte[]>>();
        List<List<byte[]>> textureData = new List<List<byte[]>>();

        void CloseFile()
        {
            if (fileOpen == false) return;

            fileOpen = false;
            fileBytes = new byte[0];
            filePath = "";

            ntp3Indices.Clear();
            ntp3Size.Clear();
            gidxCount.Clear();
            gidxHeaderSizes.Clear();
            gidxIndices.Clear();
            gidxSizes.Clear();
            ntp3Headers.Clear();
            gidxHeaders.Clear();
            textureData.Clear();
            listBox1.SelectedIndex = -1;
            listBox1.Items.Clear();

            w_texsize.Text = "Texture size: ???";
            w_textype.Text = "Texture type: ???";
        }

        void OpenFile()
        {
            CloseFile();

            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();

            if (o.FileName == "" || File.Exists(o.FileName) == false) return;

            filePath = o.FileName;
            fileBytes = File.ReadAllBytes(filePath);

            int fileStart = XfbinParser.GetFileSectionIndex(fileBytes);
            int textureCount = 0;

            ntp3Indices = Main.b_FindBytesList(fileBytes, Encoding.ASCII.GetBytes("NTP3"), fileStart);
            ntp3Size = new List<int>();
            gidxCount = new List<int>();
            gidxIndices = new List<List<int>>();
            gidxSizes = new List<List<int>>();
            textureData = new List<List<byte[]>>();

            for (int x = 0; x < ntp3Indices.Count; x++)
            {                
                int size = Main.b_ReadIntRev(fileBytes, ntp3Indices[x] - 0x4);
                byte count = fileBytes[ntp3Indices[x] + 0x7];

                ntp3Size.Add(size);
                gidxCount.Add(count);
                byte[] ntp3Header = Main.b_ReadByteArray(fileBytes, ntp3Indices[x], 0x10);
                ntp3Headers.Add(ntp3Header);

                gidxIndices.Add(new List<int>());
                gidxSizes.Add(new List<int>());

                // Get first GIDX index
                int firstIndex = ntp3Indices[x] + 0x10;
                gidxIndices[x].Add(firstIndex);

                byte headersize = fileBytes[firstIndex + 0xD];
                gidxHeaderSizes.Add(new List<int>());
                gidxHeaderSizes[x].Add(headersize);

                // Add data for GIDX 0
                int gidxsize = Main.b_ReadIntRev(fileBytes, firstIndex);
                gidxSizes[x].Add(gidxsize);
                int actualGidxIndex = firstIndex;
                byte[] gidxHeader = Main.b_ReadByteArray(fileBytes, actualGidxIndex, headersize);
                gidxHeaders.Add(new List<byte[]>());
                gidxHeaders[x].Add(gidxHeader);

                textureData.Add(new List<byte[]>());
                textureData[x].Add(Main.b_ReadByteArray(fileBytes, actualGidxIndex + headersize, gidxsize - headersize));

                Console.WriteLine(x.ToString() + ": " + textureData[x][0].Length.ToString("X2"));

                actualGidxIndex = firstIndex + gidxsize;

                // Add data for rest of GIDX
                for (int y = 1; y < count; y++)
                {
                    gidxsize = Main.b_ReadIntRev(fileBytes, actualGidxIndex);
                    gidxSizes[x].Add(gidxsize);
                    headersize = fileBytes[actualGidxIndex + 0xD];
                    gidxHeaderSizes[x].Add(headersize);

                    gidxIndices[x].Add(actualGidxIndex);

                    gidxHeader = Main.b_ReadByteArray(fileBytes, actualGidxIndex, headersize);
                    gidxHeaders[x].Add(gidxHeader);

                    textureData[x].Add(Main.b_ReadByteArray(fileBytes, actualGidxIndex + headersize, gidxsize - headersize));
                    Console.WriteLine(x.ToString() + ": " + textureData[x][y].Length.ToString("X2"));

                    actualGidxIndex = actualGidxIndex + gidxsize;
                }

                // Add items to list
                for (int y = 0; y < count; y++)
                {
                    listBox1.Items.Add(
                        "Texture " + x.ToString() + " " + y.ToString() + " " +
                        "- NTP3: " + ntp3Indices[x].ToString("X2") + 
                        ", GIDX: " + gidxIndices[x][y].ToString("X2") + 
                        ", Size: " + gidxSizes[x][y].ToString("X2"));
                }
            }

            fileOpen = true;
        }

        void ImportDDS(int ntp3, int gidx)
        {
            if (fileOpen == false || listBox1.SelectedIndex == -1) return;

            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();

            if (o.FileName == "" || File.Exists(o.FileName) == false) return;

            byte[] texture = File.ReadAllBytes(o.FileName);
            string type_ = Main.b_ReadString(texture, 0x54);

            //MessageBox.Show(type_);
            
            // Replace texture type
            switch(type_)
            {
                default:
                    gidxHeaders[ntp3][gidx][0x13] = 8;
                    break;
                case "DXT1":
                    gidxHeaders[ntp3][gidx][0x13] = 0;
                    break;
                case "DXT3":
                case "DXT5":
                    if (texture[0xA] == 0xA)
                    {
                        gidxHeaders[ntp3][gidx][0x13] = 1;
                        //gidxHeaders[ntp3][gidx][0x9] = 0xA;
                    }
                    else gidxHeaders[ntp3][gidx][0x13] = 2;
                    break;
            }

            // Replace size
            byte[] ddsy = Main.b_ReadByteArray(texture, 0xC, 2);
            byte[] ddsx = Main.b_ReadByteArray(texture, 0x10, 2);

            gidxHeaders[ntp3][gidx][0x14] = ddsx[1];
            gidxHeaders[ntp3][gidx][0x15] = ddsx[0];
            gidxHeaders[ntp3][gidx][0x16] = ddsy[1];
            gidxHeaders[ntp3][gidx][0x17] = ddsy[0];

            textureData[ntp3][gidx] = Main.b_ReadByteArray(texture, 0x80, texture.Length - 0x80);

            MessageBox.Show("Done importing texture.");
        }

        void ExportDDS(int ntp3, int gidx, bool showwin = true, string path = "")
        {
            byte textype = gidxHeaders[ntp3][gidx][0x13];
            byte[] actual = new byte[0];

            switch(textype)
            {
                case 0:
                    actual = Main.b_AddBytes(actual, header_dxt1);
                    break;
                case 1:
                    actual = Main.b_AddBytes(actual, header_dxt35m);
                    break;
                case 2:
                    actual = Main.b_AddBytes(actual, header_dxt35);
                    break;
                case 8:
                    actual = Main.b_AddBytes(actual, header_565);
                    break;
            }

            byte[] resx = new byte[] { 0, 0,
                    gidxHeaders[ntp3][gidx][0x14],
                    gidxHeaders[ntp3][gidx][0x15]
                };

            byte[] resy = new byte[] { 0, 0,
                    gidxHeaders[ntp3][gidx][0x16],
                    gidxHeaders[ntp3][gidx][0x17]
                };

            byte[] sizet = new byte[]
            {
                gidxHeaders[ntp3][gidx][0x8],
                gidxHeaders[ntp3][gidx][0x9],
                gidxHeaders[ntp3][gidx][0xA],
                gidxHeaders[ntp3][gidx][0xB]
            };

            actual = Main.b_ReplaceBytes(actual, resy, 0xC, 1);
            actual = Main.b_ReplaceBytes(actual, resx, 0x10, 1);
            actual = Main.b_ReplaceBytes(actual, sizet, 0x14, 1);
            actual = Main.b_AddBytes(actual, textureData[ntp3][gidx]);

            SaveFileDialog s = new SaveFileDialog();
            if (path == "") s.ShowDialog();
            else s.FileName = path;

            if(s.FileName != "")
            {
                File.WriteAllBytes(s.FileName, actual);
                if(showwin) MessageBox.Show("Texture exported.");
            }
        }

        void SaveFile()
        {
            int ntp3count = gidxHeaders.Count;

            byte[] actual = new byte[0];
            //actual = Main.b_AddBytes(actual, fileBytes, 0, 0, ntp3Indices[0]);
            //int actualIndex = ntp3Indices[0];
            int actualIndex = 0;

            for (int x = 0; x < ntp3count; x++)
            {
                actual = Main.b_AddBytes(actual, fileBytes, 0, actualIndex, (ntp3Indices[x] - actualIndex));
                actualIndex = actualIndex + (ntp3Indices[x] - actualIndex);

                byte gidxcount = (byte)gidxHeaders[x].Count;
                actual = Main.b_AddBytes(actual, fileBytes, 0, ntp3Indices[x], 0x10);
                actual[actual.Length - 0x10 + 0x7] = gidxcount;

                int thisNtp3Size = 0x10;
                for(int y = 0; y < gidxcount; y++)
                {
                    thisNtp3Size = thisNtp3Size + gidxHeaderSizes[x][y];
                    thisNtp3Size = thisNtp3Size + textureData[x][y].Length;
                }
                // Fix size before NTP3
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(thisNtp3Size), actual.Length - 0x10 - 0x4, 1);

                // Fix second size before NTP3
                actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(thisNtp3Size + 4), actual.Length - 0x10 - 0x18, 1);

                for (int y = 0; y < gidxcount; y++)
                {
                    // Add GIDX header
                    actual = Main.b_AddBytes(actual, gidxHeaders[x][y], 0, 0, gidxHeaderSizes[x][y]);

                    // Fix first GIDX size
                    actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(textureData[x][y].Length + gidxHeaderSizes[x][y]), actual.Length - gidxHeaderSizes[x][y], 1);

                    // First second GIDX size
                    actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes(textureData[x][y].Length), actual.Length - gidxHeaderSizes[x][y] + 0x8, 1);

                    actual = Main.b_AddBytes(actual, textureData[x][y]);
                }
                actualIndex = actualIndex + ntp3Size[x];
            }
            actual = Main.b_AddBytes(actual, fileBytes, 0, actualIndex, fileBytes.Length - actualIndex);

            SaveFileDialog s = new SaveFileDialog();
            s.ShowDialog();
            if (s.FileName != "")
            {
                File.WriteAllBytes(s.FileName, actual);
                MessageBox.Show("Saved file.");
            }

            //actual = Main.b_ReplaceBytes(actual, BitConverter.GetBytes())
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        Bitmap bmp;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileOpen == false || listBox1.SelectedIndex == -1) return;

            int x = listBox1.SelectedIndex;
            string[] split = listBox1.Items[x].ToString().Split(' ');
            int actualNtp3 = int.Parse(split[1]);
            int actualGidx = int.Parse(split[2]);

            //MessageBox.Show(actualNtp3.ToString("X2") + ", " + actualGidx.ToString("X2"));

            int resx = Main.b_byteArrayToIntRev(new byte[] { 0, 0,
                    gidxHeaders[actualNtp3][actualGidx][0x14],
                    gidxHeaders[actualNtp3][actualGidx][0x15]
                });

            int resy = Main.b_byteArrayToIntRev(new byte[] { 0, 0,
                    gidxHeaders[actualNtp3][actualGidx][0x16],
                    gidxHeaders[actualNtp3][actualGidx][0x17]
                });

            w_texsize.Text = "Texture size: " + resx.ToString() + "x" + resy.ToString();

            /*string a = "";
            for(int b = 0; b < gidxHeaders[actualNtp3][actualGidx].Length; b++)
            {
                a = a + gidxHeaders[actualNtp3][actualGidx][b].ToString("X2") + " ";
            }
            MessageBox.Show(a);*/

            byte texType = gidxHeaders[actualNtp3][actualGidx][0x13];
            byte[] rawimg = new byte[0];

            switch (texType)
            {
                default:
                    w_textype.Text = "Texture type: Unknown";
                    pictureBox1.Image = blackImage;
                    break;
                case 0:
                    w_textype.Text = "Texture type: DXT1";
                    rawimg = DxtUtil.DecompressDxt1(textureData[actualNtp3][actualGidx], resx, resy);
                    break;
                case 1:
                    w_textype.Text = "Texture type: DXT3/DXT5 with mipmaps";
                    rawimg = DxtUtil.DecompressDxt5(textureData[actualNtp3][actualGidx], resx, resy);
                    break;
                case 2:
                    w_textype.Text = "Texture type: DXT3/DXT5";
                    rawimg = DxtUtil.DecompressDxt5(textureData[actualNtp3][actualGidx], resx, resy);
                    break;
                case 8:
                    w_textype.Text = "Texture type: 5.6.5";
                    pictureBox1.Image = blackImage;
                    break;
            }

            if(rawimg.Length > 0)
            {
                bmp = new Bitmap(resx, resy, PixelFormat.Format32bppArgb);
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, resx, resy), ImageLockMode.WriteOnly, bmp.PixelFormat);
                Marshal.Copy(rawimg, 0, bmpData.Scan0, resx * resy * 4);
                bmp.UnlockBits(bmpData);
                pictureBox1.Image = bmp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileOpen == false || listBox1.SelectedIndex == -1) return;

            int x = listBox1.SelectedIndex;
            string[] split = listBox1.Items[x].ToString().Split(' ');
            int actualNtp3 = int.Parse(split[1]);
            int actualGidx = int.Parse(split[2]);

            ImportDDS(actualNtp3, actualGidx);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileOpen == false) return;

            SaveFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fileOpen == false || listBox1.SelectedIndex == -1) return;

            int x = listBox1.SelectedIndex;
            string[] split = listBox1.Items[x].ToString().Split(' ');
            int actualNtp3 = int.Parse(split[1]);
            int actualGidx = int.Parse(split[2]);

            ExportDDS(actualNtp3, actualGidx);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fileOpen == false) return;

            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog c = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            c.IsFolderPicker = true;

            string path = "";
            if (c.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                path = c.FileName;
            }
            else
            {
                return;
            }

            for (int x = 0; x < listBox1.Items.Count; x++)
            {
                string[] split = listBox1.Items[x].ToString().Split(' ');
                int actualNtp3 = int.Parse(split[1]);
                int actualGidx = int.Parse(split[2]);

                ExportDDS(actualNtp3, actualGidx, false, path + "\\texture" + x.ToString() + ".dds");
            }
        }
    }
}
