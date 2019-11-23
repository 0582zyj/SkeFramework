using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.DataBase.Common.DataCommon
{
    /// <summary>
    /// 表达式帮助类
    /// </summary>
    public class ExpressionHelper
    {
        #region 单例模式
        /// <summary>
        /// 单例模式
        /// </summary>
        private static ExpressionHelper _SimpleInstance = null;
        public static ExpressionHelper Instance()
        {
            if (_SimpleInstance == null)
            {
                _SimpleInstance = new ExpressionHelper();
            }
            return _SimpleInstance;
        }
        #endregion

        /// <summary>
        /// 获取SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public string GetSql<T, TKey>(Expression<Func<T, TKey>> exp)
        {
            return DealExpression(exp.Body);
        }
        /// <summary>
        /// 获取SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public string GetSql<T>(Expression<Func<T, bool>> exp)
        {
            return DealExpression(exp.Body);
        }
        /// <summary>
        /// 解析Expression表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="need"></param>
        /// <returns></returns>
        private string DealExpression(Expression exp, bool need = false)
        {
            string name = exp.GetType().Name;
            switch (name)
            {
                case ConstantsData.BinaryExpression:
                case ConstantsData.LogicalBinaryExpression:
                case ConstantsData.MethodBinaryExpression:
                case ConstantsData.SimpleBinaryExpression:
                    {
                        BinaryExpression b_exp = exp as BinaryExpression;
                        if (exp.NodeType == ExpressionType.Add
                            || exp.NodeType == ExpressionType.Subtract
                            )
                        {
                            return "(" + DealBinary(b_exp) + ")";
                        }

                        if (!need) return DealBinary(b_exp);
                        BinaryExpression b_left = b_exp.Left as BinaryExpression;
                        BinaryExpression b_right = b_exp.Right as BinaryExpression;
                        if (b_left != null && b_right != null)
                        {
                            return "(" + DealBinary(b_exp) + ")";
                        }
                        return DealBinary(b_exp);
                    }
                case ConstantsData.MemberExpression:
                case ConstantsData.PropertyExpression:
                case ConstantsData.FieldExpression:
                    return DealMember(exp as MemberExpression);
                case ConstantsData.ConstantExpression:
                    return DealConstant(exp as ConstantExpression);
                case ConstantsData.MemberInitExpression:
                    return DealMemberInit(exp as MemberInitExpression);
                case ConstantsData.UnaryExpression:
                    return DealUnary(exp as UnaryExpression);
                case ConstantsData.MethodCallExpressionN:
                case ConstantsData.InstanceMethodCallExpression1:
                case ConstantsData.InstanceMethodCallExpressionN:
                    return DealMethodsCall(exp as MethodCallExpression);
                default:
                    Console.WriteLine("error:" + name);
                    return "";
            }

        }
        private string DealFieldAccess(FieldAccessException f_exp)
        {
            var c = f_exp;
            return "";
        }
        private string DealMethodsCall(MethodCallExpression m_exp)
        {
            var k = m_exp;
            var g = k.Arguments[0];
            if (k.Method.DeclaringType.FullName == "System.Convert")
            {
                return DealExpression(g);
            }
            if (k.Method.Name == "Contains")
            {
                if (k.Method.DeclaringType.Name.Contains("List`1"))
                {
                    var exp1 = k.Arguments[0];
                    var exp2 = k.Object;
                    string methods = " IN ";
                    return DealExpression(exp1) + methods + DealExpression(exp2);
                }
                else
                {
                    var exp1 = k.Arguments[0];
                    var exp2 = k.Object;
                    string methods = " LIKE ";
                    char[] trimChars = "'".ToCharArray();
                    return DealExpression(exp2) + methods + "'%" + DealExpression(exp1).Trim(trimChars) + "%'";
                }
            }
            /// 控制函数所在类名。
            if (k.Method.DeclaringType != typeof(SQLMethods))
            {
                throw new Exception("无法识别函数");
            }
            switch (k.Method.Name)
            {
                case "DB_Length":
                    {
                        var exp = k.Arguments[0];
                        return "LEN(" + DealExpression(exp) + ")";
                    }
                case "DB_In":
                case "DB_NotIn":
                    {
                        var exp1 = k.Arguments[0];
                        var exp2 = k.Arguments[1];
                        string methods = string.Empty;
                        if (k.Method.Name == "In")
                        {
                            methods = " IN ";
                        }
                        else
                        {
                            methods = " NOT IN ";
                        }
                        return DealExpression(exp1) + methods + DealExpression(exp2);
                    }
                case "DB_Like":
                case "DB_NotLike":
                    {
                        var exp1 = k.Arguments[0];
                        var exp2 = k.Arguments[1];
                        string methods = string.Empty;
                        if (k.Method.Name == "DB_Like")
                        {
                            methods = " LIKE ";
                        }
                        else
                        {
                            methods = " NOT LIKE ";
                        }
                        return DealExpression(exp1) + methods + DealExpression(exp2);
                    }
            }
            ///   未知的函数
            throw new Exception("意外的函数");
        }
        private string DealUnary(UnaryExpression u_exp)
        {
            var m = u_exp;
            return DealExpression(u_exp.Operand);

        }
        private string DealMemberInit(MemberInitExpression mi_exp)
        {
            var i = 0;
            string exp_str = string.Empty;
            foreach (var item in mi_exp.Bindings)
            {
                MemberAssignment c = item as MemberAssignment;
                if (i == 0)
                {
                    exp_str += c.Member.Name.ToUpper() + "=" + DealExpression(c.Expression);
                }
                else
                {
                    exp_str += "," + c.Member.Name.ToUpper() + "=" + DealExpression(c.Expression);
                }
                i++;
            }
            return exp_str;

        }
        private string DealBinary(BinaryExpression exp)
        {
            return DealExpression(exp.Left) + NullValueDeal(exp.NodeType, DealExpression(exp.Right, true));// GetOperStr(exp.NodeType) + DealExpression(exp.Right, true);
        }
        private string GetOperStr(ExpressionType e_type)
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
        private string DealField(MemberExpression exp)
        {
            return Eval(exp).ToString();
        }
        private string DealMember(MemberExpression exp)
        {
            if (exp.Expression != null)
            {
                if (exp.Expression.GetType().Name == "TypedParameterExpression")
                {
                    return exp.Member.Name;
                }
                return Eval(exp).ToString();
            }


            Type type = exp.Member.ReflectedType;
            PropertyInfo propertyInfo = type.GetProperty(exp.Member.Name, BindingFlags.Static | BindingFlags.Public);
            object o;
            if (propertyInfo != null)
            {
                o = propertyInfo.GetValue(null);
            }
            else
            {
                FieldInfo field = type.GetField(exp.Member.Name, BindingFlags.Static | BindingFlags.Public);
                o = field.GetValue(null);
            }
            return GetValueFormat(o);

        }
        private string DealConstant(ConstantExpression exp)
        {
            var ccc = exp.Value.GetType();

            if (exp.Value == null)
            {
                return "NULL";
            }
            return GetValueFormat(exp.Value);
        }
        private string NullValueDeal(ExpressionType NodeType, string value)
        {
            if (value.ToUpper() != "NULL")
            {
                return GetOperStr(NodeType) + value;
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
        private string GetValueFormat(object obj)
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
        private object Eval(MemberExpression member)
        {
            var cast = Expression.Convert(member, typeof(object));
            object c = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
            return GetValueFormat(c);
        }
    }

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
    /// <summary>
    /// 静态关键字
    /// </summary>
    public class ConstantsData
    {
        public const string BinaryExpression = "BinaryExpression";
        public const string LogicalBinaryExpression = "LogicalBinaryExpression";
        public const string MethodBinaryExpression = "MethodBinaryExpression";
        public const string SimpleBinaryExpression = "SimpleBinaryExpression";
        public const string MemberExpression = "MemberExpression";
        public const string PropertyExpression = "PropertyExpression";
        public const string FieldExpression = "FieldExpression";
        public const string ConstantExpression = "ConstantExpression";
        public const string MemberInitExpression = "MemberInitExpression";
        public const string UnaryExpression = "UnaryExpression";
        public const string MethodCallExpressionN = "MethodCallExpressionN";
        public const string InstanceMethodCallExpression1 = "InstanceMethodCallExpression1";
        public const string InstanceMethodCallExpressionN = "InstanceMethodCallExpressionN";
    }
}
