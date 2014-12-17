using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DOGPlatform
{
    class cDirDataSourceWellSection
    {
        public static trackLayerDepthDataList setupDataListTrackLayerDepth(string filePath,float fTop,float fBase)
        {
            trackLayerDepthDataList trackDataListLayerDepth = new trackLayerDepthDataList();
            trackDataListLayerDepth.fListDS1 = new List<float>();
            trackDataListLayerDepth.fListDS2 = new List<float>();
            trackDataListLayerDepth.ltStrXCM = new List<string>();

            string sData = "";
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                sData = sr.ReadToEnd();
            }

            string[] split = sData.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i = i + 3)
            {
                float fCurrentTop = float.Parse(split[i]);
                float fCurrentBase = float.Parse(split[i + 1]);
                if (fTop <= fCurrentTop && fCurrentBase <= fBase)
                {
                    trackDataListLayerDepth.fListDS1.Add(fCurrentTop);
                    trackDataListLayerDepth.fListDS2.Add(fCurrentBase);
                    trackDataListLayerDepth.ltStrXCM.Add(split[i + 2]);
                }
            }
            return trackDataListLayerDepth;
        }

        public static trackJSJLDataList setupDataListTrackJSJL(string filePath, float fTop, float fBase)
        {
            trackJSJLDataList trackDataListJSJL = new trackJSJLDataList();
            trackDataListJSJL.fListDS1 = new List<float>();
            trackDataListJSJL.fListDS2 = new List<float>();
            trackDataListJSJL.iListJSJL  = new List<int>();

            string sData = "";
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                sData = sr.ReadToEnd();
            }

            string[] split = sData.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i = i + 3)
            {
                float fCurrentTop = float.Parse(split[i]);
                float fCurrentBase = float.Parse(split[i + 1]);
                int iJSJL = int.Parse(split[i + 2]);
                if (fTop <= fCurrentTop && fCurrentBase <= fBase)
                {
                    trackDataListJSJL.fListDS1.Add(fCurrentTop);
                    trackDataListJSJL.fListDS2.Add(fCurrentBase);
                    trackDataListJSJL.iListJSJL.Add(iJSJL);
                }
            }
            return trackDataListJSJL;
        }

        public static trackInputPerforationDataList setupDataListTrackPerforation(string filePath, float fTop, float fBase)
        {
            trackInputPerforationDataList trackDataListPerforation = new trackInputPerforationDataList();
            trackDataListPerforation.fListDS1 = new List<float>();
            trackDataListPerforation.fListDS2 = new List<float>();
            trackDataListPerforation.ltStrYYYYMM  = new List<string>();
            if(File.Exists(filePath))
            {
                string sData = "";
                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
                {
                    sData = sr.ReadToEnd();
                }

                string[] split = sData.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < split.Length; i = i + 4)
                {
                    string sJH = split[i];
                    string iYYYYMM = split[i+1];
                    float fCurrentTop = float.Parse(split[i + 2]);
                    float fCurrentBase = float.Parse(split[i + 3]);

                    if (fTop <= fCurrentTop && fCurrentBase <= fBase)
                    {
                        trackDataListPerforation.fListDS1.Add(fCurrentTop);
                        trackDataListPerforation.fListDS2.Add(fCurrentBase);
                        trackDataListPerforation.ltStrYYYYMM.Add(iYYYYMM);
                    }
                }
            } 
            return trackDataListPerforation;
        }

        public static trackLogDataList  setupDataListTrackLog(string filePath, float fTop, float fBase)
        {
            trackLogDataList trackDataListLog = new trackLogDataList();
            trackDataListLog.fListMD = new List<float>();
            trackDataListLog.fListValue = new List<float>();

            string sData = "";
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                sData = sr.ReadToEnd();
            }
            
            string[] split = sData.Trim().Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 1)
            {
                int iStartPosition = 1; //读取的开始字符位置
                trackDataListLog.sLogName = split[0];
                int iInterval = 2; //定义抽稀比例
                for (int i = iStartPosition; i < split.Length; i = i + 2 * iInterval)
                {
                    float fMD = float.Parse(split[i]);
                    float fValue = float.Parse(split[i + 1]);
                    if (fTop <= fMD && fMD <= fBase)
                    {
                        trackDataListLog.fListMD.Add(fMD);
                        trackDataListLog.fListValue.Add(fValue);
                    }
                }
            }
            return trackDataListLog;
        }

        void addLogData(string sJH, string sSelectedLogName,string filePath)
        {
            cIOinputLog.extractTextLog2File(sJH, sSelectedLogName, filePath);
        }
    }
}
