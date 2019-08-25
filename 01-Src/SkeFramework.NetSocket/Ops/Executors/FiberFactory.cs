using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Ops.Executors
{
    /// <summary>
    ///     Factory class for creating Fiber instances
    /// </summary>
    public static class FiberFactory
    {
        internal static IFiber CreateFiber(IExecutor internalExecutor, int workerThreads)
        {
            throw new NotImplementedException();
        }

        internal static IFiber CreateFiber(int workerThreads)
        {
            throw new NotImplementedException();
        }
    }
}