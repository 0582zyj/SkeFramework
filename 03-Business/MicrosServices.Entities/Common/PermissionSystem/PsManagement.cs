using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using SkeFramework.DataBase.Common.AttributeExtend;


namespace MicrosServices.Entities.Common
{
    public class PsManagement : IEntity
    {
        public const string TableName = "ps_management";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("权限编号")]
        public Int64 ManagementNo { get; set; }
        [Description("父节点")]
        public Int64 ParentNo { get; set; }
        [Description("树节点编号")]
        public string TreeLevelNo { get; set; }
        [Description("名称")]
        public string Name { get; set; }
        [Description("描述")]
        public string Description { get; set; }
        [Description("权限值")]
        public string Value { get; set; }
        [Description("权限类型")]
        public int Type { get; set; }
        [Description("排序")]
        public int Sort { get; set; }
        [Description("平台编号")]
        public long PlatformNo { get; set; }
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
