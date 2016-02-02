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
    public partial class FrmSetting : MetroForm
    {
        string appconfig = CommonHelper.AppRootPath + "\\PluginManageTool.exe.config";
        XmlDocument xmlDoc;
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(appconfig);
            XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode("appSettings/add[@key='WebPlatformPath']");
            if (xn != null)
                txtwebpath.Text = xn.Attributes["value"].Value;
            xn = xmlDoc.DocumentElement.SelectSingleNode("appSettings/add[@key='WinformPlatformPath']");
            if (xn != null)
                txtwinpath.Text = xn.Attributes["value"].Value;
            xn = xmlDoc.DocumentElement.SelectSingleNode("appSettings/add[@key='devenvExeFile']");
            if (xn != null)
                txtvspath.Text = xn.Attributes["value"].Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (xmlDoc == null) return;
            XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode("appSettings/add[@key='WebPlatformPath']");
            if (xn != null)
                xn.Attributes["value"].Value = txtwebpath.Text.Trim();
            xn = xmlDoc.DocumentElement.SelectSingleNode("appSettings/add[@key='WinformPlatformPath']");
            if (xn != null)
                xn.Attributes["value"].Value = txtwinpath.Text.Trim();
            xn = xmlDoc.DocumentElement.SelectSingleNode("appSettings/add[@key='devenvExeFile']");
            if (xn != null)
                xn.Attributes["value"].Value = txtvspath.Text.Trim();
            xmlDoc.Save(appconfig);

            CommonHelper.WebPlatformPath = CommonHelper.PathCombine(CommonHelper.AppRootPath, txtwebpath.Text.Trim());
            CommonHelper.WinformPlatformPath = CommonHelper.PathCombine(CommonHelper.AppRootPath, txtwinpath.Text.Trim());
            VSOpenCommand.devenvExeFile =txtvspath.Text.Trim();

            MessageBoxEx.Show("保存成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
