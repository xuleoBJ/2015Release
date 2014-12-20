using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace DOGPlatform.SVG
{
    class cSVGDocSection : cSVGBaseSection
    {

        public cSVGDocSection(int width,int height,int iDX, int iDY)
            : base( width, height, iDX, iDY)
        {
        
        }

        public cSVGDocSection(int width, int height, int iDX, int iDY,string sUnit)
            : base(width, height, iDX, iDY, sUnit)
        {

        }

        public List<string> ltStrJH_SVG = new List<string>();
        public List<double> dfListX_Real = new List<double>();
        public List<double> dfListY_Real = new List<double>();
        public List<float> fListKB_Real = new List<float>();
        public List<int> iListWellType_Real = new List<int>();
       
        public void readSectionInfor2InialView(string filePath)
        {
            ltStrJH_SVG.Clear();
            dfListX_Real.Clear();
            dfListY_Real.Clear();
            fListKB_Real.Clear();
            fListDS1Showed_SVG.Clear();
            fListDS2Showed_SVG.Clear();
            iListWellType_Real.Clear();

            using (StreamReader sr = new StreamReader(filePath))
            {
                String line;
                string[] split;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    split = line.Trim().Split(new char[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    ltStrJH_SVG.Add(split[0]);
                    fListDS1Showed_SVG.Add(float.Parse(split[1]));
                    fListDS2Showed_SVG.Add(float.Parse(split[2]));
                    dfListX_Real.Add(double.Parse(split[3]));
                    dfListY_Real.Add(double.Parse(split[4]));
                    fListKB_Real.Add(float.Parse(split[5]));
                    iListWellType_Real.Add(int.Parse(split[6]));
                }
            }

        }
        
        public void  addgElement(XmlElement gElement,int idx)  //剖面图Y不能移动
        {
            string sTranslate = "translate(" + idx.ToString() + ",0)";
            gElement.SetAttribute("transform", sTranslate);
            XmlNode importNewsItem = svgDoc.ImportNode(gElement, true);
            this.gSVG.AppendChild(importNewsItem);
        }

        public new  void addgElement(XmlElement gElement, int idx,int idy)  //剖面图Y不能移动
        {
            string sTranslate = "translate(" + idx.ToString() + "," + idy.ToString() + ")";
            gElement.SetAttribute("transform", sTranslate);
            XmlNode importNewsItem = svgDoc.ImportNode(gElement, true);
            this.gSVG.AppendChild(importNewsItem);
        }
      
        public void addgElement(XmlElement gElement, Point pt)
        {
            addgElement(gElement, pt.X, pt.Y);
        }

        public XmlElement addgConnectLayerTrack
        (List<float> fListDS1, List<float> fListDS2, List<string> ltStrXCM, float m_KB, float f_distance,
        List<float> fListDS1_Well2, List<float> fListDS2_Well2, List<string> ltStrXCM_Well2, float m_KB_Well2)
        {
            XmlElement gConnectTrack = svgDoc.CreateElement("g");
            gConnectTrack.SetAttribute("id", "idConnectLayer");
           // setLayerColorByXML();
            for (int i = 0; i < ltStrXCM.Count; i++)
            {
                float _top = -m_KB + fListDS1[i];
                float _bottom = -m_KB + fListDS2[i];
                string sXCM = ltStrXCM[i];
                int _indextXCM = ltStrXCM_Well2.IndexOf(sXCM);
                if (_indextXCM >= 0)
                {
                    float _top_before = -m_KB_Well2 + fListDS1_Well2[_indextXCM];
                    float _bottom_before = -m_KB_Well2 + fListDS2_Well2[_indextXCM];
                    XmlElement gConnectPath = svgDoc.CreateElement("path");
                    //圆弧算法还需要完善   
                    // string d = "M-50 " + _top.ToString() + "h50" + "v" + (_bottom - _top).ToString() + "h-20" + "q -20,0 -50,0" + "L -" + f_distance.ToString() + " " + _bottom_before.ToString() + "v" + (_top_before - _bottom_before).ToString() + "z";
                    string d = "M-50 " + _top.ToString() + "h50" + "v" + (_bottom - _top).ToString() + "h-50" + "L -" + f_distance.ToString() + " " + _bottom_before.ToString() + "v" + (_top_before - _bottom_before).ToString() + "z";

                    gConnectPath.SetAttribute("d", d);
                    gConnectPath.SetAttribute("style", "stroke-width:0.2");
                    gConnectPath.SetAttribute("stroke", "black");
                    gConnectPath.SetAttribute("fill-opacity", "0.2");
                    if (cProjectData.ltStrProjectXCM.Contains(sXCM))
                    {
                        int _iColorIndex = cProjectData.ltStrProjectXCM.IndexOf(sXCM);
                        gConnectPath.SetAttribute("fill", colorList[_iColorIndex]);

                    }
                    else
                    {
                        gConnectPath.SetAttribute("fill", "none");
                    }
                    gConnectTrack.AppendChild(gConnectPath);

                }

            }
            return gConnectTrack;
        }

        public struct itemViewLayerDepth
        {
            public float fViewX;
            public float fViewY;
            public float fViewHeight;
            public string sXCM;
        }
        public XmlElement gConnectPath(itemViewLayerDepth well1LayerDepthItem, itemViewLayerDepth well2LayerDepthItem)
        {
            int iWidthExtent = 50;
            XmlElement gConnectLayer = svgDoc.CreateElement("path");
            //圆弧算法还需要完善   
            // string d = "M-50 " + _top.ToString() + "h50" + "v" + (_bottom - _top).ToString() + "h-20" + "q -20,0 -50,0" + "L -" + f_distance.ToString() + " " + _bottom_before.ToString() + "v" + (_top_before - _bottom_before).ToString() + "z";
            string d = "M" + (well1LayerDepthItem.fViewX + iWidthExtent).ToString() + " " + (well1LayerDepthItem.fViewY - well1LayerDepthItem.fViewHeight).ToString()
               + "h-" + iWidthExtent.ToString() + " " + "v-" + well1LayerDepthItem.fViewHeight.ToString() + "h" + iWidthExtent.ToString() +
                "L" + (well2LayerDepthItem.fViewX - iWidthExtent).ToString() + " " + well2LayerDepthItem.fViewY.ToString() + "h" + iWidthExtent.ToString()
                + "v" + well2LayerDepthItem.fViewHeight.ToString() + "h-" + iWidthExtent.ToString() + "z";

            gConnectLayer.SetAttribute("d", d);
            gConnectLayer.SetAttribute("style", "stroke-width:0.2");
            gConnectLayer.SetAttribute("stroke", "black");
            gConnectLayer.SetAttribute("fill-opacity", "0.8");
            return gConnectLayer;
        }

        public XmlElement addgConnectLayerTrack
          (cWellSectionSVG well1, trackLayerDepthDataList well1LayerDepthDataList,
            cWellSectionSVG well2, trackLayerDepthDataList well2LayerDepthDataList)
        {
            Cursor.Current = Cursors.WaitCursor;
            XmlElement gConnectTrack = svgDoc.CreateElement("g");
            gConnectTrack.SetAttribute("id", "idConnectLayer");
            //setLayerColorByXML();
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
