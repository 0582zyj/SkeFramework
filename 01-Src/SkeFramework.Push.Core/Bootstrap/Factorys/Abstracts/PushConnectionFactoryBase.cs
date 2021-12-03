using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Constants;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Bootstrap.Factorys.Abstracts
{
    /// <summary>
    /// 推送基础实现
    /// </summary>
    public abstract class PushConnectionFactoryBase<TNotification> : IPushConnectionFactory where TNotification : INotification
    {
        protected  IPushBroker<INotification> pushBroker;

        public void SetRelatedPushBroker(IPushBroker<INotification> broker)
        {
            pushBroker = broker;
        }

        public IPushConnection<INotification> Create()
        {
            return NewPushConnection(null) as IPushConnection<INotification>;
        }


        /// <summary>
        /// 创建一个服务端推送
        /// </summary>
        /// <param name="connectionConfig"></param>
        /// <returns></returns>
        public virtual IPushConnection<TNotification> NewPushConnection(IConnectionConfig connectionConfig)
        {
            if(!(pushBroker is IPushBroker<TNotification>))
            {
                return null;
            }
            IPushBroker<TNotification> broker = pushBroker as IPushBroker<TNotification>;
            var connection = NewPushConnectionInternal(broker, connectionConfig);
            return connection;
        }

        /// <summary>
        /// 创建推送连接具体实现
        /// </summary>
        /// <returns></returns>
        protected abstract IPushConnection<TNotification> NewPushConnectionInternal(IPushBroker<TNotification> pushBroker, IConnectionConfig connectionConfig);

       
    }
}