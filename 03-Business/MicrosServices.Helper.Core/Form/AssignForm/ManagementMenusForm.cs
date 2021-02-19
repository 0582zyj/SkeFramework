using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Form
{
    /// <summary>
    /// 权限菜单
    /// </summary>
    public class ManagementMenusForm
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string inputUser { get; set; }
        /// <summary>
        /// 权限编号
        /// </summary>
        public long managementNo { get; set; }
        /// <summary>
        /// 菜单编号数组
        /// </summary>
        public long[] menuNos { get; set; }
    }
}
