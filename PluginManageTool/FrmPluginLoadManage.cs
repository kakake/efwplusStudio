using DevComponents.DotNetBar;
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
using System.Xml;

namespace PluginManageTool
{
    public partial class FrmPluginLoadManage : MetroForm
    {
        public FrmPluginLoadManage()
        {
            InitializeComponent();
        }

        private void FrmPluginLoadManage_Load(object sender, EventArgs e)
        {
            loadxml();
            loadplugintree();
        }

        private void loadplugintree()
        {
            List<PluginClass> plist = new List<PluginClass>();

            PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            List<PluginClass> plist1 = PluginSysManage.GetAllPlugin();
            plist.AddRange(plist1.FindAll(x => x.plugintype == "WebModulePlugin"));

            PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            List<PluginClass> plist2 = PluginSysManage.GetAllPlugin();
            plist.AddRange(plist2.FindAll(x => (x.plugintype == "WinformModulePlugin" || x.plugintype == "WcfModulePlugin")));

            LoadTree(plist);
        }

        private void LoadTree(List<PluginClass> plist)
        {
            treePlugin.Nodes.Clear();

            TreeNode webnode = new TreeNode();
            webnode.Tag = null;
            webnode.Text = "Web插件";
            treePlugin.Nodes.Add(webnode);

            TreeNode winformnode = new TreeNode();
            winformnode.Tag = null;
            winformnode.Text = "Winform插件";
            treePlugin.Nodes.Add(winformnode);

            TreeNode wcfnode = new TreeNode();
            wcfnode.Tag = null;
            wcfnode.Text = "Wcf插件";
            treePlugin.Nodes.Add(wcfnode);

            for (int i = 0; i < plist.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Tag = plist[i];
                node.Text = plist[i].name;
                if (plist[i].plugintype == "WebModulePlugin")
                {
                    webnode.Nodes.Add(node);
                }
                else if (plist[i].plugintype == "WinformModulePlugin")
                {
                    winformnode.Nodes.Add(node);
                }
                else if (plist[i].plugintype == "WcfModulePlugin")
                {
                    wcfnode.Nodes.Add(node);
                }
            }

            treePlugin.ExpandAll();
        }


        private void loadxml()
        {
            string pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            textEditorControl.IsReadOnly = true;
            textEditorControl.LoadFile(pluginsysFile);

            pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            textEditorControl1.IsReadOnly = true;
            textEditorControl1.LoadFile(pluginsysFile);
        }

        //将菜单设置为启动项
        private void btnSetstartitem_Click(object sender, EventArgs e)
        {
            //FrmDevSetting setting = new FrmDevSetting();
            //setting.ShowDialog();

            if (gridpluginmenu.CurrentCell == null) return;
            if (treePlugin.SelectedNode == null || treePlugin.SelectedNode.Tag == null) return;
            PluginClass pc = (PluginClass)treePlugin.SelectedNode.Tag;

            List<menuClass> mlist = gridpluginmenu.DataSource as List<menuClass>;
            menuClass menu = mlist[gridpluginmenu.CurrentCell.RowIndex];

            if (pc.plugintype == "WebModulePlugin")
            {
                string netwebserver = CommonHelper.AppRootPath + "\\NetWebServer.exe.config";
                XmlDocument xmlDoc_webserver = new System.Xml.XmlDocument();
                xmlDoc_webserver.Load(netwebserver);
                XmlNode node = xmlDoc_webserver.DocumentElement.SelectSingleNode("appSettings/add[@key='defaultpage']");
                node.Attributes["value"].Value = menu.menupath;
                xmlDoc_webserver.Save(netwebserver);
            }
            else if (pc.plugintype == "WinformModulePlugin")
            {
                string pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                string appconfig = CommonHelper.WinformPlatformPath + "\\EFWWin.exe.config";
                XmlDocument xmlDoc_app = new System.Xml.XmlDocument();
                xmlDoc_app.Load(appconfig);
                PluginSysManage.pluginsysFile = pluginsysFile;
                XmlNode node = xmlDoc_app.DocumentElement.SelectSingleNode("appSettings/add[@key='ClientType']");
                node.Attributes["value"].Value = "Winform";
                xmlDoc_app.Save(appconfig);
                PluginSysManage.SetWinformEntry(menu.pluginname, menu.viewname == "" ? menu.controllername : menu.controllername + "|" + menu.viewname);
            }
            else if (pc.plugintype == "WcfModulePlugin")
            {
                string pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                string appconfig = CommonHelper.WinformPlatformPath + "\\EFWWin.exe.config";
                XmlDocument xmlDoc_app = new System.Xml.XmlDocument();
                xmlDoc_app.Load(appconfig);
                PluginSysManage.pluginsysFile = pluginsysFile;
                XmlNode node = xmlDoc_app.DocumentElement.SelectSingleNode("appSettings/add[@key='ClientType']");
                node.Attributes["value"].Value = "WCFClient";
                xmlDoc_app.Save(appconfig);
                PluginSysManage.SetWcfClientEntry(menu.pluginname, menu.viewname == "" ? menu.controllername : menu.controllername + "|" + menu.viewname);
            }

            MessageBoxEx.Show("设置启动项成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void treePlugin_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                PluginClass pc = (PluginClass)e.Node.Tag;
                string path=null;
                if (pc.plugintype == "WebModulePlugin")
                {
                    path = CommonHelper.WebPlatformPath + "\\" + pc.path;
                }
                else if (pc.plugintype == "WinformModulePlugin")
                {
                    path = CommonHelper.WinformPlatformPath + "\\" + pc.path;
                }
                else if (pc.plugintype == "WcfModulePlugin")
                {
                    path = CommonHelper.WinformPlatformPath + "\\" + pc.path;
                }

                pluginxmlClass plugin=null;
                if (File.Exists(path))
                {
                    PluginXmlManage.pluginfile = path;
                    plugin = PluginXmlManage.getpluginclass();
                }

                if (plugin != null)
                {
                    gridpluginmenu.AutoGenerateColumns = false;
                    gridpluginmenu.DataSource = plugin.menu;
                }
            }
        }

        private void gridpluginmenu_Click(object sender, EventArgs e)
        {
            if (gridpluginmenu.CurrentCell != null)
            {
                List<menuClass> mlist = gridpluginmenu.DataSource as List<menuClass>;
                menuClass menu = mlist[gridpluginmenu.CurrentCell.RowIndex];

                txtMenuName.Text = menu.menuname;
                txtMenuPath.Text = menu.menupath;
                txtPluginName.Text = menu.pluginname;
                txtControllerName.Text = menu.controllername;
                txtViewName.Text = menu.viewname;
                txtMemo.Text = menu.memo;
            }
        }
    }
}
