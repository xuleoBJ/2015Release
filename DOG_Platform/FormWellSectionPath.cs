using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using DOGPlatform.XML;
using DOGPlatform.SVG;

namespace DOGPlatform
{
    public partial class FormWellSectionPath : Form
    {
        string dirSectionData = Path.Combine(cProjectManager.dirPathTemp, "sectionResTemp");
        List<string> ltStrSelectedJH = new List<string>();  //联井剖面井号
        //存储绘图剖面数据结构
        List<cWellSectionSVG> listWellsSection = new List<cWellSectionSVG>();
     
          public FormWellSectionPath()
        {
            InitializeComponent();
            InitFormWellsGroupControl();
        }

        private void InitFormWellsGroupControl()
        {
            cPublicMethodForm.inialListBox(lbxJH, cProjectData.ltStrProjectJH);
            cPublicMethodForm.inialComboBox(cbbTopXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbBottomXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbLeftLogName, cProjectData.ltStrLogSeriers);
            cPublicMethodForm.inialComboBox(cbbRightLogName, cProjectData.ltStrLogSeriers);
            List<string> ltStrWellArrageItem = new List<string>();
            ltStrWellArrageItem.Add("等距排列");
            ltStrWellArrageItem.Add("按实际间距");
            cPublicMethodForm.inialComboBox(cbbWellArrangeMode, ltStrWellArrageItem);
        }
        private void btn_deleteWell_Click(object sender, EventArgs e)
        {

            if (lbxJHSeclected.SelectedItem != null)
            {
                string sWellItem = lbxJHSeclected.SelectedItem.ToString();
                lbxJHSeclected.Items.Remove(sWellItem);
            }
            if (lbxJHSeclected.Items.Count>0)
            lbxJHSeclected.SetSelected(lbxJHSeclected.Items.Count - 1, true);


        }

        private void btn_addWell_Click(object sender, EventArgs e)
        {
            string sWellItem = "";
            if (lbxJH.SelectedIndex >= 0)
            {
                sWellItem = lbxJH.SelectedItem.ToString();
                if (lbxJHSeclected.Items.IndexOf(sWellItem) < 0)
                    lbxJHSeclected.Items.Add(sWellItem);
                lbxJHSeclected.SetSelected(lbxJHSeclected.Items.Count - 1, true);
            }
            else
            {
                MessageBox.Show("请从左侧点选井号添加入剖面图");
            }
        }

        private void btn_upWell_Click(object sender, EventArgs e)
        {//若不是第一行则上移

            if (lbxJHSeclected.SelectedIndex > 0)
            {
                int index = lbxJHSeclected.SelectedIndex;
                string temp = lbxJHSeclected.Items[index - 1].ToString();
                lbxJHSeclected.Items[index - 1] = lbxJHSeclected.SelectedItem.ToString(); ;
                lbxJHSeclected.Items[index] = temp;
                lbxJHSeclected.SelectedIndex = index - 1;
            }
        }

        private void btn_downWell_Click(object sender, EventArgs e)
        {
            if (lbxJHSeclected.SelectedIndex < lbxJHSeclected.Items.Count - 1)
            {
                //若不是第最后一行则下移
                int index = lbxJHSeclected.SelectedIndex;
                string temp = lbxJHSeclected.Items[index + 1].ToString();
                lbxJHSeclected.Items[index + 1] = lbxJHSeclected.SelectedItem.ToString(); ;
                lbxJHSeclected.Items[index] = temp;
                lbxJHSeclected.SelectedIndex = index + 1;
            }
        }

