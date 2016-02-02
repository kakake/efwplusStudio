using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlAdmin;
using System.Data;
using Newtonsoft.Json;
using CoreFrame.API;
using CoreFrame.Init;
using NVelocity.App;
using Commons.Collections;
using NVelocity.Runtime;
using NVelocity;
using System.IO;
using NVelocity.Context;
using CoreFrame.Common;

namespace AppCenter.Manager.Controller
{
    public class websqlserverController:CoreFrame.API.AbstractJQBEController
    {
        public void Login()
        {
            string servername = ParamsData["servername"];
            string logintype = ParamsData["logintype"];
            string uid = "";
            string pwd = "";
            uid = ParamsData["uid"];
            pwd = ParamsData["pwd"];

            bool useIntegrated;
            SqlServer server;
            SqlAdmin.Security security = new SqlAdmin.Security();

            if (logintype == "0")
            {
                server = new SqlServer(servername, uid, pwd, true);
                useIntegrated = true;
            }
            else
            {

                server = new SqlServer(servername, uid, pwd, false);
                useIntegrated = false;
            }

            if (server.IsUserValid())
            {
                if (useIntegrated)
                {
                    AdminUser.CurrentUser = new AdminUser(servername, uid, pwd, true);
                    security.WriteCookieForFormsAuthentication(server.Username, server.Password, false, SqlLoginType.NTUser);
                }
                else
                {
                    AdminUser.CurrentUser = new AdminUser(servername, uid, pwd, false);
                    security.WriteCookieForFormsAuthentication(
                        server.Username,
                        server.Password,
                        false,
                        SqlLoginType.Standard);
                }

                PutOutData.Add("server", server);

                server.Connect();
                SqlDatabaseCollection databases = server.Databases;
                server.Disconnect();
                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("encodedname");
                dt.Columns.Add("size");
                for (int i = 0; i < databases.Count; i++)
                {
                    SqlDatabase database = databases[i];
                    dt.Rows.Add(new object[] { database.Name, database.Name, database.Size == -1 ? "Unknown" : String.Format("{0}MB", database.Size) });
                }

                JsonResult = RetSuccess(null, JavaScriptConvert.SerializeObject(dt));
            }
            else
            {
                JsonResult = RetError("数据库连接失败！");
            }
        }

        public void GetServer()
        {
            if (sessionData.ContainsKey("server"))
            {
                SqlServer server = (SqlServer)sessionData["server"];

                server.Connect();
                SqlDatabaseCollection databases = server.Databases;
                server.Disconnect();
                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("encodedname");
                dt.Columns.Add("size");
                for (int i = 0; i < databases.Count; i++)
                {
                    SqlDatabase database = databases[i];
                    dt.Rows.Add(new object[] { database.Name, database.Name, database.Size == -1 ? "Unknown" : String.Format("{0}MB", database.Size) });
                }

                JsonResult= RetSuccess(null, JavaScriptConvert.SerializeObject(dt));
            }
            else
            {
                JsonResult = RetError();
            }
        }

