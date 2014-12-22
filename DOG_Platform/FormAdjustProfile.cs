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

        public  void  readPI()
        {
            //string filePath = Path.Combine(dirAdjustProfile, fileNamePI);
            //if (File.Exists(filePath)) { cIOBase.}
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            this.chart1.ChartAreas[0].AxisX.Title = "时间(min)";
            this.chart1.ChartAreas[0].AxisY.Title = "压降(mp)";

            this.chart1.ChartAreas[0].AxisX.Minimum = 0.0;
            Color[] colorSet = new Color[4] { Color.Red, Color.Blue, Color.Green, Color.Purple };
            this.chart1.PaletteCustomColors = colorSet;

            List<string> listJH = cPublicMethodForm.getLtStrOfdgvColoum(this.dgvPI, 0).Distinct().ToList();
            foreach (string sJH in listJH) 
            {
                Series series = this.chart1.Series.Add(sJH);
                //series.Color;

                List<float> listSJ = new List<float>();
                List<float> listValue = new List<float>();

                for (int i = 0; i < dgvPI.Rows.Count; i++)
                {
                    if (dgvPI.Rows[i].Cells[1].Value != null)
                        listSJ.Add(float.Parse(dgvPI.Rows[i].Cells[1].Value.ToString()));
                    else listSJ.Add(0.0f);
                    if (dgvPI.Rows[i].Cells[2].Value != null)
                        listValue.Add(float.Parse(dgvPI.Rows[i].Cells[2].Value.ToString()));
                    else listValue.Add(0.0f);
                }

                for (int i = 0; i<listSJ.Count; i++)
                {
                    chart1.Series[sJH].Points.AddXY(listSJ[i], listValue[i]);
                }

                chart1.Series[sJH].ChartType = SeriesChartType.Line;
            
            }
           
            // Set palette.
           

            // Set title.
            this.chart1.Titles.Add("压力下降分析曲线");
        }

        private void btnImportPI_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(dirAdjustProfile)) System.IO.Directory.CreateDirectory(dirAdjustProfile);
            string filePath=Path.Combine(dirAdjustProfile,fileNamePI);
            cPublicMethodForm.readDataGridView2TXTFile(this.dgvPI, filePath);
            MessageBox.Show("数据导入完成。");
        }

        private void btnCopyFromExcelPI_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.DataGridViewCellPaste(dgvPI);
        }

        private void btnDelDgvLinePI_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.deleteSelectedRowInDataGridView(dgvPI);
        }

        private void tbcAdjustProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcAdjustProfile.SelectedTab == this.tbgPI) 
            {
                string filePath = Path.Combine(dirAdjustProfile, fileNamePI);
                if(File.Exists(filePath)) cPublicMethodForm.read2DataGridViewByTextFile( filePath,this.dgvPI); 
            }
        }

       
      
    }
}
