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
    /// 推送记录表
    /// </summary>
    public class RtMessage : IEntity
    {
        public const string TableName = "rt_message";
        [KeyAttribute(true)]
        [Description("ID")]
        public Int64 id { get; set; }
        [Description("推送端口")]
        public string UserId { get; set; }
        [Description("应用ID")]
        public string AppId { get; set; }
        [Description("描述")]
        public string Message { get; set; }
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

