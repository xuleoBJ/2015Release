using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DOGPlatform.SVG;
using System.IO;
using System.Xml.Linq;
using System.Drawing.Imaging;
namespace DOGPlatform
{
    public partial class FormPatternElement : Form
    {
        public FormPatternElement()
        {
            InitializeComponent();
        }

            //dictionaryPatternSand.Add( "粗砂岩",101);
            //dictionaryPatternSand.Add("中砂岩",102);
            //dictionaryPatternSand.Add("细砂岩",103);
            //dictionaryPatternSand.Add("粉砂岩",104);
            //dictionaryPatternSand.Add("中细砂岩", 105);
            //dictionaryPatternSand.Add("粉细砂岩", 106);
            //dictionaryPatternSand.Add("石英砂岩", 107);
            //dictionaryPatternSand.Add("铁质砂岩", 108);
            //dictionaryPatternSand.Add("海绿石砂岩", 109);
            //dictionaryPatternSand.Add("玄武质砂岩", 127);

        string dPath=  "M5,5 c0,150 400,150 400,0  Z";
        string svgFilePath = Path.Combine(cProjectManager.dirPathMap, "pattern.svg");

        int numfilePathTemp = 0;

        private void button1_Click(object sender, EventArgs e)
        {

            //cSVGDocPatternFace cFaceMap = new cSVGDocPatternFace(20, 20);

            //string d = "M200,300 Q400,50 600,50 T1000,300 Z";
            //cFaceMap.addgElement(cFaceMap.addFaceFloodPlain(d), 500, 500);

            //cFaceMap.makeSVGfile(svgFilePath);
            //FormWebNavigation formSVGView = new FormWebNavigation(svgFilePath); 
            //formSVGView.Show();
        }

        private void btnChannelSand_Click(object sender, EventArgs e)
        {

            //cSVGDocPatternFace cFaceMap = new cSVGDocPatternFace(20, 20);

            //string d = "M10,10 q200,-150 400,-150 T1000,300 Z";
            //cFaceMap.addgElement(cFaceMap.addFaceChannelSand(d), 500, 500);

            //cFaceMap.makeSVGfile(svgFilePath);
            //FormWebNavigation formSVGView = new FormWebNavigation(svgFilePath); 
            //formSVGView.Show();
        }

        private void bthLitho_Click(object sender, EventArgs e)
        {
            string sLithoName = "粗砂岩";
            string sID = "101";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "pattern", "patterns.svg");
            XDocument xDoc = XDocument.Load(filePath);
            XElement xroot = xDoc.Root;

            if (xroot != null)
            {
                // bool x=xroot.HasElements("defs");
                XElement xdefs = xroot.Element("{http://www.w3.org/2000/svg}" + "defs");
                if (xdefs != null)
                {
                    xdefs.Add(cSVGXEPatternLithoSand.lithoPatternDefsSand(sLithoName, sID, 20, 10, 3, "yellow", "red", true));
                }

                xDoc.Save(filePath);
            }
        }

        private void btnSandBar_Click(object sender, EventArgs e)
        {

            //cSVGDocPatternFace cFaceMap = new cSVGDocPatternFace(20, 20);

            //string d = "M200,200 q200,-150 400,-150 t600,0 v50 q-200,100 -400,100  Z";
            //cFaceMap.addgElement(cFaceMap.addFaceChannelSand(d), 500, 500);

            //cFaceMap.makeSVGfile(svgFilePath);
            //FormWebNavigation formSVGView = new FormWebNavigation(svgFilePath); 
            //formSVGView.Show();
        }

       

