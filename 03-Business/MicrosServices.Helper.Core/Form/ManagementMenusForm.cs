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
        public string InputUser { get; set; }
        /// <summary>
        /// 权限编号
        /// </summary>
        public long ManagementNo { get; set; }
        /// <summary>
        /// 菜单编号数组
        /// </summary>
        public long[] MenuNos { get; set; }
    }
}
