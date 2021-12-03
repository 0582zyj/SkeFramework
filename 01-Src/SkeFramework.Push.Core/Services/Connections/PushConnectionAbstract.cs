using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Services.Connections
{
    /// <summary>
    /// 客户端推送反应堆
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public abstract class PushConnectionAbstract<TNotification> : IPushConnection<TNotification> where TNotification : INotification
    {
        protected readonly IPushBroker<TNotification> innerPushBroker;

        public PushConnectionAbstract(IPushBroker<TNotification> pushBroker)
        {
            innerPushBroker = pushBroker;
        }

        public abstract Task Send(TNotification notification);
    }
}
