using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Mqtt.DataEntities.Constants
{
   public class MqttClientOptionKey
    {
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

        /// <summary>
        /// 
        /// </summary>
        public const string Publish = "Publish";
        /// <summary>
        /// 
        /// </summary>
        public const string Subscriber = "Subscriber";
        
    }
}
