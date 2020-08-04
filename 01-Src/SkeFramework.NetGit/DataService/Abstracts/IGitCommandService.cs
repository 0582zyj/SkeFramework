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
    public interface  IGitCommandService
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
        /// <summary>
        /// 设置本地配置项
        /// </summary>
        /// <param name="settingName">配置名称</param>
        /// <param name="value">值</param>
        /// <param name="replaceAll"></param>
        /// <returns></returns>
        Result SetInLocalConfig(string settingName, string value, bool replaceAll = false);
        /// <summary>
        /// 删除本地配置项
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        Result DeleteFromLocalConfig(string settingName);


        Result ForceCheckout(string target);
       
        Result ForegroundFetch(string remote);
      
    }
}
