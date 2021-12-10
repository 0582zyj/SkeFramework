using SkeFramework.Push.Mqtt.Connection;
using SkeFramework.Push.Mqtt.DataEntities;
using SkeFramework.Push.Mqtt.DataEntities.Constants;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap.Factorys.Abstracts;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Core.NetLog;
using SkeFramework.Push.Core.Bootstrap.Factorys;
using SkeFramework.Push.Mqtt.Brokers;
using SkeFramework.Push.Core.Services.Brokers;
using MQTTnet.Core.Client;

namespace SkeFramework.Push.Mqtt.PushFactorys
{
    /// <summary>
    /// MQTT客户端推送链接工厂
    /// </summary>
    public class MqttPushFactory : PushServerFactoryBase<MqttClient, TopicNotification>
    {
        /// <summary>
        /// 创建一个反应堆
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        protected override PushBroker<MqttClient, TopicNotification> NewPushBrokerInternal(IConnectionConfig config)
        {
            return new MqttClientBroker(this);
        }
        /// <summary>
        /// 创建一个推送链接
        /// </summary>
        /// <param name="pushBroker">推送客户端</param>
        /// <param name="config">链接配置</param>
        /// <returns></returns>
        protected override IPushConnection<TopicNotification> NewPushConnectionInternal(IPushBroker<TopicNotification> pushBroker, IConnectionConfig config)
        {
            if (config==null|| !config.HasOption(MqttClientOptionKey.TaskId.ToString()))
                return null;
            string taskId = config.GetOption<string>(MqttClientOptionKey.TaskId);
            switch (taskId)
            {
                case MqttClientOptionKey.Publish:
                    return new PublishPushConnection(pushBroker);
                case MqttClientOptionKey.Subscriber:
                    return new SubscriberPushConnection(pushBroker);
                default:
                    LogAgent.Info($"客户端[{taskId}]连接创建失败");
                    return null;
            }
        }
    }
}
