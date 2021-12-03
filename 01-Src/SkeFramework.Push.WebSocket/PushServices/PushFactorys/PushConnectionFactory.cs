using SkeFramework.Core.Push.Interfaces;
using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Bootstrap.Factorys;
using SkeFramework.Push.Core.Configs;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services;
using SkeFramework.Push.Core.Services.Brokers;
using SkeFramework.Push.WebSocket.DataEntities;
using SkeFramework.Push.WebSocket.PushServices.PushClients;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.WebSocket.PushServices.PushFactorys
{

    public class PushConnectionFactory : PushServerFactoryBase<WebSocketServer, WebSocketNotifications>
    {

        protected override PushBroker<WebSocketServer, WebSocketNotifications> NewPushBrokerInternal(IConnectionConfig connectionConfig)
        {
            PushBroker<WebSocketServer,WebSocketNotifications> pushBroker = new WebSocketPushBroker(this);
            return pushBroker;
        }

      

        protected override IPushConnection<WebSocketNotifications> NewPushConnectionInternal(IPushBroker<WebSocketNotifications> pushBroker, IConnectionConfig connectionConfig)
        {
            return new PushClientConnection();
        }



    }
}
