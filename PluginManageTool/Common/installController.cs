using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace PluginManageTool.Common
{
    public class installController
    {

        //执行SQL语句，用来测试指定数据库是否存在
        private static void ExcuteSQL(string commandText, string connectionString)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                connection.ConnectionString = connectionString;
                connection.Open();
                commandText = commandText.Replace("SET ANSI_NULLS ON", "").Replace("GO", "").Replace("SET QUOTED_IDENTIFIER ON", "");
                sqlCommand = new SqlCommand(commandText, connection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                sqlCommand.Connection.Close();
            }
        }

        private static string GetDBDefaultCollation(string connectionString, string dbName)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(string.Format("SELECT DATABASEPROPERTYEX('{0}', 'Collation')", dbName), connection);
            string collation = sqlCommand.ExecuteScalar().ToString().Trim();
            sqlCommand.Connection.Close();
            return collation;
        }

        //测试用户填写的数据库信息是否正确
        public static bool CheckDBConnection(string sqlIp, string sqlUsername, string sqlPassword, string dbName)
        {
            
            bool result = false;
            dbName = string.IsNullOrEmpty(dbName) ? "master" : dbName;
            SqlConnection connection = new SqlConnection();
            try
            {

                connection.ConnectionString = string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false",
                                                  sqlIp, sqlUsername, sqlPassword, dbName);
                connection.Open();
                result = true;
            }
            catch (SqlException e)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        //创建数据库
        public static bool CreateDatabase(string sqlIp, string sqlManager, string sqlManagerPassword, string sqlName)
        {
            bool result = false;

            string connectionString = string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false",
                                                  sqlIp, sqlManager, sqlManagerPassword, "master");
            string commandText = string.Format("CREATE DATABASE [{0}]", sqlName);
            try
            {
                ExcuteSQL(commandText, connectionString);//执行创建数据库的TSQL；
                result = true;
            }
            catch (SqlException e)
            {
                result = false;
            }
            return result;
        }

        //检查数据库排序规则
        public static bool CheckDBCollation(string sqlIp, string sqlUsername, string sqlPassword, string dbName)
        {
            bool result = false;
            string dbCollation = GetDBDefaultCollation(string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false",
                                                  sqlIp, sqlUsername, sqlPassword, dbName), dbName);
            if (dbCollation.IndexOf("Chinese_PRC") < 0)
                //result = "{result:false,message:\"数据库排序规则不是简体中文,请调整为简体中文后重新运行安装程序\"}";
                result = false;
            else
                result = true;
            return result;
        }

        //检查数据库与表是否已存在
        public static bool DBSourceExist(string sqlIp, string sqlUsername, string sqlPassword, string dbName)
        {
            bool result = false;
            try
            {
                ExcuteSQL("SELECT COUNT(1) FROM [BaseUser]", string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false",
                                                      sqlIp, sqlUsername, sqlPassword, dbName));
                result = true;
            }
            catch (SqlException e)
            {
                result = false;
            }

            //result = string.IsNullOrEmpty(result) ? "{result:true,message:\"数据库已存在\",code:0}" : result.Replace("'", "\'");
            return result;
        }

        
        //创建表和相关索引，约束
        public static bool CreateTable(string sqlIp, string sqlUsername, string sqlPassword, string dbName, string scriptSql)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader objReader = new StreamReader(scriptSql, Encoding.UTF8))
            {
                sb.Append(objReader.ReadToEnd());
                objReader.Close();
            }


            bool result = false;
            try
            {
                ExcuteSQL(sb.ToString(), string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false",
                                                      sqlIp, sqlUsername, sqlPassword, dbName));

                result = true;

            }
            catch (SqlException e)
            {
                result = false;
            }


            return result;
        }
    }
}
