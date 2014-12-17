using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace DOGPlatform
{
    class cIOProject : cIOBase
    {
   
        public static List<ItemLayerSplitFactor> readDicLayerSplitFactor2Struct()
        {
            List<ItemLayerSplitFactor> listLayerSplitFactorDic = new List<ItemLayerSplitFactor>();

            int iLineIndex = 0;
            try
            {
                using (StreamReader sr = new StreamReader(cProjectManager.filePathLayerSplitFactorDic))
                {
                    String line;
                    ItemLayerSplitFactor sttLayerSplitFactor = new ItemLayerSplitFactor();
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        iLineIndex++;
                        if (iLineIndex > 1)
                        {
                            string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            sttLayerSplitFactor.sJH = split[0];
                            sttLayerSplitFactor.sXCM = split[1];
                            sttLayerSplitFactor.YYYYMM = split[2];
                            sttLayerSplitFactor.fLayerSplitFactor = float.Parse(split[3]);
                            listLayerSplitFactorDic.Add(sttLayerSplitFactor);
                        }

                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return listLayerSplitFactorDic;

        }
        public static List<ItemWellProductionDic> readProductionWellDic2Struct(string filePath)
        {
            List<ItemWellProductionDic> listWellProductionDicItem = new List<ItemWellProductionDic>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    String line;
                    int iLine = 0;
                    ItemWellProductionDic sttWellProductionDicItem = new ItemWellProductionDic();
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        iLine++;
                        string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (iLine > 1)
                        {
                            sttWellProductionDicItem.sJH = split[0];
                            sttWellProductionDicItem.sYM = split[1];
                            sttWellProductionDicItem.sXCM = split[2];
                            sttWellProductionDicItem.fSCTS = float.Parse(split[3]);
                            sttWellProductionDicItem.fYC_liquid = float.Parse(split[4]);
                            sttWellProductionDicItem.fYC_oil = float.Parse(split[5]);
                            sttWellProductionDicItem.fYC_water = float.Parse(split[6]);
                            sttWellProductionDicItem.fYC_gas = float.Parse(split[7]);
                            sttWellProductionDicItem.fSum_liquid = float.Parse(split[8]);
                            sttWellProductionDicItem.fSum_oil = float.Parse(split[9]);
                            sttWellProductionDicItem.fSum_water = float.Parse(split[10]);
                            sttWellProductionDicItem.fSum_gas = float.Parse(split[11]);
                            sttWellProductionDicItem.fRC_liquid = float.Parse(split[12]);
                            sttWellProductionDicItem.fRC_oil = float.Parse(split[13]);
                            sttWellProductionDicItem.fRC_water = float.Parse(split[14]);
                            sttWellProductionDicItem.fRC_gas = float.Parse(split[15]);
                            sttWellProductionDicItem.fWaterCut = float.Parse(split[16]);
                            sttWellProductionDicItem.fGOR = float.Parse(split[17]);
                            sttWellProductionDicItem.fTY = float.Parse(split[18]);
                            sttWellProductionDicItem.fYY = float.Parse(split[19]);
                            sttWellProductionDicItem.fJY = float.Parse(split[20]);
                            sttWellProductionDicItem.fLY = float.Parse(split[21]);
                            listWellProductionDicItem.Add(sttWellProductionDicItem);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return listWellProductionDicItem;

        }
        public static List<ItemWellInjectionDic> readInjectionWellDic2Struct(string filePath)
        {
            List<ItemWellInjectionDic> listWellInjectionItem = new List<ItemWellInjectionDic>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    String line;
                    int iLine = 0;
                    ItemWellInjectionDic sttWellInjectionItem = new ItemWellInjectionDic();
                    while ((line = sr.ReadLine()) != null)  //delete the line whose legth is 0
                    {
                        iLine++;
                        string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (iLine > 1)
                        {
                            sttWellInjectionItem.sJH = split[0];
                            sttWellInjectionItem.sYM = split[1];
                            sttWellInjectionItem.sXCM = split[2];
                            sttWellInjectionItem.fZSTS = float.Parse(split[3]);
                            sttWellInjectionItem.fRZSL = float.Parse(split[4]);
                            sttWellInjectionItem.fYZSL = float.Parse(split[5]);
                            sttWellInjectionItem.fLZSL = float.Parse(split[6]);
                            sttWellInjectionItem.fLY = float.Parse(split[7]);
                            sttWellInjectionItem.fJY = float.Parse(split[8]);
                            sttWellInjectionItem.fYY = float.Parse(split[9]);
                            sttWellInjectionItem.fTY = float.Parse(split[10]);
                            sttWellInjectionItem.fPY = float.Parse(split[11]);

                            listWellInjectionItem.Add(sttWellInjectionItem);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return listWellInjectionItem;

        }

        public void readNavigation2List()
        {
            string[] split;
            if (File.Exists(cProjectManager.filePathWellNavigation))
            {
                using (StreamReader sr = new StreamReader(cProjectManager.filePathWellNavigation))
                {
                    String line;
                    int iLine = 0;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        iLine++;
                        split = line.Trim().Split(new char[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (iLine > 1)//第一行存的是比例尺
                        {
                            //ltStrWellName_view.Add(split[0]);
                            //iListX_view.Add(int.Parse(split[1]));
                            //iListY_view.Add(int.Parse(split[2]));
                        }
                        else
                        {
                            cProjectData.fMapScale = float.Parse(split[1]);
                            cProjectData.dfMapXrealRefer = double.Parse(split[3]);
                            cProjectData.dfMapYrealRefer = double.Parse(split[5]);
                        }

                    }
                }
            }
        }
    }
}
