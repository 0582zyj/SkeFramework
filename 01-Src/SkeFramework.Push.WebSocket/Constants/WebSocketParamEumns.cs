using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.WebSocket.Constants
{
    /// <summary>
    /// 参数枚举
    /// </summary>
    public enum WebSocketParamEumns
    {
        [Description("是否使用证书")]
        IsUseCertificate,
        [Description("服务名称")]
        ServerName,
        [Description("服务端口")]
        Port,
        [Description("安全机制类型")]
        ServerSecurity,
        [Description("证书名称")]
        ServerStoreName,
        [Description("服务端证书")]
        ServerThumbprint
    }
}
