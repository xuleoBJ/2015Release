﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DOGPlatform.SVG
{
    class cSVGXEPatternLithoSand
    {

        public static XElement lithoPattern(string sURL)
        {
            XNamespace  xn= "http://www.w3.org/2000/svg";
            
            XElement pattern = new XElement(xn+"g",new XAttribute("xmlns", "http://www.w3.org/2000/svg"));

            //pattern.SetAttributeValue("id", "idLithonSand");
            pattern.SetAttributeValue("stroke", "black");

            XElement gPath = new XElement(xn + "path");

            gPath.SetAttributeValue("stroke", "black");
            gPath.SetAttributeValue("d", "M5,5 c500,150 400,150 400,0  Z");
            gPath.SetAttributeValue("style", "stroke-width:1");
            gPath.SetAttributeValue("fill", "url(#"+sURL+")");
            pattern.Add(gPath);
            return pattern;
        }


        public static XElement lithoPatternDefsSand(string sURL, int iWidthUnit, int iHeightUnit, int rSand, string backColor, string circleInnerColor, bool hasSplitLine)
        {
            int numColumn = 0;
            int numRow = 0;
            string fillColor = backColor;
            string strokeColor = backColor;

            int size = rSand;

            //首先确定格式 单元格数和是否显示 分割线  
            List<XElement> listPatternMark = new List<XElement>();

            numColumn = 2;
            numRow = 2;

            XElement pattern1 = patternElementSand(iWidthUnit, iHeightUnit, 0, 0, size, circleInnerColor);
            listPatternMark.Add(pattern1);
            XElement pattern3 = patternElementSand(iWidthUnit, iHeightUnit, 1, 1, size, circleInnerColor);
            listPatternMark.Add(pattern3);

            XNamespace xn = "http://www.w3.org/2000/svg";
            XElement circleConglomerate = new XElement(xn + "circle", new XAttribute("xmlns", "http://www.w3.org/2000/svg"));
            XElement lithoPattern = new XElement(xn + "pattern", new XAttribute("xmlns", "http://www.w3.org/2000/svg"));
            lithoPattern.SetAttributeValue("id", sURL);
            lithoPattern.SetAttributeValue("patternUnits", "userSpaceOnUse");
            lithoPattern.SetAttributeValue("x", "0");
            lithoPattern.SetAttributeValue("y", "0");
            lithoPattern.SetAttributeValue("width", (iWidthUnit * numColumn).ToString());
            lithoPattern.SetAttributeValue("height", (iHeightUnit * numRow).ToString());
            lithoPattern.SetAttributeValue("viewBox", "0 0 " + (iWidthUnit * numColumn).ToString() + " " + (iHeightUnit * numRow).ToString());

            XElement gBackRect = backRect(backColor, iWidthUnit, iHeightUnit, numColumn, numRow);
            lithoPattern.Add(gBackRect);

            if (hasSplitLine == true)
            {
                lithoPattern.Add(splitLine(iWidthUnit, iHeightUnit, numColumn, numRow));
            }

            for (int i = 0; i < listPatternMark.Count; i++)
            {
                lithoPattern.Add(listPatternMark[i]);
            }

            return lithoPattern;
        }


        //分隔线
        public static XElement splitLine(int iWidthUnit, int iHeightUnit, int numColumn, int numRow)
        {
            XNamespace xn = "http://www.w3.org/2000/svg";
            XElement circleConglomerate = new XElement(xn + "circle", new XAttribute("xmlns", "http://www.w3.org/2000/svg"));
            XElement gSplitLine = new XElement(xn + "g", new XAttribute("xmlns", "http://www.w3.org/2000/svg"));
            for (int i = 0; i < numRow; i++)
            {
                XElement gPath = new XElement(xn + "path", new XAttribute("xmlns", "http://www.w3.org/2000/svg"));
                string dPath = "M 0 " + (iHeightUnit * i).ToString() + " h" + (iWidthUnit * numColumn).ToString();
                gPath.SetAttributeValue("d", dPath);
                gPath.SetAttributeValue("stroke-width", "0.5");
                gPath.SetAttributeValue("stroke", "black");
                gPath.SetAttributeValue("fill", "none");
                gSplitLine.Add(gPath);
            }
            return gSplitLine;
        }
        //砂符号
        public static XElement patternElementSand(int iWidthUnit, int iHeightUnit, int orderRow, int orderColumn, float fRadus, string fillColor)
        {
            int ix = iWidthUnit * orderColumn + iWidthUnit / 2; //元素X位置
            int iy = iHeightUnit * orderRow + iHeightUnit / 2; //元素Y位置
            XElement patternElement = circleSand(ix, iy, fRadus, fillColor);
            return patternElement;
        }

        public static XElement circleSand(int cx, int cy, float r, string fillColor)//砂岩圈，不填充
        {

            XNamespace xn = "http://www.w3.org/2000/svg";
            XElement circleConglomerate = new XElement(xn + "circle", new XAttribute("xmlns", "http://www.w3.org/2000/svg"));

            circleConglomerate.SetAttributeValue("cx", cx.ToString());
            circleConglomerate.SetAttributeValue("cy", cy.ToString());
            circleConglomerate.SetAttributeValue("r", r.ToString());
            circleConglomerate.SetAttributeValue("stroke", "black");
            circleConglomerate.SetAttributeValue("stroke-width", "0.5");
            circleConglomerate.SetAttributeValue("fill", fillColor);
            return circleConglomerate;
        }

        //背景rect
        public static XElement backRect(string backColor, int iWidthUnit, int iHeightUnit, int numColumn, int numRow)
        {
            XNamespace xn = "http://www.w3.org/2000/svg";
            XElement gBackRect = new XElement(xn + "rect", new XAttribute("xmlns", "http://www.w3.org/2000/svg"));
            gBackRect.SetAttributeValue("x", "0");
            gBackRect.SetAttributeValue("y", "0");
            gBackRect.SetAttributeValue("width", (iWidthUnit * numColumn).ToString());
            gBackRect.SetAttributeValue("height", (iHeightUnit * numRow).ToString());
            gBackRect.SetAttributeValue("stroke", backColor);
            gBackRect.SetAttributeValue("stroke-width", "1");
            gBackRect.SetAttributeValue("fill", backColor);
            return gBackRect;
        }
    }
}
