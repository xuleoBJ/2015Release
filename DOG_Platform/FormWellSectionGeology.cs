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
    //成图方法 
    // 1. 选择井
    // 2. 根据层位或者海拔确定深度段深度
    // 3. 根据深度提取成图原始数据文件，放在临时文件下
    // 4. 读取各个临时文件，成图
    //
    public partial class FormWellSectionGeology : Form
    {
        string dirSectionData = Path.Combine(cProjectManager.dirPathTemp, "sectionGeoTemp");
        string fileNameSectionProfile = "profile.txt";
        string fileNameSectionLayerDepth = "layerDepth.txt";
        string fileNameSectionJSJL = "jsjl.txt";
        string fileNameSectionPerforation = "inputPerforation.txt";
        enum typeFlatted
        {
            海拔深度,
            顶面拉平,
            底面拉平,
        }
        List<string> ltStrSelectedJH = new List<string>();  //联井剖面井号
        //存储绘图剖面数据结构
        List<cWellSectionSVG> listWellsSection = new List<cWellSectionSVG>();
     
        public FormWellSectionGeology()
        {
            InitializeComponent();
        }
        private void FormNewWellSection_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitFormWellsGroupControl();
        }

        private void InitFormWellsGroupControl()
        {
            cPublicMethodForm.inialListBox(lbxJH, cProjectData.ltStrProjectJH);
            cPublicMethodForm.inialComboBox(cbbTopXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbBottomXCM, cProjectData.ltStrProjectXCM);
            cPublicMethodForm.inialComboBox(cbbLogName, cProjectData.ltStrLogSeriers);
            dgvLayerColorSetting.Columns.Add("Layer", "小层名");
            dgvLayerColorSetting.Columns.Add("Color", "颜色");
            for (int i = 0; i < cProjectData.ltStrProjectXCM.Count; i++)
            {
                string _sItem = cProjectData.ltStrProjectXCM[i];
                dgvLayerColorSetting.Rows.Add(_sItem);
                dgvLayerColorSetting.Rows[i].Cells[1].Style.BackColor = Color.Red;
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
        private void btn_upWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.upItemInListBox(lbxJHSeclected);
        }
        private void btn_downWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.downItemInListBox(lbxJHSeclected);
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

        private void btnSectionData_Click(object sender, EventArgs e)
        {
            setDepthIntervalShowedBYLayer();
        }

        void initializeTreeViewWellCollection()
        {
            this.tvwWellSectionCollection.Nodes.Clear();
            for (int i = 0; i < ltStrSelectedJH.Count; i++)
            {
                TreeNode tnWell = new TreeNode();
                tnWell.Text = ltStrSelectedJH[i];
                tnWell.Name = ltStrSelectedJH[i];
                tnWell.Nodes.Add("左侧曲线");
                tnWell.Nodes.Add("右侧曲线");
                tvwWellSectionCollection.Nodes.Add(tnWell);
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

        private void btnGenerateDataByInputDepth_Click(object sender, EventArgs e)
        {
            setDepthIntervalShowedBYElevationDepth();
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



        void openExitGraph()
        {
            XDocument sectionMapXML = XDocument.Load(cProjectManager.xmlSectionCSS);
            XElement XWellCollect = sectionMapXML.Element("SectionMap").Element("WellCollection");

            foreach (XElement el in XWellCollect.Elements())
            {
                string sJH = el.Attribute("id").Value;
                double dbX = double.Parse(el.Element("X").Value);
                double dbY = double.Parse(el.Element("Y").Value);
                float fKB = float.Parse(el.Element("KB").Value);
                float fTopShowed = float.Parse(el.Element("fShowedTop").Value);
                float fBaseShowed = float.Parse(el.Element("fShowedBottom").Value);
            }
        }

        void generateSectionGraph(int iTypeFlatted, string filenameSVGMap)
        {
            //xml存数据不合适 因为会有大量的井数据，但是可以存个样式，样式搭配数据，样式里可以有道宽，这样做到数据和样式的分离，成图解析器解析样式就OK。

            List<Point> PListWellPositon = new List<Point>();

            for (int i = 0; i < this.listWellsSection.Count; i++)
            {
                cWellSectionSVG itemWell = listWellsSection[i];
                //传入的深度与绘制无关，传入的海拔就是正，绘制时海拔向下为正
                if (iTypeFlatted == (int)typeFlatted.海拔深度) itemWell.fDepthFlatted = itemWell.fKB;
                if (iTypeFlatted == (int)typeFlatted.顶面拉平) itemWell.fDepthFlatted = itemWell.fKB - itemWell.fShowedDepthTop;
                if (iTypeFlatted == (int)typeFlatted.底面拉平) itemWell.fDepthFlatted = itemWell.fKB - itemWell.fShowedDepthBase;
             
                if(rdbPlaceBywellPosition.Checked==true ) PListWellPositon.Add(cCordinationTransform.getPointViewByJH(listWellsSection[i].sJH));
                if (rdbPlaceByEqual.Checked == true) PListWellPositon.Add(new Point(100+300*i,0));
                if (rdbPlaceBYWellDistance.Checked == true)
                {
                    PListWellPositon.Add(new Point(100 + 300 * i, 0)); 
                    //if (i == 0) PListWellPositon.Add(new Point(100, 0));
                    //else
                    //{
                    //    Point pointConvert2View = cCordinationTransform.getPointViewByJH(ltStrSelectedJH[i - 1]);
                    //    Point pointWell0Convert2View = cCordinationTransform.getPointViewByJH(ltStrSelectedJH[i]);
                    //    int iDistance = Convert.ToInt16(c2DGeometryAlgorithm.calDistance2D(pointConvert2View, pointWell0Convert2View));
                    //    PListWellPositon.Add(new Point(listWellsSection[i - 1].fXview + iDistance, 0));
                    //}
                }
            }

            cSVGDocSection cSection = new cSVGDocSection(5000, 5000,0,0);
            cSection.addSVGTitle(string.Join("-",listWellsSection.Select(p=>p.sJH).ToList())+ "剖面图",100, 100);
            XmlElement returnElemment;

            //海拔深度时 增加海拔尺，拉平不要海拔尺
            if (iTypeFlatted == (int)typeFlatted.海拔深度)
            {
                int upDepthElevationRuler = 0;
                int downDepthElevationRuler = -5000;
                int iScaleElevationRuler = 50;
                cSVGSectionTrackElevationRuler cElevationRuler = new cSVGSectionTrackElevationRuler();
                returnElemment = cElevationRuler.gElevationRuler(downDepthElevationRuler, upDepthElevationRuler, iScaleElevationRuler);
                cSection.addgElement(returnElemment, 0);
            }

            //根据井序列循环添加井剖面
            for (int i = 0; i < listWellsSection.Count; i++)
            {
                string sJH = listWellsSection[i].sJH;

                List<ItemWellPath> currentWellPathList = cProjectData.listProjectWell.Find(p => p.sJH == sJH).WellPathList;
                float fTopShowed = listWellsSection[i].fShowedDepthTop;
                float fBaseShowed = listWellsSection[i].fShowedDepthBase;
                float fDepthFlatted = listWellsSection[i].fDepthFlatted;
                int iCurrerntWellHorizonPotion = PListWellPositon[i].X;

                cSVGSectionWell currentWell = new cSVGSectionWell(sJH);

                if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                {
                    ItemWellPath wellPathTop = cIOinputWellPath.getWellPathItemByJHAndMD(sJH, fTopShowed);
                    ItemWellPath wellPathBase = cIOinputWellPath.getWellPathItemByJHAndMD(sJH, fBaseShowed);
                    returnElemment = currentWell.gWellCone(sJH, wellPathTop.f_TVD ,wellPathBase.f_TVD, fDepthFlatted, 10, 5);
                }
                else returnElemment = currentWell.gWellCone(sJH, fTopShowed, fBaseShowed, fDepthFlatted, 10, 5);
                currentWell.addTrack(returnElemment, 0);

                //增加地层道
                string filePathLayer = Path.Combine(dirSectionData, sJH,fileNameSectionLayerDepth);
                trackLayerDepthDataList trackDataListLayerDepth =
                    cDirDataSourceWellSection.setupDataListTrackLayerDepth(filePathLayer, fTopShowed, fBaseShowed);
                int iTrackWidth = 15;
                cSVGSectionTrackLayer layerTrack = new cSVGSectionTrackLayer(iTrackWidth);
                layerTrack.iTextSize = 6;
                if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                    returnElemment = layerTrack.gXieTrack2VerticalLayerDepth(sJH, trackDataListLayerDepth, fDepthFlatted); 
                else returnElemment = layerTrack.gTrackLayerDepth(sJH,trackDataListLayerDepth, fDepthFlatted);
                currentWell.addTrack(returnElemment, iTrackWidth);

                //增加解释结论道
                string filePathJSJL = Path.Combine(dirSectionData, sJH ,fileNameSectionJSJL);
                trackJSJLDataList trackDataListJSJL = cDirDataSourceWellSection.setupDataListTrackJSJL(filePathJSJL, fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackJSJL JSJLTrack = new cSVGSectionTrackJSJL(iTrackWidth);
                if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2) 
                    returnElemment = JSJLTrack.gXieTrack2VerticalJSJL(sJH, trackDataListJSJL, fDepthFlatted);
                 else returnElemment = JSJLTrack.gTrackJSJL(sJH,trackDataListJSJL, fDepthFlatted);
                currentWell.addTrack(returnElemment, -iTrackWidth);

                //增加射孔道
                string filePathInputPerforation = Path.Combine(dirSectionData, sJH ,fileNameSectionPerforation);
                trackInputPerforationDataList trackDataListPerforation = cDirDataSourceWellSection.setupDataListTrackPerforation(filePathInputPerforation, fTopShowed, fBaseShowed);
                iTrackWidth = 15;
                cSVGSectionTrackPeforation perforationTrack = new cSVGSectionTrackPeforation(iTrackWidth);
                if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                    returnElemment = perforationTrack.gXieTrack2VerticalPerforation(sJH, trackDataListPerforation, fDepthFlatted);
                 else returnElemment = perforationTrack.gTrackPerforation(sJH,trackDataListPerforation, fDepthFlatted);
                currentWell.addTrack(returnElemment, -2 * iTrackWidth);


                //增加吸水剖面
                string filePathProfile = Path.Combine(dirSectionData, sJH,fileNameSectionProfile);
                //trackInputPerforationDataList trackDataListPerforation = cDirDataSourceWellSection.setupDataListTrackPerforation(filePathInputPerforation, fTopShowed, fBaseShowed);
                //iTrackWidth = 15;
                //cSVGSectionTrackPeforation perforationTrack = new cSVGSectionTrackPeforation(iTrackWidth);
                //if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                //    returnElemment = perforationTrack.gXieTrack2VerticalPerforation(sJH, trackDataListPerforation, fDepthFlatted);
                //else returnElemment = perforationTrack.gTrackPerforation(sJH, trackDataListPerforation, fDepthFlatted);
                //currentWell.addTrack(returnElemment, -2 * iTrackWidth);

                //增加左边曲线
                string fileLeftLogScrPath = Path.Combine(dirSectionData, sJH + "\\left");
                foreach (string fileLog in Directory.GetFileSystemEntries(fileLeftLogScrPath))
                {
                    trackLogDataList trackDataListLeftLog = cDirDataSourceWellSection.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;

                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                        returnElemment = logTrack.gXieTrack2VerticalLog(sJH, trackDataListLeftLog, fDepthFlatted);
                    else returnElemment = logTrack.gTrackLog(sJH, trackDataListLeftLog, fDepthFlatted);
                    currentWell.addTrack(returnElemment, -30);
                }

                //增加右边曲线
                string fileRightLogScrPath = Path.Combine(dirSectionData, sJH + "\\right");
                foreach (string fileLog in Directory.GetFileSystemEntries(fileRightLogScrPath))
                {
                    trackLogDataList trackDataListRightLog = cDirDataSourceWellSection.setupDataListTrackLog(fileLog, fTopShowed, fBaseShowed);
                    iTrackWidth = 15;
                    cSVGSectionTrackLog logTrack = new cSVGSectionTrackLog(iTrackWidth);
                    if (rdbDepthModelTVD.Checked == true && currentWellPathList.Count > 2)
                        returnElemment = logTrack.gXieTrack2VerticalLog(sJH, trackDataListRightLog, fDepthFlatted);
                    else returnElemment = logTrack.gTrackLog(sJH,  trackDataListRightLog, fDepthFlatted);

                    currentWell.addTrack(returnElemment, iTrackWidth);
                } 
                cSection.addgElement(currentWell.gWell, iCurrerntWellHorizonPotion);
            }

            bool bConnect = this.cbxConnectSameLayerName.Checked;

            if (bConnect == true)
            {
                for (int i = 0; i < listWellsSection.Count - 1; i++)
                {
                    string filePathLayer = Path.Combine(dirSectionData, listWellsSection[i].sJH,fileNameSectionLayerDepth);
                    trackLayerDepthDataList well1LayerDepthDataList = cDirDataSourceWellSection.setupDataListTrackLayerDepth(filePathLayer, listWellsSection[i].fShowedDepthTop, listWellsSection[i].fShowedDepthBase);
                    string file2PathLayer = Path.Combine(dirSectionData, listWellsSection[i + 1].sJH ,fileNameSectionLayerDepth);
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


      
        private void btnMakeSection_Click(object sender, EventArgs e)
        {
            string filenameSVGMap;

            if (this.tbxTitle.Text == "") filenameSVGMap = string.Join("-", ltStrSelectedJH.ToArray()) + "-section.svg"; 
            else filenameSVGMap = this.tbxTitle.Text + ".svg";

            if (rdbFlattedByDepth.Checked == true) generateSectionGraph((int)(typeFlatted.海拔深度), filenameSVGMap) ; 
            else if (rdbFlattedByTopDepth.Checked == true) generateSectionGraph((int)(typeFlatted.顶面拉平), filenameSVGMap);
            else if (rdbFlattedByBaseDepth.Checked == true) generateSectionGraph((int)(typeFlatted.底面拉平), filenameSVGMap);

        }

        private void btnAddLayerDepth_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionData, sJH,fileNameSectionLayerDepth);
                    cIOinputLayerDepth cSelectLayerDepth = new cIOinputLayerDepth();
                    cSelectLayerDepth.selectSectionDrawData2File(sJH, filePath);
                }
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
                {
                    if (wellNote.Text != "深度尺")
                        wellNote.Nodes.Add("地层");
                }
                tvwWellSectionCollection.ExpandAll();
                cXDocSection.addTrackLayer(cProjectManager.xmlSectionCSS, "idLayer",20);
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }

        private void btnAddJSJLTrack_Click(object sender, EventArgs e)
        {
            if (listWellsSection.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionData, sJH, fileNameSectionJSJL);
                    cIOinputJSJL.selectSectionDrawData2File(sJH, filePath);
                }
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
                {
                    if (wellNote.Text != "深度尺")
                        wellNote.Nodes.Add("解释结论");
                }
                tvwWellSectionCollection.ExpandAll();
                cXDocSection.addTrackJSJL(cProjectManager.xmlSectionCSS, "idJSJL", 20);
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }

        void addLogData(int iLeftOrRight, string sSelectedLogName)
        {
            //iLeftOrRight, 0 左 1 右
            foreach (string sJH in ltStrSelectedJH)
            {
                string filePath = Path.Combine(dirSectionData, sJH + "\\left\\" + sSelectedLogName + ".txt");
                if (iLeftOrRight == 1)
                {
                    filePath = Path.Combine(dirSectionData, sJH + "\\right\\" + sSelectedLogName + ".txt");
                }
                cIOinputLog.extractTextLog2File(sJH, sSelectedLogName, filePath);
            }
            foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
            {
                TreeNode tnLog = new TreeNode();
                tnLog.Text = sSelectedLogName;
                wellNote.Nodes[iLeftOrRight].Nodes.Add(tnLog);
                
            }

            tvwWellSectionCollection.ExpandAll();

           
            string sLogName=this.cbbLogName.SelectedItem.ToString();
            string sLogColor=cPublicMethodBase.getRGB(cbbLogColor.BackColor);
            float fRightValue=Convert.ToSingle(nUDLogRightValue.Value);
            float fLeftValue=Convert.ToSingle(nUDLogLeftValue.Value);
            cXDocSection.addTrackLog(cProjectManager.xmlSectionCSS, "idLog#" + sLogName, 20, iLeftOrRight, sLogName, fLeftValue, fRightValue, sLogColor);
        }
        
        void deleteLogData(int iLeftOrRight, string sSelectedLogName)
        {

            foreach (string sJH in ltStrSelectedJH)
            {
                string filePath = Path.Combine(dirSectionData, sJH + "\\leftLog.txt");
                if (iLeftOrRight == (int)LeftOrRight.right)
                {
                    filePath = Path.Combine(dirSectionData, sJH + "\\rightLog.txt");
                }

            }
            foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
            {
                if (iLeftOrRight == (int)LeftOrRight.left)
                {
                    TreeNode tnLeftLog = new TreeNode();
                    tnLeftLog.Text = "左侧曲线";
                    tnLeftLog.Name = LeftOrRight.left.ToString();
                    tnLeftLog.Nodes.Add(sSelectedLogName);
                    wellNote.Nodes.Add(tnLeftLog);
                }
                else
                {
                    TreeNode tnRightLog = new TreeNode();
                    tnRightLog.Text = "右侧曲线";
                    tnRightLog.Name = LeftOrRight.right.ToString();
                    tnRightLog.Nodes.Add(sSelectedLogName);
                    wellNote.Nodes.Add(tnRightLog);
                }
            }
            tvwWellSectionCollection.ExpandAll();
        }
        private void btnAddLeftLogTrack_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0 && cbbLogName.SelectedIndex >= 0)
            {
                string sSelectedLogName = this.cbbLogName.SelectedItem.ToString();
                if(rdbLeft.Checked==true) addLogData(0, sSelectedLogName);
                else addLogData(1, sSelectedLogName); 
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }

        }
        private void cbbLeftLogColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbLogColor);
        }

       

     

        private void btnDeleteLeftLog_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0 && cbbLogName.SelectedIndex >= 0)
            {
                string sSelectedLogName = this.cbbLogName.SelectedItem.ToString();
                foreach (string sJH in ltStrSelectedJH)
                {
                    //cXMLSection.deleteDataLog2Well(false, sJH, sSelectedLogName, cProjectManager.xmlSectionCSS);
                }
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
                {
                    foreach (TreeNode trackNote in wellNote.Nodes)
                    {
                        if (trackNote.Text == sSelectedLogName)
                            wellNote.Nodes.Remove(trackNote);
                    }
                }

                tvwWellSectionCollection.ExpandAll();
            }
        }


        private void tvwWellSectionCollection_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectNode = tvwWellSectionCollection.SelectedNode;
            ContextMenuStrip cmsSection = new System.Windows.Forms.ContextMenuStrip();
            tvwWellSectionCollection.ContextMenuStrip = cmsSection;
            string _sJH = "";
            string _fileLogScrPath = "";
            string _sLogName = "";
            cContextMenuStripWellSection cContextMenuStrip;

            switch (selectNode.Level)
            {
                case 0:
                    break;
                case 1:
                    _sJH = selectNode.Parent.Text;
                    switch (selectNode.Text)
                    {
                        case "左侧曲线":
                            _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\left");
                            cContextMenuStrip = new cContextMenuStripWellSection
                                            (cmsSection, selectNode, _sJH, _sLogName, _fileLogScrPath);
                            cContextMenuStrip.setupTsmiLogAdd();
                            cmsSection = cContextMenuStrip.cms;
                            break;
                        case "右侧曲线":
                            _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\right");
                            cContextMenuStrip = new cContextMenuStripWellSection
                                            (cmsSection, selectNode, _sJH, _sLogName, _fileLogScrPath);
                            cContextMenuStrip.setupTsmiLogAdd();
                            cmsSection = cContextMenuStrip.cms;
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    _sJH = selectNode.Parent.Parent.Text;
                    _sLogName = selectNode.Text;
                    _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\right");
                    if (selectNode.Parent.Parent.Text == "左侧曲线")
                    {
                        _fileLogScrPath = Path.Combine(dirSectionData, _sJH + "\\left");
                    }

                    cContextMenuStrip = new cContextMenuStripWellSection
                         (cmsSection, selectNode, _sJH, _sLogName, _fileLogScrPath);
                    cContextMenuStrip.setupTsmiLogDelete();
                    cContextMenuStrip.setupTsmiLogSetting();
                    cmsSection = cContextMenuStrip.cms;
                    break;
                case 3:
                    MessageBox.Show(selectNode.Text);
                    break;
                default:
                    break;
            }
        }

        private void btnAddPeforation_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionData, sJH ,fileNameSectionPerforation);
                    cIOinputPerforation cSelectInputPerforation = new cIOinputPerforation();
                    cSelectInputPerforation.selectSectionDraData2File(sJH, filePath);
                }
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
                {
                    if (wellNote.Text != "深度尺")
                        wellNote.Nodes.Add("射孔");
                }
                tvwWellSectionCollection.ExpandAll();
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }

    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLayerColorSetting.CurrentCell.ColumnIndex == 1)
            {
                ColorDialog colorDialog1 = new ColorDialog();
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    dgvLayerColorSetting.CurrentCell.Style.BackColor = colorDialog1.Color;
                }
            }
        }

        private void cbbBottomXCM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbTopXCM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddProfile_Click(object sender, EventArgs e)
        {
            if (ltStrSelectedJH.Count > 0)
            {
                foreach (string sJH in ltStrSelectedJH)
                {
                    //提取所选井段数据存入绘图目录下保存
                    string filePath = Path.Combine(dirSectionData, sJH,fileNameSectionProfile);
                    cIOinputPerforation cSelectInputPerforation = new cIOinputPerforation();
                    cSelectInputPerforation.selectSectionDraData2File(sJH, filePath);
                }
                foreach (TreeNode wellNote in tvwWellSectionCollection.Nodes)
                {
                    if (wellNote.Text != "深度尺")
                        wellNote.Nodes.Add("吸水");
                }
                tvwWellSectionCollection.ExpandAll();
            }
            else
            {
                MessageBox.Show("请先确认深度段。");
            }
        }

        

              

        




    }
}
