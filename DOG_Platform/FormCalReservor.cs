using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace DOGPlatform
{
  
    public partial class FormCalReservor : Form
    {
        Voronoi voroObject;
        struct itemReservePar
        {
            public string sXCM;
            public float destiny;
            public float fBo;
        }
        class itemWellLayerReserve
        {
            public string sJH;
            public string sXCM;
            public double dbX;
            public double dbY;
            public float fYXHD;
            public float fKXD;
            public float fBHD;
            public float destinyOil;
            public double dbArea;
            public float fBo;
            public double dbReserver;
            public double dbCLFD;//储量丰度
            public List<PointD> ltPD_Vertex_Voronoi;
        }
        List<itemReservePar> layerReservaParCal = new List<itemReservePar>();
        
        public FormCalReservor()
        {
            InitializeComponent();
            setPanel();
            voroObject = new Voronoi(0.1);
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
            StreamWriter swNew = new StreamWriter(cProjectManager.filePathRunInfor, true, Encoding.UTF8);
            // رسم أضلاع فورونوي
            for (int i = 0; i < ge.Count; i++)
            {
                
                    Point p1 = new Point((int)ge[i].x1, (int)ge[i].y1);
                    Point p2 = new Point((int)ge[i].x2, (int)ge[i].y2);
                    dc.DrawLine(Pens.Black, p1.X, p1.Y, p2.X, p2.Y);

                    string sLine = "P" + i + " size1: " + ge[i].site1 + " " + ge[i].x1.ToString("0.0") + " " + ge[i].y1.ToString("0.0") + " size2: " + ge[i].site2 + "  " + ge[i].x2.ToString("0.0") + " " + ge[i].y2.ToString("0.0");
   
                   swNew.WriteLine(sLine);      
            }
            swNew.Close();
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

        List<itemWellLayerReserve> calReserve()
        {
            List<itemWellLayerReserve> listLayerWellReserves = new List<itemWellLayerReserve>();
         // 读入小层数据字典
            List<ItemDicLayerData> listData = cIODicLayerData.readDicLayerData2struct();
       //   按小层顺序筛选，并计算voronoi，通过voronic 求取每个井层的面积
            foreach (string xcm in cProjectData.ltStrProjectXCM) 
            {
                List<ItemDicLayerData> listCurrentLayerData = listData.FindAll(p => p.sXCM == xcm);
                itemReservePar currentCalResrvePar = layerReservaParCal.First(p => p.sXCM == xcm);
               
                  //如果没填得检查一遍
                //内部会排序并有对应的ID
                //尽量让排序后的sizes和Voronoi内部的size是同一个顺序，这块需要校验=Y的情况
                //如果都是相等的，可以从0开始找
                //List<ItemDicLayerData> sortedCurrentLayerData = listCurrentLayerData.OrderBy(p => p.dbY).ToList();
                List<PointF> sites = new List<PointF>();

                foreach (ItemDicLayerData well in listCurrentLayerData)
                    sites.Add(new PointF(Convert.ToSingle(well.dbX), Convert.ToSingle(well.dbY)));
            

                double[] xVal = new double[sites.Count];
                double[] yVal = new double[sites.Count];
                for (int i = 0; i < sites.Count; i++)
                {
                    xVal[i] = sites[i].X;
                    yVal[i] = sites[i].Y;
                }
                double minX= cProjectData.listProjectWell.Min(p => p.dbX)-100;
                double maxX= cProjectData.listProjectWell.Max(p => p.dbX)+100;
                double minY= cProjectData.listProjectWell.Min(p => p.dbY)-100;
                double maxY= cProjectData.listProjectWell.Max(p => p.dbY)+100;
                List<GraphEdge> list_ge = voroObject.generateVoronoi(xVal, yVal, minX, maxX, minY, maxY);
                //foreach (ItemWell well in cProjectData.listProjectWell)
                //{
                //    PointF headView = cCordinationTransform.transRealPointF2ViewPoint(
                //        well.dbX, well.dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                //    sites.Add(headView);
                //} 
                //List<GraphEdge> list_ge= MakeVoronoiGraph(sites, panelResCal.Width, panelResCal.Height);
                //定义一个数据结构 就是返回 顶点序列，边的顺或者逆时针方向的结构列表
                //注意 这里安装sites的个数找 但是 ge里egde存的是
                List<List<PointD>> list_ClockPoints = new List<List<PointD>>();
                for (int i = 0; i < sites.Count; i++) 
                {
                    List<PointD> points = new List<PointD>();
                    foreach (GraphEdge ge in list_ge) 
                    {
                        if (ge.site1 == i) points.Add(new PointD(ge.x2, ge.y2));  //如果site1ID=输入时的序号，就取site2
                        if (ge.site2 == i) points.Add(new PointD(ge.x1, ge.y1));  
                    }
                    //按序号找到所有的顶点，按顺时针或者逆时针排序后输出
                }
                //有了 listCurrentLayerData和对应的list_ClockPoints，加上对应的密度，体积系数就能按容积法求出面积，然后输出了
                for (int i=0; i < listCurrentLayerData.Count; i++) 
                {
                    itemWellLayerReserve currentWellReserver = new itemWellLayerReserve();
                    currentWellReserver.sJH = listCurrentLayerData[i].sJH;
                    currentWellReserver.sXCM = listCurrentLayerData[i].sXCM;
                    currentWellReserver.dbX = listCurrentLayerData[i].dbX;
                    currentWellReserver.dbY = listCurrentLayerData[i].dbY;
                    currentWellReserver.fYXHD = listCurrentLayerData[i].fYXHD;
                    currentWellReserver.fKXD = listCurrentLayerData[i].fKXD;
                    currentWellReserver.fBHD = listCurrentLayerData[i].fBHD;
                    currentWellReserver.destinyOil = currentCalResrvePar.destiny;
                    currentWellReserver.fBo = currentCalResrvePar.fBo;
                    currentWellReserver.ltPD_Vertex_Voronoi = list_ClockPoints[i];
                    //N=100×Ao×h×ф×Soi×ρo/fBoi 
                  currentWellReserver.dbArea = cCalBase.calArea(list_ClockPoints[i]);
                    //面积计算还原坐标结构，存的是view坐标，需要还原成原坐标系坐标
                    currentWellReserver.dbReserver = 100 * currentWellReserver.dbArea * currentWellReserver.fYXHD * currentWellReserver.fKXD * currentWellReserver.fBHD
                        * currentWellReserver.destinyOil / currentWellReserver.fBo;
                    listLayerWellReserves.Add(currentWellReserver);
                    currentWellReserver.dbCLFD = 0.0;
                    if (currentWellReserver.dbArea > 0) currentWellReserver.dbCLFD = currentWellReserver.dbReserver / currentWellReserver.dbArea;
                }

            }//end of layerXCM foreach
            return listLayerWellReserves;
        }//end of calRes

         void write2File(List<itemWellLayerReserve> listLayerWellReserves)
        {
            string filePathRes = Path.Combine(cProjectManager.dirPathUsedProjectData, "储量.txt");
            StreamWriter sw = new StreamWriter(filePathRes, false, Encoding.UTF8);
           
            StreamWriter swVoi = new StreamWriter(cProjectManager.filePathVoi, false, Encoding.UTF8);
            List<string> ltStrHeadColoum = new List<string>();  //小层数据表头
            ltStrHeadColoum.Add("井号");
            ltStrHeadColoum.Add("小层名");
            ltStrHeadColoum.Add("X");
            ltStrHeadColoum.Add("Y");
            ltStrHeadColoum.Add("有效厚度(m)");
            ltStrHeadColoum.Add("孔隙度(%)");
            ltStrHeadColoum.Add("饱和度(%)");
            ltStrHeadColoum.Add("含油面积(km2)");
            ltStrHeadColoum.Add("密度(t/m3)");
            ltStrHeadColoum.Add("体积系数");
            ltStrHeadColoum.Add("储量(万吨)"); 
            ltStrHeadColoum.Add("储量丰度（万吨/平方公里）");
            sw.WriteLine(string.Join("\t", ltStrHeadColoum.ToArray()));
            foreach (itemWellLayerReserve item in listLayerWellReserves)
            {
                List<string> ltStrWrited = new List<string>();
                ltStrWrited.Add(item.sJH);
                ltStrWrited.Add(item.sXCM);
                ltStrWrited.Add(item.dbX.ToString("0.0"));
                ltStrWrited.Add(item.dbY.ToString("0.0"));
                ltStrWrited.Add(item.fYXHD.ToString("0.0")); //顶深海拔
                ltStrWrited.Add(item.fKXD.ToString("0.0"));
                ltStrWrited.Add(item.fBHD.ToString("0.0"));
                ltStrWrited.Add(item.dbArea.ToString("0.000"));
                ltStrWrited.Add(item.destinyOil.ToString("0.0"));
                ltStrWrited.Add(item.fBo.ToString("0.00"));

                ltStrWrited.Add(item.dbReserver.ToString("0.000")); //底深MD
                ltStrWrited.Add(item.dbCLFD.ToString("0.0")); //底深MD
                sw.WriteLine(string.Join("\t", ltStrWrited.ToArray()));

                List<string> liStrVoi = new List<string>();
                liStrVoi.Add(item.sJH);
                liStrVoi.Add(item.sXCM);
                liStrVoi.Add(item.dbX.ToString("0.0"));
                liStrVoi.Add(item.dbY.ToString("0.0"));
                foreach (PointD pd in item.ltPD_Vertex_Voronoi)
                {
                    liStrVoi.Add(pd.X.ToString("0.0"));
                    liStrVoi.Add(pd.Y.ToString("0.0"));
                }
                swVoi.WriteLine(string.Join("\t", liStrVoi.ToArray()));
            }
            sw.Close();
            swVoi.Close();

        }
        void inialCalPar()
        {
         for(int i=0;i<this.dgvCalpar.RowCount-1;i++)
         {
             itemReservePar cureentLayerPar=new itemReservePar();
             cureentLayerPar.sXCM = dgvCalpar.Rows[i].Cells[0].Value.ToString();
             float des = 0.8f;
             float.TryParse(dgvCalpar.Rows[i].Cells[1].Value.ToString(), out des);
             cureentLayerPar.destiny = des;
             float fBo = 1.1f;
             float.TryParse(dgvCalpar.Rows[i].Cells[1].Value.ToString(), out fBo);
             cureentLayerPar.fBo = fBo;
             layerReservaParCal.Add(cureentLayerPar);
             
         }
         foreach (string sxcm in cProjectData.ltStrProjectXCM)
         {
             //如果没有对应的计算参数给个默认值
             if (layerReservaParCal.Count==0||layerReservaParCal.Select(p=>p.sXCM).ToList().IndexOf(sxcm)<0)
             {
                 itemReservePar currentCalResrvePar = new itemReservePar();
                 currentCalResrvePar.sXCM = sxcm;
                 currentCalResrvePar.destiny = 0.8f;
                 currentCalResrvePar.fBo = 1.1f;
                 layerReservaParCal.Add(currentCalResrvePar);
             }
         }
        }
        private void btnCalRes_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            inialCalPar();
        
            write2File(calReserve());
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
               ts.Hours, ts.Minutes, ts.Seconds,
               ts.Milliseconds / 10);
            MessageBox.Show("计算完成。消耗时间：" + elapsedTime);
            Cursor.Current = Cursors.Default;
        }

     
    }
}
