using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Common.ExtendAttributes
{
    /// <summary>
    /// 枚举描述属性
    /// </summary>
    public class EnumAttribute : DescriptionAttribute
    {
        /// <summary>
        /// 枚举扩展值
        /// </summary>
        public virtual string Code { get; }
        /// <summary>
        /// 枚举额外值
        /// </summary>
        public object ExtraValue { get; }


        public EnumAttribute(string desc) : this(desc, "", null)
        {

        }

        public EnumAttribute(string desc, string code) : this(desc, code, null)
        {

        }

        public EnumAttribute(string desc, string code, object extraValue) : base(desc)
        {
            this.Code = code;
            this.ExtraValue = extraValue;
        }

    }
}
