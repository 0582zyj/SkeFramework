using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SkeFramework.DataBase.Common.DataCommon;
using SkeFramework.DataBase.Interfaces;

namespace SkeFramework.DataBase.DataAccess.DataHandle.Common
{
    /// <summary>
    /// 表结构通用实现【通过抽象实现接口【_mSerialProxy】完成】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DataTableHandle<TEntity> : Repository<TEntity>, IDataTableHandle<TEntity> where TEntity : class,new()
    {
        /// <summary>
        /// 表名
        /// </summary>
        protected string _mTableName;
        /// <summary>
        /// 是否允许修改表名
        /// </summary>
        private readonly bool _mAllowModifyTableName;

        public DataTableHandle(IRepository<TEntity> serialer, string tableName, bool allowModifyTableName)
            : base(serialer)
        {
            _mTableName = tableName;
            _mAllowModifyTableName = allowModifyTableName;
        }


        /// <summary>
        /// 获取默认分页数据
        /// </summary>
        /// <param name="curPage">第几页</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="la">条件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetDefaultPagedList(int curPage = 1, int pageSize = 10, Expression<Func<TEntity, bool>> la = null, Expression<Func<TEntity, string>> orderBy = null)
        {
            return this.GetPagedList<string>(curPage, pageSize, la, orderBy);
        }
        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public TEntity GetModelByKey(string Key)
        {
            var type = typeof(TEntity);
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
            sSQL += " Where " + keyname + "=" + Key;
            DataTable dt = RepositoryHelper.GetDataTable(CommandType.Text, sSQL);
            if (dt != null && dt.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<TEntity>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
            }
            return null;
        }
    }
}
