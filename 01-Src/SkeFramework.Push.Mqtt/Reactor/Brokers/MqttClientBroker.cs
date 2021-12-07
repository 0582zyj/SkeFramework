using MQTTnet;
using MQTTnet.Core;
using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using MQTTnet.Core.Serializer;
using Newtonsoft.Json;
using SkeFramework.Push.Mqtt.DataEntities;
using SkeFramework.Push.Mqtt.DataEntities.Constants;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services.Brokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Push.Mqtt.Reactor.Managers;
using SkeFramework.Push.Core.Services.Workers;
using SkeFramework.Push.Core.Services;

namespace SkeFramework.Push.Mqtt.Brokers
{
    /// <summary>
    /// Mqtt客户端实现
    /// </summary>
    public class MqttClientBroker : PushBroker<MqttClient, TopicNotification>, IPushBroker<TopicNotification>
    {
        /// <summary>
        /// 客户端连接参数
        /// </summary>
        private MqttClientOptions options;

        public MqttClientBroker(IPushConnectionFactory<TopicNotification> connectionFactory) :
            base(connectionFactory)
        {

        }
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="config"></param>
        public override void SetupParamOptions(IConnectionConfig config)
        {
            try
            {
                string ClientId = String.Empty;
                string tcpServer = String.Empty;
                string tcpPort = String.Empty;
                string mqttUser = String.Empty;
                string mqttPassword = String.Empty;
                if (config.HasOption(MqttClientOptionKey.ClientId.ToString()))
                    ClientId = config.GetOption(MqttClientOptionKey.ClientId.ToString()).ToString();
                if (config.HasOption(MqttClientOptionKey.tcpServer.ToString()))
                    tcpServer = config.GetOption(MqttClientOptionKey.tcpServer.ToString()).ToString();
                if (config.HasOption(MqttClientOptionKey.tcpPort.ToString()))
                    tcpPort = config.GetOption(MqttClientOptionKey.tcpPort.ToString()).ToString();
                if (config.HasOption(MqttClientOptionKey.mqttUser.ToString()))
                    mqttUser = config.GetOption(MqttClientOptionKey.mqttUser.ToString()).ToString();
                if (config.HasOption(MqttClientOptionKey.mqttPassword.ToString()))
                    mqttPassword = config.GetOption(MqttClientOptionKey.mqttPassword.ToString()).ToString();
                var mqttFactory = new MqttClientFactory();
                options = new MqttClientTcpOptions
                {
                    ClientId = ClientId,
                    Server = tcpServer,
                    Port = int.Parse(tcpPort),
                    ProtocolVersion = MqttProtocolVersion.V311,
                    DefaultCommunicationTimeout = TimeSpan.FromSeconds(10),
                    WillMessage = new MqttApplicationMessage($"LastWill/{ClientId.Trim()}", Encoding.UTF8.GetBytes("I Lost the connection!"), MqttQualityOfServiceLevel.ExactlyOnce, true)
                };

                if (!string.IsNullOrEmpty(mqttUser))
                {
                    options.UserName = mqttUser;
                    options.Password = mqttPassword;
                }

                options.CleanSession = true;
                options.KeepAlivePeriod = TimeSpan.FromSeconds(5);

                this.refactor = mqttFactory.CreateMqttClient() as MqttClient;
                this.refactor.Connected += MqttClient_Connected;
                this.refactor.Disconnected += MqttClient_Disconnected;
                this.refactor.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;

            }
            catch (Exception ex)
            {
                LogAgent.Info($"客户端尝试连接出错.>{ex.Message}");
            }
        }

        #region 开启和停止

