using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using SkeFramework.Push.Mqtt.DataEntities;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Push.Mqtt.DataEntities.Constants;
using SkeFramework.Push.Mqtt.Brokers;
using SkeFramework.Push.Mqtt.Reactor.Connection.Abstracts;

namespace SkeFramework.Push.Mqtt.Connection
{
    /// <summary>
    /// 订阅推送链接
    /// </summary>
    public class SubscriberPushConnection : PushConnectionProxy, IPushConnection<TopicNotification>
    {

        public SubscriberPushConnection(IPushBroker<TopicNotification> pushBroker,string connectionTag) : base(pushBroker, connectionTag)
        {
        }
        public SubscriberPushConnection(IPushBroker<TopicNotification> pushBroker) : base(pushBroker, MqttClientOptionKey.Subscriber)
        {
        }
        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public override Task Send(TopicNotification notification)
        {
            string topic = notification.Topic.ToString();
            return ((MqttClientBroker)innerPushBroker).ClientSubscribeTopic(topic, notification.QualityOfServiceLevel);
        }
      
    }
}