using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SkeFramework.Core.SqlExpression;
using SkeFramework.DataBase.Common.DataCommon;
using SkeFramework.DataBase.Common.DataFactory;
using SkeFramework.DataBase.Interfaces;

namespace SkeFramework.DataBase.DataAccess.DataHandle.Achieve
{
    /// <summary>
    /// 数据库具体实现
    /// </summary>
    /// <typeparam name="TRealType"></typeparam>
    /// <typeparam name="TInterface"></typeparam>
    public class DBRepository<TRealType, TInterface> : IRepository<TInterface>
        where TRealType : class, TInterface, new()
        where TInterface : class, new()
    {
        /// <summary>
        /// 数据库链接
        /// </summary>
        protected IDbConnection conn { get; private set; }
        public DBRepository()
        {

        }
        /// <summary>
        /// 插入方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(TInterface entity, IDbTransaction trans = null)
        {
            if (!(entity is TRealType)) return 0;
            var type = typeof(TInterface);
            var tablename = PropertiesHelper.Instance().GetTableName(type);
            if (string.IsNullOrEmpty(tablename)) return -1;
            var sbColumnList = new List<string>();
            var sbParaList = new List<string>();
            var ParaList = new List<DbParameter>();
            var AllProperties = PropertiesHelper.Instance().GetAllProperties(type);
            var KeyProperties = PropertiesHelper.Instance().GetKeyProperties(type);
            var AllPropertiesExceptKeyAndIgnore = AllProperties.Except(KeyProperties);
            //动态生成sql语句
            foreach (var item in AllPropertiesExceptKeyAndIgnore)
            {
                sbColumnList.Add(item.Name);
                sbParaList.Add("@" + item.Name);
                ParaList.Add(DbFactory.Instance().CreateDataParameter(string.Format("@{0}", item.Name), item.GetValue(entity)));
            }
            var sSQL = string.Format("insert into {0}({1}) values({2});", tablename,
                 string.Join(",", sbColumnList.ToArray()),
                 string.Join(",", sbParaList.ToArray()));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL,trans, ParaList.ToArray());
        }
        /// <summary>
        /// 更新方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TInterface entity)
        {
            if (!(entity is TRealType)) return 0;
            var type = typeof(TInterface);
            var tablename = PropertiesHelper.Instance().GetTableName(type);
            if (string.IsNullOrEmpty(tablename)) return -1;
            var sbColumnList = new List<string>();
            var sbWhereList = new List<string>();
            var ParaList = new List<DbParameter>();
            var AllProperties = PropertiesHelper.Instance().GetAllProperties(type);
            var KeyProperties = PropertiesHelper.Instance().GetKeyProperties(type);
            var ignoreProperties = PropertiesHelper.Instance().GetIgnoreProperties(type);
            var AllPropertiesExceptKeyAndIgnore = AllProperties.Except(KeyProperties).Except(ignoreProperties);
            //动态生成sql语句
            foreach (var item in AllPropertiesExceptKeyAndIgnore)
            {
                object value = item.GetValue(entity, null);
                sbColumnList.Add(string.Format("{0}=@{0}", item.Name));
                ParaList.Add(DbFactory.Instance().CreateDataParameter(string.Format("@{0}", item.Name), item.GetValue(entity, null)));
                //if (value != null)
                //{
                // }
            }
            //where条件
            foreach (var item in KeyProperties)
            {
                sbWhereList.Add(string.Format("{0}=@{0}", item.Name));
                ParaList.Add(DbFactory.Instance().CreateDataParameter(string.Format("@{0}", item.Name), item.GetValue(entity)));
            }
            var sSQL = string.Format("Update {0} Set {1} WHERE {2};", tablename,
            string.Join(",", sbColumnList.ToArray()), string.Join(",", sbWhereList.ToArray()));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, ParaList.ToArray());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            var type = typeof(TInterface);
            var tablename = PropertiesHelper.Instance().GetTableName(type);
            if (string.IsNullOrEmpty(tablename)) return -1;
            var sbColumnList = new List<string>();
            var WhereList = new List<string>();
            var ParaList = new List<DbParameter>();
            var KeyProperties = PropertiesHelper.Instance().GetKeyProperties(type);
            foreach (var item in KeyProperties)
            {
                WhereList.Add(string.Format("{0}=@{0}", item.Name));
                ParaList.Add(DbFactory.Instance().CreateDataParameter(string.Format("@{0}", item.Name), id));
            }
            var sSQL = string.Format("Delete from {0} WHERE {1};", tablename, string.Join("AND", WhereList.ToArray()));
            Console.WriteLine(sSQL);
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, ParaList.ToArray());
        }
        /// <summary>
        /// 根据条件获取记录信息
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public TInterface Get(Expression<Func<TInterface, bool>> func)
        {
            return this.GetList(func).FirstOrDefault();
        }
        /// <summary>
        /// 根据条件获取记录
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<TInterface> GetList(Expression<Func<TInterface, bool>> where = null)
        {
            var type = typeof(TInterface);
            var tablename = PropertiesHelper.Instance().GetTableName(type);
            if (string.IsNullOrEmpty(tablename)) return null;
            var sbColumnList = new List<string>();
            var AllProperties = PropertiesHelper.Instance().GetAllProperties(type);
            //动态生成sql语句
            foreach (var item in AllProperties)
            {
                sbColumnList.Add(item.Name);
            }
            var sSQL = string.Format("SELECT {0} FROM {1} ", string.Join(",", sbColumnList.ToArray()), tablename);
            if (where != null)
            {
                string whereSQL = ExpressionHelper.Instance().GetSql(where);
                if (whereSQL.Length > 0)
                {
                    sSQL += " Where " + whereSQL;
                }
            }
            DataTable dt = RepositoryHelper.GetDataTable(CommandType.Text, sSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<TInterface>>(JsonConvert.SerializeObject(dt));
            }
            return new List<TInterface>();
        }
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public IEnumerable<TInterface> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<TInterface, bool>> where,
                Expression<Func<TInterface, TKey>> orderBy)
        {
            var type = typeof(TInterface);
            var tablename = PropertiesHelper.Instance().GetTableName(type);
            var keyname = PropertiesHelper.Instance().GetKeyName(type);
            if (string.IsNullOrEmpty(tablename)) return null;
            var sbColumnList = new List<string>();
            var AllProperties = PropertiesHelper.Instance().GetAllProperties(type);
            //动态生成sql语句
            foreach (var item in AllProperties)
            {
                sbColumnList.Add(item.Name);
            }
            var sSQL = string.Format("SELECT {0} FROM {1} ", string.Join(",", sbColumnList.ToArray()), tablename);
            if (where != null)
            {
                string whereSQL = ExpressionHelper.Instance().GetSql(where);
                if (whereSQL.Length > 0)
                {
                    sSQL += " Where " + whereSQL;
                }
            }
            if (orderBy != null)
            {
                string orderBySQL = ExpressionHelper.Instance().GetSql<TInterface, TKey>(orderBy);
                if (orderBySQL.Length > 0)
                {
                    sSQL += " Order by " + orderBySQL;
                }
            }
            else
            {
                sSQL += " Order by " + keyname;
            }
            DataTable dt = RepositoryHelper.GetDataTable(CommandType.Text, sSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<TInterface>>(JsonConvert.SerializeObject(dt))
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            return new List<TInterface>();
        }
        /// <summary>
        /// 根据条件获取记录个数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Count(Expression<Func<TInterface, bool>> where = null)
        {
            var type = typeof(TInterface);
            var tablename = PropertiesHelper.Instance().GetTableName(type);
            var keyName = PropertiesHelper.Instance().GetKeyName(type);
            if (string.IsNullOrEmpty(tablename)) return -1;
            var sbColumnList = new List<string>();
            var sSQL = string.Format("SELECT COUNT({0}) FROM {1} ", keyName, tablename);
            if (where != null)
            {
                string whereSQL = ExpressionHelper.Instance().GetSql(where);
                if (whereSQL.Length > 0)
                {
                    sSQL += " Where " + whereSQL;
                }
            }
            object obj = RepositoryHelper.ExecuteScalar(CommandType.Text, sSQL);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }
    }
}
