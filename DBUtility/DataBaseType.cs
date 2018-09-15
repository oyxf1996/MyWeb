using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    /// <summary>
    /// web.config文件，链接字符串 providerName
    /// </summary>
    public class DataBaseType
    {
        /// <summary>
        /// SqlServer数据库
        /// </summary>
        public const string SqlServer = "System.Data.SqlClient";
        
        /// <summary>
        /// Oracle数据库
        /// </summary>
        public const string Oracle = "System.Data.OracleClient";
        //public static string Oracle = "System.Data.Oracle.DataAccess.Client";

        /// <summary>
        /// MySql数据库
        /// </summary>
        public const string MySql = "MySql.Data.MySqlClient";

        /// <summary>
        /// SqlLite数据库
        /// </summary>
        public const string SqlLite = "System.Data.SqlLite";

        /// <summary>
        /// Access数据库
        /// </summary>
        public const string Access = "System.Data.OleDb";

        
    }
}
