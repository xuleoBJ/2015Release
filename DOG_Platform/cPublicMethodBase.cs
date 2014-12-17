using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cPublicMethodBase
    {
        
        public static void showErrInfor(string sRightNote)
        {
            if (cProjectData.sErrLineInfor== "")
            {
                MessageBox.Show(sRightNote);
            }
            else
            {
                cProjectData.sErrLineInfor= "数据可能错误如下：" + " \r\n" + cProjectData.sErrLineInfor;
                cPublicMethodForm.outputErrInfor2Text(cProjectData.sErrLineInfor);
            }

        }

        public static string getRGB(Color m_color)
        {
            string r = m_color.R.ToString();
            string g = m_color.G.ToString();
            string b = m_color.B.ToString();
            return "rgb(" + r + "," + g + "," + b + ")";
        }
        public static int getCeilingNumer(float fValue,int iInteval)
        {
             return Convert.ToInt16(Math.Ceiling(fValue) / iInteval + 1) * iInteval;
        }

        public static void printsErrLineInforIndex(int iIndexLine)
        {
            StreamWriter swNew = new StreamWriter(cProjectManager.filePathErrInfor, false, Encoding.UTF8);
            swNew.WriteLine(iIndexLine.ToString());
            swNew.Close();
        }

        public static void outputErrInfor2Text(string errInfor)
        {
            StreamWriter swNew = new StreamWriter(cProjectManager.filePathErrInfor, false, Encoding.UTF8);
            swNew.WriteLine(DateTime.Now.ToString());
            swNew.WriteLine(errInfor);
            swNew.Close();
            System.Diagnostics.Process.Start("notepad.exe", cProjectManager.filePathErrInfor);
        }

        public static List<string> ltStrJHInProject(List<string> ltStrJH )
        {
            for (int i = (ltStrJH.Count - 1); i >= 0; i--)
            {
                if (cProjectData.ltStrProjectJH.Contains(ltStrJH[i]) == false)
                    ltStrJH.RemoveAt(i);
            }
            return ltStrJH;
        }
      

    }
}
