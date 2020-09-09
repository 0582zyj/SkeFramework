using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataEntities
{
   public class RedisKey
    {
        /// <summary>
        /// WS前缀
        /// </summary>
        public static string ws_prefix = "ske.ws";
        /// <summary>
        /// 客户端认证Token
        /// </summary>
        public static string ws_connect_token = "client:token";
        /// <summary>
        /// 服务端
        /// </summary>
        public static string ws_server = "server";
        /// <summary>
        /// 在线服务端
        /// </summary>
        public static string ws_server_online = "server:online";
        /// <summary>
        /// 在线客户端
        /// </summary>
        public static string ws_client_online = "client:online";
        /// <summary>
        /// 上线事件
        /// </summary>
        public static string ws_event_online = "online";
        /// <summary>
        /// 下线事件
        /// </summary>
        public static string ws_event_offline = "offline";
        /// <summary>
        /// 发布通道
        /// </summary>
        public static string ws_publish_channel = "publish:channel";
        /// <summary>
        /// 订阅通道
        /// </summary>
        public static string ws_subscribe_channel = "subscribe:channel";
        /// <summary>
        /// 订阅列表
        /// </summary>
        public static string ws_channel_list = "channel:list";
        /// <summary>
        /// 订阅用户
        /// </summary>
        public static string ws_channel_client = "channel:client";
    }
}
