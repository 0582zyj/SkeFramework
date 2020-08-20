using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.DataBase.Interfaces
{
    /// <summary>
    /// 通用表结构接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataTableHandle<TEntity> : IRepository<TEntity> where TEntity : class,new()
    {
        /// <summary>
        /// 获取默认分页数据
        /// </summary>
        /// <param name="curPage">第几页</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="la">条件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetDefaultPagedList(int pageIndex = 1, int pageSize = 10, Expression<Func<TEntity, bool>> where = null,
                     Expression<Func<TEntity, string>> orderBy = null);
        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        TEntity GetModelByKey(string Key);
    }
}
