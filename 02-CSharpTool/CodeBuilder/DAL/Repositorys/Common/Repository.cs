using SkeFramework.Core.CodeBuilder;
using SkeFramework.Core.CodeBuilder.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuilder.DAL.Repository.Common
{
   public class Repository: IRepository
    {
        /// <summary>
        /// 通用实现接口
        /// </summary>
        private readonly IRepository _mSerialProxy;

        public Repository(IRepository serialer)
        {
            this._mSerialProxy=serialer;
        }

        public List<TableEntities> GetTableList(string database)
        {
            return this._mSerialProxy.GetTableList(database);
        }

        public List<ColumnsEntities> GetColumnsList(string database, string tableName)
        {
            return this._mSerialProxy.GetColumnsList(database, tableName);
        }
    }
}
