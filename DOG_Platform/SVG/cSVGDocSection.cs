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

        public XmlElement gWellDistanceRuler (double distance)
        {
            XmlElement gDistanceRuler = svgDoc.CreateElement("g");

            XmlElement curveHeadInfor = svgDoc.CreateElement("path");
            string sPath = "m 5 -5" + " v 5 h "+(distance-30).ToString()+" v-5";
            curveHeadInfor.SetAttribute("d", sPath);
            curveHeadInfor.SetAttribute("fill", "none");
            curveHeadInfor.SetAttribute("stroke", "black");
            gDistanceRuler.AppendChild(curveHeadInfor);
            XmlElement distanceText = svgDoc.CreateElement("text");
            distanceText.SetAttribute("x", (distance/2).ToString());
            distanceText.SetAttribute("y", (- 5).ToString());
            distanceText.SetAttribute("font-size", "20");
            distanceText.SetAttribute("fill", "red");
            distanceText.InnerText = distance.ToString()+"m";
            gDistanceRuler.AppendChild(distanceText);
            return gDistanceRuler;
        }

         
 
    }
}
