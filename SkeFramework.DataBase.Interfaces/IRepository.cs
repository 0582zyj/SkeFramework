using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.DataBase.Interfaces
{
    /// <summary>
    /// 通用仓库接口【增删改查，分页，统计】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class,new()
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(TEntity entity, IDbTransaction trans = null);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(TEntity entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(int id);

        #region 查询
        /// <summary>
        /// 根据条件查询信息
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> func);
        /// <summary>
        /// 根据条件和排序查询列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where,
                     Expression<Func<TEntity, TKey>> orderBy);
        /// <summary>
        /// 根据条件查询行数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        long Count(Expression<Func<TEntity, bool>> where = null);
        #endregion
    }
}
