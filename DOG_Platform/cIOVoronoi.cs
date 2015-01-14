using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace DOGPlatform
{
    class cIOVoronoi
    {
        public static List<itemWellLayerVoi> read2Struct()
        {
            List<itemWellLayerVoi> listReturn = new List<itemWellLayerVoi>();
            if (File.Exists(cProjectManager.filePathVoi))
            {
                using (StreamReader sr = new StreamReader(cProjectManager.filePathVoi, System.Text.Encoding.UTF8))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        itemWellLayerVoi item = new itemWellLayerVoi();
                       
                        string[] split = line.Trim().Split();

                        item.sJH = split[0];
                        item.sXCM = split[1];
                        item.dbX = 0.0;
                        item.dbY = 0.0;
                        double.TryParse(split[2], out  item.dbX);
                        double.TryParse(split[3], out item.dbY);
                        int iCount = 4;
                        while (split.Length > iCount)
                        {
                            PointF pf = new PointF();
                            pf.X = float.Parse(split[iCount]);
                            pf.Y = float.Parse(split[iCount + 1]);
                            item.ltdpVertex.Add(pf);
                            iCount = iCount + 2;
                        }
                        listReturn.Add(item);
                    }
                }
            }
            return listReturn;
        }

        public static void calVoiAndwrite2File()
        {
            write2File(calVoi());
        }

        public static List<itemWellLayerVoi> calVoi()
        {
            Voronoi voroObject = new Voronoi(0.1);
            List<itemWellLayerVoi> listLayerVoronoi = new List<itemWellLayerVoi>();
            // 读入小层数据字典
            List<ItemDicLayerData> listData = cIODicLayerData.readDicLayerData2struct();
            //   按小层顺序筛选，并计算voronoi，通过voronic 求取每个井层的面积
            foreach (string xcm in cProjectData.ltStrProjectXCM)
            {
                List<ItemDicLayerData> listCurrentLayerData = listData.FindAll(p => p.sXCM == xcm);

                //如果没填得检查一遍
                //内部会排序并有对应的ID
                //尽量让排序后的sizes和Voronoi内部的size是同一个顺序，这块需要校验=Y的情况
        
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
                double minX = cProjectData.listProjectWell.Min(p => p.dbX) - 200;
                double maxX = cProjectData.listProjectWell.Max(p => p.dbX) + 200;
                double minY = cProjectData.listProjectWell.Min(p => p.dbY) - 200;
                double maxY = cProjectData.listProjectWell.Max(p => p.dbY) + 200;
                List<GraphEdge> list_ge = voroObject.generateVoronoi(xVal, yVal, minX, maxX, minY, maxY);

                StreamWriter swNew = new StreamWriter(cProjectManager.filePathRunInfor, false, Encoding.UTF8);
                for (int i = 0; i < list_ge.Count; i++)
                {
                        Point p1 = new Point((int)list_ge[i].x1, (int)list_ge[i].y1);
                        Point p2 = new Point((int)list_ge[i].x2, (int)list_ge[i].y2);
                        string sLine = "\nP " + i + " size1: " + list_ge[i].site1+ " " + list_ge[i].x1 + ", " + list_ge[i].y1  + " size2: " + list_ge[i].site2 +" " + list_ge[i].x2 + ", " + list_ge[i].y2;
                        swNew.WriteLine(sLine);
                }
              
                swNew.Close();
                //定义一个数据结构 就是返回 顶点序列，边的顺或者逆时针方向的结构列表
                //注意 这里安装sites的个数找 但是 ge里egde存的是
                List<List<PointF>> list_ClockPoints = new List<List<PointF>>();
                for (int i = 0; i < sites.Count; i++)
                {
                    List<PointF> points = new List<PointF>();
                    foreach (GraphEdge ge in list_ge)
                    {
                        if (ge.site2 == i) points.Add(new PointF(Convert.ToSingle( ge.x2), Convert.ToSingle(ge.y2)));  //如果site1ID=输入时的序号，就取site2
                        if (ge.site1 == i) points.Add(new PointF(Convert.ToSingle(ge.x1), Convert.ToSingle(ge.y1)));
                    }
                    //按序号找到所有的顶点，按顺时针或者逆时针排序后输出
                    list_ClockPoints.Add( cSortPoints.sortPoints(points.Distinct().ToList()));
                }
                //有了 listCurrentLayerData和对应的list_ClockPoints，加上对应的密度，体积系数就能按容积法求出面积，然后输出了
                for (int i = 0; i < listCurrentLayerData.Count; i++)
                {
                    itemWellLayerVoi item=new itemWellLayerVoi();
                    item.sJH = listCurrentLayerData[i].sJH;
                    item.sXCM = listCurrentLayerData[i].sXCM;
                    item.dbX = listCurrentLayerData[i].dbX;
                    item.dbY = listCurrentLayerData[i].dbY;
                    item.ltdpVertex= list_ClockPoints[i];
                    listLayerVoronoi.Add(item);
                }

            }//end of layerXCM foreach
            return listLayerVoronoi;
        }//end of calRes

        public static void write2File(List<itemWellLayerVoi> listLayerWellVoi)
        {
                   StreamWriter swVoi = new StreamWriter(cProjectManager.filePathVoi, false, Encoding.UTF8);
            List<string> ltStrHeadColoum = new List<string>();  //小层数据表头

            foreach (itemWellLayerVoi item in listLayerWellVoi)
            {
             
                List<string> liStrVoi = new List<string>();
                liStrVoi.Add(item.sJH);
                liStrVoi.Add(item.sXCM);
                liStrVoi.Add(item.dbX.ToString("0.0"));
                liStrVoi.Add(item.dbY.ToString("0.0"));
                foreach (PointF pd in item.ltdpVertex)
                {
                    liStrVoi.Add(pd.X.ToString("0.0"));
                    liStrVoi.Add(pd.Y.ToString("0.0"));
                }
                swVoi.WriteLine(string.Join("\t", liStrVoi.ToArray()));
            }
            swVoi.Close();
        }
    }
}
