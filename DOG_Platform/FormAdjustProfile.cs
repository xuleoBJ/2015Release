using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace DOGPlatform
{
    public partial class FormAdjustProfile : Form
    {
        public FormAdjustProfile()
        {
            InitializeComponent();
            initializeForm();
        }
        string dirAdjustProfile = Path.Combine(cProjectManager.dirProject, "$AdjustProfile$");
        string fileNamePI = "#injectPI#";
        void initializeForm() 
        {
            lbxJH.DataSource= cProjectData.ltStrProjectJH;
            cbbSelectedLayerName.DataSource = cProjectData.ltStrProjectXCM;
        }

        public static void  readPI()
        {
                //if (File.Exists(cProjectManager.filePathInputHorizonalWellPath))
                //{
                //    using (StreamReader sr = new StreamReader(cProjectManager.filePathInputHorizonalWellPath, System.Text.Encoding.UTF8))
                //    {
                //        String line;
                //        int _indexLine = 0;
                //        int _dataStartLine = 7;
                //        while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                //        {
                //            _indexLine++;
                //            string[] split = line.Trim().Split(new char[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                //            if (_indexLine >= _dataStartLine)
                //            {
                //                ItemHorizonalWellPath _item = new ItemHorizonalWellPath();
                //                _item.sJH = split[0];
                //                _item.dbX = double.Parse(split[1]);
                //                _item.dbY = double.Parse(split[2]);
                //                _item.md = float.Parse(split[3]);
                //                listHorizonalWellPath.Add(_item);

                //            }
                //        }

                //    }
                //}

           
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
        
            this.chart1.ChartAreas[0].AxisX.Title = "时间";
            this.chart1.ChartAreas[0].AxisY.Title = "压降";

            Series series = this.chart1.Series.Add("aaaa");

            for (int i = 0; i < 100; i++)
            {
                chart1.Series["aaaa"].Points.AddXY(i * 10, i * 10);
            } 

            chart1.Series["aaaa"].ChartType = SeriesChartType.Line;
            // Set palette.
            this.chart1.Palette = ChartColorPalette.SeaGreen;

            // Set title.
            this.chart1.Titles.Add("压力下降分析曲线");
        }

        private void btnImportPI_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(dirAdjustProfile)) System.IO.Directory.CreateDirectory(dirAdjustProfile);

        }

        private void btnCopyFromExcelPI_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.DataGridViewCellPaste(dgvPI);
        }

        private void btnDelDgvLinePI_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.DataGridViewCellPaste(dgvPI);
        }
      
    }
}
