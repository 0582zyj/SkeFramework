using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Form
{
    /// <summary>
    /// 机构角色新增
    /// </summary>
    public class OrgRolesForm
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string inputUser { get; set; }
        /// <summary>
        /// 机构编号
        /// </summary>
        public long orgNo { get; set; }
        /// <summary>
        /// 角色编号数组
        /// </summary>
        public long[] rolesNos { get; set; }
    }
}
