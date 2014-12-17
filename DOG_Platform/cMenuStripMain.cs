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
            ToolStripMenuItem tsmiFacePattern = new ToolStripMenuItem("相图元");
            tsmiPattern.DropDownItems.Add(tsmiFacePattern);
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
            tsmiTools.DropDownItems.Add(tsmiMapManager);
            tsmiMapManager.Click += new System.EventHandler(tsmiMapManager_Click);
            tsmiTools.DropDownItems.Add(tsmiDogIE);
            tsmiDogIE.Click += new System.EventHandler(tsmiDogIE_Click);
            tsmiTools.DropDownItems.Add(tsmiLogFileConvert);
            tsmiTools.DropDownItems.Add(tsmiErrLog);
            tsmiErrLog.Click += new System.EventHandler(tsmiErrLog_Click);
            menuStrip.Items.Add(tsmiTools);
        }

        public ToolStripMenuItem tsmiLogFileConvert = new ToolStripMenuItem("测井数据格式转换");
        public ToolStripMenuItem tsmiErrLog = new ToolStripMenuItem("查看错误日志");
        public void tsmiErrLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", cProjectManager.filePathErrInfor);
        }
        public ToolStripMenuItem tsmiMapManager = new ToolStripMenuItem("图形管理");
        public void tsmiMapManager_Click(object sender, EventArgs e)
        {
            FormMapManager formOutcomeManager = new FormMapManager();
            formOutcomeManager.ShowDialog();
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
