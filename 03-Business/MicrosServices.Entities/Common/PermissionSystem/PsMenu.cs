using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using SkeFramework.DataBase.Common.AttributeExtend;

namespace MicrosServices.Entities.Common
{
    public class PsMenu : IEntity
    {
        public const string TableName = "ps_menu";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("菜单编号")]
        public Int64 MenuNo { get; set; }
        [Description("父节点")]
        public Int64 ParentNo { get; set; }
        [Description("树节点编号")]
        public string TreeLevelNo { get; set; }
        [Description("名称")]
        public string Name { get; set; }
        [Description("权限值")]
        public string Value { get; set; }
        [Description("图标")]
        public string icon { get; set; }
        [Description("跳转地址")]
        public string url { get; set; }
        [Description("排序")]
        public int Sort { get; set; }
        [Description("")]
        public long PlatformNo { get; set; }
        [Description("")]
        public int Enabled { get; set; }
        [Description("操作员")]
        public string InputUser { get; set; }
        [Description("操作时间")]
        public DateTime InputTime { get; set; }
        [Description("")]
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
