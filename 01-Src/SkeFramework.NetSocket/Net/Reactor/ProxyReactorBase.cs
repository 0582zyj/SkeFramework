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
        /// 处理接收数据[解析+分配链接]
        /// </summary>
        /// <param name="availableData">远程主机原始数据</param>
        /// <param name="networkState">网络请求数据</param>
        protected override void ReceivedData(NetworkData availableData)
        {
            //检查未处理的缓冲区数据是否超时
            networkState.CheckPraseTimeOut();
            networkState.Buffer.WriteBytes(availableData.Buffer, 0, availableData.Length);
            LogTemplate("接收数据-{0}-->>{1}", networkState.Buffer.ToArray());
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
                    ////触发整条记录的处理
                    if (frame.MatchOffset != 0)
                    {//从缓冲区移除要丢弃的数据
                        byte[] removeByte = networkState.Buffer.ReadBytes(frame.MatchOffset);
                        LogTemplate("丢弃数据-{0}-->>{1}", removeByte);
                    }
                    if (frame.FrameBytes != null && frame.FrameBytes.Length > 0)
                    {
                        INode remoteHost = availableData.RemoteHost.Clone() as INode;
                        remoteHost.TaskTag = frame.ControlCode;
                        NetworkState state = CreateNetworkState(networkState.Socket, remoteHost);
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
                LogTemplate("丢弃数据-{0}-->>{1}", networkState.Buffer.ToArray());
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
                List<IByteBuf> decoded;
                Decoder.Decode(ConnectionAdapter, receiveState.Buffer, out decoded);
                foreach (var message in decoded)
                {
                    var networkData = NetworkData.Create(receiveState.RemoteHost, message);
                    LogTemplate("处理数据-{0}-->>{1}", networkData.Buffer);
                    if (ConnectionAdapter is ReactorConnectionAdapter)
                    {//合法消息回调到线程进行分发处理
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
        /// <summary>
        /// 模板日志输出
        /// </summary>
        private void LogTemplate(string formactLog,byte[] data)
        {
            string log = String.Format(formactLog, this.Listener.reactorType.ToString(), this.Encoder.ByteEncode(data));
            LogAgent.Info(log);
        }

    }
}