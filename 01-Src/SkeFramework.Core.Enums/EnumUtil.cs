using SkeFramework.Core.Enums.ExtendAttribute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Enums
{
    public class EnumUtil
    {
        /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
        /// <returns>键值对</returns>
        public static NameValueCollection GetEnumItemDesc(Type enumType)
        {
            NameValueCollection nvc = new NameValueCollection();
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            string strText = string.Empty;
            string strValue = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                        strText = aa.Description;
                    }
                    else
                    {
                        strText = field.Name;
                    }
                    nvc.Add(strText, strValue);
                }
            }
            return nvc;
        }
        /// <summary>
        /// 枚举 int 转 枚举名称
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="itemValue">int值</param>
        /// <returns></returns>
        public static string ConvertEnumToString<T>(object itemValue)
        {
            NameValueCollection collection = EnumUtil.GetEnumItemDesc(typeof(T));
            foreach (string s in collection.AllKeys)
            {
                if (collection.GetValues(s).Equals(itemValue))
                {
                    return s;
                }
            }
            return Enum.Parse(typeof(T), itemValue.ToString()).ToString();
        }
        /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
        /// <returns>键值对</returns>
        public static Dictionary<String, EnumAttribute> ListEnumExtendAttribute(Type enumType)
        {
            Dictionary<String, EnumAttribute> nvc = new Dictionary<String, EnumAttribute>();
            Type typeDescription = typeof(EnumAttribute);
            System.Reflection.FieldInfo[] fields = enumType.GetFields();
            string strText = string.Empty;
            string strValue = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    strText = field.Name;
                    EnumAttribute item = null;
                    if (arr.Length > 0)
                    {
                        item = (EnumAttribute)arr[0];
                    }
                    nvc.Add(strText, item);
                }
            }
            return nvc;
        }
        /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
        /// <returns>键值对</returns>
        public static EnumAttribute GetEnumExtendAttribute<T>(object itemValue)
        {
            Dictionary<String, EnumAttribute> nvc = EnumUtil.ListEnumExtendAttribute(typeof(T));
            if (nvc.ContainsKey(itemValue.ToString()))
            {
                return nvc[itemValue.ToString()];
            }
            return null;
        }

    }
}