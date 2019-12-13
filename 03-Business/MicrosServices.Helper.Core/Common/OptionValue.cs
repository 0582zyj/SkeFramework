using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Common
{
    /// <summary>
    /// List
    /// </summary>
    public class OptionValue
    {
        public static OptionValue Default = new OptionValue()
        {
            Name = "全部",
            Value = -1
        };
        /// <summary>
        /// 显示
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public long Value { get; set; }

        
    }
}
