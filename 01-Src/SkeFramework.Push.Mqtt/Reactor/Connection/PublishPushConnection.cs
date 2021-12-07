using MQTTnet.Core;
using MQTTnet.Core.Client;
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

namespace SkeFramework.Push.Mqtt.Connection
{

    /// <summary>
    /// 发布
    /// </summary>
    public class PublishPushConnection : PushConnectionAbstract<TopicNotification>, IPushConnection<TopicNotification>
    {
        public PublishPushConnection(IPushBroker<TopicNotification> pushBroker):base(pushBroker, MqttClientOptionKey.Publish)
        {
        }


        public override Task Send(TopicNotification notification)
        {
            string topic = notification.Tag.ToString();
            string payload = notification.Message.ToString();
            return ((MqttClientBroker)innerPushBroker).ClientPublishMqttTopic(topic, payload);
        }

        public override void OnReceivedDataPoint(INotification datas, string controlerId)
        {
            base.OnReceivedDataPoint(datas, controlerId);
        }
    }
}
