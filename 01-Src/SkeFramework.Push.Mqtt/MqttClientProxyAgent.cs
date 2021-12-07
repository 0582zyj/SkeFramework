using MQTTnet;
using MQTTnet.Core;
using MQTTnet.Core.Adapter;
using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using MQTTnet.Core.Serializer;
using Newtonsoft.Json;
using SkeFramework.Push.Mqtt.Bootstrap;
using SkeFramework.Push.Mqtt.Brokers;
using SkeFramework.Push.Mqtt.DataEntities;
using SkeFramework.Push.Mqtt.DataEntities.Constants;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Push.Core.Listenser.Interfaces;
using SkeFramework.Push.Core.Listenser.ChannelListensers;
using SkeFramework.Push.Core.Services.Connections;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Mqtt.Reactor.Managers;

namespace SkeFramework.Push.Mqtt
{
    /// <summary>
    /// Mqtt客户端工具类
    /// </summary>
    public class MqttClientProxyAgent
    {
        #region 单例模式
        /// <summary>
        /// 协议管理器
        /// </summary>
        private static MqttClientProxyAgent mSingleInstance;
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static MqttClientProxyAgent Instance()
        {
            if (null == mSingleInstance)
            {
                mSingleInstance = new MqttClientProxyAgent();
            }
            return mSingleInstance;
        }
        #endregion

        /// <summary>
        /// 通信基类
        /// </summary>
        private MqttClientBroker pushBroker = null;
        /// <summary>
        /// 默认工作线程
        /// </summary>
        private PushConnectionWorkDocker defaultWorkDocker;
        /// <summary>
        /// 协议监听容器
        /// </summary>
        private IChannelPromise channelListenser = new DefaultChannelPromise();

        #region 开启和停止

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
                this.pushBroker = bootstrap.BuildPushBroker<MqttClientBroker, TopicNotification>();
                this.pushBroker.SetupParamOptions(bootstrap.GetConfig());
                this.pushBroker.Start();
                this.defaultWorkDocker = this.pushBroker.GetDefaultWorker() as PushConnectionWorkDocker;
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
        #endregion

        #region 发布和订阅
        public void ClientPublishMqttTopic(string topic, string payload)
        {
            TopicNotification topicNotification = new TopicNotification(MqttClientOptionKey.Publish, topic, payload);
            defaultWorkDocker.OnReceivedDataPoint(topicNotification, topic);
        }

        public void ClientSubscribeTopic(string topic)
        {
            TopicNotification topicNotification = new TopicNotification(MqttClientOptionKey.Subscriber, topic, "");
            defaultWorkDocker.OnReceivedDataPoint(topicNotification, MqttClientOptionKey.Subscriber);
        }

        public async void ClientUnSubscribeTopic(string topic)
        {
            await pushBroker.ClientUnSubscribeTopic(topic);
        }
        #endregion



        #region 界面消息监听处理
        public void DataPointListener_Send(TopicNotification networkData, PushConnectionAbstract<TopicNotification> requestChannel)
        {
         
        }
        public void DataPointListener_Receive(TopicNotification incomingData, PushConnectionAbstract<TopicNotification> responseChannel)
        {
            channelListenser.OnReceivedDataPoint(incomingData, "");
        }
        /// <summary>
        /// 添加一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public void AddDataPointListener(IChannelListener listener)
        {
            channelListenser.AddDataPointListener(listener);
        }
        /// <summary>
        /// 移除一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveDataPointListener(IChannelListener listener)
        {
            channelListenser.RemoveDataPointListener(listener);
        }
        #endregion
    }
}
