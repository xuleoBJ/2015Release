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
using mshtml;
using DOGPlatform.SVG;

namespace DOGPlatform
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FormWebNavigation : Form
    {
        string filepathSVG;
        public FormWebNavigation(string filepath)
        {
            InitializeComponent();
            this.filepathSVG = filepath;
            this.Text = filepath;
            webBrowserSVG.ObjectForScripting = this; 
            updateWebSVG();
        }

         void updateWebSVG() 
        {
            this.tabControlSVGNavigation.Dock = DockStyle.Fill;
            if (File.Exists(filepathSVG))
            {
                this.webBrowserSVG.Navigate(new Uri(filepathSVG));
                this.tbgSVGView.Text = Path.GetFileNameWithoutExtension(filepathSVG);
            }
            else 
            {
                this.webBrowserSVG.Navigate("about:blank");
            }
        }

          private void openSVGfile_Click(object sender, EventArgs e)
          {
              OpenFileDialog ofdSVGPath = new OpenFileDialog();

              ofdSVGPath.Title = " 打开项目SVG：";
              ofdSVGPath.Filter = "svg文件|*.svg|所有文件|*.*\\";

              //设置默认文件类型显示顺序 
              ofdSVGPath.FilterIndex = 1;

              //保存对话框是否记忆上次打开的目录 
              ofdSVGPath.RestoreDirectory = true;

              if (ofdSVGPath.ShowDialog() == DialogResult.OK)
              {
                  try
                  {
                      filepathSVG = ofdSVGPath.FileName;
                      webBrowserSVG.Navigate(new Uri(filepathSVG.ToString()));
                      webBrowserSVG.ObjectForScripting = this; 
                      this.Text = filepathSVG;
                  }
                  catch (System.UriFormatException)
                  {
                      MessageBox.Show("error.");
                  }
              }
          }


          private void tabControlSVGNavigation_MouseClick(object sender, MouseEventArgs e)
          {
              MessageBox.Show("ok");
          }

          private void corldrawToolStripMenuItem_Click(object sender, EventArgs e)
          {
              try
              {
                  if (this.filepathSVG != "")
                      System.Diagnostics.Process.Start(@"C:\Program Files\Corel\CorelDRAW Graphics Suite X6\Programs64\CorelDRW.exe", this.filepathSVG);
              }
              catch(Exception e1)
              {
                  MessageBox.Show("没有找到相应软件，请选择系统自动选择。");
              }
          }

          private void inkscapeToolStripMenuItem_Click(object sender, EventArgs e)
          {
              try
              {
              if (this.filepathSVG != "")
                  System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Inkscape\inkscape.exe", this.filepathSVG);
              }
              catch(Exception e1)
              {
                  MessageBox.Show("没有找到相应软件，请选择系统自动选择。");
              }
          }

          public string Test(string args)
          {
              string[] split = args.Split();
              int x1 = int.Parse(split[0]);
              int y1 = int.Parse(split[1]);
              int x2 = int.Parse(split[2]);
              int y2 = int.Parse(split[3]);
              int x3 = int.Parse(split[4]);
              int y3 = int.Parse(split[5]);
              int x4 = int.Parse(split[6]);
              int y4 = int.Parse(split[7]);
              XElement gPath = new XElement("{http://www.w3.org/2000/svg}path");
              string dPath = "M" + x1.ToString()+" "+y1.ToString()+" L"+x2.ToString()+" "+y2.ToString()+
                  " L"+x4.ToString()+" "+y4.ToString()+" L"+x3.ToString()+" "+y3.ToString()+ "z";
              gPath.Add(new XAttribute("d", dPath));
              gPath.Add(new XAttribute("stroke", "red"));
              gPath.Add(new XAttribute("stroke-width", "1"));
              gPath.Add(new XAttribute("fill", "none"));


              XDocument XDoc = XDocument.Load(filepathSVG);
              XElement Xroot = XDoc.Root;
 
              XElement Xg = Xroot.Element("{http://www.w3.org/2000/svg}g");
              Xg.Add(gPath);

              XDoc.Save(filepathSVG);
              
              webBrowserSVG.Refresh();
              return "你输入的是：" + args;
          }

          private void 画线ToolStripMenuItem_Click(object sender, EventArgs e)
          {
             // this.webBrowserSVG.Document.InvokeScript("alert", new object[] { "Hello World" });

          
              HtmlElement scriptEl = webBrowserSVG.Document.CreateElement("script");
 
              webBrowserSVG.Document.InvokeScript("sayHello");
          }

               private void FormWebNavigation_Load(object sender, EventArgs e)
          {

          }

          private void 系统自动选择ToolStripMenuItem_Click(object sender, EventArgs e)
          {
              if (this.filepathSVG != "")
                  System.Diagnostics.Process.Start(this.filepathSVG);
          }

          private void tsmiSand_Click(object sender, EventArgs e)
          {
             gLithoPatternSand2(this.filepathSVG);
          }

          public void gLithoPatternSand2(string filePath)//增加岩石类型
          {
              XDocument xDoc = XDocument.Load(filePath);
              XElement xroot = xDoc.Root;
              //XElement xdefs1 = xDoc.Element("svg:defs");
              //string sLithoName = "砂岩";
              //int iWidthUnit = 20;
              //int iHeightUnit = 10;
              //string sBackColor = "yellow";

              //foreach (var tag in xroot.DescendantNodes()) MessageBox.Show(tag.ToString());
              if (xroot != null)
              {
                 // bool x=xroot.HasElements("defs");
                  XElement xdefs = xroot.Element("{http://www.w3.org/2000/svg}" + "defs");
                  if(xdefs!=null) xdefs.Add(cSVGXEPatternLithoSand.lithoPatternDefsSand("p123", 20, 10, 2, "yellow", "red", true));
                  xroot.Add(cSVGXEPatternLithoSand.lithoPattern("p123"));

                  xDoc.Save(filePath);
              }
          }


          private void tsmiMove_Click(object sender, EventArgs e)
          {
              IHTMLDocument2 htmlDocument = this.webBrowserSVG.Document.DomDocument as IHTMLDocument2;

              IHTMLSelectionObject currentSelection = htmlDocument.selection;

              //var x = webBrowserSVG.Document.GetElementById("idText123");
              
              //x.setAttributeNS(null, "x" ,200);
              //MessageBox.Show(x.Parent.Id);
              if (currentSelection != null)
              {
                  MessageBox.Show(currentSelection.type); 
                  //IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;

                  //if (range != null)
                  //{
                  //    MessageBox.Show(range.text);
                  //}
              }
          }

          private void tsmiDel_Click(object sender, EventArgs e)
          {
              
          }
      

    }
}
