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
    /// 发布推送链接
    /// </summary>
    public class PublishPushConnection : PushConnectionAbstract<TopicNotification>, IPushConnection<TopicNotification>
    {
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
            string payload = notification.Message.ToString();
            MqttQualityOfServiceLevel serviceLevel = notification.QualityOfServiceLevel;
            bool retain = notification.Retain;
            return ((MqttClientBroker)innerPushBroker).ClientPublishMqttTopic(topic, payload, serviceLevel, retain);
        }
        /// <summary>
        /// 接受消息处理
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="controlerId"></param>
        public override void OnReceivedDataPoint(INotification datas, string controlerId)
        {
            base.OnReceivedDataPoint(datas, controlerId);
        }
    }
}
