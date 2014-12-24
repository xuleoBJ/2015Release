﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;

namespace DOGPlatform.SVG
{
    class cSVGSectionTrackConnect : cSVGSectionTrackLayer
    {
        public static List<itemViewLayerDepth> getListViewLayerConnect(string sJH, int dx,List<float> fListDS1, List<float> fListDS2, List<string> ltStrXCM, float m_KB)
        {
            List<itemViewLayerDepth> listReturn = new List<itemViewLayerDepth>();
            for (int i = 0; i < ltStrXCM.Count; i++)
            {
                itemViewLayerDepth newItem = new itemViewLayerDepth();
                float _top = fListDS1[i];
                float _bottom = fListDS2[i];
                string sXCM = ltStrXCM[i];
                float y0 = -m_KB + _top;
                float height = _bottom - _top;
                newItem.sJH = sJH;
                newItem.sXCM = sXCM;
                newItem.fViewX = dx;
                newItem.fViewY = y0;
                newItem.fViewHeight = height;
                listReturn.Add(newItem);
            }
            return listReturn;
        }

        public static List<itemViewLayerDepth> getListViewLayerConnect(string sJH,int dx, trackLayerDepthDataList layerDepthDataList, float m_KB)
        {
            return getListViewLayerConnect(sJH, dx,layerDepthDataList.fListDS1, layerDepthDataList.fListDS2, layerDepthDataList.ltStrXCM, m_KB);
        }

        public static List<itemViewLayerDepth> getListViewXieTrack2VerticalLayerConnect(string sJH, int dx, trackLayerDepthDataList layerDepthDataList, float m_KB)
        {
            List<ItemDicWellPath> listWellPathDS1 = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, layerDepthDataList.fListDS1);
            List<ItemDicWellPath> listWellPathDS2 = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, layerDepthDataList.fListDS2);
            List<float> fListTVD1 = listWellPathDS1.Select(p => p.f_TVD).ToList();
            List<float> fListTVD2 = listWellPathDS2.Select(p => p.f_TVD).ToList();
            return getListViewLayerConnect(sJH, dx, fListTVD1, fListTVD2, layerDepthDataList.ltStrXCM, m_KB);
        }
        public struct itemViewLayerDepth
        {
            public string sJH;
            public float fViewX;
            public float fViewY;
            public float fViewHeight;
            public string sXCM;
        }

        public  XmlElement gConnectPath( List<itemViewLayerDepth> listView)
        {
            XmlElement gConnectLayer = svgDoc.CreateElement("polyline");
            string _points = "";
            foreach (itemViewLayerDepth item in listView)
            {
                _points = _points + item.fViewX.ToString() + ',' + item.fViewY.ToString() + " " + (item.fViewX+30).ToString() + ',' + item.fViewY.ToString()+" ";
            }
            gConnectLayer.SetAttribute("style", "stroke-width:0.2");
            gConnectLayer.SetAttribute("stroke", "black");
            gConnectLayer.SetAttribute("fill-opacity", "1");
            gConnectLayer.SetAttribute("fill", "none");
            gConnectLayer.SetAttribute("points", _points);
           
            return gConnectLayer;
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

       

        
    }
}
