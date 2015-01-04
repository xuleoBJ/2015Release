using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace DOGPlatform
{
    class cIOMapLayer 
    {
        public List<string> selectJH2ltStrFromProductFile(string fileName)
        {
            List<string> ltStrReturnJH = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    String line;
                    int iLine = 0;
                    while ((line = sr.ReadLine()) != null)  //delete the line whose legth is 0
                    {
                        iLine++;
                        string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (iLine > 1)
                        {
                            ltStrReturnJH.Add(split[0]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return ltStrReturnJH;
        }
    }
}
