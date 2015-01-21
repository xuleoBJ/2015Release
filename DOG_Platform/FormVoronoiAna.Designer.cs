namespace DOGPlatform
{
    partial class FormVoronoiAna
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
            this.tbcVoronoi = new System.Windows.Forms.TabControl();
            this.tbgVoronoiMap = new System.Windows.Forms.TabPage();
            this.panelVoronoi = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbbData = new System.Windows.Forms.ComboBox();
            this.cbbFile = new System.Windows.Forms.ComboBox();
            this.btnData = new System.Windows.Forms.Button();
            this.btnReadFileHead = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.tbcVoronoi.SuspendLayout();
            this.tbgVoronoiMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcVoronoi
            // 
            this.tbcVoronoi.Controls.Add(this.tbgVoronoiMap);
            this.tbcVoronoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcVoronoi.Location = new System.Drawing.Point(0, 0);
            this.tbcVoronoi.Name = "tbcVoronoi";
            this.tbcVoronoi.SelectedIndex = 0;
            this.tbcVoronoi.Size = new System.Drawing.Size(935, 625);
            this.tbcVoronoi.TabIndex = 2;
            // 
            // tbgVoronoiMap
            // 
            this.tbgVoronoiMap.Controls.Add(this.splitContainer1);
            this.tbgVoronoiMap.Location = new System.Drawing.Point(4, 22);
            this.tbgVoronoiMap.Name = "tbgVoronoiMap";
            this.tbgVoronoiMap.Padding = new System.Windows.Forms.Padding(3);
            this.tbgVoronoiMap.Size = new System.Drawing.Size(927, 599);
            this.tbgVoronoiMap.TabIndex = 5;
            this.tbgVoronoiMap.Text = "Voronoi分布图";
            this.tbgVoronoiMap.UseVisualStyleBackColor = true;
            // 
            // panelVoronoi
            // 
            this.panelVoronoi.BackColor = System.Drawing.Color.White;
            this.panelVoronoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVoronoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVoronoi.Location = new System.Drawing.Point(0, 0);
            this.panelVoronoi.Name = "panelVoronoi";
            this.panelVoronoi.Size = new System.Drawing.Size(921, 509);
            this.panelVoronoi.TabIndex = 2;
            this.panelVoronoi.Paint += new System.Windows.Forms.PaintEventHandler(this.panelVoronoi_Paint);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlColor);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cbbData);
            this.splitContainer1.Panel1.Controls.Add(this.cbbFile);
            this.splitContainer1.Panel1.Controls.Add(this.btnData);
            this.splitContainer1.Panel1.Controls.Add(this.btnReadFileHead);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelVoronoi);
            this.splitContainer1.Size = new System.Drawing.Size(921, 593);
            this.splitContainer1.SplitterDistance = 80;
            this.splitContainer1.TabIndex = 3;
            // 
            // cbbData
            // 
            this.cbbData.FormattingEnabled = true;
            this.cbbData.Location = new System.Drawing.Point(51, 40);
            this.cbbData.Name = "cbbData";
            this.cbbData.Size = new System.Drawing.Size(205, 20);
            this.cbbData.TabIndex = 19;
            // 
            // cbbFile
            // 
            this.cbbFile.FormattingEnabled = true;
            this.cbbFile.Location = new System.Drawing.Point(51, 14);
            this.cbbFile.Name = "cbbFile";
            this.cbbFile.Size = new System.Drawing.Size(648, 20);
            this.cbbFile.TabIndex = 18;
            // 
            // btnData
            // 
            this.btnData.Location = new System.Drawing.Point(262, 38);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(90, 22);
            this.btnData.TabIndex = 17;
            this.btnData.Text = "数据项";
            this.btnData.UseVisualStyleBackColor = true;
            // 
            // btnReadFileHead
            // 
            this.btnReadFileHead.Location = new System.Drawing.Point(720, 12);
            this.btnReadFileHead.Name = "btnReadFileHead";
            this.btnReadFileHead.Size = new System.Drawing.Size(90, 23);
            this.btnReadFileHead.TabIndex = 16;
            this.btnReadFileHead.Text = "确定";
            this.btnReadFileHead.UseVisualStyleBackColor = true;
            this.btnReadFileHead.Click += new System.EventHandler(this.btnReadFileHead_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "文件";
            // 
            // pnlColor
            // 
            this.pnlColor.BackColor = System.Drawing.Color.Blue;
            this.pnlColor.Location = new System.Drawing.Point(407, 44);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(58, 18);
            this.pnlColor.TabIndex = 30;
            this.pnlColor.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(372, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "色系";
            // 
            // FormVoronoiAna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 625);
            this.Controls.Add(this.tbcVoronoi);
            this.Name = "FormVoronoiAna";
            this.Text = "Voronoi分析";
            this.tbcVoronoi.ResumeLayout(false);
            this.tbgVoronoiMap.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcVoronoi;
        private System.Windows.Forms.TabPage tbgVoronoiMap;
        private System.Windows.Forms.Panel panelVoronoi;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbData;
        private System.Windows.Forms.ComboBox cbbFile;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnReadFileHead;
        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Label label6;
    }
}