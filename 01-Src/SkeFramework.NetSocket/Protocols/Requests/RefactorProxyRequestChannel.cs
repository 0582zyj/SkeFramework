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
        protected NetworkData networkData;
        /// <summary>
        /// 发送间隔时间【默认】
        /// </summary>
        protected int DefaultTaskInterval;
        /// <summary>
        /// 重发次数【默认】
        /// </summary>
        protected int DefaultResendCount;

        public RefactorProxyRequestChannel(ReactorBase reactor, string controlCode)
            : this(reactor, reactor.LocalEndpoint, controlCode)
        {
        }

        public RefactorProxyRequestChannel(ReactorBase reactor, INode endPoint, string controlCode)
            : base(reactor,  endPoint, controlCode)
        {
            this.DefaultTaskInterval = NetworkConstants.DefaultTaskInterval;
            this.DefaultResendCount = NetworkConstants.DefaultTaskCount;
        }

        public override void BeginReceiveInternal()
        {
            this.Receiving = true;
            this.Dead = false;
        }

        public override void StopReceiveInternal()
        {
            if (this.connectionStatus == ResultStatusCode.TIME_OUT)
            {
                NetworkData networkData = NetworkData.Create(this.RemoteHost,new byte[0],0);
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
            if (config.HasOption(OptionKeyEnums.TaskInterval.ToString()))
            {
                this.DefaultTaskInterval = config.GetOption<int>(OptionKeyEnums.TaskInterval.ToString());
                this.Sender.Interval = this.DefaultTaskInterval;

            }
            if (config.HasOption(OptionKeyEnums.TaskResend.ToString()))
            {
                this.DefaultResendCount = config.GetOption<int>(OptionKeyEnums.TaskResend.ToString());
                this.Sender.TotalSendTimes = this.DefaultResendCount;
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
            CaseSendFrame(networkData, this.DefaultTaskInterval, this.DefaultResendCount );
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
