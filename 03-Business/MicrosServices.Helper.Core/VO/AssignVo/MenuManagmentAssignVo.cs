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
    /// 菜单权限信息
    /// </summary>
   public class MenuManagmentAssignVo
    {
        /// <summary>
        /// 菜单信息
        /// </summary>
        public PsMenu menu { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<CheckOptionValue> optionValues { get; set; }
    }
}
