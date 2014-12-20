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

            //string[] typeArray = { "01.01.2011", "02.01.2011", "03.01.2011", "04.01.2011", "05.01.2011" };
            //int[] pointsArray = { 1, 3, 9, 4, 2 };
            //int i = 5;
        
            this.chart1.ChartAreas[0].AxisX.Title = "时间";
            this.chart1.ChartAreas[0].AxisY.Title = "压降";

            // Data arrays.
            string[] seriesArray = { "Cats", "Dogs" };
            int[] pointsArray = { 1, 2 };

            // Set palette.
            this.chart1.Palette = ChartColorPalette.SeaGreen;

            // Set title.
            this.chart1.Titles.Add("压力下降分析曲线");

            // Add series.
            for (int i = 0; i < seriesArray.Length; i++)
            {
                // Add series.
                Series series = this.chart1.Series.Add(seriesArray[i]);

                // Add point.
                series.Points.Add(pointsArray[i]);
            }
        }

      
    }
}
