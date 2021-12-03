using MQTTnet;
using MQTTnet.Core;
using MQTTnet.Core.Adapter;
using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using MQTTnet.Core.Serializer;
using Newtonsoft.Json;
using SkeFramework.Core.Mqtt.Bootstrap;
using SkeFramework.Core.Mqtt.Brokers;
using SkeFramework.Core.Mqtt.DataEntities;
using SkeFramework.Core.Mqtt.DataEntities.Constants;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Mqtt
{
    /// <summary>
    /// Mqtt客户端工具类
    /// </summary>
    public class MqttClientUtil
    {
        private MqttClientBroker pushBroker = null;


        public void ClientStart(string ClientId, string tcpServer, int tcpPort, string mqttUser, string mqttPassword)
        {
            try
            {
                PushBootstrap bootstrap = new PushBootstrap()
                    .SetPushType(MqttClientType.Client)
                    .SetClientId(ClientId)
                    .SetTcpServer(tcpServer)
                    .SetTcpPort(tcpPort.ToString())
                    .SetMqttUser(mqttUser)
                    .SetMqttPassword(mqttPassword);
                pushBroker = bootstrap.BuildPushBroker<MqttClientBroker, TopicNotification>();
                pushBroker.SetupParamOptions(bootstrap.GetConfig());
                pushBroker.Start();
            }
            catch (Exception ex)
            {
                LogAgent.Error($"客户端建立连接...>{ex.ToString()}");
            }
        }
        public void ClientStop()
        {
            try
            {
                if (pushBroker == null) return;
                pushBroker.Stop();
                pushBroker = null;
            }
            catch (Exception ex)
            {
                LogAgent.Error($"客户端断开连接...>{ex.ToString()}");
            }
        }
       

        public async void ClientPublishMqttTopic(string topic, string payload)
        {
            await pushBroker.ClientPublishMqttTopic(topic, payload);
        }

        public async void ClientSubscribeTopic(string topic)
        {
           await  pushBroker.ClientSubscribeTopic(topic);
        }

        public async void ClientUnSubscribeTopic(string topic)
        {
            await pushBroker.ClientUnSubscribeTopic(topic);
        }
        /// <summary>
        /// 当客户端接收到所订阅的主题消息时
        /// </summary>
        /// <param name="e"></param>
        public void OnSubscriberMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            string text = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            string Topic = e.ApplicationMessage.Topic;
            string QoS = e.ApplicationMessage.QualityOfServiceLevel.ToString();
            string Retained = e.ApplicationMessage.Retain.ToString();
            LogAgent.Info("MessageReceived >>Topic:" + Topic + "; QoS: " + QoS + "; Retained: " + Retained);
            LogAgent.Info("MessageReceived >>Msg: " + text);
        }
    }
}
