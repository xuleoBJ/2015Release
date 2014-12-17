using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cFileOperateDicPerforation : cIOinputPerforation 
    {
        //public void generatePerforationDic()
        //{
        //    List<ItemLayerDepth> layerDepthItems =cFileOperateInputLayerDepth.readLayerDepth2Struct();
        //    cPerforationList perforationList = new cPerforationList();
        //    perforationList.readPerforation2List();

        //    StreamWriter sw = new StreamWriter(cProject.filePathPerforationDic, false, Encoding.UTF8);
        //    List<string> ltStrHeadColoum = new List<string>();  //射孔表字典
        //    ltStrHeadColoum.Add("WellName");
        //    ltStrHeadColoum.Add("LayerName");
        //    ltStrHeadColoum.Add("YMstart");
        //    ltStrHeadColoum.Add("YMend");
        //    sw.WriteLine(string.Join("\t", ltStrHeadColoum.ToArray()));
        //    if (cProject.ltStrProjectJH.Count > 0 && cProject.ltStrProjectXCM.Count > 0)
        //    {
        //        for (int i = 0; i < cProject.ltStrProjectJH.Count; i++)
        //        {
        //            for (int j = 0; j < cProject.ltStrProjectXCM.Count; j++)
        //            {

        //                string sCurrentJH = cProject.ltStrProjectJH[i].ToString();
        //                string sCurrentXCM = cProject.ltStrProjectXCM[j].ToString();

        //                float fCurrentLayerDS1 = 0;//当前层位的顶面测深
        //                float fCurrentLayerDS2 = 0;//当前层位的底面测深

        //                //读取层位顶底深，获取fCurrentLayerDS1，fCurrentLayerDS2
        //                bool bFoundInLayerDepth = false;
        //                for (int k_layerDepth = 0; k_layerDepth < this.ltStrWellName_layerDepth.Count; k_layerDepth++)
        //                {

        //                    if (sCurrentJH == ltStrWellName_layerDepth[k_layerDepth]
        //                        && sCurrentXCM == ltStrLayerName_layerDepth[k_layerDepth])
        //                    {
        //                        fCurrentLayerDS1 = this.fListTopDepth_layerDepth[k_layerDepth];
        //                        fCurrentLayerDS2 = this.fListBottomDepth_layerDepth[k_layerDepth];
        //                        bFoundInLayerDepth = true;
        //                        break;
        //                    }

        //                }

        //                //写perforatedDic

        //                List<string> ltStrWrited = new List<string>();
        //                ltStrWrited.Add(sCurrentJH);
        //                ltStrWrited.Add(sCurrentXCM);
        //                string startYMPerforated = "209912";
        //                string endYMPerforated = "209912";
        //                //有深度数据
        //                if (bFoundInLayerDepth == true)
        //                {
        //                    for (int k = 0; k < perforationList.ltStrWellName_perforation.Count(); k++)
        //                    {
        //                        //算法 当井号相同，年月大于读入射孔年月时，
        //                        //射开条件 满足 
        //                        //非（层段丁低于射孔底或者层段底高于射孔段的顶）
        //                        if (perforationList.ltStrWellName_perforation[k] == sCurrentJH)
        //                        {
        //                            if (    !(fCurrentLayerDS1 >= perforationList.fListBottomDepth_perforation[k] ||
        //                                        fCurrentLayerDS2 <= perforationList.fListTopDepth_perforation[k])           )

        //                                startYMPerforated = perforationList.ltStrYearMonth_perforation[k];
        //                        }
        //                    }

        //                }

        //                ltStrWrited.Add(startYMPerforated);
        //                ltStrWrited.Add(endYMPerforated);
        //                sw.WriteLine(string.Join("\t", ltStrWrited.ToArray()));

        //            }
        //        }
        //    }
        //    sw.Close();
        //    MessageBox.Show("射孔字典表拼写计算完毕");

        //}
    }
}
