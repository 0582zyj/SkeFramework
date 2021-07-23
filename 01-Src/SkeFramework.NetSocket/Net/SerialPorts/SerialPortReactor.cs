using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Topology;
using SkeFramework.NetSerialPort.Topology.Nodes;
using SkeFramework.NetSerialPort.Topology.ExtendNodes;
using SkeFramework.NetSerialPort.Protocols.Configs.Enums;

namespace SkeFramework.NetSerialPort.Net.SerialPorts
{
    /// <summary>
    /// 串口通信实现类
    /// </summary>
    public sealed class SerialPortReactor : ProxyReactorBase
    {
        /// <summary>
        /// 
        /// </summary>
        private SerialPort ListenerSocket;
        /// <summary>
        /// 是否打开
        /// </summary>
        public override bool IsActive { get; protected set; }
        /// <summary>
        /// 是否正在解析
        /// </summary>
        public override bool IsParsing { get; protected set; }

        public SerialPortReactor(INode listener,
            IMessageEncoder encoder, IMessageDecoder decoder, IByteBufAllocator allocator,
            int bufferSize = NetworkConstants.DEFAULT_BUFFER_SIZE)
            : base(listener, encoder, decoder, allocator,
                bufferSize)
        {
            SerialNodeConfig nodeConfig = listener.nodeConfig as SerialNodeConfig;
            ListenerSocket = new SerialPort
            {
                PortName = nodeConfig.PortName,
                BaudRate = nodeConfig.BaudRate,
                DataBits = nodeConfig.DataBits,
                StopBits = nodeConfig.StopBits,
                Parity = nodeConfig.Parity,
                ReceivedBytesThreshold = 1
            };
        }

        /// <summary>
        /// 默认配置
        /// </summary>
        /// <param name="config"></param>
        public override void Configure(IConnectionConfig config)
        {
            if (config.HasOption(OptionKeyEnums.ReadBufferSize.ToString()))
                ListenerSocket.ReadBufferSize = Convert.ToInt32(config.GetOption(OptionKeyEnums.ReadBufferSize.ToString()));
            if (config.HasOption(OptionKeyEnums.WriteBufferSize.ToString()))
                ListenerSocket.WriteBufferSize = Convert.ToInt32(config.GetOption(OptionKeyEnums.WriteBufferSize.ToString()));
            if (config.HasOption(OptionKeyEnums.ParseTimeOut.ToString()))
                networkState.TimeOutSeconds = Convert.ToInt64(config.GetOption(OptionKeyEnums.ParseTimeOut.ToString()));
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        protected override void StartInternal()
        {
            var receiveState = CreateNetworkState(Listener, Node.Empty());
            if (!SocketMap.ContainsKey(this.LocalEndpoint.nodeConfig.ToString()))
            {
                RefactorRequestChannel adapter;
                adapter = new RefactorProxyRequestChannel(this, this.LocalEndpoint, "none");
                SocketMap.Add(this.LocalEndpoint.nodeConfig.ToString(), adapter);
            }
            ListenerSocket.DataReceived += new SerialDataReceivedEventHandler(PortDataReceived);
            ListenerSocket.Open();
            IsActive = true;
        }
        /// <summary>
        /// 关闭监听
        /// </summary>
        protected override void StopInternal()
        {
            //NO-OP
        }
        /// <summary>
        /// 数据接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(50);
            ////禁止接收事件时直接退出
            //if (OnReceive==null) return;
            if (this.WasDisposed) return;  //如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环
            try
            {
                this.IsParsing = true;  //设置标记，说明已经开始处理数据，一会儿要使用系统UI
                int n = ListenerSocket.BytesToRead;
                byte[] buf = new byte[n];
                this.ListenerSocket.Read(buf, 0, n);
                NetworkData networkData = NetworkData.Create(this.Listener, buf, n);
                this.ReceivedData(networkData);
            }
            catch (Exception ex)
            {
                string enmsg = string.Format("Serial Port {0} Communication Fail\r\n" + ex.ToString(), ListenerSocket.PortName);
            }
            finally
            {
                IsParsing = false;   //监听完毕， UI可关闭串口
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
                List<IByteBuf> encodedMessages;
                Encoder.Encode(ConnectionAdapter, buf, out encodedMessages);
                foreach (var message in encodedMessages)
                {
                    ListenerSocket.Write(message.ToArray(), message.ReaderIndex, message.ReadableBytes);
                    string log = String.Format("{0}:串口发送数据-->>{1}", DateTime.Now.ToString("hh:mm:ss"), this.Encoder.ByteEncode(message.ToArray()));
                    Console.WriteLine(log);
                    clientSocket.Receiving = true;
                }
            }
            catch (Exception ex)
            {
                CloseConnection(ex, clientSocket);
            }
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
