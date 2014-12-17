using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cIOinputPerforation
    {
     
       
        public static void creatWellGeoHeadFile(string sJH)
        {
            string inputFilepath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameWellPerforation);
            List<string> ltStrHeadColoum = new List<string>();
            ltStrHeadColoum.Add("井号");
            ltStrHeadColoum.Add("小层名");
            ltStrHeadColoum.Add("射孔年月");
            ltStrHeadColoum.Add("堵孔年月");
            ltStrHeadColoum.Add("射孔顶深");
            ltStrHeadColoum.Add("射孔底深");
             string sFirstLine =DateTime.Today.ToString()+" "+ sJH + "#JSJL";
             cIOGeoEarthText.creatFileGeoHeadText(inputFilepath, sFirstLine, ltStrHeadColoum);
        }
        public static void creatInputFile(string _sJH, List<string> listLinesInput)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellPerforation);
            cIOBase.write2file(listLinesInput, filePath);
        }

        static List<ItemPerforationInput> readInputFile(string _sJH)
        {
            List<ItemPerforationInput> listReturn = new List<ItemPerforationInput>();
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
                                ItemPerforationInput sttJSJL = ItemPerforationInput.parseLine(line);
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
            List<ItemPerforationInput> listinput = readInputFile(_sJH);
            List<string> ltStrLine = new List<string>();
            foreach (ItemPerforationInput itemInput in listinput)
            {
                List<ItemLayerDepth> listLayerDepth = cIOinputLayerDepth.readLayerDepth2Struct(_sJH);
                foreach (ItemLayerDepth layItem in listLayerDepth)
                {
                    ItemPerforationDic itemDic = new ItemPerforationDic();
                    itemDic.sJH = itemInput.sJH;
                    itemDic.YMstart = itemInput.sYM;
                    itemDic.sXCM = layItem.sXCM;
                    if (layItem.fDS1 != layItem.fDS2)
                    {
                        itemDic.fDS1 = layItem.fDS1;
                        itemDic.fDS2 = layItem.fDS2;
                        itemDic.YMend = "209912";
                    }
                    else 
                    {
                        itemDic.fDS1 = layItem.fDS1;
                        itemDic.fDS2 = layItem.fDS2;
                        itemDic.YMend = "190001"; 
                    }
                    ltStrLine.Add(ItemPerforationDic.item2string(itemDic));
                }
            }
            cIOGeoEarthText.addDataLines2GeoEarTxt(filePath, ltStrLine);

        }

        //读取输入射孔数据
        public static List<ItemPerforationInput> readInputPerforation2Struct(string _sJH)
        {
           List<ItemPerforationInput> listInputPeforation = new List<ItemPerforationInput>();
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
                            ItemPerforationInput item=ItemPerforationInput.parseLine(line);
                            listInputPeforation.Add(item);
                        }

                    }
                }
            }
            return listInputPeforation;
        }
       

        public void selectSectionDraData2File(string sJH, string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
            string sReturn = "";
            foreach (var item in readInputPerforation2Struct(sJH))
                sReturn +=ItemPerforationInput.item2string(item) + "\t";
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
