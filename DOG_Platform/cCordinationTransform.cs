using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DOGPlatform
{
    class cCordinationTransform:cPublicMethodBase
    {
        public static PointF transRealPointF2ViewPointF
          (double df_XReal, double df_YReal, double df_XRealRefer, double df_YRealRefer, float f_scale)
        {
            PointF PointFReturn = new PointF();
            PointFReturn.X = Convert.ToSingle((df_XReal - df_XRealRefer) * f_scale);
            PointFReturn.Y = Convert.ToSingle((df_YRealRefer - df_YReal) * f_scale);
            return PointFReturn;
        }

        public static Point transRealPointF2ViewPoint
    (double df_XReal, double df_YReal, double df_XRealRefer, double df_YRealRefer, int iWidth, float f_Xscale, int iheight, float f_Yscale)
        {
            Point PointReturn = new Point();
            PointReturn.X = Convert.ToInt32((df_XReal - df_XRealRefer) * f_Xscale);
            PointReturn.Y = Convert.ToInt32((df_YRealRefer - df_YReal) * f_Yscale);

            return PointReturn;
        }

        public static Point transRealPointF2ViewPoint
    (double df_XReal, double df_YReal, double df_XRealRefer, double df_YRealRefer, float fscale)
        {
            Point PointReturn = new Point();
            PointReturn.X = Convert.ToInt32((df_XReal - df_XRealRefer) * fscale);
            PointReturn.Y = Convert.ToInt32((df_YRealRefer - df_YReal) * fscale);
            return PointReturn;
        }

        public static Point transRealPointF2ViewPointByCurrentSystemSetting(double df_XReal, double df_YReal)
        {
            Point PointReturn = new Point();
            PointReturn.X = Convert.ToInt32((df_XReal - cProjectData.dfMapXrealRefer) *  cProjectData.fMapScale);
            PointReturn.Y = Convert.ToInt32((cProjectData.dfMapYrealRefer - df_YReal) * cProjectData.fMapScale);
            return PointReturn;
        }
        public static double transXview2Xreal(int iXscreen, double df_XrealRefer, float fscale)
        {
            return iXscreen / fscale + df_XrealRefer;
        }

        public static double transYview2Yreal(int iYscreen, double df_YrealRefer, float fscale)
        {
            return df_YrealRefer - iYscreen / fscale;
        }
    
        public static Point getPointViewByWellName(string sJH)
        {
            List<ItemWellHead> listWellHead = cIOinputWellHead.readWellHead2Struct();

            double dbX = 0;
            double dbY = 0;
            for (int k = 0; k < listWellHead.Count; k++)
            {
                if (listWellHead[k].sJH == sJH)
                {
                    dbX = listWellHead[k].dbX;
                    dbY = listWellHead[k].dbY;
                    break;
                }
            }
            Point pointConvert2View = cCordinationTransform.transRealPointF2ViewPoint(dbX, dbY,
               cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.fMapScale);
            return pointConvert2View;
        }

    }
}
