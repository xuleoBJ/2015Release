using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using DOGPlatform.XML;
using DOGPlatform.SVG;
namespace DOGPlatform
{
    public partial class FormWellsGroup : Form
    {
        List<cWellSectionSVG> listWellsSection = new List<cWellSectionSVG>();
        struct dirSection
        {
            public string sDirSectionData;
        }
        dirSection dirSectionTemp;
        List<string> ltStrSelectedJH = new List<string>();  //栅状图井井号
        List<float> fListDS1Showed = new List<float>();   //绘制的顶深
        List<float> fListDS2Showed = new List<float>();  //绘制的底深
        string fileDrawSourceInfor = cProjectManager.dirPathTemp + "FD_FDSectionInfor.txt";
        string fileDrawSourceText = cProjectManager.dirPathTemp + "FD_textTrack.txt";
        string fileDrawSourceLeftLog = cProjectManager.dirPathTemp + "FD_leftLogFile.txt";
        string fileDrawSourceRightLog = cProjectManager.dirPathTemp + "FD_rightLogFile.txt";
        string fileDrawSourceJSJL = cProjectManager.dirPathTemp + "FD_JSJLTrack.txt";
        string fileDrawSourceLayerDepth = cProjectManager.dirPathTemp + "FD_layerDepthTrack.txt";
        string fileDrawSourcePerforated = cProjectManager.dirPathTemp + "FD_PerforatedTrack.txt";
        private void InitDataStruct()
        {
            dirSectionTemp.sDirSectionData = Path.Combine(cProjectManager.dirPathTemp, "fenceTemp");
        } 

       
        public FormWellsGroup()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            InitFormWellsGroupControl();
            InitDataStruct();
        }
        private void InitFormWellsGroupControl()
        {
    
            cPublicMethodForm.inialListBox(lbxJH, cProjectData.ltStrProjectJH);
            cPublicMethodForm.inialComboBox(cbbTopXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbBottomXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(iWellIntervalDistanceNameLeft, cProjectData.ltStrLogSeriers);
            cPublicMethodForm.inialComboBox(iWellIntervalDistanceNameRight, cProjectData.ltStrLogSeriers);
        }
        void updateProjectDirection()
        {
            ltStrSelectedJH.Clear();
            fListDS1Showed.Clear();
            fListDS2Showed.Clear();
            try
            {
                using (StreamReader sr = new StreamReader(fileDrawSourceInfor, System.Text.Encoding.Default))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                    {
                        string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        ltStrSelectedJH.Add(split[0]);
                        fListDS1Showed.Add(float.Parse(split[1]));
                        fListDS2Showed.Add(float.Parse(split[2]));

                    }
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        private void btn_addWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.transferItemFromleftListBox2rightListBox(lbxJH, lbxJHSeclected);
        }
        private void btn_deleteWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.deleteSlectedItemFromListBox(lbxJHSeclected);
        }

        private void btnSectionData_Click(object sender, EventArgs e)
        {
            setDepthIntervalShowedBYLayer();
        }
        void updateSelectedListJH()
        {
            listWellsSection.Clear();
            ltStrSelectedJH.Clear();
            foreach (object selecteditem in lbxJHSeclected.Items)
            {
                string strItem = selecteditem as String;
                ltStrSelectedJH.Add(strItem);
            }
        }
        //void initializeTreeViewWellCollection()
        //{
        //    this.tvwWellSectionCollection.Nodes.Clear();
        //    for (int i = 0; i < ltStrSelectedJH.Count; i++)
        //    {
        //        TreeNode tnWell = new TreeNode();
        //        tnWell.Text = ltStrSelectedJH[i];
        //        tnWell.Name = ltStrSelectedJH[i];
        //        tnWell.Nodes.Add("左侧曲线");
        //        tnWell.Nodes.Add("右侧曲线");
        //        tvwWellSectionCollection.Nodes.Add(tnWell);
        //    }
        //}
        private void setDepthIntervalShowedBYLayer()
        {
            updateSelectedListJH();
            List<string> ltStrSelectedXCM = new List<string>();

            string sTopXCM = this.cbbTopXCM.SelectedItem.ToString();
            int iTopIndex = cProjectData.ltStrProjectXCM.IndexOf(sTopXCM);
            string sBottomXCM = this.cbbBottomXCM.SelectedItem.ToString();
            int iBottomIndex = cProjectData.ltStrProjectXCM.IndexOf(sBottomXCM);

            if (iBottomIndex - iTopIndex >= 0)
            {
                ltStrSelectedXCM = cProjectData.ltStrProjectXCM.GetRange(iTopIndex, iBottomIndex - iTopIndex + 1);
                //initializeTreeViewWellCollection();
                int _up = Convert.ToInt16(this.nUDtopDepthUp.Value);
                int _down = Convert.ToInt16(this.nUDbottomDepthDown.Value);

                for (int i = 0; i < ltStrSelectedJH.Count; i++)
                {
                    cWellSectionSVG _wellSection = new cWellSectionSVG(ltStrSelectedJH[i], 0, 0);
                    //有可能上下层有缺失。。。所以这块的技巧是找出深度序列，取最大最小值
                    cIOinputLayerDepth fileLayerDepth = new cIOinputLayerDepth();
                    List<float> fListDS1Return = fileLayerDepth.selectDepthListFromLayerDepthByJHAndXCMList(ltStrSelectedJH[i], ltStrSelectedXCM);
                    if (fListDS1Return.Count > 0)  //返回值为空 说明所选层段整个缺失！
                    {
                        _wellSection.fShowedDepthTop = fListDS1Return.Min() - _up;
                        _wellSection.fShowedDepthBase = fListDS1Return.Max() + _down;
                    }

                    listWellsSection.Add(_wellSection);
                }
                generateSectionCssXML();
                generateSectionDataDirectory();
            }
            else
            {
                MessageBox.Show("上层应该比下层选择高，请重新选择。");
            }
        }
        void generateSectionDataDirectory()
        {
            if (Directory.Exists(dirSectionTemp.sDirSectionData)) Directory.Delete(dirSectionTemp.sDirSectionData, true);
            Directory.CreateDirectory(dirSectionTemp.sDirSectionData);
            foreach (cWellSectionSVG item in listWellsSection)
            {
                string jhDir = Path.Combine(dirSectionTemp.sDirSectionData, item.sJH);
                Directory.CreateDirectory(jhDir);
                Directory.CreateDirectory(jhDir + "\\left");
                Directory.CreateDirectory(jhDir + "\\right");
            }
        }
        void generateSectionCssXML()
        {
            if (File.Exists(cProjectManager.xmlSectionCSS))
            {
                File.Delete(cProjectManager.xmlSectionCSS);
            }
            cXDocSection.generateXmlFile(cProjectManager.xmlSectionCSS);
        }
        private void setDepthIntervalShowed(object sender, WaitWindowEventArgs e)
        {
            List<string> ltStrSelectedXCM = new List<string>();
            string sTopXCM = this.cbbTopXCM.SelectedItem.ToString();
            int iTopIndex = cProjectData.ltStrProjectXCM.IndexOf(sTopXCM);
            string sBottomXCM = this.cbbBottomXCM.SelectedItem.ToString();
            int iBottomIndex = cProjectData.ltStrProjectXCM.IndexOf(sBottomXCM);
            if (iBottomIndex - iTopIndex >= 0)
            {
                fListDS1Showed.Clear();
                fListDS2Showed.Clear();
                ltStrSelectedXCM = cProjectData.ltStrProjectXCM.GetRange(iTopIndex, iBottomIndex - iTopIndex + 1);

                int _up = Convert.ToInt16(this.nUDtopDepthUp.Value);
                int _down = Convert.ToInt16(this.nUDbottomDepthDown.Value);

                for (int i = 0; i < ltStrSelectedJH.Count; i++)
                {
                    cIOinputLayerDepth fileLayerDepth = new cIOinputLayerDepth();
                    List<float> fListDS1Return = fileLayerDepth.selectDepthListFromLayerDepthByJHAndXCMList(ltStrSelectedJH[i], ltStrSelectedXCM);
                    if (fListDS1Return.Count > 0)  //返回值为空 说明所选层段整个缺失！
                    {
                        fListDS1Showed.Add(fListDS1Return.Min() - _up);
                        fListDS2Showed.Add(fListDS1Return.Max() + _down);
                    }
                    else
                    {
                        fListDS1Showed.Add(0);
                        fListDS2Showed.Add(0);
                    }
                }

                //generateSectionDrawData();
            }
            else
            {
                MessageBox.Show("上层应该比下层选择高，请重新选择。");
            }
            //获取层位深度上移深度

        }
        
        private void lbxJHSeclected_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltStrSelectedJH.Clear();
            foreach (object selecteditem in lbxJHSeclected.Items)
            {
                string strItem = selecteditem as String;
                ltStrSelectedJH.Add(strItem);
            }
        }
   
