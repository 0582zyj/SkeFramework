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
    /// 机构角色分配
    /// </summary>
    public class OrgAssignVo
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public PsOrganization OrgInfo { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<CheckOptionValue> optionValues { get; set; }
    }
}
