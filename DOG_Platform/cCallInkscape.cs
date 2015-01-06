using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cCallInkscape
    {
        public static void callInk(string svgfilepath)
        {
            if (!File.Exists(cProjectManager.filePathInkscape))
            {
                if (findPathInkscape() == false) MessageBox.Show("未找到编辑模块，请输入路径或者选择模块。");
            }
            if (svgfilepath != "" && File.Exists(cProjectManager.filePathInkscape)) 
                System.Diagnostics.Process.Start(cProjectManager.filePathInkscape, svgfilepath); 
            
        }

        static bool findPathInkscape()
        {
            List<string> ListSearchPath = new List<string>();
            ListSearchPath.Add(@"C:\Program Files\Inkscape");
            ListSearchPath.Add(@"C:\Program Files (x86)\Inkscape");
            ListSearchPath.Add(@"D:\Program Files\Inkscape");
            ListSearchPath.Add(@"D:\Program Files (x86)\Inkscape");
            foreach (string sPath in ListSearchPath) 
            {
                string filePath=Path.Combine(sPath,"inkscape.exe");
                if (File.Exists(filePath)) 
                {
                    cProjectManager.filePathInkscape = filePath;
                    cProjectManager.filePahtsvgPattern = Path.Combine(Path.GetDirectoryName(cProjectManager.filePathInkscape), "share", "pattern", "patterns.svg"); 
                    return true ;
                }
            }
            return false;
        }
    }
}
