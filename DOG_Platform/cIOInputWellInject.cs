using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cIOInputWellInject 
    {
        public static void creatInputFile(string _sJH, List<string> listLinesInput)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellInject);
            cIOBase.write2file(listLinesInput, filePath);
        }
        public static List<ItemInputWellInject> readInput2Struct(string _sJH)
        {
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, _sJH, cProjectManager.fileNameInputWellInject);
            List<ItemInputWellInject> listInputReturn = new List<ItemInputWellInject>();
            if (File.Exists(filePath))
            {
                List<string> ltLines = cIOBase.getListStrFromTextByFirstWord(filePath, _sJH);
                foreach (string line in ltLines)
                {
                    if (line.TrimEnd() != "")
                    {
                        ItemInputWellInject item = ItemInputWellInject.parseLine(line);
                        listInputReturn.Add(item);
                    }
                }

            }

            return listInputReturn;
        }
    }
}
