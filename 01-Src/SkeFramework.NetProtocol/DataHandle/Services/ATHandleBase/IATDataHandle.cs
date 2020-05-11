using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkeFramework.NetProtocol.DataHandle.HandleBase;

namespace SkeFramework.NetProtocol.DataHandle.ATHandleBase
{
    /// <summary>
    /// AT处理接口
    /// </summary>
   public interface IATDataHandle: IDataHandleBase
    {
        /// <summary>
        /// 请求发送扫描指令
        /// </summary>
        /// <returns></returns>
        void RequestATScan();
        /// <summary>
        /// 请求设置地址
        /// </summary>
        /// <param name="mac"></param>
        void RequestATSetRemoteAddress(string mac);
    }
}
