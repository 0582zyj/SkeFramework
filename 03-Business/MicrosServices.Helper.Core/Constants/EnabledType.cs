using SkeFramework.Core.Common.ExtendAttributes;
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
        [EnumAttribute( "启用", "1")] ACTIVE = 1,
        [EnumAttribute( "停用", "0")] INACTIVE = 0,
    }
}
