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
    /// 邮件记录
    /// </summary>
    public class RtEmail : IEntity
    {
        public const string TableName = "rt_email";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("应用ID")]
        public string AppId { get; set; }
        [Description("邮件发送人")]
        public string FromEmail { get; set; }
        [Description("邮件接收人")]
        public string ToEmail { get; set; }
        [Description("主题")]
        public string Subject { get; set; }
        [Description("消息")]
        public string Message { get; set; }
        [Description("内容类型")]
        public string Type { get; set; }
        [Description("运行状态")]
        public int Status { get; set; }
        [Description("创建时间")]
        public DateTime InputTime { get; set; }
        [Description("处理时间")]
        public DateTime HandleTime { get; set; }
        [Description("处理结果")]
        public string HandleResult { get; set; }
        [Description("到达时间")]
        public DateTime AvailTime { get; set; }
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
