using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services.Brokers;

namespace SkeFramework.Push.Core.Bootstrap.Factorys
{
    /// <summary>
    /// 推送工厂基础实现
    /// </summary>
    public abstract class PushServerFactoryBase<TNotification> : IPushServerFactory<TNotification> where TNotification:INotification
    {
        public IPushConnection<INotification> Create()
        {
            return NewPushConnectionInternal();
        }

        public IPushBroker<TNotification> NewPushBroker(IConnectionConfig connectionConfig)
        {
            var reactor = NewPushBrokerInternal(connectionConfig);
            reactor.SetupParamOptions(connectionConfig);
            reactor.DefaultWorks = 1;
            return reactor;
        }

        /// <summary>
        /// 创建服务端具体实现
        /// </summary>
        /// <param name="listenAddress"></param>
        /// <returns></returns>
        protected abstract PushBroker<TNotification> NewPushBrokerInternal(IConnectionConfig connectionConfig);
        /// <summary>
        /// 创建推送连接具体实现
        /// </summary>
        /// <returns></returns>
        protected abstract IPushConnection<INotification> NewPushConnectionInternal();

     
    }
}
