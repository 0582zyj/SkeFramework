using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataHandler.InitializeServices
{
    public class MemberInitExpressionHandle : AbstractExpressionHandle
    {
        public MemberInitExpressionHandle(string expressionName) : base(expressionName) { }

        public override string DealExpression(Expression exp, bool need = false)
        {
            string sql = base.DealExpression(exp);
            if (!String.IsNullOrEmpty(sql))
            {
                return sql;
            }
            MemberInitExpression expression = exp as MemberInitExpression;
            var i = 0;
            string exp_str = string.Empty;
            foreach (var item in expression.Bindings)
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
    }
}
