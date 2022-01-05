using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Mqtt.DataEntities.Constants
{
   public class MqttClientOptionKey
    {
        #region MqttClient配置
        /// <summary>
        /// 
        /// </summary>
        public const string ClientId = "ClientId";

        /// <summary>
        /// 
        /// </summary>
        public const string tcpServer = "tcpServer";
        /// <summary>
        /// 
        /// </summary>
        public const string tcpPort = "tcpPort";
        /// <summary>
        /// 
        /// </summary>
        public const string mqttUser = "mqttUser";
        /// <summary>
        /// 
        /// </summary>
        public const string mqttPassword = "mqttPassword";
        #endregion

        /// <summary>
        /// 默认订阅处理链接
        /// </summary>
        public const string DefaultSubscriberConfig = "DefaultSubscriberConfig";
        /// <summary>
        /// 发布任务KET
        /// </summary>
        public const string Publish = "Publish";
        /// <summary>
        /// 订阅主题的KET
        /// </summary>
        public const string Subscriber = "Subscriber";
        /// <summary>
        /// 消息内容
        /// </summary>
        public const string payload = "payload";
        /// <summary>
        /// 消息主题
        /// </summary>
        public const string topic = "topic";
        /// <summary>
        /// 消息内容
        /// </summary>
        public const string willMessage = "willMessage";


    }
}
