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
                        item.dbX = 0.0;
                        item.dbY = 0.0;

                        string[] split = line.Trim().Split();

                        item.sJH = split[0];
                        item.sXCM = split[1];
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
    }
}