        /// <summary>
        /// 启动
        /// </summary>
        protected override async void PushServerStart()
        {
            this.ServiceConnectionFactory.SetRelatedPushBroker(this);
            this.WorkDocker = new PushConnectionWorkDocker(this, this.ServiceConnectionFactory);
            LogAgent.Info($"客户端[{options.ClientId}]尝试连接...");
            await this.refactor.ConnectAsync(options);
        }
        /// <summary>
        /// 停止
        /// </summary>
        protected override void PushServerStop()
        {
            try
            {
                if (this.refactor == null) return;
                this.refactor.DisconnectAsync();
                this.refactor = null;
            }
            catch (Exception ex)
            {
                LogAgent.Info($"客户端[{options.ClientId}]尝试断开连接..." + ex.ToString());
            }
        }
        #endregion

        #region 消息事件
        /// <summary>
        /// 接受消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {

            string text = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            string Topic = e.ApplicationMessage.Topic;
            string QoS = e.ApplicationMessage.QualityOfServiceLevel.ToString();
            string Retained = e.ApplicationMessage.Retain.ToString();
            LogAgent.Info("客户端[{options.ClientId}]尝试收到消息...>>>Topic:" + Topic + "; QoS: " + QoS + "; Retained: " + Retained + ";Msg: " + text);
            ServiceWorkerAdapter<TopicNotification> serviceWorker=  this.WorkDocker.GetServiceWorkerAdapter(Topic);
            if (serviceWorker == null)
            {
                return;
            }
            string taskId = serviceWorker.Connection.GetTag();
            TopicNotification topicNotification = new TopicNotification(taskId, Topic,text);
            serviceWorker.Connection.OnReceivedDataPoint(topicNotification, taskId);
        }
        /// <summary>
        /// 断开连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MqttClient_Disconnected(object sender, EventArgs e)
        {
            LogAgent.Info($"客户端[{options.ClientId}]断开连接...>{sender.ToString()}");
        }
        /// <summary>
        /// 连接成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MqttClient_Connected(object sender, EventArgs e)
        {
            LogAgent.Info($"客户端[{options.ClientId}]成功连接");
        }
        #endregion

        #region 发布消息
        /// <summary>
        /// 发布Topic消息
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="payload"></param>
        public async Task ClientPublishMqttTopic(string topic, string payload)
        {
            try
            {
                var message = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(payload), MqttQualityOfServiceLevel.AtLeastOnce, true);
                LogAgent.Info(string.Format("客户端[{0}]发布主题[{1}]消息[{2}]成功！", options.ClientId, topic,payload));
                await this.refactor.PublishAsync(message);
            }
            catch (Exception ex)
            {
                LogAgent.Info(string.Format("客户端[{0}]发布主题[{1}]消息[{2}]异常！>{3}", options.ClientId, topic, payload, ex.Message));
            }
        }
        /// <summary>
        /// 订阅Topic
        /// </summary>
        /// <param name="topic"></param>
        public async Task<IList<MqttSubscribeResult>> ClientSubscribeTopic(string topic)
        {
            try
            {
                List<TopicFilter> topicFilters = new List<TopicFilter>()
                {
                    new TopicFilter(topic,MqttQualityOfServiceLevel.AtLeastOnce),
                };
                LogAgent.Info(string.Format("客户端[{0}]订阅主题[{1}]成功！", this.options.ClientId, topic));
                return await this.refactor.SubscribeAsync(topicFilters);
            }
            catch (Exception ex)
            {
                LogAgent.Info(string.Format("客户端[{0}]订阅主题[{1}]异常！>{2}", this.options.ClientId, topic, ex.Message));
            }
            return null;
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="topic"></param>
        public async Task ClientUnSubscribeTopic(string topic)
        {
            try
            {
                await this.refactor.UnsubscribeAsync(topic);
                LogAgent.Info(string.Format("客户端[{0}]取消主题[{1}]成功！", this.options.ClientId, topic));
            }
            catch (Exception ex)
            {
                LogAgent.Info(string.Format("客户端[{0}]取消主题[{1}]异常！>{2}", options.ClientId, topic, ex.Message));
            }
        }
        #endregion

    }
}
