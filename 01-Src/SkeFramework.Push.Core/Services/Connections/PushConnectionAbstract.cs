using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Listenser.ChannelListensers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Services.Connections
{
    /// <summary>
    /// 客户端推送反应堆
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public abstract class PushConnectionAbstract<TNotification> : DefaultChannelPromise, IPushConnection<TNotification>
        where TNotification : class, INotification
    {
        public event ReceivedDataCallback<TNotification> OnReceivedDataCallback;
        /// <summary>
        /// 推送反应堆
        /// </summary>
        protected readonly IPushBroker<TNotification> innerPushBroker;
        /// <summary>
        /// 推送链接标识
        /// </summary>
        protected readonly string innerConnectionTag;



        public PushConnectionAbstract(IPushBroker<TNotification> pushBroker, string connectionTag)
        {
            innerPushBroker = pushBroker;
            innerConnectionTag = connectionTag;
        }

        /// <summary>
        /// 数据发送
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public abstract Task Send(TNotification notification);
        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public override bool OnReceivedDataPoint(INotification datas, string taskId)
        {
            bool result = BeginReceiveInternal(datas, taskId);
            if (!result)
                return false;
            result= base.OnReceivedDataPoint(datas, taskId);
            StopReceiveInternal(datas, taskId);
            if (!result)
                return false;
            OnReceivedDataCallback?.Invoke(datas as TNotification, this);
            return result;
        }
        /// <summary>
        /// 获取连接标识
        /// </summary>
        /// <returns></returns>
        public virtual string GetTag()
        {
            return this.innerConnectionTag;
        }

        /// <summary>
        /// 开始接受
        /// </summary>
        public abstract bool BeginReceiveInternal(INotification datas, string taskId);
        /// <summary>
        /// 结束接受
        /// </summary>
        public abstract bool StopReceiveInternal(INotification datas, string taskId);
    }
}