        public void GetDbTable()
        {
            try
            {
                string dbname = ParamsData["database"];

                SqlServer server = (SqlServer)sessionData["server"];
                server.Connect();
                SqlDatabase database = server.Databases[dbname];
                SqlTableCollection tables = database.Tables;
                SqlStoredProcedureCollection splist = database.StoredProcedures;



                List<treeNode> tree = new List<treeNode>();
                treeNode rootnode = new treeNode(-1, dbname);
                rootnode.attributes = new Dictionary<string, object>();
                rootnode.attributes.Add("type", "db");
                rootnode.iconCls = "icon-db";

                rootnode.children = new List<treeNode>();

                treeNode rootnode2 = new treeNode(-2, "表");
                if (tables.Count > 0)
                    rootnode2.state = "closed";
                rootnode2.attributes = new Dictionary<string, object>();
                rootnode2.attributes.Add("type", "");
                rootnode2.iconCls = "";
                rootnode.children.Add(rootnode2);//

                //rootnode.children = new List<treeNode>();
                treeNode rootnode3 = new treeNode(-3, "存储过程");
                if (splist.Count > 0)
                    rootnode3.state = "closed";
                rootnode3.attributes = new Dictionary<string, object>();
                rootnode3.attributes.Add("type", "");
                rootnode3.iconCls = "";
                rootnode.children.Add(rootnode3);//

                if (tables.Count > 0)
                {
                    rootnode2.children = new List<treeNode>();
                    for (int i = 0; i < tables.Count; i++)
                    {
                        if (tables[i].TableType == SqlObjectType.User)
                        {
                            SqlColumnCollection columns = tables[i].Columns;

                            treeNode node = new treeNode(i, tables[i].Name);
                            if (columns.Count > 0)
                                node.state = "closed";
                            node.attributes = new Dictionary<string, object>();
                            node.attributes.Add("type", "table");
                            node.iconCls = "icon-table";
                            rootnode2.children.Add(node);


                            if (columns.Count > 0)
                            {
                                node.children = new List<treeNode>();
                                for (int k = 0; k < columns.Count; k++)
                                {
                                    string defaultvalue = columns[k].ColumnInformation.DefaultValue == null ? "null" : columns[k].ColumnInformation.DefaultValue;
                                    treeNode node2 = new treeNode(k, columns[k].ColumnInformation.Name + "(" + columns[k].ColumnInformation.DataType + "," + defaultvalue + ")");
                                    node2.attributes = new Dictionary<string, object>();
                                    node2.attributes.Add("type", "column");
                                    if (columns[k].ColumnInformation.Key)
                                        node2.iconCls = "icon-key";
                                    else
                                        node2.iconCls = "icon-column";
                                    node.children.Add(node2);
                                }
                            }
                        }
                    }
                }

                if (splist.Count > 0)
                {
                    rootnode3.children = new List<treeNode>();
                    for (int i = 0; i < splist.Count; i++)
                    {
                        if (splist[i].StoredProcedureType == SqlObjectType.StoredProcedure)
                        {
                            treeNode node = new treeNode(i, splist[i].Name);
                            node.attributes = new Dictionary<string, object>();
                            node.attributes.Add("type", "sp");
                            rootnode3.children.Add(node);
                        }
                    }
                }

                tree.Add(rootnode);

                server.Disconnect();
                JsonResult = ToTreeJson(tree);
            }
            catch (Exception err)
            {
                JsonResult = RetError("获取数据库相关数据错误：//n//r" + err.Message);
            }
        }

        public void GetQuerySQLTpl()
        {
            string sqltext = ParamsData["sqltext"];
            string dbname = ParamsData["database"];
            string sqlname = ParamsData["sqlname"];


            if (sqltext.Trim() != "")
            {
                try
                {
                    SqlServer server = (SqlServer)sessionData["server"];

                    server.Connect();
                    SqlDatabase database = server.Databases[dbname];
                    DataTable[] tables = null;
                    tables = database.Query(sqltext);
                    server.Disconnect();

                    string tpltext = "";
                    string msg = "";

                    List<string>[] tplcollist = new List<string>[tables.Length];
                    List<string> tplcolumn;

                    for (int i = 0; i < tables.Length; i++)
                    {
                        tplcolumn = new List<string>();
                        for (int k = 0; k < tables[i].Columns.Count; k++)
                        {
                            tplcolumn.Add(tables[i].Columns[k].ColumnName);
                        }
                        tplcollist[i] = tplcolumn;

                        msg += "<br/>结果" + i + "：" + tables[i].Rows.Count + "条记录 ";
                    }

                    msg = "执行SQL：" + sqltext + " " + "共有" + tables.Length + "结果：" + msg;

                    ViewData.Add("data", tplcollist);
                    ViewData.Add("sqlname", sqlname);
                    ViewData.Add("msg", msg);

                    ViewResult = ToView(@"Views\SqlWebAdmin\retPage.tpl");
                }
                catch (Exception err)
                {
                    ViewResult = JavaScriptConvert.ToString("SQL执行错误： ") + err.Message;
                }
            }
            else
            {
                ViewResult = "SQL为空";
            }
        }

