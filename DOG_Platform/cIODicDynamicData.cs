using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cIODicDynamicData
    {
        public static void generateDynamicData()
        {
        //数据太多，按时间建立文件拆分是最好的解决办法，即避免了井分割 又避免了层分割。
            foreach (string sYM in cProjectData.ltStrProjectYM) 
            {
             //创建文件
                string filePath = Path.Combine(cProjectManager.dirPathUsedProjectData, sYM+ cProjectManager.fileExtensionDynamic);
                StreamWriter swNew = new StreamWriter(filePath, false, Encoding.UTF8);
                foreach (string sJH in cProjectData.ltStrProjectJH)
                {
                    ItemInputWellProduct currentProductItem = cIOinputWellProduct.readInput2Struct(sJH).Find(p=>p.sYM==sYM);
                    ItemInputWellInject currentInjectItem = cIOInputWellInject.readInput2Struct(sJH).Find(p => p.sYM == sYM);
                    //情况1 有油井文件没有水井文件或者水井文件内为0
                    float lcy = 0.0f;
                    if (currentProductItem.sYM != null)
                    {
                        lcy = currentProductItem.fSum_oil;
                    }

                    if (currentInjectItem.sYM!=null)
                    {
                        lcy = currentInjectItem.fLZSL;
                    }


                    swNew.WriteLine(sJH + "\t" + "all" + "\t" + lcy.ToString());
                    foreach (string sXCM in cProjectData.ltStrProjectXCM)
                        swNew.WriteLine(sJH + "\t" + sXCM + "\t" + lcy.ToString());
                }
                swNew.Close();

            }
        }
    }
}
