using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.DataBase.Common.AttributeExtend;

namespace SkeFramework.DataBase.Common.DataCommon
{
    /// <summary>
    /// 属性帮助类
    /// </summary>
    public class PropertiesHelper
    {
        private static PropertiesHelper _SimpleInstance = null;

        public static PropertiesHelper Instance()
        {
            if (_SimpleInstance == null)
            {
                _SimpleInstance = new PropertiesHelper();
            }
            return _SimpleInstance;
        }
        /// <summary>
        /// 获取所有属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public PropertyInfo[] GetAllProperties(Type type)
        {
            return type.GetProperties();
        }

        /// <summary>
        /// 通过反射获取表名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetTableName(Type type)
        {
            // 创造一个实例
            dynamic obj = Activator.CreateInstance(type);
            return obj.GetTableName();
        }
        /// <summary>
        /// 获取主键名字
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetKeyName(Type type)
        {
            // 创造一个实例
            dynamic obj = Activator.CreateInstance(type);
            return obj.GetKey();
        }
        /// <summary>
        /// 获取DescriptionAttribute为Key的属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public PropertyInfo[] GetKeyProperties(Type type)
        {
            var KeyProperties = new List<PropertyInfo>();
            var AllProperties = type.GetProperties();
            foreach (var item in AllProperties)
            {
                //获取属性的描述
                KeyAttribute[] obs = item.GetCustomAttributes(typeof(KeyAttribute), false) as KeyAttribute[];
                if (obs.Length > 0)
                {
                    KeyProperties.Add(item);
                }
            }
            return KeyProperties.ToArray();
        }
        /// <summary>
        /// 获取DescriptionAttribute为ignore的属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public PropertyInfo[] GetIgnoreProperties(Type type)
        {
            var IgnoreProperties = new List<PropertyInfo>();
            var AllProperties = type.GetProperties();
            foreach (var item in AllProperties)
            {
                //获取属性的描述
                IgonreAttribute[] obs = item.GetCustomAttributes(typeof(IgonreAttribute), false) as IgonreAttribute[];
                if (obs.Length > 0)
                {
                    IgnoreProperties.Add(item);
                }
            }
            return IgnoreProperties.ToArray();
        }


        public object GetValue<T>(T obj, string propertyName)
        {
            PropertyInfo propertyInfo;
            propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
            return propertyInfo.GetValue(obj, null);
        }

        public void SetValue<T, K>(T obj, string propertyName, K val)
        {
            PropertyInfo propertyInfo;
            propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
            if (propertyInfo != null)
            {
                Type type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                propertyInfo.SetValue(obj, (val == null) ? null : Convert.ChangeType(val, type), null);
            }
        }

    }
}
