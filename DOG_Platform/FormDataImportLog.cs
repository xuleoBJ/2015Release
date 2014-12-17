using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DOGPlatform
{
    public partial class FormDataImportLog : Form
    {
        public FormDataImportLog(string _sJH)
        {
            InitializeComponent();
            initializeForm(_sJH);
        }

        string sJH="";
        string filePathSourceLogFile = "";

        void initializeForm(string _sJH)
        {
            this.Text = _sJH;
            this.sJH=_sJH;
            List<string> logFormatText = new List<string>();
            logFormatText.Add("Forward1.0");
            logFormatText.Add("ascii");
            cbbLogFormat.DataSource = logFormatText;
            this.cbbLogFormat.SelectedIndex = 0;
        }

        private void btnOpenEX_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd_logFilePath = new OpenFileDialog();

            ofd_logFilePath.Title = sJH;
            ofd_logFilePath.Filter = "txt文件|*.txt|所有文件|*.*";
            //设置默认文件类型显示顺序 
            ofd_logFilePath.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            ofd_logFilePath.RestoreDirectory = true;
            if (ofd_logFilePath.ShowDialog() == DialogResult.OK)
            {
                filePathSourceLogFile=ofd_logFilePath.FileName;
                this.tbxUserFilePath.Text = filePathSourceLogFile;

                cPublicMethodForm.textboxViewText(this.tbxView, filePathSourceLogFile, 20);
              
                List<string> ltStrHead= getListLogHeadByLogFormat(cbbLogFormat.SelectedIndex ,filePathSourceLogFile) ;
                //column=index+1 and colunm 0 is depth
                for (int i = 0; i < ltStrHead.Count;i++ )
                    {
                        this.dgvLog.Rows.Add(ltStrHead[i], (i+2).ToString(),ltStrHead[i].ToUpper());
                    }
            }
            
        }

        List<string> getListLogHeadByLogFormat(int indexFormat,string filepath) 
        {
            List<string> listLogHeadColumn = new List<string>();
            if (indexFormat == 0) listLogHeadColumn=cIOinputLog.getLogSerierNamesFromLogForward(filepath);
            return listLogHeadColumn; 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //depth can't delete need deal
            cPublicMethodForm.deleteSelectedRowInDataGridView(dgvLog);

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            importTextLogForWard1();
            this.Close();
            cProjectData.setProjectGlobalLogSeriers();
        }

        void importTextLogForWard1() 
        {
            List<string> ltStrLogHead = new List<string>();
            List<int> ltIndexLog = new List<int>();
            
            for (int i = 0; i < dgvLog.Rows.Count-1; i++) 
            {
                ltStrLogHead.Add(dgvLog.Rows[i].Cells["logNameNew"].Value.ToString());
                ltIndexLog.Add(Convert.ToInt16(dgvLog.Rows[i].Cells["logNum"].Value)-1); //指数比列多1
            }
            for (int i = 0; i < ltIndexLog.Count; i++) 
            {
                string _logName = ltStrLogHead[i];
                int _indexLog = ltIndexLog[i];
                string _logFilePath = Path.Combine(cProjectManager.dirPathWellDir, sJH, _logName +cProjectManager.fileExtensionWellLog);
                List<string> _ltLogFileHead = new List<string>();
                _ltLogFileHead.Add("Depth");
                _ltLogFileHead.Add(_logName);
                string _firstLine = sJH + "此处加上测井曲线描述";
                cIOGeoEarthText.creatFileGeoHeadText(_logFilePath, _firstLine, _ltLogFileHead);
                cIOGeoEarthText.addDataLines2GeoEarTxt(_logFilePath, cIOinputLog.readLogData(filePathSourceLogFile, 7, _indexLog)); 
            }
            //全局测井头更新
            foreach (string _s in ltStrLogHead) 
            {
                if (cProjectData.ltStrLogSeriers.IndexOf(_s) < 0) cProjectData.ltStrLogSeriers.Add(_s);
            }
            

            //保留原输入测井数据,刚才导入时loghead删除了depth，保留文件时加上
            string filePath = Path.Combine(cProjectManager.dirPathWellDir, sJH,cProjectManager.fileNameInputWellLog);
            ltStrLogHead.Insert(0, "DEPTH");
            cIOGeoEarthText.creatFileGeoHeadText(filePath, sJH, ltStrLogHead);
            ltIndexLog.Insert(0, 0);
            cIOGeoEarthText.addDataLines2GeoEarTxt(filePath, cIOinputLog.readLogData(filePathSourceLogFile, 7, ltIndexLog));
            MessageBox.Show("导入完成。");
        }


    }
}
