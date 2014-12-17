using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace DOGPlatform.SVG
{
    class cSVGSectionTrackElevationRuler : cSVGSectionTrack
    {
        public cSVGSectionTrackElevationRuler() 
        {
        
        }
        public  XmlElement gElevationRuler(int m_minElevationDepth, int m_maxElevationDepth, int m_tickInveral_main)
        {
            XmlElement gElevationRuler = svgDoc.CreateElement("g");
            gElevationRuler.SetAttribute("id", "idTrackElevationRuler");
            XmlElement gLine = svgDoc.CreateElement("line");
            gLine.SetAttribute("x1", "0");
            gLine.SetAttribute("y1", (-m_minElevationDepth).ToString());
            gLine.SetAttribute("x2", "0");
            gLine.SetAttribute("y2", (-m_maxElevationDepth).ToString());
            gLine.SetAttribute("stroke", "black");
            gLine.SetAttribute("stroke-width", "0.5");
            gElevationRuler.AppendChild(gLine);
            XmlElement gRect = svgDoc.CreateElement("rect");
            gRect.SetAttribute("x", "0");
            gRect.SetAttribute("y", (-m_maxElevationDepth).ToString());
            gRect.SetAttribute("width", "50");
            gRect.SetAttribute("height", (m_maxElevationDepth-m_minElevationDepth).ToString());
            gRect.SetAttribute("style", "stroke-width:0.5");
            gRect.SetAttribute("stroke", "black");
            gRect.SetAttribute("fill", "none");
            gElevationRuler.AppendChild(gRect);
            int iCurrentDepth = (Convert.ToInt16(m_minElevationDepth) / m_tickInveral_main + 1) * m_tickInveral_main;
            while (iCurrentDepth <= m_maxElevationDepth)
            {
                XmlElement gDepthTick = svgDoc.CreateElement("path");
                gDepthTick.SetAttribute("stroke-width", "1");
                string d = "M 50 " + (-iCurrentDepth).ToString() + " h -8 ";
                if  (iCurrentDepth % m_tickInveral_main != 0)
                {
                    d = "M 50 " + (-iCurrentDepth).ToString() + " h -4 "; 
                }
                gDepthTick.SetAttribute("stroke", "black");
                gDepthTick.SetAttribute("d", d);
                gElevationRuler.AppendChild(gDepthTick);

                if (iCurrentDepth % m_tickInveral_main == 0)
                {
                    XmlElement gTickText = svgDoc.CreateElement("text");
                    gTickText.SetAttribute("x", "4");
                    gTickText.SetAttribute("y", (-iCurrentDepth).ToString());
                    gTickText.SetAttribute("fill", "black");
                    gTickText.SetAttribute("font-size", "12");
                    gTickText.SetAttribute("strole-width", "0.5");
                    gTickText.InnerText = iCurrentDepth.ToString();
                    gElevationRuler.AppendChild(gTickText);
                }
                iCurrentDepth = iCurrentDepth + m_tickInveral_main / 5; 

            }

            return gElevationRuler;

        }
    }
}
