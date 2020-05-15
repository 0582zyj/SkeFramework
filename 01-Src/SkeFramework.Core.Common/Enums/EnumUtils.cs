using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Common.Enums
{
    /// <summary>
    /// 枚举工具类
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// 获取描述值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum source)
        {
            Type typeDescription = typeof(DescriptionAttribute);
            string strValue = source.ToString();
            FieldInfo fieldinfo = source.GetType().GetField(strValue);
            if (fieldinfo != null)
            {
                Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs != null)
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    return da.Description;
                }
            }
            return strValue;
        }

    }
}
