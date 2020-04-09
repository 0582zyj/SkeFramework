using System;
using System.Collections.Generic;
using SkeFramework.NetSerialPort.Bootstrap;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Topology;
using ULCloudLockTool.BLL.SHProtocol.DataHandle;

namespace ULCloudLockTool.BLL.SHProtocol
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
        protected IReactor reactor;
        /// <summary>
        /// 协议适配类
        /// </summary>
        protected IConnection connectionAdapter;
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
        /// 界面监听消息列表
        /// </summary>
        protected List<IDataPointListener> _mDataPointListeners = null;

        #region 协议处理代理
        /// <summary>
        /// 端口是否打开
        /// </summary>
        /// <returns></returns>
        public bool IsReactorOpen
        {
            get {
                if (this.reactor == null)
                {
                    return false;
                }
                return this.reactor.IsActive;
            }private set{}
        }
        /// <summary>
        /// 启动一个通信
        /// </summary>
        /// <param name="nodeConfig"></param>
        public virtual bool StartReactor(NodeConfig nodeConfig)
        {
            try
            {
                var bootstrapper = new ServerBootstrap()
               .WorkerThreads(2)
               .Build();
                reactor = bootstrapper.NewReactor(NodeBuilder.BuildNode().Host(nodeConfig));
                reactor.ConnectionAdapter = new ProtocolUT("12", (ReactorBase)reactor);
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
        #endregion

        #region 界面消息监听处理
        public void DataPointListener_Receive(NetworkData incomingData, IConnection responseChannel)
        {
            if (_mDataPointListeners != null && _mDataPointListeners.Count>0)
            {
                foreach(var item in _mDataPointListeners)
                {
                    item.OnReceivedDataPoint(incomingData, responseChannel.Local.TaskTag);
                }
            }
        }
        /// <summary>
        /// 添加一个监听者
        /// </summary>
        /// <param name="listener"></param>

        public void AddDataPointListener(IDataPointListener listener)
        {
            if (null == listener)
            {
                return;
            }
            if (null == _mDataPointListeners)
            {
                _mDataPointListeners = new List< IDataPointListener>();
            }
            if (_mDataPointListeners.Contains(listener))
            {
                return;
            }
            _mDataPointListeners.Add(listener);
        }
        /// <summary>
        /// 移除一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveDataPointListener(IDataPointListener listener)
        {
            if (null == listener)
            {
                return;
            }
            if (_mDataPointListeners!=null&& _mDataPointListeners.Contains(listener))
            {
                _mDataPointListeners.Remove(listener);
            }
        }
        #endregion

    }
}