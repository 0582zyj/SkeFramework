using SkeFramework.Core.SqlExpression.DataUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataHandler.ClassServices
{
    /// <summary>
    /// 成员表达式
    /// </summary>
    public class MemberExpressionHandle : AbstractExpressionHandle
    {
        public MemberExpressionHandle(string expressionName) : base(expressionName) { }

        public override string DealExpression(Expression exp, bool need = false)
        {
            string sql = base.DealExpression(exp);
            if (!String.IsNullOrEmpty(sql))
            {
                return sql;
            }
            MemberExpression b_exp = exp as MemberExpression;
            if (b_exp.Expression != null)
            {
                if (b_exp.Expression.GetType().Name == "TypedParameterExpression")
                {
                    return b_exp.Member.Name;
                }
                return ExpressionTypeUtil.Eval(b_exp).ToString();
            }


            Type type = b_exp.Member.ReflectedType;
            PropertyInfo propertyInfo = type.GetProperty(b_exp.Member.Name, BindingFlags.Static | BindingFlags.Public);
            object o;
            if (propertyInfo != null)
            {
                o = propertyInfo.GetValue(null);
            }
            else
            {
                FieldInfo field = type.GetField(b_exp.Member.Name, BindingFlags.Static | BindingFlags.Public);
                o = field.GetValue(null);
            }
            return ExpressionTypeUtil.GetValueFormat(o);
        }


       
    }
}
