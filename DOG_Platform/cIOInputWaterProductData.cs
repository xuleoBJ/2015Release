using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cIOInputWaterProductData : cIOBase
    {
        public static void creatInputFile(string _sJH, List<string> listLinesInput)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellInject);
            cIOBase.write2file(listLinesInput, filePath);
        }
        public static void selectData2FileFromWaterWellProductByJH(string sJH, string filePathWrited)
        {
            StreamWriter sw = new StreamWriter(filePathWrited, false, Encoding.UTF8);
            //using (StreamReader sr = new StreamReader(cProjectManager.filePathInputInjectWellData, Encoding.UTF8))
            //{
            //    String line;
            //    int iLine = 0;
            //    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
            //    {
            //        iLine++;
            //        string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            //        if (split[0] == sJH)
            //        {
            //            sw.WriteLine(line);
            //        }
            //    }
            //}
            sw.Close();
        }
    }
}
