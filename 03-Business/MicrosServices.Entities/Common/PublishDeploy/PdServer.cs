using SkeFramework.DataBase.Common.AttributeExtend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Common.PublishDeploy
{
    public class PdServer : IEntity
    {
        public const string TableName = "pd_server";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("编码")]
        public Int64 ServerNo { get; set; }
        [Description("名称")]
        public string Name { get; set; }
        [Description("IP")]
        public string IP { get; set; }
        [Description("端口")]
        public int Port { get; set; }
        [Description("描述")]
        public string Description { get; set; }
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
