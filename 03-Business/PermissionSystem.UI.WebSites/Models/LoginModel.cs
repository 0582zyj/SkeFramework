using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PermissionSystem.UI.WebSites.Models
{
    public class LoginModel
    {
        private static LoginModel _simpleInstance = null;

        public static LoginModel Instance()
        {
            if (_simpleInstance == null)
            {
                _simpleInstance = new LoginModel();
            }
            return _simpleInstance;
        }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserNo { get; set; }
        /// <summary>
        /// 通信密钥
        /// </summary>
        public string Token { get;set;}
        /// <summary>
        /// 权限值
        /// </summary>
        public long ManagementValue { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public string UserRolesName { get; set; }
        /// <summary>
        /// 账号规则
        /// </summary>
        public string UserRule { get; set; }
    }
}