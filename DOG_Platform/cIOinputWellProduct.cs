using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cIOinputWellProduct : cIOBase
    {
        public static void creatInputFile(string _sJH, List<string> listLinesInput)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellProduct);
            cIOBase.write2file(listLinesInput, filePath);
        }

        public static List<ItemInputWellProduct> readInput2Struct(string _sJH)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellProduct);
            List<ItemInputWellProduct> listInputReturn = new List<ItemInputWellProduct>();
            if (File.Exists(filePath))
            {
                List<string> ltLines = cIOBase.getListStrFromTextByFirstWord(filePath, _sJH);
                foreach (string line in ltLines)
                {
                    if (line.TrimEnd() != "")
                    {
                        ItemInputWellProduct item = ItemInputWellProduct.parseLine(line);
                       listInputReturn.Add(item);
                    }
                }

            }

            return listInputReturn;
        }
    }
}
