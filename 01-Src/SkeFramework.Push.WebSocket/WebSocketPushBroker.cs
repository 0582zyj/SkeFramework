using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services.Brokers;
using SkeFramework.Push.WebSocket.Constants;
using SkeFramework.Push.WebSocket.DataEntities;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.WebSocket
{
    /// <summary>
    /// WebSocker服务端推送
    /// </summary>
    public class WebSocketPushBroker<TNotification> : PushBroker<TNotification>, IPushBroker<TNotification>
        where TNotification : INotification
    {
        /// <summary>
        /// 服务端类
        /// </summary>
        private SuperWebSocket.WebSocketServer Websocket;
        /// <summary>
        /// 启动参数
        /// </summary>
        private WebSocketParam SocketParam;

        public WebSocketPushBroker(IPushConnectionFactory<TNotification> connectionFactory):base(connectionFactory)
        {
        }

        #region 启动和关闭
        /// <summary>
        /// 启动参数设置
        /// </summary>
        /// <param name="connectionConfig"></param>
        public override void SetupParamOptions(IConnectionConfig connectionConfig)
        {
            if (connectionConfig.Options.Count > 0)
            {
                SocketParam = new WebSocketParam();
                if (connectionConfig.HasOption(WebSocketParamEumns.IsUseCertificate.ToString()))
                {
                    SocketParam.IsUseCertificate = Convert.ToBoolean(connectionConfig.GetOption(WebSocketParamEumns.IsUseCertificate.ToString()));
                }
                else if (connectionConfig.HasOption(WebSocketParamEumns.Port.ToString()))
                {
                    SocketParam.Port = Convert.ToInt32(connectionConfig.GetOption(WebSocketParamEumns.Port.ToString()));
                }
                else if (connectionConfig.HasOption(WebSocketParamEumns.ServerName.ToString()))
                {
                    SocketParam.ServerName = Convert.ToString(connectionConfig.GetOption(WebSocketParamEumns.ServerName.ToString()));
                }
                else if (connectionConfig.HasOption(WebSocketParamEumns.ServerSecurity.ToString()))
                {
                    SocketParam.ServerSecurity = Convert.ToString(connectionConfig.GetOption(WebSocketParamEumns.ServerSecurity.ToString()));
                }
                else if (connectionConfig.HasOption(WebSocketParamEumns.ServerStoreName.ToString()))
                {
                    SocketParam.ServerStoreName = Convert.ToString(connectionConfig.GetOption(WebSocketParamEumns.ServerStoreName.ToString()));
                }
                else if (connectionConfig.HasOption(WebSocketParamEumns.ServerThumbprint.ToString()))
                {
                    SocketParam.ServerThumbprint = Convert.ToString(connectionConfig.GetOption(WebSocketParamEumns.ServerThumbprint.ToString()));
                }
            }
        }
        /// <summary>
        /// 启动WebSocket
        /// </summary>
        protected override void PushServerStart()
        {
            try
            {
                if (SocketParam == null)
                    return;
                this.Websocket = new WebSocketServer();
                bool IsSetup = false;
                if (SocketParam.IsUseCertificate)
                {
                    IsSetup = this.Websocket.Setup(new RootConfig(),
                         new ServerConfig
                         {
                             Name = SocketParam.ServerName,
                             MaxConnectionNumber = 1000,
                             Mode = SocketMode.Tcp,
                             Port = SocketParam.Port, //80,
                             ClearIdleSession = false,
                             ClearIdleSessionInterval = 86400,
                             ListenBacklog = 1000,
                             ReceiveBufferSize = 1024,
                             SendBufferSize = 1024,
                             KeepAliveInterval = 1,
                             KeepAliveTime = 55,
                             Security = SocketParam.ServerSecurity,
                             SyncSend = false,
                             Certificate = new CertificateConfig
                             {
                                 StoreName = SocketParam.ServerStoreName,
                                 StoreLocation = System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                                 Thumbprint = SocketParam.ServerThumbprint
                             }
                         }, new SuperSocket.SocketEngine.SocketServerFactory());
                }
                else
                {
                    IsSetup = Websocket.Setup(new RootConfig(), new ServerConfig
                    {
                        Name = SocketParam.ServerName,
                        MaxConnectionNumber = 1000,
                        Port = SocketParam.Port,
                        SyncSend = false,
                        ClearIdleSession = false,
                        ClearIdleSessionInterval = 86400,
                    });
                }
                if (IsSetup == false)
                {
                    LogAgent.Info(Websocket.Name + " Failed to setup!");
                    return;
                }
                LogAgent.Info(Websocket.Name + " Setup Success...!");
                this.Websocket.NewSessionConnected += WebSocket_NewSessionConnected;
                this.Websocket.NewMessageReceived += WebSocket_NewMessageReceived;
                this.Websocket.SessionClosed += WebSocket_SessionClosed;
                if (this.Websocket.Start() == false)
                {
                    LogAgent.Info(Websocket.Name + " Failed to start!");
                    return;
                }
                LogAgent.Info("Server Listen at " + this.Websocket.Listeners[0].EndPoint.Address.ToString());
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 停止WebSocket
        /// </summary>
        protected override void PushServerStop()
        {
            this.Websocket.Stop();
        }
        #endregion

        /// <summary>
        /// 客户端关闭触发事件
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        private void WebSocket_SessionClosed(WebSocketSession session, CloseReason value)
        {
            try
            {
                LogAgent.Info(Websocket.Name + string.Format(" Session Close:{0}, Path:{1}, IP:{2}", value.ToString(), session.Path, session.RemoteEndPoint));
            }
            catch (Exception e)
            {
                LogAgent.Info(string.Format("{0} Websocket_SessionClosed()", e.ToString()));
            }
        }
        /// <summary>
        /// 新消息收到事件
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        private void WebSocket_NewMessageReceived(WebSocketSession session, string value)
        {
            try
            {
                LogAgent.Info(Websocket.Name + " ClientIP:" + session.RemoteEndPoint + "Receive:" + value.ToString());
            }
            catch (Exception e)
            {
                LogAgent.Info(string.Format("{0} Websocket_NewMessageReceived()", e.ToString()));
            }
        }
        /// <summary>
        /// 客户端新链接事件
        /// </summary>
        /// <param name="session"></param>
        private void WebSocket_NewSessionConnected(WebSocketSession session)
        {
            try
            {
                LogAgent.Info(string.Format(Websocket.Name + " New Session Connected:{0}, Path:{1}, Host:{2}, IP:{3}",
                    session.SessionID.ToString(), session.Path, session.Host, session.RemoteEndPoint));
            }
            catch (Exception e)
            {
                LogAgent.Info(string.Format("{0} Websocket_NewSessionConnected()", e.ToString()));
            }
        }

       
    }
}
