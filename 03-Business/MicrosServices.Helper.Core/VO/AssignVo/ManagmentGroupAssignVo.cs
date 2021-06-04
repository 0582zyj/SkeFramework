using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.VO.AssignVo
{
    /// <summary>
    /// 分组权限分配信息
    /// </summary>
    public class ManagmentGroupAssignVo
    {
        public string inputUser { get; set; }
        /// <summary>
        /// 分组权限信息
        /// </summary>
        public PsManagement management { get; set; }
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<CheckOptionValue> optionValues { get; set; }
    }
}
