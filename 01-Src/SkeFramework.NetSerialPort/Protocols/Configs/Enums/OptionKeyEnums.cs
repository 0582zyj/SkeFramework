using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSerialPort.Protocols.Configs.Enums
{

    /// <summary>
    /// 键值对枚举
    /// </summary>
    public enum OptionKeyEnums
    {
        [Description("应答模式")]
        ProcessMode,
        [Description("读缓冲区大小")]
        ReadBufferSize,
        [Description("写缓冲区大小")]
        WriteBufferSize,
        [Description("解析超时时间")]
        ParseTimeOut,
        [Description("协议超时时间")]
        ProtocolTimeOut,
    }
}
