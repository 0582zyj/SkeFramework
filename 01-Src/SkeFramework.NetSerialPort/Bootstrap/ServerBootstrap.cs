using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;

namespace SkeFramework.NetSerialPort.Bootstrap
{
    public class ServerBootstrap : AbstractBootstrap
    {
        public ServerBootstrap()
            : base()
        {
            UseProxies = true;
            WorkersShareFiber(true);
            BufferBytes = NetworkConstants.DEFAULT_BUFFER_SIZE;
            Workers = 2;
        }

        public ServerBootstrap(ServerBootstrap other)
            : base(other)
        {
            UseProxies = other.UseProxies;
            BufferBytes = other.BufferBytes;
            WorkersShareFiber(other.UseSharedFiber);
            Workers = other.Workers;
        }


        protected int Workers { get; set; }

        protected int BufferBytes { get; set; }

        protected bool UseProxies { get; set; }

        protected bool UseSharedFiber { get; set; }

        public ServerBootstrap WorkersShareFiber(bool shareFiber)
        {
            UseSharedFiber = shareFiber;
            SetOption("proxiesShareFiber", UseSharedFiber);
            return this;
        }


        public ServerBootstrap WorkerThreads(int workerThreadCount)
        {
            if (workerThreadCount < 1) throw new ArgumentException("Can't be below 1", "workerThreadCount");
            Workers = workerThreadCount;
            return this;
        }

        public ServerBootstrap BufferSize(int bufferSize)
        {
            if (bufferSize < 1024) throw new ArgumentException("Can't be below 1024", "bufferSize");
            BufferBytes = bufferSize;
            return this;
        }

        public ServerBootstrap WorkersAreProxies(bool useProxies)
        {
            UseProxies = useProxies;
            return this;
        }


        public new ServerBootstrap SetConfig(IConnectionConfig config)
        {
            base.SetConfig(config);
            return this;
        }

        public new ServerBootstrap SetDecoder(IMessageDecoder decoder)
        {
            base.SetDecoder(decoder);
            return this;
        }

        public new ServerBootstrap SetEncoder(IMessageEncoder encoder)
        {
            base.SetEncoder(encoder);
            return this;
        }

        public new ServerBootstrap SetAllocator(IByteBufAllocator allocator)
        {
            base.SetAllocator(allocator);
            return this;
        }

      

        public new ServerBootstrap OnReceive(ReceivedDataCallback receivedDataCallback)
        {
            base.OnReceive(receivedDataCallback);
            return this;
        }


        public new ServerBootstrap SetOption(string optionKey, object optionValue)
        {
            base.SetOption(optionKey, optionValue);
            return this;
        }

        public override void Validate()
        {
            if (1 <= (int)ReactorTypes && (int)ReactorTypes <= 4) throw new ArgumentException("Type must be set");
            if (Workers < 1) throw new ArgumentException("Workers must be at least 1");
            if (BufferBytes < 1024) throw new ArgumentException("BufferSize must be at least 1024");
        }

        protected override IConnectionFactory BuildInternal()
        {
            switch (ReactorTypes)
            {
                case ReactorType.SerialPorts:
                    return new SPServerFactory(this);
                default:
                    throw new InvalidOperationException("This shouldn't happen");
            }
        }

        public new IServerFactory Build()
        {
            return (IServerFactory)BuildInternal();
        }
    }
}