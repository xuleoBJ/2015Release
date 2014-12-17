using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cCalProjectAnaFiles
    {
        //把JSJL文件归到LayerDepth层号
        public void matchJSJL2LayerDepth()
        {
            //cProjectData.sErrLineInfor = "";
            //List<ItemLayerDepth> listLayerDepth = cIOinputLayerDepth.readLayerDepth2Struct();
            ////List<ItemJSJL> listJSJL = cIOinputJSJL.readJSJL2Struct();
            //List<ItemJSJL> listJSJL = new List<ItemJSJL>(); 
            //StreamWriter sw = new StreamWriter(cProjectManager.dirPathUsedProjectData + "$jsjlMatchLayerDepth$.txt", false, Encoding.UTF8);

            //for (int i = 0; i < listJSJL.Count; i++)
            //{
            //    ItemJSJL currentItemJSJL = listJSJL[i];
            //    string sCurrentJH = currentItemJSJL.sJH;
            //    string sXCM = "-999";
            //    for (int j = 0; j < listLayerDepth.Count; j++)
            //    {
            //        ItemLayerDepth currentItemLayerDepth = listLayerDepth[j];
            //        if (sCurrentJH == currentItemLayerDepth.sJH)
            //        {
            //            //当井号相同，并且jsjl的顶深>=layer的顶深并且jsjj结论的底深<=layer的底深
            //            if (currentItemJSJL.fDS1 >= currentItemLayerDepth.fDS1 && currentItemJSJL.fDS2 <= currentItemLayerDepth.fDS2)
            //            {
            //                sXCM = currentItemLayerDepth.sXCM;
            //                break;
            //            }
            //            else if (currentItemJSJL.fDS1 < currentItemLayerDepth.fDS1 && currentItemJSJL.fDS2 >= currentItemLayerDepth.fDS2)
            //            {
            //                cProjectData.sErrLineInfor = cProjectData.sErrLineInfor + sCurrentJH + "\tJsjl " + currentItemJSJL.fDS1.ToString() + "\t"
            //                    + currentItemJSJL.fDS2.ToString() + " 与LayerDepth\t " + currentItemLayerDepth.fDS1.ToString() + "串层！\r\n";
            //            }
            //        }
            //    }
            //    List<string> ltStrWrited = new List<string>();
            //    ltStrWrited.Add(sCurrentJH);
            //    ltStrWrited.Add(sXCM);
            //    ltStrWrited.Add(currentItemJSJL.fDS1.ToString());
            //    ltStrWrited.Add(currentItemJSJL.fDS2.ToString());
            //    ltStrWrited.Add(currentItemJSJL.fSandThickness.ToString());
            //    ltStrWrited.Add(currentItemJSJL.fNetPaySand.ToString());
            //    ltStrWrited.Add(currentItemJSJL.fKXD.ToString());
            //    ltStrWrited.Add(currentItemJSJL.fSTL.ToString());
            //    ltStrWrited.Add(currentItemJSJL.fBHD.ToString());
            //    ltStrWrited.Add(currentItemJSJL.iJSJL.ToString());
            //    sw.WriteLine(string.Join("\t", ltStrWrited.ToArray()));
            //}

            //sw.Close();

            //if (cProjectData.sErrLineInfor== "")
            //{
            //    MessageBox.Show("解释结论匹配小层深度完成OK。");
            //}
            //else
            //{
            //    cProjectData.sErrLineInfor= "解释结论与分层数据串层信息：" + " \r\n" + cProjectData.sErrLineInfor;
            //    MessageBox.Show("解释结论匹配小层深度完成，请从日志文件中查看串层信息，调整后重新计算。", "注意！");
            //    cPublicMethodForm.outputErrInfor2Text(cProjectData.sErrLineInfor);
            //}

        }
        //劈分JSJL文件与LayerDepth串层
        public void splitJSJL2LayerDepth()
        {
            MessageBox.Show("需要修改调整");
        }


    }
}
