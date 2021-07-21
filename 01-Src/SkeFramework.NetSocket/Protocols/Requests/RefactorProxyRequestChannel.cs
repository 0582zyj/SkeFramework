using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Configs.Enums;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Connections.Tasks;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Requests
{
    /// <summary>
    /// 请求代理类
    /// </summary>
   public class RefactorProxyRequestChannel : RefactorRequestChannel
    {
        /// <summary>
        /// 通信数据
        /// </summary>
        private NetworkData networkData;

        public RefactorProxyRequestChannel(ReactorBase reactor, string controlCode)
            : this(reactor, reactor.LocalEndpoint, controlCode)
        {
        }

        public RefactorProxyRequestChannel(ReactorBase reactor, INode endPoint, string controlCode)
            : base(reactor,  endPoint, controlCode)
        {
        }

        public override void BeginReceiveInternal()
        {
            this.Receiving = true;
        }

        public override void StopReceiveInternal()
        {
            if (this.connectionStatus == ResultStatusCode.TIME_OUT)
            {
                NetworkData networkData = NetworkData.Empty;
                networkData.ResultData = new TaskResult(false, ResultStatusCode.TIME_OUT, ResultStatusCode.TIME_OUT.GetDesc(), this.networkData);
                //超时回调通知   
                base.OnReceive(networkData);
            }
        }

        public override void Configure(IConnectionConfig config)
        {
            this.connectionConfig = config;
            if (config.HasOption(OptionKeyEnums.ProtocolTimeOut.ToString()))
            {
                int ProtocolTimeOut = (int)config.GetOption(OptionKeyEnums.ProtocolTimeOut.ToString());
                if (ProtocolTimeOut > -1 && ProtocolTimeOut < NetworkConstants.BackoffIntervals.Length)
                {
                    this.Timeout = NetworkConstants.BackoffIntervals[ProtocolTimeOut];
                }
            }
            
        }
        /// <summary>
        /// 执行任务【默认重发3次3S超时】
        /// </summary>
        /// <param name="connectionTask"></param>
        public override void ExecuteTaskSync(ConnectionTask connectionTask)
        {
            networkData = CreateNetworkData(connectionTask);
            if (networkData == null)
                return;
            CaseSendFrame(networkData, NetworkConstants.DefaultTaskInterval, NetworkConstants.DefaultTaskCount);
        }
        /// <summary>
        /// 生成发送数据
        /// </summary>
        /// <param name="connectionTask"></param>
        /// <returns></returns>
        protected virtual NetworkData CreateNetworkData(ConnectionTask connectionTask)
        {
            return null;
        }
    }
}
