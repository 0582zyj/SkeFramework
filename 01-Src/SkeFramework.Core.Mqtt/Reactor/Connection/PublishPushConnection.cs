using MQTTnet.Core;
using MQTTnet.Core.Client;
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
    /// 发布
    /// </summary>
    public class PublishPushConnection : PushConnectionAbstract<TopicNotification>, IPushConnection<TopicNotification>
    {
        public PublishPushConnection(IPushBroker<TopicNotification> pushBroker):base(pushBroker)
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
                    var message = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(payload), MqttQualityOfServiceLevel.AtLeastOnce, true);
                    innerPushBroker.GetRefactorBroker<MqttClient>().PublishAsync(message);
                    LogAgent.Info(string.Format("客户端[{0}]发布主题[{1}]成功！", innerPushBroker.ToString(), topic));
                }
                catch (Exception ex)
                {
                    LogAgent.Info(string.Format("客户端[{0}]发布主题[{1}]异常！>{2}", innerPushBroker.ToString(), topic, ex.Message));
                }
            });
        }
    }
}
