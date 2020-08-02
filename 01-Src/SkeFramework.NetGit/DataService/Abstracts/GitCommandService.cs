using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataHandle.ProcessHandle;
using SkeFramework.NetGit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataService
{
    public class GitCommandService : IGitCommand
    {
        private GitProcess Process;

        public GitCommandService(GitProcess gitProcess)
        {
            Process  = gitProcess;
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

        public  ConfigResult GetFromGlobalConfig(string settingName)
        {
            return new ConfigResult(
                Process.InvokeGitOutsideEnlistment("config --global " + settingName),
                settingName);
        }

        public bool TryGetRemotes(out string[] remotes, out string error)
        {
            Result result = Process.InvokeGitInWorkingDirectoryRoot("remote", fetchMissingObjects: false);

            if (result.ExitCodeIsFailure)
            {
                remotes = null;
                error = result.Errors;
                return false;
            }

            remotes = result.Output
                            .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            error = null;
            return true;
        }
    }
}
