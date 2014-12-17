using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace DOGPlatform
{
    class ItemWellMapLayer : ItemLayerDataDic
    {
        public int iWellType;
        public ItemWellMapLayer() { }
        public ItemWellMapLayer(ItemLayerDataDic item)
        {
            this.sJH = item.sJH;
            this.sXCM = item.sXCM;
            this.dbX = item.dbX;
            this.dbY = item.dbY;
            this.dfZ = item.dfZ;
            this.fDCHD = item.fDCHD;
            this.fSH = item.fSH;
            this.fYXHD = item.fYXHD;
            this.fKXD = item.fKXD;
            this.fSTL = item.fSTL;
            this.fBHD = item.fBHD;
            this.fDS1_md = item.fDS1_md;
            this.fDS2_md = item.fDS2_md;
            this.fDS1_TVD = item.fDS1_TVD;
            //present find TypeWell in ProjectData.cwells fututure find it in wellTypeDic
            this.iWellType = cProjectData.listProjectWell.Find(p => p.sJH == item.sJH).iWellType;
        }

        public ItemWellMapLayer(string sJH)
        {
            ItemWellHead item = new ItemWellHead(sJH);
            this.sJH = sJH;
            this.sXCM = "0";
            this.dbX = item.dbX;
            this.dbY = item.dbY;
            this.dfZ = item.fKB;
            this.iWellType = item.iWellType;
            //present find TypeWell in ProjectData.cwells fututure find it in wellTypeDic
            this.iWellType = cProjectData.listProjectWell.Find(p => p.sJH == item.sJH).iWellType;
        }



        public static string item2string(ItemWellMapLayer item)
        {
            List<string> ltStrWrited = new List<string>();
            ltStrWrited.Add(item.sJH);
            ltStrWrited.Add(item.sXCM);
            ltStrWrited.Add(item.dbX.ToString());
            ltStrWrited.Add(item.dbY.ToString());
            ltStrWrited.Add(item.dfZ.ToString());
            ltStrWrited.Add(item.fDCHD.ToString());
            ltStrWrited.Add(item.fSH.ToString());
            ltStrWrited.Add(item.fYXHD.ToString());
            ltStrWrited.Add(item.fKXD.ToString());
            ltStrWrited.Add(item.fSTL.ToString());
            ltStrWrited.Add(item.fBHD.ToString());
            ltStrWrited.Add(item.fDS1_md.ToString());
            ltStrWrited.Add(item.fDS2_md.ToString());
            ltStrWrited.Add(item.fDS1_TVD.ToString());
            ltStrWrited.Add(item.iWellType.ToString());
            return string.Join("\t", ltStrWrited.ToArray());
        }

        public new static ItemWellMapLayer parseLine(string line)
        {
            string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            ItemWellMapLayer item = new ItemWellMapLayer();
            if (split.Count() >= 12)
            {

                item.sJH = split[0];
                item.sXCM = split[1]; 
                item.dbX = 0.0;
                double.TryParse(split[2], out item.dbX);
                item.dbY = 0;
                double.TryParse(split[3], out item.dbY);
                item.dfZ = 0;
                double.TryParse(split[4], out item.dfZ);
                item.fDCHD = 0.0f;
                float.TryParse(split[5], out item.fDCHD);
                item.fSH = 0.0f;
                float.TryParse(split[6], out item.fSH);
                item.fYXHD = 0.0f;
                float.TryParse(split[7], out item.fYXHD);
                item.fKXD = 0.0f;
                float.TryParse(split[8], out item.fKXD);
                item.fSTL = 0.0f;
                float.TryParse(split[9], out item.fSTL);
                item.fBHD = 0.0f;
                float.TryParse(split[10], out item.fBHD);
                item.fDS1_md = 0.0f;
                float.TryParse(split[11], out item.fDS1_md);
                item.fDS2_md = 0.0f;
                float.TryParse(split[12], out item.fDS2_md);
                item.fDS1_TVD = 0.0f;
                float.TryParse(split[13], out item.fDS1_TVD);
                item.iWellType= 0;
                int.TryParse(split[14], out item.iWellType);
            }
            return item;
        } 
    }
}
