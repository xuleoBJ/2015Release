using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cIOinputWellHead
    {
        public cIOinputWellHead() 
        {
            listWellHead = readWellHead2Struct();
        }

        public List<ItemWellHead> listWellHead { get; set; }
        public static void readInput2Project(string userInputText, string sProjectInputText)
        {
            //first 需要验证文件格式是否正确，数据格式是否正确，不正确应该放弃

        }

        public static List<string> getLtStrJH()
        {
            List<ItemWellHead> listWellHead = readWellHead2Struct();
            List<string> ltJH = listWellHead.Select(p => p.sJH).ToList();
            return ltJH;
        }
        public static void readInput2Project(DataGridView dataGridView, string sProjectInputText)
        {
            bool IsDataOK = true; //数据未校验
            //数据校验过程
            cProjectData.sErrLineInfor = "";
            for (int j = 0; j < dataGridView.RowCount - 1; j++)
                for (int i = 0; i < dataGridView.ColumnCount; i++)
                {   //判读数据是否缺失
                    //井号，X,Y不能缺失，海拔缺失和井型缺失，补0占位
                    if (dataGridView.Rows[j].Cells[i].Value == null)
                    {
                        String line = "";
                        if (i <= 2)
                        {
                            //错误信息提示，并且不能导入数据
                            line = "文件第" + (j + 1).ToString() + "行" + "第" + (i + 1).ToString() + "列数据可能缺失或者有错误，请查看。" + "\r\n";
                            cProjectData.sErrLineInfor += line;
                            IsDataOK = false;
                        }
                        else 
                        {  //错误信息写入，但仍旧可以导入数据
                            line = "文件第" + (j + 1).ToString() + "行" + "第" + (i + 1).ToString() + "列数据可能缺失或者有错误，请查看。" + "\r\n";
                            cProjectData.sErrLineInfor += line;
                            dataGridView.Rows[j].Cells[i].Value = "0"; 
                        }
                    }
                }

            if (IsDataOK == true) //数据通过所有的校验过程，整理成所需要的格式
            {
                StreamWriter swWrited = new StreamWriter(sProjectInputText, false, Encoding.UTF8);

                for (int j = 0; j < dataGridView.RowCount - 1; j++)
                {
                    List<string> listData = new List<string>();
                    for (int i = 0; i < dataGridView.ColumnCount; i++)
                    {
                        listData.Add(dataGridView.Rows[j].Cells[i].Value.ToString());
                    }
                    swWrited.Write(string.Join("\t", listData.ToArray()) + "\n");
                }
                swWrited.Close();
            }
            else
            {
                MessageBox.Show("数据有错误，请查看相关信息！", "提示信息");
                cPublicMethodForm.outputErrInfor2Text(cProjectData.sErrLineInfor);
            }
        }

        public static List<ItemWellHead> readWellHead2Struct()
        {
            List<ItemWellHead> listWellHead = new List<ItemWellHead>();
            try
            {
                if (File.Exists(cProjectManager.filePathInputWellhead))
                {
                    using (StreamReader sr = new StreamReader(cProjectManager.filePathInputWellhead, System.Text.Encoding.UTF8))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                        {
                            ItemWellHead sttWellHead = new ItemWellHead();
                            string[] split = line.Trim().Split( );
                            sttWellHead.sJH = split[0];
                            sttWellHead.dbX = double.Parse(split[1]);
                            sttWellHead.dbY = double.Parse(split[2]);
                            sttWellHead.fKB = float.Parse(split[3]);
                            sttWellHead.iWellType = int.Parse(split[4]);
                            listWellHead.Add(sttWellHead);
                        }
                    }
                }
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return listWellHead;
        }
        
        public static  void deleteJHFromWellHead(string sJH)
        {
            cIOBase.deleteLinesByFirstWordFromText(cProjectManager.filePathInputWellhead, sJH, 0);
        }

        public static void updateWellHead(ItemWellHead itemNewWellHead)
        {
            cIOBase.replaceLineByFirstWord(cProjectManager.filePathInputWellhead, itemNewWellHead.sJH, ItemWellHead.item2string(itemNewWellHead));
        }


        static public void codeReplaceWellHead()
        {
            cProjectData.sErrLineInfor = "";
            string fileNameTempWellhead = cProjectManager.dirPathTemp + "wellHead.txt";
            StreamWriter swWellHead = new StreamWriter(fileNameTempWellhead, false, Encoding.UTF8);
            using (StreamReader sr = new StreamReader(cProjectManager.filePathInputWellhead, System.Text.Encoding.UTF8))
            {
                String line;
                while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                {
                    string[] split = line.Trim().Split(new char[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    split[4] = ItemWellHead.codeReplace(split[4]);
                    swWellHead.WriteLine(string.Join("\t", split));
                }
            }
            swWellHead.Close();

            File.Copy(fileNameTempWellhead, cProjectManager.filePathInputWellhead, true);
            File.Delete(fileNameTempWellhead);
        }
       
    }
}
