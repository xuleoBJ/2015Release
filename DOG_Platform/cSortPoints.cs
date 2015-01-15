using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DOGPlatform
{
    class cSortPoints
    {
        struct XYAngleID 
        {
            public int id;
            public float fx;
            public float fy;
            public double angle;
        }

        public static List<PointF> sortPoints(List<PointF> points,PointF pCent)
        {
            List<XYAngleID> dfListAngle = new List<XYAngleID>();
            for (int i = 0; i < points.Count; i++)
            {
                XYAngleID agID = new XYAngleID();
                agID.id = i;
                agID.fx = points[i].X;
                agID.fy = points[i].Y;
                agID.angle = Angle(points[i], pCent); ;
                dfListAngle.Add(agID); 
            }

            List<PointF> listReturn = new List<PointF>();

            foreach (XYAngleID item in dfListAngle.OrderBy(p => p.angle))
            {
                listReturn.Add(new PointF(item.fx, item.fy));
            }

            return listReturn;

        }
         public static double Angle(PointF p1, PointF pCent)
        {
            //Calculate the angle
            double angle = System.Math.Atan2(p1.Y - pCent.Y, p1.X - pCent.X);

            // Convert to degrees
            angle = angle * 180 / System.Math.PI;
            if (p1.Y - pCent.Y > 0)
                angle = 180 + angle;
            return angle;
        }
       
      

    }
}
