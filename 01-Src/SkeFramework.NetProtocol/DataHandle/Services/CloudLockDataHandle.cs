using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULCloudLockTool.BLL.SHProtocol.Constants;
using ULCloudLockTool.BLL.SHProtocol.DataHandle.ATHandleBase;

namespace ULCloudLockTool.BLL.SHProtocol.DataHandle.Services
{
   public class CloudLockDataHandle: ATDataHandle
    {

        public override void RequestATSetRemoteAddress(string mac)
        {
            string strAddress = "AT+RADDREXT=" + mac + ",1\r\n";
            base.RequestReactorFunction(ProtocolConst.ATSetRemoteAddress, strAddress);
        }
    }
}
