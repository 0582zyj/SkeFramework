using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using SkeFramework.Core.WebSocketPush.PushServices.PushClients;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.PushServices
{
    /// <summary>
    /// WebSocket代理类
    /// </summary>
    public static class WebSocketProxyAgent
    {
        #region 链接配置
        /// <summary>
        /// 点对点发送
        /// </summary>
        static WebSocketClient _instance;
        public static WebSocketClient SingleInstance => _instance ?? throw new Exception("使用前请初始化 Initialization(...);");
        /// <summary>
        /// 订阅发送
        /// </summary>
        static WebSocketChannelClient _channelInstance;
        public static WebSocketChannelClient ChannelInstance => _channelInstance ?? throw new Exception("使用前请初始化 Initialization(...);");
        
        /// <summary>
        /// 初始化单点发送
        /// </summary>
        /// <param name="options"></param>
        public static void Initialization(WebSocketClientConfig options)
        {
            _instance = new WebSocketClient(options);
        }

        /// <summary>
        /// 初始化订阅发送
        /// </summary>
        /// <param name="options"></param>
        public static void InitializationChannel(WebSocketClientConfig options)
        {
            _channelInstance = new WebSocketChannelClient(options);
        }
        #endregion

        /// <summary>
        /// ImServer 连接前的负载、授权，返回 ws 目标地址，使用该地址连接 websocket 服务端
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="clientMetaData">客户端相关信息，比如ip</param>
        /// <returns>websocket 地址：ws://xxxx/ws?token=xxx</returns>
        public static string PrevConnectServer(Guid clientId, string clientMetaData) => SingleInstance.PrevConnectServer(clientId, clientMetaData);
        /// <summary>
        /// 向指定的多个客户端id发送消息
        /// </summary>
        /// <param name="senderClientId">发送者的客户端id</param>
        /// <param name="receiveClientId">接收者的客户端id</param>
        /// <param name="message">消息</param>
        /// <param name="receipt">是否回执</param>
        public static void SendMessage(Guid senderClientId, IEnumerable<Guid> receiveClientId, string message, bool receipt = false) =>
            SingleInstance.SendMessage(senderClientId, receiveClientId, message, receipt);

        /// <summary>
        /// 获取所在线客户端id
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Guid> GetClientListByOnline() => SingleInstance.GetClientListByOnline();

        /// <summary>
        /// 判断客户端是否在线
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public static bool HasOnline(Guid clientId) => SingleInstance.HasOnline(clientId);

        /// <summary>
        /// 事件订阅
        /// </summary>
        /// <param name="online">上线</param>
        /// <param name="offline">下线</param>
        public static void EventBus(
            Action<TokenValue> online,
            Action<TokenValue> offline) => SingleInstance.EventSubscribe(online, offline);

        #region 订阅

        /// <summary>
        /// 加入订阅
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="chan">订阅名称</param>
        public static void SubscribeChannel(Guid clientId, string chan) => ChannelInstance.SubscribeChannel(clientId, chan);
        /// <summary>
        /// 离开订阅
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="chans">订阅名称</param>
        public static void UnSubscribeChannel(Guid clientId, params string[] chans) => ChannelInstance.UnSubscribeChannel(clientId, chans);
        /// <summary>
        /// 获取群聊所有客户端id（测试）
        /// </summary>
        /// <param name="chan">订阅名称</param>
        /// <returns></returns>
        public static Guid[] GetChannelClientList(string chan) => ChannelInstance.GetChannelClientList(chan);
        /// <summary>
        /// 清理群聊的离线客户端（测试）
        /// </summary>
        /// <param name="chan">订阅名称</param>
        public static void ClearChannelClient(string chan) => ChannelInstance.ClearChannelClient(chan);
        /// <summary>
        /// 获取所有订阅和在线人数
        /// </summary>
        /// <returns>订阅名和在线人数</returns>
        public static IEnumerable<OnlineChannelVo> GetChannelList() => ChannelInstance.GetChannelList();
        /// <summary>
        /// 获取用户参与的所有订阅
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <returns></returns>
        public static string[] GetChanListByClientId(Guid clientId) => ChannelInstance.GetChanListByClientId(clientId);
        /// <summary>
        /// 获取订阅的在线人数
        /// </summary>
        /// <param name="chan">订阅名称</param>
        /// <returns>在线人数</returns>
        public static long GetChannelOnline(string chan) => ChannelInstance.GetChannelOnline(chan);

        /// <summary>
        /// 发送订阅消息，所有在线的用户将收到消息
        /// </summary>
        /// <param name="senderClientId">发送者的客户端id</param>
        /// <param name="chan">订阅名称</param>
        /// <param name="message">消息</param>
        public static void SendChannelMessage(Guid senderClientId, string chan, string message) => ChannelInstance.SendChanMessage(senderClientId, chan, message);
        #endregion
    }
}
