using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cIOinputWellPerforation
    {
     
        public static void creatWellGeoHeadFile(string sJH)
        {
            string inputFilepath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameWellPerforation);
            List<string> ltStrHeadColoum = new List<string>();
            ltStrHeadColoum.Add("井号");
            ltStrHeadColoum.Add("小层名");
            ltStrHeadColoum.Add("射孔年月");
            ltStrHeadColoum.Add("堵孔年月");
            ltStrHeadColoum.Add("射开");
             string sFirstLine =DateTime.Today.ToString()+" "+ sJH + "#射孔数据";
             cIOGeoEarthText.creatFileGeoHeadText(inputFilepath, sFirstLine, ltStrHeadColoum);
        }
        public static void creatInputFile(string _sJH, List<string> listLinesInput)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellPerforation);
            cIOBase.write2file(listLinesInput, filePath);
        }

        static List<ItemInputPerforate> readInputFile(string _sJH)
        {
            List<ItemInputPerforate> listReturn = new List<ItemInputPerforate>();
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellPerforation);
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    String line;
                    int iLine = 0;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        iLine++;
                        if (iLine > 0)
                        {
                            if (line.TrimEnd() != "")
                            {
                                ItemInputPerforate sttJSJL = ItemInputPerforate.parseLine(line);
                                if (sttJSJL.sJH != null) listReturn.Add(sttJSJL);
                            }
                        }
                    }
                }
            }
            return listReturn;

        }
        public static void creatWellGeoFile(string _sJH)
        {
           
            creatWellGeoHeadFile(_sJH);

            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameWellPerforation);
            List<ItemInputPerforate> listPeforationInput = readInputFile(_sJH);
            List<string> ltStrLine = new List<string>();
            //需要填上 每口井 每个小层的射孔情况
            //按井和小层关键字输出，由于处在单井文件夹下，所以 每个文件有井和小层那么多行
            //每个小层应该 射孔井段在小层内的fds1和fds2
            //但是 由于多数是合层射孔，就需要劈分射孔数据到各个小层了，所以存在交叉问题
            //要分不同交叉情况处理
            //还有可能全层找不到射孔数据，只能把射孔段顶底深付成成段顶深，射孔时间付成190001
            foreach (ItemDicLayerDepth layerItem in cIOinputLayerDepth.readLayerDepth2Struct(_sJH))
            {
                ItemDicPerforation itemDic = new ItemDicPerforation();
                itemDic.sJH = layerItem.sJH;
                itemDic.sXCM = layerItem.sXCM;
                if (layerItem.fDS1 != layerItem.fDS2) //顶底深一样的是缺失层
                {
                    bool bFind = false;
                    float fSKHD = 0.0f;
                    //先把射孔设计付为极大，然后找到后缩小
                    //目前是 同一层段 不同时期射孔的 只能归到最初时间
                    itemDic.YMstart = "209912";
                    foreach (ItemInputPerforate itemporationInput in listPeforationInput)
                    {  
                       if(!(itemporationInput.fDS2>=layerItem.fDS1)||(itemporationInput.fDS1<=layerItem.fDS2)) //判断交叉
                        {
                            if (int.Parse(itemporationInput.sYM) <= int.Parse(itemDic.YMstart)) itemDic.YMstart = itemporationInput.sYM;
                            float _fDS1=0.0f;
                            float _fDS2 = 0.0f ;
                            //如果 层段和射孔段交叉，顶深 谁大写谁 底深 谁小写谁，目的是卡在层段内
                            if (layerItem.fDS1 >= itemporationInput.fDS1) _fDS1 = layerItem.fDS1;
                            else _fDS1 = itemporationInput.fDS1;
                            if (layerItem.fDS2  >= itemporationInput.fDS2) _fDS2 = itemporationInput.fDS2;
                            else _fDS2 = layerItem.fDS2;
                            fSKHD = fSKHD + _fDS2 - _fDS1;
                            bFind = true;
                        }
                    }
                    if (bFind == false)
                    {
                        itemDic.fSKHD = 0;
                        itemDic.YMend = "190001";
                    }
                    else { itemDic.YMend = "209912"; itemDic.fSKHD = float.Parse(fSKHD.ToString("0.0"));}

                }
                else
                {
                    itemDic.fSKHD = 0;
                    itemDic.YMstart = "190001";
                    itemDic.YMend = "190001";
                }

                ltStrLine.Add(ItemDicPerforation.item2string(itemDic));
            } 
            cIOGeoEarthText.addDataLines2GeoEarTxt(filePath, ltStrLine);
        }

        //读取输入射孔数据
        public static List<ItemInputPerforate> readInputPerforation2Struct(string _sJH)
        {
           List<ItemInputPerforate> listInputPeforation = new List<ItemInputPerforate>();
            int iLineIndex = 0;
            string inputFilePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellPerforation);
           if(File.Exists(inputFilePath))
           {  
                using (StreamReader sr = new StreamReader(inputFilePath))
                {
                    String line;
                   
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        iLineIndex++;
                        if (iLineIndex >= 1)
                        {
                            ItemInputPerforate item=ItemInputPerforate.parseLine(line);
                            listInputPeforation.Add(item);
                        }

                    }
                }
            }
            return listInputPeforation;
        }
       

        public void selectSectionDrawData2File(string sJH, string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
            string sReturn = "";
            foreach (var item in readInputPerforation2Struct(sJH))
                sReturn +=ItemInputPerforate.item2string(item) + "\t";
            sw.Write(sReturn);
            sw.Close();
        }

        public static string selectPerforation2String(string sJH)
        {
            string sReturn = "";
            foreach (var item in readInputPerforation2Struct(sJH))
            {
                string _YYYYMM = item.sYM;
                float _top = item.fDS1;
                float _bottom = item.fDS2;
                sReturn += _YYYYMM + "\t" + _top.ToString() + '\t' + _bottom.ToString() + "\t";
            }
            return sReturn;

        }

   
    }
}
