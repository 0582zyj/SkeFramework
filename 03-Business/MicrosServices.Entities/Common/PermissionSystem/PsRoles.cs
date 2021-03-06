using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using SkeFramework.DataBase.Common.AttributeExtend;

namespace MicrosServices.Entities.Common
{
    public class PsRoles : IEntity
    {
        public const string TableName = "ps_roles";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("角色编号")]
        public Int64 RolesNo { get; set; }
        [Description("父节点")]
        public long ParentNo { get; set; }
        [Description("树节点编号")]
        public string TreeLevelNo { get; set; }
        [Description("角色名称")]
        public string Name { get; set; }
        [Description("描述")]
        public string Description { get; set; }
        [Description("权限值")]
        public string ManagementValue { get; set; }
        [Description("平台编号")]
        public int PlatformNo { get; set; }
        [Description("启用状态")]
        public int Enabled { get; set; }
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
