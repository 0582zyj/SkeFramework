using System;
using System.Collections.Generic;
using System.Text;

namespace SkeFramework.Core.WebSocketPush.DataEntities.DataCommons
{
    /// <summary>
    /// Token信息
    /// </summary>
    public class TokenValue
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public Guid SessionId { get; set; }
        /// <summary>
        /// 客户端扩展信息
        /// </summary>
        public string clientExtraProps { get; set; }
    }
}
