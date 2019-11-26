using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Enums.ExtendAttribute
{
    /// <summary>
    /// 枚举扩展属性
    /// </summary>
    public class EnumAttribute : Attribute
    {
        public EnumAttribute(int type, string desc)
        {
            this.Type = type;
            this.Desc = desc;
        }
        public int Type { get; private set; }
        public string Desc { get; private set; }
    }
}
