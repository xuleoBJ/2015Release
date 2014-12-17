namespace DOGPlatform
{
    partial class FormAddSectionLog
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
            this.tpgInfor = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxJH = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.cbbLog = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnAddLog = new System.Windows.Forms.Button();
            this.tpgInfor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpgInfor
            // 
            this.tpgInfor.Controls.Add(this.groupBox1);
            this.tpgInfor.Location = new System.Drawing.Point(4, 22);
            this.tpgInfor.Name = "tpgInfor";
            this.tpgInfor.Padding = new System.Windows.Forms.Padding(3);
            this.tpgInfor.Size = new System.Drawing.Size(463, 127);
            this.tpgInfor.TabIndex = 0;
            this.tpgInfor.Text = "增加曲线";
            this.tpgInfor.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddLog);
            this.groupBox1.Controls.Add(this.cbbLog);
            this.groupBox1.Controls.Add(this.label64);
            this.groupBox1.Controls.Add(this.tbxJH);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(21, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 89);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 70;
            this.label2.Text = "井名";
            // 
            // tbxJH
            // 
            this.tbxJH.Location = new System.Drawing.Point(56, 32);
            this.tbxJH.Name = "tbxJH";
            this.tbxJH.Size = new System.Drawing.Size(70, 21);
            this.tbxJH.TabIndex = 71;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(136, 34);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(41, 12);
            this.label64.TabIndex = 77;
            this.label64.Text = "曲线名";
            // 
            // cbbLog
            // 
            this.cbbLog.BackColor = System.Drawing.SystemColors.Window;
            this.cbbLog.FormattingEnabled = true;
            this.cbbLog.Location = new System.Drawing.Point(183, 31);
            this.cbbLog.Name = "cbbLog";
            this.cbbLog.Size = new System.Drawing.Size(79, 20);
            this.cbbLog.TabIndex = 83;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgInfor);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(471, 153);
            this.tabControl1.TabIndex = 5;
            // 
            // btnAddLog
            // 
            this.btnAddLog.Location = new System.Drawing.Point(289, 28);
            this.btnAddLog.Name = "btnAddLog";
            this.btnAddLog.Size = new System.Drawing.Size(73, 23);
            this.btnAddLog.TabIndex = 84;
            this.btnAddLog.Text = "增加曲线";
            this.btnAddLog.UseVisualStyleBackColor = true;
            this.btnAddLog.Click += new System.EventHandler(this.btnAddLog_Click);
            // 
            // FormAddSectionLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 153);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormAddSectionLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAddSectionLog";
            this.tpgInfor.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpgInfor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbbLog;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox tbxJH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnAddLog;
    }
}