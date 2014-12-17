using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DOGPlatform;

namespace DOGPlatform
{
   
    public partial class FormMain : Form
    {
        public static List<string> ltTV_SelectedJH = new List<string>();
        public static List<string> ltTV_SelectedLogNames = new List<string>();

        public static string filePathWebSVG = "";

        string sJHselectedOnPanel = "";



        List<TabPage> listTabpageMain = new List<TabPage>(); //主面板
        List<ToolStripButton> listToolStripButtonsDraw = new List<ToolStripButton>();//动态添加菜单

        OpreateMode currentOpreateMode = OpreateMode.Initial;
        public FormMain()
        {
            InitializeComponent();

            intializeMyForm();
            if (cSoftwareLimited.limitedDay() == false)
            {
                MessageBox.Show("软件已经过期，请联系软件作者QQ：38643987.", "提示");
                System.Environment.Exit(0);
            }
        }
        private void intializeMyForm()
        {

            tvProjectData.ImageList = this.imageListMain;
            listTabpageMain.Add(tbgWellNavigation);
            listTabpageMain.Add(tbgIE);
            listTabpageMain.Add(tbgWellHead);
            listTabpageMain.Add(tbgLayerSeriers);
            for (int i = 3; i >= 2; i--)
            {
                this.tbcMain.TabPages.Remove(tbcMain.TabPages[i]);
            }

            listToolStripButtonsDraw.Add(tsBtnDrawLine);
            listToolStripButtonsDraw.Add(tsBtnMove);
            listToolStripButtonsDraw.Add(tsBtnDrawPolyGon);
            initialCbbScale();
        }

        //初始化控件当新建工程或者打开工程时
        void initialCbbScale()
        {
            List<string> listScale = new List<string>();
            listScale.Add("10000");
            listScale.Add("20000");
            listScale.Add("25000");
            listScale.Add("50000");
            listScale.Add("5000");
            listScale.Add("2000");
            listScale.Add("1000");
            listScale.Add("500");
            listScale.Add("250");
            listScale.Add("200");
            listScale.Add("100000");
            cbbScale.Items.Clear();
            foreach (string sItem in listScale)
            {
                cbbScale.Items.Add(sItem);
            }
            cbbScale.SelectedIndex = 0;
           
        }

        bool openProject()
        {
            cProjectManager OpenProject = new cProjectManager();
            if (OpenProject.loadProjectData())
            {
                this.ToolStripStatusLabelProjectionInfor.Text = "工程路径：" + cProjectManager.dirProject;
                return true;
            }
            else
            {
                return false;
            }
        }
      

        private void updateTreeView()
        {
            tvProjectData.CheckBoxes = true;
            tvProjectData.Nodes.Clear();
            cTreeViewProjectData.setupTNwell(tvProjectData);
            cTreeViewProjectData.setupTNLayer(tvProjectData);

            foreach (TreeNode tn in tvProjectData.Nodes)
            {
                if (tn.Level == 0)
                {
                    tn.Expand();
                }
            }

            WellNavitationInvalidate();
            updateTreeViewWindows();
        }

        void updateTreeViewWindows()
        {

            tvwWindows.CheckBoxes = true;
            tvwWindows.Nodes.Clear();
            foreach (TabPage tbg in listTabpageMain)
            {
                TreeNode tabNote = new TreeNode();
                tabNote.Name = tbg.Name;
                tabNote.Text = tbg.Text;
                tvwWindows.Nodes.Add(tabNote);
                if (tbcMain.TabPages.Contains(tbg))
                {
                    tabNote.Checked = true;
                }
            }
        }

        #region  工程菜单
        private void tsmiNewProject_Click(object sender, EventArgs e)
        {
            createNewProject();
            updateTreeView();
        }
        private void tsmiOpenProject_Click(object sender, EventArgs e)
        {
            if (openProject()) updateTreeView();
        }
        private void tsmSaveProject_Click(object sender, EventArgs e)
        {
            cProjectManager.saveProject();
        }
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void tsBtnSaveProject_Click(object sender, EventArgs e)
        {
            cProjectManager.saveProject();
        }
        #endregion

        #region  输入数据
        private void importDataGridView(DataGridView dgv, string filepath)
        {
            cPublicMethodForm.readDataGridView2TXTFile(dgv, filepath);
            cPublicMethodForm.read2DataGridViewByTextFile(filepath, dgv);
        }
        private void showInputStaticGeologyTabpage()
        {
            List<TabPage> listTabPageStaticData = new List<TabPage>();

            listTabPageStaticData.Add(this.tbgWellHead);
            listTabPageStaticData.Add(this.tbgLayerSeriers);

            foreach (TabPage tg in listTabPageStaticData)
            {
                if (tg.Parent == null)
                {
                    tbcMain.TabPages.Add(tg);
                }
            }
        }
        private void updateInputData(DataGridView dgv, string inputFilepath)
        {
            cPublicMethodForm.updateInputStartWithJH(inputFilepath, dgv);
            cPublicMethodForm.read2DataGridViewByTextFile(inputFilepath, dgv);
        }

