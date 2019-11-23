using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataHandler.UnaryServices
{
    public class UnaryExpressionHandle : AbstractExpressionHandle
    {
        public UnaryExpressionHandle(string expressionName):base(expressionName) { }

        public override string DealExpression(Expression exp, bool need = false)
        {
            base.DealExpression(exp);
            UnaryExpression expression = exp as UnaryExpression;
            return DealExpression(expression.Operand);
        }
    }
}
