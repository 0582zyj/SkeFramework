using MicrosServices.Entities.Common.PublishDeploy;
using SkeFramework.NetGit.Constants;
using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataConfig;
using SkeFramework.NetGit.DataService.CloneServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosServices.API.PublishDeploy.Handles
{
    /// <summary>
    /// Git处理程序
    /// </summary>
    public class GitHandle
    {
        /// <summary>
        /// 拉去项目代码
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool GitProjectSourceCode(PdProject project)
        {
            string enlistmentRoot = project.SourcePath;
            string workingDirectory = project.SourcePath;
            string repoUrl = project.VersionUrl;
            string gitBinPath = project.GitBinPath;
            GitBaseConfig config = new GitAuthConfig(enlistmentRoot, workingDirectory, repoUrl, gitBinPath);
            CloneService cloneService = new CloneService(config);
            ConfigResult configResult= cloneService.GetFromLocalConfig(GitConstant.GitCommandConfig.RemoteOriginUrl);
            string value = "";
            string error = "";
            Result result;
            if (configResult.TryParseAsString(out value, out error))
            {
                result = cloneService.GitPull();
            }
            else
            {
                result = cloneService.GitClone(project.VersionUrl, project.GitBranch);
            }

            Console.WriteLine(result.Errors + " " + result.Output);

            return result.ExitCodeIsSuccess;
        }
    }
}
