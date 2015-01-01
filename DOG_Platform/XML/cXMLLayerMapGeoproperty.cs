using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DOGPlatform.XML
{
    class cXMLLayerMapGeoproperty:cXMLbase
    {
        public static void addWellProperty2XML(string filePathxmlLayerMap, List<string> ltsJH, List<string> ltsValue, TypeShowValue TypeShowValue)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            //查找看是否存在节点，如果存在删除原来的xmlnode段，重新插入新段
            if (currentNode != null)
            {
                XmlElement WellText = xmlLayerMap.CreateElement("WellProperty");


                XmlElement eleMent = xmlLayerMap.CreateElement("fontSize");
                eleMent.InnerText = "4";
                WellText.AppendChild(eleMent);

                eleMent = xmlLayerMap.CreateElement("TypeShowValue");
                eleMent.InnerText = TypeShowValue.ToString();
                WellText.AppendChild(eleMent);
                eleMent = xmlLayerMap.CreateElement("DX_Text"); //标注偏移
                eleMent.InnerText = "10";
                WellText.AppendChild(eleMent);
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

        public static void delWellPropertyNode(string filePathxmlLayerMap)
        {
            string parentNodePath = @"/LayerMapConfig";
            string _tagName = "WellGeologyProperty";
            delNodes(filePathxmlLayerMap, parentNodePath, _tagName);
        }
        public static void addWellProperty2XML(string filePathxmlLayerMap, List<string> ltsValue)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig");
            //查找看是否存在节点，如果存在删除原来的xmlnode段，重新插入新段
            ;
            if (currentNode != null)
            {
                XmlElement WellText = xmlLayerMap.CreateElement("WellGeologyProperty");
                WellText.SetAttribute("id", "idGeoproperty");

                XmlElement eleMent = xmlLayerMap.CreateElement("fontSize");
                eleMent.InnerText = "4";
                WellText.AppendChild(eleMent);
                eleMent = xmlLayerMap.CreateElement("DX_Text"); //标注偏移
                WellText.AppendChild(eleMent);
                eleMent.InnerText = "10";
                WellText.AppendChild(eleMent);
                for (int i = 0; i < ltsValue.Count; i++)
                {
                    XmlElement elem = xmlLayerMap.CreateElement("WellValue");
                    elem.InnerText = ltsValue[i];
                    WellText.AppendChild(elem);
                }
                currentNode.AppendChild(WellText);
            }

            xmlLayerMap.Save(filePathxmlLayerMap);

        }

        public static void setLayerGeoglogyProperty_Dxoffset(string filePathxmlLayerMap, int iOffset)
        {
            XDocument xmlLayerMap = XDocument.Load(filePathxmlLayerMap);
            xmlLayerMap.Element("LayerMapConfig").Element("WellLayerGeologyPropery").Element("DX_Text").Value = iOffset.ToString("0");
            xmlLayerMap.Save(filePathxmlLayerMap);
        }
        public static void setLayerGeoglogyProperty_Dyoffset(string filePathxmlLayerMap, int iOffset)
        {
            XDocument xmlLayerMap = XDocument.Load(filePathxmlLayerMap);
            xmlLayerMap.Element("LayerMapConfig").Element("WellLayerGeologyPropery").Element("DY_Text").Value = iOffset.ToString("0");
            xmlLayerMap.Save(filePathxmlLayerMap);
        }
        public static void setLayerGeoglogyPropertyTextsize(string xmlFilePath, int iSize)
        {
            XDocument xmlLayerMap = XDocument.Load(xmlFilePath);
            xmlLayerMap.Element("LayerMapConfig").Element("WellLayerGeologyPropery").Element("fontSize").Value = iSize.ToString();
            xmlLayerMap.Save(xmlFilePath);
        }
      
    }
}
