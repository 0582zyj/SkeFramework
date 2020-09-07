using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SkeFramework.DataBase.Common.DataFactory
{
    /// <summary>
    /// 数据库访问对象工厂
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        private string connectionString;
        /// <summary>
        /// 数据库类型
        /// </summary>
        private string ProviderName;
        public DbFactory()
        {
        }
        /// <summary>
        /// 单例模式
        /// </summary>
        private static DbFactory _simpleInstance = null;
        public static DbFactory Instance()
        {
            if (_simpleInstance == null || _simpleInstance.connectionString==null)
            {
                _simpleInstance = new DbFactory();
                var collection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"];
                _simpleInstance.connectionString = collection.ConnectionString;
                _simpleInstance.ProviderName = collection.ProviderName.ToLower();
            }
            return _simpleInstance;
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateDbConnection()
        {
            IDbConnection connection = null;
            switch (ProviderName)
            {
                case ProviderType.MySQL:
                    connection = new MySqlConnection(connectionString);
                    break;
                case ProviderType.SQLite:
                    connection = new SQLiteConnection(connectionString);
                    break;
            }
            return connection;
        }
        /// <summary>
        /// 创建Command对象
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateDbCommand()
        {
            IDbCommand command = null;
            switch (ProviderName)
            {
                case ProviderType.MySQL:
                    command = new MySqlCommand();
                    break;
                case ProviderType.SQLite:
                    command = new SQLiteCommand(connectionString);
                    break;
            }
            return command;
        }
        /// <summary>
        /// 创建DataAdapter对象
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        public IDataAdapter CreateDataAdapter(IDbCommand comm)
        {
            IDataAdapter adapter = null;
            switch (ProviderName)
            {
                case ProviderType.MySQL:
                    adapter = new MySqlDataAdapter(comm as MySqlCommand);
                    break;
                case ProviderType.SQLite:
                    adapter = new SQLiteDataAdapter(comm as SQLiteCommand);
                    break;
            }
            return adapter;
        }
        /// <summary>
        /// 创建SQL参数对象
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        public DbParameter CreateDataParameter(string FieldName, object FieldValue)
        {
            DbParameter adapter = null;
            switch (ProviderName)
            {
                case ProviderType.MySQL:
                    adapter = new MySqlParameter(FieldName, FieldValue);
                    break;
                case ProviderType.SQLite:
                    adapter = new SQLiteParameter(FieldName, FieldValue);
                    break;
            }
            return adapter;
        }
        /// <summary>
        /// 获取分页SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public string PageSql(string sql, long pageIndex, int pageSize)
        {
            if (pageSize < 0)
            {
                return sql;
            }
            else if (ProviderName.Equals(ProviderType.MySQL))
            {
                return $"{sql} limit {pageIndex},{pageSize}";
            }
            else if (ProviderName.Equals(ProviderType.SQLite))
            {
                return $"{sql} limit {pageSize} offset {(pageIndex - 1) * pageSize}";
            }
            throw new Exception("配置的数据库类型暂时不支持自动拼装分页sql");
        }
        /// <summary>
        /// 检查数据库提供者类型是否支持
        /// </summary>
        /// <returns></returns>
        public bool CheckProviderTypeIsSupport()
        {
            return this.ProviderName.ToLower().Equals(ProviderType.MySQL)
                || this.ProviderName.ToLower().Equals(ProviderType.SQLite);
        }
    }
    /// <summary>
    /// 连接器提供类型
    /// </summary>
    public class ProviderType
    {
        public const string MySQL = "mysql.data.mysqlclient";
        public const string SQLite = "system.data.sqlite.ef6";
    }
}
