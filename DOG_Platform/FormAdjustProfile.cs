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
            this.chartPI.ChartAreas[0].AxisX.Title = "时间(min)";
            this.chartPI.ChartAreas[0].AxisY.Title = "压降(mp)";

            this.chartPI.ChartAreas[0].AxisX.Minimum = 0.0;
            chartPI.Series.Clear();

            List<string> ltStrline= cPublicMethodForm.readDataGridView2ListLine(this.dgvPI);
            List<string> listJH = cPublicMethodForm.getLtStrOfdgvColoum(this.dgvPI, 0).Distinct().ToList();
            List<double> ltdfPI = new List<double>();
            foreach (string sJH in listJH) 
            {
                List<string> ltStrLinecurrentJH = cIOBase.getListStrFromStringListByFirstWord(ltStrline, sJH);

                Series series = this.chartPI.Series.Add(sJH);

                List<double> listSJ = new List<double>();
                List<double> listValue = new List<double>();

                foreach (string sLine in ltStrLinecurrentJH) 
                {
                    string[] split = sLine.Split();

                    listSJ.Add(double.Parse(split[1]));
                    listValue.Add(double.Parse(split[2])); 
                }

                ltdfPI.Add(cCalBase.calPI(listSJ,listValue));
                for (int i = 0; i<listSJ.Count; i++)
                {
                    chartPI.Series[sJH].Points.AddXY(listSJ[i], listValue[i]);
                }

                chartPI.Series[sJH].ChartType = SeriesChartType.Line;
            
            }

            chartPI.Palette = ChartColorPalette.Bright;
            this.chartPI.Titles.Add("压力下降分析曲线");

            this.dgvResult.Rows.Clear();
            for (int i = 0; i < listJH.Count;i++ )
            {
                int index = this.dgvResult.Rows.Add();
                this.dgvResult.Rows[index].Cells[0].Value = listJH[i];
                this.dgvResult.Rows[index].Cells[1].Value = "2";
                this.dgvResult.Rows[index].Cells[2].Value = ltdfPI[i].ToString("0.0");
            }
           
            
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
                this.chartPI.ChartAreas[0].AxisX.Title = "时间(min)";
                this.chartPI.ChartAreas[0].AxisY.Title = "压降(mp)";
                this.chartPI.ChartAreas[0].AxisX.Minimum = 0.0;
                chartPI.Series.Clear();
            }
        }

       
      
    }
}
