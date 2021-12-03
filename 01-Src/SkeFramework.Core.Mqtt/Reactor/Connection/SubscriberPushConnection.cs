using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using SkeFramework.Core.Mqtt.DataEntities;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Mqtt.Connection
{
    /// <summary>
    /// 订阅
    /// </summary>
    public class SubscriberPushConnection : PushConnectionAbstract<TopicNotification>, IPushConnection<TopicNotification>
    {
        public SubscriberPushConnection(IPushBroker<TopicNotification> pushBroker) : base(pushBroker)
        {
        }

        public override Task Send(TopicNotification notification)
        {
            return Task.Factory.StartNew(delegate
            {
                string topic = notification.Tag.ToString();
                string payload = notification.Message.ToString();
                try
                {
                    List<TopicFilter> topicFilters = new List<TopicFilter>()
                    {
                        new TopicFilter(topic,MqttQualityOfServiceLevel.AtLeastOnce),
                    };
                    this.innerPushBroker.GetRefactorBroker<MqttClient>().SubscribeAsync(topicFilters);
                    LogAgent.Info(string.Format("客户端[{0}]订阅主题[{1}]成功！", this.innerPushBroker.GetRefactorBroker<MqttClient>().ToString(), topic));
                }
                catch (Exception ex)
                {
                    LogAgent.Info(string.Format("客户端[{0}]发布主题[{1}]异常！>{2}", innerPushBroker.ToString(), topic, ex.Message));
                }
            });


        }
    }
}