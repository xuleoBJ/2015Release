using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cIOinputJSJL 
    {
        
        public static void write2File(string filePath, List<ItemJSJL> listItemJSJL)
        {
            StreamWriter swNew = new StreamWriter(filePath, false, Encoding.UTF8);
            foreach (ItemJSJL item in listItemJSJL) swNew.WriteLine(ItemJSJL.item2string(item));
            swNew.Close();
        }
    
        public static List<ItemJSJL> readJSJL2Struct(string _sJH)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameWellJSJL);
            List<ItemJSJL> listJSJLreturn = new List<ItemJSJL>();
            if (File.Exists(filePath))
            {
                List<string> ltLines = cIOGeoEarthText.getDataLineListStringFromGeoText(filePath);
                foreach (string line in ltLines)
                {
                    if (line.TrimEnd() != "")
                    {
                        ItemJSJL sttJSJL = ItemJSJL.parseLine(line);
                        if (sttJSJL.sJH != null)                      listJSJLreturn.Add(sttJSJL);
                    }
                }

            }

            return listJSJLreturn;
        }

        public static void creatWellGeoHeadFile(string sJH)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameWellJSJL);
            List<string> ltStrHeadColoum = new List<string>();
            ltStrHeadColoum.Add("井号");
            ltStrHeadColoum.Add("顶深");
            ltStrHeadColoum.Add("底深");
            ltStrHeadColoum.Add("砂厚");
            ltStrHeadColoum.Add("有效厚度");
            ltStrHeadColoum.Add("孔隙度");
            ltStrHeadColoum.Add("渗透率");
            ltStrHeadColoum.Add("饱和度");
            ltStrHeadColoum.Add("解释结论");
            string sFirstLine = sJH + "#JSJL";
            cIOGeoEarthText.creatFileGeoHeadText(filePath, sFirstLine, ltStrHeadColoum);
        }
        
        public static void creatWellGeoFile(string _sJH)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameWellJSJL);
            List<ItemJSJL> ltJSJL = readInputFile(_sJH);
            creatWellGeoHeadFile(_sJH);
            List<string> ltStrLine = new List<string>();
            foreach (ItemJSJL _item in ltJSJL.FindAll(p => p.sJH == _sJH))
            {
                ltStrLine.Add(ItemJSJL.item2string(_item));
            }
            cIOGeoEarthText.addDataLines2GeoEarTxt(filePath, ltStrLine); 
            
        }

        static List<ItemJSJL> readInputFile(string _sJH)
        {
            List<ItemJSJL> listJSJLreturn = new List<ItemJSJL>();
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputJSJL);
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
                                ItemJSJL sttJSJL = ItemJSJL.parseLine(line);
                                if (sttJSJL.sJH != null) listJSJLreturn.Add(sttJSJL);
                            }
                        }
                    }
                }
            }
            return listJSJLreturn;

        }

        public static void creatInputFile(string _sJH, List<string> listLinesInput)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputJSJL);
            cIOBase.write2file(listLinesInput, filePath);
            //List<string> ltStrHeadColoum = new List<string>();
            //ltStrHeadColoum.Add("井号");
            //ltStrHeadColoum.Add("顶深");
            //ltStrHeadColoum.Add("底深");
            //ltStrHeadColoum.Add("砂厚");
            //ltStrHeadColoum.Add("有效厚度");
            //ltStrHeadColoum.Add("孔隙度");
            //ltStrHeadColoum.Add("渗透率");
            //ltStrHeadColoum.Add("饱和度");
            //ltStrHeadColoum.Add("解释结论");
           // cIOBase.creatTextFile(ltStrHeadColoum, ltStrLine, filePath);
        }
      
        public static void deleteItemFromJSJL(string _sJH)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameWellJSJL);
            cIOGeoEarthText.deleteLinesByFirstWordFromGeoEarTxt(filePath, _sJH);
        }
        public static void deleteItemFromJSJL(List<string> ltStrJH)
        {
            foreach (string _jh in ltStrJH)
            {
                deleteItemFromJSJL(_jh);
            } 
        }
 
        public static void selectData2FileFromJSJLByJH(string sJH, string filePathWrited)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameWellJSJL);
            cIOBase.write2file(cIOBase.selecFromTextByFirstWord2StringList(filePath, sJH), filePathWrited);

        }
        public static string selectIntervalDataFromJSJLByJHAndDepth(string sJH, float fDS1, float fDS2)
        {
            string sReturn = "";
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameWellJSJL);
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                int iLine = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    iLine++;
                    if (iLine > 5)
                    {
                        string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        float _top = float.Parse(split[1]);
                        float _bottom = float.Parse(split[2]);
                        int _jsjL = int.Parse(split[8]);
                        if (split[0] == sJH && fDS1 <= _top && _bottom <= fDS2)
                        {
                            sReturn += sJH + "\t" + _top.ToString() + '\t' + _bottom.ToString() + "\t" + _jsjL.ToString() + "\r\n";
                        }
                    }
                  
                }
            }

            return sReturn;

        }


        public static string selectJSJL2String(string sJH)
        {
            string sReturn = "";


            foreach (var item in readJSJL2Struct(sJH)) sReturn += ItemJSJL.item2string(item) + "\t";
            
            return sReturn;
        }
        public static void selectSectionDrawData2File(string sJH,string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);

            string sReturn = "";

            foreach (var item in readJSJL2Struct(sJH))
            {
                float _top = item.fDS1;
                float _bottom = item.fDS2;
                int _jsjL = item.iJSJL;
                sReturn += _top.ToString() + '\t' + _bottom.ToString() + "\t" + _jsjL.ToString() + "\t";
            }

            sw.Write(sReturn);
            sw.Close();
        }
       
        public trackJSJLDataList selectJSJL2DataList(string sJH)
        {
            trackJSJLDataList dataList = new trackJSJLDataList();
            dataList.fListDS1 = new List<float>();
            dataList.fListDS2 = new List<float>();
            dataList.iListJSJL = new List<int>();

            foreach (var item in readJSJL2Struct(sJH))
            {
                dataList.fListDS1.Add(item.fDS1);
                dataList.fListDS2.Add(item.fDS2);
                dataList.iListJSJL.Add(item.iJSJL);
            }
            return dataList;
        }
     
        static public void codeReplaceJSJL(string filePath)
        {
            string fileNameTempJSJL = cProjectManager.dirPathTemp + "JSJL.txt";
            StreamWriter swJSJL = new StreamWriter(fileNameTempJSJL, false, Encoding.UTF8);
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
            {
                String line;
                int iLine = 0;

                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    iLine++;
                    string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    split[8] = ItemJSJL.codeReplace(split[8]);
                  
                    swJSJL.WriteLine(string.Join("\t", split));

                }
            }
            swJSJL.Close();
            File.Copy(fileNameTempJSJL, filePath, true);
        }

    }
}
