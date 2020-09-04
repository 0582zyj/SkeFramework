using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.WebSocketPush.PushServices;
using SkeFramework.Core.WebSocketPush.PushServices.PushClients;

namespace MicrosServices.API.RealTimeSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        public string Ip => this.Request.Headers["X-Real-IP"].FirstOrDefault() ?? this.Request.HttpContext.Connection.RemoteIpAddress.ToString();

        /// <summary>
        /// 获取websocket分区
        /// </summary>
        /// <param name="websocketId">本地标识，若无则不传，接口会返回新的，请保存本地localStoregy重复使用</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> preConnect([FromForm] Guid? websocketId)
        {
            if (websocketId == null) websocketId = Guid.NewGuid();
            WebSocketHelper.Initialization(new WebSocketSessionOptions
            {
                Redis = new CSRedis.CSRedisClient("127.0.0.1:6379,poolsize=5"),
                Servers = new List<string>() { "localhost:52848" }, //集群配置
            });
            var wsserver = WebSocketHelper.PrevConnectServer(websocketId.Value, this.Ip);
            object  obj= new
            {
                code = 0,
                server = wsserver,
                websocketId = websocketId
            };
            return new JsonResponses(obj);
        }

        /// <summary>
        /// 获取订阅列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object getChannels()
        {
            return new
            {
                code = 0,
                channels = WebSocketHelper.GetChanList().Select(a => new { a.chan, a.online })
            };
        }

        /// <summary>
        /// 订阅消息频道
        /// </summary>
        /// <param name="websocketId">本地标识，若无则不传，接口会返回，请保存本地重复使用</param>
        /// <param name="channel">消息频道</param>
        /// <returns></returns>
        [HttpPost("subscr-channel")]
        public object subscrChannel([FromForm] Guid websocketId, [FromForm] string channel)
        {
            WebSocketHelper.JoinChan(websocketId, channel);
            return new
            {
                code = 0
            };
        }

        /// <summary>
        /// 群发-频道发送
        /// </summary>
        /// <param name="channel">消息频道</param>
        /// <param name="content">发送内容</param>
        /// <returns></returns>
        [HttpPost("send-channelmsg")]
        public object sendChannelmsg([FromForm] Guid websocketId, [FromForm] string channel, [FromForm] string message)
        {
            WebSocketHelper.SendChanMessage(websocketId, channel, message);
            return new
            {
                code = 0
            };
        }
        /// <summary>
        /// 点对点发送
        /// </summary>
        /// <param name="senderWebsocketId">发送者</param>
        /// <param name="receiveWebsocketId">接收者</param>
        /// <param name="message">发送内容</param>
        /// <param name="isReceipt">是否需要回执</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> sendmsg([FromForm] Guid senderWebsocketId, [FromForm] Guid receiveWebsocketId, [FromForm] string message, [FromForm] bool isReceipt = false)
        {
            WebSocketHelper.SendMessage(senderWebsocketId, new[] { receiveWebsocketId }, message, isReceipt);
            return new JsonResponses
            {
                code = 0
            };
        }
    }
}
