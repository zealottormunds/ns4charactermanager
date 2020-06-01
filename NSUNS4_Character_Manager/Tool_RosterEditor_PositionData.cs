using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NSUNS4_Character_Manager
{
	public class Tool_RosterEditor_PositionData : Form
	{
		public Tool_RosterEditor tool;

		public int IndexInFile = -1;

		private string[] PositionCategory = new string[42]
		{
			"P1 NOT SELECTED - X: ",
			"P1 NOT SELECTED - Z: ",
			"P1 NOT SELECTED - Y: ",
			"P2 NOT SELECTED - X: ",
			"P2 NOT SELECTED - Z: ",
			"P2 NOT SELECTED - Y: ",
			"P1 SELECTED - X: ",
			"P1 SELECTED - Z: ",
			"P1 SELECTED - Y: ",
			"P2 SELECTED - X: ",
			"P2 SELECTED - Z: ",
			"P2 SELECTED - Y: ",
			"P1 VSLOAD - X: ",
			"P1 VSLOAD - Z: ",
			"P1 VSLOAD - Y: ",
			"P2 VSLOAD - X: ",
			"P2 VSLOAD - Z: ",
			"P2 VSLOAD - Y: ",
			"P1 ROTATION NOT SELECTED: ",
			"P2 ROTATION NOT SELECTED: ",
			"P1 ROTATION SELECTED: ",
			"P2 ROTATION SELECTED: ",
			"P1 ROTATION VSLOAD: ",
			"P2 ROTATION VSLOAD: ",
			"P1 LIGHTING A NOT SELECTED: ",
			"P1 LIGHTING B NOT SELECTED: ",
			"P1 LIGHTING C NOT SELECTED: ",
			"P2 LIGHTING A NOT SELECTED: ",
			"P2 LIGHTING B NOT SELECTED: ",
			"P2 LIGHTING C NOT SELECTED: ",
			"P1 LIGHTING A SELECTED: ",
			"P1 LIGHTING B SELECTED: ",
			"P1 LIGHTING C SELECTED: ",
			"P2 LIGHTING A SELECTED: ",
			"P2 LIGHTING B SELECTED: ",
			"P2 LIGHTING C SELECTED: ",
			"UNKNOWN VALUE A: ",
			"UNKNOWN VALUE B: ",
			"UNKNOWN VALUE C: ",
			"UNKNOWN VALUE D: ",
			"UNKNOWN VALUE E: ",
			"UNKNOWN VALUE F: "
		};

		public List<float> PositionList = new List<float>();

		private IContainer components = null;

		private ListBox listBox1;

		private Label label1;

		private NumericUpDown numericUpDown1;

		private Button button1;

		private Button button2;

		private Label label2;

		public Tool_RosterEditor_PositionData(Tool_RosterEditor t, byte[] positionData, int ListIndex)
		{
			InitializeComponent();
			tool = t;
			IndexInFile = ListIndex;
			PopulateList(positionData);
		}

		public void PopulateList(byte[] positionData)
		{
			for (int x = 0; x < 42; x++)
			{
				float actual = Main.b_ReadFloat(positionData, x * 4);
				string positionCategory = PositionCategory[x];
				PositionList.Add(actual);
				listBox1.Items.Add(positionCategory + PositionList[x]);
			}
		}

		public void UpdateValue()
		{
			int x = listBox1.SelectedIndex;
			if (x != -1)
			{
				PositionList[x] = (float)numericUpDown1.Value;
				listBox1.Items[x] = PositionCategory[x] + PositionList[x];
				numericUpDown1.Value = 0m;
			}
			else
			{
				MessageBox.Show("No entry selected...");
			}
		}

		public void ApplyChanges()
		{
			byte[] gibberish = new byte[0];
			for (int a = 0; a < 42; a++)
			{
				gibberish = Main.b_AddFloat(gibberish, PositionList[a]);
			}
			tool.GibberishBytes[IndexInFile] = gibberish;
			MessageBox.Show("Position data saved.");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			ApplyChanges();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			UpdateValue();
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex != -1)
			{
				numericUpDown1.Value = (decimal)PositionList[listBox1.SelectedIndex];
			}
			else
			{
				numericUpDown1.Value = 0m;
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
			numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			button1 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
			SuspendLayout();
			listBox1.Font = new System.Drawing.Font("Consolas", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			listBox1.FormattingEnabled = true;
			listBox1.ItemHeight = 15;
			listBox1.Location = new System.Drawing.Point(13, 26);
			listBox1.Name = "listBox1";
			listBox1.Size = new System.Drawing.Size(410, 244);
			listBox1.TabIndex = 0;
			listBox1.SelectedIndexChanged += new System.EventHandler(listBox1_SelectedIndexChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(14, 284);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(34, 13);
			label1.TabIndex = 1;
			label1.Text = "Value";
			numericUpDown1.DecimalPlaces = 5;
			numericUpDown1.Increment = new decimal(new int[4]
			{
				10,
				0,
				0,
				0
			});
			numericUpDown1.Location = new System.Drawing.Point(53, 282);
			numericUpDown1.Maximum = new decimal(new int[4]
			{
				500000,
				0,
				0,
				0
			});
			numericUpDown1.Minimum = new decimal(new int[4]
			{
				500000,
				0,
				0,
				-2147483648
			});
			numericUpDown1.Name = "numericUpDown1";
			numericUpDown1.Size = new System.Drawing.Size(287, 20);
			numericUpDown1.TabIndex = 2;
			button1.Location = new System.Drawing.Point(346, 281);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(77, 22);
			button1.TabIndex = 3;
			button1.Text = "Save entry";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			button2.Location = new System.Drawing.Point(12, 306);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(411, 32);
			button2.TabIndex = 4;
			button2.Text = "Apply changes";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(14, 8);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(117, 13);
			label2.TabIndex = 5;
			label2.Text = "Select a position to edit";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(435, 347);
			base.Controls.Add(label2);
			base.Controls.Add(button2);
			base.Controls.Add(button1);
			base.Controls.Add(numericUpDown1);
			base.Controls.Add(label1);
			base.Controls.Add(listBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "Tool_RosterEditor_PositionData";
			Text = "Edit position data...";
			((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
