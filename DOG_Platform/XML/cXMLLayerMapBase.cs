using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DOGPlatform.XML
{
    class cXMLLayerMapBase : cXMLbase
    {

        //config mapLayer xml , 1 section is style, 2 section is data,svg or gdi parse xml in different methond,but only one source. 
        public static void creatLayerMapConfigXML(string xmlConfigLayerMap,int iWidth,int iHeight)
        {
            //cProject.xmlConfigLayerMap = xmlConfigLayerMap;
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", null);
            doc.AppendChild(dec);
            //创建一个根节点（一级）
            XmlElement root = doc.CreateElement("LayerMapConfig");
            doc.AppendChild(root);

            XmlNode node;
            XmlElement eleMent;

            node = doc.CreateElement("Layer");

            eleMent = doc.CreateElement("LayerName");
            eleMent.SetAttribute("LayerName", "LayerName");
            eleMent.SetAttribute("LayerYearAndMonth", DateTime.Now.ToString());
            eleMent.InnerText = "LayerName";
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("xRef");
            eleMent.InnerText =cProjectData.dfMapXrealRefer.ToString();
            node.AppendChild(eleMent);


            eleMent = doc.CreateElement("yRef");
            eleMent.InnerText = cProjectData.dfMapYrealRefer.ToString();
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("dfMapScale");
            eleMent.InnerText = cProjectData.dfMapScale.ToString();
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("width");
            eleMent.InnerText = iWidth.ToString();
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("height");
            eleMent.InnerText = iHeight.ToString(); 
            node.AppendChild(eleMent);

            root.AppendChild(node);


            //定制井位图属性
            node = doc.CreateElement("WellInfor");
            //是否显示
            eleMent = doc.CreateElement("IsShow");
            eleMent.SetAttribute("value", "1");
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("r");
            eleMent.InnerText = "4";
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("rLineWidth");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            root.AppendChild(node);
            //定制井数据
            node = doc.CreateElement("JHText");
            //是否显示
            eleMent = doc.CreateElement("IsShow");
            eleMent.SetAttribute("value", "1");
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("fontColor");
            eleMent.SetAttribute("value", "black");
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("fontType");
            eleMent.SetAttribute("value", "black");
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("fontSize");
            eleMent.InnerText = "5";
            node.AppendChild(eleMent);

            eleMent = doc.CreateElement("DX_Text"); //标注偏移
            eleMent.InnerText = "10";
            node.AppendChild(eleMent);
            //eleMent = doc.CreateElement("DY_Text"); //标注偏移
            //eleMent.SetAttribute("value", "10");
            node.AppendChild(eleMent);

            root.AppendChild(node);



            //定制井点属性图
            node = doc.CreateElement("WellLayerGeologyPropery");
            //是否显示
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("fontCorlor");
            eleMent.InnerText = "black";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("fontSize");
            eleMent.InnerText = "4";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("DX_Text"); //标注整体X偏移距离
            eleMent.InnerText = "5";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("DY_Text"); //标注整体Y偏移距离
            eleMent.InnerText = "5";
            node.AppendChild(eleMent);
            root.AppendChild(node);


            //定制Histogram
            node = doc.CreateElement("StaticData");
            //是否显示
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("IsShowText");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("dfscale");
            eleMent.InnerText = "2";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("textFontSize");
            eleMent.InnerText = "3";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("DX_Text"); //标注偏移
            eleMent.InnerText = "3";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("DY_Text"); //标注偏移
            eleMent.InnerText = "-3";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            //定制测井曲线
            node = doc.CreateElement("LogFace");
            eleMent = doc.CreateElement("fVScale");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("trackWidth");
            eleMent.InnerText = "20";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("lineWidth");
            eleMent.InnerText = "0.5";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("leftLogColor");
            eleMent.InnerText = "blue";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("rightLogColor");
            eleMent.InnerText = "red";
            node.AppendChild(eleMent);

            root.AppendChild(node);

            //定制水平井显示
            node = doc.CreateElement("HorizonalWell");
            //是否显示
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("lineColor");
            eleMent.InnerText = "red";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("lineWidth");
            eleMent.InnerText = "2";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            //定制Fault
            node = doc.CreateElement("FaultLine");
            //是否显示
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("color");
            eleMent.InnerText = "red";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("lineWidth");
            eleMent.InnerText = "2";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            //定制比例尺
            node = doc.CreateElement("ScaleRuler");
            //是否显示
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            eleMent = doc.CreateElement("sacle");
            eleMent.InnerText = "1:500";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            //定制指南针
            node = doc.CreateElement("Compass");
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            //定制边框
            node = doc.CreateElement("Mapframe");
            eleMent = doc.CreateElement("IsShow");
            eleMent.InnerText = "1";
            node.AppendChild(eleMent);
            root.AppendChild(node);

            doc.Save(xmlConfigLayerMap);
            //MessageBox.Show("设置保存。");


        }

        public static void setJHsize(string xmlFilePath, int iSize)
        {
            string nodePath = "/LayerMapConfig/JHText/fontSize";
            updateNodeValue(xmlFilePath, nodePath, iSize);
        }
        public static void setRdiusValueWellCircle(string xmlFilePath, int iR)
        {
            string nodePath = "/LayerMapConfig/WellSymbol/r";
            updateNodeValue(xmlFilePath, nodePath, iR); 
        }
        public static void setLineWidthWellCircle(string xmlFilePath, int iWidth)
        {
            string nodePath="/LayerMapConfig/WellSymbol/rLineWidth";
            updateNodeValue(xmlFilePath, nodePath, iWidth);
        }
       
        public static void setJH_Dxoffset(string xmlFilePath, int iOffset)
        {
            string nodePath="/LayerMapConfig/JHText/DX_Text";
            updateNodeValue(xmlFilePath, nodePath, iOffset);
        }
    
       

        public static void setStaticDataVScale(string filePathxmlLayerMap, float fVScale)
        {
            XDocument xmlLayerMap = XDocument.Load(filePathxmlLayerMap);
            xmlLayerMap.Element("LayerMapConfig").Element("StaticData").Element("dfscale").Value = fVScale.ToString("0.0");
            xmlLayerMap.Save(filePathxmlLayerMap);
        }

        public static void delJHDataNode(string filePathxmlLayerMap)
        {
            string parentNodePath = @"/LayerMapConfig/WellInfor";
            string _tagName = "Well";
            delNodes(filePathxmlLayerMap, parentNodePath, _tagName);
        }
        public static void addJHDataNode2XML(string filePathxmlLayerMap, string strNode)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig/WellInfor");
            if (currentNode != null)
            {
                string _tagName = "Well";
                XmlElement elem = xmlLayerMap.CreateElement(_tagName);
                elem.InnerText = strNode;
                currentNode.AppendChild(elem);
            }
            xmlLayerMap.Save(filePathxmlLayerMap);
        }

        public static void addWellTextNode2XML(string filePathxmlLayerMap, List<string> ltsJH, List<string> ltsValue)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            //查找看是否存在节点，如果存在删除原来的xmlnode段，重新插入新段
            if (currentNode != null)
            {
                XmlElement WellText = xmlLayerMap.CreateElement("WellText");
                for (int i = 0; i < ltsJH.Count; i++)
                {
                    XmlElement elem = xmlLayerMap.CreateElement("WellValue");
                    elem.InnerText = ltsJH[i] + "\t" + ltsValue[i];
                    WellText.AppendChild(elem);
                }
                currentNode.AppendChild(WellText);
            }

            xmlLayerMap.Save(filePathxmlLayerMap);

        }
        
        public static void addLineDataNode2XML(string str) { }

    }
}
