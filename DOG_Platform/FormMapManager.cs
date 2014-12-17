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
    public partial class FormMapManager : Form
    {
        public FormMapManager()
        {
            InitializeComponent();
            InitialControl();
        }

         private void InitialControl()
         {
             string[] filenames =Directory.GetFiles(cProjectManager.dirPathMap, "*.svg");
             cPublicMethodForm.inialListBox(lbxMapManager, filenames.ToList());
         }

         private void btnOpenFile_Click(object sender, EventArgs e)
         {
             string filepath = cPublicMethodForm.getSelectedItemTextFromListBox(this.lbxMapManager);
             FormWebNavigation formSVGView = new FormWebNavigation(filepath);
             formSVGView.Show();
         }
   
        

    }
}
