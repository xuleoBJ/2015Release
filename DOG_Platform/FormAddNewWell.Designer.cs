namespace DOGPlatform
{
    partial class FormAddNewWell
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
            this.tbxWellName = new System.Windows.Forms.TextBox();
            this.tbxDX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxDY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxKB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbWellType = new System.Windows.Forms.ComboBox();
            this.btnAddWell = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "井名";
            // 
            // tbxWellName
            // 
            this.tbxWellName.Location = new System.Drawing.Point(81, 37);
            this.tbxWellName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxWellName.Name = "tbxWellName";
            this.tbxWellName.Size = new System.Drawing.Size(269, 25);
            this.tbxWellName.TabIndex = 1;
            this.tbxWellName.Text = "NewWell1";
            // 
            // tbxDX
            // 
            this.tbxDX.Location = new System.Drawing.Point(81, 80);
            this.tbxDX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxDX.Name = "tbxDX";
            this.tbxDX.Size = new System.Drawing.Size(269, 25);
            this.tbxDX.TabIndex = 3;
            this.tbxDX.Text = "0.0";
            this.tbxDX.TextChanged += new System.EventHandler(this.tbxDX_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "X";
            // 
            // tbxDY
            // 
            this.tbxDY.Location = new System.Drawing.Point(81, 122);
            this.tbxDY.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxDY.Name = "tbxDY";
            this.tbxDY.Size = new System.Drawing.Size(269, 25);
            this.tbxDY.TabIndex = 5;
            this.tbxDY.Text = "0.0";
            this.tbxDY.TextChanged += new System.EventHandler(this.tbxDY_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y";
            // 
            // tbxKB
            // 
            this.tbxKB.Location = new System.Drawing.Point(80, 167);
            this.tbxKB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxKB.Name = "tbxKB";
            this.tbxKB.Size = new System.Drawing.Size(269, 25);
            this.tbxKB.TabIndex = 7;
            this.tbxKB.Text = "0.0";
            this.tbxKB.TextChanged += new System.EventHandler(this.tbxKB_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 174);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "海拔";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 221);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "井别";
            // 
            // cbbWellType
            // 
            this.cbbWellType.FormattingEnabled = true;
            this.cbbWellType.Location = new System.Drawing.Point(85, 216);
            this.cbbWellType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbbWellType.Name = "cbbWellType";
            this.cbbWellType.Size = new System.Drawing.Size(264, 23);
            this.cbbWellType.TabIndex = 9;
            // 
            // btnAddWell
            // 
            this.btnAddWell.Location = new System.Drawing.Point(138, 265);
            this.btnAddWell.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddWell.Name = "btnAddWell";
            this.btnAddWell.Size = new System.Drawing.Size(100, 29);
            this.btnAddWell.TabIndex = 10;
            this.btnAddWell.Text = "增加入库";
            this.btnAddWell.UseVisualStyleBackColor = true;
            this.btnAddWell.Click += new System.EventHandler(this.btnAddWell_Click);
            // 
            // FormAddNewWell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 337);
            this.Controls.Add(this.btnAddWell);
            this.Controls.Add(this.cbbWellType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxKB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxDY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxDX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxWellName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddNewWell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "井头设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxWellName;
        private System.Windows.Forms.TextBox tbxDX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxDY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxKB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbWellType;
        private System.Windows.Forms.Button btnAddWell;
    }
}