using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Core.Data.Vo
{
    /// <summary>
    /// 登录操作者信息
    /// </summary>
    public class OperatorVo
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userNo { get; set; }
        /// <summary>
        /// 平台编号
        /// </summary>
        public long platformNo { get; set; }
    }
}
