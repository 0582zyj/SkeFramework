using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.VO
{
    /// <summary>
    /// 用户角色分配
    /// </summary>
   public class RolesAssignVo
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        private long userNo { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UcUsersSetting UsersSettingInfo { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<CheckOptionValue> optionValues { get; set; }
    }
}
