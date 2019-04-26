using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.DataBase.Interfaces
{
    /// <summary>
    /// 数据管理工厂接口
    /// </summary>
    public interface IDataHandleFactory
    {
        /// <summary>
        /// 对象生成器
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        THandle GetDataHandleCommon<THandle, TData>()
            where THandle : class, IDataTableHandle<TData>
            where TData : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="tableName"></param>
        /// <returns></returns>
        IRepository<TData> GetConfigDataSerialer<TData>(String tableName)
            where TData : class, new();
    }
}
