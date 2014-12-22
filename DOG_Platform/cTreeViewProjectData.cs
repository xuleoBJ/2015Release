using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DOGPlatform
{
    class cTreeViewProjectData
    {
      
        public static void setupTNwell(TreeView _tv)
        {
            TreeNode tnWells = new TreeNode();
        
            tnWells.Name = "tnWells";
            tnWells.Text = "井";
            setupTNGlobeWellLog(_tv,tnWells);
            foreach (string sJH in cProjectData.ltStrProjectJH)
            {
                TreeNode tnJH = new TreeNode(sJH,3,3);
                TreeNode tnWellLogDir = new TreeNode("well logs",0,1);

                string _wellDir = Path.Combine(cProjectManager.dirPathWellDir, sJH);

                foreach (string _item in Directory.GetFiles(_wellDir,"*"+cProjectManager.fileExtensionWellLog))
                {
                    string _log = Path.GetFileNameWithoutExtension(_item);
                    TreeNode tnLog = new TreeNode(_log, 5, 5);
                    tnWellLogDir.Nodes.Add(tnLog);
                }
                tnJH.Nodes.Add(tnWellLogDir);
                
               
                tnWells.Nodes.Add(tnJH);
            }
            _tv.Nodes.Add(tnWells);
        }

        public static void updateTN_GlobeWellLog(TreeView _tv)
        {
            TreeNode tnGlobeWellLogs = _tv.Nodes[0].Nodes.Cast<TreeNode>().First(r => r.Name == "tnGlobalLogs" || r.Text == "global well logs");
            tnGlobeWellLogs.Nodes.Clear();
            foreach (string item in cProjectData.ltStrLogSeriers)
            {
                TreeNode _tn = new TreeNode(item, 5, 5);
                tnGlobeWellLogs.Nodes.Add(_tn);
            }
        }

        public static void setupTNGlobeWellLog(TreeView _tv,TreeNode td)
        {
            TreeNode tnGlobeWellLogs = new TreeNode("global well logs");
            tnGlobeWellLogs.Name = "tnGlobalLogs";
            tnGlobeWellLogs.Text = "global well logs";
            foreach (string item in cProjectData.ltStrLogSeriers)
            {
                TreeNode _tn = new TreeNode(item, 5, 5);
                tnGlobeWellLogs.Nodes.Add(_tn);
            }
            td.Nodes.Add(tnGlobeWellLogs);
        }
        //public void setupTNwellTops(TreeView tvw)
        //{
        //    TreeNode tnWellTops = new TreeNode();
        //    tnWellTops.Name = "tnWellTops";
        //    tnWellTops.Text = "分层数据";
        //    foreach (string sXCM in cProjectData.ltStrProjectXCM)
        //    {
        //        TreeNode tnXCM = new TreeNode(sXCM, 4, 4);
        //        tnWellTops.Nodes.Add(tnXCM);
        //    }
        //    tvw.Nodes.Add(tnWellTops);
        //}
        public static void setupTNLayer(TreeView _tv)
        {
            TreeNode tn = new TreeNode();

            tn.Name = "tnWellTops";
            tn.Text = "分层";
            foreach (string _layer in cProjectData.ltStrProjectXCM)
            {
                TreeNode _tnLayer = new TreeNode(_layer, 4,4);
                tn.Nodes.Add(_tnLayer);
            }
            _tv.Nodes.Add(tn);
        }

        public static void setupTNWellLog(TreeNode tnWellLogDir, string sJH)
        {
            tnWellLogDir.Nodes.Clear();
            string _wellDir = Path.Combine(cProjectManager.dirPathWellDir, sJH);

            foreach (string _item in Directory.GetFiles(_wellDir, "*" + cProjectManager.fileExtensionWellLog))
            {
                string _log = Path.GetFileNameWithoutExtension(_item);
                TreeNode tnLog = new TreeNode(_log, 5, 5);
                tnWellLogDir.Nodes.Add(tnLog);
            }

        }
    }
}