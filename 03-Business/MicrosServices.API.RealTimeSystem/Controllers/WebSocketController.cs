using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MicrosServices.Helper.Core.RealTimeSystems.VO;
using SkeFramework.Core.ApiCommons.DataUtil;
using SkeFramework.Core.Network.Responses;
using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
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
        public ActionResult<JsonResponses> PreConnect([FromForm] string appId,[FromForm] Guid? websocketId)
        {
            if (websocketId == null) websocketId = Guid.NewGuid();
            WebSocketUtils.Initialization(new WebSocketClientConfig
            {
                Redis = new CSRedis.CSRedisClient(ApplicationConfigUtil.GetAppSeting("WebSocketServer", "CSRedisClient")),
                Servers = ApplicationConfigUtil.GetAppSeting("WebSocketServer", "Servers").Split(",").ToList(), //集群配置
                PathMatch=String.IsNullOrEmpty(appId)?"":appId
            });
            var wsserver = WebSocketUtils.PrevConnectServer(websocketId.Value, this.Ip);
            return new JsonResponses(new ConnectVo()
            {
                Server = wsserver,
                WebsocketId = websocketId
            });
        }

        /// <summary>
        /// 获取订阅列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<JsonResponses> GetChannels()
        {
            List<OnlineChannelVo> onlineChannelVos = WebSocketUtils.GetChanList().ToList();
            return new JsonResponses(onlineChannelVos);
        }

        /// <summary>
        /// 订阅消息频道
        /// </summary>
        /// <param name="websocketId">本地标识，若无则不传，接口会返回，请保存本地重复使用</param>
        /// <param name="channel">消息频道</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> SubscrChannel([FromForm] Guid websocketId, [FromForm] string channel)
        {
            WebSocketUtils.JoinChan(websocketId, channel);
            return JsonResponses.Success;
        }

        /// <summary>
        /// 群发-频道发送
        /// </summary>
        /// <param name="channel">消息频道</param>
        /// <param name="content">发送内容</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<JsonResponses> SendChannelMessage([FromForm] Guid websocketId, [FromForm] string channel, [FromForm] string message)
        {
            WebSocketUtils.SendChannelMessage(websocketId, channel, message);
            return JsonResponses.Success;
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
        public ActionResult<JsonResponses> SendMessage([FromForm] Guid senderWebsocketId, [FromForm] Guid receiveWebsocketId, [FromForm] string message, [FromForm] bool isReceipt = false)
        {
            WebSocketUtils.SendMessage(senderWebsocketId, new[] { receiveWebsocketId }, message, isReceipt);
            return JsonResponses.Success;
        }
    }
}
