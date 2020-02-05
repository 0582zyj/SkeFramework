using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Form
{
    /// <summary>
    /// 机构角色
    /// </summary>
    public class OrgRolesForm
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string InputUser { get; set; }
        /// <summary>
        /// 机构编号
        /// </summary>
        public long OrgNo { get; set; }
        /// <summary>
        /// 角色编号数组
        /// </summary>
        public long[] RolesNos { get; set; }
    }
}