        public void QuerySQL()
        {
            string sqltext = ParamsData["sqltext"];
            string dbname = ParamsData["database"];

            if (sqltext.Trim() != "")
            {
                try
                {
                    SqlServer server = (SqlServer)sessionData["server"];
                    
                    server.Connect();
                    SqlDatabase database = server.Databases[dbname];
                    DataTable[] tables = null;
                    tables = database.Query(sqltext);
                    server.Disconnect();

                    string tpltext="";
                    string tablesdata="";
                    string msg="";

                    List<string>[] tplcollist = new List<string>[tables.Length];
                    List<string> tplcolumn;

                    for (int i = 0; i < tables.Length; i++)
                    {
                        tplcolumn = new List<string>();
                        for (int k = 0; k < tables[i].Columns.Count; k++)
                        {
                            tplcolumn.Add(tables[i].Columns[k].ColumnName);
                        }
                        tplcollist[i] = tplcolumn;

                        if (i == 0)
                            tablesdata += JavaScriptConvert.SerializeObject(tables[i]);
                        else
                            tablesdata += "," + JavaScriptConvert.SerializeObject(tables[i]);

                        //msg += "结果" + i + "：" + tables[i].Rows.Count + "条记录 ";
                    }

                    tablesdata = "[" + tablesdata + "]";

                    //TemplateHelper template = new TemplateHelper();
                    //template.Put("data", tplcollist);

                    //tpltext = template.BuildString("retPage.tpl");

                    //msg = "执行SQL：" + sqltext + " " + "共有" + tables.Length + "结果： " + msg;

                    string json = "{\"tpltext\":\"" + tpltext + "\",\"tables\":" + tablesdata + ",\"msg\":\"" + msg + "\"}";

                    JsonResult = RetSuccess(null, json);
                }
                catch (Exception err)
                {
                    JsonResult = RetError(JavaScriptConvert.ToString("SQL执行错误： ") + err.Message);
                }
            }
            else
            {
                JsonResult = RetError("SQL为空");
            }
        }

        public void ExportTableData()
        {
            string dbname = ParamsData["database"];
            string tablename = ParamsData["table"];
            SqlServer server = (SqlServer)sessionData["server"];

            server.Connect();
            SqlDatabase database = server.Databases[dbname];
            SqlTableCollection tables = database.Tables;
            SqlTable t = tables[tablename];

            StringBuilder scriptResult = new StringBuilder();
            scriptResult.Append(t.ScriptData(SqlScriptType.Comments));
            server.Disconnect();

            JsonResult = scriptResult.ToString();
        }

