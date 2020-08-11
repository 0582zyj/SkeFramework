using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Entities.Constants
{
    /// <summary>
    /// 日志类型枚举
    /// </summary>
    public enum LogTypeEumns
    {
        [Description("登录类型")]
        Login=100,
        [Description("拉取代码")]
        PublishGit =800,
        [Description("发布服务")]
        PublishCmd = 801,
    }
}
