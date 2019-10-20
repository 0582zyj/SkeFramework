using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Connections
{
    /// <summary>
    ///     Wraps the <see cref="IReactor" /> itself inside a <see cref="IConnection" /> object and makes it callable
    ///     directly to end users
    /// </summary>
    public class ReactorConnectionAdapter : IConnection
    {
        private ReactorBase _reactor;

        public ReactorConnectionAdapter(ReactorBase reactor)
        {
            _reactor = reactor;
        }

        public event ReceivedDataCallback Receive
        {
            add { _reactor.OnReceive += value; }
            remove { _reactor.OnReceive -= value; }
        }

        public IMessageEncoder Encoder
        {
            get { return _reactor.Encoder; }
        }

        public IMessageDecoder Decoder
        {
            get { return _reactor.Decoder; }
        }

        public IByteBufAllocator Allocator
        {
            get { return _reactor.Allocator; }
        }

        public DateTimeOffset Created { get; private set; }
        public INode RemoteHost { get; private set; }

        public INode Local
        {
            get { return null; }// _reactor.LocalEndpoint.ToNode(_reactor.Transport); }
        }

        public TimeSpan Timeout { get; private set; }


        public bool WasDisposed { get; private set; }

        public bool Receiving
        {
            get { return _reactor.IsActive; }
        }

        public bool IsOpen()
        {
            return _reactor.IsActive;
        }

        public int Available
        {
            get { throw new NotSupportedException("[Available] is not supported on ReactorConnectionAdapter"); }
        }

        public int MessagesInSendQueue
        {
            get { return 0; }
        }

        public void Configure(IConnectionConfig config)
        {
            _reactor.Configure(config);
        }

        public void Open()
        {
            if (_reactor.IsActive) return;
            _reactor.Start();
        }

        public void BeginReceive()
        {
            Open();
        }

        public void BeginReceive(ReceivedDataCallback callback)
        {
            Receive += callback;
        }

        public void StopReceive()
        {
            Receive += (data, channel) => { };
        }

        public void Close()
        {
            _reactor.Stop();
        }

        public void Send(NetworkData data)
        {
            _reactor.Send(data);
        }

        public void Send(byte[] buffer, int index, int length, INode destination)
        {
            _reactor.Send(buffer, index, length, destination);
        }


        #region IDisposable methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!WasDisposed)
            {
                if (disposing)
                {
                    Close();
                    if (_reactor != null)
                    {
                        ((IDisposable)_reactor).Dispose();
                        _reactor = null;
                    }
                }
            }
            WasDisposed = true;
        }

        #endregion
    }

}
