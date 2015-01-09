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
    public partial class FormCalReservor : Form
    {
        Voronoi voroObject;
        public FormCalReservor()
        {
            InitializeComponent();
            setPanel();
            voroObject = new Voronoi(1);
        }

        private void dgvPayPropery_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPageWellPotion_Click(object sender, EventArgs e)
        {

        }


        void setPanel()
        {
            if (cProjectData.ltStrProjectJH.Count > 0)
            {
                int iSacleUnit = 500; //定义网格单位
                if (cProjectData.dfMapScale == 0) cProjectData.dfMapScale = 0.1;
                cProjectData.dfMapXrealRefer = Math.Floor(cProjectData.listProjectWell.Min(p => p.dbX) / iSacleUnit - 1) * iSacleUnit;
                cProjectData.dfMapYrealRefer = (Math.Ceiling(cProjectData.listProjectWell.Max(p => p.dbY) / iSacleUnit) + 1) * iSacleUnit;

                double xMaxDistance = cProjectData.listProjectWell.Max(p => p.dbX) - cProjectData.listProjectWell.Min(p => p.dbX);
                double yMaxDistance = cProjectData.listProjectWell.Max(p => p.dbY) - cProjectData.listProjectWell.Min(p => p.dbY);

                int iPanelWidth = Convert.ToInt32((int)(xMaxDistance / iSacleUnit + 3) * iSacleUnit * cProjectData.dfMapScale);//显示好看pannel比最大大3个网格
                int iPanelHeight = Convert.ToInt32((int)(yMaxDistance / iSacleUnit + 3) * iSacleUnit * cProjectData.dfMapScale);//显示好看pannel比最大大3个网格
                panelResCal.Dock = System.Windows.Forms.DockStyle.None;

                panelResCal.Width = iPanelWidth;
                panelResCal.Height = iPanelHeight;
                panelResCal.Location = new Point(0, 0);
                 
                this.panelResCal.Invalidate();
                this.panelResCal.Focus();
            }

        }
        void spreadPoints(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;

            List<PointF> sites = new List<PointF>();

            foreach (ItemWell well in cProjectData.listProjectWell)
            {
                PointF headView = cCordinationTransform.transRealPointF2ViewPoint(
                    well.dbX, well.dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                sites.Add(headView);
                Pen wellPen = new Pen(Color.Black, 2);
                if (well.iWellType == 3) wellPen = new Pen(Color.Red, 2);
                else if (well.iWellType == 5) wellPen = new Pen(Color.Green, 2);
                else if (well.iWellType == 15) wellPen = new Pen(Color.Blue, 2);

                Pen blackPen = new Pen(Color.Black, 1);
                dc.DrawEllipse(wellPen, headView.X-1.5f, headView.Y-1.5f, 3, 3);
                Brush blackBrush = Brushes.Black;
                Font font = new Font("黑体", 8);
                dc.DrawString(well.sJH, font, blackBrush,
                               headView.X + 3, headView.Y + 3);
            }
            
            List<GraphEdge> ge;
            ge = MakeVoronoiGraph(sites, panelResCal.Width , panelResCal.Height);

            this.tbxOut.Text = "sites=" + sites.Count.ToString() + "GraphEdge:" + ge.Count.ToString() + "\r\n";
            // رسم أضلاع فورونوي
            for (int i = 0; i < ge.Count; i++)
            {
                try
                {
                    Point p1 = new Point((int)ge[i].x1, (int)ge[i].y1);
                    Point p2 = new Point((int)ge[i].x2, (int)ge[i].y2);
                    dc.DrawLine(Pens.Black, p1.X, p1.Y, p2.X, p2.Y);
                }
                catch
                {
                    string s = "\nP " + i + " size1" + ge[i].site1 + "size2" + ge[i].site2 +" "+ ge[i].x1 + ", " + ge[i].y1 + " || " + ge[i].x2 + ", " + ge[i].y2;
                }
                string s1 = "P " + i + ": size1" + ge[i].site1 + " size2" + ge[i].site2 + " " + ge[i].x1.ToString("0.0") + ", " + ge[i].y1.ToString("0.0") + " || " + ge[i].x2.ToString("0.0") + ", " + ge[i].y2.ToString("0.0") + "\r\n";
                 this.tbxOut.Text += s1;
            }
            base.OnPaint(e);
        }

        List<GraphEdge> MakeVoronoiGraph(List<PointF> sites, int width, int height)
        {
            double[] xVal = new double[sites.Count];
            double[] yVal = new double[sites.Count];
            for (int i = 0; i < sites.Count; i++)
            {
                xVal[i] = sites[i].X;
                yVal[i] = sites[i].Y;
            }
            return voroObject.generateVoronoi(xVal, yVal, 0, width, 0, height);

        }

        private void panelResCal_Paint(object sender, PaintEventArgs e)
        {
            addGrid(e);
            spreadPoints(e);
        }
        void addGrid(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            Font font = new Font("黑体", 8);
            Brush blueBrush = Brushes.Blue;
            Pen pen = new Pen(Color.LightBlue, 0.5F);
            for (int i = 1; i * 500 * cProjectData.dfMapScale < this.panelResCal.Width; i++)
            {
                int iXCurrentView = Convert.ToInt32(i * 500 * cProjectData.dfMapScale);
                Point point1 = new Point(iXCurrentView, 0);
                Point point2 = new Point(iXCurrentView, this.panelResCal.Height);
                dc.DrawLine(pen, point1, point2);
                dc.DrawString((cProjectData.dfMapXrealRefer + i * 500).ToString(), font, blueBrush, iXCurrentView, 0);
            }

            for (int i = 1; i * 500 * cProjectData.dfMapScale < this.panelResCal.Height; i++)
            {
                int iYCurrentView = Convert.ToInt32(i * 500 * cProjectData.dfMapScale);
                Point point3 = new Point(0, iYCurrentView);
                Point point4 = new Point(this.panelResCal.Width, iYCurrentView);
                dc.DrawLine(pen, point3, point4);
                dc.DrawString((cProjectData.dfMapYrealRefer - i * 500).ToString(), font, blueBrush, 0, iYCurrentView);
            }

            base.OnPaint(e);
        }


        private void panelResCal_Click(object sender, EventArgs e)
        {
            setPanel();
        }


        private void panelResCal_MouseMove(object sender, MouseEventArgs e)
        {
            this.tssLblInfor.Text = e.X + " " + e.Y;
        }
    }
}
