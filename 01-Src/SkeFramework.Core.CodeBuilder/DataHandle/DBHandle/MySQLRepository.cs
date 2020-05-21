using Newtonsoft.Json;
using SkeFramework.Core.CodeBuilder.DataEntities;
using SkeFramework.Core.CodeBuilder.DataFactory;
using SkeFramework.Core.CodeBuilder.DataUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.CodeBuilder.DataHandle.DBHandle
{

    public class MySQLRepository : IRepository
    {


        public List<TableEntities> GetTableList(string database)
        {
            string sSQL = "select * from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA=@Database;";
            var ParaList = new List<DbParameter>();
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@database", database));
            DataTable dt = SqlHelper.GetDataTable(CommandType.Text, sSQL, ParaList.ToArray());
            if (dt != null && dt.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<TableEntities>>(JsonConvert.SerializeObject(dt));
            }
            return new List<TableEntities>();
        }

        public List<ColumnsEntities> GetColumnsList(string tableName, string database)
        {
            string sSQL = "select * from  INFORMATION_SCHEMA.COLUMNS Where TABLE_NAME=@tableName AND TABLE_SCHEMA=@Database;";
            var ParaList = new List<DbParameter>();
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@Database", database));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@tableName", tableName));
            DataTable dt = SqlHelper.GetDataTable(CommandType.Text, sSQL, ParaList.ToArray());
            if (dt != null && dt.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<ColumnsEntities>>(JsonConvert.SerializeObject(dt));
            }
            return new List<ColumnsEntities>();
        }

    }
}
