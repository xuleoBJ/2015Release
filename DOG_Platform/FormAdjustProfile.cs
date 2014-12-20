using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DOGPlatform
{
    public partial class FormAdjustProfile : Form
    {
        public FormAdjustProfile()
        {
            InitializeComponent();
            initializeForm();
        }
        void initializeForm() 
        {
            lbxJH.DataSource= cProjectData.ltStrProjectJH;
            cbbSelectedLayerName.DataSource = cProjectData.ltStrProjectXCM;
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

   

      
    }
}
