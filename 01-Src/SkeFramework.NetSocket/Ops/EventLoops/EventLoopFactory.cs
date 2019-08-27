using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Net;
using SkeFramework.NetSocket.Ops.Executors;

namespace SkeFramework.NetSocket.Ops.EventLoops
{
    public static class EventLoopFactory
    {
        public static IEventLoop CreateThreadedEventLoop(int defaultSize = 2, IExecutor internalExecutor = null)
        {
            return new ThreadedEventLoop(internalExecutor, defaultSize);
        }

        public static NetworkEventLoop CreateNetworkEventLoop(int defaultSize = 2, IExecutor internalExecutor = null)
        {
            return new NetworkEventLoop(internalExecutor, defaultSize);
        }
    }
}
