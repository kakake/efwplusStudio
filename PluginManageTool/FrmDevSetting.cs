using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using PluginManageTool.Common;
using System.Xml;
using DevComponents.DotNetBar;

namespace PluginManageTool
{
    public partial class FrmDevSetting : MetroForm
    {
        private string pluginsysFile = CommonHelper.WinformPlatformPath + "\\Config\\pluginsys.xml";
        private string appconfig = CommonHelper.WinformPlatformPath + "\\EFWWin.exe.config";
        private string netwebserver = CommonHelper.AppRootPath + "\\NetWebServer.exe.config";

        XmlDocument xmlDoc_plugin;
        XmlDocument xmlDoc_app;
        XmlDocument xmlDoc_webserver;
        List<PluginClass> plist;

        public FrmDevSetting()
        {
            InitializeComponent();
        }

        string _starturl = null;
        public FrmDevSetting(string starturl)
        {
            InitializeComponent();
            _starturl = starturl;
        }

        string _pluginname = null;
        string _controllername = null;
        public FrmDevSetting(string pluginname,string controllername)
        {
            InitializeComponent();
            _pluginname = pluginname;
            _controllername = controllername;
        }

        private void FrmDevSetting_Load(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
            xmlDoc_plugin = new System.Xml.XmlDocument();
            xmlDoc_plugin.Load(pluginsysFile);

            xmlDoc_app = new System.Xml.XmlDocument();
            xmlDoc_app.Load(appconfig);

            xmlDoc_webserver = new System.Xml.XmlDocument();
            xmlDoc_webserver.Load(netwebserver);

            PluginSysManage.pluginsysFile = pluginsysFile;
            plist = PluginSysManage.GetAllPlugin();

            XmlNode node= xmlDoc_app.DocumentElement.SelectSingleNode("appSettings/add[@key='ClientType']");
            if (node != null)
            {
                string ClientType = node.Attributes["value"].Value;
                if (ClientType == "Winform")
                {
                    rbwinform.Checked = true;
                }
                else if (ClientType == "WCFClient")
                {
                    rbwcfclient.Checked = true;
                }

                rbwinform_CheckedChanged(null, null);
            }

            node = xmlDoc_app.DocumentElement.SelectSingleNode("appSettings/add[@key='WCF_endpoint']");
            if (node != null)
            {
                txtwcfendpoint.Text = node.Attributes["value"].Value;
            }

            node = xmlDoc_webserver.DocumentElement.SelectSingleNode("appSettings/add[@key='defaultpage']");
            if (node != null)
            {
                txtStartPage.Text = node.Attributes["value"].Value;
            }
            node = xmlDoc_webserver.DocumentElement.SelectSingleNode("appSettings/add[@key='port']");
            if (node != null)
            {
                txtPort.Text = node.Attributes["value"].Value;
            }


        }

        private void rbwinform_CheckedChanged(object sender, EventArgs e)
        {
            cbpname.DisplayMember = "name";
            cbpname.ValueMember = "name";
            string entryplugin, entrycontroller;
            if (rbwinform.Checked)
            {
                cbpname.DataSource = plist.FindAll(x => x.plugintype == "WinformModulePlugin");
                PluginSysManage.GetWinformEntry(out entryplugin, out entrycontroller);
            }
            else
            {
                cbpname.DataSource = plist.FindAll(x => x.plugintype == "WcfModulePlugin");
                PluginSysManage.GetWcfClientEntry(out entryplugin, out entrycontroller);
            }

            cbpname.SelectedValue = entryplugin;
            txtcname.Text = entrycontroller;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlNode node = xmlDoc_app.DocumentElement.SelectSingleNode("appSettings/add[@key='ClientType']");
            if (rbwinform.Checked)
            {
                node.Attributes["value"].Value = "Winform";
                PluginSysManage.SetWinformEntry(cbpname.Text, txtcname.Text);
            }
            else if (rbwcfclient.Checked)
            {
                node.Attributes["value"].Value = "WCFClient";
                PluginSysManage.SetWcfClientEntry(cbpname.Text, txtcname.Text);
            }

            node = xmlDoc_app.DocumentElement.SelectSingleNode("appSettings/add[@key='WCF_endpoint']");
            node.Attributes["value"].Value = txtwcfendpoint.Text;

            xmlDoc_app.Save(appconfig);
            
            MessageBoxEx.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            XmlNode node = xmlDoc_webserver.DocumentElement.SelectSingleNode("appSettings/add[@key='defaultpage']");
            node.Attributes["value"].Value = txtStartPage.Text;
            node = xmlDoc_webserver.DocumentElement.SelectSingleNode("appSettings/add[@key='port']");
            node.Attributes["value"].Value = txtPort.Text;

            xmlDoc_webserver.Save(netwebserver);

            MessageBoxEx.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
