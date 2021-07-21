using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkeFramework.Core.NetLog;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net.Constants;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.DataFrame;
using SkeFramework.NetSerialPort.Protocols.Response;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Net.Reactor
{
    public abstract class ProxyReactorBase : ReactorBase
    {
        protected Dictionary<string, IConnection> SocketMap = new Dictionary<string, IConnection>();

        protected ProxyReactorBase(INode nodeConfig,
            IMessageEncoder encoder, IMessageDecoder decoder, IByteBufAllocator allocator,
            int bufferSize = NetworkConstants.DEFAULT_BUFFER_SIZE)
            : base(nodeConfig, encoder, decoder, allocator, bufferSize)
        {
            BufferSize = bufferSize;
            networkState = CreateNetworkState(Listener, this.LocalEndpoint, allocator.Buffer(bufferSize), bufferSize);
        }

        /// <summary>
        ///  If true, proxies created for each inbound connection share the parent's thread-pool. If false, each proxy is
        ///  allocated
        ///     its own thread pool.
        ///     Defaults to true.
        /// </summary>
        public bool ProxiesShareFiber { get; protected set; }

        /// <summary>
        /// 处理接收数据[解析+分配链接]
        /// </summary>
        /// <param name="availableData">远程主机原始数据</param>
        /// <param name="networkState">网络请求数据</param>
        protected override void ReceivedData(NetworkData availableData)
        {
            //检查未处理的缓冲区数据是否超时
            networkState.CheckPraseTimeOut();
            networkState.Buffer.WriteBytes(availableData.Buffer, 0, availableData.Length);
            string log = String.Format("{0}:接收数据-{1}-->>{2}", DateTime.Now.ToString("hh:mm:ss"),
                this.Listener.reactorType.ToString(), this.Encoder.ByteEncode(networkState.Buffer.ToArray()));
            LogAgent.Info(log);
            ReactorConnectionAdapter adapter = ((ReactorConnectionAdapter)ConnectionAdapter);
            while (networkState.Buffer.ReadableBytes > 0)
            {
                if (networkState.CheckPraseTimeOut())
                {
                    return;
                }
                byte[] readableBuffer = networkState.Buffer.ToArray();
                FrameBase frame = adapter.ParsingReceivedData(readableBuffer);
                if (frame != null)
                {
                    //触发整条记录的处理
                    IConnection connection = adapter.GetConnection(frame);
                    if (connection != null)
                    {
                        connection.RemoteHost.TaskTag = connection.ControlCode;
                        networkState.RemoteHost = connection.RemoteHost;
                    }
                    else
                    {
                        networkState.RemoteHost = this.LocalEndpoint;
                        networkState.RemoteHost.TaskTag = "none";
                    }
                    if (frame.MatchOffset != 0)
                    {//从缓冲区移除要丢弃的数据
                        byte[] removeByte = networkState.Buffer.ReadBytes(frame.MatchOffset);
                        log = String.Format("{0}:丢弃数据-{1}-->>{2}", DateTime.Now.ToString("hh:mm:ss"), this.Listener.reactorType, this.Encoder.ByteEncode(removeByte));
                        LogAgent.Info(log);
                    }
                    if (frame.FrameBytes != null && frame.FrameBytes.Length > 0)
                    {
                        NetworkState state = CreateNetworkState(networkState.Socket, networkState.RemoteHost);
                        state.RawBuffer = networkState.Buffer.ReadBytes(frame.FrameBytes.Length);
                        this.ReceiveCallback(state);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (networkState.Buffer.WritableBytes == 0)
            {
                log = String.Format("{0}:丢弃数据-{1}-->>{2}", DateTime.Now.ToString("hh:mm:ss"), this.Listener.reactorType, this.Encoder.ByteEncode(networkState.Buffer.ToArray()));
                LogAgent.Info(log);
            }
        }

        /// <summary>
        /// 接收数据回调[协议进程]
        /// </summary>
        /// <param name="receiveState"></param>
        protected void ReceiveCallback(NetworkState receiveState)
        {
            try
            {
                if (receiveState.RemoteHost == null)
                    return;
                var received = receiveState.RawBuffer.Length;
                if (received == 0)
                    return;
                receiveState.Buffer.WriteBytes(receiveState.RawBuffer, 0, received);
                if (SocketMap.ContainsKey(receiveState.RemoteHost.nodeConfig.ToString()))
                {
                    var connection = SocketMap[receiveState.RemoteHost.nodeConfig.ToString()];
                    receiveState.RemoteHost = connection.RemoteHost;
                }
                else
                {
                    RefactorProxyResponseChannel adapter = new RefactorProxyResponseChannel(this, null, receiveState.RemoteHost);
                    SocketMap.Add(adapter.RemoteHost.nodeConfig.ToString(), adapter);
                }

                List<IByteBuf> decoded;
                Decoder.Decode(ConnectionAdapter, receiveState.Buffer, out decoded);
                foreach (var message in decoded)
                {
                    var networkData = NetworkData.Create(receiveState.RemoteHost, message);
                    string log = String.Format("{0}:处理数据-{1}-->>{2}", DateTime.Now.ToString("hh:mm:ss"), this.Listener.reactorType.ToString(), this.Encoder.ByteEncode(networkData.Buffer));
                    LogAgent.Info(log);
                    if (ConnectionAdapter is ReactorConnectionAdapter)
                    {
                        ((ReactorConnectionAdapter)ConnectionAdapter).networkDataDocker.AddNetworkData(networkData);
                        ((EventWaitHandle)((ReactorConnectionAdapter)ConnectionAdapter).protocolEvents[(int)ProtocolEvents.PortReceivedData]).Set();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}