using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevComponents.DotNetBar;
using PluginManageTool.Common;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Net;
using Newtonsoft.Json;
using DevComponents.DotNetBar.Controls;
using System.Threading;
using Microsoft.Office.Interop.Visio;

namespace PluginManageTool
{
    public partial class FrmMain : MetroAppForm
    {
        string _pluginType = "-1";

        public FrmMain()
        {
            InitializeComponent();

            MetroColorGeneratorParameters[] metroThemes = MetroColorGeneratorParameters.GetAllPredefinedThemes();
            foreach (MetroColorGeneratorParameters mt in metroThemes)
            {
                ButtonItem theme = new ButtonItem(mt.ThemeName, mt.ThemeName);
                theme.Command = new Command(components, new EventHandler(ChangeMetroThemeExecuted));
                theme.CommandParameter = mt;
                colorThemeButton.SubItems.Add(theme);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            gridweb.AutoGenerateColumns = false;
            gridlocal.AutoGenerateColumns = false;
            griddev.AutoGenerateColumns = false;

            //this.BeginInvoke(new BeginInvokeDelegate(InitData),null);
            InitData();

            loadTitle();
        }

        //delegate void BeginInvokeDelegate();
        private void InitData()
        {
            LoadPluginData("-1");//插件开发
            LoadPluginData1("-1");//本地插件
            LoadPluginData2("-1");  //插件中心
        }

        string entryplugin, entrycontroller,starturl;
        public void loadTitle()
        {
            this.Text = "efwplus Studio(插件开发工具)";
            string appconfig = CommonHelper.WinformPlatformPath + "\\EFWWin.exe.config";
            XmlDocument xmlDoc_app = new System.Xml.XmlDocument();
            xmlDoc_app.Load(appconfig);
            string netwebserver = CommonHelper.AppRootPath + "\\NetWebServer.exe.config";
            XmlDocument xmlDoc_webserver = new System.Xml.XmlDocument();
            xmlDoc_webserver.Load(netwebserver);

            XmlNode node = xmlDoc_app.DocumentElement.SelectSingleNode("appSettings/add[@key='ClientType']");
            if (node != null)
            {
                string ClientType = node.Attributes["value"].Value;
                if (ClientType == "Winform")
                {
                    PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                    PluginSysManage.GetWinformEntry(out entryplugin, out entrycontroller);
                }
                else if (ClientType == "WCFClient")
                {
                    PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                    PluginSysManage.GetWcfClientEntry(out entryplugin, out entrycontroller);
                }
            }

            node = xmlDoc_webserver.DocumentElement.SelectSingleNode("appSettings/add[@key='defaultpage']");
            if (node != null)
            {
                starturl = node.Attributes["value"].Value;
            }

            //this.Text = "efwplus开发平台" + "  " + "当前启动插件[" + entryplugin + "]  控制器[" + entrycontroller + "]";
            labelItemtitle.Text = "Win启动项：[" + entryplugin + "@" + entrycontroller + "] Web启动项：[" + starturl + "]";
        }

        void bcx_BeforeCellPaint(object sender, BeforeCellPaintEventArgs e)
        {
            DataGridViewLabelXColumn bcx = sender as DataGridViewLabelXColumn;

            if (bcx != null)
            {
                DataTable dt = gridweb.DataSource as DataTable;
                int rowindex = e.RowIndex;
                int Id = Convert.ToInt32(dt.Rows[rowindex]["Id"]);
                string name = dt.Rows[rowindex]["name"].ToString();
                string title = dt.Rows[rowindex]["title"].ToString();
                string introduction = dt.Rows[rowindex]["introduction"].ToString();
                string downloadpath = dt.Rows[rowindex]["downloadpath"].ToString();
                string pluginsize = dt.Rows[rowindex]["pluginsize"].ToString();
                string pversion = dt.Rows[rowindex]["pversion"].ToString();
                string author = dt.Rows[rowindex]["author"].ToString();
                string updatedate = dt.Rows[rowindex]["updatedate"].ToString();
                string ptype = dt.Rows[rowindex]["plugintype"].ToString();
                string url = CommonHelper.plugin_serverurl + downloadpath;
                bcx.TextAlignment = StringAlignment.Near;
                bcx.Text = "*"+title + "*    " + "版本:" + pversion + "    " + "作者:" + author + "     " + "更新时间:" + updatedate + "\r\n" + introduction;
            }
        }

        #region 插件中心
        //过滤开发插件
        private void pluginType2_Click(object sender, EventArgs e)
        {
            string tag = (sender as PopupItem).Tag.ToString();
            _pluginType = tag;
            LoadPluginData2(tag);
        }

        private void LoadPluginData2(string pluginType)
        {
            new Thread((ThreadStart)(delegate()
            {
                try
                {
                    labloading.Invoke((MethodInvoker)delegate() { labloading.Visible = true; });

                    List<PluginClass> plist = new List<PluginClass>();

                    string url = CommonHelper.plugin_serverurl + "/Controller.aspx?controller=efwplus_website@PluginController&method=getpluginlist_client&plugintype=" + pluginType;
                    HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, null, null);
                    string ret = HttpWebResponseUtility.GetHttpData(response);
                    string data = (HttpWebResponseUtility.ToResult(ret) as Newtonsoft.Json.JavaScriptArray).ToArray()[0].ToString();
                    DataTable dt = HttpWebResponseUtility.ToDataTable(JavaScriptConvert.DeserializeObject(data));

                    gridweb.Invoke((MethodInvoker)delegate() { gridweb.DataSource = dt; });

                    DataGridViewLabelXColumn bcx = gridweb.Columns["ctitle"] as DataGridViewLabelXColumn;
                    if (bcx != null)
                    {
                        bcx.BeforeCellPaint += bcx_BeforeCellPaint;
                    }

                    labloading.Invoke((MethodInvoker)delegate() { labloading.Visible = false; });
                }
                catch (Exception err)
                {
                    //MessageBox.Show("请求数据失败！\n" + err.Message);
                    MessageBoxEx.Show("请求数据失败！\n" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            })).Start();
        }
        //下载安装
        private void btnDownSetup_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridweb.CurrentCell == null) return;

                DataTable dt = gridweb.DataSource as DataTable;
                int rowindex = gridweb.CurrentCell.RowIndex;
                int Id = Convert.ToInt32(dt.Rows[rowindex]["Id"]);
                string name = dt.Rows[rowindex]["name"].ToString();
                string title = dt.Rows[rowindex]["title"].ToString();
                string downloadpath = dt.Rows[rowindex]["downloadpath"].ToString();
                string ptype = dt.Rows[rowindex]["plugintype"].ToString();
                string url = CommonHelper.plugin_serverurl + downloadpath;

                string localpath = "";
                //string pluginpath = "";
                //string pluginsyspath = "";
                bool ishave = false;
                switch (ptype)
                {
                    case "web":
                        localpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + name + ".zip";
                        //pluginpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + name;
                        //pluginsyspath = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                        PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                        if (PluginSysManage.ContainsPlugin("WebModulePlugin", name))
                        {
                            ishave = true;
                        }
                        break;
                    case "winform":
                        localpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + name + ".zip";
                        //pluginpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + name;
                        //pluginsyspath = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                        PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                        if (PluginSysManage.ContainsPlugin("WinformModulePlugin", name))
                        {
                            ishave = true;
                        }
                        break;
                    case "wcf":
                        localpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + name + ".zip";
                        //pluginpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + name;
                        //pluginsyspath = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                        PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                        if (PluginSysManage.ContainsPlugin("WcfModulePlugin", name))
                        {
                            ishave = true;
                        }
                        break;

                }
                //先判断此插件本地是否存在
                if (ishave == true)
                {
                    //MessageBox.Show("你选择的插件本地已安装！");
                    MessageBoxEx.Show("你选择的插件本地已安装！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                frmprogress progress = new frmprogress();
                
                //上传
                UpDownLoadFileHelper updown = new UpDownLoadFileHelper();
                updown.Cdelegate = new UpDownLoadFileHelper.controldelegate(progress.refreshControl);
                updown.UpDown = UpDownLoadFileHelper.updown.下载;
                updown.Completed = delegate()
                {
                    progress.Close();
                    //解压安装
                    if (CommonHelper.PluginSetup(localpath) == true)
                    {
                        File.Delete(localpath);
                        //MessageBox.Show("已下载,并且完成安装！");
                        MessageBoxEx.Show("已下载,并且完成安装！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPluginData1("-1");//本地插件
                    }
                };
                updown.Cancelled = delegate()
                {
                    progress.Close();
                    //MessageBox.Show("下载失败！");
                    MessageBoxEx.Show("下载失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                updown.Start(url, localpath);
                progress.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBoxEx.Show("下载失败！\r\n" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnplugindetail_Click(object sender, EventArgs e)
        {
            if (gridweb.CurrentCell == null) return;
            DataTable dt = gridweb.DataSource as DataTable;
            int rowindex = gridweb.CurrentCell.RowIndex;
            int Id = Convert.ToInt32(dt.Rows[rowindex]["Id"]);
            string url = CommonHelper.plugin_serverurl + "/efwplus/PluginController-PluginDetail/" + Id;
            Process.Start(url);
        }
        #endregion

        #region 本地插件


        //过滤开发插件
        private void pluginType1_Click(object sender, EventArgs e)
        {
            string tag = (sender as PopupItem).Tag.ToString();
            _pluginType = tag;
            LoadPluginData1(tag);
        }

        private void LoadPluginData1(string pluginType)
        {
            List<PluginClass> plist = new List<PluginClass>();

            PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            List<PluginClass> plist1 = PluginSysManage.GetAllPlugin();
            plist.AddRange(plist1.FindAll(x => x.plugintype == "WebModulePlugin" && x.isdevelopment == "0"));

            PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            List<PluginClass> plist2 = PluginSysManage.GetAllPlugin();
            plist.AddRange(plist2.FindAll(x => (x.plugintype == "WinformModulePlugin" || x.plugintype == "WcfModulePlugin") && x.isdevelopment == "0"));

            List<PluginClass> list=null;
            if (pluginType == "-1")
                list = plist;
            else
            {
                switch (pluginType)
                {
                    case "web":
                        list = plist.FindAll(x => x.plugintype == "WebModulePlugin");
                        break;
                    case "winform":
                        list = plist.FindAll(x => x.plugintype == "WinformModulePlugin");
                        break;
                    case "wcf":
                        list = plist.FindAll(x => x.plugintype == "WcfModulePlugin");
                        break;

                }
            }

            gridlocal.Invoke((MethodInvoker)delegate() { gridlocal.DataSource = list; });
        }

        private void btnexplorer1_Click(object sender, EventArgs e)
        {
            if (gridlocal.CurrentCell == null) return;
            List<PluginClass> plist = gridlocal.DataSource as List<PluginClass>;
            PluginClass pc = plist[gridlocal.CurrentCell.RowIndex];
            string dirplugin = "";
            if (pc.plugintype == "WinformModulePlugin" || pc.plugintype == "WcfModulePlugin")
            {
                FileInfo finfo = new FileInfo(CommonHelper.WinformPlatformPath + "\\" + pc.path);
                if (finfo.Exists)
                {
                    dirplugin = finfo.Directory.FullName;
                }
            }
            else if (pc.plugintype == "WebModulePlugin")
            {
                FileInfo finfo = new FileInfo(CommonHelper.WebPlatformPath + "\\" + pc.path);
                if (finfo.Exists)
                {
                    dirplugin = finfo.Directory.FullName;
                }
            }
            if (dirplugin != "")
                CommonHelper.ShellExecute(IntPtr.Zero, "open", "explorer.exe", dirplugin, "", CommonHelper.ShowCommands.SW_NORMAL);
     
        }

        private void biDelete1_Click(object sender, EventArgs e)
        {
            if (gridlocal.CurrentCell == null) return;

            if (MessageBoxEx.Show("是否移除此插件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;

            List<PluginClass> plist = gridlocal.DataSource as List<PluginClass>;
            PluginClass pc = plist[gridlocal.CurrentCell.RowIndex];
            FileInfo finfo = null;
            if (pc.plugintype == "WinformModulePlugin" || pc.plugintype == "WcfModulePlugin")
            {
                finfo = new FileInfo(CommonHelper.WinformPlatformPath + "\\" + pc.path);
                if (finfo.Exists)
                {
                    //移除dll
                    PluginXmlManage.pluginfile = finfo.FullName;
                    pluginxmlClass plugin = PluginXmlManage.getpluginclass();
                    foreach (setupClass ic in plugin.setup)
                    {
                        string dllpath = CommonHelper.PathCombine(finfo.Directory.FullName, ic.copyto);
                        if (new FileInfo(dllpath).Exists)
                        {
                            File.Delete(dllpath);
                        }
                    }

                    PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                    PluginSysManage.DeletePlugin(pc.plugintype, pc.name);
                    if (finfo.Directory.Exists)
                        finfo.Directory.Delete(true);
                }
            }
            else if (pc.plugintype == "WebModulePlugin")
            {
                finfo = new FileInfo(CommonHelper.WebPlatformPath + "\\" + pc.path);
                if (finfo.Exists)
                {
                    //移除dll
                    PluginXmlManage.pluginfile = finfo.FullName;
                    pluginxmlClass plugin = PluginXmlManage.getpluginclass();
                    foreach (setupClass ic in plugin.setup)
                    {
                        string dllpath = CommonHelper.PathCombine(finfo.Directory.FullName, ic.copyto);
                        if (new FileInfo(dllpath).Exists)
                        {
                            File.Delete(dllpath);
                        }
                    }

                    PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                    PluginSysManage.DeletePlugin(pc.plugintype, pc.name);
                    if (finfo.Directory.Exists)
                        finfo.Directory.Delete(true);
                }
            }

            LoadPluginData1(_pluginType);
        }

        //更新插件
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridlocal.CurrentCell == null) return;
                List<PluginClass> plist = gridlocal.DataSource as List<PluginClass>;
                PluginClass pc = plist[gridlocal.CurrentCell.RowIndex];

                string url = CommonHelper.plugin_serverurl + "/Controller.aspx?controller=efwplus_website@PluginController&method=getplugin_client&name=" + pc.name;
                HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, null, null);
                string ret = HttpWebResponseUtility.GetHttpData(response);
                string pversion = (HttpWebResponseUtility.ToResult(ret) as JavaScriptObject)["pversion"].ToString();
                string downloadpath = (HttpWebResponseUtility.ToResult(ret) as JavaScriptObject)["downloadpath"].ToString();
                string p_url = CommonHelper.plugin_serverurl + downloadpath;

                string path="";
                string localpath = "";
                string pluginsysFile="";
                if (pc.plugintype == "WebModulePlugin")
                {
                    path = CommonHelper.WebPlatformPath + "\\" + pc.path;
                    localpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + pc.name + ".zip";
                    pluginsysFile=CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                }
                else if (pc.plugintype == "WinformModulePlugin")
                {
                    path = CommonHelper.WinformPlatformPath + "\\" + pc.path;
                    localpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + pc.name + ".zip";
                   pluginsysFile=CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                }
                else if (pc.plugintype == "WcfModulePlugin")
                {
                    path = CommonHelper.WinformPlatformPath + "\\" + pc.path;
                    localpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + pc.name + ".zip";
                    pluginsysFile=CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                }
                PluginXmlManage.pluginfile = path;
                pluginxmlClass plugin = PluginXmlManage.getpluginclass();
                if (pversion == plugin.version)
                {
                    //MessageBox.Show("不用更新，已经是最新版的插件！");
                    MessageBoxEx.Show("不用更新，已经是最新版的插件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                frmprogress progress = new frmprogress();
                
                //下载
                UpDownLoadFileHelper updown = new UpDownLoadFileHelper();
                updown.Cdelegate = new UpDownLoadFileHelper.controldelegate(progress.refreshControl);
                updown.UpDown = UpDownLoadFileHelper.updown.下载;
                updown.Completed = delegate()
                {
                    progress.Close();
                    //先卸载原来插件
                    PluginSysManage.pluginsysFile = pluginsysFile;
                    PluginSysManage.DeletePlugin(pc.plugintype, pc.name);
                    Directory.Delete(new FileInfo(path).Directory.FullName,true);

                    //解压安装
                    if (CommonHelper.PluginSetup(localpath) == true)
                    {
                        File.Delete(localpath);
                        MessageBoxEx.Show("更新完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };
                updown.Cancelled = delegate()
                {
                    progress.Close();
                    
                    MessageBoxEx.Show("下载失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                updown.Start(p_url, localpath);
                progress.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBoxEx.Show("更新失败！\r\n" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //插件包安装
        private void btnpkgsetup_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (CommonHelper.PluginSetup(openFileDialog.FileName) == true)
                {
                    MessageBoxEx.Show("完成插件包安装！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPluginData1("-1");//本地插件
                }
            }
        }

        #endregion

        #region 插件开发
        //过滤开发插件
        private void pluginType_Click(object sender, EventArgs e)
        {
            string tag = (sender as PopupItem).Tag.ToString();
            _pluginType = tag;
            LoadPluginData(tag);
        }

        private void LoadPluginData(string pluginType)
        {
            List<PluginClass> plist = new List<PluginClass>();

            PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
            List<PluginClass> plist1 = PluginSysManage.GetAllPlugin();
            plist.AddRange(plist1.FindAll(x => x.plugintype == "WebModulePlugin" && x.isdevelopment == "1"));

            PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
            List<PluginClass> plist2 = PluginSysManage.GetAllPlugin();
            plist.AddRange(plist2.FindAll(x => (x.plugintype == "WinformModulePlugin" || x.plugintype == "WcfModulePlugin") && x.isdevelopment == "1"));

            List<PluginClass> list = null;
            if (pluginType == "-1")
                list = plist;
            else
            {
                switch (pluginType)
                {
                    case "web":
                        list = plist.FindAll(x => x.plugintype == "WebModulePlugin");
                        break;
                    case "winform":
                        list = plist.FindAll(x => x.plugintype == "WinformModulePlugin");
                        break;
                    case "wcf":
                        list = plist.FindAll(x => x.plugintype == "WcfModulePlugin");
                        break;

                }
            }

            griddev.Invoke((MethodInvoker)delegate() { griddev.DataSource = list; });
        }
        //新建插件
        private void newProject_Click(object sender, EventArgs e)
        {
            FrmAdd frmadd = new FrmAdd();
            frmadd.ShowDialog();
            if (frmadd.isCommit)
                LoadPluginData("-1");

            loadTitle();
        }
        //插件发布
        private void btnPack_Click(object sender, EventArgs e)
        {
            if (CommonHelper.issuekey == "!@#$%^")
            {
                if (griddev.CurrentCell == null) return;

                List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
                PluginClass pc = plist[griddev.CurrentCell.RowIndex];

                FrmPack pack = new FrmPack(pc);
                pack.ShowDialog();
            }
            else
            {
                //MessageBox.Show("sorry,你没有发布插件的权限！");
                MessageBoxEx.Show("sorry,你没有发布插件的权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //VS打开
        private void btnvsopen_Click(object sender, EventArgs e)
        {
            try
            {
                if (griddev.CurrentCell == null) return;
                List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
                PluginClass pc = plist[griddev.CurrentCell.RowIndex];
                string slnname = "";
                if (pc.plugintype == "WinformModulePlugin" || pc.plugintype == "WcfModulePlugin")
                {
                    FileInfo finfo = new FileInfo(CommonHelper.WinformPlatformPath + "\\" + pc.path);
                    if (finfo.Exists)
                    {
                        slnname = finfo.Directory.FullName + "\\" + "EFW_" + pc.name + ".sln";
                    }
                }
                else if (pc.plugintype == "WebModulePlugin")
                {
                    FileInfo finfo = new FileInfo(CommonHelper.WebPlatformPath + "\\" + pc.path);
                    if (finfo.Exists)
                    {
                        slnname = finfo.Directory.FullName + "\\" + "EFW_" + pc.name + ".sln";
                    }

                    //修改web.config中的EntLib.config配置目录
                    string webconfig = CommonHelper.WebPlatformPath + "\\Web.config";
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(webconfig);
                    XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode("enterpriseLibrary.ConfigurationSource/sources/add[@name='EntLibConfiguration']");
                    if (xn != null)
                        xn.Attributes["filePath"].Value = CommonHelper.WebPlatformPath + @"\Config\EntLib.config";
                    xmlDoc.Save(webconfig);

                }
                if (slnname != "")
                {
                    VSOpenCommand.Open(slnname);
                }
            }
            catch (Exception err)
            {
                MessageBoxEx.Show("请先配置好VisualStudio工具，点击界面上右上角的“设置”!\n\r" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //打开所在目录
        private void btnexplorer_Click(object sender, EventArgs e)
        {
            if (griddev.CurrentCell == null) return;
            List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
            PluginClass pc = plist[griddev.CurrentCell.RowIndex];
            string dirplugin = "";
            if (pc.plugintype == "WinformModulePlugin" || pc.plugintype == "WcfModulePlugin")
            {
                FileInfo finfo = new FileInfo(CommonHelper.WinformPlatformPath +"\\"+ pc.path);
                if (finfo.Exists)
                {
                    dirplugin = finfo.Directory.FullName;
                }
            }
            else if (pc.plugintype == "WebModulePlugin")
            {
                FileInfo finfo = new FileInfo(CommonHelper.WebPlatformPath + "\\" + pc.path);
                if (finfo.Exists)
                {
                    dirplugin = finfo.Directory.FullName;
                }
            }
            if (dirplugin != "")
                CommonHelper.ShellExecute(IntPtr.Zero, "open", "explorer.exe", dirplugin, "", CommonHelper.ShowCommands.SW_NORMAL);
        }
        //移除插件
        private void biDelete_Click(object sender, EventArgs e)
        {
            if (griddev.CurrentCell == null) return;

            if (MessageBoxEx.Show("是否移除此插件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;

            List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
            PluginClass pc = plist[griddev.CurrentCell.RowIndex];
            FileInfo finfo = null;
            if (pc.plugintype == "WinformModulePlugin" || pc.plugintype == "WcfModulePlugin")
            {
                finfo = new FileInfo(CommonHelper.WinformPlatformPath + "\\" + pc.path);
                if (finfo != null)
                {
                    //移除dll
                    PluginXmlManage.pluginfile = finfo.FullName;
                    pluginxmlClass plugin = PluginXmlManage.getpluginclass();
                    foreach (setupClass ic in plugin.setup)
                    {
                        string dllpath = CommonHelper.PathCombine(finfo.Directory.FullName, ic.copyto);
                        if (new FileInfo(dllpath).Exists)
                        {
                            File.Delete(dllpath);
                        }
                    }

                    PluginSysManage.pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                    PluginSysManage.DeletePlugin(pc.plugintype, pc.name);
                    if (finfo.Directory.Exists)
                        finfo.Directory.Delete(true);

                   
                }
            }
            else if (pc.plugintype == "WebModulePlugin")
            {
                finfo = new FileInfo(CommonHelper.WebPlatformPath + "\\" + pc.path);
                if (finfo != null)
                {
                    //移除dll
                    PluginXmlManage.pluginfile = finfo.FullName;
                    pluginxmlClass plugin = PluginXmlManage.getpluginclass();
                    foreach (setupClass ic in plugin.setup)
                    {
                        string dllpath = CommonHelper.PathCombine(finfo.Directory.FullName, ic.copyto);
                        if (new FileInfo(dllpath).Exists)
                        {
                            File.Delete(dllpath);
                        }
                    }

                    PluginSysManage.pluginsysFile = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                    PluginSysManage.DeletePlugin(pc.plugintype, pc.name);
                    if (finfo.Directory.Exists)
                        finfo.Directory.Delete(true);

                   
                }
            }

            LoadPluginData(_pluginType);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            FrmDevSetting frmdev = new FrmDevSetting();
            frmdev.ShowDialog();
            loadTitle();
        }
        private void btnCode_Click(object sender, EventArgs e)
        {
            if (griddev.CurrentCell == null) return;
            List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
            PluginClass pc = plist[griddev.CurrentCell.RowIndex];
            FrmCodeMaker code = new FrmCodeMaker(pc);
            code.ShowDialog();
        }

        private void 编译ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (griddev.CurrentCell == null) return;
            string retmsg = "";
            try
            {
                List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
                PluginClass pc = plist[griddev.CurrentCell.RowIndex];
                FileInfo file = null;
                if (pc.plugintype == "WinformModulePlugin" || pc.plugintype == "WcfModulePlugin")
                {
                    file = new FileInfo(CommonHelper.WinformPlatformPath + "\\" + pc.path);
                }
                else if (pc.plugintype == "WebModulePlugin")
                {
                    file = new FileInfo(CommonHelper.WebPlatformPath + "\\" + pc.path);
                }
                string slnpath = file.Directory.FullName + "\\EFW_" + pc.name + ".sln";
                retmsg = MsBuildCommand.Build(slnpath);

                loadTitle();
                MessageBoxEx.Show("编译成功！\r\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBoxEx.Show("编译失败！\r\n" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //添加现有插件项目
        private void AddProject_Click(object sender, EventArgs e)
        {
            openPlugin.InitialDirectory = CommonHelper.PathCombine(CommonHelper.AppRootPath, "..\\");
            if (openPlugin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = openPlugin.FileName;
                PluginXmlManage.pluginfile = file;
                pluginxmlClass plugin = PluginXmlManage.getpluginclass();

                string pluginsyspath = null;
                string plugintype = "";
                if (plugin.plugintype == "Web")
                {
                    plugintype = "WebModulePlugin";
                    pluginsyspath = CommonHelper.WebPlatformPath + "\\Config\\pluginsys.xml";
                }
                else if (plugin.plugintype == "Winform")
                {
                    plugintype = "WinformModulePlugin";
                    pluginsyspath = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                }
                else if (plugin.plugintype == "Wcf")
                {
                    plugintype = "WcfModulePlugin";
                    pluginsyspath = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
                }

                if (PluginSysManage.ContainsPlugin(plugintype, plugin.name) == false)
                {
                    PluginSysManage.pluginsysFile = pluginsyspath;
                    PluginSysManage.AddPlugin(plugintype, plugin.name, "ModulePlugin/" + plugin.name + "/plugin.xml", plugin.title, "1");
                    
                    MessageBoxEx.Show("插件添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPluginData("-1");
                }
                else
                {
                    MessageBoxEx.Show("你选择的插件已添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        private void biHelp_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.efwplus.cn/efwplus/Docs/index.htm");
        }

        private void metroShell1_SettingsButtonClick(object sender, EventArgs e)
        {
            FrmSetting fs = new FrmSetting();
            fs.ShowDialog();
        }

        private void metroShell1_HelpButtonClick(object sender, EventArgs e)
        {
            Process.Start("http://www.efwplus.cn");
        }

        private void ChangeMetroThemeExecuted(object sender, EventArgs e)
        {
            ICommandSource source = (ICommandSource)sender;
            MetroColorGeneratorParameters theme = (MetroColorGeneratorParameters)source.CommandParameter;
            StyleManager.MetroColorGeneratorParameters = theme;
        }

        private void UpdateControlsSizeAndLocation()
        {
            //metroToolbar1.Location = new Point((this.Width - metroToolbar1.Width) / 2, metroToolbar1.Parent.Height - metroToolbar1.Height - 1);
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateControlsSizeAndLocation();
            base.OnResize(e);
        }

        private void buttonItem30_Click(object sender, EventArgs e)
        {
            if (gridweb.CurrentCell == null) return;

            DataTable dt = gridweb.DataSource as DataTable;
            int rowindex = gridweb.CurrentCell.RowIndex;
            int Id = Convert.ToInt32(dt.Rows[rowindex]["Id"]);
            string name = dt.Rows[rowindex]["name"].ToString();
            string ptype = dt.Rows[rowindex]["plugintype"].ToString();
            string srcpath = dt.Rows[rowindex]["srcpath"].ToString();
            //string designdoc = dt.Rows[rowindex]["designdoc"].ToString();
            string url = CommonHelper.plugin_serverurl + srcpath;

            if (srcpath == "") return;

            string localpath = "";
            switch (ptype)
            {
                case "web":
                    localpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + name + "_src.zip";
                    break;
                case "winform":
                    localpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + name + "_src.zip";
                    break;
                case "wcf":
                    localpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + name + "_src.zip";
                    break;
            }

            frmprogress progress = new frmprogress();
            //下载
            UpDownLoadFileHelper updown = new UpDownLoadFileHelper();
            updown.Cdelegate = new UpDownLoadFileHelper.controldelegate(progress.refreshControl);
            updown.UpDown = UpDownLoadFileHelper.updown.下载;
            updown.Completed = delegate()
            {
                progress.Close();
                MessageBoxEx.Show("源代码下载完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            updown.Cancelled = delegate()
            {
                progress.Close();
                MessageBoxEx.Show("下载失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            updown.Start(url, localpath);
            progress.ShowDialog();

            //MessageBoxEx.Show("暂未开放！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            if (gridweb.CurrentCell == null) return;

            DataTable dt = gridweb.DataSource as DataTable;
            int rowindex = gridweb.CurrentCell.RowIndex;
            int Id = Convert.ToInt32(dt.Rows[rowindex]["Id"]);
            string name = dt.Rows[rowindex]["name"].ToString();
            string ptype = dt.Rows[rowindex]["plugintype"].ToString();
            //string srcpath = dt.Rows[rowindex]["srcpath"].ToString();
            string designdoc = dt.Rows[rowindex]["designdoc"].ToString();
            string url = CommonHelper.plugin_serverurl + designdoc;

            if (designdoc == "") return;

            string localpath = "";
            switch (ptype)
            {
                case "web":
                    localpath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + name + "\\" + name + "_designdoc.vsd";
                    break;
                case "winform":
                    localpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + name + "\\" + name + "_designdoc.vsd";
                    break;
                case "wcf":
                    localpath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + name + "\\" + name + "_designdoc.vsd";
                    break;
            }

            frmprogress progress = new frmprogress();
            //下载
            UpDownLoadFileHelper updown = new UpDownLoadFileHelper();
            updown.Cdelegate = new UpDownLoadFileHelper.controldelegate(progress.refreshControl);
            updown.UpDown = UpDownLoadFileHelper.updown.下载;
            updown.Completed = delegate()
            {
                progress.Close();
                MessageBoxEx.Show("设计文档下载完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            updown.Cancelled = delegate()
            {
                progress.Close();
                MessageBoxEx.Show("下载失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            updown.Start(url, localpath);
            progress.ShowDialog();
            //MessageBoxEx.Show("暂未开放！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void labelefw_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.efwplus.cn");
        }

        private void btnOpenEwin_Click(object sender, EventArgs e)
        {
            CommonHelper.OpenProcess(CommonHelper.AppRootPath + "\\EFWWin.bat");
        }

        private void btnOpenWcfhost_Click(object sender, EventArgs e)
        {
            CommonHelper.OpenProcess(CommonHelper.AppRootPath + "\\WCF服务主机.bat");
        }

        private void btnOpenWeb_Click(object sender, EventArgs e)
        {
            CommonHelper.OpenProcess(CommonHelper.AppRootPath + "\\NetWebServer.exe");
        }

        private void btnFBWin_Click(object sender, EventArgs e)
        {
            FrmIssue frmissue = new FrmIssue("WinformModulePlugin");
            frmissue.ShowDialog();
        }

        private void btnFBWcf_Click(object sender, EventArgs e)
        {
            FrmIssue frmissue = new FrmIssue("WcfModulePlugin");
            frmissue.ShowDialog();
        }

        private void btnFBWeb_Click(object sender, EventArgs e)
        {
            FrmIssue frmissue = new FrmIssue("WebModulePlugin");
            frmissue.ShowDialog();
        }

        private void tsRun1_Click(object sender, EventArgs e)
        {
            if (gridlocal.CurrentCell == null) return;
            List<PluginClass> plist = gridlocal.DataSource as List<PluginClass>;
            PluginClass pc = plist[gridlocal.CurrentCell.RowIndex];

            if (pc.plugintype == "WinformModulePlugin" || pc.plugintype == "WcfModulePlugin")
            {
                if (pc.name != entryplugin)
                {
                    FrmDevSetting frmdev = new FrmDevSetting();
                    frmdev.ShowDialog();
                    loadTitle();
                    return;
                }
                if (pc.plugintype == "WcfModulePlugin")
                {
                    CommonHelper.OpenProcess(CommonHelper.AppRootPath + "\\WCF服务主机.bat");
                }
                CommonHelper.OpenProcess(CommonHelper.AppRootPath + "\\EFWWin.bat");
            }
            else if (pc.plugintype == "WebModulePlugin")
            {
                CommonHelper.OpenProcess(CommonHelper.AppRootPath + "\\NetWebServer.exe");
            }
        }

        private void tsRun2_Click(object sender, EventArgs e)
        {
            if (griddev.CurrentCell == null) return;
            List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
            PluginClass pc = plist[griddev.CurrentCell.RowIndex];
            if (pc.plugintype == "WinformModulePlugin" || pc.plugintype == "WcfModulePlugin")
            {
                if (pc.name != entryplugin)
                {
                    FrmDevSetting frmdev = new FrmDevSetting();
                    frmdev.ShowDialog();
                    loadTitle();
                    return;
                }

                if (pc.plugintype == "WcfModulePlugin")
                {
                    CommonHelper.OpenProcess(CommonHelper.AppRootPath + "\\WCF服务主机.bat");
                }
                CommonHelper.OpenProcess(CommonHelper.AppRootPath + "\\EFWWin.bat");
            }
            else if (pc.plugintype == "WebModulePlugin")
            {
                CommonHelper.OpenProcess(CommonHelper.AppRootPath + "\\NetWebServer.exe");
            }
        }

        private void griddev_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewX grid = sender as DataGridViewX;
                if (grid.CurrentCell != null)
                {
                    grid.CurrentCell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            }
        }

        private void btnStartSetting_Click(object sender, EventArgs e)
        {
            FrmDevSetting frmdev = new FrmDevSetting();
            frmdev.ShowDialog();
            loadTitle();
        }

        private void btnLoadPlugin_Click(object sender, EventArgs e)
        {
            FrmPluginLoadManage load = new FrmPluginLoadManage();
            load.ShowDialog();
            loadTitle();
        }

        private void 配置插件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (griddev.CurrentCell == null) return;

            List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
            PluginClass pc = plist[griddev.CurrentCell.RowIndex];

            FrmPluginConfigEdit config = new FrmPluginConfigEdit(pc);
            config.ShowDialog();
        }

        private void 编辑设计文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (griddev.CurrentCell == null) return;

            List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
            PluginClass pc = plist[griddev.CurrentCell.RowIndex];

            string path = string.Empty;
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

            string docfile = new FileInfo(path).DirectoryName + "\\" + (new FileInfo(path).Directory.Name) + "_designdoc.vsd";
            if (!File.Exists(docfile))
            {
                File.Copy(CommonHelper.AppRootPath + "\\Template\\designdoc.vsd", docfile);
            }
            ApplicationClass app = new ApplicationClass();
            FileInfo file = new FileInfo(docfile);
            Document doc = app.Documents.OpenEx(docfile, (short)Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visOpenRW);
            app.BeforeDocumentSave += app_BeforeDocumentSave;
        }

        void app_BeforeDocumentSave(Document doc)
        {
            if (griddev.CurrentCell == null) return;

            List<PluginClass> plist = griddev.DataSource as List<PluginClass>;
            PluginClass pc = plist[griddev.CurrentCell.RowIndex];
            string path = string.Empty;
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
            string docpic = new FileInfo(path).DirectoryName + "_pic";
            if (!Directory.Exists(docpic))
            {
                Directory.CreateDirectory(docpic);
            }
            foreach (Page page in doc.Pages)
            {
                page.Export(docpic + "\\" + page.Name + ".jpg");
            }
        }
    }
}
