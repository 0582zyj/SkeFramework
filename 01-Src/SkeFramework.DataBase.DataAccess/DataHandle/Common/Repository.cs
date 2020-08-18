using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.DataBase.Interfaces;

namespace SkeFramework.DataBase.DataAccess.DataHandle.Common
{
    /// <summary>
    /// 仓库实现【通过抽象实现接口【_mSerialProxy】完成】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class,new()
    {
        /// <summary>
        /// 通用实现接口
        /// </summary>
        private readonly IRepository<TEntity> _mSerialProxy;

        public Repository(IRepository<TEntity> serialer)
        {
            this._mSerialProxy = serialer;
        }

        /// <summary>
        /// 获取通用接口列表
        /// </summary>
        /// <returns></returns>
        protected internal virtual IRepository<TEntity> GetSerialProxy()
        {
            return _mSerialProxy;
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(TEntity entity, IDbTransaction trans = null)
        {
            if (!IsCheck(entity))
            {
                return -1;
            }
            return _mSerialProxy.Insert(entity, trans);
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            if (!IsCheck(entity))
            {
                return -1;
            }
            return _mSerialProxy.Update(entity);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            if (null == _mSerialProxy)
            {
                return -1;
            }
            return _mSerialProxy.Delete(id);
        }
        /// <summary>
        /// 检查是否参数通过或者仓库具体实现是否完成
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsCheck(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            if (null == _mSerialProxy)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 根据条件查询信息
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> func)
        {
            return _mSerialProxy.Get(func);
        }
        /// <summary>
        /// 根据条件和排序查询列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(System.Linq.Expressions.Expression<Func<TEntity, bool>> where = null)
        {
            return _mSerialProxy.GetList(where);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> whereLambda,
                  Expression<Func<TEntity, TKey>> orderBy,bool isAsc=true)
        {
            return _mSerialProxy.GetPagedList<TKey>(pageIndex, pageSize, whereLambda, orderBy, isAsc);
        }
        /// <summary>
        /// 根据条件查询行数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Count(Expression<Func<TEntity, bool>> where = null)
        {
            return _mSerialProxy.Count(where);
        }
    }
}
