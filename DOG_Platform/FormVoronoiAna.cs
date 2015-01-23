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
    public partial class FormVoronoiAna : Form
    {
        public FormVoronoiAna()
        {
            InitializeComponent();
            inialForm();
        }
        void inialForm() 
        {
            string[] files = System.IO.Directory.GetFiles(cProjectManager.dirPathUsedProjectData, "*.txt");
            this.cbbFile.DataSource = files;
        }

        private void btnReadFileHead_Click(object sender, EventArgs e)
        {
            string filePathSelect = cbbFile.SelectedItem.ToString();
            string lineHead;

            using (StreamReader reader = new StreamReader(filePathSelect))
            {
                lineHead = reader.ReadLine();
            }
            cbbData.DataSource = null;
            cbbData.DataSource = lineHead.Split();
        }

        private void panelVoronoi_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) pnlColor.BackColor = colorDialog.Color;
        }

      
    }
}