        private void btnOpenWellHead_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.read2DataGridViewByTextFile(dgvWellHead);
        }
        private void btnImportWellHead_Click(object sender, EventArgs e)
        {
            cIOinputWellHead.readInput2Project(this.dgvWellHead, cProjectManager.filePathInputWellhead);

            cIOinputWellHead.codeReplaceWellHead();

            cDataQuanlityControl cTestControl = new cDataQuanlityControl();
            bool DataOKwellHead = cTestControl.dataCheckInputWellHead();

            if (DataOKwellHead == true)
            {
                cProjectData.ltStrProjectJH.Clear();
                foreach (string _sJH in cIOinputWellHead.getLtStrJH())
                {
                    cProjectData.ltStrProjectJH.Add(_sJH);
                    cProjectManager.createWellDir(_sJH);
                }
                
                cProjectData.setProjectWellsInfor();
                updateTreeView();
                this.tbcMain.SelectedIndex = 0;
            }


        }
     
        private void btnOpenLayerSeriers_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.read2DataGridViewByTextFile(dgvLayerSeriers);
        }
       
        private void btnImportLayerSeriers_Click(object sender, EventArgs e)
        {
            importDataGridView(dgvLayerSeriers, cProjectManager.filePathInputLayerSeriers);
            cProjectData.getProjectXCM();
            cProjectManager.createLayerDir();
            updateTreeView();
        }
       
        private void btnCopyFromExcelWellHead_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.DataGridViewCellPaste(dgvWellHead);
        }

        private void btnCopyFromExcelLayerSeriers_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.DataGridViewCellPaste(dgvLayerSeriers);
        }

        #endregion

        #region  地质
        private void tsmiLayerGeology_Click(object sender, EventArgs e)
        {
            FormMapLayer formLayerMap = new FormMapLayer();
            formLayerMap.ShowDialog();
            updateWebSVG();  
        }

        private void tsmiSectionReservior_Click(object sender, EventArgs e)
        {
            FormWellSectionPath FormWellsGroup = new FormWellSectionPath();
            FormWellsGroup.ShowDialog();
            updateWebSVG();  
        }

        private void ToolStripStatusLabelProjectionInfor_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", cProjectManager.dirProject);
        }

        private void calHeterogeneityInterLayerWorkerMethod(object sender, WaitWindowEventArgs e)
        {
            cCalGeologyStatistics cCalHeterogeneity = new cCalGeologyStatistics();
            cCalHeterogeneity.calHeterogeneityInterLayer();
        }
        private void calHeterogeneityInnerLayerWorkerMethod(object sender, WaitWindowEventArgs e)
        {
            cCalGeologyStatistics cCalHeterogeneity = new cCalGeologyStatistics();
            cCalHeterogeneity.calHeterogeneityInnerLayer();
        }

        
        private void calXCSJBWorkerMethod(object sender, WaitWindowEventArgs e)
        {
            cIODicLayerData cCalLayerData = new cIODicLayerData();
            cCalLayerData.generateLayerData();

        }
        private void calMatchJsJlWorkerMethod(object sender, WaitWindowEventArgs e)
        {
            cCalProjectAnaFiles cCalTest = new cCalProjectAnaFiles();
            cCalTest.matchJSJL2LayerDepth();
        }
        private void calSplitJSJLWorkerMethod(object sender, WaitWindowEventArgs e)
        {
            cCalProjectAnaFiles cCalTest = new cCalProjectAnaFiles();
            cCalTest.splitJSJL2LayerDepth();
        }
        #endregion

        private void tsmiCalWellDistance_Click(object sender, EventArgs e)
        {
            FormCalWellDistance formCalDistance = new FormCalWellDistance();
            formCalDistance.Show();
        }
        private void tsmiProjectGraphManager_Click(object sender, EventArgs e)
        {
            FormMapManager formOutcomeManager = new FormMapManager();
            formOutcomeManager.ShowDialog();
        }
        private void tsmi注采关系分析_Click(object sender, EventArgs e)
        {
            FormInjProAna forminjectProductAna = new FormInjProAna();
            forminjectProductAna.Show();
        }


        private void tsmiCalWellTypeDictionary_Click(object sender, EventArgs e)
        {
            cDicOperateFileWellType cCalTest = new cDicOperateFileWellType();
            cCalTest.generateWellTypeDic();
        }

        private void tsmiSectionWellPattern_Click(object sender, EventArgs e)
        {
            FormWellsGroup formFD = new FormWellsGroup();
            formFD.Show();
        }
       
        private void tsmiCalWellProductionDictionary_Click(object sender, EventArgs e)
        {
         
        }
        private void tsmiCalProductionFoctor_Click(object sender, EventArgs e)
        {
            FormSettingSplitFactor formSplitFactor = new FormSettingSplitFactor();
            formSplitFactor.ShowDialog();
        }
       
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tbcMain.SelectedTab.Name == this.tbgWellHead.Name)
            {
                this.dgvWellHead.Height = this.tbcMain.Height - 120;
                if (File.Exists(cProjectManager.filePathInputWellhead))
                {
                    cPublicMethodForm.read2DataGridViewByTextFile(cProjectManager.filePathInputWellhead, dgvWellHead);
                }
            }

            if (this.tbcMain.SelectedTab.Name == this.tbgLayerSeriers.Name)
            {
                this.dgvLayerSeriers.Height = this.tbcMain.Height - 100;
                if (File.Exists(cProjectManager.filePathInputLayerSeriers))
                {
                    cPublicMethodForm.read2DataGridViewByTextFile(cProjectManager.filePathInputLayerSeriers, dgvLayerSeriers);
                }
            }
           
        }
       
      
        private void tsmiDebug_Click(object sender, EventArgs e)
        {
            FormConfig formConfig = new FormConfig();
            formConfig.ShowDialog();
        }
        
        private void tsmiDeleteSelectedWellInPanel_Click(object sender, EventArgs e)
        {
            if (sJHselectedOnPanel != "")
            {
                DialogResult dialogResult = MessageBox.Show("当前选中井为：" + sJHselectedOnPanel + "，确认删除？", "删除选中井", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    cIOinputWellHead fileWellHead = new cIOinputWellHead();
                    fileWellHead.deleteJHFromWellHead(sJHselectedOnPanel);
                    cProjectData.ltStrProjectJH.Remove(sJHselectedOnPanel); 
                    WellNavitationInvalidate();
                    updateTreeView();
                }
                //else if (dialogResult == DialogResult.No)
                //{
                //    //do something else
                //}

            }
        }

        private void 部署井ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddNewWell formAddWell = new FormAddNewWell();
            formAddWell.ShowDialog();
            updateTreeView();
            cProjectData.ltStrProjectJH.Clear();
            cProjectData.ltStrProjectJH = cIOinputWellHead.getLtStrJH(); 
            WellNavitationInvalidate();
        }

        private void tsmiCalSplitJSJL_Click(object sender, EventArgs e)
        {
            WaitWindow.Show(this.calSplitJSJLWorkerMethod);
        }

        private void tsmiCal_JSJLMatch_Click(object sender, EventArgs e)
        {
            WaitWindow.Show(this.calMatchJsJlWorkerMethod);
        }

        private void tsmiSectionSingleWell_Click(object sender, EventArgs e)
        {
            FormSingleWellLog formSingleWellLog = new FormSingleWellLog();
            formSingleWellLog.Show();
        }

        private void tsmiCalLayerHeterogeneityInner_Click(object sender, EventArgs e)
        {
            WaitWindow.Show(this.calHeterogeneityInnerLayerWorkerMethod);
        }

        private void tsmiShowLayerHeterogeneityInner_Click(object sender, EventArgs e)
        {
            FormDataTable formDatatable = new FormDataTable(cProjectManager.filePathInnerLayerHeterogeneity);
            formDatatable.Show();
        }


        private void tsmiShowLayerHeterogeneityInner1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", cProjectManager.filePathInterLayerHeterogeneity);
        }

        private void tsmsCalXCSJB_Click(object sender, EventArgs e)
        {
            WaitWindow.Show(this.calXCSJBWorkerMethod);
        }

        private void tsmiShowXCSJB_Click(object sender, EventArgs e)
        {
            FormDataTable newFormData = new FormDataTable(cProjectManager.filePathLayerDataDic);
            newFormData.Show();
        }

        private void tsmiWellPointDataPie_Click(object sender, EventArgs e)
        {
            FormDataAnalysis formRoseMap = new FormDataAnalysis();
            formRoseMap.ShowDialog();
        }

        private void tsmiLayerInjectProductSystem_Click(object sender, EventArgs e)
        {
            FormInjProMap formInjProSystemMap = new FormInjProMap();
            formInjProSystemMap.Show();
        }

        private void tsmiSectionStratum_Click(object sender, EventArgs e)
        {
            FormWellSectionGeology formReserviorSection = new FormWellSectionGeology();
            formReserviorSection.ShowDialog();
            updateWebSVG(); 
        }


        private void btnInputWellheaddelDgvLine_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.deleteSelectedRowInDataGridView(dgvWellHead);
        }

        private void btnInputLayerSerieresdelDgvLine_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.deleteSelectedRowInDataGridView(dgvLayerSeriers);
        }

        private void tsmiLayerProductionState_Click(object sender, EventArgs e)
        {

        }


        private void tabControlMain_DoubleClick(object sender, EventArgs e)
        {
            if (tbcMain.SelectedIndex > 1)
            {
                TabPage currentPage = tbcMain.SelectedTab;
                tbcMain.TabPages.Remove(currentPage);
            }
        }

        void initializeTabGraph()
        {
            string dir = cProjectManager.dirPathMap;
            tvProjectGraph.ImageList = this.imageListMain;
            cPublicMethodForm.ListDirectory(tvProjectGraph, dir);
            tvProjectGraph.ExpandAll();
        }

        private void tabControlProject_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tbcProject.SelectedTab == tbgProjectGraph) //选择了图形tbg
            {
                initializeTabGraph();
                string[] filenames = Directory.GetFiles(cProjectManager.dirPathMap, "*.svg");
            }
            if (tbcProject.SelectedTab == this.tbgProjectMapPattern)
            {
                try
                {
                    string _filePathSVGOpened = Path.Combine(cProjectManager.dirPathMap, tvProjectGraph.SelectedNode.Text);
                    LoadTreeViewFromXmlFile(_filePathSVGOpened, tvMapPattern);
                }
                catch (XmlException xmlEx)
                {
                    MessageBox.Show(xmlEx.Message);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void tvwProjectGraph_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tvProjectGraph.ContextMenuStrip = cmsProject;
            TreeNode selectNode = tvProjectGraph.SelectedNode;
            cmsProject.Items.Clear();

            switch (selectNode.Level)
            {
                case 0:
                    break;
                case 1:
                    cContextMenuStripSVGGraph cTS = new cContextMenuStripSVGGraph(cmsProject, selectNode, selectNode.Text);
                    cTS.setupTsmiOpenInInkscape();
                    cTS.setupTsmiOpenIE();
                    cTS.setupTsmiDeleteFile();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }


            filePathWebSVG = Path.Combine(cProjectManager.dirPathMap, tvProjectGraph.SelectedNode.Text);
            updateWebSVG();  
         
        }


        void updateWebSVG()
        {
            this.tbcMain.SelectedTab = tbgIE;

             try
            {

                if (filePathWebSVG.EndsWith(".svg"))
                {
                    this.webBrowserIE.Navigate(new Uri(filePathWebSVG));
                    this.tbgIE.Text = filePathWebSVG;
                }
                else
                {
                    this.tbcMain.SelectedTab = tbgWellNavigation; 
                }
            }
            catch (System.UriFormatException)
            {
                MessageBox.Show("error.");
            }
            
        }

        void createNewProject()
        {
            if (cProjectManager.creatProject())
            {
                showInputStaticGeologyTabpage();
                this.ToolStripStatusLabelProjectionInfor.Text = "工程路径：" + cProjectManager.dirPathUserData;
            }
        }

        private void tsBtnNewProject_Click(object sender, EventArgs e)
        {
            createNewProject();
        }

        private void tsBtnOpenProject_Click(object sender, EventArgs e)
        {
            openProject();
        }

        private void tsBtnZoonIn_Click(object sender, EventArgs e)
        {
            if (tbcMain.SelectedIndex == 0)
            {
                cProjectData.fMapScale = cProjectData.fMapScale * 1.2F;
                cbbScale.Text = (1000.0 / cProjectData.fMapScale).ToString("0");
                WellNavitationInvalidate();
            }
            if (tbcMain.SelectedIndex == 1) 
            {
                webBrowserIE.Focus();
                SendKeys.Send("^{+}");
            }
        }

        private void tsBtnZoomOut_Click(object sender, EventArgs e)
        {
            if (tbcMain.SelectedIndex == 0)
            {
                cProjectData.fMapScale = cProjectData.fMapScale * 0.8F;
                cbbScale.Text = (1000.0 / cProjectData.fMapScale).ToString("0");
                WellNavitationInvalidate();
            }
            if (tbcMain.SelectedIndex == 1)
            {
                webBrowserIE.Focus();
                SendKeys.Send("^{-}");
            }
        }
        private void tsCbbScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbScale.SelectedIndex >= 0)
            {
                float fScale = float.Parse(cbbScale.SelectedItem.ToString());
                cProjectData.fMapScale = 1000 / fScale;
                WellNavitationInvalidate();
            }
        }
           

        private void tvProjectData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.tvProjectData.ContextMenuStrip = this.cmsProject;
            TreeNode selectNode = tvProjectData.SelectedNode;
            cmsProject.Items.Clear();
            switch (selectNode.Level)
            {
                case 0: //第一级菜单
                    switch (selectNode.Name)
                    {
                        case "tnWells":
                            //右键菜单
                            cContextMenuStripInputWellsManager cCMSwells = new cContextMenuStripInputWellsManager(cmsProject, selectNode);
                            cCMSwells .setupContextMenuWellMangager();
                            cmsProject = cCMSwells .cms;
                            
                            break;
                        case "tnWellTops":
                            cContextMenuStripInputLayer cmsWellTops = new cContextMenuStripInputLayer(cmsProject, selectNode, selectNode.Text);
                            cmsWellTops.setupTsmiImportLayers();
                            cmsProject = cmsWellTops.cms;
                            break; 
                    }
                    break;
                case 1://第2级菜单
                    if (selectNode.Parent.Text == "井" && selectNode.Index > 0) //index=0 是全局测井曲线
                    {
                        //右键快捷菜单配置
                        string _sJH=selectNode.Text;
                        cContextMenuStripInputWell cCMSinputWell = new cContextMenuStripInputWell(cmsProject, selectNode, _sJH);
                        cCMSinputWell.setupTsmiDataView();
                        cCMSinputWell.setupTsmiDataImport();
                    }
                    if (selectNode.Parent.Text == "井" && selectNode.Index == 0) 
                    {
                        cContextMenuStripInputWellLog cTS = new cContextMenuStripInputWellLog(cmsProject, selectNode, selectNode.Parent.Text);
                        cmsProject = cTS.cms; 
                    }
                    if (selectNode.Parent.Name == "tnWellTops")
                    {
                        cContextMenuStripInputLayer cCMSinputLayer = new cContextMenuStripInputLayer(cmsProject, selectNode, selectNode.Text);
                        cCMSinputLayer.setupTsmiImportFaultLine();
                        cCMSinputLayer.setupTsmiImportContour();
                    }
                    break;
                case 2://第3级菜单，右键快捷菜单配置
                  
                    if (selectNode.Text == "well logs")
                    {
                        cContextMenuStripInputWellLog cTS = new cContextMenuStripInputWellLog(cmsProject, selectNode, selectNode.Parent.Text);
                        cTS.setupContextMenuStripWellLog();
                        cmsProject = cTS.cms;
                    cTreeViewProjectData.setupTNWellLog(selectNode, selectNode.Parent.Text);
                    }
                    break;
                case 3://第4级菜单，右键快捷菜单配置
                    if (selectNode.Parent.Text == "well logs")
                    {
                        cContextMenuStripLogItem cTS = new cContextMenuStripLogItem(cmsProject, selectNode, selectNode.Parent.Parent.Text);
                        cTS.setupLogItem();
                        cmsProject = cTS.cms;
                    }
                    break;

                default:
                    break;
            }
        }
      

        private void 动态地质分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProductAnalisys formProductionMap = new FormProductAnalisys();
            formProductionMap.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            cMenuStripMain mainMenuStrip = new cMenuStripMain(msMain);
            mainMenuStrip.setupTsmiPattern();
            mainMenuStrip.setupTsmiDataAnalysis();
            mainMenuStrip.setupTsmiTools();
            mainMenuStrip.setupTsmiConfig();
            mainMenuStrip.setupTsmiHelps();

        }


        public void showGrpah(string filepathGrpath)
        {
            this.tbcMain.SelectedIndex = 1;
            this.webBrowserIE.Navigate(new Uri(filepathGrpath));
        }

        private void tsmiCalLayerHeterogeneityInter_Click(object sender, EventArgs e)
        {
            WaitWindow.Show(this.calHeterogeneityInterLayerWorkerMethod);
        }

        private void tsmiShowLayerHeterogeneityInter_Click(object sender, EventArgs e)
        {
            FormDataTable formDatatable = new FormDataTable(cProjectManager.filePathInterLayerHeterogeneity);
            formDatatable.Show();
        }

        private void tsmiWellPosition4Petrel_Click(object sender, EventArgs e)
        {
            cExportData4Petrel.exportWellHead();
        }


        int iNumClickLineDraw = 0;
        Point pLinePoint1 = new Point(-1, -1);
        Point pLinePoint2 = new Point(-1, -1);
        List<Point> listPointPolygon = new List<Point>();
        bool bEndDrawPolygon = true;
        void setCurrentOperationMode(object sender)
        {
            foreach (ToolStripButton currentItem in listToolStripButtonsDraw)
            {
                if (currentItem == sender) currentItem.Checked = true;
                else currentItem.Checked = false;
            }
            currentOpreateMode = OpreateMode.Initial;
            if (tsBtnDrawLine.Checked == true)
            {
                currentOpreateMode = OpreateMode.DrawLine;
                iNumClickLineDraw = 0;
            }

            if (tsBtnDrawPolyGon.Checked == true)
            {
                currentOpreateMode = OpreateMode.DrawPolygon;
                listPointPolygon.Clear();
                bEndDrawPolygon = false;
            }
        }


        private void tsBtnDrawLine_Click(object sender, EventArgs e)
        {
            tsBtnDrawLine.Checked = !tsBtnDrawLine.Checked;
            setCurrentOperationMode(sender);
        }

        private void tsBtnDrawPolyGon_Click(object sender, EventArgs e)
        {
            tsBtnDrawPolyGon.Checked = !tsBtnDrawPolyGon.Checked;
            setCurrentOperationMode(sender);
        }

        Point Opoint = new Point(0, 0);

        private void panelWellNavigation_Paint(object sender, PaintEventArgs e)
        {
            addGrid(e);
            addWellPosion(e);
            if (currentOpreateMode == OpreateMode.DrawLine)
            {
                if (iNumClickLineDraw > 0 && iNumClickLineDraw % 2 == 1)
                    addCircle(e, pLinePoint1, 4);
                else
                    addLine(e, pLinePoint1, pLinePoint2);
            }
            if (currentOpreateMode == OpreateMode.DrawPolygon)
            {

                if (listPointPolygon.Count == 1)
                    addCircle(e, pLinePoint1, 4);
                else if (listPointPolygon.Count > 1 && bEndDrawPolygon == false)
                    for (int i = 0; i < listPointPolygon.Count - 1; i++)
                        addLine(e, listPointPolygon[i], listPointPolygon[i + 1]);
                else if (listPointPolygon.Count > 2 && bEndDrawPolygon == true)
                    addPolygon(e, listPointPolygon);
            }
        }
        void addGrid(PaintEventArgs e)
        {

            Graphics dc = e.Graphics;
            Font font = new Font("黑体", 8);
            Brush blueBrush = Brushes.Blue;
            Pen pen = new Pen(Color.LightBlue, 0.5F);
            for (int i = 1; i * 500 * cProjectData.fMapScale < this.panelWellNavigation.Width; i++)
            {
                int iXCurrentView = Convert.ToInt32(i * 500 * cProjectData.fMapScale);
                Point point1 = new Point(iXCurrentView, 0);
                Point point2 = new Point(iXCurrentView, this.panelWellNavigation.Height);
                dc.DrawLine(pen, point1, point2);
                dc.DrawString((cProjectData.dfMapXrealRefer + i * 500).ToString(), font, blueBrush, iXCurrentView, 0);
            }

            for (int i = 1; i * 500 * cProjectData.fMapScale < this.panelWellNavigation.Height; i++)
            {
                int iYCurrentView = Convert.ToInt32(i * 500 * cProjectData.fMapScale);
                Point point3 = new Point(0, iYCurrentView);
                Point point4 = new Point(this.panelWellNavigation.Width, iYCurrentView);
                dc.DrawLine(pen, point3, point4);
                dc.DrawString((cProjectData.dfMapYrealRefer - i * 500).ToString(), font, blueBrush, 0, iYCurrentView);
            }

            base.OnPaint(e);
        }

        void addWellPosion(PaintEventArgs e)
        {
            if (cProjectData.listProjectWell.Count > 0)
            {
                Graphics dc = e.Graphics;
                dc.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Font font = new Font("黑体", 8);
                foreach (ItemWell itemWell in cProjectData.listProjectWell)
                {
                    Pen wellPen = new Pen(Color.Black, 2);
                    if (itemWell.iWellType == 3) wellPen = new Pen(Color.Red, 2);
                    else if (itemWell.iWellType == 5) wellPen = new Pen(Color.Green, 2);
                    else if (itemWell.iWellType == 15) wellPen = new Pen(Color.Blue, 2);

                    Pen blackPen = new Pen(Color.Black, 1);
                    List<ItemWellPath> currentWellPath = itemWell.WellPathList;
                    Point headView = cCordinationTransform.transRealPointF2ViewPoint(
                     currentWellPath[0].dbX, currentWellPath[0].dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.fMapScale);
                    dc.DrawEllipse(wellPen, headView.X, headView.Y, 6, 6);

                    int iCount=currentWellPath.Count; 
                    if (iCount > 2) 
                    {
                        List<Point> points=new List<Point>();
                        for (int k = 0; k < iCount; k++)
                        {
                            Point tailView = cCordinationTransform.transRealPointF2ViewPoint(
                            currentWellPath[k].dbX, currentWellPath[k].dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.fMapScale);
                            points.Add(tailView);
                           
                        }
                     dc.DrawLines(blackPen, points.ToArray());
                    }
                    Brush blackBrush = Brushes.Black;
                    dc.DrawString(itemWell.sJH, font, blackBrush,
                                   headView.X+6, headView.Y+6);
                }
              
                

            }
     
            base.OnPaint(e);
        }
        private string getWellNameByScreenPoint(Point pScreen)
        {
            string sJHReturn = "";
            string[] split;
            if (File.Exists(cProjectManager.filePathWellNavigation))
            {
                using (StreamReader sr = new StreamReader(cProjectManager.filePathWellNavigation))
                {
                    String line;
                    int iLine = 0;
                    bool bFind = false;
                    while ((line = sr.ReadLine()) != null && bFind == false) //delete the line whose legth is 0
                    {
                        iLine++;
                        split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (iLine > 1)//第一行存的是比例尺
                        {
                            string sWellName = split[0];
                            int iXview = int.Parse(split[1]);
                            int iYview = int.Parse(split[2]);
                            if (Math.Abs(pScreen.X - iXview) <= 5
                                && Math.Abs(pScreen.Y - iYview) <= 5)
                            {
                                sJHReturn = sWellName;
                                bFind = true;
                            }
                        }

                    }
                }
            }
            return sJHReturn;

        }
        void addLine(PaintEventArgs e, Point p1, Point p2)
        {
            Graphics dc = e.Graphics;
            Pen BlackPen = new Pen(Color.Blue, 1);

            dc.DrawLine(BlackPen, p1, p2);

            Pen RedPen = new Pen(Color.Red, 1);
            int r = 4;
            dc.DrawEllipse(RedPen, p1.X - r / 2, p1.Y - r / 2, r, r);
            dc.DrawEllipse(RedPen, p2.X - r / 2, p2.Y - r / 2, r, r);
            base.OnPaint(e);
        }

        void addPolygon(PaintEventArgs e, List<Point> listPoint)
        {
            Graphics dc = e.Graphics;
            Pen BlackPen = new Pen(Color.Blue, 1);
            dc.DrawPolygon(BlackPen, listPoint.ToArray());

            //Pen RedPen = new Pen(Color.Red, 1);
            //int r = 4;
            //dc.DrawEllipse(RedPen, p1.X - r / 2, p1.Y - r / 2, r, r);
            //dc.DrawEllipse(RedPen, p2.X - r / 2, p2.Y - r / 2, r, r);
            base.OnPaint(e);
        }

        void addCircle(PaintEventArgs e, Point pR, int r)
        {
            Graphics dc = e.Graphics;

            Pen RedPen = new Pen(Color.Red, 1);
            dc.DrawEllipse(RedPen, pR.X, pR.Y, r, r);
            base.OnPaint(e);
        }

        void WellNavitationInvalidate()
        {

            if (cProjectData.ltStrProjectJH.Count > 0)
            { 
                int _iSacleRuler = 500; //定义网格单位

                if(cProjectData.fMapScale==0.1F){
                cProjectData.dfMapXrealRefer = Math.Floor(cProjectData.listProjectWell.Min(p => p.dbX) / _iSacleRuler - 1) * _iSacleRuler;
                cProjectData.dfMapYrealRefer = (Math.Ceiling(cProjectData.listProjectWell.Max(p => p.dbY) / _iSacleRuler) + 1) * _iSacleRuler;}

                double xMaxDistance = cProjectData.listProjectWell.Max(p => p.dbX) - cProjectData.listProjectWell.Min(p => p.dbX);
                double yMaxDistance = cProjectData.listProjectWell.Max(p => p.dbY) - cProjectData.listProjectWell.Min(p => p.dbY);

                int iPanelWidth = Convert.ToInt32(Math.Ceiling(xMaxDistance * cProjectData.fMapScale) + _iSacleRuler * 3 * cProjectData.fMapScale);//显示好看pannel比最大大3个网格
                int iPanelHeight = Convert.ToInt32(Math.Ceiling(yMaxDistance * cProjectData.fMapScale) + _iSacleRuler * 3 * cProjectData.fMapScale);//显示好看pannel比最大大3个网格
                panelWellNavigation.Dock = System.Windows.Forms.DockStyle.None;

                panelWellNavigation.Width = iPanelWidth;
                panelWellNavigation.Height = iPanelHeight;
                panelWellNavigation.Location = new Point(3, 3);

               
              
                this.panelWellNavigation.Invalidate();
                this.panelWellNavigation.Focus();
            }

        }

        private void tabPageWellNavigation_Click(object sender, EventArgs e)
        {
            WellNavitationInvalidate();
        }

        private void panelWellNavigation_MouseClick(object sender, MouseEventArgs e)
        {
            switch (currentOpreateMode)
            {
                case OpreateMode.Initial:
                    double xReal = cCordinationTransform.transXview2Xreal(e.X, cProjectData.dfMapXrealRefer, cProjectData.fMapScale);
                    double yReal = cCordinationTransform.transYview2Yreal(e.Y, cProjectData.dfMapYrealRefer, cProjectData.fMapScale);
                    Point pScreen = new Point(e.X, e.Y);
                    sJHselectedOnPanel = getWellNameByScreenPoint(pScreen);
                    this.tssLabelPosition.Text = sJHselectedOnPanel + " X=" + xReal.ToString("0.0") + " Y=" + yReal.ToString("0.0");
                    break;
                case OpreateMode.DrawLine:
                    iNumClickLineDraw++;
                    if (iNumClickLineDraw > 0 && iNumClickLineDraw % 2 == 1)
                    {
                        pLinePoint1.X = e.X;
                        pLinePoint1.Y = e.Y;
                    }
                    else if (iNumClickLineDraw > 0 && iNumClickLineDraw % 2 == 0)
                    {
                        pLinePoint2.X = e.X;
                        pLinePoint2.Y = e.Y;
                    }
                    this.panelWellNavigation.Invalidate();
                    break;
                case OpreateMode.DrawPolygon:
                    if (bEndDrawPolygon == false)
                        listPointPolygon.Add(new Point(e.X, e.Y));
                    this.panelWellNavigation.Invalidate();
                    break;
            }

        }
        private void panelWellNavigation_MouseDown(object sender, MouseEventArgs e)
        {
            switch (currentOpreateMode)
            {
                case OpreateMode.Initial:
                    if (e.Button == MouseButtons.Left)
                    {
                        this.Opoint.X = e.X;
                        this.Opoint.Y = e.Y;
                        this.Cursor = Cursors.Hand;
                    }
                    break;
                case OpreateMode.DrawLine:
                    //MessageBox.Show("DrawLine MouseMove");
                    break;
            }


        }
        private void panelWellNavigation_MouseMove(object sender, MouseEventArgs e)
        {

            switch (currentOpreateMode)
            {
                case OpreateMode.Initial:
                    if (e.Button == MouseButtons.Left)
                    {
                        this.panelWellNavigation.Left = this.panelWellNavigation.Left + e.X - this.Opoint.X;
                        this.panelWellNavigation.Top = this.panelWellNavigation.Top + e.Y - this.Opoint.Y;
                    }
                    break;
                case OpreateMode.DrawLine:
                    //MessageBox.Show("DrawLine MouseMove");
                    break;
            }

        }
        private void panelWellNavigation_MouseUp(object sender, MouseEventArgs e)
        {
            switch (currentOpreateMode)
            {
                case OpreateMode.Initial:
                    this.Cursor = Cursors.Default;
                    break;
                case OpreateMode.DrawLine:
                    //MessageBox.Show("DrawLine MouseMove");
                    break;
            }

        }
        private void panelWellNavigation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switch (currentOpreateMode)
            {
                case OpreateMode.Initial:
                    this.Cursor = Cursors.Default;
                    break;
                case OpreateMode.DrawPolygon:
                    bEndDrawPolygon = true;
                    this.panelWellNavigation.Invalidate();
                    break;
            }
        }

               private void geomodelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cIOgeomodel olGeomodel = new cIOgeomodel();
            olGeomodel.writeText();
        }

        private void tsmiGridParaSetting_Click(object sender, EventArgs e)
        {
            FormGridDefine formgriddefined = new FormGridDefine();
            formgriddefined.ShowDialog();
        }
        private void AddTreeNode(XmlNode xmlNode, TreeNode treeNode)
        {
            XmlNode newNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i;
            if (xmlNode.HasChildNodes)
            {
                nodeList = xmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    newNode = xmlNode.ChildNodes[i];
                    treeNode.Nodes.Add(new TreeNode(newNode.Name));
                    tNode = treeNode.Nodes[i];
                    AddTreeNode(newNode, tNode);
                }
            }
            else
            {
                treeNode.Text = (xmlNode.OuterXml).Trim();
            }
        }
        private void LoadTreeViewFromXmlFile(string filename, TreeView trv)
        {
            // Load the XML document.
            XmlDocument xml_doc = new XmlDocument();
            xml_doc.Load(filename);
            // Add the root node's children to the TreeView.
            trv.Nodes.Clear();
            AddTreeViewChildNodes(trv.Nodes, xml_doc.DocumentElement);
            trv.CollapseAll();
        }
        private void AddTreeViewChildNodes(TreeNodeCollection parent_nodes, XmlNode xml_node)
        {
            foreach (XmlNode child_node in xml_node.ChildNodes)
            {
                // Make the new TreeView node.
                TreeNode new_node = parent_nodes.Add(child_node.Name);

                // Recursively make this node's descendants.
                AddTreeViewChildNodes(new_node.Nodes, child_node);

                // If this is a leaf node, make sure it's visible.
                if (new_node.Nodes.Count == 0) new_node.EnsureVisible();
            }
        }

             


        private void tvProjectData_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0 && e.Node.Text == "井" )
            {  
                ltTV_SelectedJH.Clear();
                if (e.Node.Checked == true)
                {
                    foreach (TreeNode _tn in e.Node.Nodes) 
                    {
                        _tn.Checked = true;
                        if(_tn.Index>0)
                        ltTV_SelectedJH.Add(_tn.Text);  //0是global well log
                    }
                }
                if (e.Node.Checked == false)
                {
                    foreach (TreeNode _tn in e.Node.Nodes)
                    {
                        _tn.Checked = false;
                    }
                }
               
            };
            if (e.Node.Level == 1 && e.Node.Parent.Text == "井" && e.Node.Index == 0)
            {
                ltTV_SelectedLogNames.Clear();
                if (e.Node.Checked == true)
                {
                    foreach (TreeNode _tn in e.Node.Nodes)
                    {
                        _tn.Checked = true;
                        ltTV_SelectedLogNames .Add(_tn.Text);  
                    }
                }
                if (e.Node.Checked == false)
                {
                    foreach (TreeNode _tn in e.Node.Nodes)
                    {
                        _tn.Checked = false;
                    }
                }
            }
            //选择的井号
            if (e.Node.Level == 1&&e.Node.Parent.Text=="井"&& e.Node.Index > 0)
            {
                string _sJH = e.Node.Text;
                if (e.Node.Checked == true)
                {
                    if (ltTV_SelectedJH.IndexOf(_sJH) < 0) ltTV_SelectedJH.Add(_sJH);

                }
                else
                { if (ltTV_SelectedJH.IndexOf(_sJH) >= 0) ltTV_SelectedJH.Remove(_sJH); }
            }
            if (e.Node.Level == 2 && e.Node.Parent.Index == 0 && e.Node.Parent.Parent.Text == "井") 
            {
                string _logName = e.Node.Text;
                if (e.Node.Checked == true)
                {
                    if (ltTV_SelectedLogNames.IndexOf(_logName) < 0) ltTV_SelectedLogNames.Add(_logName);

                }
                else
                { if (ltTV_SelectedLogNames.IndexOf(_logName) >= 0) ltTV_SelectedLogNames.Remove(_logName); }
            
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cProjectData.ltStrProjectJH.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Yes 保存项目，No 放弃修改", "关闭工程",
                          MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    cProjectManager.saveProject();
                }
            }
            
        }

        private void tsmiWells_Click(object sender, EventArgs e)
        {
            tbcMain.TabPages.Add(tbgWellHead);
        }

        private void tsmiSaveAnotherProject_Click(object sender, EventArgs e)
        {
            cProjectManager.saveProeject2otherDirectionary();

        }

        private void tsmiPetrelWellTops_Click(object sender, EventArgs e)
        {
                cExportData4Petrel.exportWellTops();
        }

        private void tsmiWellTops_Click(object sender, EventArgs e)
        {
            tbcMain.TabPages.Add(tbgLayerSeriers); 
        }

        private void tsmi4petrelproductLog_Click(object sender, EventArgs e)
        {
            cExportData4Petrel.exportWellInterpretation();
        }

      

       

        

       





    }
}
