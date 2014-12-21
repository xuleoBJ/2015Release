using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DOGPlatform.SVG
{
    class cSVGXEPatternLithoSand
    {
        public Dictionary<string, int> dictionaryPatternSand = new Dictionary<string, int>();
        void initializeDictionaryPatternSand()
        {
            dictionaryPatternSand.Add("粗砂岩", 101);
            dictionaryPatternSand.Add("中砂岩", 102);
            dictionaryPatternSand.Add("细砂岩", 103);
            dictionaryPatternSand.Add("粉砂岩", 104);
            dictionaryPatternSand.Add("中细砂岩", 105);
            dictionaryPatternSand.Add("粉细砂岩", 106);
            dictionaryPatternSand.Add("石英砂岩", 107);
            dictionaryPatternSand.Add("铁质砂岩", 108);
            dictionaryPatternSand.Add("海绿石砂岩", 109);
            dictionaryPatternSand.Add("玄武质砂岩", 127);
        }

        public static void addLithoPatternSand(string filePath) 
        {
            XDocument xDoc = XDocument.Load(filePath);
            XElement xroot = xDoc.Root;
            //XElement xdefs1 = xDoc.Element("svg:defs");
            //string sLithoName = "砂岩";
            //int iWidthUnit = 20;
            //int iHeightUnit = 10;
            //string sBackColor = "yellow";

            //foreach (var tag in xroot.DescendantNodes()) MessageBox.Show(tag.ToString());
            if (xroot != null)
            {
                // bool x=xroot.HasElements("defs");
                XElement xdefs = xroot.Element("{http://www.w3.org/2000/svg}" + "defs");
                if (xdefs != null) xdefs.Add(cSVGXEPatternLithoSand.lithoPatternDefsSand("p123", 20, 10, 2, "yellow", "red", true));
                xroot.Add(cSVGXEPatternLithoSand.lithoPattern("p123"));

                xDoc.Save(filePath);
            }
        
        }


        //自定义样式就是自己把def文件加入inkscape里面
        public XElement addLithoPatternSand(string  filePath,string sLithoName, int iWidthUnit, int iHeightUnit, string backColor, int ix, int iy, int iWidth, int iheight)//增加岩石类型
        {
            XDocument xDoc = XDocument.Load(filePath);
            XElement xroot = xDoc.Root;
            //XElement xdefs1 = xDoc.Element("svg:defs");
            //string sLithoName = "砂岩";
            //int iWidthUnit = 20;
            //int iHeightUnit = 10;
            //string sBackColor = "yellow";

            //foreach (var tag in xroot.DescendantNodes()) MessageBox.Show(tag.ToString());
            if (xroot != null)
            {
                // bool x=xroot.HasElements("defs");
                XElement xdefs = xroot.Element("{http://www.w3.org/2000/svg}" + "defs");
                if (xdefs != null) xdefs.Add(cSVGXEPatternLithoSand.lithoPatternDefsSand("p123", 20, 10, 2, "yellow", "red", true));
                xroot.Add(cSVGXEPatternLithoSand.lithoPattern("p123"));

                xDoc.Save(filePath);
            }
            int idLitho = 0;
            //根据岩石名称选pattern;
            if (dictionaryPatternSand.ContainsKey(sLithoName)) idLitho = dictionaryPatternSand[sLithoName];
            string sURL = "#123456";
            XNamespace xn = "http://www.w3.org/2000/svg";
            //string sURL = "url(#" + lithoPatternDefsSand(idLitho, iWidthUnit, iHeightUnit, backColor) + ")";
            XElement gLithoPattern = new XElement(xn + "defs", new XAttribute("xmlns", "http://www.w3.org/2000/svg"));
            gLithoPattern.SetAttributeValue("id", "idLitho");
            XElement gLithoPatternRect = new XElement(xn + "rect", new XAttribute("xmlns", "http://www.w3.org/2000/svg"));
            gLithoPatternRect.SetAttributeValue("x", ix.ToString());
            gLithoPatternRect.SetAttributeValue("y", iy.ToString());
            gLithoPatternRect.SetAttributeValue("height", iheight.ToString());
            gLithoPatternRect.SetAttributeValue("width", iWidth.ToString());
            gLithoPatternRect.SetAttributeValue("style", "stroke-width:1");
            gLithoPatternRect.SetAttributeValue("stroke", "black");
            gLithoPatternRect.SetAttributeValue("fill", sURL);
            gLithoPattern.Add(gLithoPatternRect);
            return gLithoPattern;
        }

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


        //根据用户设置，形成pattern，存入ink的配置文件内。
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
