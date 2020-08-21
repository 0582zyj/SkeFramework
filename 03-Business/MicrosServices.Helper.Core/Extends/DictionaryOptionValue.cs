using MicrosServices.Helper.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Extends
{
    public class DictionaryOptionValue: OptionValue
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string DicType { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public string DicKey { get; set; }
    }
}
