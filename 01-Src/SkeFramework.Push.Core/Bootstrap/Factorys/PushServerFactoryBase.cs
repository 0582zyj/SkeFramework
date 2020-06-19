using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Constants;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services.Brokers;

namespace SkeFramework.Push.Core.Bootstrap.Factorys
{
    /// <summary>
    /// 推送基础实现
    /// </summary>
    public abstract class PushServerFactoryBase<TNotification> : IPushServerFactory<TNotification> where TNotification:INotification
    {
        /// <summary>
        /// 创建一个推送链接
        /// </summary>
        /// <returns></returns>
        public IPushConnection<INotification> Create()
        {
            return NewPushConnectionInternal();
        }
        /// <summary>
        /// 创建一个服务端推送
        /// </summary>
        /// <param name="connectionConfig"></param>
        /// <returns></returns>
        public IPushBroker<TNotification> NewPushBroker(IConnectionConfig connectionConfig)
        {
            var reactor = NewPushBrokerInternal(connectionConfig);
            reactor.SetupParamOptions(connectionConfig);
            int Works = 1;
            if (connectionConfig.HasOption(DefaultConfigTypeEumns.Workers.ToString()))
            {
                Works = connectionConfig.GetOption<int>(DefaultConfigTypeEumns.Workers.ToString());
            }
            reactor.DefaultWorks = Works;
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
