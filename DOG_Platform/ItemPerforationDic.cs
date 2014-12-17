using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOGPlatform
{
    struct ItemPerforationDic
    {
        public string sJH;
        public string sXCM;
        public string YMstart; //射孔开始时间
        public string YMend;  //堵孔时间
        public float fDS1;
        public float fDS2;

        public static string item2string(ItemPerforationDic item)
        {
            List<string> ltStrWrited = new List<string>();
            ltStrWrited.Add(item.sJH);
            ltStrWrited.Add(item.sXCM);
            ltStrWrited.Add(item.YMstart);
            ltStrWrited.Add(item.YMend);
            ltStrWrited.Add(item.fDS1.ToString());
            ltStrWrited.Add(item.fDS2.ToString());
           
            return string.Join("\t", ltStrWrited.ToArray());
        }
    }
}
