using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using DOGPlatform.XML;
using DOGPlatform.SVG;

namespace DOGPlatform
{///
    /// 逻辑顺序：1 选择顶部小层名 2 选择底部显示小层名 3 选择时间（主要考虑不同时间井型的变化）
    /// 根据 小层名 筛选井号 存入listWellsMaper,显示时 应该显示选择层段顶的井位（或者提供可选），需要考虑斜井及断失的情况
    /// 
    /// 井确定后，可以叠加 不同的数据 例如 井点的属性啊，断层信息等。
    ///
    public partial class FormMapLayer : Form
    {
        List<ItemWellMapLayer> listWellsMapLayer = new List<ItemWellMapLayer>();
        List<string> ltStrSelectedLayers = new List<string>();
        string selectedLayer;
       
        XmlElement returnElemment;
       

        //set xml 4 store configure information and data
        string filePathXMLconfigLayerMap = "";
    

        public FormMapLayer()
        {
            InitializeComponent();
            InitFormLayerMap();
        }

        private void InitFormLayerMap()
        {
          //  cPublicMethodForm.inialListBox(lbxJHLayerMap, cProjectData.ltStrProjectJH);
            lbxJHLayerMap.DataSource = cProjectData.ltStrProjectJH;
            List<string> ltStrStaticDataChoise = new List<string>();
            ltStrStaticDataChoise.Add("砂厚");
            ltStrStaticDataChoise.Add("有效厚度");
            ltStrStaticDataChoise.Add("孔隙度");
            ltStrStaticDataChoise.Add("渗透率");
            ltStrStaticDataChoise.Add("饱和度");
            cbbSelectedXCMTop.DataSource = cProjectData.ltStrProjectXCM;
            cbbSelectedXCMBot.DataSource = cProjectData.ltStrProjectXCM;
            cbbSelectedYM.DataSource = cProjectData.ltStrProjectYM;
            cbbLeftLogName.DataSource  = cProjectData.ltStrLogSeriers;
            cbbRightLogName.DataSource = cProjectData.ltStrLogSeriers;
            cPublicMethodForm.inialListBox(lbxJH, cProjectData.ltStrProjectJH);
        } 
        private void cbbSelectedXCM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSelectedXCMBot.Items.Count > 0)
                this.cbbSelectedXCMBot.SelectedIndex = this.cbbSelectedXCMTop.SelectedIndex;
        }

        void addWells()
        {
   
            cXMLLayerMapBase.delJHDataNode(filePathXMLconfigLayerMap);
            foreach (var item in listWellsMapLayer)
                cXMLLayerMapBase.addJHDataNode2XML(filePathXMLconfigLayerMap, ItemWellMapLayer.item2string(item)) ;
            
            cPublicMethodForm.setListBoxwithltStr(lbxJHLayerMap, listWellsMapLayer.Select(p => p.sJH).ToList());

        }

        void addWellProperty() 
        {
            List<string> ltValue = listWellsMapLayer.Select(p => p.sJH+"\t"+p.dbX +"\t"+p.dbY+"\t"+p.fDCHD.ToString() 
                + "\t" + p.fSH.ToString() + "\t" + p.fYXHD.ToString()+"\t"+p.fSTL.ToString()).ToList() ;
            cXMLLayerMapGeoproperty.delWellPropertyNode(filePathXMLconfigLayerMap);
            cXMLLayerMapGeoproperty.addWellProperty2XML(filePathXMLconfigLayerMap, ltValue); 
        }

        void addHorizonal() 
        {
            XmlDocument xmlLayerMap = new XmlDocument();
            xmlLayerMap.Load(this.filePathXMLconfigLayerMap);
            XmlNode currentNode = xmlLayerMap.SelectSingleNode("/LayerMapConfig/HorizonalWell");
            XmlNodeList listIntervel = currentNode.SelectNodes("WellIntervel");

            for (int i = 0; i < listIntervel.Count; i++)
            {
                string _sInnerText = listIntervel[i].InnerText;
                string[] splitInnerText = _sInnerText.Split();
                string _sJH = splitInnerText[0];
                int _iWellType = int.Parse(splitInnerText[1]);

                Point pWellHead = new Point(int.Parse(splitInnerText[2]), int.Parse(splitInnerText[3]));
                Point pA = new Point(int.Parse(splitInnerText[4]), int.Parse(splitInnerText[5]));
                Point pB = new Point(int.Parse(splitInnerText[6]), int.Parse(splitInnerText[7]));
                //
                //returnElemment = this.svgLayerMap.gHorizonalWellIntervelLine(pWellHead ,pA,pB);
                //svgLayerMap.addgElement(returnElemment, 0, 0);
            }
        }


        void addFaultLine() 
        {
        
        }

        void addCoutour() 
        {
        
        }

        //从XML文件中读取数据，利用SVG增加g，在原始SVG图上插入新内容
        void reDrawSVGMap()
        {
            //从xml配置文件中解析数据
           

            generateSVGfilemap();

            //插入原SVG
            //openSVGInIE();
            //重绘生成
        }


        void generateSVGfilemap()
        {

            // 这块重新加载的问题 解决了 绘制的问题，重新加载仍旧需要解决一下

            string filePathSVGLayerMap = filePathXMLconfigLayerMap.Replace(".xml", ".svg");


            cSVGDocLayerMap svgLayerMap=new cSVGDocLayerMap(filePathXMLconfigLayerMap,50, 50);;
          
            if (File.Exists(filePathSVGLayerMap)) File.Delete(filePathSVGLayerMap);

            returnElemment = svgLayerMap.gWellsPosition();

            svgLayerMap.addgElement(returnElemment, 0, 0);
            if (this.cbxScaleRulerShowed.Checked == true)
            {
                returnElemment = svgLayerMap.gScaleRuler( 0, 0);
                svgLayerMap.addgElement(returnElemment, 100, 100);
            }

            if (this.cbxAddGeologyProperty.Checked == true)
            {
                returnElemment = svgLayerMap.gWellsGeologyProperty();
                svgLayerMap.addgElement(returnElemment, 0, 0);
            }

            if (this.cbxMapFrame.Checked == true)
            {
                returnElemment = svgLayerMap.gMapFrame(this.cbxGrid.Checked);
                svgLayerMap.addgElement(returnElemment, 0, 0);
            }

            if (this.cbxCompassShowed.Checked == true)
            {
                svgLayerMap.svgRoot.AppendChild(svgLayerMap.gCompass(300, 100));
            }
            svgLayerMap.makeSVGfile(filePathSVGLayerMap);
            FormMain.filePathWebSVG = filePathSVGLayerMap;
            this.Close();
        }

        void openSVGInIE(string filePath)
        {
            Form f = Application.OpenForms["FormWebNavigation"];  //查找是否打开过Form窗体  
            if (f == null)  //没打开过  
            {

            }
            else
            {
                f.Close();
            }
            FormWebNavigation formSVGView = new FormWebNavigation(filePath);
            formSVGView.Show();
        }
        private void btnMakeLayerMap_Click(object sender, EventArgs e)
        {
           
            generateSVGfilemap();

            /*       

                    //增加左侧测井曲线
                    if (cbxAddLeftLog.Checked == true)
                    {
                        foreach (string sJH in ltStrSelectedJHMapLayer)
                        {
                            int iIndexFound = cLayerMap.ltStrJHMapLayer_WellPositionSVG.IndexOf(sJH);
                            string filepathWellLeftLog = cProjectManager.dirPathTemp + "mapLeftLog\\" + sJH + ".txt";
                            if (File.Exists(filepathWellLeftLog))
                            {
                                string sLogName="";
                                List<float> fListMD = new List<float>();
                                List<float> fListLogValue = new List<float>();
                                using (StreamReader sr = new StreamReader(filepathWellLeftLog, Encoding.Default))
                                {
                                    string line;
                                    string[] split;
                                    int iLineIndex = 0;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        iLineIndex++;

                                        split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        if (iLineIndex == 1) sLogName = split[1];
                                        else if (iLineIndex > 1)
                                        {
                                            fListMD.Add(float.Parse(split[0]));
                                            fListLogValue.Add(float.Parse(split[1]));
                                        }
                                    }
                                }
                                int iLeftValue = Convert.ToInt16(nUDLeftLogLeftValue.Value);
                                int iRightValue = Convert.ToInt16(nUDLeftLogRightValue.Value);
                                string sLeftLogColor = filePathXMLconfigLayerMap.Element("LayerMapConfig").Element("LogFace").Element("leftLogColor").Value;
                                returnElemment = cLayerMap.gTrackLog(sLogName, fListMD, fListLogValue, iLeftValue, iRightValue, sLeftLogColor);
                                cLayerMap.addgElement(returnElemment, cLayerMap.iListX_WellPositionSVG[iIndexFound]-20, cLayerMap.iListY_WellPositionSVG[iIndexFound]);
                            }
                        }
                    }

                    //增加右侧测井曲线
                    if (cbxAddRightLog.Checked == true)
                    {
                        foreach (string sJH in ltStrSelectedJHMapLayer)
                        {
                            int iIndexFound = cLayerMap.ltStrJHMapLayer_WellPositionSVG.IndexOf(sJH);
                            string filepathWellRightLog = cProjectManager.dirPathTemp + "mapRightLog\\" + sJH + ".txt";
                            if (File.Exists(filepathWellRightLog))
                            {
                                string sLogName = "";
                                List<float> fListMD = new List<float>();
                                List<float> fListLogValue = new List<float>();
                                using (StreamReader sr = new StreamReader(filepathWellRightLog, Encoding.Default))
                                {
                                    string line;
                                    string[] split;
                                    int iLineIndex = 0;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        iLineIndex++;

                                        split = line.Trim().Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        if (iLineIndex == 1) sLogName = split[1];
                                        else if (iLineIndex > 1)
                                        {
                                            fListMD.Add(float.Parse(split[0]));
                                            fListLogValue.Add(float.Parse(split[1]));
                                        }

                                    }

                                }
                                int iLeftValue = Convert.ToInt16(nUDRightLogLeftValue.Value);
                                int iRightValue = Convert.ToInt16(nUDRightLogRightValue.Value);
                                returnElemment = cLayerMap.gTrackLog(sLogName, fListMD, fListLogValue, iLeftValue, iRightValue, "red");
                                cLayerMap.addgElement(returnElemment, cLayerMap.iListX_WellPositionSVG[iIndexFound], cLayerMap.iListY_WellPositionSVG[iIndexFound]);
                            }
                        }
                    }

                        
           
                    */


        }
        private void btnSelectAllJH_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbxJHLayerMap.Items.Count; i++)
            {
                lbxJHLayerMap.SetSelected(i, true);
            }
        }
        private void btnSelectNoWell_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbxJHLayerMap.Items.Count; i++)
            {
                lbxJHLayerMap.SetSelected(i, false);
            }
        }
        private void lbxJHLayerMap_SelectedValueChanged(object sender, EventArgs e)
        {
            this.lblNumSelectedWells.Text = "选择井数:" + this.lbxJHLayerMap.SelectedItems.Count.ToString();
        }


        private void btnSelectWells_Click(object sender, EventArgs e)
        {
          
        }

        private void btnWellNameFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                this.lbl_JMFont.ForeColor = fontDialog.Color;
                this.lbl_JMFont.Font = fontDialog.Font;
            }
        }



        private void btnPinchLine_Click(object sender, EventArgs e)
        {
            string filePathSVGLayerMap = "PinchSVG";

            cSVGDocLayerMap cLayerMap = new cSVGDocLayerMap(800, 1000, 0, 0);
            cLayerMap.makeSVGfile(cProjectManager.dirPathMap + filePathSVGLayerMap);
            FormWebNavigation formSVGView = new FormWebNavigation(cProjectManager.dirPathMap + filePathSVGLayerMap); formSVGView.Show();
        }

        private void btnSandBody_Click(object sender, EventArgs e)
        {
            string filePathSVGLayerMap = "SandBody.svg";

            cSVGDocLayerMap cLayerMap = new cSVGDocLayerMap(800, 1000, 0, 0);
            //XmlElement returnElemment;
            string dPath = "M50,50 Q50,100 100,100z";
            cLayerMap.addgElement(cLayerMap.gSandBody(dPath), 200, 200);

            cLayerMap.makeSVGfile(cProjectManager.dirPathMap + filePathSVGLayerMap);
            FormWebNavigation formSVGView = new FormWebNavigation(cProjectManager.dirPathMap + filePathSVGLayerMap); formSVGView.Show();
        }

        private void nUDWellCircle_R_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapBase.setRdiusValueWellCircle(filePathXMLconfigLayerMap, Convert.ToInt16(nUDWellCircle_R.Value));
        }

        private void nUDJHtext_DX_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapBase.setJH_Dxoffset(filePathXMLconfigLayerMap, Convert.ToInt16(nUDJHtext_DX.Value));
        }

        private void nUDJHFontSize_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapBase.setJHsize(filePathXMLconfigLayerMap, Convert.ToInt16(nUDJHFontSize.Value));
        }

        private void nUDFaultLineWidth_ValueChanged(object sender, EventArgs e)
        {
            XDocument filePathXMLconfigLayerMap = XDocument.Load(cProjectManager.xmlConfigLayerMap);
            filePathXMLconfigLayerMap.Element("LayerMapConfig").Element("FaultLine").Element("lineWidth").Value = nUDFaultLineWidth.Value.ToString("0");
            filePathXMLconfigLayerMap.Save(cProjectManager.xmlConfigLayerMap);
        }

        private void btnLayerMapConfig_Click(object sender, EventArgs e)
        {
            if (File.Exists(cProjectManager.xmlConfigLayerMap) == true)
            {
                File.Delete(cProjectManager.xmlConfigLayerMap);
                cXMLLayerMapBase.creatLayerMapConfigXML(cProjectManager.xmlConfigLayerMap,1000,1000);
            }
        }

        private void nUDLayerGeologyProperyFontSize_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapGeoproperty.setLayerGeoglogyPropertyTextsize(filePathXMLconfigLayerMap, Convert.ToInt16(nUDLayerGeologyProperyFontSize.Value));
        }

        private void nUDLayerGeologyProperyDyOffset_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapGeoproperty.setLayerGeoglogyProperty_Dyoffset(filePathXMLconfigLayerMap, Convert.ToInt16(nUDLayerGeologyProperyDyOffset.Value));
        }

        private void nUDLayerGeologyProperyDxOffset_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapGeoproperty.setLayerGeoglogyProperty_Dxoffset(filePathXMLconfigLayerMap, Convert.ToInt16(nUDLayerGeologyProperyDxOffset.Value));
        }

        private void generateLogmapData(object sender, WaitWindowEventArgs e)
        {
                      //List<ItemLayerDepth> itemsLayerDepth = new List<ItemLayerDepth>();
            //cIOinputLayerDepth fileLayerDepth = new cIOinputLayerDepth();
            //for (int i = 0; i < ltStrSelectedJHMapLayer.Count; i++)
            //{
            //    ItemLayerDepth layerDepthItem = new ItemLayerDepth();
            //    //去掉找不到井和层位信息的数据
            //    if (layerDepthItem.sJH != null) itemsLayerDepth.Add(layerDepthItem);

            //}
            //if (cbbLeftLogName.SelectedIndex >= 0)
            //{
            //    string sLeftLogName = cbbLeftLogName.SelectedItem.ToString();
            //    string dirLeftMaplog = cProjectManager.dirPathTemp + "mapLeftLog";
            //    generateLogFaceData(sLeftLogName, dirLeftMaplog, itemsLayerDepth);
            //}
            //if (cbbRightLogName.SelectedIndex >= 0)
            //{
            //    string sRightLogName = cbbRightLogName.SelectedItem.ToString();
            //    string dirRightMaplog = cProjectManager.dirPathTemp + "mapRightLog";
            //    generateLogFaceData(sRightLogName, dirRightMaplog, itemsLayerDepth);
            //}
        }



        private void btnAddLogFaceData_Click(object sender, EventArgs e)
        {
            WaitWindow.Show(this.generateLogmapData);

        }

        private void FormMapLayer_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void nUDLogfVScale_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapLog.setLogFaceVScale(filePathXMLconfigLayerMap, Convert.ToSingle(nUDLogfVScale.Value));
        }

        private void nUDTrackWidth_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapLog.setLogFaceTrackWidth(filePathXMLconfigLayerMap, Convert.ToInt16(nUDTrackWidth.Value));
        }

        private void nUDLogLineWidth_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapLog.setLogFaceLineWidth(filePathXMLconfigLayerMap, Convert.ToSingle(nUDLogLineWidth.Value));
        }

             private void nUDStaticDatadfscale_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapBase.setStaticDataVScale(filePathXMLconfigLayerMap, Convert.ToSingle(nUDStaticDatadfscale.Value));
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            this.generateSVGfilemap();
        }

        private void cbxAddHorizonWell_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAddHorizonWell.Checked == true)
            {
                //add svg文件中水平井段
                if (File.Exists(filePathXMLconfigLayerMap))
                {
                    cXMLLayerMapHorizonalWell.delHorizonalWellIntervalNode(this.filePathXMLconfigLayerMap);
                    List<ItemHorizonalWellPath> ltHorizonalWellPath = cIOinputHorizionalWellPath.readHozironalWellPath2Struct();
                    List<string> _ltStrJHMapLayer = ltHorizonalWellPath.Select(x => x.sJH).Distinct().ToList();
                    List<string> _ltStrJHNOWellHead = new List<string>();
                    foreach (string _sjh in _ltStrJHMapLayer)
                    {
                        //井必须在project井范围内
                        if (cProjectData.ltStrProjectJH.IndexOf(_sjh) >= 0)
                        {
                            ItemHorizonalWellPath head = ltHorizonalWellPath.Find(x => x.sJH == _sjh);
                            ItemHorizonalWellPath tail = ltHorizonalWellPath.FindLast(x => x.sJH == _sjh);
                            // 井号+ 井型 + 井口view坐标 + head view 坐标 + tail view 坐标 

                            List<string> _ltStrData = new List<string>();
                            _ltStrData.Add(_sjh);

                            ItemWellMapLayer item = this.listWellsMapLayer.Find(x => x.sJH == _sjh);
                            if (item != null)
                            {
                           //     _ltStrData.Add(item.iWellType.ToString());
                           //     _ltStrData.Add(item.iXview.ToString());
                           //     _ltStrData.Add(item.iYview.ToString());

                           //     Point headView =
                           //cPublicMethodCordinationTransform.transRealPointF2ViewPoint(head.dbX, head.dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                           //     _ltStrData.Add(headView.X.ToString());
                           //     _ltStrData.Add(headView.Y.ToString());
                           //     Point tailView =
                           //cPublicMethodCordinationTransform.transRealPointF2ViewPoint(tail.dbX, tail.dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                           //     _ltStrData.Add(tailView.X.ToString());
                           //     _ltStrData.Add(tailView.Y.ToString());
                                string _data = string.Join(" ", _ltStrData);
                                cXMLLayerMapHorizonalWell.addHorizonalWellIntervalNode2XML(this.filePathXMLconfigLayerMap, _data);
                            }
                            else _ltStrJHNOWellHead.Add(_sjh);
                        }
                    }
                    if (_ltStrJHNOWellHead.Count > 0)
                        MessageBox.Show(string.Join(" ", _ltStrJHNOWellHead) + " 没有找到对应井位，不参加绘制。");
                }
                else MessageBox.Show("请先创建原始图件。");
            }
            else
            {   //删除水平井段
                cXMLLayerMapHorizonalWell.delHorizonalWellIntervalNode(this.filePathXMLconfigLayerMap);
            }
        }

        private void btnAddHorizonalIntervel_Click(object sender, EventArgs e)
        {
            reDrawSVGMap();
        }

        private void numUDHorizonalLineWidth_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapHorizonalWell.setLineWidthHorzionalInterval(filePathXMLconfigLayerMap, Convert.ToInt16(this.nUDLineWidthHorizonalInterval.Value));
        }

        private void nUDCirleLineWidth_ValueChanged(object sender, EventArgs e)
        {
            cXMLLayerMapBase.setLineWidthWellCircle(filePathXMLconfigLayerMap, Convert.ToInt16(this.nUDCirleLineWidth.Value));
        }

        private void cbbColorHorizonalInterval_MouseClick(object sender, MouseEventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(this.cbbColorHorizonalInterval);
            cXMLLayerMapHorizonalWell.setColorHorionalInterval(this.filePathXMLconfigLayerMap, this.cbbColorLeftLog.BackColor.Name.ToString());
   //         MessageBox.Show(this.cbbColorLeftLog.BackColor.Name.ToString());
        }

        private void btnSelectOK_Click(object sender, EventArgs e)
        {
            selectedLayer = cbbSelectedXCMTop.SelectedItem.ToString(); 
            string fileName=cbbSelectedXCMTop.Text + "-" + cbbSelectedXCMBot.Text;

            filePathXMLconfigLayerMap = Path.Combine(cProjectManager.dirPathMap, fileName + ".xml");


            List<ItemLayerDataDic> listLayerDataSelected = cIODicLayerData.readDicLayerData2struct().FindAll(p=>p.sXCM==selectedLayer);
            listWellsMapLayer.Clear();
            if (listLayerDataSelected.Count > 0)
            {
                foreach (ItemLayerDataDic item in listLayerDataSelected) listWellsMapLayer.Add(new ItemWellMapLayer(item));

                if (!File.Exists(filePathXMLconfigLayerMap))
                {
                    cXMLLayerMapBase.creatLayerMapConfigXML(filePathXMLconfigLayerMap, 1000, 1000);
                }
                else
                {

                }

                addWells();
            }
           
        }

        private void cbxAddGeologyProperty_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAddGeologyProperty.Checked == true) addWellProperty();
        }

        private void btn_addWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.transferItemFromleftListBox2rightListBox(lbxJH, lbxJHSeclected);
        }

        private void btn_deleteWell_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.deleteSlectedItemFromListBox(lbxJHSeclected);
        }

      

             




    }
}
