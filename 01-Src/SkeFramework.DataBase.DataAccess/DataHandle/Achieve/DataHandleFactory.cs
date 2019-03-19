using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.DataBase.Interfaces;

namespace SkeFramework.DataBase.DataAccess.DataHandle.Achieve
{
    /// <summary>
    /// 抽象数据管理工厂
    /// </summary>
    public abstract class DataHandleFactory : IDataHandleFactory
    {
        /// <summary>
        /// 数据工厂实例延迟绑定，由应用层进行操作
        /// </summary>
        private static IDataHandleFactory _staticInstance = null;

        public static void SetDataHandleFactory(IDataHandleFactory factory)
        {
            _staticInstance = factory;
        }

        /// <summary>
        /// 数据访问统一入口
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="controlerManager"></param>
        /// <returns></returns>
        public static THandle GetDataHandle<THandle, TData>()
            where THandle : class, IDataTableHandle<TData>
            where TData : class, new()
        {
            if (null != _staticInstance)
            {
                return _staticInstance.GetDataHandleCommon<THandle, TData>();
            }
            return default(THandle);
        }

        public virtual THandle GetDataHandleCommon<THandle, TData>()
            where THandle : class, IDataTableHandle<TData>
            where TData : class, new()
        {
            return null;
        }

        protected static bool IsSubclassOf(Type parentType, Type childType)
        {
            return parentType == childType || parentType.IsSubclassOf(childType);
        }


        /// <summary>
        /// 数据访问接口由应用层实现
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="controlerManager"></param>
        /// <returns></returns>
        public virtual IRepository<TData> GetConfigDataSerialer<TData>(string tableName) where TData : class, new()
        {
            return null;
        }

    }
}