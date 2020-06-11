using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Configs
{
    /// <summary>
    /// 连接参数配置
    /// </summary>
    public interface IConnectionConfig
    {
        IList<KeyValuePair<string, object>> Options { get; }

        /// <summary>
        /// 设置参数和值
        /// </summary>
        /// <param name="optionKey">名称</param>
        /// <param name="optionValue">值</param>
        /// <returns></returns>
        IConnectionConfig SetOption(string optionKey, object optionValue);

        /// <summary>
        /// 判断某个Key是否存在
        /// </summary>
        /// <param name="optionKey">名称</param>
        /// <returns>true if found, false otherwise</returns>
        bool HasOption(string optionKey);

        /// <summary>
        ///     Checks to see if we have a set option of ths key in the dictionary AND
        ///     that the value of this option is of type
        ///     <typeparam name="T"></typeparam>
        /// </summary>
        /// <param name="optionKey">The name of the value to check</param>
        /// <returns>true if found and of type T, false otherwise</returns>
        bool HasOption<T>(string optionKey);

        /// <summary>
        /// 获取Key对应的值
        /// </summary>
        /// <param name="optionKey">The name of the value to get</param>
        /// <returns>the object if found, null otherwise</returns>
        object GetOption(string optionKey);

        /// <summary>
        ///  获取值泛型方法
        ///  <typeparam name="T"></typeparam>
        /// </summary>
        /// <param name="optionKey">The name of the value to get</param>
        /// <returns>the object as instance of type T if found, default(T) otherwise</returns>
        T GetOption<T>(string optionKey);
    }

}
