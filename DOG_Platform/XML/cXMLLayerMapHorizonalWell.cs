using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace DOGPlatform.XML
{
    class cXMLLayerMapHorizonalWell:cXMLbase 
    { 
        public static void delHorizonalWellIntervalNode(string xmlLayerMap)
        {
            string parentNodePath=@"/LayerMapConfig/HorizonalWell";
            string _tagName = "WellIntervel";
            delNodes(xmlLayerMap,parentNodePath,_tagName);
        }

        public static XmlNodeList getHorizonalWellIntervalNodeList(string filePathxmlLayerMap)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            return   xmlLayerMap.SelectNodes("/LayerMapConfig/HorizonalWell/WellIntervel");
        }
        public static void addHorizonalWellIntervalNode2XML(string filePathxmlLayerMap, string strNode)
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(filePathxmlLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig/HorizonalWell");
            //查找看是否存在水平井段，如果存在删除原来的xmlnode段，重新插入新段
            if (currentNode != null)
            {
                //XmlNode _delData = xmlLayerMap.SelectSingleNode("/LayerMapConfig/WellSymbol/Data");
                //if (_delData != null) _delData.ParentNode.RemoveChild(_delData);
                XmlElement elem = xmlLayerMap.CreateElement("WellIntervel");
                elem.InnerText = strNode;
                currentNode.AppendChild(elem);
            }

            xmlLayerMap.Save(filePathxmlLayerMap);
        }
        public static void setLineWidthHorzionalInterval(string _xmlFilePath, float _fLineWidth)
        {
            string _nodePath = "/LayerMapConfig/HorizonalWell/lineWidth";
            updateNodeValue(_xmlFilePath, _nodePath, _fLineWidth);
        }
        public static void setColorHorionalInterval(string _xmlFilePath, string _sColorName)
        {
            string _nodePath = "/LayerMapConfig/HorizonalWell/lineColor";
            updateNodeValue(_xmlFilePath, _nodePath, _sColorName);
        }
    }
}
