using SkeFramework.Push.Mqtt.Brokers;
using SkeFramework.Push.Mqtt.DataEntities;
using SkeFramework.Push.Mqtt.DataEntities.Constants;
using SkeFramework.Push.Mqtt.PushFactorys;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Bootstrap.Factorys;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Mqtt.Bootstrap
{
    /// <summary>
    /// 推送引导程序
    /// </summary>
    public class MqttPushBootstrap : AbstractBootstrap
    {

        /// <summary>
        /// 推送类型
        /// </summary>
        private MqttClientType PushType = MqttClientType.None;

        public MqttPushBootstrap()
        {
            this.config = new DefaultConnectionConfig();
        }

        #region 引导程序参数设置
        /// <summary>
        /// 设置服务端类型
        /// </summary>
        /// <param name="pushType"></param>
        /// <returns></returns>
        public MqttPushBootstrap SetPushType(MqttClientType mqttClientType)
        {
            PushType = mqttClientType;
            return this;
        }

        /// <summary>
        /// 设置客户端ID
        /// </summary>
        /// <returns></returns>
        public MqttPushBootstrap SetClientId(string ClientId)
        {
            this.config.SetOption(MqttClientOptionKey.ClientId, ClientId);
            return this;
        }
        /// <summary>
        /// 设置服务器地址
        /// </summary>
        /// <returns></returns>
        public MqttPushBootstrap SetTcpServer(string tcpServer)
        {
            this.config.SetOption(MqttClientOptionKey.tcpServer, tcpServer);
            return this;
        }
        /// <summary>
        /// 设置端口
        /// </summary>
        /// <returns></returns>
        public MqttPushBootstrap SetTcpPort(string tcpPort)
        {
            this.config.SetOption(MqttClientOptionKey.tcpPort, tcpPort);
            return this;
        }
        /// <summary>
        /// 设置用户名
        /// </summary>
        /// <returns></returns>
        public MqttPushBootstrap SetMqttUser(string mqttUser)
        {
            this.config.SetOption(MqttClientOptionKey.mqttUser, mqttUser);
            return this;
        }
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <returns></returns>
        public MqttPushBootstrap SetMqttPassword(string mqttPassword)
        {
            this.config.SetOption(MqttClientOptionKey.mqttPassword, mqttPassword);
            return this;
        }

        #endregion

        #region 服务端和链接设置
        /// <summary>
        /// 检查参数
        /// </summary>
        public override void Validate()
        {
            if (PushType == MqttClientType.None)
            {
                throw new ArgumentException("Can't be none", "PushType");
            }
        }
        /// <summary>
        /// 创建服务端链接
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <returns></returns>
        public override IPushServerFactory<TopicNotification> BuildPushServerFactory<TopicNotification>()
        {
            var dataType = typeof(DataEntities.TopicNotification);
            if (IsSubclassOf(typeof(DataEntities.TopicNotification), dataType))
            {
                return new MqttPushFactory() as IPushServerFactory<TopicNotification>;
            }
            return null;
        }
        #endregion

    }
}
