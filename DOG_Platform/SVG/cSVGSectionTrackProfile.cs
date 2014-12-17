using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DOGPlatform.SVG
{
    class cSVGSectionTrackProfile : cSVGSectionTrack
    {
        public cSVGSectionTrackProfile(int _iTrackWidth)
            : base(_iTrackWidth)
        {

        }
        XmlElement gPatternProfile(double x0, double y0, float fpercentZRL,double height)
        {
            XmlElement gRect = svgDoc.CreateElement("rect");
            gRect.SetAttribute("x", x0.ToString());
            gRect.SetAttribute("y", y0.ToString());
            gRect.SetAttribute("width", (this.iTrackWidth * fpercentZRL).ToString());
            gRect.SetAttribute("height", height.ToString());
            gRect.SetAttribute("style", "stroke-width:0.1");
            gRect.SetAttribute("stroke", "none");
            gRect.SetAttribute("fill", "blue");
            return gRect;
        }
        public XmlElement gTrackProfile(string sJH,List<float> fListTopTVD, List<float> fListBottomTVD, List<float> fListPercentZRL, List<float> fListZRL,float m_KB)
        {
            XmlElement gProfileTrack = svgDoc.CreateElement("g");
            gProfileTrack.SetAttribute("id", sJH + "#TrackProfile");
            for (int i = 0; i < fListTopTVD.Count; i++)
            {
                float _top = fListTopTVD[i];
                float _bottom = fListBottomTVD[i];
                float _fpercentZRL = fListPercentZRL[i];
                float _fZRL = fListZRL[i];
                float x0 = 0;
                float y0 = -m_KB + _top;
                float height = _bottom - _top;
               gProfileTrack.AppendChild(gPatternProfile(x0,y0,_fpercentZRL/100,height)); 

            }

            return gProfileTrack;
        }
    }
}
