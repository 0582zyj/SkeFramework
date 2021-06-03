using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Form.AssignForm
{
    /// <summary>
    /// 菜单权限新增
    /// </summary>
    public class MenuManagementsForm
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string inputUser { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public long menuNo{ get; set; }
        /// <summary>
        /// 权限编号数组
        /// </summary>
        public long[] managementNos { get; set; }
    }
}
