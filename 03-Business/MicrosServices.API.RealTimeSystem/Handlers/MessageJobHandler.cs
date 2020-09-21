using MicrosServices.API.RealTimeSystem.DataUtil;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common.RealTimeSystem;
using MicrosServices.Entities.Constants;
using SkeFramework.Core.ApiCommons.DataUtil;
using SkeFramework.Core.Common.Collections;
using SkeFramework.Core.WebSocketPush.DataEntities.DataCommons;
using SkeFramework.Core.WebSocketPush.PushServices;
using SkeFramework.Core.WebSocketPush.PushServices.PushClients;
using SkeFramework.Schedule.NetJob.DataAttributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicrosServices.API.RealTimeSystem.Handlers
{
    [JobAttribute]
    public class MessageJobHandler
    {
        public const int DefaultTimeOutSecond = 0;

        [ScheduleAttribute("MessageJob", "*/1", RunImmediately = true)]
        public void MessageJob(CancellationToken token)
        {
            Trace.WriteLine (DateTime.Now.ToString() + " test1 start");
            string appId = ApplicationConfigUtil.GetAppSeting("WebSocketServer", "WsPath");
            WebSocketProxyAgent.Initialization(ApplicationConfigUtil.GetAppSeting("WebSocketServer", "CSRedisClient"),
               appId);
           
            int Status =(int) MessageStatusEumns.Ready;
            List<string> AppIdList = new List<string>() { appId };
            long Count = DataHandleManager.Instance().RtMessageHandle.CountRtMessage(Status, DefaultTimeOutSecond, AppIdList);
            if (Count > 0)
            {
                List<RtMessage> MessageList = DataHandleManager.Instance().RtMessageHandle.GetRtMessageList(Status, DefaultTimeOutSecond, AppIdList);
                if (!CollectionUtils.IsEmpty(MessageList))
                {
                    foreach (var message in MessageList)
                    {
                        string clientRedisKey = RedisUtil.GetUserIdRedisKey(message.AppId, message.UserId);
                        string SessionId = RedisUtil.GetWebSocketSessionID(clientRedisKey);
                        List<Guid> receiveList = new List<Guid>() { new Guid(SessionId) };
                        WebSocketNotifications notifications = new WebSocketNotifications()
                        {
                            SenderSessionId = Guid.Empty,
                            ReceiveSessionIds = receiveList,
                            Message = message.Message,
                            Receipt = true,
                            NotificationTag = message.id
                        };
                        WebSocketProxyAgent.SendMessage(notifications);
                    }
                }
            }
            Trace.WriteLine(DateTime.Now.ToString() + " test1 end");
        }
    }
}
