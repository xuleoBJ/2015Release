using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
namespace DOGPlatform.SVG
{
    class cSVGSectionTrackConnect : cSVGSectionTrackLayer
    {
        public struct itemViewLayerDepth
        {
            public float fViewX;
            public float fViewY;
            public float fViewHeight;
            public string sXCM;
        }
       
        public XmlElement gConnectPath(itemViewLayerDepth well1LayerDepthItem, itemViewLayerDepth well2LayerDepthItem)
        {
            int iWidthExtent = 30;
            XmlElement gConnectLayer = svgDoc.CreateElement("path");
            //圆弧算法还需要完善   
            // string d = "M-50 " + _top.ToString() + "h50" + "v" + (_bottom - _top).ToString() + "h-20" + "q -20,0 -50,0" + "L -" + f_distance.ToString() + " " + _bottom_before.ToString() + "v" + (_top_before - _bottom_before).ToString() + "z";
            string d = "M" + (well1LayerDepthItem.fViewX + iWidthExtent).ToString() + " " + (well1LayerDepthItem.fViewY +well1LayerDepthItem.fViewHeight).ToString()
               + "h-" + iWidthExtent.ToString() + " " + "v-" + well1LayerDepthItem.fViewHeight.ToString() + "h" + iWidthExtent.ToString() +
                "L" + (well2LayerDepthItem.fViewX - iWidthExtent).ToString() + " " + well2LayerDepthItem.fViewY.ToString() + "h" + iWidthExtent.ToString()
                + "v" + well2LayerDepthItem.fViewHeight.ToString() + "h-" + iWidthExtent.ToString() + "z";

            string sXCM = well1LayerDepthItem.sXCM;
            gConnectLayer.SetAttribute("d", d);
            gConnectLayer.SetAttribute("style", "stroke-width:0.2");
            gConnectLayer.SetAttribute("stroke", "black");
            gConnectLayer.SetAttribute("fill-opacity", "0.5");
            if (cProjectData.ltStrProjectXCM.Contains(sXCM))
            {
                int _iColorIndex = cProjectData.ltStrProjectXCM.IndexOf(sXCM);
                gConnectLayer.SetAttribute("fill", colorList[_iColorIndex]);

            }
            else
            {
                gConnectLayer.SetAttribute("fill", "none");
            }
            return gConnectLayer;
        }

        public XmlElement addgConnectLayerTrack
          (cWellSectionSVG well1, trackLayerDepthDataList well1LayerDepthDataList,
            cWellSectionSVG well2, trackLayerDepthDataList well2LayerDepthDataList)
        {
            Cursor.Current = Cursors.WaitCursor;
            XmlElement gConnectTrack = svgDoc.CreateElement("g");
            gConnectTrack.SetAttribute("id", "idConnectLayer");
            setLayerColor();
            for (int i = 0; i < well1LayerDepthDataList.ltStrXCM.Count; i++)
            {
                itemViewLayerDepth well1LayerItem;
                well1LayerItem.fViewX = well1.fXview;
                well1LayerItem.fViewY = -well1.fDepthFlatted + well1LayerDepthDataList.fListDS1[i];
                well1LayerItem.sXCM = well1LayerDepthDataList.ltStrXCM[i];
                well1LayerItem.fViewHeight = well1LayerDepthDataList.fListDS2[i] - well1LayerDepthDataList.fListDS1[i];

                //在上一口井找到了同名层
                int iIndex = well2LayerDepthDataList.ltStrXCM.IndexOf(well1LayerDepthDataList.ltStrXCM[i]);
                if (iIndex >= 0)
                {
                    itemViewLayerDepth well_last_LayerItem;
                    well_last_LayerItem.fViewX = well2.fXview;
                    well_last_LayerItem.fViewY = -well2.fDepthFlatted + well2LayerDepthDataList.fListDS1[iIndex];
                    well_last_LayerItem.sXCM = well2LayerDepthDataList.ltStrXCM[iIndex];
                    well_last_LayerItem.fViewHeight = well2LayerDepthDataList.fListDS2[iIndex] - well2LayerDepthDataList.fListDS1[iIndex];
                    gConnectTrack.AppendChild(gConnectPath(well1LayerItem, well_last_LayerItem));
                }

            }
            Cursor.Current = Cursors.Default;
            return gConnectTrack;
        }

        
    }
}
