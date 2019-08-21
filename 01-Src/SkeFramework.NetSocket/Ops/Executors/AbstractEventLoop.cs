using System;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Ops.Executors
{
    /// <summary>
    ///     Abstract base class for working with <see cref="IEventLoop" /> instances inside a <see cref="IConnection" />
    /// </summary>
    public abstract class AbstractEventLoop : IEventLoop
    {
        protected AbstractEventLoop(IFiber scheduler)
        {
            Scheduler = scheduler;
        }

        protected IFiber Scheduler { get; }

        public bool AcceptingJobs
        {
            get { return Scheduler.Running; }
        }

        public void Execute(Action op)
        {
            Scheduler.Add(op);
        }

        public Task ExecuteAsync(Action op)
        {
            return Scheduler.Executor.ExecuteAsync(op);
        }

        public void Execute(IList<Action> op)
        {
            foreach (var o in op)
            {
                Scheduler.Add(o);
            }
        }

        public void Execute(Task task)
        {
            Scheduler.Add(task.RunSynchronously);
        }

        public Task ExecuteAsync(IList<Action> op)
        {
            return Scheduler.Executor.ExecuteAsync(op);
        }

        public void Execute(IList<Action> ops, Action<IEnumerable<Action>> remainingOps)
        {
            Scheduler.Executor.Execute(ops, remainingOps);
        }

        public Task ExecuteAsync(IList<Action> ops, Action<IEnumerable<Action>> remainingOps)
        {
            return Scheduler.Executor.ExecuteAsync(ops, remainingOps);
        }

        public void Shutdown()
        {
            Scheduler.Stop();
        }

        public void Shutdown(TimeSpan gracePeriod)
        {
            Scheduler.Shutdown(gracePeriod);
        }

        public Task GracefulShutdown(TimeSpan gracePeriod)
        {
            return Scheduler.GracefulShutdown(gracePeriod);
        }

        public bool InThread(Thread thread)
        {
            return Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId;
        }

        public abstract IExecutor Clone();

        public bool WasDisposed { get; private set; }

        /// <summary>
        ///     Returns a new <see cref="IEventLoop" /> that can be chained after this one
        /// </summary>
        public abstract IExecutor Next();

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool isDisposing)
        {
            if (isDisposing && !WasDisposed)
            {
                WasDisposed = true;
                Scheduler.Dispose();
            }
        }

        #endregion
    }
}