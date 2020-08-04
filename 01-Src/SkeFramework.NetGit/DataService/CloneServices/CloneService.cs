using SkeFramework.NetGit.Constants;
using SkeFramework.NetGit.DataCommon;
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
using static SkeFramework.NetGit.Constants.GitConstant;

namespace SkeFramework.NetGit.DataService.CloneServices
{
    /// <summary>
    /// 克隆服务
    /// </summary>
    public class CloneService: GitCommandService, IGitCommandService
    {
        private bool FullClone = false;


        public CloneService(GitBaseConfig config):base(config)
        {
            this.Output = Console.Out;
        }
        /// <summary>
        /// 克隆项目
        /// </summary>
        /// <param name="RepoUrl"></param>
        /// <param name="Branch"></param>
        /// <returns></returns>
        public Result GitClone(string RepoUrl,string Branch)
        {
            string gitBinPath = this.GitVersion(); 
            if (string.IsNullOrWhiteSpace(gitBinPath))
            {
                return new Result(GitConstant.GitIsNotInstalledError);
            }
            // protocol.version=2 is broken right now.
            this.SetInLocalConfig("protocol.version", "1");
            this.SetInLocalConfig("remote.origin.url", RepoUrl);
            this.SetInLocalConfig("remote.origin.fetch", "+refs/heads/*:refs/remotes/origin/*");
            this.SetInLocalConfig("remote.origin.promisor", "true");
            this.SetInLocalConfig("remote.origin.partialCloneFilter", "blob:none");
            string branch = Branch ?? "master";
            this.SetInLocalConfig($"branch.{branch}.remote", "origin");
            this.SetInLocalConfig($"branch.{branch}.merge", $"refs/heads/{branch}");
            if (!this.FullClone)
            {
                this.SparseCheckoutInit(null);
            }
            Result fetchResult = null;
            bool result = !this.ShowStatusWhileRunning(() => 
            {
                    fetchResult = this.ForegroundFetch("origin");
                    return fetchResult.ExitCodeIsSuccess;
            }, "Fetching objects from remote");
            if (result)
            {
                if (!fetchResult.Errors.Contains("filtering not recognized by server"))
                {
                    return new Result($"Failed to complete regular clone: {fetchResult?.Errors}");
                }
            }
            result = !this.ShowStatusWhileRunning(() =>
            {
                this.DeleteFromLocalConfig("remote.origin.promisor");
                this.DeleteFromLocalConfig("remote.origin.partialCloneFilter");
                fetchResult = this.ForegroundFetch("origin");
                return fetchResult.ExitCodeIsSuccess;
            }, "Fetching objects from remote");
            if (fetchResult.ExitCodeIsFailure && result)
            {
                return new Result($"Failed to complete regular clone: {fetchResult?.Errors}");
            }
            Result checkoutResult = null;
            result = !this.ShowStatusWhileRunning(() =>
            {
                checkoutResult = this.ForceCheckout(branch);
                return checkoutResult.ExitCodeIsSuccess;
            },$"Checking out '{branch}'");
            if (result)
            {
                return new Result($"Failed to complete regular clone: {checkoutResult?.Errors}");
            }
            return new Result("","",Result.SuccessCode);
        }
        /// <summary>
        /// 签出
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public  Result SparseCheckoutInit(GitBaseConfig config)
        {
            if (config == null)
            {
                return this.Process.InvokeGitInWorkingDirectoryRoot("sparse-checkout init --cone", fetchMissingObjects: true);
            }
            return config.CreateGitProcess().InvokeGitInWorkingDirectoryRoot("sparse-checkout init --cone", fetchMissingObjects: true);
        }
        /// <summary>
        /// 拉取命令
        /// </summary>
        /// <returns></returns>
        public Result GitPull()
        {
            return this.Process.InvokeGitInWorkingDirectoryRoot("pull", fetchMissingObjects: true);
        }

        

    }
}
