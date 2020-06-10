using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.WebSocket.DataEntities
{
    /// <summary>
    /// 服务端启动参数
    /// </summary>
    public class WebSocketParam
    {
        /// <summary>
        /// 是否使用证书
        /// </summary>
        public bool IsUseCertificate { get; set; } = false;
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServerName { get; set; } = "WebSocketServer";
        /// <summary>
        /// 服务端口
        /// </summary>
        public int Port { get; set; } = 9900;
        /// <summary>
        /// 安全机制类型
        /// </summary>
        public string ServerSecurity { get; set; } = "tls";
        /// <summary>
        /// 证书名称
        /// </summary>
        public string ServerStoreName { get;  set; }
        /// <summary>
        /// 服务端证书
        /// </summary>
        public string ServerThumbprint { get;  set; }
    }
}
