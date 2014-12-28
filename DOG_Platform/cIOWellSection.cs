using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DOGPlatform.XML;
using System.Windows.Forms;
namespace DOGPlatform
{
    class cIOWellSection 
    {
      
        public static void delLog(string srcDir,string sLogName)
        {
          string filePath=Path.Combine(srcDir,sLogName+".txt");
          if (File.Exists(filePath))  File.Delete(filePath); 
        }
         public static void addLog(string sJH,string sLogName,string filePath)
        {
            cIOinputLog.extractTextLog2File(sJH, sLogName, filePath);
        }

         public static void addLogProperty(string sLogName, string sLogColor, float fRightValue, float fLeftValue, int iLeftOrRight) 
         {
             cXDocSection.addTrackLog(cProjectManager.xmlSectionCSS, "idLog#" + sLogName, 20, iLeftOrRight, sLogName, fLeftValue, fRightValue, sLogColor);
         }
        
    }
}
