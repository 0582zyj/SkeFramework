using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Constants
{
    /// <summary>
    /// 消息状态列表
    /// </summary>
    public enum MessageStatusEumns
    {
        [Description("待处理")]
        Ready = 0,
        [Description("已处理")]
        Complete = 1,
        [Description("失败")]
        Failed = 400,
    }
}
