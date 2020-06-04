namespace NSUNS4_Character_Manager
{
    partial class Tool_AddCostume
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
            this.label1 = new System.Windows.Forms.Label();
            this.w_base = new System.Windows.Forms.TextBox();
            this.w_model = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base character (Example: 2nrt)";
            // 
            // w_base
            // 
            this.w_base.Location = new System.Drawing.Point(13, 30);
            this.w_base.Name = "w_base";
            this.w_base.Size = new System.Drawing.Size(229, 20);
            this.w_base.TabIndex = 1;
            // 
            // w_model
            // 
            this.w_model.Location = new System.Drawing.Point(13, 75);
            this.w_model.Name = "w_model";
            this.w_model.Size = new System.Drawing.Size(229, 20);
            this.w_model.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Model to use:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 34);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add costume";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Tool_AddCostume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 152);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.w_model);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.w_base);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Tool_AddCostume";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add costume";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox w_base;
        public System.Windows.Forms.TextBox w_model;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button button1;
    }
}