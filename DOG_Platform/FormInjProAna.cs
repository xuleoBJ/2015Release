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
        List<string> ltStrWellName = new List<string>();
        List<double> dfListX = new List<double>();
        List<double> dfListY = new List<double>();
        List<float> fListKB = new List<float>();
        List<int> iListWellType = new List<int>();
       
        List<string> ltStrProductWell = new List<string>();
        List<string> ltStrInjectWell = new List<string>();
        string sInjectWellSelected;
        List<string> ltStrProductWellSelected = new List<string>();
        string sSelectedLayer;

        public void readWellHead()
        {
            string[] split;
            try
            {
                using (StreamReader sr = new StreamReader(cProjectManager.filePathInputWellhead, System.Text.Encoding.Default))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        split = line.Trim().Split(new char[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        ltStrWellName.Add(split[0]);
                        dfListX.Add(double.Parse(split[1]));
                        dfListY.Add(double.Parse(split[2]));
                        fListKB.Add(float.Parse(split[3]));
                        iListWellType.Add(int.Parse(split[4]));
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void InitControl()
        {

            readWellHead();

            cPublicMethodForm.inialComboBox(cbbSelectedLayerName, cProjectData.ltStrProjectXCM);
          
           
          
            for (int i = 0; i < this.ltStrWellName.Count; i++)
            {
                if (this.iListWellType[i] == 15)
                    ltStrInjectWell.Add(ltStrWellName[i]);
                else
                    ltStrProductWell.Add(ltStrWellName[i]);
            }
            cPublicMethodForm.inialListBox(lbxJH, ltStrProductWell);
            cPublicMethodForm.inialComboBox(cbbInjectWell, ltStrInjectWell);

            cPublicMethodForm.inialComboBox(cbbSlectedYM, cProjectData.ltStrProjectYM);
   

       }

        private void btnCalResult_Click(object sender, EventArgs e)
        {

            string sInjectWellSelected = this.cbbInjectWell.SelectedItem.ToString();

            List<string> ltStrSelectedJH = cPublicMethodForm.ltStrSelectedItemsReturnFromListBox(this.lbxJH);

        
            string fileName = cProjectManager.dirPathUsedProjectData + sInjectWellSelected + "-WellDistance.txt";
          //  cCalDistance.calWellHeadWellDistance(sInjectWellSelected, ltStrSelectedJH, fileName);

            cPublicMethodForm.read2DataGridViewByTextFile(fileName, this.dgvInj2Pro);
            
        }

        private void btnSelectAllWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setListBoxChooseAll(lbxJH);
        }

        private void btnSelectNoWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setListBoxChooseNo(lbxJH);
        }

        private void btnSetSelectedJH_Click(object sender, EventArgs e)
        {

            ltStrProductWellSelected.Clear();
            ltStrProductWellSelected = cPublicMethodForm.ltStrSelectedItemsReturnFromListBox(lbxJH);
            sInjectWellSelected = this.cbbInjectWell.SelectedItem.ToString();
            MessageBox.Show(string.Join("\t", ltStrProductWellSelected.ToArray()), "Num=" + ltStrProductWellSelected.Count.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {

            cIOinputPerforation cTest = new cIOinputPerforation();
            sSelectedLayer = this.cbbSelectedLayerName.SelectedItem.ToString();
            string sSelectedYM = this.cbbSlectedYM.SelectedItem.ToString();
            //ltStrProductWellSelected = cTest.selectJHFromDicPerforationByYMLayerName(sSelectedLayer, sSelectedYM);
            for (int i = 0; i < lbxJH.Items.Count; i++)
            {
                lbxJH.SetSelected(i, false);
                if (ltStrProductWellSelected.Contains(lbxJH.Items[i].ToString()))
                    lbxJH.SetSelected(i, true);
            }

        }

        private void FormInjProAna_Load(object sender, EventArgs e)
        {

        }
    }
}
