using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGDocPatternFace:cSVGBasePattern
    {/*
        public cSVGDocPatternFace( int iDX, int iDY)
            : base( iDX, iDY)
        {
        
        }

        public string addFaceFloodplainPatternDefs() //泛滥平原
        {
            string sIDPattern = "idFaceFloodplain";
            XmlElement sandBodyPattern = svgDoc.CreateElement("pattern");
            sandBodyPattern.SetAttribute("id", sIDPattern);
            sandBodyPattern.SetAttribute("patternUnits", "userSpaceOnUse");
            sandBodyPattern.SetAttribute("x", "0");
            sandBodyPattern.SetAttribute("y", "0");
            sandBodyPattern.SetAttribute("width", "10");
            sandBodyPattern.SetAttribute("height", "5");
            sandBodyPattern.SetAttribute("viewBox", "0 0 10 5");

            XmlElement gRect = svgDoc.CreateElement("rect");
            gRect.SetAttribute("x", "0");
            gRect.SetAttribute("y", "0");
            gRect.SetAttribute("width", "10");
            gRect.SetAttribute("height", "5");
            gRect.SetAttribute("stroke", "none");
            //gRect.SetAttribute("style", "stroke-width:0.1");
            gRect.SetAttribute("fill", "rgb(0,255,0)");
            sandBodyPattern.AppendChild(gRect);

            XmlElement gPath = svgDoc.CreateElement("path");
            gPath.SetAttribute("d", "M 1 1 L2 2 L3 1");
            gPath.SetAttribute("stroke", "black");
            gPath.SetAttribute("style", "stroke-width:0.1");
            gPath.SetAttribute("fill", "none");
            sandBodyPattern.AppendChild(gPath);
            svgDefs.AppendChild(sandBodyPattern);
            return sIDPattern;
        }
        public XmlElement addFaceFloodPlain(string d)
        {
            string sURL = "url(#" + addFaceFloodplainPatternDefs() + ")";
            XmlElement gFace = svgDoc.CreateElement("g");
            gFace.SetAttribute("ID", "idFace");

            XmlElement gFacePath = svgDoc.CreateElement("path");
            gFacePath.SetAttribute("d", d);
            gFacePath.SetAttribute("style", "stroke-width:0.1");
            gFacePath.SetAttribute("stroke", "black");
            gFacePath.SetAttribute("fill", sURL);

            gFace.AppendChild(gFacePath);
            return gFace;
        }
        public string addFaceChannelSandPatternDefs() //河道砂
        {
            string sIDPattern = "idFaceChannelSand";
            XmlElement sandBodyPattern = svgDoc.CreateElement("pattern");
            sandBodyPattern.SetAttribute("id", sIDPattern);
            sandBodyPattern.SetAttribute("patternUnits", "userSpaceOnUse");
            sandBodyPattern.SetAttribute("x", "0");
            sandBodyPattern.SetAttribute("y", "0");
            sandBodyPattern.SetAttribute("width", "10");
            sandBodyPattern.SetAttribute("height", "5");
            sandBodyPattern.SetAttribute("viewBox", "0 0 10 5");

            XmlElement gRect = svgDoc.CreateElement("rect");
            gRect.SetAttribute("x", "0");
            gRect.SetAttribute("y", "0");
            gRect.SetAttribute("width", "10");
            gRect.SetAttribute("height", "5");
            gRect.SetAttribute("stroke", "none");
            gRect.SetAttribute("fill-opacity", "0.5");
            gRect.SetAttribute("fill", "rgb(255,255,0)");
            sandBodyPattern.AppendChild(gRect);

            XmlElement gSandCircle = svgDoc.CreateElement("circle");
            gSandCircle.SetAttribute("cx", "2.5");
            gSandCircle.SetAttribute("cy", "2.5");
            gSandCircle.SetAttribute("r", "0.5");
            sandBodyPattern.AppendChild(gSandCircle);
            svgDefs.AppendChild(sandBodyPattern);
            return sIDPattern;
        }
        public XmlElement addFaceChannelSand(string d)//河道砂
        {
            string sURL = "url(#" + addFaceChannelSandPatternDefs() + ")";
            XmlElement gFace = svgDoc.CreateElement("g");
            gFace.SetAttribute("ID", "idFace");

            XmlElement gFacePath = svgDoc.CreateElement("path");
            gFacePath.SetAttribute("d", d);
            gFacePath.SetAttribute("style", "stroke-width:0.1");
            gFacePath.SetAttribute("stroke", "black");
            gFacePath.SetAttribute("fill", sURL);

            gFace.AppendChild(gFacePath);
            return gFace;
        }
      * */
    }
}
