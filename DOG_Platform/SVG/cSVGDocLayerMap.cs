using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Linq;

namespace DOGPlatform.SVG
{
    class cSVGDocLayerMap : cBaseMapSVG
    {
        //set defautl xmlConfigPath 
        public string xmlConfigPath = cProjectManager.xmlConfigLayerMap;
        XmlDocument xmlLayerMap = new XmlDocument();
   
     
        public cSVGDocLayerMap(string _filePathXMLConfig,int _iDX, int _iDY)
            : base(_iDX, _iDY)
        {
            this.xmlConfigPath = _filePathXMLConfig;
            
            xmlLayerMap.Load(xmlConfigPath);
            double.TryParse(xmlLayerMap.SelectSingleNode("/LayerMapConfig/Layer/xRef").InnerText,out xRef);
            double.TryParse(xmlLayerMap.SelectSingleNode("/LayerMapConfig/Layer/yRef").InnerText,out yRef);
            float.TryParse(xmlLayerMap.SelectSingleNode("/LayerMapConfig/Layer/dfMapScale").InnerText, out dfscale);
        }

        public cSVGDocLayerMap(string _filePathXMLConfig, int width, int height, int iDX, int iDY)
            : base(width, height, iDX, iDY)
        {
            this.xmlConfigPath = _filePathXMLConfig;
        }

        public cSVGDocLayerMap(int width, int height, int iDX, int iDY)
            : base(width, height, iDX, iDY)
        {

        }


        public void delgWellPosition()
        {
            XmlNode gWells =svgRoot.SelectSingleNode("/svg/g/g[@id='idWell']");
            if (gWells != null) gWells.RemoveAll();
        }


        //解析传入的XML中的井坐标，然后成图
        public XmlElement gWellsPosition()
        {
            delgWellPosition();
            string _JHFontSize = xmlLayerMap.SelectSingleNode("/LayerMapConfig/JHText/fontSize").InnerText;
            int _radis = int.Parse(xmlLayerMap.SelectSingleNode("/LayerMapConfig/WellInfor/r").InnerText);
            int _iCirlceWidth = int.Parse(xmlLayerMap.SelectSingleNode("/LayerMapConfig/WellInfor/rLineWidth").InnerText);

            string _DX_JHText = xmlLayerMap.SelectSingleNode("LayerMapConfig/JHText/DX_Text").InnerText;

            XmlElement gWellPositon = svgDoc.CreateElement("g");
            gWellPositon.SetAttribute("id", "idWell");


            List<ItemWellView> listWellView = new List<ItemWellView>();

            foreach (XmlNode xn in xmlLayerMap.SelectNodes("/LayerMapConfig/WellInfor/Well"))
            {
                string _data = xn.InnerText;
                ItemWellMapLayer item=ItemWellMapLayer.parseLine(_data);
                Point pointConvert2View = cCordinationTransform.transRealPointF2ViewPoint(item.dbX, item.dbY, xRef, yRef, this.dfscale);
   
                gWellPositon.AppendChild(gWell(item.sJH, pointConvert2View.X, pointConvert2View.Y, item.iWellType, _JHFontSize, _radis, _iCirlceWidth, _DX_JHText)); 
              
            }
          
            return gWellPositon;
        }

