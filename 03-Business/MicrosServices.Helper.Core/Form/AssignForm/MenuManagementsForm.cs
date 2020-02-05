using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Form.AssignForm
{
    public class MenuManagementsForm
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string InputUser { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public long MenuNo{ get; set; }
        /// <summary>
        /// 权限编号数组
        /// </summary>
        public long[] ManagementNos { get; set; }
    }
}
