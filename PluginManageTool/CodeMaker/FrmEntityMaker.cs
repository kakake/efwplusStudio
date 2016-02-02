using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using PluginManageTool.Common;
using DevComponents.DotNetBar;

namespace PluginManageTool.CodeMaker
{
    public partial class FrmEntityMaker : Form, PluginManageTool.CodeMaker.ICodeMaker
    {
        DataSet dsDb = null;
        string ProjectPath;
        public FrmEntityMaker(string pluginname, string _ProjectPath, string[] filepaths)
        {
            InitializeComponent();

            txtnamespace.Text = pluginname + ".Entity";
            ProjectPath = _ProjectPath+"\\"+filepaths[0];

            dsDb = new DataSet();
            DataTable DbTableList = dsDb.Tables.Add("DbTableList");
            DbTableList.Columns.Add("Check", typeof(Boolean));
            DbTableList.Columns.Add("TableName", typeof(string));

            DataTable DbColumnList = dsDb.Tables.Add("DbColumnList");
            DbColumnList.Columns.Add("TableName", typeof(string));
            DbColumnList.Columns.Add("colname", typeof(string));
            DbColumnList.Columns.Add("typename", typeof(string));
            DbColumnList.Columns.Add("_identity", typeof(Boolean));
            DbColumnList.Columns.Add("remarks", typeof(string));

            gridTable.DataSource = DbTableList;
        }

        public string GetTypeMappingValue(string name, string key)
        {
            string xpath = name + "/Item[@key='" + key + "']";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(CommonHelper.AppRootPath+"\\Template\\TemplateFile\\TypeMapping.xml");
            XmlNodeList xmlNodes = xmlDoc.DocumentElement.SelectNodes(xpath);

            if (xmlNodes.Count > 0)
            {
                return xmlNodes[0].Attributes["value"].Value;
            }
            return null;
        }

        private void gridTable_Click(object sender, EventArgs e)
        {
            if (gridTable.CurrentCell != null)
            {
                bool b = Convert.ToBoolean(gridTable[0, gridTable.CurrentCell.RowIndex].Value);
                if (b)
                    gridTable[0, gridTable.CurrentCell.RowIndex].Value = false;
                else
                    gridTable[0, gridTable.CurrentCell.RowIndex].Value = true;
            }
        }

        private void btnGetTableList_Click(object sender, EventArgs e)
        {
            try
            {
                dsDb.Tables["DbTableList"].Rows.Clear();
                DataRow dr;

                DataBaseOperator database = new DataBaseOperator(this.DbcontionStr.Text);

                SqlDataReader read;

                string strsql = @"select   table_name as tabname   from   INFORMATION_SCHEMA.TABLES order by table_name";
                read = database.ExecuteReader(strsql);
                while (read.Read())
                {
                    dr = dsDb.Tables["DbTableList"].NewRow();
                    dr["Check"] = false;
                    dr["TableName"] = read["tabname"].ToString();
                    dsDb.Tables["DbTableList"].Rows.Add(dr);
                }
                read.Close();

                strsql = @"select b.name as tablename,a.name as colname
                                    ,(select top 1 name from sys.types where system_type_id=a.system_type_id) as typename
          
                                    ,(case when is_identity=1 then 'Y' else 'N' end) as _identity ,c.value as remarks
                                    from sys.columns a  left join sys.objects b on a.object_id=b.object_id 
									left join sys.extended_properties c  on a.object_id=c.major_id and a.column_id=c.minor_id
                                    where b.type='U' and b.name in (select   table_name as tabname   from   INFORMATION_SCHEMA.TABLES);";
                read = database.ExecuteReader(strsql);
                while (read.Read())
                {
                    dr = dsDb.Tables["DbColumnList"].NewRow();
                    dr["TableName"] = read["tablename"].ToString();
                    dr["colname"] = read["colname"].ToString();
                    dr["typename"] = read["typename"].ToString();
                    dr["_identity"] = read["_identity"].ToString() == "Y" ? true : false;
                    dr["remarks"] = read["remarks"].ToString();
                    dsDb.Tables["DbColumnList"].Rows.Add(dr);
                }
            }
            catch (Exception err)
            {
                MessageBoxEx.Show("操作失败！\n\r" + err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAll.Checked == true)
            {
                for (int i = 0; i < gridTable.RowCount; i++)
                {
                    gridTable[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < gridTable.RowCount; i++)
                {
                    bool b = Convert.ToBoolean(gridTable[0, i].Value);
                    if (b)
                        gridTable[0, i].Value = false;
                    else
                        gridTable[0, i].Value = true;
                }
            }
        }

        private void btnGeneration_Click(object sender, EventArgs e)
        {

            DataTable DbTableList = dsDb.Tables["DbTableList"];
            DataTable DbColumnList = dsDb.Tables["DbColumnList"];
            for (int i = 0; i < DbTableList.Rows.Count; i++)
            {
                if (Convert.ToBoolean(DbTableList.Rows[i][0]) == true)
                {
                    string tempName = DbTableList.Rows[i][1].ToString();
                    TemplateHelper template = new TemplateHelper(CommonHelper.AppRootPath+"\\Template\\TemplateFile");

                    EntityTemplateData data = new EntityTemplateData();
                    data.AppName = txtnamespace.Text;
                    data.ClassName = tempName;
                    data.TableName = DbTableList.Rows[i][1].ToString();
                    data.Property = new List<ClassProperty>();

                    DataRow[] drs = DbColumnList.Select("TableName='" + data.TableName + "'");
                    for (int k = 0; k < drs.Length; k++)
                    {
                        if (drs[k]["colname"].ToString().ToLower() != "workid")//过滤掉WorkId字段
                        {
                            ClassProperty pro = new ClassProperty();
                            pro.varName = " _" + drs[k]["colname"].ToString().ToLower();
                            pro.DataKey = Convert.ToBoolean(drs[k]["_identity"]) == true ? "true" : "false";
                            pro.FieldName = drs[k]["colname"].ToString();
                            pro.IsInsert = Convert.ToBoolean(drs[k]["_identity"]) != true ? "true" : "false";
                            //pro.IsSingleQuote = CommonHelper.GetTypeMappingIsSingleQuote("SqlServerToCS", drs[k]["typename"].ToString());
                            pro.Match = drs[k]["typename"].ToString() == "uniqueidentifier" ? "Custom:Guid" : "";
                            pro.PropertyName = drs[k]["colname"].ToString();
                            pro.remarks = drs[k]["remarks"].ToString();
                            pro.TypeName = GetTypeMappingValue("SqlServerToCS", drs[k]["typename"].ToString());
                            data.Property.Add(pro);
                        }
                    }

                    template.Put("Entity", data);

                    string code = template.BuildString("TableEntity.cs");

                    FileInfo file = new FileInfo(ProjectPath+"\\" + tempName + ".cs");
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

            }
            MessageBoxEx.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void CodeSave()
        {
            btnGeneration_Click(null, null);
        }
    }
}
