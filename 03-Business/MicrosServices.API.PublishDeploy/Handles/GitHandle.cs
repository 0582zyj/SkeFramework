using MicrosServices.API.PublishDeploy.Constants;
using MicrosServices.BLL.Business;
using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Constants;
using SkeFramework.Core.Common.Enums;
using Newtonsoft.Json;
using SkeFramework.NetGit.Constants;
using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataConfig;
using SkeFramework.NetGit.DataHandle.ProcessHandle;
using SkeFramework.NetGit.DataService.CloneServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public bool GitProjectSourceCode(PdProject project, string RequestUser)
        {
            string enlistmentRoot = project.SourcePath;
            string workingDirectory = project.SourcePath;
            string repoUrl = project.VersionUrl;
            string gitBinPath = project.GitBinPath;
            GitBaseConfig config = new GitAuthConfig(enlistmentRoot, workingDirectory, repoUrl, gitBinPath);
            CloneService cloneService = new CloneService(config);
            ConfigResult configResult = cloneService.GetFromLocalConfig(GitConstant.GitCommandConfig.RemoteOriginUrl);
            string value = "";
            string error = "";
            Result result;
            if (configResult.TryParseAsString(out value, out error))
            {
                ConfigResult configResult1 = cloneService.GetFromLocalConfig($"branch.{project.GitBranch}.remote");
                if (configResult1.TryParseAsString(out value, out error) && !String.IsNullOrEmpty(value))
                {
                    result = cloneService.GitPull();
                }
                else
                {
                    result = cloneService.ForceCheckout(project.GitBranch);
                }
            }
            else
            {
                result = cloneService.GitClone(project.VersionUrl, project.GitBranch);
            }
            string message = JsonConvert.SerializeObject(project);
            string HandleUser = ServerConstData.ServerName;
            LoginResultType resultType = result.ExitCode == 0 ? LoginResultType.SUCCESS_PUBLISHGIT : LoginResultType.FAILED;
            int HandleResult = (int)resultType;
            DataHandleManager.Instance().UcLoginLogHandle.
                InsertPublishDeployGitLog(RequestUser, message, HandleUser, HandleResult, result.Output);
            return result.ExitCodeIsSuccess;
        }
        /// <summary>
        /// 执行发布命令
        /// </summary>
        /// <param name="project"></param>
        /// <param name="RequestUser"></param>
        /// <returns></returns>
        public bool RunPublishBat(PdProject project, string RequestUser)
        {
            if (project == null)
            {
                return false;
            }
            string batPath = project.SourcePath + project.ProjectFile;
            if (File.Exists(batPath))
            {
                FileInfo fileInfo = new FileInfo(batPath);
                if (!fileInfo.Exists)
                {
                    return false;
                }
                string enlistmentRoot = project.SourcePath;
                string workingDirectory = fileInfo.Directory.ToString();
                string repoUrl = project.VersionUrl;
                string gitBinPath = project.MSBuildPath;
                GitBaseConfig config = new GitAuthConfig(enlistmentRoot, workingDirectory, repoUrl, gitBinPath);
                GitProcess process = config.CreateGitProcess();
                int exitCode = -1;

                List<string> commandList = new List<string>();
                commandList.Add(fileInfo.Name);
                List<string> reulit = this.Shell("cmd.exe", "/k ", 5 * 60 * 1000, fileInfo.Directory.ToString(), out exitCode, commandList.ToArray());

                LoginResultType resultType = exitCode == 0 && reulit.Contains("    0 个错误") ? LoginResultType.SUCCESS_PUBLISHCMD : LoginResultType.FAILED;
                string message = JsonConvert.SerializeObject(project);
                string HandleUser = ServerConstData.ServerName;
                int HandleResult = (int)resultType;
                string HandleMessage = resultType == LoginResultType.SUCCESS_PUBLISHCMD ? resultType.GetEnumDescription() : String.Join(";", reulit);
                DataHandleManager.Instance().UcLoginLogHandle.
                InsertCommonLog(RequestUser, message, LogTypeEumns.PublishCmd, HandleUser, HandleResult, HandleMessage);
                if (exitCode == 0 && reulit.Contains("    0 个错误"))
                {
                    return true;
                }
            }
            else
            {
                DataHandleManager.Instance().UcLoginLogHandle.
               InsertPublishDeployGitLog(RequestUser, "batPath:" + batPath, ServerConstData.ServerName, 400, "文件不存在;");
            }
            return false;
        }

        #region Cmd执行某个Bat文件
        private List<string> Shell(string exeFile, string Arguments,
            int timeout, string workingDir, out int exitCode, params string[] command)
        {

            List<string> response = new List<string>();
            List<string> output = new List<string>();
            List<string> error = new List<string>();
            Process process = new Process();

            process.StartInfo.FileName = exeFile; //设置要启动的应用程序，如：fastboot
            process.StartInfo.Arguments = Arguments; // 设置应用程序参数，如： flash boot0 "A_Debug/boot0.img"
            process.StartInfo.Verb = "runas";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.EnableRaisingEvents = true;  // 获取或设置在进程终止时是否应激发 Exited 事件；不论是正常退出还是异常退出。
            process.StartInfo.WorkingDirectory = workingDir; // **重点**，工作目录，必须是 bat 批处理文件所在的目录
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => Redirected(output, sender, e);
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => Redirected(error, sender, e);

            process.Start();
            //向cmd窗口发送输入信息
            int lenght = command.Length;
            if (lenght > 0)
            {
                foreach (string com in command)
                {
                    process.StandardInput.WriteLine(com);//输入CMD命令
                }
                process.StandardInput.WriteLine("exit");//结束执行，很重要的
            }
            process.BeginOutputReadLine();  // 开启异步读取输出操作
            process.BeginErrorReadLine();  // 开启异步读取错误操作

            bool exited = process.WaitForExit(timeout);
            if (!exited)
            {
                process.Kill();  // 通过超时判断是否执行失败，极可能为假死状态。
                // 记录日志
                response.Add("Error: timed out");
            }

            response.AddRange(output);
            response.AddRange(error);
            exitCode = process.ExitCode; // 0 为正常退出。

            return response;
        }
        private void Redirected(List<string> dataList, object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null) { dataList.Add(e.Data); }
        }
        #endregion
    }
}
