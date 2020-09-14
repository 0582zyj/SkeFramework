using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.RealTimeSystems.VO
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class ClientVo
    {
        /// <summary>
        /// 应用标识
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 访问用户标识
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 访问用户IP
        /// </summary>
        public string AppIp { get; set; }
    }
}
