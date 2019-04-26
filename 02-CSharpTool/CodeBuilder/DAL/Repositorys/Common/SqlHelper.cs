using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBuilder.DataFactory;

namespace CodeBuilder.Repositorys
{
    public class SqlHelper
    {
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] commParemeters)
        {
            using (IDbConnection conn = DbFactory.Instance().CreateDbConnection())
            {
                conn.Open();
                using (IDbCommand command = DbFactory.Instance().CreateDbCommand())
                {
                    PrepareCommand(command, conn, null, cmdType, cmdText, commParemeters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行查询，并返回结果集第一行第一列
        /// </summary>
        /// <param name="cmdType">执行类型</param>
        /// <param name="cmdText">存储过程名字或SQL语句</param>
        /// <param name="commParemeters">参数</param>
        /// <returns>结果集第一行第一列</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] commParemeters)
        {
            using (IDbConnection conn = DbFactory.Instance().CreateDbConnection())
            {
                conn.Open();
                using (IDbCommand command = DbFactory.Instance().CreateDbCommand())
                {
                    PrepareCommand(command, conn, null, cmdType, cmdText, commParemeters);
                    object val = command.ExecuteScalar();
                    command.Parameters.Clear();
                    return val;
                }

            }
        }

        /// <summary>
        /// 执行查询的SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public static DataTable GetDataTable(CommandType cmdType, string cmdText, params DbParameter[] commParemeters)
        {
            DataSet ds = new DataSet();//定义一个容器ds
            using (IDbConnection conn = DbFactory.Instance().CreateDbConnection())
            {
                conn.Open();
                using (IDbCommand command = DbFactory.Instance().CreateDbCommand())
                {
                    PrepareCommand(command, conn, null, cmdType, cmdText, commParemeters);
                    IDataAdapter sda = DbFactory.Instance().CreateDataAdapter(command);//实例化SqlDataAdapter对象
                    sda.Fill(ds);//填充数据
                    return ds.Tables[0];//返回DataSet
                }
            }
        }

        private static void PrepareCommand(IDbCommand cmd, IDbConnection conn, IDbTransaction trans, CommandType cmdType,
             string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
                foreach (var parm in cmdParms)
                    cmd.Parameters.Add(parm);
        }
    }
}
