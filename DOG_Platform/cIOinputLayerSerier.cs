using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cIOinputLayerSerier
    {
        //读取Layerseriers
        List<string> ltStrLayerName_layerSeriers = new List<string>();
        public  void readLayerSeriers2List()
        {
            ltStrLayerName_layerSeriers.Clear();
            string[] split;
            try
            {
                using (StreamReader sr = new StreamReader(cProjectManager.filePathInputLayerSeriers, System.Text.Encoding.Default))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        ltStrLayerName_layerSeriers.Add(split[0]);
                    }
                }

            }

            catch (Exception e)
            {
      //          MessageBox.Show(e.ToString());
            }

        }

        public static List<string> getSelectedXCMList(int _indexTopLayer, int _indexBotLayer)
        {
            return cProjectData.ltStrProjectXCM.Skip(_indexTopLayer).Take(_indexBotLayer - _indexTopLayer + 1).ToList();
        }

        public static void creatInputFaultFile(string _xcm, List<string> listLinesInput)
        {
            string filePath = Path.Combine(cProjectManager.dirPathLayerDir, _xcm, cProjectManager.fileNameInputFaults);
            cIOBase.write2file(listLinesInput, filePath);
        }

    }
}
