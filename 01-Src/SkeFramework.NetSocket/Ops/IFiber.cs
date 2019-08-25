using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Ops.Executors
{
    /// <summary>
    /// 用于轻量级线程和执行的接口
    /// </summary>
    public interface IFiber : IDisposable
    {
        /// <summary>
        /// The internal executor used to execute tasks
        /// </summary>
        IExecutor Executor { get; }

        /// <summary>
        /// Is this Fiber still running?
        /// </summary>
        bool Running { get; }

        bool WasDisposed { get; }

        void Add(Action op);

        /// <summary>
        /// Replaces the current <see cref="Executor"/> with a new <see cref="IEventExecutor"/> instance
        /// </summary>
        /// <param name="executor">The new executor</param>
        void SwapExecutor(IExecutor executor);

        /// <summary>
        /// Shuts down this Fiber within the allotted timeframe
        /// </summary>
        /// <param name="gracePeriod">The amount of time given for currently executing tasks to complete</param>
        void Shutdown(TimeSpan gracePeriod);

        /// <summary>
        /// Shuts down this fiber within the allotted timeframe and provides a task that can be waited on during the interim
        /// </summary>
        /// <param name="gracePeriod">The amount of time given for currently executing tasks to complete</param>
        Task GracefulShutdown(TimeSpan gracePeriod);

        /// <summary>
        /// Performs a hard-stop on the Fiber - no more actions can be executed
        /// </summary>
        void Stop();

        void Dispose(bool isDisposing);

        /// <summary>
        /// Creates a deep clone of this <see cref="IFiber"/> instance
        /// </summary>
        IFiber Clone();
    }
}
