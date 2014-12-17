using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOGPlatform
{
    class cCalDistance
    {
        public static float calDistance2D(float x1, float y1, float x2, float y2)
        {
            return Convert.ToSingle(Math.Pow((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2), 0.5));
        }
        public static double calDistance2D(double x1, double y1, double x2, double y2)
        {
            return Math.Pow((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2), 0.5);
        }
    }
}
