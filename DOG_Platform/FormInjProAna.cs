using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DOGPlatform
{
    public partial class FormInjProAna : Form
    {
        public FormInjProAna()
        {
            InitializeComponent();
            InitControl();
        }
       
        List<string> ltJHProductWell = new List<string>();
        List<string> ltJHInjectWell = new List<string>();
        string sInjectWellSelected;
        List<string> ltJHProductWellSelected = new List<string>();
        string sSelectedLayer;

        private void InitControl()
        {
            ltJHInjectWell = cIODicWellType.getJHListInject();
            ltJHProductWell = cIODicWellType.getJHListProduct();
            cPublicMethodForm.inialComboBox(cbbSelectedLayerName, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbWellProduct, ltJHProductWell);
            cPublicMethodForm.inialComboBox(cbbWellInject, ltJHInjectWell);
       }

        private void btnCalResult_Click(object sender, EventArgs e)
        {
            string sInjectWellSelected = this.cbbWellInject.SelectedItem.ToString();
            string fileName = cProjectManager.dirPathUsedProjectData + sInjectWellSelected + "-WellDistance.txt";

            float skhd = 0; //根据井号，小层号查找射孔厚度

          //  cCalDistance.calWellHeadWellDistance(sInjectWellSelected, ltStrSelectedJH, fileName);
            cPublicMethodForm.read2DataGridViewByTextFile(fileName, this.dgvInj2Pro);
        }



        private void btnAddConnectWell_Click(object sender, EventArgs e)
        {
            int index = this.dgvInj2Pro.Rows.Add();
            this.dgvInj2Pro.Rows[index].Cells[0].Value = cbbWellInject.SelectedItem.ToString();
            this.dgvInj2Pro.Rows[index].Cells[1].Value = cbbWellProduct.SelectedItem.ToString();
        }

        private void btnDelConnectJH_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.deleteSelectedRowInDataGridView(dgvInj2Pro);
        }

     

        
    }
}
