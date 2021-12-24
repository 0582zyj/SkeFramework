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
using SkeFramework.Push.Mqtt.Reactor.Connection.Abstracts;

namespace SkeFramework.Push.Mqtt.Connection
{

    /// <summary>
    /// 发布推送链接
    /// </summary>
    public class PublishPushConnection : PushConnectionProxy, IPushConnection<TopicNotification>
    {

        public PublishPushConnection(IPushBroker<TopicNotification> pushBroker,string connectionTag) : base(pushBroker, connectionTag)
        {
        }
        public PublishPushConnection(IPushBroker<TopicNotification> pushBroker):base(pushBroker, MqttClientOptionKey.Publish)
        {
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public override Task Send(TopicNotification notification)
        {
            string topic = notification.Tag.ToString();
            byte[] payload = Encoding.UTF8.GetBytes(notification.Message);
            MqttQualityLevel serviceLevel = notification.QualityOfServiceLevel;
            bool retain = notification.Retain;
            Task task= ((MqttClientBroker)innerPushBroker).ClientPublishMqttTopic(topic, payload, serviceLevel, retain);
            return task;
        }
      
       
    }
}
