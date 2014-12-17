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

namespace DOGPlatform
{
    public partial class FormPatternElement : Form
    {
        public FormPatternElement()
        {
            InitializeComponent();
        }

        string dPath=  "M5,5 c0,150 400,150 400,0  Z";
        string svgFilePath = Path.Combine(cProjectManager.dirPathMap, "pattern.svg");

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
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            generateSandLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
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

        private void btnLimestone_Click(object sender, EventArgs e)
        {
            string sLithoName = "石灰岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternLimesWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternLimesHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternLimesBackColor.BackColor);
            generateLimesLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
        }

        private void btnDolomite_Click(object sender, EventArgs e)
        {
            string sLithoName = "白云岩";
            int iWidthPattern = Convert.ToInt16(nUDPatternLimesWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternLimesHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternLimesBackColor.BackColor);
            generateLimesLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
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
            string sLithoName = "configSand";
            int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
            int r = Convert.ToInt16(nUDSandRadius.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
            string sCircleInnerColor = cPublicMethodBase.getRGB(this.cbbInnerColor.BackColor);
            bool hasSplitLine=this.cbxHasSplitLine.Checked;

            string filePathSVGMap =Path.Combine(cProjectManager.dirPathMap, sLithoName + ".svg");

            cSVGDocPatternSand cLithoPattern = new cSVGDocPatternSand(20, 20);
            XmlElement lithoElement = cLithoPattern.addLithoPatternSand(sLithoName, iWidthPattern, iHeightPattern, r,dPath, sBackColor, sCircleInnerColor,hasSplitLine);
            cLithoPattern.addgElement(lithoElement, 0, 0);

            cLithoPattern.makeSVGfile(filePathSVGMap);
            FormWebNavigation formSVGView = new FormWebNavigation(filePathSVGMap);
            formSVGView.Show();
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
          
            cSVGDocPatternSand cLithoPattern = new cSVGDocPatternSand(20, 20);
            XmlElement lithoElement = cLithoPattern.addLithoPatternSand(sLithoName, iWidthPattern, iHeightPattern, sBackColor, d);
            cLithoPattern.addgElement(lithoElement, 0, 0);
            cLithoPattern.makeSVGfile(filePathSVGMap);

            FormWebNavigation formSVGView = new FormWebNavigation(filePathSVGMap);
            formSVGView.Show();
           
        }

        void generateLimesLithoPattern(string sLithoName, int iWidthPattern, int iHeightPattern, string sBackColor, string d)
        {
            string filePathSVGMap = Path.Combine(cProjectManager.dirPathMap, sLithoName + ".svg");

            cSVGDocPatternCarbonatite cLithoPattern = new cSVGDocPatternCarbonatite(20, 20);
            XmlElement lithoElement = cLithoPattern.addLithoLimesPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, d);
            cLithoPattern.addgElement(lithoElement, 0, 0);

            cLithoPattern.makeSVGfile(filePathSVGMap);
            FormWebNavigation formSVGView = new FormWebNavigation(filePathSVGMap); formSVGView.Show();
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
            generateLimesLithoPattern(sLithoName, iWidthPattern, iHeightPattern, sBackColor, dPath); 
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

     




   

    }
}
