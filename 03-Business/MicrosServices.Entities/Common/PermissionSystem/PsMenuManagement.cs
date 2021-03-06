using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using SkeFramework.DataBase.Common.AttributeExtend;

namespace MicrosServices.Entities.Common
{
    public class PsMenuManagement : IEntity
    {
        public const string TableName = "ps_menu_management";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("角色名称")]
        public string Name { get; set; }
        [Description("描述")]
        public string Description { get; set; }
        [Description("权限值")]
        public Int64 MenuNo { get; set; }
        [Description("")]
        public Int64 ManagementNo { get; set; }
        [Description("启用状态")]
        public int Type { get; set; }
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
