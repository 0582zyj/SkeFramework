using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Constants.Enums
{
    /// <summary>
    /// 角色类型枚举
    /// </summary>
    public enum RolesType
    {
        [Description("普通角色")] GENERAL_TYPE = 0,
        [Description("授权角色")] AUTH_TYPE = 1,
    }
}