        string generateSectionGraph(string filenameSVGMap)
        {
            //继续初始化值
            double dfscale = cProjectData.dfMapScale;
            for (int i = 0; i < this.listWellsSection.Count; i++)
            {
                cWellSectionSVG itemWell = listWellsSection[i];
             
                Point pointConvert2View =
                   cCordinationTransform.transRealPointF2ViewPoint(itemWell.dbX, itemWell.dbY,
                    cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, dfscale);
                itemWell.fXview = pointConvert2View.X;
                itemWell.fYview = pointConvert2View.Y;
            }

            int iWidth =  2000;
            cSVGDocSection cSection = new cSVGDocSection( iWidth, 5000, 0, 50, "");
            cSection.addSVGTitle(10, 10);
            XmlElement returnElemment;
            for (int i = 0; i < listWellsSection.Count; i++)
            {
                string sJH = listWellsSection[i].sJH;
                float fTopShowed = listWellsSection[i].fShowedDepthTop;
                float fBaseShowed = listWellsSection[i].fShowedDepthBase;
                float fDepthFlatted = 0;
                float fCurrerntWellXView = listWellsSection[i].fXview;
                float fCurrerntWellYView = listWellsSection[i].fYview;



                //以下程序段画井柱子
                cSVGSectionWell currentWell = new cSVGSectionWell(sJH);
                returnElemment = currentWell.gWellCone(sJH, fTopShowed, fBaseShowed, fDepthFlatted, 10, 5);
                currentWell.addTrack(returnElemment, 0);

                //增加地层道
                string filePathLayer = Path.Combine(dirSectionTemp.sDirSectionData, sJH + "\\layerDepth.txt");
                trackLayerDepthDataList trackDataListLayerDepth =
                    cDirDataSourceWellSection.setupDataListTrackLayerDepth(filePathLayer, fTopShowed, fBaseShowed);
                int iTrackWidth = 15;
                cSVGSectionTrackLayer layerTrack = new cSVGSectionTrackLayer(iTrackWidth);
                layerTrack.iTextSize = 6;
                returnElemment = layerTrack.gTrackFenceLayerDepth(trackDataListLayerDepth, fDepthFlatted);
                currentWell.addTrack(returnElemment, iTrackWidth);

                //增加解释结论道
                string filePathJSJL = Path.Combine(dirSectionTemp.sDirSectionData, sJH + "\\jsjl.txt");
                trackJSJLDataList trackDataListJSJL = cDirDataSourceWellSection.setupDataListTrackJSJL(filePathJSJL, fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackJSJL JSJLTrack = new cSVGSectionTrackJSJL(iTrackWidth);
                returnElemment = JSJLTrack.gTrackJSJL(sJH,trackDataListJSJL, fDepthFlatted);
                currentWell.addTrack(returnElemment, -iTrackWidth);

                //增加射孔道
                string filePathInputPerforation = Path.Combine(dirSectionTemp.sDirSectionData, sJH + "\\inputPerforation.txt");
                trackInputPerforationDataList trackDataListPerforation = cDirDataSourceWellSection.setupDataListTrackPerforation(filePathInputPerforation, fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackPeforation perforationTrack = new cSVGSectionTrackPeforation(iTrackWidth);
                returnElemment = perforationTrack.gTrackPerforation(sJH,trackDataListPerforation, fDepthFlatted);
                currentWell.addTrack(returnElemment, -2 * iTrackWidth);

                //增加左边曲线
                cFileOperateDicLogHeadProject fileDicLog = new cFileOperateDicLogHeadProject();

                string fileLeftLogScrPath = Path.Combine(dirSectionTemp.sDirSectionData, sJH + "\\left");
                string[] fileList = Directory.GetFileSystemEntries(fileLeftLogScrPath);
                foreach (string fileLog in fileList)
                {
                    trackLogDataList trackDataListLeftLog = cDirDataSourceWellSection.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;
                    ItemLogHeadInfor ItemLogHeadInforItem = fileDicLog.selectLogHeadItem(sJH, trackDataListLeftLog.sLogName);
                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    returnElemment = logTrack.gTrackLog(ItemLogHeadInforItem, trackDataListLeftLog, fDepthFlatted);
                    currentWell.addTrack(returnElemment, -30);
                }
                //增加右边曲线
                string fileRightLogScrPath = Path.Combine(dirSectionTemp.sDirSectionData, sJH + "\\right");
                foreach (string fileLog in Directory.GetFileSystemEntries(fileRightLogScrPath))
                {
                    trackLogDataList trackDataListRightLog = cDirDataSourceWellSection.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;
                    ItemLogHeadInfor ItemLogHeadInforItem = fileDicLog.selectLogHeadItem(sJH, trackDataListRightLog.sLogName);
                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    returnElemment = logTrack.gTrackLog(ItemLogHeadInforItem, trackDataListRightLog, fDepthFlatted);
                    currentWell.addTrack(returnElemment, iTrackWidth);
                }
                //画井柱子结束

                //将井柱子增加到剖面图中
                cSection.addgElement(currentWell.gWell, Convert.ToInt32(fCurrerntWellXView), Convert.ToInt32(fCurrerntWellYView));
        
            }



            cSection.makeSVGfile(cProjectManager.dirPathMap + filenameSVGMap);
            return cProjectManager.dirPathMap + filenameSVGMap;

        }
        private void btnMakeSection_Click(object sender, EventArgs e)
        {

            string filenameSVGMap;
            if (this.tbxTitle.Text == "")
            {
                filenameSVGMap = string.Join("-", ltStrSelectedJH.ToArray()) + "_group.svg";
            }
            else
            {
                filenameSVGMap = this.tbxTitle.Text + ".svg";
            }
            FormWebNavigation formSVGView;

            formSVGView = new FormWebNavigation(generateSectionGraph(filenameSVGMap));
            formSVGView.Show();
        }

        private void btnAddJSJLTrack_Click(object sender, EventArgs e)
        {
            if (listWellsSection.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionTemp.sDirSectionData, sJH + "\\jsjl.txt");
                    cIOinputJSJL.selectSectionDrawData2File(sJH, filePath);
                }

                this.lbxTracksCollection.Items.Add("解释结论");
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }

        private void btnAddLayerDepth_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionTemp.sDirSectionData, sJH + "\\layerDepth.txt");
                    cIOinputLayerDepth cSelectLayerDepth = new cIOinputLayerDepth();
                    cSelectLayerDepth.selectSectionDrawData2File(sJH, filePath);
                }
                this.lbxTracksCollection.Items.Add("地层");
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
         
        }

        private void btnAddLogTrack_Click(object sender, EventArgs e)
        {
            WaitWindow.Show(this.generateLogFile);
            this.lbxTracksCollection.Items.Add("测井曲线道");
        }
        private void generateLogFile(object sender, WaitWindowEventArgs e)
        {
            cProjectData.sErrLineInfor= "";
            generateDrawLogFile(fileDrawSourceLeftLog, this.iWellIntervalDistanceNameLeft.SelectedItem.ToString(), this.iWellIntervalDistancecorlorLeft.BackColor.Name);
            generateDrawLogFile(fileDrawSourceRightLog, this.iWellIntervalDistanceNameRight.SelectedItem.ToString(), this.iWellIntervalDistancecorlorRight.BackColor.Name);
            if (cProjectData.sErrLineInfor== "")
            {
                MessageBox.Show("完成曲线道");
            }
            else
            {
                MessageBox.Show("数据有一些缺失，请点击查看。");
                cPublicMethodForm.outputErrInfor2Text(cProjectData.sErrLineInfor);
            }
        }
        private void generateDrawLogFile(string sLogFile, string sLogName, string sCurveColor)
        {
            try
            {
                StreamWriter sw = new StreamWriter(sLogFile, false, Encoding.UTF8);
                string sReturnLog = "JH" + "\t" + "MD" + "\t" + sLogName + "\r\n";
                for (int i = 0; i < this.ltStrSelectedJH.Count; i++)
                {

                    string sItemJh = this.ltStrSelectedJH[i];
                    float fItemDS1Showed = this.fListDS1Showed[i];
                    float fItemDS2Showed = this.fListDS2Showed[i];

                    string filePathLog = cProjectManager.dirPathLog + sItemJh + "_$#log";
                    
                    if (File.Exists(filePathLog) == true)
                    {
                        int iLogSeriersIndex = 0;
                        
                        using (StreamReader sr = new StreamReader(filePathLog, Encoding.Default))
                        {
                            string line;
                            int iLine = 0;
                            while ((line = sr.ReadLine()) != null) //delete the line whose legth is 0
                            {
                                iLine++;
                                string[] split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);

                                if (iLine == 1)
                                {
                                    iLogSeriersIndex = split.ToList().IndexOf(sLogName);
                                }
                                else if (iLine %2==0)
                                {
                                    float fCurrent = float.Parse(split[0]);
                                    if (iLogSeriersIndex > 0)
                                    {
                                        if (fItemDS1Showed <= fCurrent && fCurrent <= fItemDS2Showed)
                                        {
                                            sReturnLog +=sItemJh+"\t"+split[0] + '\t' + split[iLogSeriersIndex] + "\r\n";
                                        }
                                    }

                                }

                            }
                        }
                        
                    }
                    else
                    {
                        cProjectData.sErrLineInfor+= sItemJh + "无测井数据";
                    }

                }
                sw.Write(sReturnLog);
                sw.Close();

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }

        }
        private void iWellIntervalDistancecorlorLeft_MouseClick(object sender, MouseEventArgs e)
        {
            ColorDialog CDG = new ColorDialog();
   
            if (CDG.ShowDialog() == DialogResult.OK)
            {
                this.iWellIntervalDistancecorlorLeft.BackColor = CDG.Color;
            }  
        }
        private void iWellIntervalDistancecorlorRight_MouseClick(object sender, MouseEventArgs e)
        {
            ColorDialog CDG = new ColorDialog();
            if (CDG.ShowDialog() == DialogResult.OK)
            {
                this.iWellIntervalDistancecorlorRight.BackColor = CDG.Color;
            }
            //MessageBox.Show(iWellIntervalDistancecorlorRight.BackColor.R.ToString());
            //MessageBox.Show(iWellIntervalDistancecorlorRight.BackColor.G.ToString());
            //MessageBox.Show(iWellIntervalDistancecorlorRight.BackColor.B.ToString());
        }

        private void btnAddPerforation_Click(object sender, EventArgs e)
        {
       
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionTemp.sDirSectionData, sJH + "\\inputPerforation.txt");
                    cIOinputPerforation cSelectInputPerforation = new cIOinputPerforation();
                    cSelectInputPerforation.selectSectionDraData2File(sJH, filePath);
                }
                this.lbxTracksCollection.Items.Add("射孔");
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }
       
        private void lbxJHSeclected_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ltStrSelectedJH.Clear();
            foreach (object selecteditem in lbxJHSeclected.Items)
            {
                string strItem = selecteditem as String;
                ltStrSelectedJH.Add(strItem);
            }

        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            lbxJHSeclected.Items.Clear();
        }

        private void btnScale_Click(object sender, EventArgs e)
        {
            cProjectData.dfMapScale =cProjectData.dfMapScale* 1.5F;
            //generateSectionDrawData();
            MessageBox.Show("当前比例尺:" + cProjectData.dfMapScale.ToString());
        }

        private void tabControlFenceDiagram_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tclWellsGroup.SelectedIndex == 1 && File.Exists(cProjectManager.xmlConfigFenceDiagram )== false)
            { cXMLFenceDiagram.creatFenceDiagramSettingXML(cProjectManager.xmlConfigFenceDiagram); }
        }

    
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.inialListBox(lbxJHSeclected, cProjectData.ltStrProjectJH);
        }

        private void FormMapFence_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

    }
}
