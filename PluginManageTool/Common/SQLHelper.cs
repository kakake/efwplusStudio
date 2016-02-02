//===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//===============================================================================

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace PluginManageTool.Common
{

    #region DataBaseOperator
    /// <summary>
    /// Sql数据库操作类
    /// </summary>
    public class DataBaseOperator  {
        private string connectionString;
        public DataBaseOperator( string _connectionstring ) {
            connectionString = _connectionstring;
        }

        #region ExecuteNonQuery

        public int ExecuteNonQuery( string cmdText ) {
            return SqlHelper.ExecuteNonQuery( connectionString, CommandType.Text, cmdText, null );
        }

        public int ExecuteNonQuery( CommandType cmdType, string cmdText ) {
            return SqlHelper.ExecuteNonQuery( connectionString, cmdType, cmdText, null );
        }

        public int ExecuteNonQuery( CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {
            return SqlHelper.ExecuteNonQuery( connectionString, cmdType, cmdText, commandParameters );
        }

        #endregion

        #region ExecuteReader

        public SqlDataReader ExecuteReader( string cmdText ) {
            return SqlHelper.ExecuteReader( connectionString, CommandType.Text, cmdText, null );
        }

        public SqlDataReader ExecuteReader( CommandType cmdType, string cmdText ) {
            return SqlHelper.ExecuteReader( connectionString, cmdType, cmdText, null );
        }

        public SqlDataReader ExecuteReader( CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {
            return SqlHelper.ExecuteReader( connectionString, cmdType, cmdText, commandParameters );
        }

        #endregion

        #region ExecuteDataSet

        public DataSet ExecuteDataSet( string cmdText ) {
            return SqlHelper.ExecuteDataSet( connectionString, CommandType.Text, cmdText, null );
        }

        public DataSet ExecuteDataSet( CommandType cmdType, string cmdText ) {
            return SqlHelper.ExecuteDataSet( connectionString, cmdType, cmdText, null );
        }

        public DataSet ExecuteDataSet( CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {
            return SqlHelper.ExecuteDataSet( connectionString, cmdType, cmdText, commandParameters );
        }

        #endregion

        #region ExecuteScalar

        public object ExecuteScalar( string cmdText ) {
            return SqlHelper.ExecuteScalar( connectionString, CommandType.Text, cmdText, null );
        }

        public object ExecuteScalar( CommandType cmdType, string cmdText ) {
            return SqlHelper.ExecuteScalar( connectionString, cmdType, cmdText, null );
        }

        public object ExecuteScalar( CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {
            return SqlHelper.ExecuteScalar( connectionString, cmdType, cmdText, commandParameters );
        }
        #endregion

        #region Cache
        public static void CacheParameters( string cacheKey, params SqlParameter[] commandParameters ) {
            SqlHelper.CacheParameters( cacheKey, commandParameters );
        }
        public static SqlParameter[] GetCachedParameters( string cacheKey ) {
            return SqlHelper.GetCachedParameters( cacheKey );
        }
        #endregion
    }
    #endregion

    #region SqlHelper
    /// <summary>
    /// The SqlHelper class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public abstract class SqlHelper {

        /// <summary>
        /// Database connection strings
        /// </summary>
        //public static readonly string ConnectionStringLocalTransaction = ConfigurationManager.ConnectionStrings["SQLConnString1"].ConnectionString;
        //public static readonly string ConnectionStringInventoryDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString2"].ConnectionString;
        //public static readonly string ConnectionStringOrderDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString3"].ConnectionString;
        //public static readonly string ConnectionStringProfile = ConfigurationManager.ConnectionStrings["SQLProfileConnString"].ConnectionString;		

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized( new Hashtable() );

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery( string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {

            SqlCommand cmd = new SqlCommand();

            using( SqlConnection conn = new SqlConnection( connectionString ) ) {
                PrepareCommand( cmd, conn, null, cmdType, cmdText, commandParameters );
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">an existing database connection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery( SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand( cmd, connection, null, cmdType, cmdText, commandParameters );
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery( SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand( cmd, trans.Connection, trans, cmdType, cmdText, commandParameters );
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader( string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection( connectionString );

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try {
                PrepareCommand( cmd, conn, null, cmdType, cmdText, commandParameters );
                SqlDataReader rdr = cmd.ExecuteReader( CommandBehavior.CloseConnection );
                cmd.Parameters.Clear();
                return rdr;
            } catch {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 生成DataSet
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet( string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection( connectionString );

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try {
                PrepareCommand( cmd, conn, null, cmdType, cmdText, commandParameters );
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill( ds );
                cmd.Parameters.Clear();
                return ds;
            } catch {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar( string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {
            SqlCommand cmd = new SqlCommand();

            using( SqlConnection connection = new SqlConnection( connectionString ) ) {
                PrepareCommand( cmd, connection, null, cmdType, cmdText, commandParameters );
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">an existing database connection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar( SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters ) {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand( cmd, connection, null, cmdType, cmdText, commandParameters );
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="commandParameters">an array of SqlParamters to be cached</param>
        public static void CacheParameters( string cacheKey, params SqlParameter[] commandParameters ) {
            parmCache[ cacheKey ] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters( string cacheKey ) {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[ cacheKey ];

            if( cachedParms == null )
                return null;

            SqlParameter[] clonedParms = new SqlParameter[ cachedParms.Length ];

            for( int i = 0, j = cachedParms.Length ; i < j ; i++ )
                clonedParms[ i ] = (SqlParameter)( (ICloneable)cachedParms[ i ] ).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private static void PrepareCommand( SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms ) {

            if( conn.State != ConnectionState.Open )
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if( trans != null )
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if( cmdParms != null ) {
                foreach( SqlParameter parm in cmdParms )
                    cmd.Parameters.Add( parm );
            }
        }
    }
    #endregion
}