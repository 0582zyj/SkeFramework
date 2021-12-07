using MQTTnet.Core;
using SkeFramework.Push.Core.Interfaces;
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

        public TopicNotification(string tag ,string topic, string message)
        {
            this.Tag = tag;
            this.Topic = topic;
            this.Message = message;
        }
    }
}
