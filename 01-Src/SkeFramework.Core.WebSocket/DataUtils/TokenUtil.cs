using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataUtils
{
    /// <summary>
    /// Token工具类
    /// </summary>
   public class TokenUtil
    {
        /// <summary>
        /// 生成一个Token
        /// </summary>
        /// <returns></returns>
        public static string GeneratorToken()
        {
            return $"{Guid.NewGuid()}{Guid.NewGuid()}".Replace("-", "");
        }
    }
}
