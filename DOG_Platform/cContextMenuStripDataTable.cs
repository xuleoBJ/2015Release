using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms; 

namespace DOGPlatform
{
    class cContextMenuStripDataTable : cContextMenuStripTreeNodeBase
    {
        public cContextMenuStripDataTable(ContextMenuStrip _cms, TreeNode _tnSelected, string _sFileName)
            : base(_cms, _tnSelected, _sFileName)
        {

        }

        public void setupTsmiOpenNewWindow()
        {
            ToolStripMenuItem tsmiOpenedNewWin = new ToolStripMenuItem();
            tsmiOpenedNewWin.Text = "窗口打开";
            tsmiOpenedNewWin.Click += new System.EventHandler(tsmiOpenedNewWin_Click);
            cms.Items.Add(tsmiOpenedNewWin);
        }
        private void tsmiOpenedNewWin_Click(object sender, EventArgs e)
        {
              FormDataTable formDatatable = new FormDataTable(this.sFileName);
              formDatatable.Show();
        }
    }
}
