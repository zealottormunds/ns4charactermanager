namespace NSUNS4_Character_Manager
{
    partial class Tool_AddCharacter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.w_basechar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.w_chara = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.w_page = new System.Windows.Forms.NumericUpDown();
            this.w_pos = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.w_model = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.w_page)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_pos)).BeginInit();
            this.SuspendLayout();
            // 
            // w_basechar
            // 
            this.w_basechar.Location = new System.Drawing.Point(12, 75);
            this.w_basechar.Name = "w_basechar";
            this.w_basechar.Size = new System.Drawing.Size(267, 20);
            this.w_basechar.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Character to use as a base (Example: 1nrt)";
            // 
            // w_chara
            // 
            this.w_chara.Location = new System.Drawing.Point(12, 30);
            this.w_chara.Name = "w_chara";
            this.w_chara.Size = new System.Drawing.Size(267, 20);
            this.w_chara.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Characode ID (Example: 1abc)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Roster page";
            // 
            // w_page
            // 
            this.w_page.Location = new System.Drawing.Point(12, 162);
            this.w_page.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.w_page.Name = "w_page";
            this.w_page.Size = new System.Drawing.Size(267, 20);
            this.w_page.TabIndex = 9;
            this.w_page.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // w_pos
            // 
            this.w_pos.Location = new System.Drawing.Point(12, 205);
            this.w_pos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.w_pos.Name = "w_pos";
            this.w_pos.Size = new System.Drawing.Size(267, 20);
            this.w_pos.TabIndex = 11;
            this.w_pos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Roster position";
            // 
            // w_model
            // 
            this.w_model.Location = new System.Drawing.Point(12, 119);
            this.w_model.Name = "w_model";
            this.w_model.Size = new System.Drawing.Size(267, 20);
            this.w_model.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(259, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Character model (Leave empty if same as Characode)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 237);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(267, 39);
            this.button1.TabIndex = 14;
            this.button1.Text = "Add character";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Tool_AddCharacter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 288);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.w_model);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.w_pos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.w_page);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.w_basechar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.w_chara);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Tool_AddCharacter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add character";
            ((System.ComponentModel.ISupportInitialize)(this.w_page)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_pos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox w_basechar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox w_chara;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown w_page;
        private System.Windows.Forms.NumericUpDown w_pos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox w_model;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
    }
}