using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Protocols.Constants;

namespace ULCloudLockTool.BLL.SHProtocol.DataHandle
{
    /// <summary>
    /// 点量状态变化监听
    /// </summary>
    public interface IDataPointListener
    {
        void OnReceivedDataPoint(NetworkData datas, string controlerId);
    }
}
