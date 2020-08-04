using SkeFramework.NetGit.Constants;
using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataHandle.ProcessHandle
{
    /// <summary>
    /// Git进程
    /// </summary>
    public class GitProcess
    {
        private static readonly Encoding UTF8NoBOM = new UTF8Encoding(false);

        /// <summary>
        /// Git程序路径
        /// </summary>
        private string gitBinPath;
        /// <summary>
        /// 工作路径
        /// </summary>
        public string workingDirectoryRoot;
        /// <summary>
        /// Git代码路径
        /// </summary>
        private string dotGitRoot;
        /// <summary>
        /// 执行进程
        /// </summary>
        private Process executingProcess;
        /// <summary>
        /// 是否停止
        /// </summary>
        private bool stopping;

        /// <summary>
        /// Lock taken for duration of running executingProcess.
        /// </summary>
        private object executionLock = new object();

        /// <summary>
        /// Lock taken when changing the running state of executingProcess.
        ///
        /// Can be taken within executionLock.
        /// </summary>
        private object processLock = new object();

        public bool LowerPriority { get; set; }

        #region 构造函数
        public GitProcess(GitBaseConfig authConfig)
       : this(authConfig.GitBinPath, authConfig.WorkingDirectoryRoot)
        {
            stopping = false;
        }

        public GitProcess(string gitBinPath, string workingDirectoryRoot)
        {
            if (string.IsNullOrWhiteSpace(gitBinPath))
            {
                throw new ArgumentException(nameof(gitBinPath));
            }

            this.gitBinPath = gitBinPath;
            this.workingDirectoryRoot = workingDirectoryRoot;

            if (this.workingDirectoryRoot != null)
            {
                this.dotGitRoot = Path.Combine(this.workingDirectoryRoot, GitConstant.DotGit.Root);
            }
        }
        #endregion

        #region Command
        /// <summary>
        /// 获取远程Git地址
        /// </summary>
        /// <returns></returns>
        public ConfigResult GetOriginUrl()
        {
            return new ConfigResult(this.InvokeGitAgainstDotGitFolder("config --local remote.origin.url"), "remote.origin.url");
        }

        public bool TryGetRemotes(out string[] remotes, out string error)
        {
            Result result = this.InvokeGitInWorkingDirectoryRoot("remote", fetchMissingObjects: false);

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

        public ConfigResult GetFromLocalConfig(string settingName)
        {
            return new ConfigResult(this.InvokeGitAgainstDotGitFolder("config --local " + settingName), settingName);
        }



        #endregion

        #region InvokeGit

        public Result InvokeGitInWorkingDirectoryRoot(string command,bool fetchMissingObjects,Action<StreamWriter> writeStdIn = null,Action<string> parseStdOutLine = null,bool userInteractive = true)
        {
            return this.InvokeGitImpl(command,workingDirectory: this.workingDirectoryRoot,dotGitDirectory: null,fetchMissingObjects: fetchMissingObjects,
                writeStdIn: writeStdIn,parseStdOutLine: parseStdOutLine,timeoutMs: -1, userInteractive: userInteractive);
        }

        /// <summary>
        /// Invokes git.exe without a working directory set.
        /// </summary>
        /// <remarks>
        /// For commands where git doesn't need to be (or can't be) run from inside an enlistment.
        /// eg. 'git init' or 'git version'
        /// </remarks>
        public Result InvokeGitOutsideEnlistment(string command)
        {
            return this.InvokeGitOutsideEnlistment(command, null, null);
        }

        public Result InvokeGitOutsideEnlistment(string command, Action<StreamWriter> writeStdIn,Action<string> parseStdOutLine,int timeout = -1)
        {
            return this.InvokeGitImpl(
                command,
                workingDirectory: Environment.SystemDirectory,
                dotGitDirectory: null,
                fetchMissingObjects: false,
                writeStdIn: writeStdIn,
                parseStdOutLine: parseStdOutLine,
                timeoutMs: timeout);
        }
        /// <summary>
        /// 对征用的.git文件夹调用git.exe。
        /// 此方法应该仅用于忽略工作目录的git命令
        /// </summary>

        public Result InvokeGitAgainstDotGitFolder(string command)
        {
            return this.InvokeGitAgainstDotGitFolder(command, null, null);
        }


        public Result InvokeGitAgainstDotGitFolder(
            string command,
            Action<StreamWriter> writeStdIn,
            Action<string> parseStdOutLine,
            string gitObjectsDirectory = null)
        {
            // This git command should not need/use the working directory of the repo.
            // Run git.exe in Environment.SystemDirectory to ensure the git.exe process
            // does not touch the working directory
            return this.InvokeGitImpl(
                command,
                workingDirectory: Environment.SystemDirectory,
                dotGitDirectory: this.dotGitRoot,
                fetchMissingObjects: false,
                writeStdIn: writeStdIn,
                parseStdOutLine: parseStdOutLine,
                timeoutMs: -1,
                gitObjectsDirectory: gitObjectsDirectory);
        }



        public virtual Result InvokeGitImpl(
           string command,
           string workingDirectory,
           string dotGitDirectory,
           bool fetchMissingObjects,
           Action<StreamWriter> writeStdIn,
           Action<string> parseStdOutLine,
           int timeoutMs,
           string gitObjectsDirectory = null,
           bool userInteractive = true)
        {
            //if (writeStdIn != null)
            //{
            //    return new Result(string.Empty, "Attempting to use to stdin, but the process does not have the right input encodings set.", Result.GenericFailureCode);
            //}

            try
            {
                // From https://msdn.microsoft.com/en-us/library/system.diagnostics.process.standardoutput.aspx
                // To avoid deadlocks, use asynchronous read operations on at least one of the streams.
                // Do not perform a synchronous read to the end of both redirected streams.
                using (this.executingProcess = this.GetGitProcess(
                                                        command,
                                                        workingDirectory,
                                                        dotGitDirectory,
                                                        fetchMissingObjects: fetchMissingObjects,
                                                        redirectStandardError: true,
                                                        gitObjectsDirectory: gitObjectsDirectory,
                                                        userInteractive: userInteractive))
                {
                    StringBuilder output = new StringBuilder();
                    StringBuilder errors = new StringBuilder();

                    this.executingProcess.ErrorDataReceived += (sender, args) =>
                    {
                        if (args.Data != null)
                        {
                            errors.Append(args.Data + "\n");
                        }
                    };
                    this.executingProcess.OutputDataReceived += (sender, args) =>
                    {
                        if (args.Data != null)
                        {
                            if (parseStdOutLine != null)
                            {
                                parseStdOutLine(args.Data);
                            }
                            else
                            {
                                output.Append(args.Data + "\n");
                            }
                        }
                    };

                    lock (this.executionLock)
                    {
                        lock (this.processLock)
                        {
                            if (this.stopping)
                            {
                                return new Result(string.Empty, nameof(GitProcess) + " is stopping", Result.FailureCode);
                            }

                            this.executingProcess.Start();

                            this.executingProcess.BeginOutputReadLine();
                            this.executingProcess.BeginErrorReadLine();

                            try
                            {
                                if (this.LowerPriority)
                                {
                                    this.executingProcess.PriorityClass = ProcessPriorityClass.BelowNormal;
                                }

                                if (writeStdIn != null)
                                {
                                    writeStdIn.Invoke(this.executingProcess.StandardInput);
                                    this.executingProcess.StandardInput.Close();
                                }
                            }
                            catch (InvalidOperationException)
                            {
                                // This is thrown if the process completes before we can set a property.
                            }
                            catch (Win32Exception)
                            {
                                // This is thrown if the process completes before we can set a property.
                            }

                            if (!this.executingProcess.WaitForExit(timeoutMs))
                            {
                                this.executingProcess.Kill();

                                return new Result(output.ToString(), "Operation timed out: " + errors.ToString(), Result.FailureCode);
                            }
                        }

                        return new Result(output.ToString(), errors.ToString(), this.executingProcess.ExitCode);
                    }
                }
            }
            catch (Win32Exception e)
            {
                return new Result(string.Empty, e.Message, Result.FailureCode);
            }
            finally
            {
                this.executingProcess = null;
            }
        }
        /// <summary>
        /// 获取Git进程
        /// </summary>
        /// <param name="command">命令行</param>
        /// <param name="workingDirectory">工作路径</param>
        /// <param name="dotGitDirectory">获取路径</param>
        /// <param name="fetchMissingObjects"></param>
        /// <param name="redirectStandardError"></param>
        /// <param name="gitObjectsDirectory"></param>
        /// <param name="userInteractive"></param>
        /// <returns></returns>
        private Process GetGitProcess( string command,string workingDirectory,string dotGitDirectory, bool fetchMissingObjects,
          bool redirectStandardError, string gitObjectsDirectory, bool userInteractive = true)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo(this.gitBinPath);
            //设置工作路径
            processInfo.WorkingDirectory = workingDirectory;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardInput = true;
            //进行输出的重定向
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = redirectStandardError;
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //表示不要为这个命令单独创建一个控制台窗口
            processInfo.CreateNoWindow = false;

            processInfo.StandardOutputEncoding = UTF8NoBOM;
            processInfo.StandardErrorEncoding = UTF8NoBOM;

            // Removing trace variables that might change git output and break parsing
            // List of environment variables: https://git-scm.com/book/gr/v2/Git-Internals-Environment-Variables
            foreach (string key in processInfo.EnvironmentVariables.Keys.Cast<string>().ToList())
            {
                // If GIT_TRACE is set to a fully-rooted path, then Git sends the trace
                // output to that path instead of stdout (GIT_TRACE=1) or stderr (GIT_TRACE=2).
                if (key.StartsWith("GIT_TRACE", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        if (!Path.IsPathRooted(processInfo.EnvironmentVariables[key]))
                        {
                            processInfo.EnvironmentVariables.Remove(key);
                        }
                    }
                    catch (ArgumentException)
                    {
                        processInfo.EnvironmentVariables.Remove(key);
                    }
                }
            }

            processInfo.EnvironmentVariables["GIT_TERMINAL_PROMPT"] = "0";
            processInfo.EnvironmentVariables["GCM_VALIDATE"] = "0";

            if (!userInteractive)
            {
                processInfo.EnvironmentVariables["GCM_INTERACTIVE"] = "Never";
            }

            if (gitObjectsDirectory != null)
            {
                processInfo.EnvironmentVariables["GIT_OBJECT_DIRECTORY"] = gitObjectsDirectory;
            }

            if (!fetchMissingObjects)
            {
                command = $"-c {GitConstant.GitConfig.UseGvfsHelper}=false {command}";
            }

            if (!string.IsNullOrEmpty(dotGitDirectory))
            {
                command = "--git-dir=\"" + dotGitDirectory + "\" " + command;
            }

            processInfo.Arguments = command;

            Process executingProcess = new Process();
            executingProcess.StartInfo = processInfo;

            return executingProcess;
        }

        #endregion

    }
}
