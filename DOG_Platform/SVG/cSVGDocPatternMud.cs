using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGDocPatternMud : cSVGBasePattern
    {
        public cSVGDocPatternMud(int iDX, int iDY)
        {
            initializeDictionaryPatternMud();
        }
        public Dictionary<string, int> dictionaryPatternMud = new Dictionary<string, int>();
        void initializeDictionaryPatternMud()
        {
            dictionaryPatternMud.Add("泥岩", 401);
            dictionaryPatternMud.Add("粉砂质泥岩", 402);
            dictionaryPatternMud.Add("砂质泥岩", 403);
            dictionaryPatternMud.Add("灰质泥岩", 406);
            dictionaryPatternMud.Add("石膏质泥岩", 409);
        }

        public XmlElement addLithoPatternMud(string sLithoName, int iWidthUnit, int iHeightUnit, string backColor, string d)//增加岩石类型
        {
            int idLitho = 0;
            //根据岩石名称选pattern;
            if (dictionaryPatternMud.ContainsKey(sLithoName)) idLitho = dictionaryPatternMud[sLithoName];
            //根据岩石名称选pattern;
            string sURL = "url(#" + lithoPatternDefsMud(idLitho, iWidthUnit, iHeightUnit, backColor) + ")";

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
        public string lithoPatternDefsMud(int idLitho, int iWidthUnit, int iHeightUnit, string backColor)
        {
            int numColumn = 0;
            int numRow = 0;
            string fillColor = backColor;
            string strokeColor = backColor;

            bool hasSplitLine = true;

            //首先确定格式 单元格数和是否显示 分割线  
            List<XmlElement> listPatternMark = new List<XmlElement>();
            if (idLitho == 401)
            {
                numColumn = 2;
                numRow = 2;
                //XmlElement pattern1 = patternElementMud(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern3 = patternElementMud(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern3);
            }
            if (idLitho == 402)
            {
                numColumn = 2;
                numRow = 2;
                //XmlElement pattern1 = patternElementMud(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern2 = patternElementSiltSand(iWidthUnit, iHeightUnit, 0, 1, 0.5F, "yellow");
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementMud(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern4 = patternElementSiltSand(iWidthUnit, iHeightUnit, 1, 0, 0.5F, "yellow");
                //listPatternMark.Add(pattern4);
            }
            if (idLitho == 403)
            {
                numColumn = 3;
                numRow = 2;
                //XmlElement pattern1 = patternElementSand(iWidthUnit, iHeightUnit, 0, 0, 1F, "yellow");
                //listPatternMark.Add(pattern1);
                //XmlElement pattern2 = patternElementMud(iWidthUnit, iHeightUnit, 0, 1);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementMud(iWidthUnit, iHeightUnit, 0, 2);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern4 = patternElementMud(iWidthUnit, iHeightUnit, 1, 0);
                //listPatternMark.Add(pattern4);
                //XmlElement pattern5 = patternElementMud(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern5);
                //XmlElement pattern6 = patternElementSand(iWidthUnit, iHeightUnit, 1, 2, 1F, "yellow");
                //listPatternMark.Add(pattern6);
            }
            if (idLitho == 406)
            {
                numColumn = 3;
                numRow = 2;
                //XmlElement pattern1 = patternElementLimes(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern2 = patternElementMud(iWidthUnit, iHeightUnit, 0, 1);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementMud(iWidthUnit, iHeightUnit, 0, 2);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern4 = patternElementMud(iWidthUnit, iHeightUnit, 1, 0);
                //listPatternMark.Add(pattern4);
                //XmlElement pattern5 = patternElementMud(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern5);
                //XmlElement pattern6 = patternElementLimes(iWidthUnit, iHeightUnit, 1, 2);
                //listPatternMark.Add(pattern6);
            }
            if (idLitho == 409)
            {
                numColumn = 3;
                numRow = 2;
                //XmlElement pattern1 =patternElementGypsum(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern2 = patternElementMud(iWidthUnit, iHeightUnit, 0, 1);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementMud(iWidthUnit, iHeightUnit, 0, 2);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern4 = patternElementMud(iWidthUnit, iHeightUnit, 1, 0);
                //listPatternMark.Add(pattern4);
                //XmlElement pattern5 = patternElementMud(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern5);
                //XmlElement pattern6 = patternElementGypsum(iWidthUnit, iHeightUnit, 1, 2);
                //listPatternMark.Add(pattern6);
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
