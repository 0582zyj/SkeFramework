using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.RealTimeSystems.VO
{
    /// <summary>
    /// 链接信息
    /// </summary>
   public class ConnectVo
    {
        /// <summary>
        /// 服务端地址
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 链接ID标识
        /// </summary>
        public Guid? WebsocketId { get; set; }
    }
}
