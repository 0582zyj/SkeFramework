using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Form
{
    /// <summary>
    /// 角色权限新增
    /// </summary>
   public class ManagementRolesForm
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string inputUser { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public long rolesNo { get; set; }
        /// <summary>
        /// 权限编号数组
        /// </summary>
        public long[] managementNos { get; set; }
    }
}
