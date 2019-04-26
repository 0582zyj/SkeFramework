using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPBroadcast.Helpers;

namespace UDPBroadcast.Services
{
   public class CommPortUdp
    {
        public delegate void SendDelegate(string message);
        public SendDelegate SendMessageCallback;
        public delegate void ReceiveDelegate(string message);
        public ReceiveDelegate ReceiveMessageCallback;

        private UdpClient sendUdpClient;
        private UdpClient receiveUdpClient;
        // 组播IP地址
        private IPEndPoint broadcastIpEndPoint;
        private bool IsRunning = true;

        public int LocalPort = 52441;
        public string LocalIP = "";
        public string LocalGroupIp = "192.1.1.1";
        public string SendGroupIp = "192.1.1.1";
        public CommPortUdp()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            this.LocalIP = ips[1].ToString();
            this.LocalPort = 52441;
            // 默认组,组播地址是有范围
            // 具体关于组播和广播的介绍参照我上一篇博客UDP编程
            // 本地组播组
            this.LocalGroupIp = "224.0.0.1";
            // 发送到的组播组
            this.SendGroupIp = "224.0.0.1";
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message, bool IsBroadcast=true)
        {
            // 根据选择的模式发送信息
            if (IsBroadcast == true)
            {
                // 广播模式(自动获得子网中的IP广播地址)
                broadcastIpEndPoint = new IPEndPoint(IPAddress.Broadcast, this.LocalPort);
            }
            else
            {
                // 组播模式
                broadcastIpEndPoint = new IPEndPoint(IPAddress.Parse(SendGroupIp), LocalPort);
            }
            // 启动发送线程发送消息
            Thread sendThread = new Thread(ProcessSendMessage);
            sendThread.Start(message);
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="IsJoinGroup"></param>
        public void Receive(bool IsJoinGroup,string IP,int Port)
        {
            // 创建接收套接字
            this.LocalIP = IP;
            this.LocalPort = Port;
            IPAddress localIp = IPAddress.Parse(this.LocalIP);
            IPEndPoint localIpEndPoint = new IPEndPoint(localIp, this.LocalPort);
            receiveUdpClient = new UdpClient(localIpEndPoint);
            // 加入组播组
            if (IsJoinGroup == true)
            {
                receiveUdpClient.JoinMulticastGroup(IPAddress.Parse(this.LocalGroupIp));
                receiveUdpClient.Ttl = 50;
            }
            // 启动接受线程
            Thread threadReceive = new Thread(ProcessReceiveMessage);
            threadReceive.Start();
        }
        
        #region 线程
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void ProcessSendMessage(object obj)
        {
            string message = obj.ToString();
            byte[] messagebytes = Encoding.Unicode.GetBytes(message);
            sendUdpClient = new UdpClient();
            // 发送消息到组播或广播地址
            sendUdpClient.Send(messagebytes, messagebytes.Length, broadcastIpEndPoint);
            sendUdpClient.Close();
            if (SendMessageCallback != null)
            {
                SendMessageCallback(message);
            }
        }
        /// <summary>
        /// 接收消息处理
        /// </summary>
        /// <param name="obj"></param>
        private void ProcessReceiveMessage(object obj)
        {
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            do
            {
                try
                {
                    // 关闭receiveUdpClient时此时会产生异常
                    byte[] receiveBytes = receiveUdpClient.Receive(ref remoteIpEndPoint);
                    string receivemessage = ByteHelper.GetBytesText(receiveBytes, 0, receiveBytes.Length);
                    // 显示消息内容
                    if (ReceiveMessageCallback != null)
                    {
                        ReceiveMessageCallback(receivemessage);
                    }
                }
                catch
                {
                    break;
                }
            } while (IsRunning);
        }
        #endregion

        #region Dispose
        public void receiveUdpClientDispose()
        {
            if (receiveUdpClient != null)
            {
                receiveUdpClient.Close();
            }
        }
        #endregion
    }
}
