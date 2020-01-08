using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Protocols.Response;
using SkeFramework.NetSerialPort.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULCloudLockTool.BLL.SHProtocol.Constants;
using ULCloudLockTool.BLL.SHProtocol.DataFrame.Interfaces;
using ULCloudLockTool.BLL.SHProtocol.DataFrame.Services;

namespace ULCloudLockTool.BLL.SHProtocol.BusinessCase.Response
{
    /// <summary>
    /// 默认响应
    /// </summary>
    public class DefaultResponse : RefactorProxyResponseChannel
    {
        public DefaultResponse(ReactorBase reactor)
        : this(reactor, null)
        {
            Receive += ProtocolProxyAgent.Instance().DataPointListener_Receive;
        }

        public DefaultResponse(ReactorBase reactor, RefactorRequestChannel request)
            : base(reactor, request)
        {
        }

        public override void Configure(IConnectionConfig config)
        {
            base.Configure(config);
        }


        public override void OnReceive(NetworkData data)
        {
            
            if (data.Buffer[2] == ProtocolConst.ATPraseLockInfo)
            {
                IDataFrame dataFrame = new ATDataFrame();
                int tagIndex = 10;
                byte[] dataBuffer = data.Buffer;
                if (dataBuffer.Length >= 30)
                {
                    tagIndex = 12;
                }
                if (dataBuffer.Length > tagIndex && dataBuffer[tagIndex] == 0x3A)
                {
                    int bodyStartIndex = tagIndex + 1;
                    byte[] ReceiveBytes = new byte[dataBuffer.Length - bodyStartIndex];
                    dataBuffer.ToList().CopyTo(bodyStartIndex, ReceiveBytes, 0, dataBuffer.Length - bodyStartIndex);
                    data.RemoteHost.TaskTag = ProtocolConst.ATPraseLockInfo.ToString();
                    data.ResultData = dataFrame.ProcessDataFrame(ReceiveBytes);
                }
                else
                {
                    Console.WriteLine("丢弃未处理数据:" + this.Encoder.ByteEncode(data.Buffer));
                    return;
                }
            }
            else
            {
                data.RemoteHost.TaskTag = ProtocolConst.ATScanDevice.ToString();
            }
            base.OnReceive(data);

        }

    }
}
