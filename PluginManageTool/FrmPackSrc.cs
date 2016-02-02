using DevComponents.DotNetBar.Metro;
using PluginManageTool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluginManageTool
{
    public partial class FrmPackSrc : MetroForm
    {
        string prjpath = string.Empty;

        public FrmPackSrc(string srcpath)
        {
            InitializeComponent();

            prjpath = srcpath;

            treeSrc.Nodes.Clear();

            TreeNode node = new TreeNode();
            node.Tag = srcpath;
            node.Text = new DirectoryInfo(srcpath).Name;
            node.Checked = true;
            treeSrc.Nodes.Add(node);

            LoadTree(srcpath,node);
            treeSrc.ExpandAll();
        }

        private void LoadTree(string path, TreeNode pnode)
        {
            DirectoryInfo sDir = new DirectoryInfo(path);
            if (sDir.Exists)
            {
                TreeNode node = new TreeNode();
                node.Tag = path;
                node.Text = sDir.Name;
                if (sDir.Name != "bin" && sDir.Name != "obj" && pnode.Checked)
                    node.Checked = true;
                else
                    node.Checked = false;
                pnode.Nodes.Add(node);

                DirectoryInfo[] subDirArray = sDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    LoadTree(subDir.FullName, node);
                }
            }
        }

        private void treeSrc_AfterCheck(object sender, TreeViewEventArgs e)
        {
            setChecked(e.Node, e.Node.Checked);
        }

        private void setChecked(TreeNode node,bool value)
        {
            foreach (TreeNode n in node.Nodes)
            {
                n.Checked = value;

                setChecked(n, value);
            }
        }

        private List<string> getexceptDir()
        {
            List<string> list = new List<string>();
            gettreepath(treeSrc.Nodes[0], list);
            return list;
        }

        private void gettreepath(TreeNode node, List<string> list)
        {
            if (node.Checked)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    gettreepath(n, list);
                }
            }
            else
            {
                list.Add(node.Tag.ToString());
            }
        }

        public bool isOk = false;
        public string srcfile_zip = string.Empty;
        private void btnzip_Click(object sender, EventArgs e)
        {
            List<string> paths = getexceptDir();
            string srcfile = prjpath + "_src";

            if (File.Exists(srcfile + ".zip"))
            {
                File.Delete(srcfile + ".zip");
            }

            Directory.CreateDirectory(srcfile);

            CommonHelper.CopyFolder(prjpath, srcfile,paths);
            FastZipHelper.compress(srcfile, srcfile + ".zip");
            Directory.Delete(srcfile, true);

            srcfile_zip = srcfile + ".zip";
            isOk = true;

            this.Close();
        }
    }
}
