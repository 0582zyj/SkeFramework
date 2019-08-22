﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Ops
{
    /// <summary>
    ///     Interface used for creating chained eventloops
    ///     for processing network streams / events
    /// </summary>
    public interface IEventLoop : IExecutor, IDisposable
    {
        /// <summary>
        ///     Was this event loop disposed?
        /// </summary>
        bool WasDisposed { get; }

        /// <summary>
        ///     Return the next executor in the chain
        /// </summary>
        IExecutor Next();
    }
}
