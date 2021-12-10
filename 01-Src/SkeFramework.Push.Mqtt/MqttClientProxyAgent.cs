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
        protected static MqttClientProxyAgent mSingleInstance;
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public  static MqttClientProxyAgent Instance()
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
        /// <summary>
        /// 启动客户端连接
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="tcpServer"></param>
        /// <param name="tcpPort"></param>
        /// <param name="mqttUser"></param>
        /// <param name="mqttPassword"></param>
        public virtual void ClientStart(string ClientId, string tcpServer, int tcpPort, string mqttUser, string mqttPassword)
        {
            try
            {
                MqttPushBootstrap bootstrap = new MqttPushBootstrap()
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
        /// <summary>
        /// 关闭客户端
        /// </summary>
        public virtual void ClientStop()
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
        /// <summary>
        /// 发布主题
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="payload"></param>
        public void ClientPublishMqttTopic(string topic, string payload, MqttQualityLevel serviceLevel = MqttQualityLevel.AtLeastOnce, bool retain=true)
        {
            TopicNotification topicNotification = new TopicNotification(MqttClientOptionKey.Publish, topic, payload, serviceLevel,retain);
            defaultWorkDocker.OnReceivedDataPoint(topicNotification, topic);
        }
        /// <summary>
        /// 订阅主题消息
        /// </summary>
        /// <param name="topic"></param>
        public void ClientSubscribeTopic(string topic, MqttQualityLevel serviceLevel= MqttQualityLevel.AtLeastOnce)
        {
            TopicNotification topicNotification = new TopicNotification(MqttClientOptionKey.Subscriber, topic, "",  serviceLevel);
            defaultWorkDocker.OnReceivedDataPoint(topicNotification, MqttClientOptionKey.Subscriber);
        }
        /// <summary>
        /// 取消主题订阅
        /// </summary>
        /// <param name="topic"></param>
        public async void ClientUnSubscribeTopic(string topic)
        {
            await pushBroker.ClientUnSubscribeTopic(topic);
        }
        #endregion


        #region 界面消息监听处理
        /// <summary>
        /// 发送数据监听器
        /// </summary>
        /// <param name="networkData"></param>
        /// <param name="requestChannel"></param>
        public virtual void DataPointListener_Send(TopicNotification networkData, PushConnectionAbstract<TopicNotification> requestChannel)
        {
         
        }
        /// <summary>
        /// 接收数据监听器
        /// </summary>
        /// <param name="incomingData"></param>
        /// <param name="responseChannel"></param>
        public virtual void DataPointListener_Receive(TopicNotification incomingData, PushConnectionAbstract<TopicNotification> responseChannel)
        {
            channelListenser.OnReceivedDataPoint(incomingData, "");
        }
        /// <summary>
        /// 添加一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public virtual void AddDataPointListener(IChannelListener listener)
        {
            channelListenser.AddDataPointListener(listener);
        }
        /// <summary>
        /// 移除一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public virtual void RemoveDataPointListener(IChannelListener listener)
        {
            channelListenser.RemoveDataPointListener(listener);
        }
        #endregion
    }
}
