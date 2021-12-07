using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Configs
{
    /// <summary>
    /// 默认连接配置
    /// </summary>
    public class DefaultConnectionConfig : IConnectionConfig
    {
        /// <summary>
        /// 内容key
        /// </summary>
        public const string data = "data";
        /// <summary>
        /// 值列表
        /// </summary>
        private readonly Dictionary<string, object> _options = new Dictionary<string, object>();
        /// <summary>
        /// 设置某个Key的值
        /// </summary>
        /// <param name="optionKey"></param>
        /// <param name="optionValue"></param>
        /// <returns></returns>
        public IConnectionConfig SetOption(string optionKey, object optionValue)
        {
            _options[optionKey] = optionValue;
            return this;
        }
        /// <summary>
        /// 判断是否存在Key
        /// </summary>
        /// <param name="optionKey"></param>
        /// <returns></returns>
        public bool HasOption(string optionKey)
        {
            return _options.ContainsKey(optionKey);
        }
        /// <summary>
        /// 判断是否存在Key并且值类型一致
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionKey"></param>
        /// <returns></returns>
        public bool HasOption<T>(string optionKey)
        {
            return _options.ContainsKey(optionKey) && _options[optionKey] is T;
        }
        /// <summary>
        /// 获取某个Key的值
        /// </summary>
        /// <param name="optionKey"></param>
        /// <returns></returns>
        public object GetOption(string optionKey)
        {
            if (_options.ContainsKey(optionKey))
            {
                return _options[optionKey];
            }
            return default(object);
        }
        /// <summary>
        /// 获取某个Key的值并返回具体泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="optionKey"></param>
        /// <returns></returns>
        public T GetOption<T>(string optionKey)
        {
            if (_options.ContainsKey(optionKey))
            {
                return (T)_options[optionKey];
            }
            return default(T);
        }
        /// <summary>
        /// 获取值列表
        /// </summary>
        IList<KeyValuePair<string, object>> IConnectionConfig.Options
        {
            get
            {
                return _options.ToList();
            }
        }
    }
}
