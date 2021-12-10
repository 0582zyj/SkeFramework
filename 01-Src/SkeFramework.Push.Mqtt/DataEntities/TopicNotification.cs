using MQTTnet.Core;
using MQTTnet.Core.Protocol;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Mqtt.DataEntities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Mqtt.DataEntities
{
    /// <summary>
    /// topic消息实体
    /// </summary>
    public class TopicNotification : INotification
    {
        private readonly MqttApplicationMessage mqttApplicationMessage;
        /// <summary>
        /// 链接标识
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// 消息质量级别
        /// </summary>
        public MqttQualityLevel QualityOfServiceLevel { get; set; }
        /// <summary>
        /// 是否保留
        /// </summary>
        public bool Retain { get; set; }


        public bool IsDeviceRegistrationIdValid()
        {
            return true;
        }

        public TopicNotification()
        {
        }

        public TopicNotification(MqttApplicationMessage mqttMessage)
        {
            mqttApplicationMessage = mqttMessage;
        }

        public TopicNotification(string tag, string topic, string message)
            :this( tag,  topic,  message, MqttQualityLevel.AtLeastOnce,true)
        {
         
        }
        public TopicNotification(string tag, string topic, string message, MqttQualityLevel serviceLevel)
          : this(tag, topic, message, serviceLevel, true)
        {

        }
        public TopicNotification(string tag ,string topic, string message, MqttQualityLevel serviceLevel,bool retain)
        {
            this.Tag = tag;
            this.Topic = topic;
            this.Message = message;
            this.QualityOfServiceLevel = serviceLevel;
            this.Retain = retain;
        }
    }
}
