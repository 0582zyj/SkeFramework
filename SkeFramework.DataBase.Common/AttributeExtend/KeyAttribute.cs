using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.DataBase.Common.AttributeExtend
{
    /// <summary>
    /// 主键属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
        private bool _IsKeyField;
        public virtual bool IsKeyField
        {
            get { return this._IsKeyField; }
            set { this._IsKeyField = value; }
        }
        public KeyAttribute()
        {

        }
        public KeyAttribute(bool IsKeyField)
        {
            this._IsKeyField = IsKeyField;
        }
    }
}
