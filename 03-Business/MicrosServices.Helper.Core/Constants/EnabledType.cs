using SkeFramework.Core.Enums.ExtendAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Constants
{
    /// <summary>
    /// 启用状态类型枚举
    /// </summary>
    public enum EnabledType
    {
        [EnumAttribute(1, "启用")] ACTIVE = 1,
        [EnumAttribute(0, "停用")] INACTIVE = 0,
    }
}
