using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DOGPlatform
{
    class cMenuStripMain : cMenuStripDog
    {
        public cMenuStripMain(MenuStrip _menustrip)
            : base(_menustrip)
        { 

        }
        public void setupTsmiConfig()
        {
            ToolStripMenuItem tsmiConfig = new ToolStripMenuItem("配置");
            tsmiConfig.DropDownItems.Add(tsmiDebug);
            tsmiDebug.Click += new System.EventHandler(tsmiDebug_Click);
            ToolStripMenuItem tsmiDataManager = new ToolStripMenuItem("数据管理");
            tsmiConfig.DropDownItems.Add(tsmiDataManager);
            tsmiDataManager.Click += new System.EventHandler(tsmiDataManager_Click);
            menuStrip.Items.Add(tsmiConfig);
        }
        public void tsmiDataManager_Click(object sender, EventArgs e) 
        {
            FormWellManager formDI = new FormWellManager();
            formDI.ShowDialog();
        }
        public void tsmiDebug_Click(object sender, EventArgs e)
        {
            FormConfig formConfig = new FormConfig();
            formConfig.ShowDialog();
        }
        public ToolStripMenuItem tsmiDebug = new ToolStripMenuItem("配置调试");

        public void setupTsmiPattern()
        {
            ToolStripMenuItem tsmiPattern = new ToolStripMenuItem("图元配置");
            ToolStripMenuItem tsmiLithoPattern = new ToolStripMenuItem("岩相图元");
            tsmiPattern.DropDownItems.Add(tsmiLithoPattern);
            tsmiLithoPattern.Click += new System.EventHandler(tsmiLithoPattern_Click);
            menuStrip.Items.Add(tsmiPattern);
        }

        private void tsmiLithoPattern_Click(object sender, EventArgs e)
        {
            FormPatternElement fpe = new FormPatternElement();
            fpe.Show();
        }

        public void setupTsmiDataAnalysis()
        {
             ToolStripMenuItem tsmiDataAnalysis = new ToolStripMenuItem("数据挖掘");
             ToolStripMenuItem tsmiPoleMap = new ToolStripMenuItem("极点图分析");
             tsmiPoleMap.Click += new System.EventHandler(tsmiPoleMap_Click);
             tsmiDataAnalysis.DropDownItems.Add(tsmiPoleMap);
             ToolStripMenuItem tsmiMapPieAna = new ToolStripMenuItem("井点图分析");
             tsmiMapPieAna.Click += new System.EventHandler(tsmiMapPieAna_Click);
             tsmiDataAnalysis.DropDownItems.Add(tsmiMapPieAna);
             menuStrip.Items.Add(tsmiDataAnalysis);
        }

         public void tsmiMapPieAna_Click(object sender, EventArgs e)
        {
            FormDataAnalysis formRoseMap = new FormDataAnalysis();
            formRoseMap.ShowDialog();
        }
           
       
        public void tsmiPoleMap_Click(object sender, EventArgs e)
        {
            FormDataAnalysis formRoseMap = new FormDataAnalysis();
            formRoseMap.ShowDialog();
        }
        public void setupTsmiTools()
        {
            ToolStripMenuItem tsmiTools = new ToolStripMenuItem("工具");
            tsmiTools.DropDownItems.Add(tsmiErrLog);
            tsmiErrLog.Click += new System.EventHandler(tsmiErrLog_Click);
            menuStrip.Items.Add(tsmiTools);
        }

        public void setupTsmiWellSections()
        {
            ToolStripMenuItem tsmiTools = new ToolStripMenuItem("剖面分析");
            tsmiTools.DropDownItems.Add(tsmiWellSection);
            tsmiWellSection.Click += new System.EventHandler(tsmiWellSection_Click);
            tsmiTools.DropDownItems.Add(tsmiWellPathSection);
            tsmiWellPathSection.Click += new System.EventHandler(tsmiWellPathSection_Click);
            menuStrip.Items.Add(tsmiTools);
        }

        public ToolStripMenuItem tsmiWellSection = new ToolStripMenuItem("剖面分析");
        public void tsmiWellSection_Click(object sender, EventArgs e)
        {

        }
        public ToolStripMenuItem tsmiWellPathSection = new ToolStripMenuItem("斜井剖面分析");
        public void tsmiWellPathSection_Click(object sender, EventArgs e)
        {

        }


        public ToolStripMenuItem tsmiErrLog = new ToolStripMenuItem("查看错误日志");
        public void tsmiErrLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", cProjectManager.filePathErrInfor);
        }

        public void setupTsmiWellGroup()
        {
            ToolStripMenuItem tsmiTools = new ToolStripMenuItem("井组分析");
            tsmiTools.DropDownItems.Add(tsmiWellGroupFence);
            tsmiWellGroupFence.Click += new System.EventHandler(tsmiWellGroupFence_Click);
            menuStrip.Items.Add(tsmiTools);
        }

        public ToolStripMenuItem tsmiWellGroupFence = new ToolStripMenuItem("井组栅状图");
        public void tsmiWellGroupFence_Click(object sender, EventArgs e)
        {
            FormWellsGroup formFD = new FormWellsGroup();
            formFD.Show();
        }
        public ToolStripMenuItem tsmiDogIE = new ToolStripMenuItem("浏览器");
        public void tsmiDogIE_Click(object sender, EventArgs e)
        {
            FormWebNavigation forWebSVG = new FormWebNavigation("");
            forWebSVG.Show();
        }

        public void setupTsmiHelps()
        {
            ToolStripMenuItem tsmiHelps = new ToolStripMenuItem("帮助");
            ToolStripMenuItem tsmiVersion = new ToolStripMenuItem("版本");
            tsmiHelps.DropDownItems.Add(tsmiVersion);
            tsmiVersion.Click += new System.EventHandler(tsmiVersion_Click);
            tsmiHelps.DropDownItems.Add(tsmiHelp);
            menuStrip.Items.Add(tsmiHelps);
        }
        
        public void tsmiVersion_Click(object sender, EventArgs e)
        {
            FormCopyRight formCP = new FormCopyRight();
            formCP.ShowDialog();
        }
        public ToolStripMenuItem tsmiHelp = new ToolStripMenuItem("帮助");
      

    }
}
