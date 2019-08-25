﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Channels;
using SkeFramework.NetSocket.DataUtil;
using SkeFramework.NetSocket.Ops;
using SkeFramework.NetSocket.Ops.Executors;

namespace SkeFramework.NetSocket.Net
{
    /// <summary>
    ///     A shared <see cref="IFiber" /> instance that avoids disposing the original Fiber in the event of a shutdown
    /// </summary>
    public class SharedFiber : IFiber
    {
        private readonly IFiber _sharedFiber;

        private volatile Deadline _gracefulShutdownDeadline = Deadline.Never;

        public SharedFiber(IFiber sharedFiber)
        {
            _sharedFiber = sharedFiber;
        }

        public IExecutor Executor { get; private set; }

        public bool Running
        {
            get { return _gracefulShutdownDeadline.HasTimeLeft; }
        }

        public bool WasDisposed { get; private set; }

        public void Add(Action op)
        {
            if (_gracefulShutdownDeadline.HasTimeLeft)
                _sharedFiber.Add(op);
        }

        public void SwapExecutor(IExecutor executor)
        {
            //no-op
        }

        public void Shutdown(TimeSpan gracePeriod)
        {
            _gracefulShutdownDeadline = Deadline.Now + gracePeriod;
        }

        public Task GracefulShutdown(TimeSpan gracePeriod)
        {
            Shutdown(gracePeriod);
            return TaskRunner.Delay(gracePeriod);
        }

        public void Stop()
        {
            _gracefulShutdownDeadline = Deadline.Now;
        }

        public IFiber Clone()
        {
            return new SharedFiber(_sharedFiber);
        }

        #region IDisposable members

        public void Dispose(bool isDisposing)
        {
            if (!WasDisposed)
            {
                if (isDisposing)
                {
                    Stop();
                }
            }

            WasDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
