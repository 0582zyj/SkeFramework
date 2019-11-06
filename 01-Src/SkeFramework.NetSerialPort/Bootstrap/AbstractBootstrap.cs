using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Buffers.Allocators.Achieves;
using SkeFramework.NetSerialPort.Buffers.Serialization;
using SkeFramework.NetSerialPort.Net;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Connections;

namespace SkeFramework.NetSerialPort.Bootstrap
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
            Allocator = new DefaultByteBufAllocator();
            ReactorTypes = ReactorType.SerialPorts;
        }

        protected AbstractBootstrap(AbstractBootstrap other)
            : this()
        {
            ReceivedData = other.ReceivedData != null ? (ReceivedDataCallback)other.ReceivedData.Clone() : null;
            foreach (var option in other.Config.Options)
            {
                Config.SetOption(option.Key, option.Value);
            }

            Encoder = other.Encoder;
            Decoder = other.Decoder;

        }

        #region 事件
        protected ReceivedDataCallback ReceivedData { get; set; }
        #endregion

        #region 引导属性
        public ReactorType ReactorTypes { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        protected IConnectionConfig Config { get; set; }

        protected IMessageDecoder Decoder { get; set; }

        protected IMessageEncoder Encoder { get; set; }

        protected IByteBufAllocator Allocator { get; set; }
        #endregion

        #region 属性注入

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
