using SkeFramework.Core.SqlExpression.DataUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataHandler.BinaryServices
{
    /// <summary>
    /// 二元表达式
    /// </summary>
    public class BinaryExpressionHandle : AbstractExpressionHandle
    {
        public BinaryExpressionHandle(string expressionName) : base(expressionName) { }

        public override string DealExpression(Expression exp, bool need = false)
        {
            string sql = base.DealExpression(exp);
            if (!String.IsNullOrEmpty(sql))
            {
                return sql;
            }
            BinaryExpression b_exp = exp as BinaryExpression;
            ExpressionType NodeTyoe = exp.NodeType;
            switch(NodeTyoe)
            {
                case ExpressionType.OrElse:
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                    return "(" + DealBinary(b_exp) + ")";
                default:
                    break;
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

        private string DealBinary(BinaryExpression exp)
        {
            return DealExpression(exp.Left) + ExpressionTypeUtil.NullValueDeal(exp.NodeType, DealExpression(exp.Right, true));
        }
    }
}