        //private void setDepthIntervalShowed(object sender, WaitWindowEventArgs e)
        //{
        //    List<string> ltStrSelectedXCM = new List<string>();
        //    this.lbxTracksCollection.Items.Clear();
        //    updateSectionMapSourceFile();
        //    this.tvwWellSectionCollection.Nodes.Clear();
        //    //this.tvwWellSectionCollection.Nodes.Add("深度尺");
        //    string sTopXCM = this.cbbTopXCM.SelectedItem.ToString();
        //    int iTopIndex = cProjectManager.ltStrProjectXCM.IndexOf(sTopXCM);
        //    string sBottomXCM = this.cbbBottomXCM.SelectedItem.ToString();
        //    int iBottomIndex = cProjectManager.ltStrProjectXCM.IndexOf(sBottomXCM);
        //    if (iBottomIndex - iTopIndex >= 0)
        //    {
        //        fListDS1Showed.Clear();
        //        fListDS2Showed.Clear();
        //        ltStrSelectedXCM = cProjectManager.ltStrProjectXCM.GetRange(iTopIndex, iBottomIndex - iTopIndex + 1);

        //        int _up = Convert.ToInt16( this.nUDtopDepthUp.Value);
        //        int _down = Convert.ToInt16( this.nUDbottomDepthDown.Value);

        //        for (int i = 0; i < ltStrSelectedJH.Count; i++)
        //        {
        //            tvwWellSectionCollection.Nodes.Add(ltStrSelectedJH[i]);
        //            cSelect4WellSection cSelectTest = new cSelect4WellSection();
        //            List<float> fListDS1Return = cSelectTest.selectTopDepthListFromLayerDepthByJHAndXCMList(ltStrSelectedJH[i], ltStrSelectedXCM);
        //            if (fListDS1Return.Count > 0)  //返回值为空 说明所选层段整个缺失！
        //            {
        //                fListDS1Showed.Add(fListDS1Return.Min() - _up);
        //                fListDS2Showed.Add(fListDS1Return.Max() + _down);
        //            }
        //            else 
        //            {
        //                fListDS1Showed.Add(0);
        //                fListDS2Showed.Add(0);
        //            }
        //        }
        //        generateSectionDrawData();
        //    }
        //    else
        //    {
        //        MessageBox.Show("上层应该比下层选择高，请重新选择。");
        //    }
       
         
        //}


        void initializeTreeViewWellCollection()
        {
            this.lbxTracksCollection.Items.Clear();

            this.tvwWellSectionCollection.Nodes.Clear();
            this.tvwWellSectionCollection.Nodes.Add("深度尺");
            for (int i = 0; i < ltStrSelectedJH.Count; i++)
            {
                tvwWellSectionCollection.Nodes.Add(ltStrSelectedJH[i]);
            }
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

        private void btnGenerteDataByLayerName_Click(object sender, EventArgs e)
        {
            setDepthIntervalShowedBYLayer();
        }

        private void tbxTopInput_TextChanged(object sender, EventArgs e)
        {
            cPublicMethodForm.inputTextBoxIntOnly(this.tbxTopElevationInput);
        }

        private void tbxBottomInput_TextChanged(object sender, EventArgs e)
        {
            cPublicMethodForm.inputTextBoxIntOnly(this.tbxBottomElevationInput);
        }

        private void setDepthIntervalShowedBYElevationDepth()
        {
            updateSelectedListJH();
            int iTopElevation = int.Parse(this.tbxTopElevationInput.Text);
            int iBottomElevation = int.Parse(this.tbxBottomElevationInput.Text);

            initializeTreeViewWellCollection();
            for (int i = 0; i < ltStrSelectedJH.Count; i++)
            {
                cWellSectionSVG _wellSection = new cWellSectionSVG(ltStrSelectedJH[i], 0, 0);
                //海拔转成md
                _wellSection.fShowedDepthTop = _wellSection.fKB - iTopElevation;
                _wellSection.fShowedDepthBase = _wellSection.fKB - iBottomElevation;
                listWellsSection.Add(_wellSection);
            }
            cXDocSection.generateSectionCssXML();
            generateSectionDataDirectory();
        }

        private void btnGenerateDataByInputDepth_Click(object sender, EventArgs e)
        {
         setDepthIntervalShowedBYElevationDepth();
        }

        private void FormWellSectionGraph_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
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

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(cProjectManager.dirPathTemp));
        }

