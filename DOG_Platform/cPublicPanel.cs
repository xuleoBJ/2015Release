using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DOGPlatform
{
    class cPanel
    {
        public Panel panel;
        public cPanel(Panel _panel) 
        {
            this.panel = _panel;
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
                panel.Dock = System.Windows.Forms.DockStyle.None;

                panel.Width = iPanelWidth;
                panel.Height = iPanelHeight;
                panel.Location = new Point(0, 0);

                panel.Invalidate();
                panel.Focus();
            }

        }
        void addGrid(PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            Font font = new Font("黑体", 8);
            Brush blueBrush = Brushes.Blue;
            Pen pen = new Pen(Color.LightBlue, 0.5F);
            for (int i = 1; i * 500 * cProjectData.dfMapScale < panel.Width; i++)
            {
                int iXCurrentView = Convert.ToInt32(i * 500 * cProjectData.dfMapScale);
                Point point1 = new Point(iXCurrentView, 0);
                Point point2 = new Point(iXCurrentView, panel.Height);
                dc.DrawLine(pen, point1, point2);
                dc.DrawString((cProjectData.dfMapXrealRefer + i * 500).ToString(), font, blueBrush, iXCurrentView, 0);
            }

            for (int i = 1; i * 500 * cProjectData.dfMapScale < panel.Height; i++)
            {
                int iYCurrentView = Convert.ToInt32(i * 500 * cProjectData.dfMapScale);
                Point point3 = new Point(0, iYCurrentView);
                Point point4 = new Point(panel.Width, iYCurrentView);
                dc.DrawLine(pen, point3, point4);
                dc.DrawString((cProjectData.dfMapYrealRefer - i * 500).ToString(), font, blueBrush, 0, iYCurrentView);
            }
        }
    }
}
