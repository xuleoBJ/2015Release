using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cIOinputInjectionProfile
    {
        /// <summary>
        /// 
        /// </summary>

        public static List<ItemInjectionProfile> readInjectionProfile2Struct(string sJH)
        {
            List<ItemInjectionProfile> listItems = new List<ItemInjectionProfile>();
            string inputFilepath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameWellProfile);
            if (File.Exists(inputFilepath))
            {
                //using (StreamReader sr = new StreamReader(cProjectManager.fileNameInputWellProfile, System.Text.Encoding.UTF8))
                //{
                //    String line;
                //    int _indexLine = 0;
                //    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                //    {
                //        _indexLine++;
                //        ItemInjectionProfile item = ItemInjectionProfile.parseLine(line);
                //        listItems.Add(item);
                //    }
                //}

            }
 
            return listItems;
        }

        public static void creatFile(string filePath)
        {
            List<string> ltStrHeadColoum = new List<string>();
            ltStrHeadColoum.Add("井号");
            ltStrHeadColoum.Add("年月YYYYMM");
            ltStrHeadColoum.Add("吸水段顶深m");
            ltStrHeadColoum.Add("吸水段底深m");
            ltStrHeadColoum.Add("绝对吸水量(方)");
            string sFirstLine = DateTime.Today.ToString()+ "#InjectProFile";
            cIOGeoEarthText.creatFileGeoHeadText(filePath, sFirstLine, ltStrHeadColoum);
        }

        public static void creatInputFile(string _sJH, List<string> listLinesInput)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellProfile);
            cIOBase.write2file(listLinesInput, filePath);
        }

        struct itemInputProfile 
        {
            public string sJH;
            public string sYM;
            public float fDS1;
            public float fDS2;
            public float fZRL; 
        }


        static List<itemInputProfile> readInputFile(string _sJH) 
        {
            List<itemInputProfile> listInputItem = new List<itemInputProfile>();

            string inputFilePath =
                      Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellProfile);
            if (File.Exists(inputFilePath))
            {
                using (StreamReader sr = new StreamReader(inputFilePath))
                {
                    String line;
                    int _indexLine = 0;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        _indexLine++;
                        itemInputProfile currentItem = new itemInputProfile();
                        string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (_indexLine >= 1)
                        {
                            currentItem.sJH = split[0];
                            currentItem.sYM = split[1];
                            currentItem.fDS1 = 0.0f;
                            float.TryParse(split[2], out currentItem.fDS1);
                            currentItem.fDS2 = 0.0f;
                            float.TryParse(split[3], out currentItem.fDS2);
                            currentItem.fZRL = 0.0f;
                            float.TryParse(split[4], out currentItem.fZRL);
                            listInputItem.Add(currentItem);
                        }
                    }
                }
            }
            return listInputItem;
        }
        public static void creatWellGeoFile(string _sJH)
        {
            creatWellGeoHeadFile(_sJH);
            List<ItemInjectionProfile> listInjectionProfile = new List<ItemInjectionProfile>();
            List<itemInputProfile> listInputProfile = readInputFile(_sJH);
            foreach (string _YM in listInputProfile.Select(p=>p.sYM).Distinct()) 
            {
                List<itemInputProfile> listInputCurrentYM = listInputProfile.FindAll(p => p.sYM == _YM);
                float fZZRL = listInputCurrentYM.Sum(p => p.fZRL); ; //当前年月总注入量
                foreach (itemInputProfile _item in listInputCurrentYM) 
                { 
                    ItemInjectionProfile itemOut = new ItemInjectionProfile();
                    itemOut.sJH = _item.sJH;
                    itemOut.sYM = _item.sYM;
                    itemOut.fDS1 = _item.fDS1;
                    itemOut.fDS2 = _item.fDS2;
                    itemOut.fZRL = _item.fZRL;
                    itemOut.fPercentZR = (_item.fZRL / fZZRL)*100;
                    itemOut.fXSHD = _item.fDS2 - _item.fDS1;
                    itemOut.FXSQD = _item.fZRL / itemOut.fXSHD;
                    listInjectionProfile.Add(itemOut);
                }

            }
            List<string> ltStrLine = new List<string>();
            foreach (ItemInjectionProfile _item in listInjectionProfile)
            {
                ltStrLine.Add(ItemInjectionProfile.item2string(_item));
            }
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameWellProfile);
            cIOGeoEarthText.addDataLines2GeoEarTxt(filePath, ltStrLine); 
        }
        public static void creatWellGeoHeadFile(string sJH)
        {
            string inputFilepath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameWellProfile);
            List<string> ltStrHeadColoum = new List<string>();
            ltStrHeadColoum.Add("井号");
            ltStrHeadColoum.Add("年月");
            ltStrHeadColoum.Add("顶深m");
            ltStrHeadColoum.Add("底深m");
            ltStrHeadColoum.Add("绝对吸水量(方)");
            ltStrHeadColoum.Add("相对注入%");
            ltStrHeadColoum.Add("吸水厚度");
            ltStrHeadColoum.Add("吸水强度");
            string sFirstLine = DateTime.Today.ToString()+"$WellProfile";
            cIOGeoEarthText.creatFileGeoHeadText(inputFilepath, sFirstLine, ltStrHeadColoum);
        }

        public void selectSection2File(string sJH, string filePath)
        {
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
            string sReturn = ""; 
              foreach (var item in readInjectionProfile2Struct(sJH))
             {
                 sReturn = sReturn +" "+ ItemInjectionProfile.item2string(item);
             
             }

              sw.Write(sReturn);
            sw.Close();
        }

      
    }
}
