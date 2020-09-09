using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataEntities.Constants
{
    /// <summary>
    /// 错误代码枚举
    /// </summary>
    public enum WebSocketErrorCodeType:int
    {
        [Description("推送应用未启动")]
        ServiceNotStart=4001,
        [Description("授权错误：Token已过期，请重新获取")]
        TokenExpired = 4002,
    }
}