        private void cbbPatternBackColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbPatternSandBackColor);
        }


         void addDef4Limestone( string sLithoName ,int id)
        {
            int iWidthPattern = Convert.ToInt16(nUDPatternLimesWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternLimesHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternLimesBackColor.BackColor);
 
            string filePahtsvgPattern = @"C:\Program Files (x86)\Inkscape\share\patterns";
            string filePath = Path.Combine(filePahtsvgPattern, "patterns.svg");
            XDocument xDoc = XDocument.Load(filePath);
            XElement xroot = xDoc.Root;

            if (xroot != null)
            {
                // bool x=xroot.HasElements("defs");
                XElement xdefs = xroot.Element("{http://www.w3.org/2000/svg}" + "defs");
                if (xdefs != null) xdefs.AddFirst(cSVGXEPatternCarbonatie.lithoPatternLimesDefs(sLithoName, id.ToString(), id, iWidthPattern, iHeightPattern, sBackColor));
                xDoc.Save(filePath);
                MessageBox.Show("图案添加完成");
            } 
        }


        private void btnLimestone_Click(object sender, EventArgs e)
        {
            string sLithoName = "石灰岩";
            int sID = 201;
            addDef4Limestone(sLithoName, sID);
        }

        private void btnDolomite_Click(object sender, EventArgs e)
        {
            string sLithoName = "白云岩";
            int sID = 202;
            addDef4Limestone(sLithoName, sID);
        }

        private void btnMud_Click(object sender, EventArgs e)
        {
            string sLithoName = "泥岩";
            int iWidthPattern = Convert.ToInt16(this.nUDPatternMudWidth.Value);
            int iHeightPattern = Convert.ToInt16(this.nUDPatternMudHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbPatternMudBackColor.BackColor);
            generateMudLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        private void btnSandConfig_Click(object sender, EventArgs e)
        {
            string sLithoName =this.tbxPatternNameSand.Text;
            string sID = sLithoName.GetHashCode().ToString();
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            int iR = Convert.ToInt16(nUDSandRadius.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            string sSandColor=cPublicMethodBase.getRGB(this.cbbInnerColor.BackColor); 
            string filePahtsvgPattern = @"C:\Program Files (x86)\Inkscape\share\patterns";
            string filePath = Path.Combine(filePahtsvgPattern, "patterns.svg");
            XDocument xDoc = XDocument.Load(filePath);
            XElement xroot = xDoc.Root;

            if (xroot != null)
            {
                // bool x=xroot.HasElements("defs");
                XElement xdefs = xroot.Element("{http://www.w3.org/2000/svg}" + "defs");
                if (xdefs != null)
                {
                    xdefs.AddFirst(cSVGXEPatternLithoSand.lithoPatternDefsSand(sLithoName, sID, iWidthPattern, iHeightPattern, 3, sBackColor, sSandColor, true));
                }

                xDoc.Save(filePath);
                MessageBox.Show("图案添加完成");
            }
                 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sLithoName = "细砂岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnQuartzSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "石英砂岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            string sLithoName = "中砂岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor,dPath); 
        }

        void generateGravelLithoPattern(string sLithoName, int iWidthPattern, int iHeightPattern, string sBackColor, string d)
        {
            string filePathSVGMap = Path.Combine(cProjectManager.dirPathMap, sLithoName + ".svg");

            cSVGDocPatternGravel cLithoPattern = new cSVGDocPatternGravel(20, 20);
            XmlElement lithoElement = cLithoPattern.addLithoPatternGravel(sLithoName, iWidthPattern, iHeightPattern, sBackColor, d);
            cLithoPattern.addgElement(lithoElement, 0, 0);

            cLithoPattern.makeSVGfile(filePathSVGMap);
            FormWebNavigation formSVGView = new FormWebNavigation(filePathSVGMap); formSVGView.Show();
        }
        void generateSandLithoPattern(string sLithoName,int iWidthPattern,int iHeightPattern,string sBackColor ,string d)
        {
            string filePathSVGMap = Path.Combine(cProjectManager.dirPathMap, sLithoName + ".svg");

            cSVGBase cLithoPattern = new cSVGBase(20, 20);
            cLithoPattern.addSVGTitle("hahaha",100,100);
            cLithoPattern.makeSVGfile(filePathSVGMap);
            cSVGXEPatternLithoSand.addLithoPatternSand(filePathSVGMap);

            FormWebNavigation formSVGView = new FormWebNavigation(filePathSVGMap);
            formSVGView.Show();
           
        }


        private void btnHLSSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "海绿石砂岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        private void btnFeSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "铁质砂岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        private void cbbPatternLimesBackColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbPatternLimesBackColor);
        }

        private void btnFenSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "粉砂岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnZhongxiSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "中细砂岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnFenXiSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "粉细砂岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnFSZMud_Click(object sender, EventArgs e)
        {
            string sLithoName = "粉砂质泥岩";
            int iWidthPattern = Convert.ToInt16(this.nUDPatternMudWidth.Value);
            int iHeightPattern = Convert.ToInt16(this.nUDPatternMudHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbPatternMudBackColor.BackColor);
            generateMudLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }
        void generateMudLithoPattern(string sLithoName, int iWidthPattern, int iHeightPattern, string sBackColor, string d)
        {
            string filePathSVGMap = Path.Combine(cProjectManager.dirPathMap, sLithoName + ".svg");

            cSVGDocPatternMud cLithoPattern = new cSVGDocPatternMud(20, 20);
            XmlElement lithoElement = cLithoPattern.addLithoPatternMud(sLithoName, iWidthPattern, iHeightPattern, sBackColor, d);
            cLithoPattern.addgElement(lithoElement, 0, 0);

            cLithoPattern.makeSVGfile(filePathSVGMap);
            FormWebNavigation formSVGView = new FormWebNavigation(filePathSVGMap); formSVGView.Show();
        }

        private void cbbPatternMudBackColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbPatternMudBackColor);
        }

        private void cbbInnerColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbInnerColor);
        }

        private void btnShale_Click(object sender, EventArgs e)
        {
            string sLithoName = "页岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternShaleWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternShaleHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternShaleBackColor.BackColor);
            generateShaleLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        void generateShaleLithoPattern(string sLithoName, int iWidthPattern, int iHeightPattern, string sBackColor, string d)
        {
            string filePathSVGMap = Path.Combine(cProjectManager.dirPathMap, sLithoName + ".svg");
            cSVGDocPatternShale cLithoPattern = new cSVGDocPatternShale(20, 20); 
            XmlElement lithoElement = cLithoPattern.addLithoShalePattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, d);
            cLithoPattern.addgElement(lithoElement, 0, 0);

            cLithoPattern.makeSVGfile(filePathSVGMap);
            FormWebNavigation formSVGView = new FormWebNavigation(filePathSVGMap); formSVGView.Show();
        }

        private void btnSandShale_Click(object sender, EventArgs e)
        {
            string sLithoName = "砂质页岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternShaleWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternShaleHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternShaleBackColor.BackColor);
            generateShaleLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        private void btnSZmud_Click(object sender, EventArgs e)
        {
            string sLithoName = "砂质泥岩";
            int iWidthPattern = Convert.ToInt16(this.nUDPatternMudWidth.Value);
            int iHeightPattern = Convert.ToInt16(this.nUDPatternMudHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbPatternMudBackColor.BackColor);
            generateMudLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        private void btnHZmud_Click(object sender, EventArgs e)
        {
            string sLithoName = "灰质泥岩";
            int iWidthPattern = Convert.ToInt16(this.nUDPatternMudWidth.Value);
            int iHeightPattern = Convert.ToInt16(this.nUDPatternMudHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbPatternMudBackColor.BackColor);
            generateMudLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void cbbGravelBackcolor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbGravelBackcolor);
        }

        private void btnMidGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "中砾岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternGravelWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternGravelHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbGravelBackcolor.BackColor);
            generateGravelLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }
        private void btnHugeGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "巨砾岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternGravelWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternGravelHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbGravelBackcolor.BackColor);
            generateGravelLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnCuGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "粗砾岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternGravelWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternGravelHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbGravelBackcolor.BackColor);
            generateGravelLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnXiGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "细砾岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternGravelWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternGravelHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbGravelBackcolor.BackColor);
            generateGravelLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnMudGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "泥砾岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternGravelWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternGravelHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbGravelBackcolor.BackColor);
            generateGravelLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnTriGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "角砾岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternGravelWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternGravelHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbGravelBackcolor.BackColor);
            generateGravelLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnTuffTriGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "凝灰质角砾岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternGravelWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternGravelHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbGravelBackcolor.BackColor);
            generateGravelLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnTortSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "玄武质砂岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        private void btnGypsumMud_Click(object sender, EventArgs e)
        {
            string sLithoName = "石膏质泥岩";
            int iWidthPattern = Convert.ToInt16(this.nUDPatternMudWidth.Value);
            int iHeightPattern = Convert.ToInt16(this.nUDPatternMudHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(this.cbbPatternMudBackColor.BackColor);
            generateMudLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath);
        }

        private void btnAsphaltShale_Click(object sender, EventArgs e)
        {
            string sLithoName = "沥青质页岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternShaleWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternShaleHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternShaleBackColor.BackColor);
            generateShaleLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        private void btnOoliteLimes_Click(object sender, EventArgs e)
        {
            string sLithoName = "鲕粒灰岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternLimesWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternLimesHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternLimesBackColor.BackColor);
    //        generateLimesLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        private void btnChannel_Click(object sender, EventArgs e)
        {
            dPath = "M200,200 c0,150 400,150 400,0  Z";
        }

        private void btnPatternRect_Click(object sender, EventArgs e)
        {
            dPath = "M200,200 h100 v60 h-100  Z";
        }

        private void btnLayerPattern_Click(object sender, EventArgs e)
        {
            dPath = "M420,200 v-20 c0,-100 200,-100 400,-100 v20 c-200,0 -300,50 -400,100 Z";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sLithoName = "中砂岩";
            int iWidthPattern = 5;
            int iHeightPattern = 5;
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);

            string filePathSVGMap = Path.Combine(cProjectManager.dirPathMap, sLithoName + ".svg");

            cSVGDocPatternSand cLithoPattern = new cSVGDocPatternSand( 0, 0);
            for (int i = 1; i < 5; i++)
            {
                string dRect = "M" + (50 * i).ToString()+" " + (50 * i).ToString() + "h50 v20 h-50 z";
                XmlElement lithoElement = cLithoPattern.addLithoPatternSand(sLithoName, iWidthPattern, iHeightPattern, sBackColor, 50*i,80*i,30,20);
                cLithoPattern.addgElement(lithoElement, 0, 0);
            }
            cLithoPattern.makeSVGfile(filePathSVGMap);
            FormWebNavigation formSVGView = new FormWebNavigation(filePathSVGMap);
            formSVGView.Show();
           
        }

            //先保存图片 再填充预览，最后生成配置存到inkscape的系统定义中去
        private void btnPatternView_Click(object sender, EventArgs e)
        {
            SolidBrush backBrush = new SolidBrush(this.cbbPatternSandBackColor.BackColor);
            int width = Convert.ToInt16( this.nUDPatternSandWidth.Value);
            int height =Convert.ToInt16( this.nUDPatternShaleHeight.Value);
            Bitmap bmp = new Bitmap(width, height);
            SolidBrush brush = new SolidBrush( this.cbbInnerColor.BackColor);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(backBrush, 0, 0, width, height);
                g.FillEllipse(brush, width/2, height/2,3,3);
            }
           
            string fileTempPng=Path.Combine( cProjectManager.dirPathTemp,numfilePathTemp.ToString()+"pattern.png");
            if (File.Exists(fileTempPng)) File.Delete(fileTempPng);
            numfilePathTemp++;
            bmp.Save(fileTempPng, ImageFormat.Png);
            Image myImage = Image.FromFile(fileTempPng);
            TextureBrush myTextureBrush = new TextureBrush(myImage); 

            Graphics gPanel=this.panelPatternView.CreateGraphics();
            Rectangle rect = new Rectangle(0, 0, this.panelPatternView.Width, this.panelPatternView.Height);
            gPanel.FillRectangle(myTextureBrush, rect);
            //以上是gdi方式

        }

       void  addDef(string _xmlFilePath)
        {
             XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlFilePath);
            XmlNode currentNode = xmlDoc.SelectSingleNode("/svg/def");
            if (currentNode!=null) MessageBox.Show("ok"); 
        }

       private void button4_Click(object sender, EventArgs e)
       {
           int iWidthPattern = Convert.ToInt16(nUDPatternLimesWidth.Value);
           int iHeightPattern = Convert.ToInt16(nUDPatternLimesHeight.Value);
           string sBackColor = cPublicMethodBase.getRGB(cbbPatternLimesBackColor.BackColor);

           string sLithoName = this.tbxPatternNamecal.Text;
           string sID = sLithoName.GetHashCode().ToString();
     
           string filePahtsvgPattern = @"C:\Program Files (x86)\Inkscape\share\patterns";
           string filePath = Path.Combine(filePahtsvgPattern, "patterns.svg");
           XDocument xDoc = XDocument.Load(filePath);
           XElement xroot = xDoc.Root;

           if (xroot != null)
           {
               // bool x=xroot.HasElements("defs");
               XElement xdefs = xroot.Element("{http://www.w3.org/2000/svg}" + "defs");
               if (xdefs != null)  
               {
                   xdefs.AddFirst(cSVGXEPatternCarbonatie.lithoPatternLimesDefs(sLithoName, sID, 201,iWidthPattern, iHeightPattern, sBackColor));
               }

               xDoc.Save(filePath);
               MessageBox.Show("图案添加完成");
           }
       }

     




   

    }
}
