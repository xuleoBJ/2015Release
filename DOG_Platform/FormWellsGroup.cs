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
        string dirSectionData = Path.Combine(cProjectManager.dirPathTemp, "sectionGroupTemp");
        List<string> ltStrSelectedJH = new List<string>();  //联井剖面井号
        //存储绘图剖面数据结构
        List<cWellSectionSVG> listWellsSection = new List<cWellSectionSVG>();

       
        public FormWellsGroup()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            InitFormWellsGroupControl();
        }
        private void InitFormWellsGroupControl()
        {
            cPublicMethodForm.inialListBox(lbxJH, cProjectData.ltStrProjectJH);
            cPublicMethodForm.inialComboBox(cbbTopXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbBottomXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbLeftLogName, cProjectData.ltStrLogSeriers);
            cPublicMethodForm.inialComboBox(cbbRightLogName, cProjectData.ltStrLogSeriers);
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
        void initializeTreeViewWellCollection()
        {
            this.lbxTracksCollection.Items.Clear();
            this.tvwWellSectionCollection.Nodes.Clear();
            for (int i = 0; i < ltStrSelectedJH.Count; i++) tvwWellSectionCollection.Nodes.Add(ltStrSelectedJH[i]);
        }
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
                initializeTreeViewWellCollection();
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
                cXDocSection.generateSectionCssXML();
                generateSectionDataDirectory();
            }
            else
            {
                MessageBox.Show("上层应该比下层选择高，请重新选择。");
            }
        }

        void generateSectionDataDirectory()
        {
            if (Directory.Exists(dirSectionData)) Directory.Delete(dirSectionData, true);
            Directory.CreateDirectory(dirSectionData);
            foreach (cWellSectionSVG item in listWellsSection)
            {
                string jhDir = Path.Combine(dirSectionData, item.sJH);
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
        
        private void lbxJHSeclected_SelectedIndexChanged(object sender, EventArgs e)
        {
            ltStrSelectedJH.Clear();
            foreach (object selecteditem in lbxJHSeclected.Items)
            {
                string strItem = selecteditem as String;
                ltStrSelectedJH.Add(strItem);
            }
        }


        void generateSectionGraph(string filenameSVGMap)
        {

            cSVGDocSection cSection = new cSVGDocSection(4000, 4000, 0, 0);
            cSection.addSVGTitle(string.Join("-", listWellsSection.Select(p => p.sJH).ToList()) + "栅状图", 100, 100);

            XmlElement returnElemment;
            for (int i = 0; i < listWellsSection.Count; i++)
            {
                string sJH = listWellsSection[i].sJH;
                Point currentPositon = cCordinationTransform.getPointViewByJH(sJH);
                List<ItemWellPath> currentWellPathList = cProjectData.listProjectWell.Find(p => p.sJH == sJH).WellPathList;
                float fTopShowed = listWellsSection[i].fShowedDepthTop;
                float fBaseShowed = listWellsSection[i].fShowedDepthBase;
                float fDepthFlatted = listWellsSection[i].fShowedDepthTop;
                cSVGSectionWell currentWell = new cSVGSectionWell(sJH);
                if (currentWellPathList.Count <= 2)
                    returnElemment = currentWell.gWellCone(sJH, fTopShowed, fBaseShowed, fDepthFlatted, 10, 5);
                else
                {
                    returnElemment = currentWell.gPathWellCone(sJH, fTopShowed, fBaseShowed, fDepthFlatted, 10, 5);
                }
                currentWell.addTrack(returnElemment, 0);

                //增加地层道
                string filePathLayer = Path.Combine(dirSectionData, sJH + "\\layerDepth.txt");
                trackLayerDepthDataList trackDataListLayerDepth =
                    cDirDataSourceWellSection.setupDataListTrackLayerDepth(filePathLayer, fTopShowed, fBaseShowed);
                int iTrackWidth = 15;
                cSVGSectionTrackLayer layerTrack = new cSVGSectionTrackLayer(iTrackWidth);
                layerTrack.iTextSize = 6;
                if (currentWellPathList.Count <= 2)
                    returnElemment = layerTrack.gTrackLayerDepth(sJH, trackDataListLayerDepth, fDepthFlatted);
                else returnElemment = layerTrack.gPathTrackLayerDepth(sJH, trackDataListLayerDepth, fDepthFlatted);
                currentWell.addTrack(returnElemment, iTrackWidth);

                //增加解释结论道
                string filePathJSJL = Path.Combine(dirSectionData, sJH + "\\jsjl.txt");
                trackJSJLDataList trackDataListJSJL = cDirDataSourceWellSection.setupDataListTrackJSJL(filePathJSJL, fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackJSJL JSJLTrack = new cSVGSectionTrackJSJL(iTrackWidth);
                if (currentWellPathList.Count <= 2)
                    returnElemment = JSJLTrack.gTrackJSJL(sJH, trackDataListJSJL, fDepthFlatted);
                else returnElemment = JSJLTrack.gPathTrackJSJL(sJH, trackDataListJSJL, fDepthFlatted);
                currentWell.addTrack(returnElemment, -iTrackWidth);

                ////增加射孔道
                //string filePathInputPerforation = Path.Combine(dirSectionData, sJH + "\\inputPerforation.txt");
                //trackInputPerforationDataList trackDataListPerforation = cDirDataSourceWellSection.setupDataListTrackPerforation(filePathInputPerforation, fTopShowed, fBaseShowed);
                //iTrackWidth = 15;
                //cSVGSectionTrackPeforation perforationTrack = new cSVGSectionTrackPeforation(iTrackWidth);
                //if (currentWellPathList.Count <= 2)
                //    returnElemment = perforationTrack.gTrackPerforation(sJH, trackDataListPerforation, fDepthFlatted);
                //else returnElemment = JSJLTrack.gXieTrackJSJL(sJH, trackDataListJSJL, fDepthFlatted);
                //currentWell.addTrack(returnElemment, -2 * iTrackWidth);

                //增加左边曲线
                string fileLeftLogScrPath = Path.Combine(dirSectionData, sJH + "\\left");
                string[] fileList = Directory.GetFileSystemEntries(fileLeftLogScrPath);
                foreach (string fileLog in fileList)
                {
                    trackLogDataList trackDataListLeftLog = cDirDataSourceWellSection.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;
                    ItemLogHeadInfor itemHeadInfor = new ItemLogHeadInfor();
                    itemHeadInfor.sJH = sJH;
                    itemHeadInfor.sLogName = trackDataListLeftLog.sLogName;
                    itemHeadInfor.sLogColor = cPublicMethodBase.getRGB(cbbColorLeftLog.BackColor);
                    itemHeadInfor.fRightValue = Convert.ToSingle(nUDLeftLogRightValue.Value);
                    itemHeadInfor.fLeftValue = Convert.ToSingle(this.nUDLeftLogLeftValue.Value);
                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    if (currentWellPathList.Count <= 2)
                        returnElemment = logTrack.gTrackLog(itemHeadInfor, trackDataListLeftLog, fDepthFlatted);
                    else returnElemment = logTrack.gPathTrackLog(itemHeadInfor, trackDataListLeftLog, fDepthFlatted);
                    currentWell.addTrack(returnElemment, -30);
                }
                //增加右边曲线
                string fileRightLogScrPath = Path.Combine(dirSectionData, sJH + "\\right");
                foreach (string fileLog in Directory.GetFileSystemEntries(fileRightLogScrPath))
                {
                    trackLogDataList trackDataListRightLog = cDirDataSourceWellSection.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;
                    ItemLogHeadInfor itemHeadInfor = new ItemLogHeadInfor();
                    itemHeadInfor.sJH = sJH;
                    itemHeadInfor.sLogName = trackDataListRightLog.sLogName;
                    itemHeadInfor.sLogColor = cPublicMethodBase.getRGB(cbbColorRightLog.BackColor);
                    itemHeadInfor.fRightValue = Convert.ToSingle(this.nUDRightLogRightValue.Value);
                    itemHeadInfor.fLeftValue = Convert.ToSingle(this.nUDRightLogLeftValue.Value);
                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    if (currentWellPathList.Count <= 2)
                        returnElemment = logTrack.gTrackLog(itemHeadInfor, trackDataListRightLog, fDepthFlatted);
                    else returnElemment = logTrack.gPathTrackLog(itemHeadInfor, trackDataListRightLog, fDepthFlatted);
                    currentWell.addTrack(returnElemment, iTrackWidth);
                }
                cSection.addgElement(currentWell.gWell, currentPositon);
            }


            string fileSVG = Path.Combine(cProjectManager.dirPathMap, filenameSVGMap);
            cSection.makeSVGfile(fileSVG);
            FormMain.filePathWebSVG = fileSVG;
            this.Close();

        }
     
        private void btnMakeSection_Click(object sender, EventArgs e)
        {
            string filenameSVGMap;
            if (this.tbxTitle.Text == "")
            {
                if (ltStrSelectedJH.Count < 6) filenameSVGMap ="井组分析_"+ string.Join("-", ltStrSelectedJH.ToArray()) + ".svg";
                else filenameSVGMap = "井组分析_" + string.Join("-", ltStrSelectedJH.GetRange(0, 5)) + ".svg";
            }
            else
            {
                filenameSVGMap = this.tbxTitle.Text + ".svg";
            }
            generateSectionGraph(filenameSVGMap);
        }

        private void btnAddJSJLTrack_Click(object sender, EventArgs e)
        {
            if (this.listWellsSection.Count > 0)
            {
                //提取所选井段数据存入绘图目录下保存
                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionData, sJH, "jsjl.txt");
                    cIOinputJSJL.selectSectionDrawData2File(sJH, filePath);
                }
                this.lbxTracksCollection.Items.Add("解释结论");
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
                {
                    if (wellNote.Text != "深度尺")
                        wellNote.Nodes.Add("解释结论");
                }

                tvwWellSectionCollection.ExpandAll();
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }

        private void btnAddLayerDepth_Click(object sender, EventArgs e)
        {

            if (this.listWellsSection.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionData, sJH + "\\layerDepth.txt");
                    cIOinputLayerDepth cSelectLayerDepth = new cIOinputLayerDepth();
                    cSelectLayerDepth.selectSectionDrawData2File(sJH, filePath);
                }

                this.lbxTracksCollection.Items.Add("地层");

                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes) wellNote.Nodes.Add("地层");

                tvwWellSectionCollection.ExpandAll();
            }
            else MessageBox.Show("请先确认深度段。");
         
        }

        private void btnAddLogTrack_Click(object sender, EventArgs e)
        {
           

        }
    

        private void btnAddPerforation_Click(object sender, EventArgs e)
        {
            if (this.listWellsSection.Count > 0)
            {
                this.lbxTracksCollection.Items.Add("射孔");
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes) wellNote.Nodes.Add("射孔");
                tvwWellSectionCollection.ExpandAll();
            }
            else MessageBox.Show("请先确认深度段。");
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

        private void btnLogTrackAddLeft_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0 && cbbLeftLogName.SelectedIndex >= 0)
            {
                string sSelectedLogName = this.cbbLeftLogName.SelectedItem.ToString();
                addLogData((int)LeftOrRight.left, sSelectedLogName);
            }
            else MessageBox.Show("请先确认深度段。");
        }

        private void btnLogTrackAddRight_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0 && cbbRightLogName.SelectedIndex >= 0)
            {
                string sSelectedLogName = this.cbbRightLogName.SelectedItem.ToString();
                addLogData((int)LeftOrRight.right, sSelectedLogName);
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
            this.lbxTracksCollection.Items.Add("测井曲线道");
        }
    
        void addLogData(int iLeftOrRight, string sSelectedLogName)
        {
            foreach (string sJH in ltStrSelectedJH)
            {
                string filePath = Path.Combine(dirSectionData, sJH + "\\left\\" + sSelectedLogName + ".txt");
                if (iLeftOrRight == (int)LeftOrRight.right)
                {
                    filePath = Path.Combine(dirSectionData, sJH + "\\right\\" + sSelectedLogName + ".txt");
                }
                cIOinputLog.extractTextLog2File(sJH, sSelectedLogName, filePath);
            }
            foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
            {
                TreeNode tnLog = new TreeNode();
                tnLog.Text = sSelectedLogName;
                if (wellNote.Index > 0)
                {
                    wellNote.Nodes.Add(tnLog);
                }

            }
            tvwWellSectionCollection.ExpandAll();
        }

        private void cbbColorLeftLog_MouseClick(object sender, MouseEventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbColorLeftLog);
        }

        private void cbbColorRightLog_MouseClick(object sender, MouseEventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbColorRightLog);
        }

        private void tsCbbScale_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnBig_Click(object sender, EventArgs e)
        {
            cProjectData.dfMapScale = cProjectData.dfMapScale * 1.2F;
        }

    }
}
