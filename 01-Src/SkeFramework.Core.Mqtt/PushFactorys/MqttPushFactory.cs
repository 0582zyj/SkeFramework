using SkeFramework.Core.Mqtt.Connection;
using SkeFramework.Core.Mqtt.DataEntities;
using SkeFramework.Core.Mqtt.DataEntities.Constants;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap.Factorys.Abstracts;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Mqtt.PushFactorys
{
    public class MqttPushFactory : PushConnectionFactoryBase<TopicNotification>
    {
        protected override IPushConnection<TopicNotification> NewPushConnectionInternal(IPushBroker<TopicNotification> pushBroker, IConnectionConfig config)
        {
            if (config.HasOption(MqttClientOptionKey.Publish.ToString()))
                return new PublishPushConnection(pushBroker);
            else if (config.HasOption(MqttClientOptionKey.Subscriber.ToString()))
                return new SubscriberPushConnection(pushBroker);
            return null;
        }
    }
}
