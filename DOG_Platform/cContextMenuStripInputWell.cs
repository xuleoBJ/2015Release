using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DOGPlatform
{
    class cContextMenuStripInputWell : cContextMenuStripBaseWell
    {
        ToolStripMenuItem tsmiImportWellDev = new ToolStripMenuItem();
        ToolStripMenuItem tsmiDataView = new ToolStripMenuItem();
        ToolStripMenuItem tsmiImportHoronzinalWellPath = new ToolStripMenuItem();
        ToolStripMenuItem tsmiImportLog = new ToolStripMenuItem();
        ToolStripMenuItem tsmiImportInjectionProfile = new ToolStripMenuItem();
        ToolStripMenuItem tsmiImportData = new ToolStripMenuItem();
        ToolStripMenuItem tsmiDataImport = new ToolStripMenuItem();

        public cContextMenuStripInputWell(ContextMenuStrip _cms, TreeNode _tnSelected, string _sJH)
            : base(_cms, _tnSelected, _sJH)
        {
                     tsmiDataView.Text = "查看井数据";
            tsmiDataView.Click += new System.EventHandler(tsmiDataView_Click);
            tsmiImportLog.Text = "曲线导入";
            tsmiImportLog.Click += new System.EventHandler(tsmiImportLog_Click);

        }
        public void setupTsmiDataView()
        {
            cms.Items.Add(tsmiDataView);
        }
        private void tsmiDataView_Click(object sender, EventArgs e)
        {
            FormDataViewSingleWell formDataView = new FormDataViewSingleWell(this.sJH);
            formDataView.Show();
        }

        public void setupTsmiDataImport()
        {
            tsmiDataImport.Text = "导入井数据";
            tsmiDataImport.DropDownItems.Add(tsmiImportLog);
            cms.Items.Add(tsmiDataImport);
        }



        public void setupTsmiImportLog()
        {
            cms.Items.Add(tsmiImportLog);
        }
        private void tsmiImportLog_Click(object sender, EventArgs e)
        {
            FormDataImportLog frmImportLog = new FormDataImportLog(this.tnSelected.Text);
            frmImportLog.Show();
        }



    }
}
