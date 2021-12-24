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
                if (config.HasOption(MqttClientOptionKey.DefaultSubscriberConfig.ToString()))
                {
                    this.defaultConfig = config.GetOption<IConnectionConfig>(MqttClientOptionKey.DefaultSubscriberConfig.ToString());
                }
                else
                {
                    this.defaultConfig = new DefaultConnectionConfig();
                }
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
                this.brokerId = ClientId;
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
            this.WorkDocker = new PushConnectionWorkDocker(this, this.ServiceConnectionFactory,this.defaultConfig);
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
            try
            {
                string text = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                string Topic = e.ApplicationMessage.Topic;
                string QoS = e.ApplicationMessage.QualityOfServiceLevel.ToString();
                string Retained = e.ApplicationMessage.Retain.ToString();
                LogAgent.Info("客户端[{options.ClientId}]尝试收到消息...>>>Topic:" + Topic + "; QoS: " + QoS + "; Retained: " + Retained + ";Msg: " + ByteEncode(e.ApplicationMessage.Payload));
                ServiceWorkerAdapter<TopicNotification> serviceWorker = this.WorkDocker.GetServiceWorkerAdapter(Topic);
                if (serviceWorker == null)
                {//没有就交给默认订阅链接处理
                    serviceWorker = this.WorkDocker.GetServiceWorkerAdapter();
                    if (serviceWorker == null)
                    {
                        LogAgent.Error("找不到对应的处理程序"+Topic);
                        return;
                    }
                }
                string connectionTag = serviceWorker.Connection.GetTag();
                TopicNotification topicNotification = new TopicNotification(connectionTag, Topic, text);
                topicNotification.payload = e.ApplicationMessage.Payload;
                serviceWorker.Connection.OnReceivedDataPoint(topicNotification, connectionTag);
            }
            catch (Exception ex)
            {

                LogAgent.Error(ex.ToString());
            }
           
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
            TopicNotification topicNotification = new TopicNotification()
            {
                Tag = options.ClientId
            };
            this.RaiseNewConnection(topicNotification);
        }
        #endregion

        #region 发布消息
        /// <summary>
        /// 发布Topic消息
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="payload"></param>
        public async Task ClientPublishMqttTopic(string topic, byte[] payload, MqttQualityLevel serviceLevel, bool retain)
        {
            try
            {
                MqttQualityOfServiceLevel mqttQualityOfServiceLevel = (MqttQualityOfServiceLevel)Enum.ToObject(typeof(MqttQualityOfServiceLevel), (int)serviceLevel);
                var message = new MqttApplicationMessage(topic, payload, mqttQualityOfServiceLevel, retain);
                LogAgent.Info(string.Format("客户端[{0}]发布主题[{1}]消息[{2}]成功！", options.ClientId, topic, ByteEncode(payload)));
                await this.refactor.PublishAsync(message);
            }
            catch (Exception ex)
            {
                LogAgent.Info(string.Format("客户端[{0}]发布主题[{1}]消息[{2}]异常！>{3}", options.ClientId, topic, ByteEncode(payload), ex.Message));
            }
        }
        /// <summary>
        /// 订阅Topic
        /// </summary>
        /// <param name="topic"></param>
        public async Task<IList<MqttSubscribeResult>> ClientSubscribeTopic(string topic, MqttQualityLevel serviceLevel)
        {
            try
            {
                MqttQualityOfServiceLevel mqttQualityOfServiceLevel = (MqttQualityOfServiceLevel)Enum.ToObject(typeof(MqttQualityOfServiceLevel), (int)serviceLevel);
                List<TopicFilter> topicFilters = new List<TopicFilter>()
                {
                    new TopicFilter(topic,mqttQualityOfServiceLevel),
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


        public string ByteEncode(byte[] buffer, int offset = 0, int size = 0)
        {
            if (size == 0)
            {
                size = buffer.Length;
            }
            StringBuilder ret = new StringBuilder();
            string tmp = "";
            for (int i = offset; i < size; ++i)
            {
                tmp = "0" + buffer[i].ToString("X");
                ret.Append(tmp.Substring(tmp.Length - 2, 2));
                ret.Append(" ");
            }
            return ret.ToString();
        }
    }
}
