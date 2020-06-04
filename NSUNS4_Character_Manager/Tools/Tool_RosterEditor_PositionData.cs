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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(13, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(410, 244);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Value";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 5;
            this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(53, 282);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            500000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(287, 20);
            this.numericUpDown1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(346, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 22);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save entry";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(411, 32);
            this.button2.TabIndex = 4;
            this.button2.Text = "Apply changes";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select a position to edit";
            // 
            // Tool_RosterEditor_PositionData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 347);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Tool_RosterEditor_PositionData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit position data...";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