        private void btnAddTextTrack_Click(object sender, EventArgs e)
        {

       

        }

        private void btnAddLayerDepth_Click(object sender, EventArgs e)
        {
         
            if ( this.listWellsSection .Count > 0)
            {

                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionData, sJH + "\\layerDepth.txt");
                    cIOinputLayerDepth cSelectLayerDepth = new cIOinputLayerDepth();
                    cSelectLayerDepth.selectSectionDrawData2File(sJH, filePath);
                }
                this.lbxTracksCollection.Items.Add("地层");

                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes) 
                {
                    if(wellNote.Text!="深度尺")
                    wellNote.Nodes.Add("地层");
                }

                tvwWellSectionCollection.ExpandAll();
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
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

        private void cbbLeftLogColor_MouseClick(object sender, MouseEventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbColorLeftLog);
        }

        private void cbbRightLogColor_MouseClick(object sender, MouseEventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbColorRightLog);
        }

      
      
        private void generateDrawLogFile(string sLogFile, string sLogName, string sCurveColor)
        {
            

        }
        private void btnAddLogTrack_Click(object sender, EventArgs e)
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

        private void btnMakeSection_Click(object sender, EventArgs e)
        {
            string filenameSVGMap;
            if (this.tbxTitle.Text == "")
            {
                filenameSVGMap = string.Join("-", ltStrSelectedJH.ToArray()) + "-section.svg";
            }
            else
            {
                filenameSVGMap = this.tbxTitle.Text + ".svg";
            }
            generateSectionGraph(filenameSVGMap);
          
        }

         void generateSectionGraph( string filenameSVGMap)
        {
            //继续初始化值
            for (int i = 0; i < this.listWellsSection.Count; i++)
            {
                cWellSectionSVG itemWell = listWellsSection[i];
                itemWell.fDepthFlatted = itemWell.fKB;
                if (i == 0)
                {
                    itemWell.fXview = 100F;
                }
                else
                {
                        Point pointConvert2View = cCordinationTransform.getPointViewByWellName(ltStrSelectedJH[i - 1]);
                        Point pointWell0Convert2View = cCordinationTransform.getPointViewByWellName(ltStrSelectedJH[i]);
                        int iDistance = Convert.ToInt16(c2DGeometryAlgorithm.calDistance2D(pointConvert2View, pointWell0Convert2View));
                        itemWell.fXview = listWellsSection[i - 1].fXview + iDistance;
                }
            }

            cSVGDocSection cSection = new cSVGDocSection(2000, 5000, 0, 0);
            cSection.addSVGTitle(string.Join("-", listWellsSection.Select(p => p.sJH).ToList()) + "剖面图", 100, 100);

            XmlElement returnElemment;

            //增加海拔尺
            int upDepthElevationRuler = 3000;
            int downDepthElevationRuler = -10000;
            int iScaleElevationRuler = 50;
            cSVGSectionTrackElevationRuler cElevationRuler = new cSVGSectionTrackElevationRuler();
            returnElemment = cElevationRuler.gElevationRuler(downDepthElevationRuler, upDepthElevationRuler, iScaleElevationRuler);
            cSection.addgElement(returnElemment, 0);
 
            for (int i = 0; i < listWellsSection.Count; i++)
            {
                string sJH = listWellsSection[i].sJH;

                List<ItemWellPath> currentWellPathList = cProjectData.listProjectWell.Find(p => p.sJH == sJH).WellPathList;
                float fTopShowed = listWellsSection[i].fShowedDepthTop;
                float fBaseShowed = listWellsSection[i].fShowedDepthBase;
                float fDepthFlatted = listWellsSection[i].fDepthFlatted;
                int iCurrerntWellHorizonPotion = Convert.ToInt16(listWellsSection[i].fXview);
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
             //   cFileOperateDicLogHeadProject fileDicLog = new cFileOperateDicLogHeadProject();

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
                cSection.addgElement(currentWell.gWell, iCurrerntWellHorizonPotion);
            }

            bool bConnect = this.cbxConnectSameLayerName.Checked;

            if (bConnect == true)
            {
                for (int i = 0; i < listWellsSection.Count - 1; i++)
                {
                    string filePathLayer = Path.Combine(dirSectionData, listWellsSection[i].sJH + "\\layerDepth.txt");
                    trackLayerDepthDataList well1LayerDepthDataList = cDirDataSourceWellSection.setupDataListTrackLayerDepth(filePathLayer, listWellsSection[i].fShowedDepthTop, listWellsSection[i].fShowedDepthBase);
                    string file2PathLayer = Path.Combine(dirSectionData, listWellsSection[i + 1].sJH + "\\layerDepth.txt");
                    trackLayerDepthDataList well2LayerDepthDataList = cDirDataSourceWellSection.setupDataListTrackLayerDepth(file2PathLayer, listWellsSection[i + 1].fShowedDepthTop, listWellsSection[i + 1].fShowedDepthBase);
                    cSVGSectionTrackConnect layerConnect = new cSVGSectionTrackConnect();
                    returnElemment = layerConnect.addgConnectLayerTrack
                     (listWellsSection[i], well1LayerDepthDataList, listWellsSection[i + 1], well2LayerDepthDataList);
                    cSection.addgElement(returnElemment, 0);
                }

            }

            string fileSVG = Path.Combine(cProjectManager.dirPathMap, filenameSVGMap);
            cSection.makeSVGfile(fileSVG);
            FormMain.filePathWebSVG = fileSVG;
            this.Close(); 

        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            saveSectionConfigXML();
        }

        void saveSectionConfigXML() 
        {
            string SectionConfigXMLPath = cProjectManager.dirPathTemp + "SectionConfig.XML";

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", null);
            doc.AppendChild(dec);
            //创建一个根节点（一级）
            XmlElement root = doc.CreateElement("SectionGraphConfig");
            doc.AppendChild(root);

            XmlElement ele;
            ele = doc.CreateElement("ElevationRuler");

            XmlNode  node;
            node = doc.CreateElement("topElevationDepth");
            node.InnerText = "3000";
            ele.AppendChild(node);
            node = doc.CreateElement("bottomElevationDepth");
            node.InnerText = "0";
            ele.AppendChild(node);

            node = doc.CreateElement("MainScale");
            node.InnerText = "50";
            ele.AppendChild(node);

            XmlNode offset;
            offset = doc.CreateElement("offset");

            node = doc.CreateElement("Xoffset");
            node.InnerText = "-30";
            ele.AppendChild(node);
            node = doc.CreateElement("Yoffset");
            node.InnerText = "0";
            ele.AppendChild(node);

            ele.AppendChild(offset);

            root.AppendChild(ele);

 
            doc.Save(SectionConfigXMLPath);
        }

        private void btnAddElevationRuler_Click(object sender, EventArgs e)
        {
            this.lbxTracksCollection.Items.Add("海拔尺");
        }

        private void iWellIntervalDistancecorlorRight_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnAddPerforatedTrack_Click(object sender, EventArgs e)
        {
            if (this.listWellsSection.Count > 0)
            {
                
                this.lbxTracksCollection.Items.Add("射孔");

                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
                {
                    if (wellNote.Text != "深度尺")
                        wellNote.Nodes.Add("射孔");
                }

                tvwWellSectionCollection.ExpandAll();
            }
            else MessageBox.Show("请先确认深度段。");
      
        }
        private void btnMakeSectionxmlConfig_Click(object sender, EventArgs e)
        {

        }
        private void btnMakeSectionByxmlConfig_Click(object sender, EventArgs e)
        {
         
        }

        private void btnAddLithoTrack_Click(object sender, EventArgs e)
        {
            string strSelectedFileName = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文件|*.txt|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                strSelectedFileName = openFileDialog.FileName;
                using (StreamReader sr = new StreamReader(strSelectedFileName, Encoding.Default))
                {

                    this.lbxTracksCollection.Items.Add("岩性道");
                    MessageBox.Show("岩性数据准备完毕。");
                }

            }
            else
            {
                MessageBox.Show("请选重新选择岩性数据", "提示");
            }
        }
        private void btnSandBody_Click(object sender, EventArgs e)
        {
            string filenameSVGMap = "Sandlayer.svg";

            cSVGDocSection cSection = new cSVGDocSection( 800,1000,0, 0);
            //XmlElement returnElemment;
            string dPath = "M50,50 Q50,100 100,100z";
            cSection.addgElement(cSection.gSandBody(dPath), 200);

            dPath = "M300,300 v-50  h80 v50z";
            cSection.addgElement(cSection.gSandBody(dPath), 200);

             dPath ="M130 110 C 120 140, 180 140, 170 110 Z";
            cSection.addgElement(cSection.gSandBody(dPath), 300);

            dPath = "M10 10 C 20 20, 40 20, 50 10 Z";
            cSection.addgElement(cSection.gSandBody(dPath), 400);

            dPath = "M70 10 C 70 20, 120 20, 120 10 z";
            cSection.addgElement(cSection.gSandBody(dPath), 300);

            dPath = "M10 60 C 20 80, 40 80, 50 60 Z";
            cSection.addgElement(cSection.gSandBody(dPath), 300);

            dPath = "M70 60 C 70 80, 110 80, 110 60 Z";
            cSection.addgElement(cSection.gSandBody(dPath), 300);

            dPath = "M130 60 C 120 80, 180 80, 170 60Z";
            cSection.addgElement(cSection.gSandBody(dPath), 300);

            dPath = "M10 110 C 20 140, 40 140, 50 110Z";
            cSection.addgElement(cSection.gSandBody(dPath), 300);

            dPath = "M70 110 C 70 140, 110 140, 110 110z";
            cSection.addgElement(cSection.gSandBody(dPath), 300);

            dPath = "M130 110 C 120 140, 180 140, 170 110z";
            cSection.addgElement(cSection.gSandBody(dPath), 300);

            dPath = "M10 80 C 40 10, 65 10, 95 80 S 150 150, 180 80 z";
            cSection.addgElement(cSection.gSandBody(dPath), 300);


            cSection.makeSVGfile(cProjectManager.dirPathMap + filenameSVGMap);
             FormWebNavigation formSVGView = new FormWebNavigation(cProjectManager.dirPathMap + filenameSVGMap);formSVGView.Show();
        }
        private void nUDElevationScale_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("ElevationRuler").Element("MainScale").Value = nUDElevationScale.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDElevationFontSize_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("ElevationRuler").Element("tickFontSize").Value = nUDElevationFontSize.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDElevationRulerTop_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("ElevationRuler").Element("topElevationDepth").Value = nUDElevationRulerTop.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDElevationRulerBottom_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("ElevationRuler").Element("bottomElevationDepth").Value = nUDElevationRulerBottom.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDTextTrackWidth_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("TextTrack").Element("trackWidth").Value = nUDTextTrackWidth.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDTextTrackFontSize_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("TextTrack").Element("textFontSize").Value = nUDTextTrackFontSize.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDLayerTrackWidth_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("LayerTrack").Element("trackWidth").Value = nUDLayerTrackWidth.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDLayerTrackFontSize_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("LayerTrack").Element("textFontSize").Value = nUDLayerTrackFontSize.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDWellConeWidth_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("WellCone").Element("coneWidth").Value = nUDWellConeWidth.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDWellConeCircle_R_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("WellCone").Element("radisHeadCircle").Value = nUDWellConeCircle_R.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDWellConeFontSize_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("WellCone").Element("tickTextFontSize").Value = nUDWellConeFontSize.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDWellConeScale_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("WellCone").Element("MainScale").Value = nUDWellConeScale.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDWellConeJHFontSize_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("WellCone").Element("JHFontSize").Value = nUDWellConeJHFontSize.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void cbxFillLayer_CheckedChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            if (cbxFillLayer.Checked == true)
                sectionMapXML.Element("SectionMap").Element("LayerTrack").Element("autoFillColor").Value = "1";
            else
                sectionMapXML.Element("SectionMap").Element("LayerTrack").Element("autoFillColor").Value ="0";
       
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUMScaleVertical_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("Scale").Element("vScale").Value = (1000 / nUMScaleVertical.Value).ToString("0.0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDLithoTrackWidth_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("LithoTrack").Element("trackWidth").Value = nUDLithoTrackWidth.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDPerforateTrack_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("PerforationTrack").Element("trackWidth").Value = nUDPerforateTrack.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDJSJLTrackWidth_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("JSJLTrack").Element("trackWidth").Value = nUDJSJLTrackWidth.Value.ToString("0");
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void btnConnectLayer_Click(object sender, EventArgs e)
        {
            if (this.listWellsSection.Count > 0)
            {
                this.lbxTracksCollection.Items.Add("地层");
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }
        private void cbbCurveColor_leftLog_MouseClick(object sender, MouseEventArgs e)
        {   
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.cbbCurveColor_leftLog.BackColor = colorDialog1.Color;
                string m_sColorCurve = cPublicMethodForm.getRGB(colorDialog1.Color);
                XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
                sectionMapXML.Element("SectionMap").Element("LeftLogTrack").Element("colorCurve").Value = m_sColorCurve;
                sectionMapXML.Save(cProjectManager.xmlConfigSection);
            }  
        }
        private void cbbCurveColor_rightLog_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                cbbCurveColor_rightLog.BackColor = colorDialog1.Color;
                string m_sColorCurve = cPublicMethodForm.getRGB(colorDialog1.Color);
                XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
                sectionMapXML.Element("SectionMap").Element("RightLogTrack").Element("colorCurve").Value = m_sColorCurve;
                sectionMapXML.Save(cProjectManager.xmlConfigSection);
            }  
        }
        private void nUDLeftValue_leftLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("LeftLogTrack").Element("leftValue").Value = nUDLeftValue_leftLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDRightValue_leftLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("LeftLogTrack").Element("rightValue").Value = nUDRightValue_leftLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDLineWidth_leftLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("LeftLogTrack").Element("curveLineWidth").Value = nUDLineWidth_leftLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDExtractPoints_leftLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("LeftLogTrack").Element("intervalPiontNumber").Value = nUDExtractPoints_leftLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDTrackWidth_leftLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("LeftLogTrack").Element("trackWidth").Value = nUDTrackWidth_leftLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDLeftValue_rightLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("RightLogTrack").Element("leftValue").Value = nUDLeftValue_rightLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDRightValue_rightLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("RightLogTrack").Element("rightValue").Value = nUDRightValue_rightLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDLineWidth_rightLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("RightLogTrack").Element("curveLineWidth").Value = nUDLineWidth_rightLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDExtractPoints_rightLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("RightLogTrack").Element("intervalPiontNumber").Value = nUDExtractPoints_rightLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }
        private void nUDTrackWidth_rightLog_ValueChanged(object sender, EventArgs e)
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlConfigSection);
            sectionMapXML.Element("SectionMap").Element("RightLogTrack").Element("trackWidth").Value = nUDTrackWidth_rightLog.Value.ToString("0"); ;
            sectionMapXML.Save(cProjectManager.xmlConfigSection);
        }

        private void btnLogTrackAddLeft_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0 && cbbLeftLogName.SelectedIndex >= 0)
            {
                string sSelectedLogName = this.cbbLeftLogName.SelectedItem.ToString();

                addLogData((int)LeftOrRight.left, sSelectedLogName);
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
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
                if (wellNote.Index>0)
                {
                    wellNote.Nodes.Add(tnLog);
                }
             
            }
            tvwWellSectionCollection.ExpandAll();
        }

  

       

    
   




    }
}