        public XmlElement gWell
            (string sJH, int iXview, int iYview, int iWellType, string _JHFontSize, int _radis,int _iCirlceWidth, string _DX_JHText)
        {

            XmlElement gWell = svgDoc.CreateElement("g");
            gWell.SetAttribute("id", "id" + sJH);

            string m_colorWell = "none";
            if (iWellType == 3)
            {
                m_colorWell = "red";
            }
            else if (iWellType == 1)
            {
                XmlElement gWellSymbolUse = svgDoc.CreateElement("use");
                gWellSymbolUse.SetAttribute("x", iXview.ToString());
                gWellSymbolUse.SetAttribute("y", iYview.ToString());
                XmlAttribute WellSymbolNode = svgDoc.CreateAttribute("xlink", "href", "http://www.w3.org/1999/xlink");
                WellSymbolNode.Value = "#" + "idOilGasWellSymbol";
                gWellSymbolUse.Attributes.Append(WellSymbolNode);
                gWell.AppendChild(gWellSymbolUse);
                m_colorWell = "red";
            }
            else if (iWellType == 5)
            {
                m_colorWell = "Gold";
            }
            else if (iWellType == 15)
            {
                XmlElement gWellSymbolUse = svgDoc.CreateElement("use");
                gWellSymbolUse.SetAttribute("x", iXview.ToString());
                gWellSymbolUse.SetAttribute("y", iYview.ToString());
                XmlAttribute WellSymbolNode = svgDoc.CreateAttribute("xlink", "href", "http://www.w3.org/1999/xlink");
                WellSymbolNode.Value = "#" + "InjectWellSymbol";
                gWellSymbolUse.Attributes.Append(WellSymbolNode);
                gWell.AppendChild(gWellSymbolUse);
                m_colorWell = "blue";
            }

            else if (iWellType == 8)
            {
                XmlElement gWellSymbolUse = svgDoc.CreateElement("use");
                gWellSymbolUse.SetAttribute("x", iXview.ToString());
                gWellSymbolUse.SetAttribute("y", iYview.ToString());
                XmlAttribute WellSymbolNode = svgDoc.CreateAttribute("xlink", "href", "http://www.w3.org/1999/xlink");
                WellSymbolNode.Value = "#" + "idPlatformWell";
                gWellSymbolUse.Attributes.Append(WellSymbolNode);
                gWell.AppendChild(gWellSymbolUse);
                m_colorWell = "red";
            }
            else
            {
                m_colorWell = "black";
                XmlElement gWellSymbolInner = svgDoc.CreateElement("circle");
                gWellSymbolInner.SetAttribute("x", iXview.ToString());
                gWellSymbolInner.SetAttribute("y", iYview.ToString());
                gWellSymbolInner.SetAttribute("r", "1.5");
                gWellSymbolInner.SetAttribute("stroke", m_colorWell);
                gWellSymbolInner.SetAttribute("stroke-width", _iCirlceWidth.ToString());
                gWellSymbolInner.SetAttribute("fill", "none");
                gWell.AppendChild(gWellSymbolInner);
                List<ItemWellView> listWellView = new List<ItemWellView>();

            }
            XmlElement gWellSymbol = gWellCircle(iXview, iYview, _radis, m_colorWell, _iCirlceWidth);
            gWell.AppendChild(gWellSymbol);

            XmlElement gJHText = svgDoc.CreateElement("text");
            gJHText.SetAttribute("x", (iXview - int.Parse(_DX_JHText)).ToString());
            gJHText.SetAttribute("y", (iYview + int.Parse(_DX_JHText)).ToString());
            gJHText.SetAttribute("font-size", _JHFontSize);
            gJHText.SetAttribute("font-style", "normal");
            gJHText.InnerText = sJH;
            gJHText.SetAttribute("fill", m_colorWell);
            gWell.AppendChild(gJHText); 

         
            return gWell;
        }

        public List<XmlElement> addgMutiplePolyline(List<int> iListX_FaultSVG, List<int> iListY_FaultSVG, List<string> ltStrFaultName_FaultSVG)
        {
            List<XmlElement> listgPolyline = new List<XmlElement>();
            foreach (string sitem in ltStrFaultName_FaultSVG.Distinct())
            {
                int _indexFirst = ltStrFaultName_FaultSVG.IndexOf(sitem);
                int _count = ltStrFaultName_FaultSVG.LastIndexOf(sitem) - _indexFirst + 1;
                List<int> iCurrentListXFaultscreen = iListX_FaultSVG.GetRange(_indexFirst, _count);
                List<int> iCurrentListYFaultscreen = iListY_FaultSVG.GetRange(_indexFirst, _count);
                listgPolyline.Add(addgSinglePolyline(iCurrentListXFaultscreen, iCurrentListYFaultscreen, "red", 3));
            }

            return listgPolyline;
        }

        public void delNodeByID(string idPath)
        {
            XmlNode node = svgRoot.SelectSingleNode(idPath);
            if (node != null) node.RemoveAll();
        }

