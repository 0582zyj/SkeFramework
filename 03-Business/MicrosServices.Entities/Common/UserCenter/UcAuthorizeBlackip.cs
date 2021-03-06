using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using SkeFramework.DataBase.Common.AttributeExtend;

namespace MicrosServices.Entities.Common
{
    public class UcAuthorizeBlackip : IEntity
    {
        public const string TableName = "uc_authorize_blackip";
        [KeyAttribute(true)]
        [Description("主键")]
        public int id { get; set; }
        [Description("标题")]
        public string Name { get; set; }
        [Description("内容")]
        public string Message { get; set; }
        [Description("类型[0>永久；1》单次；2》循环；3限时]")]
        public int AuthorizeType { get; set; }
        [Description("次数")]
        public string AuthorizeCount { get; set; }
        [Description("开始日期")]
        public DateTime StartDate { get; set; }
        [Description("结束日期")]
        public DateTime EndDate { get; set; }
        [Description("重复星期制【1111111】")]
        public int RepeatWeek { get; set; }
        [Description("开始时间")]
        public DateTime StartTime { get; set; }
        [Description("结束时间")]
        public DateTime EndTime { get; set; }
        [Description("开始IP")]
        public string StartIP { get; set; }
        [Description("结束IP")]
        public string EndIP { get; set; }
        [Description("状态")]
        public int Enabled { get; set; }
        [Description("输入人")]
        public string InputUser { get; set; }
        [Description("输入时间")]
        public DateTime InputTime { get; set; }
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
