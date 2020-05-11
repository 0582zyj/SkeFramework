using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkeFramework.NetProtocol.Constants;
using SkeFramework.NetProtocol.DataHandle.HandleBase;

namespace SkeFramework.NetProtocol.DataHandle.ATHandleBase
{
    public class ATDataHandle : DataHandleBase, IATDataHandle
    {
        /// <summary>
        /// 请求发送扫描指令
        /// </summary>
        /// <returns></returns>
        public void RequestATScan()
        {
        
        }
        public virtual void RequestATSetRemoteAddress(string mac)
        {
          
        }
    }
}
