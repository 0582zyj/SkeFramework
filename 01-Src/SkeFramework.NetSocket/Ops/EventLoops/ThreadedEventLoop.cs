using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Ops.Executors
{
    /// <summary>
    /// 多线程事件循环
    /// </summary>
    public class ThreadedEventLoop : AbstractEventLoop
    {
        public ThreadedEventLoop(int workerThreads) : base(FiberFactory.CreateFiber(workerThreads))
        {
        }

        public ThreadedEventLoop(IExecutor internalExecutor, int workerThreads)
            : base(FiberFactory.CreateFiber(internalExecutor, workerThreads))
        {
        }

        public ThreadedEventLoop(IFiber scheduler) : base(scheduler)
        {
        }

        public override IExecutor Clone()
        {
            return new ThreadedEventLoop(Scheduler.Clone());
        }

        public override IExecutor Next()
        {
            return new ThreadedEventLoop(Scheduler);
        }
    }
}