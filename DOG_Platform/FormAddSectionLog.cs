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
    public partial class FormAddSectionLog : Form
    {
        public FormAddSectionLog(string _sJH, TreeNode _tnNode, string _filepathGoal)
        {
            InitializeComponent();
            this.sJH = _sJH;
            this.tnNode = _tnNode;
            this.filepathGoal = _filepathGoal;
            initializeControls();
        }
        void initializeControls()
        {
            this.tbxJH.Text = sJH;
            cFileOperateDicLogHeadProject cProjectDicLogItems = new cFileOperateDicLogHeadProject();
            List<string> ltStrLogName = cProjectDicLogItems.getItemByJH(sJH).Select(p => p.sLogName).ToList();
            cPublicMethodForm.inialComboBox(cbbLog, ltStrLogName);
        }
        public string sJH { get; set; }
        public TreeNode tnNode { get; set; }
        public string filepathGoal { get; set; }
        private void btnAddLog_Click(object sender, EventArgs e)
        {
            string selectedLogName = cbbLog.SelectedItem.ToString();
            //ItemLogHeadInfor ItemLogHeadInfor =cProjectData.itemListProjectLogHead.First(p=>p.sJH==sJH&&p.sLogName ==selectedLogName);
            //cIOinputLog fileLog = new cIOinputLog();
            //fileLog.extractTextLog2File(sJH, selectedLogName, ItemLogHeadInfor, filepathGoal + "\\" + selectedLogName+".txt");
            tnNode.Nodes.Add(selectedLogName);
            this.Close();
        }
    }
}
