using SkeFramework.Core.SqlExpression.DataUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataHandler.MethodServices
{
    public class MethodCallExpressionHandle : AbstractExpressionHandle
    {
        public MethodCallExpressionHandle(string expressionName) : base(expressionName) { }

        public override string DealExpression(Expression exp, bool need = false)
        {
            string sql = base.DealExpression(exp);
            if (!String.IsNullOrEmpty(sql))
            {
                return sql;
            }
            MethodCallExpression m_exp = exp as MethodCallExpression;
            var k = m_exp;
            var g = k.Arguments[0];
            if (k.Method.DeclaringType.FullName == "System.Convert")
            {
                return DealExpression(g);
            }
            var exp1 = k.Arguments[0];
            var exp2 = k.Object;
            string methods = " IN ";
            switch (k.Method.Name)
            {
                case "Contains":
                    if (k.Method.DeclaringType.Name.Contains("List`1"))
                    {
                        methods = " IN ";
                        return DealExpression(exp1) + methods + DealExpression(exp2);
                    }
                    else
                    {
                        methods = " LIKE ";
                        char[] trimChars = "'".ToCharArray();
                        return DealExpression(exp2) + methods + "'%" + DealExpression(exp1).Trim(trimChars) + "%'";
                    }
                case "Equals":
                    methods = " = ";
                    return DealExpression(exp1) + methods + DealExpression(exp2);
                case "DB_Length":
                    {
                        exp1 = k.Arguments[0];
                        return "LEN(" + DealExpression(exp1) + ")";
                    }
                case "DB_In":
                case "DB_NotIn":
                    {
                        exp1 = k.Arguments[0];
                        exp2 = k.Arguments[1];
                        methods = string.Empty;
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
                        exp1 = k.Arguments[0];
                        exp2 = k.Arguments[1];
                        methods = string.Empty;
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
                default:
                    if (k.Method.DeclaringType != typeof(SQLMethods))
                    {
                        throw new Exception("无法识别函数");
                    }
                    else
                    {
                        ///   未知的函数
                        throw new Exception("意外的函数");
                    }
            }
        }
    }
}
