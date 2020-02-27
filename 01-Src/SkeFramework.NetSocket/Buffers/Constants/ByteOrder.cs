using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Buffers.Constants
{
    /// <summary>
    /// 字节大小端排序
    /// </summary>
    public enum ByteOrder
    {
        [Description("小端默认")]
        LittleEndian = 0,
        [Description("大端")]
        BigEndian = 1
    }
}
