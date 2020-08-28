using SkeFramework.DataBase.Common.AttributeExtend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Common.RealTimeSystem
{
    /// <summary>
    /// 推送服务配置表
    /// </summary>
    public class RtPushconfig : IEntity
    {
        public const string TableName = "rt_pushconfig";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("推送服务编号")]
        public Int64 PushNo { get; set; }
        [Description("推送类型")]
        public string PushType { get; set; }
        [Description("推送端口")]
        public string PushPort { get; set; }
        [Description("应用ID")]
        public string AppId { get; set; }
        [Description("描述")]
        public string Descriptions { get; set; }
        [Description("扩展信息")]
        public string ExtraProps { get; set; }
        [Description("运行状态")]
        public int Status { get; set; }
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

