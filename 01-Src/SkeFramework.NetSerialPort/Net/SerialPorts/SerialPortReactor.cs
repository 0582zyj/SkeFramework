using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Allocators;
using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols;
using SkeFramework.NetSerialPort.Protocols.Configs;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Protocols.Response;
using SkeFramework.NetSerialPort.Topology;
using SkeFramework.NetSerialPort.Topology.Nodes;

namespace SkeFramework.NetSerialPort.Net.SerialPorts
{
    /// <summary>
    /// 串口通信实现类
    /// </summary>
   public sealed class SerialPortReactor: ProxyReactorBase
    {

        public SerialPortReactor(NodeConfig nodeConfig, 
            IMessageEncoder encoder, IMessageDecoder decoder, IByteBufAllocator allocator,
            int bufferSize = NetworkConstants.DEFAULT_BUFFER_SIZE)
            : base(nodeConfig,  encoder, decoder, allocator, 
                bufferSize)
        {
        }
        /// <summary>
        /// 是否打开
        /// </summary>
        public override bool IsActive { get; protected set; }
        /// <summary>
        /// 是否正在解析
        /// </summary>
        public override bool IsParsing { get; protected set; }

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
            var receiveState = CreateNetworkState(Listener, Node.Empty());
            if (!SocketMap.ContainsKey(this.LocalEndpoint))
            {
                RefactorRequestChannel adapter; 
                adapter = new RefactorProxyRequestChannel(this, this.LocalEndpoint);
                SocketMap.Add(this.LocalEndpoint, adapter);
            }
            Listener.DataReceived += new SerialDataReceivedEventHandler(PortDataReceived);
            Listener.Open();
            IsActive = true;

        }


        private void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(50);
            ////禁止接收事件时直接退出
            //if (OnReceive==null) return;
            if (this.WasDisposed) return;  //如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环
            try
            {
                IsParsing = true;  //设置标记，说明已经开始处理数据，一会儿要使用系统UI
                int n = Listener.BytesToRead;
                byte[] buf = new byte[n];
                Listener.Read(buf, 0, n);
                int receive_count = n;

                StringBuilder builder=new StringBuilder();
                builder.Append(Encoding.ASCII.GetString(buf));
                                
                string  receive_content = builder.ToString();

                int CRLF = -1;
                CRLF = receive_content.IndexOf("\r\n",2);
                if (CRLF != -1)
                {
                    string content = receive_content.Substring(0, CRLF+2);
                    //触发整条记录的处理
                    NetworkState state = CreateNetworkState(Listener, LocalEndpoint);
                    state.RawBuffer = Encoding.ASCII.GetBytes(content);
                    this.ReceiveCallback(state);
                }
            }
            catch (Exception ex)
            {
                string enmsg = string.Format("Serial Port {0} Communication Fail\r\n" + ex.ToString(), Listener.PortName);
            }
            finally
            {
                IsParsing = false;   //监听完毕， UI可关闭串口
            }
        }
     
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="receiveState"></param>
       private void ReceiveCallback(NetworkState receiveState)
        {
            try
            {
                var received = receiveState.RawBuffer.Length;
                if (received == 0)
                {
                    if (SocketMap.ContainsKey(receiveState.RemoteHost))
                    {
                        var connection = SocketMap[receiveState.RemoteHost];
                    }
                    return;
                }

                receiveState.Buffer.WriteBytes(receiveState.RawBuffer, 0, received);

                List<IByteBuf> decoded;
                Decoder.Decode(ConnectionAdapter, receiveState.Buffer, out decoded);

                foreach (var message in decoded)
                {
                    var networkData = NetworkData.Create(receiveState.RemoteHost, message);
                    Console.WriteLine("串口收到数据-->>" + String.Join(" ", networkData.Buffer));
                    if(ConnectionAdapter is ReactorConnectionAdapter)
                    {
                        ((ReactorConnectionAdapter)ConnectionAdapter).networkDataDocker.AddNetworkData(networkData);
                        ((EventWaitHandle)((ReactorConnectionAdapter)ConnectionAdapter).protocolEvents[(int)ProtocolEvents.PortReceivedData]).Set();
                    }
                 
                }

                //ReactorResponseChannel adapter;
                //if (SocketMap.ContainsKey(receiveState.RemoteHost))
                //{
                //    adapter = SocketMap[receiveState.RemoteHost];
                //}
                //else
                //{
                //    adapter = new ReactorProxyResponseChannel(this, receiveState.Socket);
                //    SocketMap.Add(adapter.RemoteHost, adapter);
                //}
                //ReceivedData(networkData, adapter);
            }
            catch  //node disconnected
            {
                var connection = SocketMap[receiveState.RemoteHost];
                CloseConnection(connection);
            }
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
                var buf = Allocator.Buffer(length);
                buf.WriteBytes(buffer, index, length);
                List<IByteBuf> encodedMessages;
                Encoder.Encode(ConnectionAdapter, buf, out encodedMessages);
                foreach (var message in encodedMessages)
                {
                    Listener.Write(message.ToArray(), message.ReaderIndex, message.ReadableBytes);
                    //clientSocket.Send.Socket.Write(message.ToArray(), message.ReaderIndex, message.ReadableBytes);
                    Console.WriteLine("串口发送数据-->>" +String.Join(" ", message.ToArray()));
                }
            }
            catch (Exception ex)
            {
                CloseConnection(ex, clientSocket);
            }
        }


        internal override void CloseConnection(IConnection remoteHost)
        {
            Console.WriteLine("CloseConnection-->>" + remoteHost.ToString());
            CloseConnection(null, remoteHost);
        }

        internal override void CloseConnection(Exception reason, IConnection remoteConnection)
        {
            Console.WriteLine("CloseConnection-->>"+ reason.ToString() + remoteConnection.ToString());
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
