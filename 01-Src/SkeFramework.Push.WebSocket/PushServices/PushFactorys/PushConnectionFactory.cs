using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Bootstrap.Factorys;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services;
using SkeFramework.Push.Core.Services.Brokers;
using SkeFramework.Push.WebSocket.DataEntities;
using SkeFramework.Push.WebSocket.PushServices.PushClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.WebSocket.PushServices.PushFactorys
{

    public class PushConnectionFactory : PushServerFactoryBase<WebSocketNotifications>
    {

        protected override PushBroker<WebSocketNotifications> NewPushBrokerInternal(IConnectionConfig connectionConfig)
        {
            PushBroker<WebSocketNotifications> pushBroker = new WebSocketPushBroker(this);
            return pushBroker;
        }

        protected override IPushConnection<INotification> NewPushConnectionInternal()
        {
            IPushConnection<INotification> client = new PushClientConnection();
            return client;
        }
    }
}
