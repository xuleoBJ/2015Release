using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGDocPatternCarbonatite : cSVGBasePattern
    {

      
        public Dictionary<string, int> dictionaryPatternCarbonatite = new Dictionary<string, int>();
        void initializeDictionaryPatternCarbonatite()
        {
            dictionaryPatternCarbonatite.Add("石灰岩", 201);
            dictionaryPatternCarbonatite.Add("白云岩", 202);
            dictionaryPatternCarbonatite.Add("鲕粒灰岩", 224);
        }
        public cSVGDocPatternCarbonatite(int iDX, int iDY)
        {
            initializeDictionaryPatternCarbonatite();
        }
        public XmlElement addLithoLimesPattern(string sLithoName, int iWidthUnit, int iHeightUnit, string backColor, string d)//增加岩石类型
        {
            //根据岩石名称选pattern;
            int idLitho = 0;
            //根据岩石名称选pattern;
            if (dictionaryPatternCarbonatite.ContainsKey(sLithoName)) idLitho = dictionaryPatternCarbonatite[sLithoName];
            string sURL = "url(#" + lithoPatternLimesDefs(idLitho, iWidthUnit, iHeightUnit, backColor) + ")";

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

        public string lithoPatternLimesDefs(int idLitho, int iWidthUnit, int iHeightUnit, string backColor)
        {
            int numColumn = 0;
            int numRow = 0;
            string fillColor = backColor;
            string strokeColor = backColor;
            bool hasSplitLine = true;

            //首先确定格式 单元格数和是否显示 分割线  
            List<XmlElement> listPatternMark = new List<XmlElement>();
            if (idLitho == 201)
            {
                listPatternMark.Clear();
                numColumn = 4;
                numRow = 2;
                //XmlElement pattern1 = patternElementLimes(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern2 = patternElementLimes(iWidthUnit, iHeightUnit, 0, 2);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementLimes(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern4= patternElementLimes(iWidthUnit, iHeightUnit, 1, 3);
                //listPatternMark.Add(pattern4);
            }
            if (idLitho == 202)
            {
                listPatternMark.Clear();
                numColumn = 4;
                numRow = 2;
                //XmlElement pattern1 = patternElementDolomite(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern2 = patternElementDolomite(iWidthUnit, iHeightUnit, 0, 2);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementDolomite(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern4 = patternElementDolomite(iWidthUnit, iHeightUnit, 1, 3);
                //listPatternMark.Add(pattern4);
            }

            if (idLitho == 224)
            {
                listPatternMark.Clear();
                numColumn = 4;
                numRow = 2;
                //XmlElement pattern1 = patternElementOolite(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern2 = patternElementLimes(iWidthUnit, iHeightUnit, 0, 1);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3= patternElementOolite(iWidthUnit, iHeightUnit, 0, 2);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern4 = patternElementLimes(iWidthUnit, iHeightUnit, 0, 3);
                //listPatternMark.Add(pattern4);
                //XmlElement pattern5 = patternElementLimes(iWidthUnit, iHeightUnit, 1, 0);
                //listPatternMark.Add(pattern5);
                //XmlElement pattern6 = patternElementOolite(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern6);
                //XmlElement pattern7 = patternElementLimes(iWidthUnit, iHeightUnit, 1, 2);
                //listPatternMark.Add(pattern7);
                //XmlElement pattern8 = patternElementOolite(iWidthUnit, iHeightUnit, 1, 3);
                //listPatternMark.Add(pattern8);
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
            //    XElement gSplitLine = splitLine(iWidthUnit, iHeightUnit, numColumn, numRow);
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
