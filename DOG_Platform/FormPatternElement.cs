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


        string dPath=  "M5,5 c0,150 400,150 400,0  Z";
        string svgFilePath = Path.Combine(cProjectManager.dirPathMap, "pattern.svg");

        int numfilePathTemp = 0;//生成预览图的编号

        private void bthLitho_Click(object sender, EventArgs e)
        {
            string sLithoName = "粗砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text;
            string sID = "101";
            addDef4SandStone(sLithoName, sID);
        }


        private void cbbPatternBackColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbPatternSandBackColor);
        }


         void addDef4Limestone( string sLithoName ,string sID)
        {
            int iWidthPattern = Convert.ToInt16(nUDPatternLimesWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternLimesHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternLimesBackColor.BackColor);

            cSVGXEPatternCarbonatie.addDef2Ink(sLithoName, sID, iWidthPattern, iHeightPattern, sBackColor);
        }

         void addDef4SandStone(string sLithoName, string sID)
         {
             int iWidthPattern = Convert.ToInt16(nUDPatternSandWidth.Value);
             int iHeightPattern = Convert.ToInt16(nUDPatternSandHeight.Value);
             string sBackColor = cPublicMethodBase.getRGB(cbbPatternSandBackColor.BackColor);
             string sCirleColor = cPublicMethodBase.getRGB(this.cbbInnerColor.BackColor);
             cSVGXEPatternLithoSand.addDef2Ink(sLithoName, sID, iWidthPattern, iHeightPattern, sBackColor, sCirleColor, true);
         }


         void addDef4MudStone(string sLithoName, string sID)
         {
             int iWidthPattern = Convert.ToInt16(this.nUDPatternMudWidth.Value);
             int iHeightPattern = Convert.ToInt16(this.nUDPatternMudHeight.Value);
             string sBackColor = cPublicMethodBase.getRGB(this.cbbPatternMudBackColor.BackColor);
             cSVGXEPatternMud.addDef2Ink(sLithoName, sID, iWidthPattern, iHeightPattern, sBackColor);
         }

         void addDefGravelStone(string sLithoName, string sID)
         {
             int iWidthPattern = Convert.ToInt16(nUDPatternGravelWidth.Value);
             int iHeightPattern = Convert.ToInt16(nUDPatternGravelHeight.Value);
             string sBackColor = cPublicMethodBase.getRGB(this.cbbGravelBackcolor.BackColor);
             cSVGXEPatternGravel.addDef2Ink(sLithoName, sID, iWidthPattern, iHeightPattern, sBackColor);
         }
        private void btnLimestone_Click(object sender, EventArgs e)
        {
            string sLithoName = "石灰岩";
            if (this.tbxPatternNameHuiyan.Text.Trim() != "") sLithoName = tbxPatternNameHuiyan.Text; 
            string sID = "201";
            addDef4Limestone(sLithoName, sID);
        }

        private void btnDolomite_Click(object sender, EventArgs e)
        {
            string sLithoName = "白云岩";
            if (this.tbxPatternNameHuiyan.Text.Trim() != "") sLithoName = tbxPatternNameHuiyan.Text; 
            string sID = "202";
            addDef4Limestone(sLithoName, sID);
        }

        private void btnMud_Click(object sender, EventArgs e)
        {
            string sLithoName = "泥岩";
            if (this.tbxPatternNameMud.Text.Trim() != "") sLithoName = this.tbxPatternNameMud.Text;
            string sID = "401";
            addDef4MudStone(sLithoName, sID);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sLithoName = "细砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text; 
            string sID = "103";
            addDef4SandStone(sLithoName, sID); 
        }

        private void btnQuartzSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "石英砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text; 
            string sID = "107";
            addDef4SandStone(sLithoName, sID); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sLithoName = "中砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text; 
            string sID = "102";
           addDef4SandStone( sLithoName, sID);
        }

      
       

        private void btnHLSSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "海绿石砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text; 
            string sID = "109";
            addDef4SandStone(sLithoName, sID); 
        }

        private void btnFeSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "铁质砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text; 
            string sID = "108";
            addDef4SandStone(sLithoName, sID); 
        }

        private void cbbPatternLimesBackColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbPatternLimesBackColor);
        }

        private void btnFenSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "粉砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text; 
            string sID = "104";
            addDef4SandStone(sLithoName, sID); 
        }

        private void btnZhongxiSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "中细砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text; 
            string sID = "105";
            addDef4SandStone(sLithoName, sID); 
        }

        private void btnFenXiSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "粉细砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text; 
            string sID = "106";
            addDef4SandStone(sLithoName, sID); 
        }

        private void btnFSZMud_Click(object sender, EventArgs e)
        {
            string sLithoName = "粉砂质泥岩";
            if (this.tbxPatternNameMud.Text.Trim() != "") sLithoName = this.tbxPatternNameMud.Text;
            string sID = "402";
            addDef4MudStone(sLithoName, sID);
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
            if (tbxPatternNameShale.Text.Trim() != "") sLithoName = tbxPatternNameShale.Text;
            int iWidthPattern = Convert.ToInt16(nUDPatternShaleWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternShaleHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternShaleBackColor.BackColor);
            cSVGXEPatternShale.addDef2Ink(sLithoName, "301", iWidthPattern, iHeightPattern, sBackColor);
        }


        private void btnSandShale_Click(object sender, EventArgs e)
        {
            string sLithoName = "砂质页岩";
            if (tbxPatternNameShale.Text.Trim() != "") sLithoName = tbxPatternNameShale.Text;
            int iWidthPattern = Convert.ToInt16(nUDPatternShaleWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternShaleHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternShaleBackColor.BackColor);
            cSVGXEPatternShale.addDef2Ink(sLithoName, "302", iWidthPattern, iHeightPattern, sBackColor);
    
        }

        private void btnSZmud_Click(object sender, EventArgs e)
        {
            string sLithoName = "砂质泥岩";
            if (this.tbxPatternNameMud.Text.Trim() != "") sLithoName = this.tbxPatternNameMud.Text;
            string sID = "403";
            addDef4MudStone(sLithoName, sID);
        }

        private void btnHZmud_Click(object sender, EventArgs e)
        {
            string sLithoName = "灰质泥岩";
            if (this.tbxPatternNameMud.Text.Trim() != "") sLithoName = this.tbxPatternNameMud.Text;
            string sID = "404";
            addDef4MudStone(sLithoName, sID);
        }

        private void cbbGravelBackcolor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbGravelBackcolor);
        }

        private void btnMidGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "中砾岩";
            if (this.tbxPatternNameGravel.Text.Trim() != "") sLithoName = tbxPatternNameGravel.Text;
            string sID = "503";
            addDefGravelStone(sLithoName, sID);
        }
        private void btnHugeGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "巨砾岩";
            if (this.tbxPatternNameGravel.Text.Trim() != "") sLithoName = tbxPatternNameGravel.Text;
            string sID="501";
            addDefGravelStone(sLithoName, sID);
        }

        private void btnCuGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "粗砾岩";
            if (this.tbxPatternNameGravel.Text.Trim() != "") sLithoName = tbxPatternNameGravel.Text;
            string sID = "502";
            addDefGravelStone(sLithoName, sID);
        }

        private void btnXiGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "细砾岩";
            if (this.tbxPatternNameGravel.Text.Trim() != "") sLithoName = tbxPatternNameGravel.Text;
            string sID = "504";
            addDefGravelStone(sLithoName, sID);
        }

        private void btnMudGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "泥砾岩";
            if (this.tbxPatternNameGravel.Text.Trim() != "") sLithoName = tbxPatternNameGravel.Text;
            string sID = "506";
            addDefGravelStone(sLithoName, sID);
        }

        private void btnTriGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "角砾岩";
            if (this.tbxPatternNameGravel.Text.Trim() != "") sLithoName = tbxPatternNameGravel.Text;
            string sID = "507";
            addDefGravelStone(sLithoName, sID);
        }

        private void btnTuffTriGravel_Click(object sender, EventArgs e)
        {
            string sLithoName = "凝灰质角砾岩";
            if (this.tbxPatternNameGravel.Text.Trim() != "") sLithoName = tbxPatternNameGravel.Text;
            string sID = "513";
            addDefGravelStone(sLithoName, sID);
        }

        private void btnTortSand_Click(object sender, EventArgs e)
        {
            string sLithoName = "玄武质砂岩";
            if (this.tbxPatternNameSand.Text.Trim() != "") sLithoName = tbxPatternNameSand.Text; 
            string sID = "127";
            addDef4SandStone(sLithoName, sID); 
        }

        private void btnGypsumMud_Click(object sender, EventArgs e)
        {
            string sLithoName = "石膏质泥岩";
            if (this.tbxPatternNameMud.Text.Trim() != "") sLithoName = this.tbxPatternNameMud.Text;
            string sID = "409";
            addDef4MudStone(sLithoName, sID);
        }

        private void btnAsphaltShale_Click(object sender, EventArgs e)
        {
            string sLithoName = "沥青质页岩";
            if (tbxPatternNameShale.Text.Trim() != "") sLithoName = tbxPatternNameShale.Text; 
            int iWidthPattern = Convert.ToInt16(nUDPatternShaleWidth.Value);
            int iHeightPattern = Convert.ToInt16(nUDPatternShaleHeight.Value);
            string sBackColor = cPublicMethodBase.getRGB(cbbPatternShaleBackColor.BackColor);
            cSVGXEPatternShale.addDef2Ink(sLithoName, "305", iWidthPattern, iHeightPattern, sBackColor);
        }

        private void btnOoliteLimes_Click(object sender, EventArgs e)
        {
            string sLithoName = "鲕粒灰岩";
            if (this.tbxPatternNameHuiyan.Text.Trim() != "") sLithoName = tbxPatternNameHuiyan.Text; 
            string sID = "224";
            addDef4Limestone(sLithoName, sID);
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


        private void btn_CalConfig_Click(object sender, EventArgs e)
        {
        
        }

        private void nUDSandRadius_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbbPatternShaleBackColor_Click(object sender, EventArgs e)
        {
            cPublicMethodForm.setComboBoxBackColorByColorDialog(cbbPatternShaleBackColor);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            SolidBrush backBrush = new SolidBrush(this.cbbPatternSandBackColor.BackColor);
            int width = Convert.ToInt16(this.nUDPatternSandWidth.Value);
            int height = Convert.ToInt16(this.nUDPatternShaleHeight.Value);
            Bitmap bmp = new Bitmap(width, height);
            SolidBrush brush = new SolidBrush(this.cbbInnerColor.BackColor);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(backBrush, 0, 0, width, height);
                g.FillEllipse(brush, width / 2, height / 2, 3, 3);
            }

            string fileTempPng = Path.Combine(cProjectManager.dirPathTemp, numfilePathTemp.ToString() + "pattern.png");
            if (File.Exists(fileTempPng)) File.Delete(fileTempPng);
            numfilePathTemp++;
            bmp.Save(fileTempPng, ImageFormat.Png);
            Image myImage = Image.FromFile(fileTempPng);
            TextureBrush myTextureBrush = new TextureBrush(myImage);

            Graphics gPanel = this.panelPatternView.CreateGraphics();
            Rectangle rect = new Rectangle(0, 0, this.panelPatternView.Width, this.panelPatternView.Height);
            gPanel.FillRectangle(myTextureBrush, rect);
        }

      

     

    }
}
