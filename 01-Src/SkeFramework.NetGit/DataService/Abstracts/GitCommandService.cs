﻿using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataConfig;
using SkeFramework.NetGit.DataHandle.ProcessHandle;
using SkeFramework.NetGit.DataUtils;
using SkeFramework.NetGit.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataService
{
    public class GitCommandService : IGitCommandService
    {
        /// <summary>
        /// 日志输出
        /// </summary>
        public TextWriter Output { get; set; }
        /// <summary>
        /// Git配置
        /// </summary>
        protected GitBaseConfig gitConfig;
        /// <summary>
        /// Git进程
        /// </summary>
        protected GitProcess Process;

        public GitCommandService(GitBaseConfig config)
        {
            gitConfig = config;
            Process  = gitConfig.CreateGitProcess();
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        public string GitVersion()
        {
            Result result = Process.InvokeGitOutsideEnlistment("version");
            if (result.ExitCodeIsSuccess)
            {
                return result.Output;
            }
            return "";
        }

        /// <summary>
        /// Init命令
        /// </summary>
        /// <returns></returns>
        public string GitInit()
        {
            Result result = Process.InvokeGitOutsideEnlistment("init \"" + Process.workingDirectoryRoot + "\"");
            if (result.ExitCodeIsSuccess)
            {
                return result.Output;
            }
            return "";
        }
        
        #region Git配置
        /// <summary>
        /// 获取某个全局设置结果
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public  ConfigResult GetFromGlobalConfig(string settingName)
        {
            return new ConfigResult(
                Process.InvokeGitOutsideEnlistment("config --global " + settingName),
                settingName);
        }
        /// <summary>
        /// 获取本地配置
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public ConfigResult GetFromLocalConfig(string settingName)
        {
            return new ConfigResult(this.Process.InvokeGitAgainstDotGitFolder("config --local " + settingName), settingName);
        }
        /// <summary>
        /// 设置本地配置
        /// </summary>
        /// <param name="settingName">配置名称</param>
        /// <param name="value">值</param>
        /// <param name="replaceAll"></param>
        /// <returns></returns>
        public Result SetInLocalConfig(string settingName, string value, bool replaceAll = false)
        {
            return this.Process.InvokeGitAgainstDotGitFolder(string.Format(
                "config --local {0} \"{1}\" \"{2}\"",
                 replaceAll ? "--replace-all " : string.Empty,
                 settingName,
                 value));
        }
        /// <summary>
        /// 删除本地配置
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public Result DeleteFromLocalConfig(string settingName)
        {
            return this.Process.InvokeGitAgainstDotGitFolder("config --local --unset-all " + settingName);
        }
        #endregion

        /// <summary>
        /// 强制签出某个分支
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Result ForceCheckout(string target)
        {
            return this.Process.InvokeGitInWorkingDirectoryRoot("checkout -f " + target, fetchMissingObjects: true);
        }

        public Result ForegroundFetch(string remote)
        {
            // By using "--refmap", we override the configured refspec,
            // ignoring the normal "+refs/heads/*:refs/remotes/<remote>/*".
            // The user will see their remote refs update
            // normally when they do a foreground fetch.
            return this.Process.InvokeGitInWorkingDirectoryRoot(
                $"-c credential.interactive=never fetch {remote} --quiet",
                fetchMissingObjects: true,
                userInteractive: false);
        }
        /// <summary>
        /// 显示运行状态结果
        /// </summary>
        /// <param name="action"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected bool ShowStatusWhileRunning(Func<bool> action, string message)
        {
            return ConsoleUtil.ShowStatusWhileRunning(
                action,message,this.Output,
                showSpinner: this.Output == Console.Out, initialDelayMs: 0);
        }
    }
}
