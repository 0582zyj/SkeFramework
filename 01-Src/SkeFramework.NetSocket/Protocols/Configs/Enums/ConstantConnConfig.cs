using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSocket.Protocols.Configs.Enums
{
    /// <summary>
    /// 默认静态配置
    /// </summary>
    public class ConstantConnConfig
    {
        public static IConnectionConfig ProcessModeRequest = new DefaultConnectionConfig().SetOption(OptionKeyEnums.ProcessMode.ToString(), ProcessModeValue.Request.ToString());
    }
}
