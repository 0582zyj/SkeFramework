using SkeFramework.NetSerialPort.Topology;
using SkeFramework.NetSerialPort.Topology.Nodes;
using SkeFramework.NetSerialPort.Topology.ExtendNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Net.Constants;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.Core.NetLog;
using SkeFramework.NetSerialPort.Protocols.DataFrame;

namespace SkeFramework.NetSerialPort.Net.Udp
{
    /// <summary>
    /// UDP协议通信
    /// </summary>
    public class UdpProxyReactor : ProxyReactorBase
    {
        /// <summary>
        /// 
        /// </summary>
        private Socket ListenerSocket;
        /// <summary>
        /// 当前监听点
        /// </summary>
        protected EndPoint LocalEndPoint;
        /// <summary>
        /// 远程监听点
        /// </summary>
        protected EndPoint RemoteEndPoint;
        /// <summary>
        /// 是否打开
        /// </summary>
        public override bool IsActive { get; protected set; }
        /// <summary>
        /// 是否正在解析
        /// </summary>
        public override bool IsParsing { get; protected set; }


        public UdpProxyReactor(INode listener,
            IMessageEncoder encoder, IMessageDecoder decoder, IByteBufAllocator allocator,
            int bufferSize = NetworkConstants.DEFAULT_BUFFER_SIZE)
            : base(listener, encoder, decoder, allocator,
                bufferSize)
        {
            UdpNodeConfig nodeConfig = listener.ToEndPoint<UdpNodeConfig>();
            LocalEndPoint = new IPEndPoint(IPAddress.Parse(nodeConfig.LocalAddress), nodeConfig.LocalPort);
            RemoteEndPoint = new IPEndPoint(IPAddress.Any, nodeConfig.LocalPort);
            ListenerSocket = new Socket(LocalEndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        }

        /// <summary>
        /// 默认配置
        /// </summary>
        /// <param name="config"></param>
        public override void Configure(IConnectionConfig config)
        {
            if (config.HasOption<int>("receiveBufferSize"))
            {
                ListenerSocket.ReceiveBufferSize = config.GetOption<int>("receiveBufferSize");
                this.BufferSize = ListenerSocket.ReceiveBufferSize;
            }
            if (config.HasOption<int>("sendBufferSize"))
                ListenerSocket.SendBufferSize = config.GetOption<int>("sendBufferSize");
            if (config.HasOption<bool>("reuseAddress"))
                ListenerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress,
                    config.GetOption<bool>("reuseAddress"));
            if (config.HasOption<bool>("keepAlive"))
                ListenerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive,
                    config.GetOption<bool>("keepAlive"));
            if (config.HasOption<bool>("proxiesShareFiber"))
                ProxiesShareFiber = config.GetOption<bool>("proxiesShareFiber");
            else
                ProxiesShareFiber = true;
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        protected override void StartInternal()
        {
            IsActive = true;
            NetworkState receiveState = CreateNetworkState(Listener, Node.Empty());
            if (!SocketMap.ContainsKey(this.LocalEndpoint.nodeConfig.ToString()))
            {
                RefactorRequestChannel adapter;
                adapter = new RefactorProxyRequestChannel(this, this.LocalEndpoint, "none");
                SocketMap.Add(this.LocalEndpoint.nodeConfig.ToString(), adapter);
            }
            IsActive = true;
            ListenerSocket.Bind(LocalEndPoint);
            ListenerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            ListenerSocket.BeginReceiveFrom(receiveState.RawBuffer, 0, receiveState.RawBuffer.Length, SocketFlags.None,
                ref RemoteEndPoint, PortDataReceived, receiveState);

        }
        /// <summary>
        /// 关闭监听
        /// </summary>
        protected override void StopInternal()
        {
            //NO-OP
        }
        ///// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="receiveState"></param>
        private void PortDataReceived(IAsyncResult ar)
        {
            var receiveState = (NetworkState)ar.AsyncState;
            try
            {
                var received = ListenerSocket.EndReceiveFrom(ar, ref RemoteEndPoint);
                if (received == 0)
                {
                    return;
                }
                var remoteAddress = (IPEndPoint)RemoteEndPoint;
                receiveState.RemoteHost = remoteAddress.ToNode(ReactorType.Udp);
                this.ReceivedData(NetworkData.Create(this.Listener, receiveState.RawBuffer, received), receiveState);
                //清除数据继续接收
                receiveState.RawBuffer = new byte[this.BufferSize];
                ListenerSocket.BeginReceiveFrom(receiveState.RawBuffer, 0, receiveState.RawBuffer.Length, SocketFlags.None,
    ref RemoteEndPoint, PortDataReceived, receiveState);
            }
            catch  //node disconnected
            {
                if (SocketMap.ContainsKey(receiveState.RemoteHost.nodeConfig.ToString()))
                {
                    var connection = SocketMap[receiveState.RemoteHost.nodeConfig.ToString()];
                    CloseConnection(connection);
                }
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="destination"></param>
        public override void Send(byte[] buffer, int index, int length, INode destination)
        {
            var clientSocket = SocketMap[destination.nodeConfig.ToString()];
            try
            {
                if (clientSocket.WasDisposed)
                {
                    CloseConnection(clientSocket);
                    return;
                }

                var buf = Allocator.Buffer(length);
                buf.WriteBytes(buffer, index, length);
                Encoder.Encode(ConnectionAdapter, buf, out List<IByteBuf> encodedMessages);
                foreach (var message in encodedMessages)
                {
                    var state = CreateNetworkState(clientSocket.Local, destination, message, 0);
                    ListenerSocket.BeginSendTo(message.ToArray(), 0, message.ReadableBytes, SocketFlags.None,
                      LocalEndPoint, SendCallback, state);
                    LogAgent.Info(String.Format("发送数据[UdpSocket]-->>{0}", this.Encoder.ByteEncode(message.ToArray())));
                    clientSocket.Receiving = true;
                }
            }
            catch (Exception ex)
            {
                LogAgent.Error(ex.ToString());
                CloseConnection(ex, clientSocket);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {

        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="remoteHost"></param>
        internal override void CloseConnection(IConnection remoteHost)
        {
            Console.WriteLine("CloseConnection-->>" + remoteHost.ToString());
            CloseConnection(null, remoteHost);
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="remoteConnection"></param>
        internal override void CloseConnection(Exception reason, IConnection remoteConnection)
        {
            Console.WriteLine("CloseConnection-->>" + reason.ToString() + remoteConnection.ToString());
            //NO-OP (no connections in UDP)
            //try
            //{
            //    NodeDisconnected(new SocketConnectionException(ExceptionType.Closed, reason), remoteConnection);
            //}
            //catch (Exception innerEx)
            //{
            //    OnErrorIfNotNull(innerEx, remoteConnection);
            //}
            //finally
            //{
            //    if (SocketMap.ContainsKey(remoteConnection.RemoteHost))
            //        SocketMap.Remove(remoteConnection.RemoteHost);
            //}
        }

        #region IDisposable Members

        public override void Dispose(bool disposing)
        {
            if (!WasDisposed && disposing && Listener != null)
            {
                Stop();
                ListenerSocket.Dispose();
            }
            IsActive = false;
            WasDisposed = true;
        }

        #endregion
    }
}
