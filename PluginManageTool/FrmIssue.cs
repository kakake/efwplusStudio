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

namespace PluginManageTool
{
    public partial class FrmIssue : MetroForm
    {
        private string _plugintype = null;
        public FrmIssue(string plugintype)
        {
            InitializeComponent();

            _plugintype = plugintype;
            List<PluginClass> plist2 = new List<PluginClass>();

            if (plugintype == "WinformModulePlugin")
            {
                PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                List<PluginClass> plist = PluginSysManage.GetAllPlugin();
                plist2.AddRange(plist.FindAll(x => (x.plugintype == "WinformModulePlugin")));
            }
            else if (plugintype == "WcfModulePlugin")
            {
                PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                List<PluginClass> plist = PluginSysManage.GetAllPlugin();
                plist2.AddRange(plist.FindAll(x => (x.plugintype == "WcfModulePlugin")));
            }
            else if (plugintype == "WebModulePlugin")
            {
                PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                List<PluginClass> plist = PluginSysManage.GetAllPlugin();
                plist2.AddRange(plist.FindAll(x => (x.plugintype == "WebModulePlugin")));
            }
            gridplugin.AutoGenerateColumns = false;
            gridplugin.DataSource = plist2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmIssue_Load(object sender, EventArgs e)
        {
           
        }

        List<PluginClass> list = null;
        //发布程序包
        private void btnIssue_Click(object sender, EventArgs e)
        {
            list = new List<PluginClass>();
            if (gridplugin.RowCount > 0)
            {
                for (int i = 0; i < gridplugin.RowCount; i++)
                {
                    if (Convert.ToInt32(gridplugin["ck", i].Value) == 1)
                    {
                        list.Add((gridplugin.DataSource as List<PluginClass>)[i]);
                    }
                }
            }
            if (list.Count == 0)
            {
                MessageBoxEx.Show("请先勾选至少一个插件发布为程序包！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (tbIssuePath.Text == "")
            {
                MessageBoxEx.Show("请选择发布路径！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new Loading(StartIssue).ShowDialog();
        }

        private void StartIssue(object sender, EventArgs e)
        {
           

            string mppath = null;
            string moduleplugin = null;
            string pluginxml = null;
            if (_plugintype == "WinformModulePlugin")
            {
                mppath = CommonHelper.WinformPlatformPath + "\\ModulePlugin";
                moduleplugin = tbIssuePath.Text + "\\WinformPlatform\\ModulePlugin";
                pluginxml = CommonHelper.WinformPlatformPath;
                CommonHelper.CopyFolder(CommonHelper.WinformPlatformPath, tbIssuePath.Text + "\\WinformPlatform", "ModulePlugin|WebPlugin");

                PluginSysManage.pluginsysFile = tbIssuePath.Text + "\\WinformPlatform\\Config\\pluginsys.xml";


            }
            else if (_plugintype == "WcfModulePlugin")
            {
                mppath = CommonHelper.WinformPlatformPath + "\\ModulePlugin";
                moduleplugin = tbIssuePath.Text + "\\WinformPlatform\\ModulePlugin";
                pluginxml = CommonHelper.WinformPlatformPath;
                CommonHelper.CopyFolder(CommonHelper.WinformPlatformPath, tbIssuePath.Text + "\\WinformPlatform", "ModulePlugin|WebPlugin");
                PluginSysManage.pluginsysFile = tbIssuePath.Text + "\\WinformPlatform\\Config\\pluginsys.xml";

            }
            else if (_plugintype == "WebModulePlugin")
            {
                mppath = CommonHelper.WebPlatformPath + "\\ModulePlugin";
                moduleplugin = tbIssuePath.Text + "\\WebPlatform\\ModulePlugin";
                pluginxml = CommonHelper.WebPlatformPath;
                CommonHelper.CopyFolder(CommonHelper.WebPlatformPath, tbIssuePath.Text + "\\WebPlatform", "ModulePlugin|WebPlugin");
                PluginSysManage.pluginsysFile = tbIssuePath.Text + "\\WebPlatform\\Config\\pluginsys.xml";
            }

            PluginSysManage.DeletePlugin("WebModulePlugin");
            PluginSysManage.DeletePlugin("WinformModulePlugin");
            PluginSysManage.DeletePlugin("WcfModulePlugin");

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].isdevelopment == "0")//插件包
                {
                    CommonHelper.CopyFolder(mppath + "\\" + list[i].name, moduleplugin + "\\" + list[i].name);
                }
                else
                {
                    //开发插件
                    string _mppath = mppath + "\\" + list[i].name;
                    string _moduleplugin = moduleplugin + "\\" + list[i].name;
                    string _pluginxml = pluginxml + "\\" + list[i].path;
                    PluginXmlManage.pluginfile = _pluginxml;
                    pluginxmlClass plugin = PluginXmlManage.getpluginclass();

                    foreach (issueClass ic in plugin.issue)
                    {
                        if (ic.type == "dir")
                        {
                            if (ic.source != "")
                            {
                                CommonHelper.CopyFolder(_mppath + "\\" + ic.source, _moduleplugin + "\\" + ic.path);
                            }
                            else
                            {
                                if (Directory.Exists(_mppath + "\\" + ic.path))
                                    CommonHelper.CopyFolder(_mppath + "\\" + ic.path, _moduleplugin + "\\" + ic.path);
                                else
                                    Directory.CreateDirectory(_moduleplugin + "\\" + ic.path);
                            }
                        }
                        else if (ic.type == "file")
                        {
                            if (ic.source != "")
                            {
                                new FileInfo(_mppath + "\\" + ic.source).CopyTo(_moduleplugin + "\\" + ic.path, true);
                            }
                            else
                            {
                                new FileInfo(_mppath + "\\" + ic.path).CopyTo(_moduleplugin + "\\" + ic.path, true);
                            }
                        }
                    }
                }

                PluginSysManage.AddPlugin(list[i].plugintype, list[i].name, list[i].path, list[i].title, list[i].isdevelopment);
            }

            MessageBoxEx.Show("发布程序包成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridplugin_Click(object sender, EventArgs e)
        {
            if (gridplugin.CurrentCell != null)
            {
                if (Convert.ToInt32(gridplugin["ck", gridplugin.CurrentCell.RowIndex].Value) == 0)
                {
                    gridplugin["ck", gridplugin.CurrentCell.RowIndex].Value = 1;
                }
                else
                {
                    gridplugin["ck", gridplugin.CurrentCell.RowIndex].Value = 0;
                }
            }
        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbIssuePath.Text = folderBrowser.SelectedPath;
            }
        }
    }
}
