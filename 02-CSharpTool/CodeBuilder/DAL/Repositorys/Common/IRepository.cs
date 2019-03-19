using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBuilder.Model.Entities;

namespace CodeBuilder.DAL.Repository
{
   public interface IRepository
    {
       /// <summary>
       /// 获取数据库表
       /// </summary>
       /// <param name="database">数据库名</param>
       /// <returns></returns>
       List<TableEntities> GetTableList(string database);
       /// <summary>
       /// 获取表的列
       /// </summary>
       /// <param name="database">数据库名</param>
       /// <param name="tableName">表名</param>
       /// <returns></returns>
       List<ColumnsEntities> GetColumnsList(string database,string tableName);
    }
}
