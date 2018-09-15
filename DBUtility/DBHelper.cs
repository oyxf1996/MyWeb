using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace DBUtility
{
    public sealed class DBHelper
    {
        #region 属性
        /// <summary>
        /// 连接字符串
        /// </summary>
        static string ConnectionString { get; set; }

        /// <summary>
        /// 数据库类型 读取web.config的providerName
        /// </summary>
        static string DBType { get; set; }

        /// <summary>
        /// 连接通道对象
        /// </summary>
        static IDbConnection Connection { get; set; }

        /// <summary>
        /// 连接命令对象
        /// </summary>
        static IDbCommand Command { get; set; }

        /// <summary>
        /// 事务对象
        /// </summary>
        static IDbTransaction Transaction { get; set; }

        /// <summary>
        /// DataAdapter
        /// </summary>
        static IDataAdapter DataAdapter { get; set; }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        static DBHelper()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            DBType = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ProviderName;
            InitDB();
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化方法
        /// </summary>
        static void InitDB()
        {
            if (DBType == DataBaseType.SqlServer)
            {
                Connection = new SqlConnection(ConnectionString);
                Command = new SqlCommand();
                DataAdapter = new SqlDataAdapter(Command as SqlCommand);
            }
            else if (DBType == DataBaseType.Oracle)
            {
                Connection = new OracleConnection(ConnectionString);
                Command = new OracleCommand();
                DataAdapter = new OracleDataAdapter(Command as OracleCommand);
            }
            else if (DBType == DataBaseType.MySql)
            {
                Connection = new MySqlConnection(ConnectionString);
                Command = new MySqlCommand();
                DataAdapter = new MySqlDataAdapter(Command as MySqlCommand);
            }
            else
            {
                Connection = new SqlConnection(ConnectionString);
                Command = new SqlCommand();
                DataAdapter = new SqlDataAdapter(Command as SqlCommand);
            }
        }


        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        static void Close()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }


        /// <summary>
        /// 调用公用方法
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="paras">参数化数组</param>
        static void ExecuteCommonFun(string cmdText, params IDataParameter[] paras)
        {
            CommonFun(Connection, Command, CommandType.Text, cmdText, paras);
        }


        /// <summary>
        /// 公用的设置设置数据库对象方法
        /// </summary>
        /// <param name="conn">连接通道对象</param>
        /// <param name="cmd">连接命令对象</param>
        /// <param name="cmdType">sql语句、存储过程名、表名</param>
        ///// <param name="cmdText">sql语句</param>
        /// <param name="paras">参数化数组</param>
        static void CommonFun(IDbConnection conn, IDbCommand cmd, CommandType cmdType, string cmdText,params IDataParameter[] paras)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = cmdText;
                if (paras != null && paras.Length > 0)
                {
                    cmd.Parameters.Add(paras);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        #endregion

        #region 公开方法

        /// <summary>
        /// DataSet 返回DataSet数据集
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="paras">参数化数组</param>
        /// <returns></returns>
        public static DataSet GetDataSetBySql(string cmdText, params IDataParameter[] paras)
        {
            DataSet ds = new DataSet();
            ExecuteCommonFun(cmdText, paras);
            DataAdapter.Fill(ds);
            Close();
            return ds;
        }

        /// <summary>
        /// DataTable 返回DataTable数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static DataTable GetDataTableBySql(string sql, params SqlParameter[] paras)
        {
            IDataReader idr = ExecuteReader(sql, paras);
            DataTable dt = new DataTable();
            dt.Load(idr);
            idr.Close();
            return dt;
        }


        /// <summary>
        /// ExecuteNonQuery 返回受影响的行数（非事务）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数化数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params IDataParameter[] paras)
        {
            ExecuteCommonFun(sql, paras);
            int i = Command.ExecuteNonQuery();
            Close();
            return i;
        }

        #region 事务方法

        /// <summary>
        /// ExecuteNonQuery 返回受影响的行数（事务，无参数化数组）
        /// </summary>
        /// <param name="sqlList">sql语句</param>
        /// <param name="paras">参数化数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery_Tran(List<string> sqlList)
        {
            Transaction = Connection.BeginTransaction();
            Command.Transaction = Transaction;
            int sum = 0;
            try
            {
                foreach (string sql in sqlList)
                {
                    ExecuteCommonFun(sql, null);
                    sum += Command.ExecuteNonQuery();
                }
                Transaction.Commit();
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return 0;
            }
            Close();
            return sum;
        }

        /// <summary>
        /// ExecuteNonQuery 返回受影响的行数（事务，含参数化数组）
        /// </summary>
        /// <param name="sqlParaList">Hashtable中key是sql语句，value是该语句的IDataParameter[]</param>
        /// <returns></returns>
        public static int ExecuteNonQuery_Tran(Hashtable sqlParaList)
        {
            Transaction = Connection.BeginTransaction();
            Command.Transaction = Transaction;
            int sum = 0;
            try
            {
                foreach (DictionaryEntry de in sqlParaList)
                {
                    string sql = de.Key.ToString();
                    IDataParameter[] paras = (IDataParameter[])de.Value;
                    ExecuteCommonFun(sql, paras);
                    sum += Command.ExecuteNonQuery();
                }
                Transaction.Commit();
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return 0;
            }
            finally
            {
                Close();
            }
            return sum;
        }


        #endregion

        /// <summary>
        /// ExecuteScalar 返回第一行第一列的object对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数化数组</param>
        /// <returns>object对象</returns>
        public static object ExecuteScalar(string sql, params IDataParameter[] paras)
        {
            ExecuteCommonFun(sql, paras);
            object obj = Command.ExecuteScalar();
            Close();
            return obj;
        }


        /// <summary>
        /// 调用该方法记得执行IDataReader.Close();
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数化数组</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string sql, params IDataParameter[] paras)
        {
            ExecuteCommonFun(sql, paras);
            return Command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion


        //        【注意】各个数据库的插入语句不一样，假设我们有4个字段，第一个字段fieldName1为自增字段。 
        //        对于SQLServer，不需要写自增字段。 
        //        语句是：INSERT INTO table VALUES(value2, value3, value4);

        //        对于MySQL，自增字段位置需要写null代替， 
        //        语句是：INSERT INTO table VALUES(NULL, value2, value3, value4);

        //        而对于ACCESS数据库，则必须写完整， 
        //        语句是：INSERT INTO table(fieldName2, fieldName3, fieldName4) VALUES(value2, value3, value4);

        //        综合上述，为了兼容各个数据库，插入sql语句最好写完整insert into 表名(列1,列2...) values(值1,值2...)
    }
}
