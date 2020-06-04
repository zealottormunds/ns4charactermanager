using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
	public class Tool_DuelPlayerParamEditor_Costumes : Form
	{
		public string[] str_list;

		public int str_index;

		public Tool_DuelPlayerParamEditor tool;

		public int mod;

		private IContainer components = null;

		private ListBox listBox1;

		private Label label1;

		private TextBox textBox1;

		private Button button1;

		private Button button2;

		public Tool_DuelPlayerParamEditor_Costumes(string[] list, Tool_DuelPlayerParamEditor t, int Index, int Mode = -1)
		{
			InitializeComponent();
			str_list = list;
			str_index = Index;
			tool = t;
			mod = Mode;
			if (Mode == 0)
			{
				Text = "Edit awakening costume list";
			}
			for (int x = 0; x < 20; x++)
			{
				string a = "[null]";
				if (str_list[x] != "")
				{
					a = str_list[x];
				}
				listBox1.Items.Add(x.ToString() + " - " + a);
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int x = listBox1.SelectedIndex;
			if (x != -1)
			{
				textBox1.Text = str_list[x];
			}
			else
			{
				textBox1.Text = "";
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			int x = listBox1.SelectedIndex;
			if (x != -1)
			{
				string a = textBox1.Text;
				if (a != "")
				{
					str_list[x] = textBox1.Text;
					listBox1.Items[x] = x.ToString() + " - " + textBox1.Text;
				}
				else
				{
					str_list[x] = "";
					listBox1.Items[x] = x.ToString() + " - " + "[null]";
				}
			}
			else
			{
				textBox1.Text = "";
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (mod == -1)
			{
				tool.CostumeList[str_index] = str_list;
				MessageBox.Show("Costume list saved correctly.");
			}
			else
			{
				tool.AwkCostumeList[str_index] = str_list;
				MessageBox.Show("Awakening costume list saved correctly.");
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(13, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(464, 329);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 354);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Edit entry:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 371);
            this.textBox1.MaxLength = 8;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(348, 20);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(464, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Apply changes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(368, 370);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 22);
            this.button2.TabIndex = 4;
            this.button2.Text = "Submit entry";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Tool_DuelPlayerParamEditor_Costumes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 432);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Name = "Tool_DuelPlayerParamEditor_Costumes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit costume list";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
