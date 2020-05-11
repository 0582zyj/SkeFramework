using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Common.Collections
{
    /// <summary>
    /// 集合工具类
    /// </summary>
    public static class CollectionUtils
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Boolean IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return true;
            return !source.Any();
        }
    }
}
