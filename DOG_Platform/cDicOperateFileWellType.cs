using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cDicOperateFileWellType : cCalBase 
    {
        //读入静态数据时调用
        public void generateWellTypeDic() 
        {
            List<ItemWellHead> listWellHead = cIOinputWellHead.readWellHead2Struct();
            List<ItemWellProduction> listWellProductInput = new List<ItemWellProduction>(); 
            List<ItemWellInjection> listWellInjectInput = new List<ItemWellInjection> ();
            //List<cBase.WellProductionInputItem> listWellProductInput=readInputProductionWell2Struct();
            //List<ItemWellInjection> listWellInjectInput = readInputInjectionWell2Struct();
          
            StreamWriter sw = new StreamWriter(cProjectManager.filePathWellTypeDic, false, Encoding.UTF8);

            List<string> ltStrHeadColoum = new List<string>();  
            ltStrHeadColoum.Add("WellName");
            ltStrHeadColoum.Add("YYYYMM");
            ltStrHeadColoum.Add("WellType");
            sw.WriteLine(string.Join("\t", ltStrHeadColoum.ToArray()));
            if (cProjectData.ltStrProjectJH.Count > 0 &&  cProjectData.ltStrProjectYM.Count>0 )
            {
                for (int i = 0; i < cProjectData.ltStrProjectJH.Count; i++)
                {
                 
                        string sCurrentJH = cProjectData.ltStrProjectJH[i].ToString();
                       
                        foreach (string sYMItem in cProjectData.ltStrProjectYM)
                        {
                            List<string> ltStrWrited = new List<string>();
                            ltStrWrited.Add(sCurrentJH);
                            ltStrWrited.Add(sYMItem);
                            //需要修改！！！！！！！！！
                           // int _index = wellHeads.ltStrWellName.IndexOf(sCurrentJH);
                            int iWellType = 3;
                            bool bFind = false;
                            for (int k = 0; k < listWellProductInput.Count; k++) 
                            {
                                if (listWellProductInput[k].sJH == sCurrentJH && listWellProductInput[k].sYM == sYMItem) 
                                {
                                    iWellType = 3;
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind == false) 
                            {
                                for (int k= 0; k < listWellInjectInput.Count; k++)
                                {
                                    if (listWellInjectInput[k].sJH == sCurrentJH && listWellInjectInput[k].sYM == sYMItem)
                                    {
                                        iWellType = 15;
                                        break;
                                    }
                                }
                            }


                            ltStrWrited.Add(iWellType.ToString());
                            sw.WriteLine(string.Join("\t", ltStrWrited.ToArray()));
                        }
                }
            }
            sw.Close();
            //MessageBox.Show("井型拼写计算完毕");
        }

        //读入动态数据时调用,根据油水井生产月报更新井型字典
        public void upDateWellTypeDic() 
        { 
        
        }
    }
}
