using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Form.Query
{
    /// <summary>
    /// 角色权限查询参数
    /// </summary>
    public class RoleManagmentQuery
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long RolesNo { get; set; }
        /// <summary>
        /// 权限类型列表
        /// </summary>
        public int[] ManagementType { get; set; }
    }
}
