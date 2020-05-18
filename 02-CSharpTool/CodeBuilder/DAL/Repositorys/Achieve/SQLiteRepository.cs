using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBuilder.DAL.Repository;
using CodeBuilder.DataFactory;
using CodeBuilder.Repositorys;
using Newtonsoft.Json;
using SkeFramework.Core.CodeBuilder;
using SkeFramework.Core.CodeBuilder.DataEntities;

namespace CodeBuilder.DAL.Repositorys.Achieve
{
    public class SQLiteRepository:IRepository
    {
        public List<TableEntities> GetTableList(string database)
        {
            string sSQL = "select name AS TABLE_NAME from sqlite_master where type='table' order by name;";
            DataTable dt = SqlHelper.GetDataTable(CommandType.Text, sSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<TableEntities>>(JsonConvert.SerializeObject(dt));
            }
            return new List<TableEntities>();
        }

        public List<ColumnsEntities> GetColumnsList(string tableName, string database)
        {
            string sSQL =string.Format( "PRAGMA  table_info('{0}')",tableName);
            DataTable dt = SqlHelper.GetDataTable(CommandType.Text, sSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<ColumnsEntities> result = new List<ColumnsEntities>();
                foreach(DataRow row in dt.Rows)
                {
                    string type = row["type"].ToString();
                    int startIndex = type.IndexOf("(");
                    var datatype = type;
                    string length = null;
                    if (startIndex > -1)
                    {
                        datatype = type.Substring(0, startIndex);
                        length =type.Substring(startIndex+1, type.IndexOf(")")-startIndex-1);
                    }
                
                    result.Add(new ColumnsEntities()
                    {
                        ORDINAL_POSITION =Convert.ToInt32( row["cid"]),
                        COLUMN_NAME=row["name"].ToString(),
                        DATA_TYPE = datatype.Trim(),
                        CHARACTER_MAXIMUM_LENGTH=length,
                        COLUMN_COMMENT = row["name"].ToString(),
                    });
                }
                return result;
            }
            return new List<ColumnsEntities>();
        } 
    }
}
