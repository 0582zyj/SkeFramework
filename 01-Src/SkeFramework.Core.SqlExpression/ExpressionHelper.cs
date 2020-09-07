using SkeFramework.Core.SqlExpression.Bootstrap.Factorys;
using SkeFramework.Core.SqlExpression.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression
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

        private ServerFactory serverFactory = new ServerFactory();
        private IExpressionHandle expressionHandle;
        /// <summary>
        /// 获取SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public string GetSql<T, TKey>(Expression<Func<T, TKey>> exp)
        {
            Expression ex = exp.Body;
            string name = ex.GetType().Name;
            expressionHandle = serverFactory.NewExpressionHandle(name);
            if (expressionHandle != null)
            {
                return expressionHandle.DealExpression(ex);
            }
            return "error";
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
            Expression ex = exp.Body;
            string name = ex.GetType().Name;
            expressionHandle = serverFactory.NewExpressionHandle(name);
            if (expressionHandle != null)
            {
                return expressionHandle.DealExpression(ex);
            }
            return "error";
        }

    }
}
