using MQTTnet.Core.Client;
using MQTTnet.Core.Packets;
using MQTTnet.Core.Protocol;
using SkeFramework.Push.Mqtt.DataEntities;
using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Mqtt.Reactor.Connection.Abstracts
{
    /// <summary>
    /// 推送抽象实现
    /// </summary>
    public class PushConnectionProxy : PushConnectionAbstract<TopicNotification>, IPushConnection<TopicNotification>
    {

        public PushConnectionProxy(IPushBroker<TopicNotification> pushBroker, string connectionTag) : base(pushBroker, connectionTag)
        {
        }



        public override Task Send(TopicNotification notification)
        {
            return null;
        }

        /// <summary>
        /// 消息处理开始
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="controlerId"></param>
        public override bool BeginReceiveInternal(INotification datas, string taskId)
        {
            return true;
        }
        /// <summary>
        /// 消息处理结束
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="controlerId"></param>
        public override bool StopReceiveInternal(INotification datas, string taskId)
        {
            return true;
        }
     
    }
}