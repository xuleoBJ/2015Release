﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Linq;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGBase
    {
        public XmlDocument svgDoc = new XmlDocument();
        public const string svgNS = "http://www.w3.org/1999/xlink";
        public int iSVGoffsetX = 0;
        public int iSVGoffsetY = 0;
        public int iSVGWidth = 200;
        public int iSVGHeight = 1000;
        public XmlElement svgRoot;
        public XmlDeclaration svgDec;
        public XmlElement gSVG;
        public XmlElement svgScript;
        public XmlElement svgCss;
        public XmlElement svgDefs;
        public XmlAttribute xLinkNode;

        public cSVGBase()
            : this(0, 0)
        {

        }
        public cSVGBase(int _iDX, int _iDY)
            : this(2000, 1500, _iDX, _iDY)
        {

        }
        public cSVGBase(int _iWidth, int _iHeight, int _iDX, int _iDY)
        {
            this.iSVGWidth = _iWidth;
            this.iSVGHeight = _iHeight;
            this.iSVGoffsetX = _iDX;
            this.iSVGoffsetY = _iDY;
            svgDec = svgDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            svgDoc.AppendChild(svgDec);
            svgRoot = svgDoc.CreateElement("svg");
            svgRoot.SetAttribute("xmlns:svg", "http://www.w3.org/2000/svg");
            svgRoot.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
            xLinkNode = svgDoc.CreateAttribute("xlink", "href", "http://www.w3.org/1999/xlink");
            svgRoot.Attributes.Append(xLinkNode);
            svgRoot.SetAttribute("version", "1.1");
            svgRoot.SetAttribute("height", this.iSVGHeight.ToString() + "px");
            svgRoot.SetAttribute("width", this.iSVGWidth.ToString()+ "px");
            string sViewBox = "0 0 " + this.iSVGWidth.ToString() + " " + this.iSVGHeight.ToString();
            svgRoot.SetAttribute("viewBox", sViewBox);
            svgDefs = svgDoc.CreateElement("defs");
            svgRoot.AppendChild(svgDefs);

            svgCss = svgDoc.CreateElement("style");
            svgRoot.AppendChild(svgCss);

            svgScript = svgDoc.CreateElement("script");

            svgScript.SetAttribute("type", "application/ecmascript");
            XmlAttribute striptXL = svgDoc.CreateAttribute("xlink", "href", "http://www.w3.org/1999/xlink");
            //striptXL.Value = "xl.js";
            svgScript.Attributes.Append(striptXL);
            svgRoot.AppendChild(svgScript);

            gSVG = svgDoc.CreateElement("g");
            string sTranslate = "translate(" + iSVGoffsetX.ToString() + "," + iSVGoffsetY.ToString() + ")";
            gSVG.SetAttribute("transform", sTranslate);
            gSVG.SetAttribute("id", "idAllg");
            gSVG.SetAttribute("xml:space", "preserve");
            svgRoot.AppendChild(gSVG);
            svgDoc.AppendChild(svgRoot);

        }

        public cSVGBase(int iWidth, int iHeight, int iDX, int iDY, string sPageUnit)
            : this(iWidth, iHeight, iDX, iDY)
        {
            this.iSVGoffsetX = iDX;
            this.iSVGoffsetY = iDY;
            svgRoot.SetAttribute("height", iHeight.ToString() + sPageUnit);
            svgRoot.SetAttribute("width", iWidth.ToString() + sPageUnit);
        }
        public void addgElement(XmlElement gElement, int ix, int iy)
        {
            string sTranslate = "translate(" + ix.ToString() + "," + iy.ToString() + ")";
            gElement.SetAttribute("transform", sTranslate);
            this.gSVG.AppendChild(gElement);
        }

        public void addSVGTitle()
        {
            addSVGTitle("Title", 0, 0);
        }
        public void addSVGTitle(int ix, int iy)
        {
            addSVGTitle("Title", ix, iy);
        }
        public void addSVGTitle(string sTitle, int ix, int iy)
        {
            XmlElement svgTitle = svgDoc.CreateElement("text");
            svgTitle.SetAttribute("id", "idTitle");
            svgTitle.SetAttribute("x", ix.ToString());
            svgTitle.SetAttribute("y", iy.ToString());
            svgTitle.SetAttribute("font-size", "10pt");
            svgTitle.SetAttribute("fill", "red");
            svgTitle.InnerText = sTitle;
            svgRoot.AppendChild(svgTitle);
        }
        public void addSVGTitle(string sTitle)
        {
            addSVGTitle(sTitle, 0, 0);
        }


        public void makeSVGfile(string filePath)
        {
            svgDoc.Save(filePath);
        }





    }
}
