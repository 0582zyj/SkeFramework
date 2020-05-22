using SkeFramework.Core.Common.ExtendAttributes;
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

        /// <summary>
        /// 获取描述值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static EnumAttribute GetEnumAttribute(this Enum source)
        {
            Type typeDescription = typeof(EnumAttribute);
            string strValue = source.ToString();
            FieldInfo fieldinfo = source.GetType().GetField(strValue);
            if (fieldinfo != null)
            {
                Object[] objs = fieldinfo.GetCustomAttributes(typeof(EnumAttribute), false);
                if (objs != null)
                {
                    return (EnumAttribute)objs[0];
                }
            }
            return null;
        }


        /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
        /// <returns>键值对</returns>
        public static Dictionary<string,Object> GetEnumItemDesc(Type enumType)
        {
            Dictionary<string, Object> nvc = new Dictionary<string, Object>();
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            string strValue = string.Empty;
            object Value =null;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();

                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        Value = arr[0];
                    }
                    else
                    {
                        Value = field.Name;
                    }
                    nvc.Add(strValue, Value);
                }
            }
            return nvc;
        }

        /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
        /// <returns>键值对</returns>
        public static TEnum ConvertEnumAttribute<TEnum>(object itemValue) where TEnum : struct
        {
            Dictionary<string, object> keyValuePairs = GetEnumItemDesc(typeof(TEnum));
            foreach(var item in keyValuePairs)
            {
                if(item.Value is EnumAttribute)
                {
                    EnumAttribute enumAttribute = (EnumAttribute)item.Value;
                    if (enumAttribute.Code.Equals(itemValue.ToString()))
                    {
                        TEnum result;
                        Enum.TryParse(item.Key, out result);
                        return result;
                    }
                }
            }
            return default(TEnum);
        }

    }
}
