using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGSectionWell:cSVGBase
    {
        public XmlElement gWell { get; set; }
        public cSVGSectionWell()
        {
           gWell = svgDoc.CreateElement("g");
        }
        public cSVGSectionWell(string sJH)
        {
            gWell = svgDoc.CreateElement("g");
            gWell.SetAttribute("id", sJH);
        }
       
        public void addTrack(XmlElement gElement, int idx)  //剖面图Y不能移动
        {
            string sTranslate = "translate(" + idx.ToString() + ",0)";
            gElement.SetAttribute("transform", sTranslate);
            XmlNode importNewsItem = svgDoc.ImportNode(gElement, true);
            gWell.AppendChild(importNewsItem);
        }

        public XmlElement gWellCone(string sJH, float m_minMesureDepth, float m_maxMesureDepth,
            float m_KB, int m_tickInveral_main, int m_tickInveral_min)
        {
            XmlElement gWellCone = svgDoc.CreateElement("g");
            gWellCone.SetAttribute("id", sJH+"#bone" );
            gWellCone.SetAttribute("stroke", "black");
            gWellCone.SetAttribute("stroke-width", "0.5");
            XmlElement gJHHeadLine = svgDoc.CreateElement("line");
            gJHHeadLine.SetAttribute("x1", "-50");
            gJHHeadLine.SetAttribute("y1", (-m_KB + m_minMesureDepth).ToString());
            gJHHeadLine.SetAttribute("x2", "50");
            gJHHeadLine.SetAttribute("y2", (-m_KB + m_minMesureDepth).ToString());
            //gJHHeadLine.SetAttribute("stroke-width", "1");
            gWellCone.AppendChild(gJHHeadLine);

            XmlElement gJHText = svgDoc.CreateElement("text");
            gJHText.SetAttribute("x", "-5");
            gJHText.SetAttribute("y", (-m_KB - 10 + m_minMesureDepth).ToString());
            gJHText.SetAttribute("font-size", "10");
            gJHText.InnerText = sJH;
            gWellCone.AppendChild(gJHText);


            int iCurrentDepth = (Convert.ToInt16(m_minMesureDepth) / m_tickInveral_main + 1) * m_tickInveral_main;
            while (iCurrentDepth <= m_maxMesureDepth)
            {
                XmlElement gDepthTick = svgDoc.CreateElement("path");
                string d = "M0 " + ((-m_KB + iCurrentDepth).ToString()) + " h 3 ";
                gDepthTick.SetAttribute("d", d);
                gWellCone.AppendChild(gDepthTick);

                XmlElement gTickText = svgDoc.CreateElement("text");
                gTickText.SetAttribute("x", "3");
                gTickText.SetAttribute("y", (-m_KB + iCurrentDepth).ToString());
                gTickText.SetAttribute("font-size", "4");
            //    gTickText.SetAttribute("stroke-width", "1");
                gTickText.InnerText = iCurrentDepth.ToString();
                gWellCone.AppendChild(gTickText);
                iCurrentDepth = iCurrentDepth + m_tickInveral_main;
            }
            int m_iValueStartTinyTick = (Convert.ToInt16(m_minMesureDepth) / m_tickInveral_min + 1) * m_tickInveral_min;
            //cPublicMethodCommon.getCeilingNumer(m_minMesureDepth, m_tickInveral_min);
            while (m_iValueStartTinyTick <= m_maxMesureDepth)
            {
                XmlElement gDepthMinTick = svgDoc.CreateElement("path");
                string dDepthMinTick = "M0 " + (-m_KB + m_iValueStartTinyTick).ToString() + " h 2 ";
                gDepthMinTick.SetAttribute("d", dDepthMinTick);
                gWellCone.AppendChild(gDepthMinTick);
                m_iValueStartTinyTick = m_iValueStartTinyTick + m_tickInveral_min;
            }
            XmlElement gWellBoleLine = svgDoc.CreateElement("line");
            gWellBoleLine.SetAttribute("x1", "0");
            gWellBoleLine.SetAttribute("y1", (-m_KB + m_minMesureDepth).ToString());
            gWellBoleLine.SetAttribute("x2", "0");
            gWellBoleLine.SetAttribute("y2", (-m_KB + m_maxMesureDepth).ToString());
            gWellBoleLine.SetAttribute("stroke-width", "1");
            gWellCone.AppendChild(gWellBoleLine);
            XmlElement gWellHead = svgDoc.CreateElement("circle");
            gWellHead.SetAttribute("cx", "0");
            gWellHead.SetAttribute("cy", (-m_KB + m_minMesureDepth).ToString());
            gWellHead.SetAttribute("r", "3");
            gWellHead.SetAttribute("fill", "red");
            gWellCone.AppendChild(gWellHead);
            return gWellCone;
        }


        public XmlElement gPathWellCone(string sJH, float m_minMesureDepth, float m_maxMesureDepth,
           float m_KB, int m_tickInveral_main, int m_tickInveral_min)
        {

            XmlElement gWellCone = svgDoc.CreateElement("g");
            gWellCone.SetAttribute("id", sJH + "#bone");
            gWellCone.SetAttribute("stroke", "black");
            gWellCone.SetAttribute("stroke-width", "0.5");
           
            List<float> fListMD = new List<float>();
            float _fCurrentMD = m_minMesureDepth;
            int _iStep = 0;
            while (_fCurrentMD <= m_maxMesureDepth)
            {
                _iStep++;
                fListMD.Add(_fCurrentMD);
                _fCurrentMD = (int)m_minMesureDepth / 10 * 10+_iStep*m_tickInveral_main; 
            }
            List<ItemWellPath> listWellPath = cIOinputWellPath.getWellPathItemListByJHAndMDList(sJH, fListMD);
            string _pointWellPath = "";
            for (int i = 0; i < fListMD.Count; i++)
            {
                ItemWellPath currentWellPath = listWellPath[i];
                double _x0 = currentWellPath.f_dx;
                double _y0 = -m_KB + currentWellPath.f_TVD;
                _pointWellPath = _pointWellPath + _x0.ToString() + ',' + _y0.ToString() + " ";

                XmlElement gDepthTick = svgDoc.CreateElement("path");
                string d = "M "+_x0.ToString() +" " + _y0.ToString() + " h 3 ";
                gDepthTick.SetAttribute("d", d);
                gWellCone.AppendChild(gDepthTick);

                XmlElement gTickText = svgDoc.CreateElement("text");
                gTickText.SetAttribute("x", (_x0+3).ToString());
                gTickText.SetAttribute("y", _y0.ToString());
                gTickText.SetAttribute("font-size", "4");
                 gTickText.SetAttribute("stroke-width", "0.5");
                gTickText.InnerText = fListMD[i].ToString();
                gWellCone.AppendChild(gTickText);
            }
            XmlElement gJHHeadLine = svgDoc.CreateElement("line");
            gJHHeadLine.SetAttribute("x1", (listWellPath[0].f_dx-50).ToString());
            gJHHeadLine.SetAttribute("y1", (-m_KB + listWellPath[0].f_TVD).ToString());
            gJHHeadLine.SetAttribute("x2", (listWellPath[0].f_dx+50).ToString());
            gJHHeadLine.SetAttribute("y2", (-m_KB + listWellPath[0].f_TVD).ToString());
            //gJHHeadLine.SetAttribute("stroke-width", "1");
            gWellCone.AppendChild(gJHHeadLine);

            XmlElement gJHText = svgDoc.CreateElement("text");
            gJHText.SetAttribute("x", (listWellPath[0].f_dx-5).ToString());
            gJHText.SetAttribute("y", (-m_KB - 10 + listWellPath[0].f_TVD).ToString());
            gJHText.SetAttribute("font-size", "10");
            gJHText.InnerText = sJH;
            gWellCone.AppendChild(gJHText);
            XmlElement gWellHead = svgDoc.CreateElement("circle");
            gWellHead.SetAttribute("cx", (listWellPath[0].f_dx).ToString());
            gWellHead.SetAttribute("cy", (-m_KB + listWellPath[0].f_TVD).ToString());
            gWellHead.SetAttribute("r", "3");
            gWellHead.SetAttribute("fill", "red");
            gWellCone.AppendChild(gWellHead);

            XmlElement gWellPath = svgDoc.CreateElement("polyline");
            gWellPath.SetAttribute("style", "stroke-width:1");
            gWellPath.SetAttribute("stroke", "black");
            gWellPath.SetAttribute("fill", "none");
            gWellPath.SetAttribute("points", _pointWellPath);
            gWellCone.AppendChild(gWellPath);

          
            return gWellCone;
            
        }
    }
}
