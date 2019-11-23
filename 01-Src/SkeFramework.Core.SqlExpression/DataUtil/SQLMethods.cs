using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataUtil
{
    /// <summary>
    /// SQL常用方法
    /// </summary>
    public static class SQLMethods
    {
        public static bool DB_In<T>(this T t, List<T> list)  // in
        {
            return true;
        }
        public static Boolean DB_NotIn<T>(this T t, List<T> list) // not in
        {
            return true;
        }
        public static int DB_Length(this string t)  // len();
        {
            return 0;
        }
        public static bool DB_Like(this string t, string str) // like
        {
            return true;
        }
        public static bool DB_NotLike(this string t, string str) // not like 
        {
            return true;
        }
    }
}
