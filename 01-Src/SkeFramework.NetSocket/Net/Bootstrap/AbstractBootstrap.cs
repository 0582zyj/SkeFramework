using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers;
using SkeFramework.NetSocket.Channels;
using SkeFramework.NetSocket.Serialization;

namespace SkeFramework.NetSocket.Net.Bootstrap
{
    /// <summary>
    /// 引导程序抽象实现
    /// </summary>
    public abstract class AbstractBootstrap
    {
        protected AbstractBootstrap()
        {
            Config = new DefaultConnectionConfig();
            Encoder = Encoders.DefaultEncoder;
            Decoder = Encoders.DefaultDecoder;
            Allocator = null;
            Type = TransportType.Tcp;
        }

        protected AbstractBootstrap(AbstractBootstrap other) : this()
        {
            ReceivedData = other.ReceivedData != null ? (ReceivedDataCallback)other.ReceivedData.Clone() : null;
            ConnectionEstablishedCallback = other.ConnectionEstablishedCallback != null
                ? (ConnectionEstablishedCallback)other.ConnectionEstablishedCallback.Clone()
                : null;
            ConnectionTerminatedCallback = other.ConnectionTerminatedCallback != null
                ? (Net.ConnectionTerminatedCallback)other.ConnectionTerminatedCallback.Clone()
                : null;
            ExceptionCallback = other.ExceptionCallback != null
                ? (ExceptionCallback)other.ExceptionCallback.Clone()
                : null;

            foreach (var option in other.Config.Options)
            {
                Config.SetOption(option.Key, option.Value);
            }

            Encoder = other.Encoder;
            Decoder = other.Decoder;
        }

        #region 事件
        protected ReceivedDataCallback ReceivedData { get; set; }

        protected ConnectionEstablishedCallback ConnectionEstablishedCallback { get; set; }

        protected ConnectionTerminatedCallback ConnectionTerminatedCallback { get; set; }

        protected ExceptionCallback ExceptionCallback { get; set; }
        #endregion

        #region 引导属性
        /// <summary>
        /// 配置
        /// </summary>
        protected IConnectionConfig Config { get; set; }

        protected TransportType Type { get; set; }

        protected IMessageDecoder Decoder { get; set; }

        protected IMessageEncoder Encoder { get; set; }

        protected IByteBufAllocator Allocator { get; set; }
        #endregion

        #region 属性注入
        public virtual AbstractBootstrap SetTransport(TransportType type)
        {
            Type = type;
            return this;
        }

        public virtual AbstractBootstrap SetDecoder(IMessageDecoder decoder)
        {
            Decoder = decoder;
            return this;
        }

        public virtual AbstractBootstrap SetEncoder(IMessageEncoder encoder)
        {
            Encoder = encoder;
            return this;
        }

        public virtual AbstractBootstrap SetAllocator(IByteBufAllocator allocator)
        {
            Allocator = allocator;
            return this;
        }

        public virtual AbstractBootstrap SetConfig(IConnectionConfig config)
        {
            Config = config;
            return this;
        }

        public virtual AbstractBootstrap SetOption(string optionKey, object optionValue)
        {
            Config = Config.SetOption(optionKey, optionValue);
            return this;
        }

        public virtual AbstractBootstrap OnReceive(ReceivedDataCallback receivedDataCallback)
        {
            ReceivedData = receivedDataCallback;
            return this;
        }

        public virtual AbstractBootstrap OnConnect(ConnectionEstablishedCallback connectionEstablishedCallback)
        {
            ConnectionEstablishedCallback = connectionEstablishedCallback;
            return this;
        }

        public virtual AbstractBootstrap OnDisconnect(ConnectionTerminatedCallback connectionTerminatedCallback)
        {
            ConnectionTerminatedCallback = connectionTerminatedCallback;
            return this;
        }

        public virtual AbstractBootstrap OnError(ExceptionCallback exceptionCallback)
        {
            ExceptionCallback = exceptionCallback;
            return this;
        }
        #endregion

        public IConnectionFactory Build()
        {
            Validate();
            return BuildInternal();
        }

        public abstract void Validate();

        protected abstract IConnectionFactory BuildInternal();

      
    }
}
