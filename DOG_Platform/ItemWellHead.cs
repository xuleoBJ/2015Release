using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    struct ItemWellHead
    {
            public string sJH;
            public double dbX;
            public double dbY;
            public float fKB;
            public int iWellType;

            public ItemWellHead(string _sJH) 
            {
                sJH = _sJH;
                this.dbX = 0.0;
                this.dbY = 0.0;
                this.fKB = 0.0f;
                this.iWellType = 0;
               if (File.Exists(cProjectManager.filePathInputWellhead))
                {
                    using (StreamReader sr = new StreamReader(cProjectManager.filePathInputWellhead, System.Text.Encoding.UTF8))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                        {
                            string[] split = line.Trim().Split(new char[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                            if (split[0] == _sJH)
                            {
                                sJH = split[0];
                                dbX = double.Parse(split[1]);
                                dbY = double.Parse(split[2]);
                                fKB = float.Parse(split[3]);
                                iWellType = int.Parse(split[4]);
                                break;
                            }
                        }
                    }
                }


            }

            public static string item2string(ItemWellHead item)
            {
                List<string> ltStrWrited = new List<string>();
                ltStrWrited.Add(item.sJH);
                ltStrWrited.Add(item.dbX.ToString());
                ltStrWrited.Add(item.dbY.ToString());
                ltStrWrited.Add(item.fKB.ToString());
                ltStrWrited.Add(item.iWellType.ToString());
                return string.Join("\t", ltStrWrited.ToArray());
            }

            static public string codeReplace(string _sJX)
            {
                if (_sJX.IndexOf("油井") >= 0 ||_sJX.ToUpper() =="OIL"|| _sJX == "3") _sJX = "3";
                else if (_sJX.IndexOf("水井") >= 0|| _sJX == "15") _sJX = "15";
                else if (_sJX.IndexOf("水井") >= 0 || _sJX == "5") _sJX = "5";
                else _sJX = "0";
                return _sJX;
            } 
    }
}
