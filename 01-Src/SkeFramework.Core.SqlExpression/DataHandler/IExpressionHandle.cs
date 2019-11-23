using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.DataHandler
{
    /// <summary>
    /// SQL转换接口
    /// </summary>
    public interface IExpressionHandle
    {
        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        string DealExpression(Expression exp, bool need = false);
    }
}
