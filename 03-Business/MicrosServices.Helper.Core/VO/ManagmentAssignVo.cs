using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core
{
    /// <summary>
    /// 权限分配信息
    /// </summary>
    public class ManagmentAssignVo
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        public PsRoles RolesInfo { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<CheckOptionValue> optionValues { get; set; }
    }
}
