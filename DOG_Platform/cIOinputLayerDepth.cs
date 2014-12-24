﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cIOinputLayerDepth 
    {
       
        public static List<ItemDicLayerDepth> readLayerDepth2Struct(string _sJH)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameWellLayerDepth);
            List<ItemDicLayerDepth> listLayerDepth = new List<ItemDicLayerDepth>();
            if(File.Exists(filePath))
            {
                List<string> ltLines=cIOGeoEarthText.getDataLineListStringFromGeoText(filePath);
                foreach(string line in ltLines)
                {
                  if(line.TrimEnd()!="")
                        {
                            ItemDicLayerDepth sttLayerDepth = new ItemDicLayerDepth();
                             string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                             sttLayerDepth.sJH = split[0];
                            sttLayerDepth.sXCM = split[1];
                            sttLayerDepth.fDS1 = float.Parse(split[2]);
                            sttLayerDepth.fDS2 = float.Parse(split[3]);
                            listLayerDepth.Add(sttLayerDepth);
                        }  
                }
                
            }

            
            return listLayerDepth;
        }

        public static void creatWellGeoHeadFile(string sJH)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameWellLayerDepth);
            List<string> ltStrHeadColoum = new List<string>();
            ltStrHeadColoum.Add("井号");
            ltStrHeadColoum.Add("层名");
            ltStrHeadColoum.Add("顶深(md)m");
            ltStrHeadColoum.Add("底深(md)m");
            string sFirstLine = sJH + "#LayerDepth";
            cIOGeoEarthText.creatFileGeoHeadText(filePath, sFirstLine, ltStrHeadColoum);
        }

        public static void creatInputFile(string _sJH, List<string> listLinesInput)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputLayerDepth);

            cIOBase.write2file(listLinesInput, filePath);

            //List<string> ltStrHead = new List<string>();
            //ltStrHead.Add("井号");
            //ltStrHead.Add("小层名");
            //ltStrHead.Add("测深m")
            //cIOBase.creatTextFile(ltStrHead, ltStrLine,filePath);
        }
        public static void creatWellGeoFile(string _sJH)
        {
             List<ItemLayerDepthInput> ltLayerDepthInput=readInputFile(_sJH);
                creatWellGeoHeadFile(_sJH);
              //增加layer循环
                List<ItemDicLayerDepth> ltLayerDepthWrite = new List<ItemDicLayerDepth>();
                int iCount=cProjectData.ltStrProjectXCM.Count;
                for (int i = 0; i <iCount; i++) 
                {
                    //先按每个井号、层位附上0值
                    ItemDicLayerDepth _item = new ItemDicLayerDepth();
                    _item.sJH = _sJH;
                    _item.sXCM = cProjectData.ltStrProjectXCM[i];
                    _item.fDS1 = 0.0f;
                    _item.fDS2 = 0.0f;
                    //在输入的列表中查找，找到小层名的，把顶深和底深赋值，底深付下个顶深
                    int _iFind=-1;  //设置没找到的标识
                    for (int k = 0; k < ltLayerDepthInput.Count; k++)
                    {
                        if (_item.sXCM == ltLayerDepthInput[k].sXCM)
                        {
                            _item.fDS1 = ltLayerDepthInput[k].fTop;
                            if (k < ltLayerDepthInput.Count - 1) _item.fDS2 = ltLayerDepthInput[k + 1].fTop;
                            if (k == ltLayerDepthInput.Count - 1) _item.fDS2 = _item.fDS1;//如果找到了 最后一行的处理，
                            _iFind=1; //找到了
                            break;
                        }
                    }
                    if(_iFind<0)//如何没找到
                    {
                      if(i==0){_item.fDS1=ltLayerDepthInput[0].fTop;_item.fDS2=_item.fDS1;} 
                      if(i>0){_item.fDS1=ltLayerDepthWrite[ltLayerDepthWrite.Count-1].fDS1;_item.fDS2=_item.fDS1;}
                    }
                  ltLayerDepthWrite.Add(_item);
                }

                string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameWellLayerDepth);
               
                List<string> ltStrLine = new List<string>();
                foreach (ItemDicLayerDepth _item in ltLayerDepthWrite)
                {
                    ltStrLine.Add(ItemDicLayerDepth.item2string(_item));
                }
                cIOGeoEarthText.addDataLines2GeoEarTxt(filePath, ltStrLine);
            
        }

       public static List<ItemLayerDepthInput > readInputFile(string _sJH) 
        {
            List<ItemLayerDepthInput> returnItem = new List<ItemLayerDepthInput>();
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputLayerDepth);
            if (File.Exists(filePath)) { 
            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                String line;
                int iLine = 0;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    iLine++;
                    if (iLine > 0)
                    {
                        ItemLayerDepthInput _item = new ItemLayerDepthInput();
                        string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        _item.sJH = split[0];
                        _item.sXCM = split[1];
                        _item.fTop = 0.0f;
                        float.TryParse(split[2],out _item.fTop);
                        returnItem.Add(_item); 
                    }

                }
            }
            }
            return returnItem;
        
        }


       public static string getXCMByJHAndDepthInterval(string _sJH, float fTop,float fBase)
       {
           List<string> listXCM = new List<string>();
           List<ItemDicLayerDepth> listLayerDepth = cIOinputLayerDepth.readLayerDepth2Struct(_sJH);
           List<ItemLayerDepthInput> listLayerDepthInput = cIOinputLayerDepth.readInputFile(_sJH);
           List<string> layerInput = listLayerDepthInput.Select(p => p.sXCM).ToList();
           string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameWellJSJL);
           List<string> ltStrLine = new List<string>();
          
           string _topXCM = "";
           string _botXCM = "";
           int mark = 0;

           for (int i = 0; i < listLayerDepth.Count; i++)
           {
               if (fTop >= listLayerDepth[i].fDS1 && fTop <= listLayerDepth[i].fDS2)
               { _topXCM = listLayerDepth[i].sXCM; mark = 1; }
               if (fBase >= listLayerDepth[i].fDS1 && fBase <= listLayerDepth[i].fDS2)
               { _botXCM = listLayerDepth[i].sXCM; mark = 2; break; }
           }
           string _xcm = "";
           if (mark == 0) _xcm = "";
           else if (mark == 2)
           {
               if (_topXCM == _botXCM) _xcm = _topXCM;
               else if (_topXCM == "" || _botXCM == "") _xcm = _topXCM + "_" + _botXCM;
               else
               {
                   int _iStart = layerInput.IndexOf(_topXCM);
                   if (_iStart < 0) { _iStart = 0; _xcm = ""; }
                   else
                   {
                       int _count = layerInput.IndexOf(_botXCM) - _iStart + 1;
                       _xcm = string.Join("+", layerInput.GetRange(_iStart, _count).ToArray());
                   }
               }
           }
           else _xcm = _topXCM + "_" + _botXCM; //这个等于1的问题需要分析

           return _xcm; 

       }
        public static void deleteItemFromLayerDepth(string sJH)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, sJH, cProjectManager.fileNameWellLayerDepth);
            cIOGeoEarthText.deleteLinesByFirstWordFromGeoEarTxt(filePath, sJH);
        }

        public static void deleteItemFromLayerDepth(List<string> ltStrJH)
        {
            foreach (string _jh in ltStrJH) 
            {
                deleteItemFromLayerDepth(_jh);
            }
        }
        // 根据井号和小层名从LayerDepth中得到顶深
        public float selectTopDepthMDByJHAndXCM(string sJH, string sXCM)
        {
            float fReturnTopDepthMD = -999;
            ItemDicLayerDepth returnItem = selectItemsByJHAndXCM(sJH, sXCM);
            if (returnItem.sJH!= null) 
            {
                fReturnTopDepthMD = returnItem.fDS1;
            }
            return fReturnTopDepthMD;
        }

        public  ItemDicLayerDepth selectItemsByJHAndXCM(string sJH, string sXCM)
        {
            return readLayerDepth2Struct(sJH).Find(p => p.sXCM == sXCM);
        }


        public List<float> selectDepthListFromLayerDepthByJHAndXCMList(string sJH, List<string> ltStrXCM)
        {
            List<float> fListTopDepths = (from item in readLayerDepth2Struct(sJH)
                                          where ltStrXCM.Contains(item.sXCM)
                                          select item.fDS1).ToList<float>();
            return fListTopDepths;

        }


        public string selectIntervalDataFromLayerDepthByJHAndDepth(string sJH, float fListDS1, float fListDS2)
        {
            string sReturn = "";
            List<ItemDicLayerDepth> layerDepthItems = readLayerDepth2Struct(sJH);

            for (int i = 0; i < layerDepthItems.Count; i++)
            {
                if (layerDepthItems[i].sJH  == sJH)
                {
                    float _top = layerDepthItems[i].fDS1;
                    float _bottom = layerDepthItems[i].fDS2;
                    string _xcm = layerDepthItems[i].sXCM;
                    if (fListDS1 <= _top && _bottom <= fListDS2)
                    {
                        sReturn += sJH + '\t' + _xcm + "\t" + _top.ToString() + '\t' + _bottom.ToString() + "\r\n";
                    }
                }

            }

            return sReturn;

        }




        public void selectSectionDrawData2File(string sJH,string filePath)
        {
              StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
              string sReturn = "";

              foreach (var item in readLayerDepth2Struct(sJH))
              {
                  float _top = item.fDS1;
                  float _bottom = item.fDS2;
                  string _xcm = item.sXCM;
                  sReturn += _top.ToString() + '\t' + _bottom.ToString() + "\t" + _xcm + "\t";
              }
              sw.Write(sReturn);
              sw.Close();
        }

        public string selectLayerDepth2String(string sJH)
        {
            string sReturn = "";
            foreach (var item in readLayerDepth2Struct(sJH)) sReturn += ItemDicLayerDepth.item2string(item) + "\t";
            return sReturn;

        }

       
    }
}
