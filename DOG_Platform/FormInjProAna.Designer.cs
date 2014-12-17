namespace DOGPlatform
{
    partial class FormInjProAna
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
            this.lbxJH = new System.Windows.Forms.ListBox();
            this.btnCalResult = new System.Windows.Forms.Button();
            this.btnSelectNoWell = new System.Windows.Forms.Button();
            this.btnSelectAllWell = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cbbInjectWell = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbSelectedLayerName = new System.Windows.Forms.ComboBox();
            this.btnSetSelectedJH = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbSlectedYM = new System.Windows.Forms.ComboBox();
            this.dgvInj2Pro = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.属性 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInj2Pro)).BeginInit();
            this.SuspendLayout();
            // 
            // lbxJH
            // 
            this.lbxJH.FormattingEnabled = true;
            this.lbxJH.ItemHeight = 12;
            this.lbxJH.Location = new System.Drawing.Point(67, 46);
            this.lbxJH.Margin = new System.Windows.Forms.Padding(2);
            this.lbxJH.Name = "lbxJH";
            this.lbxJH.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxJH.Size = new System.Drawing.Size(124, 316);
            this.lbxJH.TabIndex = 11;
            // 
            // btnCalResult
            // 
            this.btnCalResult.Location = new System.Drawing.Point(199, 271);
            this.btnCalResult.Margin = new System.Windows.Forms.Padding(2);
            this.btnCalResult.Name = "btnCalResult";
            this.btnCalResult.Size = new System.Drawing.Size(114, 26);
            this.btnCalResult.TabIndex = 15;
            this.btnCalResult.Text = "分析注采";
            this.btnCalResult.UseVisualStyleBackColor = true;
            this.btnCalResult.Click += new System.EventHandler(this.btnCalResult_Click);
            // 
            // btnSelectNoWell
            // 
            this.btnSelectNoWell.Location = new System.Drawing.Point(199, 60);
            this.btnSelectNoWell.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectNoWell.Name = "btnSelectNoWell";
            this.btnSelectNoWell.Size = new System.Drawing.Size(114, 26);
            this.btnSelectNoWell.TabIndex = 14;
            this.btnSelectNoWell.Text = "全不选";
            this.btnSelectNoWell.UseVisualStyleBackColor = true;
            this.btnSelectNoWell.Click += new System.EventHandler(this.btnSelectNoWell_Click);
            // 
            // btnSelectAllWell
            // 
            this.btnSelectAllWell.Location = new System.Drawing.Point(199, 25);
            this.btnSelectAllWell.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectAllWell.Name = "btnSelectAllWell";
            this.btnSelectAllWell.Size = new System.Drawing.Size(114, 26);
            this.btnSelectAllWell.TabIndex = 13;
            this.btnSelectAllWell.Text = "全选";
            this.btnSelectAllWell.UseVisualStyleBackColor = true;
            this.btnSelectAllWell.Click += new System.EventHandler(this.btnSelectAllWell_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "注入井号";
            // 
            // cbbInjectWell
            // 
            this.cbbInjectWell.FormattingEnabled = true;
            this.cbbInjectWell.Location = new System.Drawing.Point(71, 9);
            this.cbbInjectWell.Name = "cbbInjectWell";
            this.cbbInjectWell.Size = new System.Drawing.Size(108, 20);
            this.cbbInjectWell.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "生产井号";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(199, 198);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 26);
            this.button1.TabIndex = 14;
            this.button1.Text = "选择对应层位射开井";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "选择小层";
            // 
            // cbbSelectedLayerName
            // 
            this.cbbSelectedLayerName.FormattingEnabled = true;
            this.cbbSelectedLayerName.Location = new System.Drawing.Point(200, 115);
            this.cbbSelectedLayerName.Name = "cbbSelectedLayerName";
            this.cbbSelectedLayerName.Size = new System.Drawing.Size(114, 20);
            this.cbbSelectedLayerName.TabIndex = 18;
            // 
            // btnSetSelectedJH
            // 
            this.btnSetSelectedJH.Location = new System.Drawing.Point(199, 236);
            this.btnSetSelectedJH.Name = "btnSetSelectedJH";
            this.btnSetSelectedJH.Size = new System.Drawing.Size(114, 26);
            this.btnSetSelectedJH.TabIndex = 20;
            this.btnSetSelectedJH.Text = "确认井号";
            this.btnSetSelectedJH.UseVisualStyleBackColor = true;
            this.btnSetSelectedJH.Click += new System.EventHandler(this.btnSetSelectedJH_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "选择年月";
            // 
            // cbbSlectedYM
            // 
            this.cbbSlectedYM.FormattingEnabled = true;
            this.cbbSlectedYM.Location = new System.Drawing.Point(199, 163);
            this.cbbSlectedYM.Name = "cbbSlectedYM";
            this.cbbSlectedYM.Size = new System.Drawing.Size(114, 20);
            this.cbbSlectedYM.TabIndex = 21;
            // 
            // dgvInj2Pro
            // 
            this.dgvInj2Pro.AllowUserToOrderColumns = true;
            this.dgvInj2Pro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInj2Pro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.属性});
            this.dgvInj2Pro.Location = new System.Drawing.Point(329, 13);
            this.dgvInj2Pro.Name = "dgvInj2Pro";
            this.dgvInj2Pro.RowTemplate.Height = 23;
            this.dgvInj2Pro.Size = new System.Drawing.Size(436, 388);
            this.dgvInj2Pro.TabIndex = 36;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "基准井";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "对应井";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "井距(m)";
            this.Column3.Name = "Column3";
            // 
            // 属性
            // 
            this.属性.HeaderText = "属性";
            this.属性.Name = "属性";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(199, 309);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 26);
            this.button2.TabIndex = 37;
            this.button2.Text = "计算注采井数比";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FormInjProAna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 413);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgvInj2Pro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbSlectedYM);
            this.Controls.Add(this.btnSetSelectedJH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbSelectedLayerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbbInjectWell);
            this.Controls.Add(this.btnCalResult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSelectNoWell);
            this.Controls.Add(this.btnSelectAllWell);
            this.Controls.Add(this.lbxJH);
            this.Name = "FormInjProAna";
            this.Text = "注采对应计算分析";
            this.Load += new System.EventHandler(this.FormInjProAna_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInj2Pro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxJH;
        private System.Windows.Forms.Button btnCalResult;
        private System.Windows.Forms.Button btnSelectNoWell;
        private System.Windows.Forms.Button btnSelectAllWell;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbbInjectWell;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbSelectedLayerName;
        private System.Windows.Forms.Button btnSetSelectedJH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbSlectedYM;
        private System.Windows.Forms.DataGridView dgvInj2Pro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 属性;
        private System.Windows.Forms.Button button2;
    }
}