        public XmlElement gWellsGeologyProperty()
        {
            delNodeByID("/svg/g/g[@id='idGeoproperty']");
           
            XmlElement gWellsProperty = svgDoc.CreateElement("g");
            gWellsProperty.SetAttribute("id", "idGeoproperty");

             string sFontSize = "6";
            List<ItemWellView> listWellView = new List<ItemWellView>();

            foreach (XmlNode xn in xmlLayerMap.SelectNodes("/LayerMapConfig/WellGeologyProperty/WellValue"))
            {
                string _data = xn.InnerText;
                string[] split=_data.Split();
                Point pointConvert2View = cCordinationTransform.transRealPointF2ViewPoint(
                        double.Parse(split[1]), double.Parse(split[2]), xRef, yRef, this.dfscale);
                int iXview=pointConvert2View.X;
                int iYview = pointConvert2View.Y; 
                string sDCHD=split[3];
                string SH=split[4];
                string sYXHD=split[5];
                string STL=split[6];
                ItemWellMapLayer item = ItemWellMapLayer.parseLine(_data);
                

                gWellsProperty.AppendChild(gProperty(  iXview, iYview, sDCHD,SH,sYXHD,STL,sFontSize));

            }

            return gWellsProperty;
        }
        public XmlElement gProperty( int iXview, int iYview, string sDCHD,string SH,string sYXHD,string STL,string sFontSize)
        {

            XmlElement gWellLayerProperty = svgDoc.CreateElement("g");

            XmlElement gSHText = svgDoc.CreateElement("text");
            gSHText.SetAttribute("x", (iXview - 2).ToString());
            gSHText.SetAttribute("y", (iYview + 5).ToString());
            gSHText.SetAttribute("font-size", sFontSize);
            gSHText.SetAttribute("font-style", "normal");
            gSHText.SetAttribute("fill", "black");
            gWellLayerProperty.AppendChild(gSHText);
            if (float.Parse(sDCHD) > 0)
            {
                gSHText.InnerText = SH;

                XmlElement gYXHDText = svgDoc.CreateElement("text");
                gYXHDText.SetAttribute("x", (iXview - 2).ToString());
                gYXHDText.SetAttribute("y", iYview.ToString());
                gYXHDText.SetAttribute("font-size", sFontSize);
                //gYXHDText.SetAttribute("font-style", "normal");
                gYXHDText.SetAttribute("style", "text-decoration: underline;");
                gYXHDText.InnerText = sYXHD;
                gYXHDText.SetAttribute("fill", "black");
                gWellLayerProperty.AppendChild(gYXHDText);

                XmlElement gPath = svgDoc.CreateElement("path");
                string d = "M" + (iXview - 2).ToString() + " " + (iYview + 1).ToString() + "h8";
                gPath.SetAttribute("d", d);
                gPath.SetAttribute("y", iYview.ToString());
                gPath.SetAttribute("stroke", "black");
                gPath.SetAttribute("stroke-width", "0.5");
                gPath.SetAttribute("fill", "black");
                gWellLayerProperty.AppendChild(gPath);

                XmlElement gSTLText = svgDoc.CreateElement("text");
                gSTLText.SetAttribute("x", (iXview + 6).ToString());
                gSTLText.SetAttribute("y", (iYview + 3).ToString());
                gSTLText.SetAttribute("font-size", sFontSize);
                gSTLText.SetAttribute("font-style", "normal");
                gSTLText.InnerText = STL;
                gSTLText.SetAttribute("fill", "black");
                gWellLayerProperty.AppendChild(gSTLText);
            }
            else
            {
                gSHText.InnerText = "△";
            }

            return gWellLayerProperty;
        }
        public XmlElement gIPLine(Point PWell1, Point PWell2)
        {
            XmlElement gIPLine = svgDoc.CreateElement("g");
            gIPLine.SetAttribute("id", "gIPLine");

            gIPLine.SetAttribute("x1", PWell1.X.ToString());
            gIPLine.SetAttribute("y1", PWell1.Y.ToString());
            gIPLine.SetAttribute("x2", PWell2.X.ToString());
            gIPLine.SetAttribute("y2", PWell2.Y.ToString());
            gIPLine.SetAttribute("stroke", "black");
            gIPLine.SetAttribute("stroke-width", "1");

            return gIPLine;

        }
        /// <summary>
        /// add horizonalWellInterval 
        /// </summary>
        /// <param name="p0">wellhead view position</param>
        /// <param name="p1">A view position</param>
        /// <param name="p2">B view positon</param>
        /// <returns></returns>
        public XmlElement gHorizonalWellIntervelLine(Point p0, Point p1, Point p2)
        {
            XDocument xeLayerMap = XDocument.Load(this.xmlConfigPath);
            string _sLineWidth = xeLayerMap.Element("LayerMapConfig").Element("HorizonalWell").Element("lineWidth").Value;
            string _sLineColor = xeLayerMap.Element("LayerMapConfig").Element("HorizonalWell").Element("lineColor").Value;
            if (_sLineColor == "") _sLineColor = "black";
            int _iNoteFontSize = 8;

            XmlElement gHorizonWellIntervel = svgDoc.CreateElement("g");
            gHorizonWellIntervel.SetAttribute("id", "idHorizonWellLine");
            gHorizonWellIntervel.SetAttribute("stroke", _sLineColor);
            XmlElement gLine1 = svgDoc.CreateElement("line");
            gLine1.SetAttribute("x1", p0.X.ToString());
            gLine1.SetAttribute("y1", p0.Y.ToString());
            gLine1.SetAttribute("x2", p1.X.ToString());
            gLine1.SetAttribute("y2", p1.Y.ToString());
            gLine1.SetAttribute("stroke-dasharray", "3");
            gLine1.SetAttribute("stroke-width", _sLineWidth);
            gHorizonWellIntervel.AppendChild(gLine1);
          
            XmlElement gLine2 = svgDoc.CreateElement("line");
            gLine2.SetAttribute("x1", p1.X.ToString());
            gLine2.SetAttribute("y1", p1.Y.ToString());
            gLine2.SetAttribute("x2", p2.X.ToString());
            gLine2.SetAttribute("y2", p2.Y.ToString());
            gLine2.SetAttribute("stroke-dasharray", "0");
            gLine2.SetAttribute("stroke", _sLineColor);
            gLine2.SetAttribute("stroke-width", _sLineWidth);
            gHorizonWellIntervel.AppendChild(gLine2);

            XmlElement gA = svgDoc.CreateElement("text");
            gA.SetAttribute("x", (p1.X + 2).ToString());
            gA.SetAttribute("y", (p1.Y - 2).ToString());
            gA.InnerText = "A";
            gA.SetAttribute("font-size", _iNoteFontSize.ToString());
            gHorizonWellIntervel.AppendChild(gA);

            XmlElement gB = svgDoc.CreateElement("text");
            gB.SetAttribute("x", (p2.X + 2).ToString());
            gB.SetAttribute("y", (p2.Y - 2).ToString());
            gB.SetAttribute("font-size", _iNoteFontSize.ToString());
            //gB.SetAttribute("font-style", "normal");
            gB.InnerText = "B";
            //gB.SetAttribute("fill", m_colorWell);
            gHorizonWellIntervel.AppendChild(gB);

            return gHorizonWellIntervel;
        }

