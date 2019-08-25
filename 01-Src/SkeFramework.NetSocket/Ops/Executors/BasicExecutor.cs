using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Channels;
using SkeFramework.NetSocket.DataUtil;

namespace SkeFramework.NetSocket.Ops
{
    /// <summary>
    ///     Basic synchronous executor
    /// </summary>
    public class BasicExecutor : IExecutor
    {
        protected ScheduledValue<bool> AcceptingJobsDeadline;

        public BasicExecutor()
        {
            AcceptingJobsDeadline = new ScheduledValue<bool>(true);
        }

        public bool AcceptingJobs
        {
            get { return AcceptingJobsDeadline.Value; }
        }

        public virtual void Execute(Action op)
        {
            if (!AcceptingJobs) return;

            op();
        }

        public Task ExecuteAsync(Action op)
        {
            return TaskRunner.Run(op);
        }

        public virtual void Execute(IList<Action> op)
        {
            Execute(op, null);
        }

        public void Execute(Task task)
        {
            if (!AcceptingJobs) return;

            task.RunSynchronously();
        }

        public Task ExecuteAsync(IList<Action> op)
        {
            return ExecuteAsync(op, null);
        }

        public virtual void Execute(IList<Action> ops, Action<IEnumerable<Action>> remainingOps)
        {
            for (var i = 0; i < ops.Count; i++)
            {
                if (!AcceptingJobs)
                {
                    //remainingOps.NotNull(obj => remainingOps(ops.Skip(i + 1)));
                    break;
                }

                ops[i]();
            }
        }

        public Task ExecuteAsync(IList<Action> ops, Action<IEnumerable<Action>> remainingOps)
        {
            return TaskRunner.Run(() =>
            {
                for (var i = 0; i < ops.Count; i++)
                {
                    if (!AcceptingJobs)
                    {
                        //remainingOps.NotNull(obj => remainingOps(ops.Skip(i + 1)));
                        break;
                    }

                    ops[i]();
                }
            });
        }

        public virtual void Shutdown()
        {
            AcceptingJobsDeadline.Value = false;
        }

        public virtual void Shutdown(TimeSpan gracePeriod)
        {
            AcceptingJobsDeadline.Schedule(false, gracePeriod);
        }

        public Task GracefulShutdown(TimeSpan gracePeriod)
        {
            Shutdown(gracePeriod);
            return TaskRunner.Delay(gracePeriod);
        }

        public bool InThread(Thread thread)
        {
            return Thread.CurrentThread.ManagedThreadId == thread.ManagedThreadId;
        }

        public IExecutor Clone()
        {
            return new BasicExecutor();
        }
    }
}