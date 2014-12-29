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

        void enableMenu() 
        {
            tsmiData.Enabled = true;
            tsmiGeologyLayer.Enabled = true;
            tsmiGeologySection.Enabled = true;
            tsmiWellConnect.Enabled = true;
            tsmiSaveAnotherProject.Enabled = true;
            tsmiSaveProject.Enabled = true;

            tsmiProfileDecision.Enabled = true;
            tsmiDynamicCal.Enabled = true;
        }

        private void intializeMyForm()
        {
            tvProjectData.ImageList = this.imageListMain;
            tvResultGraph.ImageList = this.imageListMain;
            tvResultTable.ImageList = this.imageListMain;
            listTabpageMain.Add(tbgWellNavigation);
            listTabpageMain.Add(tbgIE);
            listTabpageMain.Add(tbgWellHead);
            listTabpageMain.Add(tbgLayerSeriers);
            for (int i = 3; i >= 2; i--)
            {
                this.tbcMain.TabPages.Remove(tbcMain.TabPages[i]);
            }
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
            tscbbScale.Items.Clear();
            foreach (string sItem in listScale) tscbbScale.Items.Add(sItem);
            tscbbScale.SelectedIndex = 0;
        }


        private void updateMainForm()
        {
            tvProjectData.CheckBoxes = true;
            tvProjectData.Nodes.Clear();
            cTreeViewProjectData.setupTNwell(tvProjectData);
            cTreeViewProjectData.setupTNLayer(tvProjectData);
            cTreeViewProjectData.setupTNLayerChilds(tvProjectData);

            foreach (TreeNode tn in tvProjectData.Nodes) if (tn.Level == 0) tn.Expand();
            this.tbcMain.SelectedIndex = 0;
            this.tbcProject.SelectedIndex = 0;

            WellNavitationInvalidate();
            updateTreeViewWindows();
        }

        void updateTreeViewWindows()
        {
            tvWindows.CheckBoxes = true;
            tvWindows.Nodes.Clear();
            foreach (TabPage tbg in listTabpageMain)
            {
                TreeNode tabNote = new TreeNode();
                tabNote.Name = tbg.Name;
                tabNote.Text = tbg.Text;
                tvWindows.Nodes.Add(tabNote);
                if (tbcMain.TabPages.Contains(tbg)) tabNote.Checked = true;
            }
        }

        #region 工程管理
        bool createNewProject()
        {
            bool bCreated = false;
            if (cProjectManager.creatProject())
            {
                cProjectData.clearProjectData();
                showInputStaticGeologyTabpage();
                tvProjectData.Nodes.Clear();
                tbcMain.SelectedTab = tbgWellHead;
                this.ToolStripStatusLabelProjectionInfor.Text = "工程路径：" + cProjectManager.dirPathUserData;
                bCreated = true;
            }
            return bCreated;
        }

        private void tsBtnNewProject_Click(object sender, EventArgs e)
        {
            if (createNewProject()) { enableMenu(); updateMainForm(); }
        }

        private void tsBtnOpenProject_Click(object sender, EventArgs e)
        {
            if (openProject()) enableMenu(); 
        }
       
        private void tsmiNewProject_Click(object sender, EventArgs e)
        {
            if (createNewProject()) { enableMenu();  }
        }

        bool openProject()
        {
            bool opened = false;
             if (cProjectManager.loadProjectData())
            {
                this.ToolStripStatusLabelProjectionInfor.Text = "工程路径：" + cProjectManager.dirProject;
                updateMainForm();
                tscbbScale.Text = ((int)(1000 / cProjectData.dfMapScale)).ToString();
                WellNavitationInvalidate();
                opened = true;
                this.webBrowserIE.Navigate("about:blank");
                filePathWebSVG = "";
            }
             return opened;
        }
        private void tsmiOpenProject_Click(object sender, EventArgs e)
        {
            if (openProject()) enableMenu(); 
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
                if (tg.Parent == null && tbcMain.Contains(tg)==false)
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
            cIOinputWellHead.readWellHead2Project(this.dgvWellHead, cProjectManager.filePathInputWellhead);
            cIOinputWellHead.codeReplaceWellHead();

            cProjectData.ltStrProjectJH.Clear();
            foreach (string _sJH in cIOinputWellHead.getLtStrJH())
            {
                cProjectData.ltStrProjectJH.Add(_sJH);
                cProjectManager.createWellDir(_sJH);
            }
            cProjectData.setProjectWellsInfor();
            this.tbcMain.SelectedIndex = 0;
            updateMainForm();
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
            updateMainForm();
            this.tbcMain.SelectedIndex = 0;
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
            foreach (string _sJH in cProjectData.ltStrProjectJH) cIOinputJSJL.matchJSJL2Layer(_sJH);
        }
        private void calSplitJSJLWorkerMethod(object sender, WaitWindowEventArgs e)
        {
            foreach (string _sJH in cProjectData.ltStrProjectJH) cIOinputJSJL.splitJSJL2Layer(_sJH);
        }
        #endregion

        private void tsmiCalWellDistance_Click(object sender, EventArgs e)
        {
            FormCalWellDistance formCalDistance = new FormCalWellDistance();
            formCalDistance.Show();
        }
    
        private void tsmi注采关系分析_Click(object sender, EventArgs e)
        {
            FormInjProAna forminjectProductAna = new FormInjProAna();
            forminjectProductAna.Show();
        }

        private void tsmiCalWellTypeDictionary_Click(object sender, EventArgs e)
        {
            cIODicWellType.updateWellTypeDic();
            MessageBox.Show("计算完成。");
        }

        private void tsmiCalPerforationDictionary_Click(object sender, EventArgs e)
        {

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
                    cPublicMethodForm.read2DataGridViewByTextFile(cProjectManager.filePathInputWellhead, dgvWellHead);
            }

            if (this.tbcMain.SelectedTab.Name == this.tbgLayerSeriers.Name)
            {
                this.dgvLayerSeriers.Height = this.tbcMain.Height - 100;
                if (File.Exists(cProjectManager.filePathInputLayerSeriers))
                    cPublicMethodForm.read2DataGridViewByTextFile(cProjectManager.filePathInputLayerSeriers, dgvLayerSeriers); 
            }

        }

        private void tsmiDeleteSelectedWellInPanel_Click(object sender, EventArgs e)
        {
            if (sJHselectedOnPanel != "")
            {
                cProjectManager.delWellFromProject(sJHselectedOnPanel);
                updateMainForm(); 
            }
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

        private void tsmiLayerInjectProductSystem_Click(object sender, EventArgs e)
        {
            FormInjProMap formInjProSystemMap = new FormInjProMap();
            formInjProSystemMap.Show();
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

        void updateTreeViewProjectGraph()
        {
            string dir = cProjectManager.dirPathMap;
            cPublicMethodForm.ListDirectory(tvResultGraph, dir);
            tvResultGraph.ExpandAll();
        }

        private void tabControlProject_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tbcProject.SelectedTab == tbgResultGraph && cProjectManager.dirProject != Path.GetTempPath()) //选择了图形tbg
            {  
                updateTreeViewProjectGraph();
            }
            if (tbcProject.SelectedTab == this.tbgResultTable )
            {
                List<string> filenames = Directory.GetFiles(cProjectManager.dirPathUsedProjectData, "*.txt").ToList();
                this.tvResultTable.Nodes.Clear();
                foreach (string item in filenames)
                {
                    TreeNode _tn = new TreeNode(Path.GetFileNameWithoutExtension(item), 6, 6);
                    this.tvResultTable.Nodes.Add(_tn);
                }
            }
        }



        void updateWebSVG()
        {
            this.tbcMain.SelectedTab = tbgIE;

            try
            {
                if (filePathWebSVG.EndsWith(".svg"))
                {
                    this.webBrowserIE.Navigate(new Uri(filePathWebSVG));
                    this.tbgIE.Text =Path.GetFileNameWithoutExtension(filePathWebSVG);
                }
                else
                {
                    this.tbcMain.SelectedTab = tbgWellNavigation;
                }
            }
            catch (System.UriFormatException)
            {
                MessageBox.Show("UriFormatException error.");
            }

        }

     

        private void tsBtnZoonIn_Click(object sender, EventArgs e)
        {
            if (tbcMain.SelectedIndex == 0)
            {
                cProjectData.dfMapScale = cProjectData.dfMapScale * 1.2F;
                tscbbScale.Text = (1000.0 / cProjectData.dfMapScale).ToString("0");
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
                cProjectData.dfMapScale = cProjectData.dfMapScale * 0.8F;
                tscbbScale.Text = (1000.0 / cProjectData.dfMapScale).ToString("0");
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
            if (tscbbScale.SelectedIndex >= 0)
            {
                float dfscale = float.Parse(tscbbScale.SelectedItem.ToString());
                cProjectData.dfMapScale = 1000 / dfscale;
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
                          case "tnWells": // //当前井管理
                            //右键菜单
                           cContextMenuStripInputWellsManager cCMSwells = new cContextMenuStripInputWellsManager(cmsProject, selectNode);
                            cCMSwells.setupContextMenuWellMangager();
                            break; 
                        case "tnWellTops": //当前选中小层管理
                            cContextMenuStripInputLayer cmsWellTops = new cContextMenuStripInputLayer(cmsProject, selectNode, selectNode.Text);
                            cmsWellTops.setupTsmi();
                            break;
                    }
                    break;
                case 1://第2级菜单
                    if (selectNode.Parent.Text == "井" && selectNode.Index > 0) //当前选中井，index=0 是全局测井曲线
                    {
                        //右键快捷菜单配置
                        string _sJH = selectNode.Text;
                        cContextMenuStripInputWell cCMSinputWell = new cContextMenuStripInputWell(cmsProject, selectNode, _sJH);
                        cCMSinputWell.setupTsmi();
                    }
                    if (selectNode.Parent.Text == "井" && selectNode.Index == 0)  //当前 全局测井曲线
                    {
                        cContextMenuStripInputWellLog cTS = new cContextMenuStripInputWellLog(cmsProject, selectNode, selectNode.Parent.Text);
                        cTS.setupTsmiExportManyWellsLog();
                    }
                    if (selectNode.Parent.Name == "tnWellTops")
                    {
                        cContextMenuStripInputLayer cCMSinputLayer = new cContextMenuStripInputLayer(cmsProject, selectNode, selectNode.Text);
                        cCMSinputLayer.setupTsmi();
                    }
                    break;
                case 2://第3级菜单，右键快捷菜单配置

                    if (selectNode.Text == "well logs")
                    {
                        cContextMenuStripInputWellLog cTS = new cContextMenuStripInputWellLog(cmsProject, selectNode, selectNode.Parent.Text);
                        cTS.setupContextMenuStripWellLog();
                        cTreeViewProjectData.setupTNWellLog(selectNode, selectNode.Parent.Text);
                    }
                    break;
                case 3://第4级菜单，右键快捷菜单配置
                    if (selectNode.Parent.Text == "well logs")
                    {
                        cContextMenuStripLogItem cTS = new cContextMenuStripLogItem(cmsProject, selectNode, selectNode.Parent.Parent.Text);
                        cTS.setupLogItem();
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
            mainMenuStrip.setupTsmiTools();
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
            for (int i = 1; i * 500 * cProjectData.dfMapScale < this.panelWellNavigation.Width; i++)
            {
                int iXCurrentView = Convert.ToInt32(i * 500 * cProjectData.dfMapScale);
                Point point1 = new Point(iXCurrentView, 0);
                Point point2 = new Point(iXCurrentView, this.panelWellNavigation.Height);
                dc.DrawLine(pen, point1, point2);
                dc.DrawString((cProjectData.dfMapXrealRefer + i * 500).ToString(), font, blueBrush, iXCurrentView, 0);
            }

            for (int i = 1; i * 500 * cProjectData.dfMapScale < this.panelWellNavigation.Height; i++)
            {
                int iYCurrentView = Convert.ToInt32(i * 500 * cProjectData.dfMapScale);
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
                    List<ItemDicWellPath> currentWellPath = itemWell.WellPathList;
                    Point headView = cCordinationTransform.transRealPointF2ViewPoint(
                     currentWellPath[0].dbX, currentWellPath[0].dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                    dc.DrawEllipse(wellPen, headView.X, headView.Y, 6, 6);

                    int iCount = currentWellPath.Count;
                    if (iCount > 2)
                    {
                        List<Point> points = new List<Point>();
                        for (int k = 0; k < iCount; k++)
                        {
                            Point tailView = cCordinationTransform.transRealPointF2ViewPoint(
                            currentWellPath[k].dbX, currentWellPath[k].dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                            points.Add(tailView);

                        }
                        dc.DrawLines(blackPen, points.ToArray());
                    }
                    Brush blackBrush = Brushes.Black;
                    dc.DrawString(itemWell.sJH, font, blackBrush,
                                   headView.X + 6, headView.Y + 6);
                }

            }
            base.OnPaint(e);
        }
        private string getWellNameByScreenPoint(Point pScreen)
        {
            foreach(ItemWell item in cProjectData.listProjectWell)
            {
                Point  itemView = cCordinationTransform.transRealPointF2ViewPoint(
                     item.dbX, item.dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                if (Math.Abs(pScreen.X - itemView.X) <= 5 && Math.Abs(pScreen.Y - itemView.Y) <= 5) return item.sJH; 
            }
            return "";
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
                if (cProjectData.dfMapScale == 0) cProjectData.dfMapScale =0.1;
               
                    cProjectData.dfMapXrealRefer = Math.Floor(cProjectData.listProjectWell.Min(p => p.dbX) / _iSacleRuler - 1) * _iSacleRuler;
                    cProjectData.dfMapYrealRefer = (Math.Ceiling(cProjectData.listProjectWell.Max(p => p.dbY) / _iSacleRuler) + 1) * _iSacleRuler;

                double xMaxDistance = cProjectData.listProjectWell.Max(p => p.dbX) - cProjectData.listProjectWell.Min(p => p.dbX);
                double yMaxDistance = cProjectData.listProjectWell.Max(p => p.dbY) - cProjectData.listProjectWell.Min(p => p.dbY);

                int iPanelWidth = Convert.ToInt32(Math.Ceiling(xMaxDistance * cProjectData.dfMapScale) + _iSacleRuler * 3 * cProjectData.dfMapScale);//显示好看pannel比最大大3个网格
                int iPanelHeight = Convert.ToInt32(Math.Ceiling(yMaxDistance * cProjectData.dfMapScale) + _iSacleRuler * 3 * cProjectData.dfMapScale);//显示好看pannel比最大大3个网格
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
                    double xReal = cCordinationTransform.transXview2Xreal(e.X, cProjectData.dfMapXrealRefer, cProjectData.dfMapScale);
                    double yReal = cCordinationTransform.transYview2Yreal(e.Y, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
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

       

        private void tvProjectData_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0 && e.Node.Text == "井")
            {
                ltTV_SelectedJH.Clear();
                if (e.Node.Checked == true)
                {
                    foreach (TreeNode _tn in e.Node.Nodes)
                    {
                        _tn.Checked = true;
                        if (_tn.Index > 0) ltTV_SelectedJH.Add(_tn.Text);  //0是global well log
                    }
                }
                if (e.Node.Checked == false)
                {
                    foreach (TreeNode _tn in e.Node.Nodes) _tn.Checked = false;
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
                        ltTV_SelectedLogNames.Add(_tn.Text);
                    }
                }
                if (e.Node.Checked == false)
                {
                    foreach (TreeNode _tn in e.Node.Nodes) _tn.Checked = false;
                }
            }
            //选择的井号
            if (e.Node.Level == 1 && e.Node.Parent.Text == "井" && e.Node.Index > 0)
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
        private void tvwProjectGraph_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tvResultGraph.ContextMenuStrip = cmsProject;
            TreeNode selectNode = tvResultGraph.SelectedNode;
            cmsProject.Items.Clear();

            switch (selectNode.Level)
            {
                case 0:
                    break;
                case 1:
                    cContextMenuStripSVGGraph cTS = new cContextMenuStripSVGGraph(cmsProject, selectNode, selectNode.Text);
                    cTS.setupTsmiOpenInInkscape();
                    cTS.setupTsmiRename();
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

            filePathWebSVG = Path.Combine(cProjectManager.dirPathMap, tvResultGraph.SelectedNode.Text);
            updateWebSVG();

        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cProjectData.ltStrProjectJH.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Yes 保存项目，No 放弃修改", "关闭工程", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) cProjectManager.saveProject();
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

        private void tsmiFence_Click(object sender, EventArgs e)
        {
            FormWellsGroup formFD = new FormWellsGroup();
            formFD.Show();
        }

        private void tsmiSection_Click(object sender, EventArgs e)
        {
            FormWellSectionGeology formReserviorSection = new FormWellSectionGeology();
            formReserviorSection.ShowDialog();
            updateWebSVG();
            updateTreeViewProjectGraph();
        }

        private void tsmiWellpathSection_Click(object sender, EventArgs e)
        {
            FormWellSectionPath FormWellsGroup = new FormWellSectionPath();
            FormWellsGroup.ShowDialog();
            updateWebSVG();
             updateTreeViewProjectGraph();
        }

        private void tsmiJSJLmatch_Click(object sender, EventArgs e)
        {
            WaitWindow.Show(this.calMatchJsJlWorkerMethod);
            MessageBox.Show("计算完成。");
        }

        private void tsmiJSJLsplit_Click(object sender, EventArgs e)
        {
            WaitWindow.Show(this.calSplitJSJLWorkerMethod);
            MessageBox.Show("计算完成。");
        }

        private void tvProjectData_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string oldNoteText = e.Node.Text;  //原来的测井名
            string sJH = e.Node.Parent.Parent.Text;
            this.BeginInvoke(new Action(() => afterAfterEditLogName(sJH, oldNoteText, e.Node)));
        }
        private void afterAfterEditLogName(string sJH,string oldNoteText, TreeNode node)
        {
            cIOinputLog.renameProjectLogFile(sJH, oldNoteText, node.Text);
        }

        private void tvProjectData_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
            if (e.Node.Level == 3) e.CancelEdit = false;
        }

        private void tvProjectGraph_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
            if (e.Node.Level > 0) e.CancelEdit = false;
        }

        private void tvProjectGraph_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string oldNoteText = e.Node.Text;  //原来的测井名
            this.BeginInvoke(new Action(() => afterAfterEditGraphName( oldNoteText, e.Node)));
        }

        private void afterAfterEditGraphName( string oldNoteText, TreeNode node)
        {
            string oldfilepath = Path.Combine(cProjectManager.dirPathMap, oldNoteText);
            string newfilepath = Path.Combine(cProjectManager.dirPathMap, node.Text);
            File.Move(oldfilepath, newfilepath);
            filePathWebSVG = Path.Combine(cProjectManager.dirPathMap, tvResultGraph.SelectedNode.Text);
            updateWebSVG();
        }

        private void tsmiAdjustProfile_Click(object sender, EventArgs e)
        {
            FormProfileSelectWells _form = new FormProfileSelectWells();
            _form.Show();
        }

        private void tsmiSectionFence_Click(object sender, EventArgs e)
        {
            FormWellsGroup _form = new FormWellsGroup();
            _form.ShowDialog();
            updateWebSVG();
            updateTreeViewProjectGraph();
        }

        private void tsmiNewWell_Click(object sender, EventArgs e)
        {
            FormWellInfor form = new FormWellInfor("newWell");
            form.ShowDialog();
        }

        private void tsBtnReflush_Click(object sender, EventArgs e)
        {
            if (cProjectManager.dirProject != Path.GetTempPath())
            {
                if (tbcMain.SelectedTab == tbgWellNavigation)
                {
                    WellNavitationInvalidate();
                    cTreeViewProjectData.updateTN_GlobeWellLog(this.tvProjectData);
                    cTreeViewProjectData.setupTNLayerChilds(this.tvProjectData);
                }
                if (tbcMain.SelectedTab == tbgIE) this.webBrowserIE.Refresh();
            }
        }

        private void tsBtnDataManager_Click(object sender, EventArgs e)
        {
            if (cProjectManager.dirProject != Path.GetTempPath())
            {
            FormImportProjectData frmImportProject = new FormImportProjectData();
            frmImportProject.ShowDialog();
            cProjectData.setProjectWellsInfor();
            }
        }

        private void tsBtnDataView_Click(object sender, EventArgs e)
        {
            if (cProjectManager.dirProject != Path.GetTempPath())
            {
                FormDataViewSingleWell formDataView = new FormDataViewSingleWell(cProjectData.ltStrProjectJH[0]);
                formDataView.Show();
            }
     
        }


        private void tvWindows_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = e.Node;
            //if (tn.Checked == false && tn.Index >= 2)
            //{
            //    foreach (TabPage tbg in tbcMain.TabPages) if (tbg.Name == tn.Name) tbcMain.TabPages.Remove(tbg);
            //}
            //if (tn.Checked == true && tn.Index >= 2)
            //{
            //    foreach (TabPage tbg in tbcMain.TabPages) if (tbg.Name == tn.Name) tbcMain.TabPages.Add(tbg);
            //}

        }

        private void tsmiPIcal_Click(object sender, EventArgs e)
        {
            FormProfilePIcal form = new FormProfilePIcal();
            form.Show();
        }

        private void tsmiInjProConnect_Click(object sender, EventArgs e)
        {
            FormInjProAna form = new FormInjProAna();
            form.Show();
        }

        private void tsmiCloseInput_Click(object sender, EventArgs e)
        {
            tbcMain.TabPages.Remove(tbgWellHead);
            tbcMain.TabPages.Remove(tbgLayerSeriers);
        }

         

      

       

      
       

    }
}
