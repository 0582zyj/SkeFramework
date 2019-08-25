using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Topology
{
    /// <summary>
    /// 空节点
    /// 表示某项是本地的，而不是联网的。
    /// </summary>
    public class EmptyNode : INode
    {
        private IPEndPoint _endPoint;
        public IPAddress Host { get; set; }
        public int Port { get; set; }
        public TransportType TransportType { get; set; }
        public string MachineName { get; set; }
        public string OS { get; set; }
        public string ServiceVersion { get; set; }
        public string CustomData { get; set; }

        public IPEndPoint ToEndPoint()
        {
            return _endPoint ?? (_endPoint = new IPEndPoint(IPAddress.None, 0));
        }

        public object Clone()
        {
            return Node.Empty();
        }
    }
}