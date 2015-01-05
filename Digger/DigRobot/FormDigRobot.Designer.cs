namespace DigRobot
{
    partial class FormDigRobot
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDigRobot));
            this.项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.OperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSet3ref = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveDataFilePath = new System.Windows.Forms.ToolStripMenuItem();
            this.tismiPickUpdata = new System.Windows.Forms.ToolStripMenuItem();
            this.ptbOriginalPic = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_infor = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip_function = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbcDig = new System.Windows.Forms.TabControl();
            this.tbgPic = new System.Windows.Forms.TabPage();
            this.tbgData = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbOriginalPic)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip_function.SuspendLayout();
            this.tbcDig.SuspendLayout();
            this.tbgPic.SuspendLayout();
            this.SuspendLayout();
            // 
            // 项目ToolStripMenuItem
            // 
            this.项目ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPicToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.项目ToolStripMenuItem.Name = "项目ToolStripMenuItem";
            this.项目ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.项目ToolStripMenuItem.Text = "项目";
            // 
            // openPicToolStripMenuItem
            // 
            this.openPicToolStripMenuItem.Name = "openPicToolStripMenuItem";
            this.openPicToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openPicToolStripMenuItem.Text = "打开图像";
            this.openPicToolStripMenuItem.Click += new System.EventHandler(this.openPicToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帮助文档ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 帮助文档ToolStripMenuItem
            // 
            this.帮助文档ToolStripMenuItem.Name = "帮助文档ToolStripMenuItem";
            this.帮助文档ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.帮助文档ToolStripMenuItem.Text = "帮助文档";
            this.帮助文档ToolStripMenuItem.Click += new System.EventHandler(this.帮助文档ToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.项目ToolStripMenuItem,
            this.OperationToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1269, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // OperationToolStripMenuItem
            // 
            this.OperationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSet3ref,
            this.tsmiSaveDataFilePath,
            this.tismiPickUpdata});
            this.OperationToolStripMenuItem.Enabled = false;
            this.OperationToolStripMenuItem.Name = "OperationToolStripMenuItem";
            this.OperationToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.OperationToolStripMenuItem.Text = "操作";
            // 
            // tsmiSet3ref
            // 
            this.tsmiSet3ref.Name = "tsmiSet3ref";
            this.tsmiSet3ref.Size = new System.Drawing.Size(184, 22);
            this.tsmiSet3ref.Text = "坐标定位";
            this.tsmiSet3ref.Click += new System.EventHandler(this.ToolStripMenuItemSetSystem3Point_Click);
            // 
            // tsmiSaveDataFilePath
            // 
            this.tsmiSaveDataFilePath.Name = "tsmiSaveDataFilePath";
            this.tsmiSaveDataFilePath.Size = new System.Drawing.Size(184, 22);
            this.tsmiSaveDataFilePath.Text = "设置导出位置";
            this.tsmiSaveDataFilePath.Click += new System.EventHandler(this.SaveDataFilePathToolStripMenuItem_Click);
            // 
            // tismiPickUpdata
            // 
            this.tismiPickUpdata.Enabled = false;
            this.tismiPickUpdata.Name = "tismiPickUpdata";
            this.tismiPickUpdata.Size = new System.Drawing.Size(184, 22);
            this.tismiPickUpdata.Text = "输入属性并拾取数据";
            this.tismiPickUpdata.Click += new System.EventHandler(this.tismiPickUpdata_Click);
            // 
            // ptbOriginalPic
            // 
            this.ptbOriginalPic.BackColor = System.Drawing.SystemColors.Control;
            this.ptbOriginalPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ptbOriginalPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ptbOriginalPic.Location = new System.Drawing.Point(3, 3);
            this.ptbOriginalPic.Name = "ptbOriginalPic";
            this.ptbOriginalPic.Size = new System.Drawing.Size(1255, 676);
            this.ptbOriginalPic.TabIndex = 1;
            this.ptbOriginalPic.TabStop = false;
            this.ptbOriginalPic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_OriginalPic_MouseClick);
            this.ptbOriginalPic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_OriginalPic_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_infor});
            this.statusStrip1.Location = new System.Drawing.Point(0, 733);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1269, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_infor
            // 
            this.toolStripStatusLabel_infor.AutoSize = false;
            this.toolStripStatusLabel_infor.Name = "toolStripStatusLabel_infor";
            this.toolStripStatusLabel_infor.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel_infor.Text = "ready";
            this.toolStripStatusLabel_infor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStrip_function
            // 
            this.contextMenuStrip_function.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip_function.Name = "contextMenuStrip_function";
            this.contextMenuStrip_function.Size = new System.Drawing.Size(113, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem1.Text = "井位";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem2.Text = "等值线";
            // 
            // tbcDig
            // 
            this.tbcDig.Controls.Add(this.tbgPic);
            this.tbcDig.Controls.Add(this.tbgData);
            this.tbcDig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcDig.Location = new System.Drawing.Point(0, 25);
            this.tbcDig.Name = "tbcDig";
            this.tbcDig.SelectedIndex = 0;
            this.tbcDig.Size = new System.Drawing.Size(1269, 708);
            this.tbcDig.TabIndex = 3;
            // 
            // tbgPic
            // 
            this.tbgPic.Controls.Add(this.ptbOriginalPic);
            this.tbgPic.Location = new System.Drawing.Point(4, 22);
            this.tbgPic.Name = "tbgPic";
            this.tbgPic.Padding = new System.Windows.Forms.Padding(3);
            this.tbgPic.Size = new System.Drawing.Size(1261, 682);
            this.tbgPic.TabIndex = 0;
            this.tbgPic.Text = "图形";
            this.tbgPic.UseVisualStyleBackColor = true;
            // 
            // tbgData
            // 
            this.tbgData.Location = new System.Drawing.Point(4, 22);
            this.tbgData.Name = "tbgData";
            this.tbgData.Padding = new System.Windows.Forms.Padding(3);
            this.tbgData.Size = new System.Drawing.Size(1261, 682);
            this.tbgData.TabIndex = 1;
            this.tbgData.Text = "数据";
            this.tbgData.UseVisualStyleBackColor = true;
            // 
            // FormDigRobot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 755);
            this.Controls.Add(this.tbcDig);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormDigRobot";
            this.Text = "DigRobot图形数字化工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDigRobot_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDigRobot_FormClosed);
            this.Load += new System.EventHandler(this.FormDigRobot_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbOriginalPic)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip_function.ResumeLayout(false);
            this.tbcDig.ResumeLayout(false);
            this.tbgPic.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem 项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.PictureBox ptbOriginalPic;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_infor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_function;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OperationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSet3ref;
        private System.Windows.Forms.ToolStripMenuItem tismiPickUpdata;
        private System.Windows.Forms.ToolStripMenuItem 帮助文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveDataFilePath;
        private System.Windows.Forms.ToolStripMenuItem openPicToolStripMenuItem;
        private System.Windows.Forms.TabControl tbcDig;
        private System.Windows.Forms.TabPage tbgPic;
        private System.Windows.Forms.TabPage tbgData;

    }
}

