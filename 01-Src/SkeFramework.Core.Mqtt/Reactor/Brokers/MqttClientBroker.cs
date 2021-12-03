using MQTTnet;
using MQTTnet.Core;
using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using MQTTnet.Core.Serializer;
using Newtonsoft.Json;
using SkeFramework.Core.Mqtt.DataEntities;
using SkeFramework.Core.Mqtt.DataEntities.Constants;
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

namespace SkeFramework.Core.Mqtt.Brokers
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

        public MqttClientBroker(IPushConnectionFactory connectionFactory):
            base(connectionFactory)
        {
            connectionFactory.SetRelatedPushBroker(this as IPushBroker<INotification>);
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
                    Port = int.Parse( tcpPort),
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
        /// <summary>
        /// 启动
        /// </summary>
        protected  override async void PushServerStart()
        {
            await this.refactor.ConnectAsync(options);
            LogAgent.Info($"客户端[{options.ClientId}]尝试连接...");
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
                LogAgent.Info($"客户端[{options.ClientId}]尝试断开连接..."+ex.ToString());
            }
        }
        /// <summary>
        /// 接受消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            LogAgent.Info($"客户端[{options.ClientId}]尝试收到消息...>{JsonConvert.SerializeObject(e.ApplicationMessage)}");
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
            LogAgent.Info($"客户端[{options.ClientId}]成功连接...>{sender.ToString()}");
        }

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
                await this.refactor.PublishAsync(message);
                LogAgent.Info(string.Format("客户端[{0}]发布主题[{1}]成功！", options.ClientId, topic));
            }
            catch (Exception ex)
            {
                LogAgent.Info(string.Format("客户端[{0}]发布主题[{1}]异常！>{2}", options.ClientId, topic, ex.Message));
            }
        }
        /// <summary>
        /// 订阅Topic
        /// </summary>
        /// <param name="topic"></param>
        public async Task<IList<MqttSubscribeResult>> ClientSubscribeTopic(string topic)
        {
            List<TopicFilter> topicFilters = new List<TopicFilter>()
            {
                new TopicFilter(topic,MqttQualityOfServiceLevel.AtLeastOnce),
            };
            LogAgent.Info(string.Format("客户端[{0}]订阅主题[{1}]成功！", this.options.ClientId, topic));
            return await this.refactor.SubscribeAsync(topicFilters);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="topic"></param>
        public async Task ClientUnSubscribeTopic(string topic)
        {
            await this.refactor.UnsubscribeAsync(topic);
            LogAgent.Info(string.Format("客户端[{0}]取消主题[{1}]成功！", this.options.ClientId, topic));
        }

    }
}
