#region << 版 本 注 释 >>
/*
 * ========================================================================
 * Copyright(c) 2014 Xuleo,Riped, All Rights Reserved.
 * ========================================================================
 *  许磊，联系电话13581625021，qq：38643987

 * ========================================================================
*/
#endregion
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

        int PageWidth = 2000;
        int PageHeight = 1500;
        string sUnit = "pt"; 

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
            cPublicMethodForm.inialComboBox(cbbUnit, new List<string>(new string[] { "mm", "pt", "px", "pc", "cm", "in"}));
            cPublicMethodForm.inialListBox(lbxJH, cProjectData.listProjectWell.FindAll(p=>p.WellPathList.Count>3).Select(p=>p.sJH).ToList());
            this.nUDrefX.Value = decimal.Parse(cProjectData.dfMapXrealRefer.ToString());
            this.nUDrefY.Value = decimal.Parse(cProjectData.dfMapYrealRefer.ToString());
            initialCbbScale();
            this.cbbScale.Text =(1000.0 / cProjectData.dfMapScale).ToString("0");
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
            foreach (string sItem in listScale) cbbScale.Items.Add(sItem);
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
            if (cbbUnit.SelectedIndex >= 0) sUnit = cbbUnit.SelectedItem.ToString();
            string filePathSVGLayerMap = filePathXMLconfigLayerMap.Replace(".xml", ".svg");
            //注意偏移量,偏移主要是为了好看 如果不偏移的话 就会绘到角落上,这时的偏移是整个偏移 后面的不用偏移了，相对偏移0，0
            int idx = 50;
            int idy = 50;
            cSVGDocLayerMap svgLayerMap = new cSVGDocLayerMap(filePathXMLconfigLayerMap, PageWidth, PageHeight, idx, idy, sUnit);
           
            //add title 
            string sTitle = this.selectedLayer+ "小层平面图";
            if (tbxTitle.Text != "") sTitle = tbxTitle.Text;
            svgLayerMap.addSVGTitle(sTitle, 50, 20);
            XmlElement returnElemment;
          
            if (File.Exists(filePathSVGLayerMap)) File.Delete(filePathSVGLayerMap);

            returnElemment = svgLayerMap.gWellsPosition();
            svgLayerMap.addgElement(returnElemment,  0, 0 );

            //如果顶层面断层数据不为空的话 应该加上断层
            //读取当前顶层的断层数据
            string _topLayer = cbbSelectedXCMTop.SelectedItem.ToString();
            List<ItemFaultLine> listFaultLine = cIOinputLayerSerier.readInputFaultFile(_topLayer);
            foreach (ItemFaultLine line in listFaultLine) 
            {
                returnElemment = svgLayerMap.gFaultline(line.ltPoints,"red",2);
                svgLayerMap.addgElement(returnElemment, 0, 0);
            }

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
            if (this.cbxAddHorizonWell.Checked == true)
            {
                XmlNodeList listHorinalNode = cXMLLayerMapHorizonalWell.getHorizonalWellIntervalNodeList(this.filePathXMLconfigLayerMap);
                foreach (XmlNode xn in listHorinalNode)
                {
                    returnElemment = svgLayerMap.gHorizonalWellIntervelLine(xn);
                    svgLayerMap.addgElement(returnElemment, 0, 0);
                }
            }
            svgLayerMap.makeSVGfile(filePathSVGLayerMap);
            FormMain.filePathWebSVG = filePathSVGLayerMap;
            this.Close();
        }

     
        private void btnMakeLayerMap_Click(object sender, EventArgs e)
        {  
            generateSVGfilemap();
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

        private void FormMapLayer_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
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
                List<string> llistHorizinalJH = new List<string>();
                foreach (object selecteditem in lbxJHSeclected.Items)
                {
                    string strItem = selecteditem as String;
                    llistHorizinalJH.Add(strItem);
                }
                //add svg文件中水平井段
                if (File.Exists(filePathXMLconfigLayerMap))
                {
                    cXMLLayerMapHorizonalWell.delHorizonalWellIntervalNode(this.filePathXMLconfigLayerMap);

                    foreach (string _sjh in llistHorizinalJH)
                    {
                        List<ItemDicWellPath> currentWellPath = cIOinputWellPath.readWellPath2Struct(_sjh);
                        //井必须在project井范围内
                        if (cProjectData.ltStrProjectJH.IndexOf(_sjh) >= 0)
                        {
                            ItemDicWellPath top = currentWellPath.Find(x => x.f_incl>80);
                            ItemDicWellPath tail = currentWellPath.FindLast(x => x.f_incl > 80);
                            // 井号+ 井型 + 井口view坐标 + head view 坐标 + tail view 坐标 

                            List<string> _ltStrData = new List<string>();
                            _ltStrData.Add(_sjh);

                            ItemWellMapLayer item = this.listWellsMapLayer.Find(x => x.sJH == _sjh);
                            if (item != null && top.sJH != null && top.sJH != null)
                            {
                                _ltStrData.Add(item.iWellType.ToString());
                                Point headView = cCordinationTransform.transRealPointF2ViewPoint(item.dbX, item.dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                                _ltStrData.Add(headView.X.ToString());
                                _ltStrData.Add(headView.Y.ToString());

                                Point topView =cCordinationTransform.transRealPointF2ViewPoint(top.dbX, top.dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                                _ltStrData.Add(topView.X.ToString());
                                _ltStrData.Add(topView.Y.ToString());
                                Point tailView = cCordinationTransform.transRealPointF2ViewPoint(tail.dbX, tail.dbY, cProjectData.dfMapXrealRefer, cProjectData.dfMapYrealRefer, cProjectData.dfMapScale);
                                _ltStrData.Add(tailView.X.ToString());
                                _ltStrData.Add(tailView.Y.ToString());
                                string _data = string.Join(" ", _ltStrData);
                                cXMLLayerMapHorizonalWell.addHorizonalWellIntervalNode2XML(this.filePathXMLconfigLayerMap, _data);
                            }
                        }
                    }
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
            string rgbColor = cPublicMethodForm.getRGB(this.cbbColorHorizonalInterval.BackColor);
            cXMLLayerMapHorizonalWell.setColorHorionalInterval(this.filePathXMLconfigLayerMap, rgbColor);
        }

        private void btnSelectOK_Click(object sender, EventArgs e)
        {
            selectedLayer = cbbSelectedXCMTop.SelectedItem.ToString(); 
            string fileName=cbbSelectedXCMTop.Text + "-" + cbbSelectedXCMBot.Text;

            filePathXMLconfigLayerMap = Path.Combine(cProjectManager.dirPathMap, fileName + ".xml");

            List<ItemDicLayerData> listLayerDataSelected = cIODicLayerData.readDicLayerData2struct().FindAll(p=>p.sXCM==selectedLayer);
            listWellsMapLayer.Clear();
            if (listLayerDataSelected.Count > 0)
            {
                foreach (ItemDicLayerData item in listLayerDataSelected) 
                {
                    //由于可能计算小层数据表后又对井做修改 所以 必须判断小层数据表的井是否在项目井范围内
                    if(cProjectData.ltStrProjectJH.IndexOf( item.sJH)>=0) listWellsMapLayer.Add(new ItemWellMapLayer(item));
                }
                if (!File.Exists(filePathXMLconfigLayerMap))
                    cXMLLayerMapBase.creatLayerMapConfigXML(filePathXMLconfigLayerMap, 1000, 1000);
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
