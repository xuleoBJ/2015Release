using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cIODicWellType 
    {
        public struct itemWellType
        {
          public string sJH;
          public int iType;
          public string YMstart;
          public string YMend;

          public static string item2string(itemWellType item)
          {
              List<string> ltStrWrited = new List<string>();
              ltStrWrited.Add(item.sJH);
              ltStrWrited.Add(item.iType.ToString());
              ltStrWrited.Add(item.YMstart.ToString());
              ltStrWrited.Add(item.YMend.ToString());
              return string.Join("\t", ltStrWrited.ToArray());
          }

          public static itemWellType parseLine(string line)
          {
              string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
              itemWellType item = new itemWellType();
              if (split.Length >= 4)
              {
                  item.sJH = split[0];
                  item.iType = 0;
                  int.TryParse(split[1], out item.iType);
                  item.YMstart = split[2];
                  item.YMend = split[3];
              }
              return item;
          } 
        }
     
        

        //读入动态数据时调用,根据油水井生产月报更新井型字典
        public static void updateWellTypeDic() 
        {
            List<itemWellType> listItemWellType = new List<itemWellType>();
            //如果有的话，读入生成文件 井号 起始时间 结束时间 井别
            foreach(string sJH in cProjectData.ltStrProjectJH)
            {
                List<ItemInputWellProduct> currentProductItem = cIOinputWellProduct.readInput2Struct(sJH);
                List<ItemInputWellInject> currentInjectItem = cIOInputWellInject.readInput2Struct(sJH);
            //情况1 有油井文件没有水井文件或者水井文件内为0
                if (currentProductItem.Count > 0 && currentInjectItem.Count == 0) 
                {
                    itemWellType newItem = new itemWellType();
                    newItem.sJH = currentProductItem[0].sJH;
                    newItem.iType = (int)TypeWell.Oil;
                    newItem.YMstart = currentProductItem[0].sYM;
                    newItem.YMend = "209912";
                    listItemWellType.Add(newItem);
                }

            //情况2 有水井文件没有油井文件或者油井文件内为0
                if (currentProductItem.Count == 0 && currentInjectItem.Count > 0)
                {
                    itemWellType newItem = new itemWellType();
                    newItem.sJH = currentInjectItem[0].sJH;
                    newItem.iType = (int)TypeWell.Injectwater;
                    newItem.YMstart = currentInjectItem[0].sYM;
                    newItem.YMend = "209912";
                    listItemWellType.Add(newItem);
                }

            //情况3 水井文件油井文件都有数据，目前算法支持 只做一次更替的，如果多次水转油 油转水的需要处理。
                if (currentProductItem.Count > 0 && currentInjectItem.Count > 0)
                {
                    itemWellType productItem = new itemWellType();
                    productItem.sJH = currentProductItem[0].sJH;
                    productItem.iType = (int)TypeWell.Injectwater;
                    productItem.YMstart = currentProductItem[0].sYM;
                    productItem.YMend = "209912";

                    itemWellType injectItem = new itemWellType();
                    injectItem.sJH = currentInjectItem[0].sJH;
                    injectItem.iType = (int)TypeWell.Injectwater;
                    injectItem.YMstart = currentInjectItem[0].sYM;
                    injectItem.YMend = "209912";

                    if (int.Parse(productItem.YMstart) <= int.Parse(injectItem.YMstart))
                    {
                        productItem.YMend = cPublicMethodBase.getYMLastMonth(injectItem.YMstart);
                        listItemWellType.Add(productItem);
                        listItemWellType.Add(injectItem);
                    }
                    else
                    {
                        injectItem.YMend = cPublicMethodBase.getYMLastMonth(productItem.YMstart);
                        listItemWellType.Add(injectItem);
                        listItemWellType.Add(productItem);
                    }
                }

            }

            //输入文件
            List<string> listLine=new List<string>();
            listLine.Add("井号 井型代码 起始时间 结束时间");
            foreach(itemWellType item in listItemWellType) listLine.Add(itemWellType.item2string(item));
            string fileOut = Path.Combine(cProjectManager.dirPathUsedProjectData, cProjectManager.filePathWellTypeDic);
            cIOBase.write2file(listLine, fileOut);
        }

        public static List<itemWellType> read2Struct() 
        {
             List<itemWellType> listReturn = new List<itemWellType>();
             string filepath = Path.Combine(cProjectManager.dirPathUsedProjectData, cProjectManager.filePathWellTypeDic);
             foreach (string sLine in cIOBase.readText2StringList(filepath, 2)) 
             {
                 listReturn.Add(itemWellType.parseLine(sLine));
             } 
            
            return listReturn;
        }

        public static List<string> getJHListProduct() 
        {
            return read2Struct().FindAll(p => p.iType == (int)TypeWell.Oil).Select(p => p.sJH).Distinct().ToList();
        }

        public static List<string> getJHListInject()
        {
            return read2Struct().FindAll(p => p.iType == (int)TypeWell.Injectwater).Select(p => p.sJH).Distinct().ToList();
        }

        
    }
}
