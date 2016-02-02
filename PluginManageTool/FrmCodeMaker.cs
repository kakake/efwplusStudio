using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PluginManageTool.CodeMaker;
using PluginManageTool.Common;
using DevComponents.DotNetBar.Metro;

namespace PluginManageTool
{
    public partial class FrmCodeMaker : MetroForm
    {
        PluginClass plugin;
        string ProjectPath;
        ICodeMaker CodeMaker;
        public FrmCodeMaker(PluginClass pc)
        {
            InitializeComponent();
            plugin = pc;
            treeCodeType.ExpandAll();

            if (plugin.plugintype == "WebModulePlugin")
            {
                txtcodepath.Text = ProjectPath = CommonHelper.WebPlatformPath + "\\ModulePlugin\\" + plugin.name;
            }
            else if (plugin.plugintype == "WinformModulePlugin" || plugin.plugintype == "WcfModulePlugin")
            {
                txtcodepath.Text = ProjectPath = CommonHelper.WinformPlatformPath + "\\ModulePlugin\\" + plugin.name;
            }

        }

        private void treeCodeType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            panelMain.Controls.Clear();
            if (e.Node.Tag == null)
            {
                return;
            }
            //显示界面
            //FrmCodeEditor|{PluginName}\Entity|Entity
            //FrmCodeEditor|..\Aspx;..\JS|AspxJS

            //FrmCodeEditor：对应代码生成界面
            //..\Aspx;..\JS：生成的代码文件存放路径，多个文件多个路径,与CodeProperty.xml配置文件中的TemplateFileName属性对应
            //AspxJS：对应配置文件CodeProperty.xml
            string frmname = e.Node.Tag.ToString().Split(new char[] { '|' })[0];
            string[] filepaths = e.Node.Tag.ToString().Split(new char[] { '|' })[1].Split(new char[] { ';' });
            string PropertyKey = null;
            if (e.Node.Tag.ToString().Split(new char[] { '|' }).Length > 2)
            {
                PropertyKey = e.Node.Tag.ToString().Split(new char[] { '|' })[2];
            }
            Form frm = GetMakerFrom(frmname, filepaths,PropertyKey);
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.FormBorderStyle = FormBorderStyle.None;
            panelMain.Controls.Add(frm);

            frm.Show();

            CodeMaker = (ICodeMaker)frm;


        }

        private Form GetMakerFrom(string frmname, string[] filepaths, string PropertyKey)
        {
            for (int i = 0; i < filepaths.Length; i++)
            {
                filepaths[i] = filepaths[i].Replace("{PluginName}", plugin.name);
            }
            Form frm=null;
            switch (frmname)
            {
                case "FrmEntityMaker":
                    frm = new FrmEntityMaker(plugin.name, ProjectPath, filepaths);
                    break;
                default:
                    frm = new FrmCodeEditor(plugin.name, ProjectPath, filepaths, PropertyKey);
                    break;
            }
            return frm;
        }

        private void btncodesave_Click(object sender, EventArgs e)
        {
            if (CodeMaker != null)
                CodeMaker.CodeSave();
        }
    }
}
