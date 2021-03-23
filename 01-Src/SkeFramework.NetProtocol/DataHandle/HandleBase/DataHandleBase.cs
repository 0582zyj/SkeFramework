using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetProtocol.Constants;

namespace SkeFramework.NetProtocol.DataHandle.HandleBase
{
    /// <summary>
    /// 基础协议请求接口
    /// </summary>
    public class DataHandleBase:IDataHandleBase
    {
        /// <summary>
        /// 执行一个任务
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="value"></param>
        public void RequestReactorFunction(int functionType,  object value)
        {
            RequestReactorFunction(functionType, null, NetworkConstants.WAIT_FOR_COMPLETE, value);
        }
        /// <summary>
        /// 执行一个任务
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        public void RequestReactorFunction(int functionType, int timeout, object value)
        {
            RequestReactorFunction(functionType, null, timeout, value);
        }
        /// <summary>
        /// 执行一个请求任务
        /// </summary>
        /// <param name="functionType"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="timeout"></param>
        /// <param name="value"></param>
        public virtual void RequestReactorFunction(int functionType, AsyncCallback asyncCallback, int timeout, object value)
        {
            ConnectionTask task = new ConnectionTask(functionType.ToString(), value);
            ((ReactorConnectionAdapter)ProtocolProxyAgent.Instance().reactorConnectionAdapter).ExecuteTaskAsync(task, asyncCallback, timeout);
        }
    }
}
