using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.UserCenter.FORM
{
    /// <summary>
    /// 登录信息类
    /// </summary>
    public class LoginInfoForm
    {
        public string UserName { get; set; }
        public string Md5Pas { get; set; }
        public string LoginerInfo { get; set; }
        public string Platform { get; set; }
    }
}
