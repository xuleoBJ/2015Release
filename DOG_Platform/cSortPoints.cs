using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DOGPlatform
{
    class cSortPoints
    {
        public static List<PointF> sortPoints(List<PointF> points) 
        {
             //find the smallest X index
            List<float> listX = points.Select(p => p.X).ToList();
            float fXMin = listX.Min();
            int indexMinX= points.FindIndex(p => p.X == fXMin);
            List<double> dfListAngle = new List<double>();
            List<double> listAngleSorted = new List<double>();
            for (int i = 0; i < points.Count; i++)
            {
                double mainPointX = (double)points[indexMinX].X;
                double mainPointY = (double)points[indexMinX].Y;
                double kone;
                if ((double)points[i].X == mainPointX && (double)points[i].Y == mainPointY)
                    kone = 0;
                else
                {
                    double otherPointX = (double)points[i].X - (double)points[indexMinX].X;
                    double otherPointY = (double)points[indexMinX].Y - (double)points[i].Y;
                    kone = Angle(otherPointX, otherPointY);
                }

                dfListAngle.Add(kone);
                listAngleSorted.Add(kone);
            }

            listAngleSorted.Sort();
            List<PointF> listReturn = new List<PointF>();
            foreach (double _angle in listAngleSorted) 
            {
                int _index = dfListAngle.IndexOf(_angle);
                listReturn.Add(points[_index]);
            }
            return listReturn;
         
        }

        public static double Angle(double px2, double py2)
        {
            double angle = 0.0;
            //Calculate the angle
            angle = System.Math.Atan(System.Math.Abs( px2) / System.Math.Abs(py2));

            // Convert to degrees
            angle = angle * 180 / System.Math.PI;

            if (py2 < 0)
                angle = 180 - angle;

            return angle;
        }

      

    }
}
