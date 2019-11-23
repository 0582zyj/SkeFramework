using SkeFramework.Core.SqlExpression.Constants;
using SkeFramework.Core.SqlExpression.DataHandler;
using SkeFramework.Core.SqlExpression.DataHandler.BinaryServices;
using SkeFramework.Core.SqlExpression.DataHandler.ClassServices;
using SkeFramework.Core.SqlExpression.DataHandler.ConstantsServices;
using SkeFramework.Core.SqlExpression.DataHandler.InitializeServices;
using SkeFramework.Core.SqlExpression.DataHandler.MethodServices;
using SkeFramework.Core.SqlExpression.DataHandler.UnaryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.SqlExpression.Bootstrap.Factorys
{
    /// <summary>
    /// 工厂接口
    /// </summary>
   public class ServerFactory
    {
        public ServerFactory()
        {
        }

        public IExpressionHandle NewExpressionHandle(string ExpressionTypeName)
        {
            switch (ExpressionTypeName)
            {
                case ExpressionTypeConstants.BinaryExpression:
                case ExpressionTypeConstants.SimpleBinaryExpression:
                case ExpressionTypeConstants.LogicalBinaryExpression:
                case ExpressionTypeConstants.MethodBinaryExpression:
                    return new BinaryExpressionHandle(ExpressionTypeName);
                case ExpressionTypeConstants.MemberExpression:
                case ExpressionTypeConstants.FieldExpression:
                case ExpressionTypeConstants.PropertyExpression:
                    return new MemberExpressionHandle(ExpressionTypeName);
                case ExpressionTypeConstants.ConstantExpression:
                    return new ConstantExpressionHandle(ExpressionTypeName);
                case ExpressionTypeConstants.MemberInitExpression:
                    return new MemberInitExpressionHandle(ExpressionTypeName);
                case ExpressionTypeConstants.UnaryExpression:
                    return new UnaryExpressionHandle(ExpressionTypeName);
                case ExpressionTypeConstants.MethodCallExpressionN:
                case ExpressionTypeConstants.InstanceMethodCallExpression1:
                case ExpressionTypeConstants.InstanceMethodCallExpressionN:
                    return new MethodCallExpressionHandle(ExpressionTypeName);
                default:
                    return null;
            }
        }
    }
}