        public void ExportAll()
        {
            string dbname = ParamsData["database"];
            bool scriptDatabase = ParamsData["ScriptDatabase"]=="1"?true:false;
            bool scriptTableSchema = ParamsData["ScriptTableScheme"] == "1" ? true : false;
            bool scriptTableData = ParamsData["ScriptTableData"] == "1" ? true : false;
            bool scriptStoredProcedures = ParamsData["ScriptStoredProcedures"] == "1" ? true : false;
            bool scriptDrop = ParamsData["ScriptDrop"] == "1" ? true : false;
            bool scriptComments = ParamsData["ScriptComments"] == "1" ? true : false;

            SqlServer server = (SqlServer)sessionData["server"];

            server.Connect();
            SqlDatabase database = server.Databases[dbname];
            SqlTableCollection tables = database.Tables;
            SqlStoredProcedureCollection sprocs = database.StoredProcedures;

            StringBuilder scriptResult = new StringBuilder();
            scriptResult.Append(String.Format("/* Generated by Web Data on {0} */\r\n\r\n", DateTime.Now.ToString()));
            scriptResult.Append("/* Options selected: ");
            if (scriptDatabase) scriptResult.Append("database ");
            if (scriptDrop) scriptResult.Append("drop-commands ");
            if (scriptTableSchema) scriptResult.Append("table-schema ");
            if (scriptTableData) scriptResult.Append("table-data ");
            if (scriptStoredProcedures) scriptResult.Append("stored-procedures ");
            if (scriptComments) scriptResult.Append("comments ");
            scriptResult.Append(" */\r\n\r\n");

            // Script flow:
            // DROP and CREATE database
            // use [database]
            // GO
            // DROP sprocs
            // DROP tables
            // CREATE tables without constraints
            // Add table data
            // Add table constraints
            // CREATE sprocs

            // Drop and create database
            if (scriptDatabase)
                scriptResult.Append(database.Script(
                    SqlScriptType.Create |
                    (scriptDrop ? SqlScriptType.Drop : 0) |
                    (scriptComments ? SqlScriptType.Comments : 0)));

            // Use database
            scriptResult.Append(String.Format("\r\nuse [{0}]\r\nGO\r\n\r\n", dbname));

            // Drop stored procedures
            if (scriptStoredProcedures && scriptDrop)
            {
                for (int i = 0; i < sprocs.Count; i++)
                {
                    if (sprocs[i].StoredProcedureType == SqlObjectType.User)
                    {
                        scriptResult.Append(sprocs[i].Script(SqlScriptType.Drop | (scriptComments ? SqlScriptType.Comments : 0)));
                    }
                }
            }

            // Drop tables (this includes schemas and data)
            if (scriptTableSchema && scriptDrop)
            {
                for (int i = 0; i < tables.Count; i++)
                {
                    if (tables[i].TableType == SqlObjectType.User)
                    {
                        scriptResult.Append(tables[i].ScriptSchema(SqlScriptType.Drop | (scriptComments ? SqlScriptType.Comments : 0)));
                    }
                }
            }

            // Create table schemas
            if (scriptTableSchema)
            {
                // First create tables with no constraints
                for (int i = 0; i < tables.Count; i++)
                {
                    if (tables[i].TableType == SqlObjectType.User)
                    {
                        scriptResult.Append(tables[i].ScriptSchema(SqlScriptType.Create | (scriptComments ? SqlScriptType.Comments : 0)));
                    }
                }
            }

            // Create table data
            if (scriptTableData)
            {
                for (int i = 0; i < tables.Count; i++)
                {
                    if (tables[i].TableType == SqlObjectType.User)
                    {
                        scriptResult.Append(tables[i].ScriptData(scriptComments ? SqlScriptType.Comments : 0));
                    }
                }
            }

            if (scriptTableSchema)
            {
                // Add defaults, primary key, and checks
                for (int i = 0; i < tables.Count; i++)
                {
                    if (tables[i].TableType == SqlObjectType.User)
                    {
                        scriptResult.Append(tables[i].ScriptSchema(SqlScriptType.Defaults | SqlScriptType.PrimaryKey | SqlScriptType.Checks | (scriptComments ? SqlScriptType.Comments : 0)));
                    }
                }

                // Add foreign keys
                for (int i = 0; i < tables.Count; i++)
                {
                    if (tables[i].TableType == SqlObjectType.User)
                    {
                        scriptResult.Append(tables[i].ScriptSchema(SqlScriptType.ForeignKeys | (scriptComments ? SqlScriptType.Comments : 0)));
                    }
                }

                // Add unique keys
                for (int i = 0; i < tables.Count; i++)
                {
                    if (tables[i].TableType == SqlObjectType.User)
                    {
                        scriptResult.Append(tables[i].ScriptSchema(SqlScriptType.UniqueKeys | (scriptComments ? SqlScriptType.Comments : 0)));
                    }
                }

                // Add indexes
                for (int i = 0; i < tables.Count; i++)
                {
                    if (tables[i].TableType == SqlObjectType.User)
                    {
                        scriptResult.Append(tables[i].ScriptSchema(SqlScriptType.Indexes | (scriptComments ? SqlScriptType.Comments : 0)));
                    }
                }
            }

            // Create stored procedures
            if (scriptStoredProcedures)
            {
                for (int i = 0; i < sprocs.Count; i++)
                {
                    if (sprocs[i].StoredProcedureType == SqlObjectType.User)
                    {
                        scriptResult.Append(sprocs[i].Script(SqlScriptType.Create | (scriptComments ? SqlScriptType.Comments : 0)));
                    }
                }
            }

            server.Disconnect();

            JsonResult = scriptResult.ToString();
        }
    }
}
