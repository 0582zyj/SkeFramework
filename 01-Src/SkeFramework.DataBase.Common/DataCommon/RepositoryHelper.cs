using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.DataBase.Common.DataFactory;
using SkeFramework.DataBase.Common.Interfaces;

namespace SkeFramework.DataBase.Common.DataCommon
{
    /// <summary>
    /// 数据库帮助方法
    /// </summary>
    public class RepositoryHelper
    {
        private static  ISQLLog Log;
        public static ISQLLog SQLLog
        {
            get
            {
                if (Log == null)
                {
                    Log = new ConsoleLog();
                }
                return Log;
            }
            set
            {
                Log = value;
            }
        }


        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] commParemeters)
        {
            using (IDbConnection conn = DbFactory.Instance().CreateDbConnection())
            {
                conn.Open();
                using (IDbCommand command = DbFactory.Instance().CreateDbCommand())
                {
                    PrepareCommand(command, conn, null, cmdType, cmdText, commParemeters);
                    RepositoryHelper.doGetSQL(cmdText, commParemeters);
                    return command.ExecuteNonQuery();
                }
            }
        }
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, IDbTransaction trans, params DbParameter[] commParemeters)
        {
            using (IDbConnection conn = DbFactory.Instance().CreateDbConnection())
            {
                conn.Open();
                using (IDbCommand command = DbFactory.Instance().CreateDbCommand())
                {
                    PrepareCommand(command, conn, trans, cmdType, cmdText, commParemeters);
                    RepositoryHelper.doGetSQL(cmdText, commParemeters);
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
                    RepositoryHelper.doGetSQL(cmdText, commParemeters);
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
                    RepositoryHelper.doGetSQL(cmdText, commParemeters);
                    return ds.Tables[0];//返回DataSet
                }
            }
        }
        /// <summary>
        /// 执行存储过程(支持Output参数)
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="storedProcedureName">存储过程名</param>
        /// <param name="output">返回参数hash表</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>存储过程操作的返回值</returns>
        /// <remarks>check by kenny on 2013/7/30</remarks>
        public static int ExecuteStoredProcedure(string storedProcedureName, ref Hashtable output, params DbParameter[] commandParameters)
        {
            using (IDbConnection conn = DbFactory.Instance().CreateDbConnection())
            {
                using (IDbCommand command = DbFactory.Instance().CreateDbCommand())
                {
                    PrepareCommand(command, conn, null, CommandType.StoredProcedure, storedProcedureName, commandParameters);
                    //--------------存储过程操作的返回值----------------------
                    //DbParameter parameter = DbFactory.Instance().CreateDataParameter("@Value", SqlDbType.Int);
                    //parameter.ParameterName = "@Value";
                    //parameter.DbType = DbType.Int64;
                    //parameter.Direction = ParameterDirection.ReturnValue;
                    //command.Parameters.Add(parameter);
                    //--------------------------------------------------------
                    command.ExecuteNonQuery();

                    //----------------------处理返回参数----------------------
                    foreach (DbParameter parm in command.Parameters)
                    {
                        if (parm.Direction == ParameterDirection.Output)
                        {
                            output.Add(parm.ParameterName, parm.Value);
                        }
                    }
                    //--------------------------------------------------------
                    command.Parameters.Clear();
                    return 1;
                }
            }
        }
        /// <summary>
        /// SQL参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
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

        /// <summary>
        /// 用于输出参数化查询语句的SQL语句
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string doGetSQL(string sSQL, DbParameter[] cmdParms)
        {
            string sSQLResult = sSQL;
            try
            {

                foreach (var propertyInfo in cmdParms)
                {
                    DbParameter obj = (DbParameter)propertyInfo;
                    sSQLResult = sSQLResult.Replace(obj.ParameterName, obj.Value.ToString());
                }
            }
            catch
            {
                sSQLResult = "Export SQL Error";
            }
            RepositoryHelper.SQLLog.OutputLog(sSQLResult);
            return sSQLResult;
        }
    }
}
