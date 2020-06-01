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
			listBox1 = new System.Windows.Forms.ListBox();
			label1 = new System.Windows.Forms.Label();
			textBox1 = new System.Windows.Forms.TextBox();
			button1 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			SuspendLayout();
			listBox1.FormattingEnabled = true;
			listBox1.Location = new System.Drawing.Point(13, 13);
			listBox1.Name = "listBox1";
			listBox1.Size = new System.Drawing.Size(464, 329);
			listBox1.TabIndex = 0;
			listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 354);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(54, 13);
			label1.TabIndex = 1;
			label1.Text = "Edit entry:";
			textBox1.Location = new System.Drawing.Point(13, 371);
			textBox1.MaxLength = 8;
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(348, 20);
			textBox1.TabIndex = 2;
			button1.Location = new System.Drawing.Point(13, 397);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(464, 23);
			button1.TabIndex = 3;
			button1.Text = "Apply changes";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			button2.Location = new System.Drawing.Point(368, 370);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(109, 22);
			button2.TabIndex = 4;
			button2.Text = "Submit entry";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(489, 432);
			base.Controls.Add(button2);
			base.Controls.Add(button1);
			base.Controls.Add(textBox1);
			base.Controls.Add(label1);
			base.Controls.Add(listBox1);
			base.Name = "Tool_DuelPlayerParamEditor_Costumes";
			Text = "Edit costume list";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
