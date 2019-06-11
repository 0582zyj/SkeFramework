using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.IO.Enums
{
    /// <summary>
    /// 大小端枚举类
    /// </summary>
    [Flags]
    public enum EndianEnum:int
    {
        BIG_ENDIAN=1,
        LITTLE_ENDIAN=0
    }
}
