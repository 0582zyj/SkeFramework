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
    public abstract class PushConnectionFactoryBase<TNotification> : IPushConnectionFactory<TNotification> where TNotification : INotification
    {
        /// <summary>
        /// 推送反应堆
        /// </summary>
        protected  IPushBroker<TNotification> pushBroker;
        /// <summary>
        /// 设置推送反应堆
        /// </summary>
        /// <param name="broker"></param>
        public void SetRelatedPushBroker(IPushBroker<TNotification> broker)
        {
            pushBroker = broker;
        }
        /// <summary>
        /// 创建一个推送链接
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public IPushConnection<TNotification> Create(IConnectionConfig config = null)
        {
            if (!(pushBroker is IPushBroker<TNotification>))
            {
                return null;
            }
            IPushBroker<TNotification> broker = pushBroker as IPushBroker<TNotification>;
            var connection = NewPushConnectionInternal(broker, config);
            return connection;
        }
        /// <summary>
        /// 创建推送连接具体实现
        /// </summary>
        /// <returns></returns>
        protected abstract IPushConnection<TNotification> NewPushConnectionInternal(IPushBroker<TNotification> pushBroker, IConnectionConfig connectionConfig);

       
    }
}