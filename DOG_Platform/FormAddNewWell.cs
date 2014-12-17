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
    public partial class FormAddNewWell : Form
    {
        public FormAddNewWell()
        {
            InitializeComponent();
            InitFormControl();
        }
        void InitFormControl() 
        {
            List<string> ltStrWellType = new List<string>();
            ltStrWellType.Add("(0)Undefined");
            ltStrWellType.Add("(1)Proposed");
            ltStrWellType.Add("(2)Dry");
            ltStrWellType.Add("(3)Oil");
            ltStrWellType.Add("(4)Minor Oil");
            ltStrWellType.Add("(5)Gas");
            ltStrWellType.Add("(6)Minor gas");
            ltStrWellType.Add("(7)Condensate");
            ltStrWellType.Add("(8)Platform");
            ltStrWellType.Add("(9)Abandoned oil and gas ");
            ltStrWellType.Add("(10)Abandoned oil Minor gas ");
            ltStrWellType.Add("(11)Abandoned oil Condensate ");
            ltStrWellType.Add("(12)Abandoned gas residual oil ");
            ltStrWellType.Add("(13)Abandoned gas condensate ");
            ltStrWellType.Add("(14)Abandoned minor oil and gas ");
            ltStrWellType.Add("(15)Inject water");
            ltStrWellType.Add("(16)Inject gas");
            ltStrWellType.Add("(17)Shallow boreHole");
            ltStrWellType.Add("(18)Drilling well");

            cPublicMethodForm.inialComboBox(cbbWellType, ltStrWellType);
        }

        private void btnAddWell_Click(object sender, EventArgs e)
        {
            ItemWellHead sttNewWell = new ItemWellHead();
            sttNewWell.sJH = tbxWellName.Text;
            sttNewWell.dbX = double.Parse(tbxDX.Text);
            sttNewWell.dbY = double.Parse(tbxDY.Text);
            sttNewWell.fKB = float.Parse(tbxKB.Text);
            sttNewWell.iWellType = cbbWellType.SelectedIndex;
            cIOinputWellHead fileWellHead = new cIOinputWellHead();
            fileWellHead.updateWellHead(sttNewWell);
            cProjectManager.createWellDir(sttNewWell.sJH);
            MessageBox.Show(sttNewWell.sJH+"添加成功。");
            this.Close();
        }

        private void tbxDX_TextChanged(object sender, EventArgs e)
        {
            cPublicMethodForm.inputTextBoxPositiveRealOnly(tbxDX);
        }

        private void tbxDY_TextChanged(object sender, EventArgs e)
        {
            cPublicMethodForm.inputTextBoxPositiveRealOnly(tbxDY);
        }

        private void tbxKB_TextChanged(object sender, EventArgs e)
        {
            cPublicMethodForm.inputTextBoxPositiveRealOnly(tbxKB);
        }
    }
}
