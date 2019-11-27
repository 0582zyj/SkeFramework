using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using SkeFramework.DataBase.Common.AttributeExtend;

namespace MicrosServices.Entities.Common
{
    public class PsPlatform : IEntity
    {
        public const string TableName = "ps_platform";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("角色编号")]
        public Int64 PlatformNo { get; set; }
        [Description("权限编号")]
        public string Name { get; set; }
        [Description("")]
        public string DefaultUserName { get; set; }
        [Description("")]
        public string DefaultUserNo { get; set; }
        [Description("操作员")]
        public string InputUser { get; set; }
        [Description("操作时间")]
        public DateTime InputTime { get; set; }
        [Description("更新时间")]
        public DateTime UpdateTime { get; set; }
        public string GetTableName()
        {
            return TableName;
        }
        public string GetKey()
        {
            return "id";
        }
    }
}
