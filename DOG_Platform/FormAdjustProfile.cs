using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

            string[] typeArray = { "01.01.2011", "02.01.2011", "03.01.2011", "04.01.2011", "05.01.2011" };
            int[] pointsArray = { 1, 3, 9, 4, 2 };
            int i = 5;
        
            this.chart1.ChartAreas[0].AxisX.Title = "时间";
            this.chart1.ChartAreas[0].AxisY.Title = "压降";
        }

      
    }
}
