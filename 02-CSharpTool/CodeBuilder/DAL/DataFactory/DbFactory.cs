using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBuilder.DAL.Repository;
using CodeBuilder.DAL.Repositorys.Achieve;
using CodeBuilder.Repositorys.Achieve;
using MySql.Data.MySqlClient;

namespace CodeBuilder.DataFactory
{
    public class DbFactory
    {
        private static DbFactory _simpleInstance = null;
        private string connectionString;
        private string databaseType;
        private string database;
        static DbFactory()
        {


        }

        public void SetProperties(string providername, string connectionstring, string database)
        {
            this.connectionString = connectionstring;
            this.databaseType = providername;
            this.database = database;
        }

        public static DbFactory Instance()
        {
            if (_simpleInstance == null)
            {
                _simpleInstance = new DbFactory();
            }
            return _simpleInstance;
        }

        public IDbConnection CreateDbConnection()
        {
            IDbConnection connection = null;
            switch (databaseType)
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

        public IDbCommand CreateDbCommand()
        {
            IDbCommand command = null;
            switch (databaseType)
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

        public IDataAdapter CreateDataAdapter(IDbCommand comm)
        {
            IDataAdapter adapter = null;
            switch (databaseType)
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

        public DbParameter CreateDataParameter(string FieldName, object FieldValue)
        {
            DbParameter adapter = null;
            switch (databaseType)
            {
                case ProviderType.MySQL:
                    adapter = new MySqlParameter(FieldName, FieldValue);
                    break;
                case ProviderType.SQLite:
                    adapter = new  SQLiteParameter(FieldName, FieldValue);
                    break;
            }
            return adapter;
        }

        public IRepository CreateRepository()
        {
            IRepository instance = null;
            switch (databaseType)
            {
                case ProviderType.MySQL:
                    instance = new MySQLRepository();
                    break;
                case ProviderType.SQLite:
                    instance = new SQLiteRepository();
                    break;
            }
            return instance;
        }

        public string GetDatabase()
        {
            return this.database;
        }

        public string GetConnectionString()
        {
            return this.connectionString;
        }
    }

    public class  ProviderType{

        public const string MySQL = "mysql.data.mysqlclient";
        public const string SQLite = "system.data.sqlite.ef";
    }
}

