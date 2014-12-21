using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DOGPlatform.SVG
{
    class cSVGXEPatternCarbonatie : cSVGXEPatternBase
    {

        public  static XElement lithoPatternLimesDefs(string stockId, string sURL, int idLitho, int iWidthUnit, int iHeightUnit, string backColor)
        {
            int numColumn = 0;
            int numRow = 0;
            string fillColor = backColor;
            string strokeColor = backColor;
            bool hasSplitLine = true;

            XNamespace xn = "http://www.w3.org/2000/svg";
            XNamespace inkscape = "http://www.inkscape.org/namespaces/inkscape";
            //首先确定格式 单元格数和是否显示 分割线  
            List<XElement> listPatternMark = new List<XElement>();
            if (idLitho == 201)
            {
                listPatternMark.Clear();
                numColumn = 4;
                numRow = 2;
                XElement pattern1 = patternElementLimes(iWidthUnit, iHeightUnit, 0, 0);
                listPatternMark.Add(pattern1);
                XElement pattern2 = patternElementLimes(iWidthUnit, iHeightUnit, 0, 2);
                listPatternMark.Add(pattern2);
                XElement pattern3 = patternElementLimes(iWidthUnit, iHeightUnit, 1, 1);
                listPatternMark.Add(pattern3);
                XElement pattern4 = patternElementLimes(iWidthUnit, iHeightUnit, 1, 3);
                listPatternMark.Add(pattern4);
            }
            if (idLitho == 202)
            {
                listPatternMark.Clear();
                numColumn = 4;
                numRow = 2;
                XElement pattern1 = patternElementDolomite(iWidthUnit, iHeightUnit, 0, 0);
                listPatternMark.Add(pattern1);
                XElement pattern2 = patternElementDolomite(iWidthUnit, iHeightUnit, 0, 2);
                listPatternMark.Add(pattern2);
                XElement pattern3 = patternElementDolomite(iWidthUnit, iHeightUnit, 1, 1);
                listPatternMark.Add(pattern3);
                XElement pattern4 = patternElementDolomite(iWidthUnit, iHeightUnit, 1, 3);
                listPatternMark.Add(pattern4);
            }

            if (idLitho == 224)
            {
                listPatternMark.Clear();
                numColumn = 4;
                numRow = 2;
                XElement pattern1 = patternElementOolite(iWidthUnit, iHeightUnit, 0, 0);
                listPatternMark.Add(pattern1);
                XElement pattern2 = patternElementLimes(iWidthUnit, iHeightUnit, 0, 1);
                listPatternMark.Add(pattern2);
                XElement pattern3 = patternElementOolite(iWidthUnit, iHeightUnit, 0, 2);
                listPatternMark.Add(pattern3);
                XElement pattern4 = patternElementLimes(iWidthUnit, iHeightUnit, 0, 3);
                listPatternMark.Add(pattern4);
                XElement pattern5 = patternElementLimes(iWidthUnit, iHeightUnit, 1, 0);
                listPatternMark.Add(pattern5);
                XElement pattern6 = patternElementOolite(iWidthUnit, iHeightUnit, 1, 1);
                listPatternMark.Add(pattern6);
                XElement pattern7 = patternElementLimes(iWidthUnit, iHeightUnit, 1, 2);
                listPatternMark.Add(pattern7);
                XElement pattern8 = patternElementOolite(iWidthUnit, iHeightUnit, 1, 3);
                listPatternMark.Add(pattern8);
            }

            XElement lithoPattern = new XElement(xn + "pattern");
            XAttribute stockid = new XAttribute(inkscape + "stockid", stockId);
            lithoPattern.Add(stockid);
            XAttribute collect = new XAttribute(inkscape + "collect", "always");
            lithoPattern.Add(collect);
            lithoPattern.SetAttributeValue("id", sURL);
            lithoPattern.SetAttributeValue("id", idLitho.ToString());
            lithoPattern.SetAttributeValue("patternUnits", "userSpaceOnUse");
            lithoPattern.SetAttributeValue("x", "0");
            lithoPattern.SetAttributeValue("y", "0");
            lithoPattern.SetAttributeValue("width", (iWidthUnit * numColumn).ToString());
            lithoPattern.SetAttributeValue("height", (iHeightUnit * numRow).ToString());

            XElement gBackRect = backRect(backColor, iWidthUnit, iHeightUnit, numColumn, numRow);
            lithoPattern.Add(gBackRect);

            if (hasSplitLine == true) lithoPattern.Add(splitLine(iWidthUnit, iHeightUnit, numColumn, numRow));

    

            for (int i = 0; i < listPatternMark.Count; i++)
            {
                lithoPattern.Add(listPatternMark[i]);
            }

            return lithoPattern;
          
        }
    }
}
