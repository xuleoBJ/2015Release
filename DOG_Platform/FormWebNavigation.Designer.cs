namespace DOGPlatform
{
    partial class FormWebNavigation
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.inkscapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统自动选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
            this.插入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSand = new System.Windows.Forms.ToolStripMenuItem();
            this.连接层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOilLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWaterLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControlSVGNavigation = new System.Windows.Forms.TabControl();
            this.tbgSVGView = new System.Windows.Forms.TabPage();
            this.webBrowserSVG = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.ToolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.ToolStripContainer1.ContentPanel.SuspendLayout();
            this.ToolStripContainer1.SuspendLayout();
            this.tabControlSVGNavigation.SuspendLayout();
            this.tbgSVGView.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.插入ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(894, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem1,
            this.tsmiEdit});
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.打开文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开文件ToolStripMenuItem1
            // 
            this.打开文件ToolStripMenuItem1.Name = "打开文件ToolStripMenuItem1";
            this.打开文件ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.打开文件ToolStripMenuItem1.Text = "打开文件";
            this.打开文件ToolStripMenuItem1.Click += new System.EventHandler(this.openSVGfile_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inkscapeToolStripMenuItem,
            this.系统自动选择ToolStripMenuItem});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(152, 22);
            this.tsmiEdit.Text = "编辑";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // inkscapeToolStripMenuItem
            // 
            this.inkscapeToolStripMenuItem.Name = "inkscapeToolStripMenuItem";
            this.inkscapeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.inkscapeToolStripMenuItem.Text = "编辑";
            this.inkscapeToolStripMenuItem.Click += new System.EventHandler(this.inkscapeToolStripMenuItem_Click);
            // 
            // 系统自动选择ToolStripMenuItem
            // 
            this.系统自动选择ToolStripMenuItem.Name = "系统自动选择ToolStripMenuItem";
            this.系统自动选择ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.系统自动选择ToolStripMenuItem.Text = "系统自动选择";
            this.系统自动选择ToolStripMenuItem.Click += new System.EventHandler(this.系统自动选择ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.画线ToolStripMenuItem,
            this.tsmiMove,
            this.tsmiDel});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 画线ToolStripMenuItem
            // 
            this.画线ToolStripMenuItem.Name = "画线ToolStripMenuItem";
            this.画线ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.画线ToolStripMenuItem.Text = "画线";
            this.画线ToolStripMenuItem.Click += new System.EventHandler(this.画线ToolStripMenuItem_Click);
            // 
            // tsmiMove
            // 
            this.tsmiMove.Name = "tsmiMove";
            this.tsmiMove.Size = new System.Drawing.Size(100, 22);
            this.tsmiMove.Text = "移动";
            this.tsmiMove.Click += new System.EventHandler(this.tsmiMove_Click);
            // 
            // tsmiDel
            // 
            this.tsmiDel.Name = "tsmiDel";
            this.tsmiDel.Size = new System.Drawing.Size(100, 22);
            this.tsmiDel.Text = "删除";
            this.tsmiDel.Click += new System.EventHandler(this.tsmiDel_Click);
            // 
            // 插入ToolStripMenuItem
            // 
            this.插入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSand,
            this.连接层ToolStripMenuItem});
            this.插入ToolStripMenuItem.Name = "插入ToolStripMenuItem";
            this.插入ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.插入ToolStripMenuItem.Text = "插入";
            // 
            // tsmiSand
            // 
            this.tsmiSand.Name = "tsmiSand";
            this.tsmiSand.Size = new System.Drawing.Size(112, 22);
            this.tsmiSand.Text = "油砂体";
            this.tsmiSand.Click += new System.EventHandler(this.tsmiSand_Click);
            // 
            // 连接层ToolStripMenuItem
            // 
            this.连接层ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOilLayer,
            this.tsmiWaterLayer});
            this.连接层ToolStripMenuItem.Name = "连接层ToolStripMenuItem";
            this.连接层ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.连接层ToolStripMenuItem.Text = "连接层";
            // 
            // tsmiOilLayer
            // 
            this.tsmiOilLayer.Name = "tsmiOilLayer";
            this.tsmiOilLayer.Size = new System.Drawing.Size(100, 22);
            this.tsmiOilLayer.Text = "油层";
            this.tsmiOilLayer.Click += new System.EventHandler(this.tsmiOilLayer_Click);
            // 
            // tsmiWaterLayer
            // 
            this.tsmiWaterLayer.Name = "tsmiWaterLayer";
            this.tsmiWaterLayer.Size = new System.Drawing.Size(100, 22);
            this.tsmiWaterLayer.Text = "水层";
            this.tsmiWaterLayer.Click += new System.EventHandler(this.tsmiWaterLayer_Click);
            // 
            // ToolStripLabel2
            // 
            this.ToolStripLabel2.Name = "ToolStripLabel2";
            this.ToolStripLabel2.Size = new System.Drawing.Size(0, 22);
            // 
            // ToolStripContainer1
            // 
            // 
            // ToolStripContainer1.BottomToolStripPanel
            // 
            this.ToolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // ToolStripContainer1.ContentPanel
            // 
            this.ToolStripContainer1.ContentPanel.Controls.Add(this.tabControlSVGNavigation);
            this.ToolStripContainer1.ContentPanel.Size = new System.Drawing.Size(894, 598);
            this.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStripContainer1.Location = new System.Drawing.Point(0, 25);
            this.ToolStripContainer1.Name = "ToolStripContainer1";
            this.ToolStripContainer1.Size = new System.Drawing.Size(894, 645);
            this.ToolStripContainer1.TabIndex = 4;
            this.ToolStripContainer1.Text = "ToolStripContainer1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(894, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "当前对象:";
            // 
            // tabControlSVGNavigation
            // 
            this.tabControlSVGNavigation.Controls.Add(this.tbgSVGView);
            this.tabControlSVGNavigation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSVGNavigation.Location = new System.Drawing.Point(0, 0);
            this.tabControlSVGNavigation.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlSVGNavigation.Name = "tabControlSVGNavigation";
            this.tabControlSVGNavigation.SelectedIndex = 0;
            this.tabControlSVGNavigation.Size = new System.Drawing.Size(894, 598);
            this.tabControlSVGNavigation.TabIndex = 1;
            this.tabControlSVGNavigation.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControlSVGNavigation_MouseClick);
            // 
            // tbgSVGView
            // 
            this.tbgSVGView.Controls.Add(this.webBrowserSVG);
            this.tbgSVGView.Location = new System.Drawing.Point(4, 22);
            this.tbgSVGView.Margin = new System.Windows.Forms.Padding(2);
            this.tbgSVGView.Name = "tbgSVGView";
            this.tbgSVGView.Padding = new System.Windows.Forms.Padding(2);
            this.tbgSVGView.Size = new System.Drawing.Size(886, 572);
            this.tbgSVGView.TabIndex = 1;
            this.tbgSVGView.Text = "View1";
            this.tbgSVGView.UseVisualStyleBackColor = true;
            // 
            // webBrowserSVG
            // 
            this.webBrowserSVG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserSVG.Location = new System.Drawing.Point(2, 2);
            this.webBrowserSVG.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowserSVG.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowserSVG.Name = "webBrowserSVG";
            this.webBrowserSVG.Size = new System.Drawing.Size(882, 568);
            this.webBrowserSVG.TabIndex = 1;
            // 
            // FormWebNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 670);
            this.Controls.Add(this.ToolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormWebNavigation";
            this.Text = "FormWebNavigation";
            this.Load += new System.EventHandler(this.FormWebNavigation_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ToolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.ToolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.ToolStripContainer1.ContentPanel.ResumeLayout(false);
            this.ToolStripContainer1.ResumeLayout(false);
            this.ToolStripContainer1.PerformLayout();
            this.tabControlSVGNavigation.ResumeLayout(false);
            this.tbgSVGView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem inkscapeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 画线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        private System.Windows.Forms.ToolStripLabel ToolStripLabel2;
        private System.Windows.Forms.ToolStripContainer ToolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统自动选择ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlSVGNavigation;
        private System.Windows.Forms.TabPage tbgSVGView;
        private System.Windows.Forms.WebBrowser webBrowserSVG;
        private System.Windows.Forms.ToolStripMenuItem 插入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSand;
        private System.Windows.Forms.ToolStripMenuItem tsmiMove;
        private System.Windows.Forms.ToolStripMenuItem tsmiDel;
        private System.Windows.Forms.ToolStripMenuItem 连接层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiOilLayer;
        private System.Windows.Forms.ToolStripMenuItem tsmiWaterLayer;
    }
}