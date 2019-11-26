using SkeFramework.Core.Enums.ExtendAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Network.Enums
{
    /// <summary>
    /// 内容类型枚举值
    /// </summary>
    public enum ContentTypeEnums
    {
        [EnumAttribute(810, "application/json")] POSTJSON,
        [EnumAttribute(811, "application/x-www-form-urlencoded")] GETFORM,
        [EnumAttribute(811, "application/x-www-form-urlencoded;")] POSTFORM,
    }
}
