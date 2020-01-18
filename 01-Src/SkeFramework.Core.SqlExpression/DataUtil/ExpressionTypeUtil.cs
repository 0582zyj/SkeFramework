using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataUtil
{
    /// <summary>
    /// 表达式类型工具类
    /// </summary>
    public class ExpressionTypeUtil
    {
        public static string NullValueDeal(ExpressionType NodeType, string value)
        {
            if (value.ToUpper() != "NULL")
            {
                return ExpressionTypeUtil.GetOperStr(NodeType) + value;
            }

            switch (NodeType)
            {
                case ExpressionType.NotEqual:
                    {
                        return " IS NOT NULL ";
                    }
                case ExpressionType.Equal:
                    {
                        return " IS NULL ";
                    }
                default: return GetOperStr(NodeType) + value;
            }
        }

        public static string GetOperStr(ExpressionType e_type)
        {
            switch (e_type)
            {
                case ExpressionType.OrElse: return " OR ";
                case ExpressionType.Or: return "|";
                case ExpressionType.AndAlso: return " AND ";
                case ExpressionType.And: return "&";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Modulo: return "%";
                case ExpressionType.Equal: return "=";
            }
            return "";
        }

        public static string GetValueFormat(object obj)
        {
            var type = obj.GetType();

            if (type.Name == "List`1") //list集合
            {
                List<string> data = new List<string>();
                var list = obj as IEnumerable;
                string sql = string.Empty;
                foreach (var item in list)
                {
                    data.Add(GetValueFormat(item));
                }
                sql = "(" + string.Join(",", data) + ")";
                return sql;
            }

            if (type == typeof(string))// 
            {
                return string.Format("'{0}'", obj.ToString());
            }
            if (type == typeof(DateTime))
            {
                DateTime dt = (DateTime)obj;
                return string.Format("'{0}'", dt.ToString("yyyy-MM-dd HH:mm:ss fff"));
            }
            return obj.ToString();
        }
        public static object Eval(MemberExpression member)
        {
            var cast = Expression.Convert(member, typeof(object));
            object c = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            return GetValueFormat(c);
        }

    }
}
