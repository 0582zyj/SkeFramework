using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Ops
{
    /// <summary>
    ///  用于执行命令和操作的接口
    /// </summary>
    public interface IExecutor
    {
        bool AcceptingJobs { get; }
        /// <summary>
        /// 执行一个操作
        /// </summary>
        /// <param name="op"></param>
        void Execute(Action op);
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        Task ExecuteAsync(Action op);
        /// <summary>
        /// 执行一系列操作
        /// </summary>
        /// <param name="op"></param>
        void Execute(IList<Action> op);
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        Task ExecuteAsync(IList<Action> op);
        /// <summary>
        /// 执行一个任务
        /// </summary>
        /// <param name="task"></param>
        void Execute(Task task);
    

        /// <summary>
        ///     Process a queue of tasks - if the IExecutor is shut down before
        ///     it has a chance to complete its queue, all of the remaining jobs
        ///     will be passed to an optional callback <see cref="remainingOps" />
        /// </summary>
        /// <param name="ops">The queue of actions to execute</param>
        /// <param name="remainingOps">
        ///     OPTIONAL. Can be null. Callback function for placing any jobs that couldn't be run
        ///     due to an exception or shutdown.
        /// </param>
        void Execute(IList<Action> ops, Action<IEnumerable<Action>> remainingOps);

        Task ExecuteAsync(IList<Action> ops, Action<IEnumerable<Action>> remainingOps);

        /// <summary>
        /// 立即关闭任务
        /// </summary>
        void Shutdown();

        /// <summary>
        ///  延迟一段时间关闭任务
        /// </summary>
        void Shutdown(TimeSpan gracePeriod);

        /// <summary>
        ///     Gracefully shuts down the executor - attempts to drain all of the remaining actions before time expires
        /// </summary>
        /// <param name="gracePeriod">The amount of time <see cref="IExecutor" /> is given to execute any remaining tasks</param>
        /// <returns>
        ///     A task with a status result of "success" as long as no unhandled exceptions are thrown by the time the
        ///     executor is terminated
        /// </returns>
        Task GracefulShutdown(TimeSpan gracePeriod);

        /// <summary>
        /// 检查该任务是否在给定线程中
        /// </summary>
        bool InThread(Thread thread);

        /// <summary>
        /// 
        /// </summary>
        IExecutor Clone();
    }
}
