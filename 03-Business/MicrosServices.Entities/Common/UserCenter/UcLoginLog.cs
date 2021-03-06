using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using SkeFramework.DataBase.Common.AttributeExtend;

namespace MicrosServices.Entities.Common
{
    public class UcLoginLog : IEntity
    {
        public const string TableName = "uc_login_log";
        [KeyAttribute(true)]
        [Description("主键")]
        public long id { get; set; }
        [Description("日志标题")]
        public string Titile { get; set; }
        [Description("日志内容")]
        public string Message { get; set; }
        [Description("日志类型")]
        public string LogType { get; set; }
        [Description("请求者")]
        public string RequestUser { get; set; }
        [Description("请求时间")]
        public DateTime RequestTime { get; set; }
        [Description("处理时间")]
        public DateTime HandleTime { get; set; }
        [Description("处理人")]
        public string HandleUser { get; set; }
        [Description("处理结果")]
        public int HandleResult { get; set; }
        [Description("处理消息")]
        public string HandleMessage { get; set; }
        [Description("访问口令")]
        public string Token { get; set; }
        [Description("过期时间")]
        public double ExpiresIn { get; set; }
        [Description("状态")]
        public int Status { get; set; }
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
