using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Response;
using SkeFramework.NetSerialPort.Topology;
using SkeFramework.NetSerialPort.Topology.Nodes;

namespace SkeFramework.NetSerialPort.Net.SerialPorts
{
   public sealed class SerialPortReactor: ProxyReactorBase
    {

        public SerialPortReactor(NodeConfig nodeConfig, 
            IMessageEncoder encoder, IMessageDecoder decoder, IByteBufAllocator allocator,
            int bufferSize = NetworkConstants.DEFAULT_BUFFER_SIZE)
            : base(nodeConfig,  encoder, decoder, allocator, 
                bufferSize)
        {
            //LocalEndpoint = new IPEndPoint(localAddress, localPort);
            //RemoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }


        public override bool IsActive { get; protected set; }

        public override void Configure(IConnectionConfig config)
        {
            if (config.HasOption<int>("ReadBufferSize"))
                Listener.ReadBufferSize = config.GetOption<int>("ReadBufferSize");
            if (config.HasOption<int>("WriteBufferSize"))
                Listener.WriteBufferSize = config.GetOption<int>("WriteBufferSize");
            else
                ProxiesShareFiber = true;
        }

        protected override void StartInternal()
        {
            IsActive = true;
            var receiveState = CreateNetworkState(Listener, Node.Empty());
            Listener.DataReceived += new SerialDataReceivedEventHandler(PortDataReceived);
        }


        private void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            //var receiveState = (NetworkState)ar.AsyncState;
            //try
            //{
            //    var received = receiveState.Socket.EndReceiveFrom(ar, ref RemoteEndPoint);
            //    if (received == 0)
            //    {
            //        if (SocketMap.ContainsKey(receiveState.RemoteHost))
            //        {
            //            var connection = SocketMap[receiveState.RemoteHost];
            //            CloseConnection(connection);
            //        }
            //        return;
            //    }

            //    var remoteAddress = (IPEndPoint)RemoteEndPoint;

            //    if (receiveState.RemoteHost.IsEmpty())
            //        receiveState.RemoteHost = remoteAddress.ToNode(TransportType.Udp);

            //    ReactorResponseChannel adapter;
            //    if (SocketMap.ContainsKey(receiveState.RemoteHost))
            //    {
            //        adapter = SocketMap[receiveState.RemoteHost];
            //    }
            //    else
            //    {
            //        adapter = new ReactorProxyResponseChannel(this, receiveState.Socket, remoteAddress,
            //            EventLoop.Clone(ProxiesShareFiber));
            //        ;
            //        SocketMap.Add(adapter.RemoteHost, adapter);
            //        NodeConnected(adapter.RemoteHost, adapter);
            //    }

            //    receiveState.Buffer.WriteBytes(receiveState.RawBuffer, 0, received);

            //    List<IByteBuf> decoded;
            //    Decoder.Decode(ConnectionAdapter, receiveState.Buffer, out decoded);

            //    foreach (var message in decoded)
            //    {
            //        var networkData = NetworkData.Create(receiveState.RemoteHost, message);
            //        ReceivedData(networkData, adapter);
            //    }

            //    //reuse the buffer
            //    if (receiveState.Buffer.ReadableBytes == 0)
            //        receiveState.Buffer.SetIndex(0, 0);
            //    else
            //        receiveState.Buffer.CompactIfNecessary();

            //    receiveState.Socket.BeginReceiveFrom(receiveState.RawBuffer, 0, receiveState.RawBuffer.Length,
            //        SocketFlags.None, ref RemoteEndPoint, ReceiveCallback, receiveState); //receive more messages
            //}
            //catch  //node disconnected
            //{
            //    var connection = SocketMap[receiveState.RemoteHost];
            //    CloseConnection(ex, connection);
            //}
        }

        public override void Send(byte[] buffer, int index, int length, INode destination)
        {
            var clientSocket = SocketMap[destination];
            try
            {
                if (clientSocket.WasDisposed)
                {
                    CloseConnection(clientSocket);
                    return;
                }

                //var buf = Allocator.Buffer(length);
                //buf.WriteBytes(buffer, index, length);
                //List<IByteBuf> encodedMessages;
                //Encoder.Encode(ConnectionAdapter, buf, out encodedMessages);
                //foreach (var message in encodedMessages)
                //{
                //    var state = CreateNetworkState(clientSocket.Socket, destination, message, 0);
                //    clientSocket.Socket.BeginSendTo(message.ToArray(), 0, message.ReadableBytes, SocketFlags.None,
                //        destination.ToEndPoint(),
                //        SendCallback, state);
                //}
            }
            catch (Exception ex)
            {
                CloseConnection(ex, clientSocket);
            }
        }


        internal override void CloseConnection(IConnection remoteHost)
        {
            CloseConnection(null, remoteHost);
        }

        internal override void CloseConnection(Exception reason, IConnection remoteConnection)
        {
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

        protected override void StopInternal()
        {
            //NO-OP
        }

        #region IDisposable Members

        public override void Dispose(bool disposing)
        {
            if (!WasDisposed && disposing && Listener != null)
            {
                Stop();
                Listener.Dispose();
            }
            IsActive = false;
            WasDisposed = true;
        }

        #endregion
    }
}
