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
        public string InputUser { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public long RolesNo { get; set; }
        /// <summary>
        /// 权限编号数组
        /// </summary>
        public long[] ManagementNos { get; set; }
    }
}
