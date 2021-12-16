using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Push.Core.Constants
{
    /// <summary>
    /// 默认设置枚举
    /// </summary>
    public enum DefaultConfigTypeEumns
    {
        [Description("初始推送线程数")]
        Workers=1,
        [Description("默认链接标识")]
        ConnectionTag = 2,
        [Description("推送消息体")]
        ResultData = 3,
        
    }
}
