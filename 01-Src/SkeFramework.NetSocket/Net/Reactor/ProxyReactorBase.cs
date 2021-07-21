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
using SkeFramework.NetSerialPort.Protocols.Requests;
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
        protected override void ReceivedData(NetworkData availableData, NetworkState networkState)
        {
       
            byte[] readableBuffer = availableData.GetReadableBuffer();
            ReactorConnectionAdapter adapter = ((ReactorConnectionAdapter)ConnectionAdapter);
            FrameBase frame = adapter.ParsingReceivedData(readableBuffer);
            if (frame != null)
            {
                //触发整条记录的处理
                INode node = null;
                IConnection connection = adapter.GetConnection(frame);
                if (connection != null)
                {
                    connection.RemoteHost.TaskTag = connection.ControlCode;
                    node = connection.RemoteHost;
                }
                else
                {
                    node = this.LocalEndpoint;
                    node.TaskTag = "none";
                }
                if (networkState == null)
                {
                    networkState = CreateNetworkState(Listener, node);
                }
                networkState.RawBuffer = frame.FrameBytes;
                this.ReceiveCallback(networkState);
            }
            else
            {
                string log = String.Format("{0}:丢弃数据-{1}-->>{2}", DateTime.Now.ToString("hh:mm:ss"), this.Listener.reactorType.ToString(), this.Encoder.ByteEncode(readableBuffer));
                LogAgent.Info(log);
            }
        }

        /// <summary>
        /// 接收数据回调[协议进程]
        /// </summary>
        /// <param name="receiveState"></param>
        protected void ReceiveCallback(NetworkState receiveState, int receiveCount = 0)
        {
            try
            {
                var received = receiveCount == 0 ? receiveState.RawBuffer.Length : receiveCount;
                if (received == 0)
                {
                    return;
                }
                receiveState.Buffer.WriteBytes(receiveState.RawBuffer, 0, received);
                INode node = receiveState.RemoteHost;
                if (SocketMap.ContainsKey(receiveState.RemoteHost.nodeConfig.ToString()))
                {
                    var connection = SocketMap[receiveState.RemoteHost.nodeConfig.ToString()];
                    node = connection.RemoteHost;
                }
                else
                {
                    RefactorProxyResponseChannel adapter = new RefactorProxyResponseChannel(this, null, node);
                    SocketMap.Add(adapter.RemoteHost.nodeConfig.ToString(), adapter);
                }

                List<IByteBuf> decoded;
                Decoder.Decode(ConnectionAdapter, receiveState.Buffer, out decoded);
                foreach (var message in decoded)
                {
                    var networkData = NetworkData.Create(receiveState.RemoteHost, message);
                    string log = String.Format("{0}:接收数据-{1}-->>{2}", DateTime.Now.ToString("hh:mm:ss"), this.Listener.reactorType.ToString(), this.Encoder.ByteEncode(networkData.Buffer));
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
                LogAgent.Error(ex.ToString());
            }
        }
    }
}