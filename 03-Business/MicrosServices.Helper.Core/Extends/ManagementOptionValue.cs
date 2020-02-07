using MicrosServices.Helper.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Extends
{
    /// <summary>
    /// 权限模型信息
    /// </summary>
    public class ManagementOptionValue : OptionValue
    {
        public new static ManagementOptionValue Default = new ManagementOptionValue()
        {
            Name = "全部",
            Value = -1,
            Code = "ALL"
        };
        /// <summary>
        /// 权限唯一码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        public int Type { get; set; }
    }
}
