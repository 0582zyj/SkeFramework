using SkeFramework.DataBase.Common.AttributeExtend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Common.BaseSystem
{
    public class BsDictionary : IEntity
    {
        public const string TableName = "bs_dictionary";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("编码")]
        public string DicCode { get; set; }
        [Description("键")]
        public string DicKey { get; set; }
        [Description("值")]
        public string DicValue { get; set; }
        [Description("描述")]
        public string Descriptions { get; set; }
        [Description("平台编号")]
        public Int64 PlatformNo { get; set; }
        [Description("启用状态")]
        public int Enabled { get; set; }
        [Description("操作员")]
        public string InputUser { get; set; }
        [Description("操作时间")]
        public DateTime InputTime { get; set; }
        [Description("更新人")]
        public string UpdateUser { get; set; }
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

