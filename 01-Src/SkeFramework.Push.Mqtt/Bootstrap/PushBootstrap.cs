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
    public class PushBootstrap: AbstractBootstrap 
    {

        /// <summary>
        /// 推送类型
        /// </summary>
        private MqttClientType PushType = MqttClientType.None;

        public PushBootstrap()
        {
            this.connectionConfig = new DefaultConnectionConfig();
        }

        #region 引导程序参数设置
        /// <summary>
        /// 设置服务端类型
        /// </summary>
        /// <param name="pushType"></param>
        /// <returns></returns>
        public PushBootstrap SetPushType(MqttClientType mqttClientType)
        {
            PushType = mqttClientType;
            return this;
        }

        /// <summary>
        /// 设置客户端ID
        /// </summary>
        /// <returns></returns>
        public PushBootstrap SetClientId(string ClientId)
        {
            this.connectionConfig.SetOption(MqttClientOptionKey.ClientId, ClientId);
            return this;
        }
        /// <summary>
        /// 设置服务器地址
        /// </summary>
        /// <returns></returns>
        public PushBootstrap SetTcpServer(string tcpServer)
        {
            this.connectionConfig.SetOption(MqttClientOptionKey.tcpServer, tcpServer);
            return this;
        }
        /// <summary>
        /// 设置端口
        /// </summary>
        /// <returns></returns>
        public PushBootstrap SetTcpPort(string tcpPort)
        {
            this.connectionConfig.SetOption(MqttClientOptionKey.tcpPort, tcpPort);
            return this;
        }
        /// <summary>
        /// 设置用户名
        /// </summary>
        /// <returns></returns>
        public PushBootstrap SetMqttUser(string mqttUser)
        {
            this.connectionConfig.SetOption(MqttClientOptionKey.mqttUser, mqttUser);
            return this;
        }
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <returns></returns>
        public PushBootstrap SetMqttPassword(string mqttPassword)
        {
            this.connectionConfig.SetOption(MqttClientOptionKey.mqttPassword, mqttPassword);
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
        /// 创建服务端具体实现
        /// </summary>
        /// <typeparam name="IPushBroker"></typeparam>
        /// <typeparam name="TNotification"></typeparam>
        /// <returns></returns>
        protected override IPushBroker GetDataHandleCommon<IPushBroker, TNotification>()
        {
            switch (PushType)
            {
                case MqttClientType.Client:
                    return new MqttClientBroker(BuildPushConnectionFactory()) as IPushBroker;
            }
            return base.GetDataHandleCommon<IPushBroker, TNotification>();
        }
        /// <summary>
        /// 创建服务端链接
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <returns></returns>
        public  IPushConnectionFactory<TopicNotification> BuildPushConnectionFactory()
        {
            var dataType = typeof(TopicNotification);
            if (IsSubclassOf(typeof(TopicNotification), dataType))
            {
                return new MqttPushFactory();
            }
            return null;
        }

        public override IPushServerFactory<TData> BuildPushServerFactory<TData>()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
