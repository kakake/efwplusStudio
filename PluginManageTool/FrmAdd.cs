using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar.Metro;
using PluginManageTool.Common;
using DevComponents.DotNetBar;

namespace PluginManageTool
{
    public partial class FrmAdd : MetroForm
    {
        public bool isCommit = false;
        string frmadddatafile = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject\\frmAddData.xml";

        public FrmAdd()
        {
            InitializeComponent();

            this.listViewFile.BeginUpdate();
            this.listViewFile.Groups.Clear();
            this.listViewFile.Items.Clear();
            this.listViewFile.EndUpdate();

            //this.treeViewClass.ExpandAll();
            //this.treeViewClass.SelectedNode = treeViewClass.Nodes[0].Nodes[0];
        }

        private void FrmAdd_Load(object sender, EventArgs e)
        {
            this.treeViewClass.BeginUpdate();
            treeViewClass.Nodes.Clear();
            LoadTree(this.treeViewClass.Nodes, null);
            this.treeViewClass.EndUpdate();
        }

        private void LoadTree(TreeNodeCollection treeNode, params string[] id)
        {
            string xpath = "treeView/treeNode";
            if (id != null)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    if (i == 0)
                        xpath += "[";
                    if (i == id.Length - 1)
                        xpath += "@id='" + id[i] + "']";
                    else
                        xpath += "@id = '" + id[i] + "' or ";
                }
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(frmadddatafile);
            XmlNodeList xmlNodes = xmlDoc.DocumentElement.SelectNodes(xpath);
            Xml2Tree(xmlNodes, treeNode);
        }

        private void Xml2Tree(XmlNodeList xmlNode, TreeNodeCollection treeNode)
        {
            foreach (XmlNode var in xmlNode)
            {
                if (var.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                TreeNode newTreeNode = new TreeNode();
                newTreeNode.Tag = var.Attributes["id"].Value;
                newTreeNode.Text = var.Attributes["text"].Value;
                treeNode.Add(newTreeNode);
                if (var.Attributes["selected"] != null && var.Attributes["selected"].Value == "true")
                    newTreeNode.TreeView.SelectedNode = newTreeNode;
                if (var.HasChildNodes)
                {
                    Xml2Tree(var.ChildNodes, newTreeNode.Nodes);
                }
            }
        }

        private void treeViewClass_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.listViewFile.BeginUpdate();
            this.listViewFile.Groups.Clear();
            this.listViewFile.Items.Clear();
            if (e.Node.Tag != null)
            {
                string xpath = "listView/listViewGroup";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(frmadddatafile);
                XmlNodeList xmlNodes = xmlDoc.DocumentElement.SelectNodes(xpath);
                foreach (XmlNode tn in xmlNodes)
                {
                    ListViewGroup group = new ListViewGroup();
                    group.Header = tn.Attributes["text"].Value;
                    listViewFile.Groups.Add(group);
                    foreach (XmlNode var in tn.ChildNodes)
                    {
                        if (e.Node.Tag.ToString()==var.Attributes["nodeid"].Value.ToString())
                        {
                            ListViewItem item = new ListViewItem();
                            AddlistItem AddItem = new AddlistItem();
                            AddItem.id = Convert.ToInt32(var.Attributes["id"].Value);
                            AddItem.text = var.Attributes["text"].Value ;
                            AddItem.imageKey = var.Attributes["imageKey"].Value;
                            AddItem.filePath = var.Attributes["filePath"].Value;
                            AddItem.Description = var.Attributes["Description"].Value;
                            AddItem.nodeid = Convert.ToInt32(var.Attributes["nodeid"].Value);
                            AddItem.plugintype = var.Attributes["plugintype"].Value;
                            
                            item.Tag = AddItem;
                            item.Text = AddItem.text;
                            item.ImageKey = AddItem.imageKey;
                            item.ToolTipText = AddItem.Description;
                            item.Group = group;
                            listViewFile.Items.Add(item);
                        }
                    }
                }
            }
            //if (e.Node.Tag != null)
            //{
            //    string path = CommonHelper.AppRootPath + "\\Template\\PluginTemplateProject";
            //    DirectoryInfo dir = new DirectoryInfo(path);
            //    FileInfo[] files = dir.GetFiles();
            //    ListViewGroup group = new ListViewGroup();
            //    group.Header = "插件项目";
            //    listViewFile.Groups.Add(group);

            //    for (int i = 0; i < files.Length; i++)
            //    {
            //        ListViewItem item = new ListViewItem();
            //        item.Tag = files[i].Name;
            //        item.Text = files[i].Name.Replace(files[i].Extension, "");
            //        item.ImageKey = "solution.png";
            //        item.ToolTipText = "";
            //        item.Group = group;
            //        listViewFile.Items.Add(item);
            //    }
            //}
            this.listViewFile.EndUpdate();
        }


        private void listViewFile_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            switch (((AddlistItem)e.Item.Tag).plugintype)
            {
                case "Winform":
                case "Wcf":
                    tbEntityDescription.Text = CommonHelper.WinformPlatformPath + "\\ModulePlugin";
                    break;
                case "Web":
                    tbEntityDescription.Text = CommonHelper.WebPlatformPath + "\\ModulePlugin";
                    break;
            }
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            if (tbEntityName.Text == "") return;
            try
            {
                if (listViewFile.SelectedItems.Count > 0)
                {
                    switch (((AddlistItem)listViewFile.SelectedItems[0].Tag).id)
                    {
                        case 1://EmptyWebProject.zip
                            NewTemplateProject.NewWebProject(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                        case 2://EmptyWinformProject.zip
                            NewTemplateProject.NewWinformProject(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                        case 3://EmptyWcfProject.zip
                            NewTemplateProject.NewWcfProject(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                        case 4://WebProject_WebForm.zip
                            NewTemplateProject.NewWebProject_WebForm(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                        case 5://WebProject_NVelocity.zip
                            NewTemplateProject.NewWebProject_NVelocity(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                        case 6://WebProject_Razor.zip
                            NewTemplateProject.NewWebProject_Razor(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                        case 7://WebProject_AspNetMvc.zip
                            NewTemplateProject.NewWebProject_AspNetMvc(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                        case 8://WebProject_RightFrame.zip
                            NewTemplateProject.NewWebProject_RightFrame(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                        case 9://WinformProject_RightFrame.zip
                            NewTemplateProject.NewWinformProject_RightFrame(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                        case 10://WcfProject_RightFrame.zip
                            NewTemplateProject.NewWcfProject_RightFrame(tbEntityName.Text, (AddlistItem)listViewFile.SelectedItems[0].Tag);
                            break;
                    }
                    isCommit = true;
                    this.Close();
                }
            }
            catch (Exception err)
            {
                //MessageBox.Show("新建项目失败！\n\t"+err.Message);
                MessageBoxEx.Show("新建项目失败！\n\t" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void tbEntityName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { btnCommit_Click(null, null); }
        }
    }

    public class AddlistItem
    {
        public int id { get; set; }
        public string text { get; set; }
        public string imageKey { get; set; }
        public int nodeid { get; set; }
        public string plugintype { get; set; }
        public string Description { get; set; }
        public string filePath { get; set; }
    }
}
