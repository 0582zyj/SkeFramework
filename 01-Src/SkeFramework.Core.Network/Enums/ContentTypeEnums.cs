﻿using SkeFramework.Core.Common.ExtendAttributes;
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
        [EnumAttribute("application/json","Post")] POSTJSON= 810,
        [EnumAttribute("application/x-www-form-urlencoded", "Get")] GETFORM= 811,
        [EnumAttribute("application/x-www-form-urlencoded", "Post")] POSTFORM= 812,
        [EnumAttribute("multipart/form-data", "Post")] POSTDATA = 813,       
    }
}
