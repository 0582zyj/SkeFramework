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
    /// 菜单分配信息
    /// </summary>
    public class MenuAssignVo
    {
        /// <summary>
        /// 权限信息
        /// </summary>
        public PsManagement ManagementInfo { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<CheckOptionValue> optionValues { get; set; }
    }
}
