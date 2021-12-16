using SkeFramework.Core.NetLog;
using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Constants;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Listenser.ChannelListensers;
using SkeFramework.Push.Core.Listenser.Interfaces;
using SkeFramework.Push.Core.Services;
using SkeFramework.Push.Core.Services.Workers;
using SkeFramework.Push.Mqtt.DataEntities;
using SkeFramework.Push.Mqtt.DataEntities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Mqtt.Reactor.Managers
{
    /// <summary>
    /// 推送链接工作容器
    /// </summary>
    public class PushConnectionWorkDocker : WorkDocker<TopicNotification>, IChannelListener
    {

        public PushConnectionWorkDocker(IPushBroker<TopicNotification> broker, IPushConnectionFactory<TopicNotification> connectionFactory, IConnectionConfig defaultConfig)
            : base(broker, connectionFactory, defaultConfig)
        {
        }

        /// <summary>
        /// 任务处理
        /// </summary>
        /// <param name="datas">任务数据</param>
        /// <param name="connectionTag">任务id</param>
        public void OnReceivedDataPoint(INotification datas, string connectionTag)
        {
            ServiceWorkerAdapter<TopicNotification> AvailableWorker = this.GetServiceWorkerAdapter(connectionTag);
            if (AvailableWorker != null && AvailableWorker.Connection != null)
            {
                AvailableWorker.QueueNotification(datas as TopicNotification);
                return;
            }
            if(!(datas is TopicNotification))
            {
                return;
            }
            TopicNotification topic = (TopicNotification)datas;
            IConnectionConfig config = new DefaultConnectionConfig();
            config.SetOption(DefaultConfigTypeEumns.ConnectionTag.ToString(), connectionTag);
            config.SetOption(DefaultConfigTypeEumns.ResultData.ToString(), datas);
            this.AddServiceWorkerAdapter(config);
        }
    }
}
