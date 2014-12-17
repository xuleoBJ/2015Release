using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cContextMenuStripSVGGraph : cContextMenuStripBaseGraph
    {
        public cContextMenuStripSVGGraph(ContextMenuStrip _cms, TreeNode _tnSelected, string _sFileName)
            : base(_cms, _tnSelected, _sFileName)
        {

        }

        public void setupTsmiOpenInInkscape()
        {
            ToolStripMenuItem tsmiImportOpenedInkscaple = new ToolStripMenuItem();
            tsmiImportOpenedInkscaple.Text = "编辑";
            tsmiImportOpenedInkscaple.Click += new System.EventHandler(tsmiImportOpenedInkscaple_Click);
            cms.Items.Add(tsmiImportOpenedInkscaple);
        }
        private void tsmiImportOpenedInkscaple_Click(object sender, EventArgs e)
        {
            try
            {
                string svgfilepath = Path.Combine(cProjectManager.dirPathMap, this.sFileName);

                if (svgfilepath != "" && !Directory.Exists(@"C:\Program Files (x86)\Inkscape\inkscape.exe"))
                    System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Inkscape\inkscape.exe", svgfilepath);

            }
            catch (Exception e1)
            {
                MessageBox.Show("未找到编辑模块，请选择其它编辑软件。");
            }
        }

        public void setupTsmiOpenIE()
        {
            ToolStripMenuItem tsmiOpenedIE = new ToolStripMenuItem();
            tsmiOpenedIE.Text = "新窗口打开";
            tsmiOpenedIE.Click += new System.EventHandler(tsmiOpenedIE_Click);
            cms.Items.Add(tsmiOpenedIE);
        }
        private void tsmiOpenedIE_Click(object sender, EventArgs e)
        {
            string svgfilepath = Path.Combine(cProjectManager.dirPathMap, this.sFileName);
            FormWebNavigation formSVGView = new FormWebNavigation(svgfilepath);
            formSVGView.Show();
        }

        public void setupTsmiDeleteFile()
        {
            ToolStripMenuItem tsmiDelete = new ToolStripMenuItem();
            tsmiDelete.Text = "删除";
            tsmiDelete.Click += new System.EventHandler(tsmiDelete_Click);
            cms.Items.Add(tsmiDelete);
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            string _filename=            this.tnSelected.Text;
            string svgfilepath = Path.Combine(cProjectManager.dirPathMap,_filename ); 
            if (File.Exists(svgfilepath))
            {
                DialogResult dialogResult = MessageBox.Show(_filename+" 确认删除？", "删除文件", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    File.Delete(svgfilepath);
                    tnSelected.Remove();
                }
            }
        }

    }
}
