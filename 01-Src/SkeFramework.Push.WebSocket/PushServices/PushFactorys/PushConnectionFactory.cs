using SkeFramework.Push.Core.Bootstrap;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.WebSocket.PushServices.PushClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.WebSocket.PushServices.PushFactorys
{

    public class PushConnectionFactory : IPushConnectionFactory
    {
        public IPushConnection<INotification> Create()
        {
            IPushConnection<INotification> client = new PushClientConnection();
            return client;
        }
    }
}
