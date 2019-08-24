﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Ops;
using SkeFramework.NetSocket.Ops.Executors;

namespace SkeFramework.NetSocket.Net
{
    /// <summary>
    ///     <see cref="Concurrency.IEventLoop" /> implementation intended to be used with Non-blocking I/O (NIO)
    ///     implementations of <see cref="IConnection" />.
    ///     Uses <see cref="IFiber" />s and a fixed size threadpool internally.
    /// </summary>
    public class NetworkEventLoop : ThreadedEventLoop
    {
        public NetworkEventLoop(int workerThreads)
            : base(workerThreads)
        {
        }

        public NetworkEventLoop(IExecutor internalExecutor, int workerThreads)
            : base(internalExecutor, workerThreads)
        {
        }

        public NetworkEventLoop(IFiber scheduler)
            : base(scheduler)
        {
        }

        public ReceivedDataCallback Receive
        {
            get { return _heliosReceive; }
            set
            {
                _internalReceive = value;
                if (value == null)
                    _heliosReceive = null;
                else
                    _heliosReceive = (data, channel) => Execute(() => _internalReceive(data, channel));
            }
        }


        public ConnectionEstablishedCallback Connection
        {
            get { return _heliosConnection; }
            set
            {
                _internalConnectionEstablished = value;
                if (value == null)
                    _heliosConnection = null;
                else
                    _heliosConnection =
                        (address, channel) => Execute(() => _internalConnectionEstablished(address, channel));
            }
        }

        public ConnectionTerminatedCallback Disconnection
        {
            get { return _heliosDisconnection; }
            set
            {
                _internalConnectionTerminated = value;
                if (value == null)
                    _heliosDisconnection = null;
                else
                    _heliosDisconnection =
                        (address, reason) => Execute(() => _internalConnectionTerminated(address, reason));
            }
        }

        public ExceptionCallback Exception { get; private set; }

        public void SetExceptionHandler(ExceptionCallback callback, IConnection connection)
        {
            _owner = connection;
            Exception = callback;
            _heliosException = exception =>
            {
                if (Exception != null)
                    Exception(exception, _owner);
            };
            Scheduler.SwapExecutor(new TryCatchExecutor(_heliosException)); //pipes errors back to the connection object
        }

        #region Clone methods

        public NetworkEventLoop Clone(bool shareFiber = false)
        {
            NetworkEventLoop eventLoop;
            if (shareFiber)
                eventLoop = new NetworkEventLoop(new SharedFiber(Scheduler));
            else
                eventLoop = new NetworkEventLoop(Scheduler.Clone());

            eventLoop.Receive = Receive;
            eventLoop.Connection = Connection;
            eventLoop.Disconnection = Disconnection;

            return eventLoop;
        }

        #endregion

        #region Internal callbacks used by IConnection and IReactor

        /*
         * These are here primarily to prevent the garbage collector from accidentally GCing one of these internal callbacks
         */
        private ReceivedDataCallback _internalReceive;
        private ConnectionEstablishedCallback _internalConnectionEstablished;
        private ConnectionTerminatedCallback _internalConnectionTerminated;

        private ReceivedDataCallback _heliosReceive;
        private ConnectionEstablishedCallback _heliosConnection;
        private ConnectionTerminatedCallback _heliosDisconnection;
        private Action<Exception> _heliosException;

        private IConnection _owner;

        #endregion
    }
}