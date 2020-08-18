using SkeFramework.Core.SqlExpression.Bootstrap.Factorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataHandler
{
    public abstract class AbstractExpressionHandle : IExpressionHandle
    {
        private string ExpressionName = "";
        private ServerFactory serverFactory = new ServerFactory();

        public AbstractExpressionHandle(string expressionName)
        {
            this.ExpressionName = expressionName;
        }

        public virtual string DealExpression(Expression exp, bool need = false)
        {
            string name = exp.GetType().Name;
            if (!name.Equals(this.ExpressionName))
            {
                IExpressionHandle expression = serverFactory.NewExpressionHandle(name);
                if (expression != null)
                {
                    return expression.DealExpression(exp);
                }
            }
            return "";
        }
    }
}
