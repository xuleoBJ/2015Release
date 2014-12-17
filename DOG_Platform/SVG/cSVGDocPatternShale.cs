using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGDocPatternShale : cSVGBasePattern
    {
        public cSVGDocPatternShale(int iDX, int iDY)
        {
            initializeDictionaryPatternShale();
        }
        public Dictionary<string, int> dictionaryPatternShale = new Dictionary<string, int>();
        void initializeDictionaryPatternShale()
        {
            dictionaryPatternShale.Add("页岩", 301);
            dictionaryPatternShale.Add("砂质页岩", 302);
            dictionaryPatternShale.Add("沥青质页岩", 305);
        }

        public XmlElement addLithoShalePattern(string sLithoName, int iWidthUnit, int iHeightUnit, string backColor, string d)//增加岩石类型
        {
            //根据岩石名称选pattern;
            int idLitho = 0;
            //根据岩石名称选pattern;
            if (dictionaryPatternShale.ContainsKey(sLithoName)) idLitho = dictionaryPatternShale[sLithoName];
            string sURL = "url(#" + lithoPatternShaleDefs(idLitho, iWidthUnit, iHeightUnit, backColor) + ")";

            XmlElement gLithoPattern = svgDoc.CreateElement("g");
            gLithoPattern.SetAttribute("ID", "idLitho");

            XmlElement gLithoPatternPath = svgDoc.CreateElement("path");
            gLithoPatternPath.SetAttribute("d", d);
            gLithoPatternPath.SetAttribute("style", "stroke-width:0.1");
            gLithoPatternPath.SetAttribute("stroke", "black");
            gLithoPatternPath.SetAttribute("fill", sURL);

            gLithoPattern.AppendChild(gLithoPatternPath);
            return gLithoPattern;
        }

        public string lithoPatternShaleDefs(int idLitho, int iWidthUnit, int iHeightUnit, string backColor)
        {
            int numColumn = 0;
            int numRow = 0;
            string fillColor = backColor;
            string strokeColor = backColor;
            bool hasSplitLine = true;

            //首先确定格式 单元格数和是否显示 分割线  
            List<XmlElement> listPatternMark = new List<XmlElement>();
            if (idLitho == 301)
            {
                listPatternMark.Clear();
                numColumn = 1;
                numRow = 1;
                //XmlElement pattern1 = patternElementShale(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
            }
            if (idLitho == 302)
            {
                listPatternMark.Clear();
                numColumn = 2;
                numRow = 2;
              
                //XmlElement pattern2 = patternElementShale(iWidthUnit, iHeightUnit, 0, 1);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementShale(iWidthUnit, iHeightUnit, 1, 0);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern5 = patternElementShale(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern5);
                //XmlElement pattern6 = patternElementShale(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern6);
                //XmlElement pattern1 = patternElementSand(iWidthUnit, iHeightUnit, 0, 0, 1, "yellow");
                //listPatternMark.Add(pattern1);
                //XmlElement pattern4 = patternElementSand(iWidthUnit, iHeightUnit, 1, 1, 1, "yellow");
                //listPatternMark.Add(pattern4);
            }

            if (idLitho == 305)
            {
                listPatternMark.Clear();
                numColumn = 2;
                numRow = 2;

                //XmlElement pattern2 = patternElementShale(iWidthUnit, iHeightUnit, 0, 1);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementShale(iWidthUnit, iHeightUnit, 1, 0);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern5 = patternElementShale(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern5);
                //XmlElement pattern6 = patternElementShale(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern6);
                //XmlElement pattern1 = patternElementAsphalt(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern4 = patternElementAsphalt(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern4);
            }


            XmlElement lithoPattern = svgDoc.CreateElement("pattern");
            lithoPattern.SetAttribute("id", idLitho.ToString());
            lithoPattern.SetAttribute("patternUnits", "userSpaceOnUse");
            lithoPattern.SetAttribute("x", "0");
            lithoPattern.SetAttribute("y", "0");
            lithoPattern.SetAttribute("width", (iWidthUnit * numColumn).ToString());
            lithoPattern.SetAttribute("height", (iHeightUnit * numRow).ToString());
            lithoPattern.SetAttribute("viewBox", "0 0 " + (iWidthUnit * numColumn).ToString() + " " + (iHeightUnit * numRow).ToString());

            //XmlElement gBackRect = backRect(backColor, iWidthUnit, iHeightUnit, numColumn, numRow);
            //lithoPattern.AppendChild(gBackRect);

            //if (hasSplitLine == true)
            //{
            //    XmlElement gSplitLine = splitLine(iWidthUnit, iHeightUnit, numColumn, numRow);
            //    lithoPattern.AppendChild(gSplitLine);
            //}

            for (int i = 0; i < listPatternMark.Count; i++)
            {
                lithoPattern.AppendChild(listPatternMark[i]);
            }

            svgDefs.AppendChild(lithoPattern);

            return idLitho.ToString();
        }


    }
}
