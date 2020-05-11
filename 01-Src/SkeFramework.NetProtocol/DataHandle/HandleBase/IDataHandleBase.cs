using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetProtocol.DataHandle.HandleBase
{
    /// <summary>
    /// 基础协议请求接口
    /// </summary>
    public interface IDataHandleBase
    {
        /// <summary>
        /// 执行一个请求任务
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        void RequestReactorFunction(int functionType, object value);
        /// <summary>
        /// 执行一个请求任务【没有回调】
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        void RequestReactorFunction(int functionType, int timeout, object value);
        /// <summary>
        /// 执行一个请求任务
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        void RequestReactorFunction(int functionType, AsyncCallback asyncCallback, int timeout, object value);
    }
}
