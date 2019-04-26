using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetConnectionPool.ConnService
{
    /// <summary>
    /// 连接类型
    /// </summary>
    public enum ConnTypeEnum
    {
        /// <summary>
        /// ODBC 数据源
        /// </summary>
        Odbc,
        /// <summary>
        /// OLE DB 数据源
        /// </summary>
        OleDb,
        /// <summary>
        /// SqlServer 数据库连接
        /// </summary>
        SqlClient,
        /// <summary>
        /// MySql 数据库连接
        /// </summary>
        MySqlClient,
        /// <summary>
        /// 默认（无分配）
        /// </summary>
        None
    }

}
