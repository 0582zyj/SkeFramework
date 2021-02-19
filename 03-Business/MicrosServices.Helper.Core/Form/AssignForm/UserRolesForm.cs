using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Form
{
    /// <summary>
    /// 用户角色新增
    /// </summary>
    public class UserRolesForm
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string inputUser { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userNo { get; set; }
        /// <summary>
        /// 角色编号数组
        /// </summary>
        public long[] rolesNos { get; set; }
    }
}