        public XmlElement gHorizonalWellIntervelLine(XmlNode horizinalNode)
        {
            string[] splitInnerText = horizinalNode.InnerText.Split();
            Point p0 = new Point(int.Parse(splitInnerText[2]), int.Parse(splitInnerText[3]));
            Point p1 = new Point(int.Parse(splitInnerText[4]), int.Parse(splitInnerText[5]));
            Point p2 = new Point(int.Parse(splitInnerText[6]), int.Parse(splitInnerText[7]));
            return  gHorizonalWellIntervelLine(p0, p1, p2);
        }


        public XmlElement gTrackLog(string sLogName, List<float> fListTVD, List<float> fListValue,
    int m_iLeftValue, int m_iRightValue, string m_sColorCurve)
        {

            float fVsacle = 1;
            int m_iTrackwidth = 25;
            string sLineWidth = "2"; 

            XmlElement gLogTrack = svgDoc.CreateElement("g");
            gLogTrack.SetAttribute("id", sLogName);
            string _points = "";
            float _xView_f = 0f;
            for (int i = 0; i < fListTVD.Count; i = i + 2)
            {
                if (-500 <= fListValue[i] && fListValue[i] < 1000)
                {
                    _xView_f = m_iTrackwidth * (fListValue[i] - m_iLeftValue) / (m_iRightValue - m_iLeftValue);
                }
                else
                {
                    _xView_f = m_iLeftValue;

                }
                _points = _points + (_xView_f).ToString() + ',' + ((fListTVD[i] - fListTVD[0]) * fVsacle).ToString() + " ";

            }
            XmlElement gLogPolyline = svgDoc.CreateElement("polyline");
            gLogPolyline.SetAttribute("stroke-width", sLineWidth);
            gLogPolyline.SetAttribute("stroke", m_sColorCurve);
            gLogPolyline.SetAttribute("fill", "none");
            gLogPolyline.SetAttribute("points", _points);
            gLogTrack.AppendChild(gLogPolyline);

            return gLogTrack;
        }


    }
}
