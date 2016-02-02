using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PluginManageTool.Common;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor;
using System.IO;
using DevComponents.DotNetBar;

namespace PluginManageTool.CodeMaker
{
    public partial class FrmCodeEditor : Form, ICodeMaker
    {
        string PluginName;
        string ProjectPath;
        string[] Filepaths;
        string PropertyKey;
        public FrmCodeEditor(string _pluginname, string _ProjectPath, string[] _filepaths, string _propertyKey)
        {
            InitializeComponent();

            PluginName = _pluginname;
            ProjectPath = _ProjectPath;
            Filepaths = _filepaths;
            PropertyKey = _propertyKey;

            CustomPropertyCollection list = new CustomPropertyCollection();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(CommonHelper.AppRootPath + "\\Template\\TemplateFile\\CodeProperty.xml");
            XmlNodeList nodelist = xmlDoc.DocumentElement.SelectNodes("CodeProperty[@PropertyKey='" + PropertyKey + "']");
            if (nodelist.Count > 0)
            {
                string CodeDataClassName = nodelist[0].Attributes["CodeDataClassName"].Value;
                string TemplateFileName = nodelist[0].Attributes["TemplateFileName"].Value;
                string CodeFileNameTopropertyName = nodelist[0].Attributes["CodeFileNameTopropertyName"].Value;
                AbstractCodeTemplateData ctdata = (AbstractCodeTemplateData)Activator.CreateInstance(Type.GetType(CodeDataClassName), null);
                ctdata.AppName = PluginName;
                ctdata.TemplateFileName = TemplateFileName;
                ctdata.CodeFileNameTopropertyName = CodeFileNameTopropertyName;

                foreach (XmlNode var in nodelist[0].ChildNodes)
                {
                    CustomProperty property = new CustomProperty(var.Attributes["name"].Value, var.Attributes["propertyName"].Value, var.Attributes["category"].Value, var.Attributes["description"].Value, ctdata);
                    if (var.Attributes["defaultValue"] != null)
                        property.DefaultValue = var.Attributes["defaultValue"].Value;
                    if (var.Attributes["IsReadOnly"] != null && var.Attributes["IsReadOnly"].Value == "true")
                        property.IsReadOnly = true;
                    if (var.Attributes["propertyName"].Value == "FileNameEditor")
                        property.EditorType = typeof(System.Windows.Forms.Design.FileNameEditor);
                    list.Add(property);
                }

                propertyGrid.PropertyValueChanged -= new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);
                propertyGrid.SelectedObject = list;
                propertyGrid.Tag = ctdata;
                propertyGrid.ExpandAllGridItems();
                propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);

            }

        }

        //属性值发生更改
        void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            AbstractCodeTemplateData data = (AbstractCodeTemplateData)((PropertyGrid)s).Tag;
            data.DataInit();
            string[] tempFiles = data.TemplateFileName.Split(new char[] { '|'});
            string[] codeFiles = data.CodeFileNameTopropertyName.Split(new char[] { '|' });

            tabControlList.TabPages.Clear();
            for (int i = 0; i < tempFiles.Length; i++)
            {
                TemplateHelper template = new TemplateHelper(CommonHelper.AppRootPath + "\\Template\\TemplateFile");
                template.Put(PropertyKey, data);
                string code = template.BuildString(tempFiles[i]);

                string codefile= data.GetType().GetProperty(codeFiles[i]).GetValue(data, null).ToString();//反射获取文件名
                TabPage tabpage = new TabPage(codefile);
                TextEditorControl textEditor = new TextEditorControl();
                textEditor.Dock = System.Windows.Forms.DockStyle.Fill;
                textEditor.ImeMode = System.Windows.Forms.ImeMode.On;
                textEditor.IsReadOnly = false;
                textEditor.ShowVRuler = false;
                textEditor.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategyForFile(CommonHelper.AppRootPath + "\\Template\\TemplateFile\\" + tempFiles[i]);
                textEditor.Text = code;
                tabpage.Controls.Add(textEditor);
                this.tabControlList.Controls.Add(tabpage);
            }
        }


        #region ICodeMaker 成员

        public void CodeSave()
        {
            try
            {
                for (int i = 0; i < Filepaths.Length; i++)
                {
                    string filepath = CommonHelper.PathCombine(ProjectPath, Filepaths[i]) + "\\" + tabControlList.TabPages[i].Text;
                    string code = (tabControlList.TabPages[i].Controls[0] as TextEditorControl).Text;

                    FileInfo file = new FileInfo(filepath);
                    if (!file.Directory.Exists)
                    {
                        file.Directory.Create();
                    }
                    if (!file.Exists)
                    {
                        //Create a file to write to.
                        using (StreamWriter sw = new StreamWriter(file.Create(), Encoding.UTF8))
                        {
                            sw.Write(code);
                        }
                    }
                }
                MessageBoxEx.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBoxEx.Show("保存失败！\n\r" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
    }

    
}
