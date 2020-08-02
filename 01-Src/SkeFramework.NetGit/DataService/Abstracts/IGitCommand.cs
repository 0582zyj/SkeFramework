using SkeFramework.NetGit.DataCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.Interfaces
{
    /// <summary>
    /// Git 常用命令接口
    /// </summary>
    public interface  IGitCommand
    {
        /// <summary>
        /// 获取Git版本信息
        /// </summary>
        /// <returns></returns>
        string GitVersion();
        /// <summary>
        /// Init命令
        /// </summary>
        /// <returns></returns>
        string GitInit();

        ConfigResult GetFromGlobalConfig(string settingName);

        bool TryGetRemotes(out string[] remotes, out string error);

    }
}
