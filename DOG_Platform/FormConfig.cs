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
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            InitFormControl();

        }
        private void InitFormControl()
        {
            cPublicMethodForm.inialComboBox(cbbJH, cProjectData.ltStrProjectJH);
        }


     
    }
}
