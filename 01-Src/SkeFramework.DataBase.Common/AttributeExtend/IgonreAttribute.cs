using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.DataBase.Common.AttributeExtend
{
    /// <summary>
    /// 忽视的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgonreAttribute : Attribute
    {
        /// <summary>
        /// 是否忽视
        /// </summary>
        private bool _IsIgnore;
        public virtual bool IsIgnore
        {
            get { return this._IsIgnore; }
            set { this._IsIgnore = value; }
        }
        public IgonreAttribute()
        {

        }
        public IgonreAttribute(bool IsIgnore)
        {
            this._IsIgnore = IsIgnore;
        }
    }
}