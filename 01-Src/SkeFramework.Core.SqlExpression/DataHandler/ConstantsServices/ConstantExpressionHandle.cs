using SkeFramework.Core.SqlExpression.DataUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataHandler.ConstantsServices
{
    /// <summary>
    /// 静态表达式
    /// </summary>
    public class ConstantExpressionHandle : AbstractExpressionHandle
    {
        public ConstantExpressionHandle(string expressionName) : base(expressionName) { }

        public override string DealExpression(Expression exp, bool need = false)
        {
            string sql = base.DealExpression(exp);
            if (!String.IsNullOrEmpty(sql))
            {
                return sql;
            }
            if(exp is ConstantExpression)
            {
                ConstantExpression expression = exp as ConstantExpression;
                var ccc = expression.Value.GetType();

                if (expression.Value == null)
                {
                    return "NULL";
                }
                return ExpressionTypeUtil.GetValueFormat(expression.Value);
            }
            return exp.ToString();


        }
    }
}
