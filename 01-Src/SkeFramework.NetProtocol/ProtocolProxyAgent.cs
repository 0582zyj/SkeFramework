using System;
using System.Collections.Generic;
using SkeFramework.NetSerialPort.Bootstrap;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Topology;
using SkeFramework.NetProtocol.DataHandle;
using SkeFramework.NetSerialPort.Protocols.Listenser.Interfaces;
using SkeFramework.NetSerialPort.Protocols.Listenser.ChannelListensers;

namespace SkeFramework.NetProtocol
{
    /// <summary>
    /// 协议代理商
    /// </summary>
    public class ProtocolProxyAgent
    {
        #region 单例模式
        /// <summary>
        /// 协议管理器
        /// </summary>
        private static ProtocolProxyAgent mSingleInstance;
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static ProtocolProxyAgent Instance()
        {
            if (null == mSingleInstance)
            {
                mSingleInstance = new ProtocolProxyAgent();
            }
            return mSingleInstance;
        }
        #endregion


        /// <summary>
        /// 通信基类
        /// </summary>
        private IReactor reactor;
        /// <summary>
        /// 协议适配类
        /// </summary>
        private IConnection connectionAdapter;
        /// <summary>
        /// 协议适配类
        /// </summary>
        public ReactorConnectionAdapter reactorConnectionAdapter
        {
            get
            {
                if (connectionAdapter is ReactorConnectionAdapter)
                {
                    return (ReactorConnectionAdapter)connectionAdapter;
                }
                return null;
            }
        }
        /// <summary>
        /// 协议监听容器
        /// </summary>
        private IChannelPromise channelListenser = new DefaultChannelPromise();

        #region 协议处理代理
        /// <summary>
        /// 端口是否打开
        /// </summary>
        /// <returns></returns>
        public bool IsReactorOpen
        {
            get
            {
                if (this.reactor == null)
                {
                    return false;
                }
                return this.reactor.IsActive;
            }
            private set { }
        }
        /// <summary>
        /// 启动一个通信
        /// </summary>
        /// <param name="nodeConfig"></param>
        public bool StartReactor(NodeConfig nodeConfig)
        {
            try
            {
                var bootstrapper = new ServerBootstrap()
               .WorkerThreads(2)
               //.SetConfig(new DefaultConnectionConfig().SetOption(OptionKeyEnums.ParseTimeOut.ToString(), 5))
               .Build();
                reactor = bootstrapper.NewReactor(NodeBuilder.BuildNode().Host(nodeConfig));
                reactor.ConnectionAdapter = new ProtocolUT("SerialProtocol", (ReactorBase)reactor);
                reactor.Start();
                connectionAdapter = reactor.ConnectionAdapter;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            return false;
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void StopReactor()
        {
            try
            {
                if (this.IsReactorOpen)
                {
                    connectionAdapter.StopReceive();
                    reactor.Stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }
        #endregion

        #region 界面消息监听处理
        public void DataPointListener_Send(NetworkData networkData, IConnection requestChannel)
        {
            string content = requestChannel.Encoder.ByteEncode(networkData.Buffer);
        }
        public void DataPointListener_Receive(NetworkData incomingData, IConnection responseChannel)
        {
            channelListenser.OnReceivedDataPoint(incomingData, responseChannel.RemoteHost.TaskTag);
        }
        /// <summary>
        /// 添加一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public void AddDataPointListener(IChannelListener listener)
        {
            channelListenser.AddDataPointListener(listener);
        }
        /// <summary>
        /// 移除一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveDataPointListener(IChannelListener listener)
        {
            channelListenser.RemoveDataPointListener(listener);
        }
        #endregion

    }
}