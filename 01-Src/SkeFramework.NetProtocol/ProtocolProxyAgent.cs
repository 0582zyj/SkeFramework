using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Bootstrap;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Topology;

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
        /// 启动一个通信
        /// </summary>
        /// <param name="nodeConfig"></param>
        public void StartReactor(NodeConfig nodeConfig)
        {
            var bootstrapper = new ServerBootstrap()
                .WorkerThreads(2)
                .Build();
            reactor = bootstrapper.NewReactor(NodeBuilder.BuildNode().Host(nodeConfig));
            reactor.ConnectionAdapter = new ProtocolUT("12", (ReactorBase)reactor);
            reactor.Start();
            connectionAdapter = reactor.ConnectionAdapter;
        }


    }
}