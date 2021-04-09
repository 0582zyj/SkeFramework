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
    /// 短信
    /// </summary>
    public class RtShortMessage : IEntity
    {
        public const string TableName = "rt_short_message";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("应用ID")]
        public string AppId { get; set; }
        [Description("手机号码")]
        public string Mobile { get; set; }
        [Description("模板ID")]
        public string TemplateId { get; set; }
        [Description("参数")]
        public string Params { get; set; }
        [Description("消息")]
        public string Message { get; set; }
        [Description("类型")]
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
