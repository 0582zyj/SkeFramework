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
using SkeFramework.Push.Mqtt.Reactor.Connection.Abstracts;
using SkeFramework.Push.Core.Listenser;

namespace SkeFramework.Push.Mqtt.Connection
{

    /// <summary>
    /// 发布推送链接
    /// </summary>
    public class PublishPushConnection : PushConnectionProxy, IPushConnection<TopicNotification>
    {

        /// <summary>
        /// 协议发送监听器
        /// </summary>
        public SenderListenser<TopicNotification> Sender;

        #region 发布推送链接状态
        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime Created { get; private set; }
        /// <summary>
        /// 超时时间设置
        /// </summary>
        public TimeSpan Timeout
        {
            get; set;
        }
        /// <summary>
        /// 是否已过期
        /// </summary>
        public bool Dead
        {
            get { return DateTime.Now.Subtract(this.Created.Add(this.Timeout)).Ticks > 0; }
            set
            {
                if (value)
                {
                    this.Timeout = TimeSpan.FromSeconds(0);
                    this.Sender.EndSend();
                }
                else this.Created = DateTime.Now;
            }
        }
        /// <summary>
        /// 当前是否正在接受
        /// </summary>
        public bool Receiving { get; set; }
        /// <summary>
        /// 协议是否已打开
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            return this.innerPushBroker.GetRefactorBroker<MqttClientBroker>().active;
        }
        /// <summary>
        /// 重置发送监听器
        /// </summary>
        public bool Reset { get; set; }
        #endregion

        #region 构造函数
        public PublishPushConnection(IPushBroker<TopicNotification> pushBroker) : this(pushBroker, MqttClientOptionKey.Publish)
        {
        }
        public PublishPushConnection(IPushBroker<TopicNotification> pushBroker,string connectionTag) : base(pushBroker, connectionTag)
        {
            Sender = new SenderListenser<TopicNotification>(this);
        }
        #endregion


        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public override Task Send(TopicNotification notification)
        {
            this.Receiving = true;
            string topic = notification.Tag.ToString();
            byte[] payload = Encoding.UTF8.GetBytes(notification.Message);
            MqttQualityLevel serviceLevel = notification.QualityOfServiceLevel;
            bool retain = notification.Retain;
            return ((MqttClientBroker)innerPushBroker).ClientPublishMqttTopic(topic, payload, serviceLevel, retain);
        }
        /// <summary>
        /// 超时重发方法 
        /// </summary>
        /// <param name="frame">    发送帧 </param>
        /// <param name="interval"> 发送间隔 </param>
        /// <param name="sendTimes"> 发送次数 </param>
        protected virtual void CaseSendFrame(TopicNotification notification, int interval=3000, int sendTimes=1)
        {
            if (null == notification)
            {
                return;
            }
            this.Sender.FrameBeSent = notification;
            this.Sender.Interval = interval;
            this.Sender.TotalSendTimes = sendTimes;
            this.Sender.Start();
        }
         
    }
}
