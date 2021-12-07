using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Listenser.ChannelListensers;
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
    public abstract class PushConnectionAbstract<TNotification> : DefaultChannelPromise, IPushConnection<TNotification> where TNotification : INotification
    {
        protected readonly IPushBroker<TNotification> innerPushBroker;
        protected readonly string innerConnectionTag;

        public PushConnectionAbstract(IPushBroker<TNotification> pushBroker,string connectionTag)
        {
            innerPushBroker = pushBroker;
            innerConnectionTag = connectionTag;
        }

        public abstract Task Send(TNotification notification);

        public virtual string GetTag()
        {
            return this.innerConnectionTag;
        }
    }
}
