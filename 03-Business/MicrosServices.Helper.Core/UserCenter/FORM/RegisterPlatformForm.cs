using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.UserCenter.FORM
{
    /// <summary>
    /// 平台注册信息
    /// </summary>
   public class RegisterPlatformForm
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserNo { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string InputUser { get; set; }
        /// <summary>
        /// 平台编号
        /// </summary>
        public long PlatformNo { get; set; }
    }
}
