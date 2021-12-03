using MQTTnet.Core;
using SkeFramework.Push.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Mqtt.DataEntities
{
    /// <summary>
    /// topic消息实体
    /// </summary>
    public class TopicNotification : INotification
    {
        private readonly MqttApplicationMessage mqttApplicationMessage;

        public object Tag { get; set; }

        public string Message { get; set; }


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
    }
}
