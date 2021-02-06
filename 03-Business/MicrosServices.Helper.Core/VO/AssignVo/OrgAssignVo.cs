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
        /// 机构名称
        /// </summary>
        public string orgName { get; set; }
        /// <summary>
        /// 机构信息
        /// </summary>
        public PsOrganization orgInfo { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<CheckOptionValue> optionValues { get; set; }
    }
}
