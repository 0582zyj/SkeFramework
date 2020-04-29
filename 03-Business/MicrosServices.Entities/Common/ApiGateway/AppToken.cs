using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Common.ApiGateway
{
    /// <summary>
    /// 应用Token
    /// </summary>
    public class AppToken
    {
        [Description("用户编号")]
        public string UserNo { get; set; }
        [Description("应用ID")]
        public string AppId { get; set; }
        [Description("密钥")]
        public string AppSecret { get; set; }
    }
}
