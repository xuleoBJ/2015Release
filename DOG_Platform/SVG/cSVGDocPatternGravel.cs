using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGDocPatternGravel:cSVGBasePattern
    {
        public cSVGDocPatternGravel(int iDX, int iDY)
        {
            initializedictionaryPatternGravel();
        }

        public Dictionary<string, int> dictionaryPatternGravel = new Dictionary<string, int>();
        void initializedictionaryPatternGravel()
        {
            dictionaryPatternGravel.Add("巨砾岩", 501);
            dictionaryPatternGravel.Add("粗砾岩", 502);
            dictionaryPatternGravel.Add("中砾岩", 503);
            dictionaryPatternGravel.Add("细砾岩", 504);
            dictionaryPatternGravel.Add("泥砾岩", 506);
            dictionaryPatternGravel.Add("角砾岩", 507);
            dictionaryPatternGravel.Add("凝灰质角砾岩", 513);
        }
        public XmlElement addLithoPatternGravel(string sLithoName, int iWidthUnit, int iHeightUnit, string backColor, string d)//增加岩石类型
        {
            int idLitho = 0;
            //根据岩石名称选pattern;
            if (dictionaryPatternGravel.ContainsKey(sLithoName)) idLitho = dictionaryPatternGravel[sLithoName];
            //根据岩石名称选pattern;
            string sURL = "url(#" + lithoPatternDefsGravel(idLitho, iWidthUnit, iHeightUnit, backColor) + ")";

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
        public string lithoPatternDefsGravel(int idLitho, int iWidthUnit, int iHeightUnit, string backColor)
        {
            int numColumn = 0;
            int numRow = 0;
            string fillColor = backColor;
            string strokeColor = backColor;

            bool hasSplitLine = true;

            //首先确定格式 单元格数和是否显示 分割线  
            List<XmlElement> listPatternMark = new List<XmlElement>();
            if (idLitho == 501)
            {
                numColumn = 2;
                numRow = 2;
                //XmlElement pattern1 = patternElementGravel(iWidthUnit, iHeightUnit, 0, 0,4);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern3 = patternElementGravel(iWidthUnit, iHeightUnit, 1, 1,4);
                //listPatternMark.Add(pattern3);
            }
            if (idLitho == 502)
            {
                numColumn = 2;
                numRow = 2;
                //XmlElement pattern1 = patternElementGravel(iWidthUnit, iHeightUnit, 0, 0, 2.5F);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern3 = patternElementGravel(iWidthUnit, iHeightUnit, 1, 1, 2.5F);
                //listPatternMark.Add(pattern3);
            }
            if (idLitho == 503)
            {
                numColumn = 2;
                numRow = 2;
                //XmlElement pattern1 = patternElementGravel(iWidthUnit, iHeightUnit, 0, 0, 2);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern3 = patternElementGravel(iWidthUnit, iHeightUnit, 1, 1, 2);
                //listPatternMark.Add(pattern3);
            }
            if (idLitho == 504)
            {
                numColumn = 2;
                numRow = 2;
                //XmlElement pattern1 = patternElementGravel(iWidthUnit, iHeightUnit, 0, 0, 1);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern3 = patternElementGravel(iWidthUnit, iHeightUnit, 1, 1, 1);
                //listPatternMark.Add(pattern3);
            }
            if (idLitho == 506)
            {
                numColumn = 2;
                numRow = 2;

                //XmlElement pattern1 = patternElementGravel(iWidthUnit, iHeightUnit, 0, 0, 1);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern2 = patternElementMud(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementGravel(iWidthUnit, iHeightUnit, 1, 1, 1);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern4 = patternElementMud(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern4);
            }

            if (idLitho == 507)
            {
                numColumn = 2;
                numRow = 2;

                //XmlElement pattern1 = patternElementTriGravel(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
 
                //XmlElement pattern3 = patternElementTriGravel(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern3);

            }
            if (idLitho == 513)
            {
                numColumn = 3;
                numRow = 2;
                //XmlElement pattern1 = patternElementTuff(iWidthUnit, iHeightUnit, 0, 0);
                //listPatternMark.Add(pattern1);
                //XmlElement pattern2 = patternElementTriGravel(iWidthUnit, iHeightUnit, 0, 1);
                //listPatternMark.Add(pattern2);
                //XmlElement pattern3 = patternElementTriGravel(iWidthUnit, iHeightUnit, 0, 2);
                //listPatternMark.Add(pattern3);
                //XmlElement pattern4 = patternElementTriGravel(iWidthUnit, iHeightUnit, 1, 0);
                //listPatternMark.Add(pattern4);
                //XmlElement pattern5 = patternElementTriGravel(iWidthUnit, iHeightUnit, 1, 1);
                //listPatternMark.Add(pattern5);
                //XmlElement pattern6 = patternElementTuff(iWidthUnit, iHeightUnit, 1, 2);
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
