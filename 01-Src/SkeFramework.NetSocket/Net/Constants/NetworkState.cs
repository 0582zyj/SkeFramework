using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Buffers.Serialization.Achieves;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Net.Constants
{
    /// <summary>
    /// 用于处理连接实例上的数据的状态对象
    /// </summary>
    public class NetworkState
    {

        public NetworkState(INode socket, INode remoteHost, IByteBuf buffer, int rawBufferLength)
        {
            Buffer = buffer;
            RemoteHost = remoteHost;
            Socket = socket;
            RawBuffer = new byte[rawBufferLength];
            TimeOutSeconds = NetworkConstants.DefaultPraseTimeOut;
        }
        /// <summary>
        /// Socket对象
        /// </summary>
        public INode Socket { get; private set; }
        /// <summary>
        /// 远程主机节点
        /// </summary>
        public INode RemoteHost { get; set; }
        /// <summary>
        /// 接收缓冲区
        /// </summary>
        public IByteBuf Buffer { get; private set; }
        /// <summary>
        /// 原始缓冲区
        /// </summary>
        public byte[] RawBuffer { get;  set; }
        /// <summary>
        /// 过期时间戳【s】
        /// </summary>
        public long TimeOutSeconds { get; set; }
        /// <summary>
        /// 检查是否超时
        /// </summary>
        /// <returns></returns>
        public bool CheckPraseTimeOut()
        {
            TimeSpan tss = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long NowTimeSpan = Convert.ToInt64(tss.TotalMilliseconds);
            long TimeOutSpan = this.Buffer.ReceiveTimeSpan + TimeOutSeconds * 1000;
            if (this.TimeOutSeconds > 0 && this.Buffer.ReceiveTimeSpan>0  && this.Buffer.ReadableBytes>0
                && (TimeOutSpan < NowTimeSpan))
            {
                byte[] removeByte = this.Buffer.ReadBytes(this.Buffer.ReadableBytes);
                string log = String.Format("{0}:串口超时丢弃数据-->>{1}", DateTime.Now.ToString("hh:mm:ss"),
                       new NoOpEncoder().ByteEncode(removeByte));
                Console.WriteLine(log);
                return true;
            }
            return false;
        }
    }
}
