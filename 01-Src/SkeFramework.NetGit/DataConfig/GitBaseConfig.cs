using SkeFramework.NetGit.Constants;
using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataHandle.ProcessHandle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataConfig
{
    /// <summary>
    /// 授权链接配置
    /// </summary>
    public abstract class GitBaseConfig
    {
        protected GitBaseConfig(string enlistmentRoot,string workingDirectoryRoot, string repoUrl,string gitBinPath, bool flushFileBuffersForPacks)
        {
            if (string.IsNullOrWhiteSpace(gitBinPath))
            {
                throw new ArgumentException("Path to git.exe must be set");
            }
            this.EnlistmentRoot = enlistmentRoot;
            this.WorkingDirectoryRoot = workingDirectoryRoot;
            this.DotGitRoot = Path.Combine(this.WorkingDirectoryRoot, GitConstant.DotGit.Root);
            this.GitBinPath = gitBinPath;
            this.FlushFileBuffersForPacks = flushFileBuffersForPacks;
            this.RepoUrl = repoUrl;
        }

        public string EnlistmentRoot { get; }

        /// <summary>
        /// 工作目录
        /// </summary>
        public string WorkingDirectoryRoot { get; }
        /// <summary>
        /// .git根路径
        /// </summary>
        public string DotGitRoot { get; private set; }
        public abstract string GitObjectsRoot { get; protected set; }
        public abstract string LocalObjectsRoot { get; protected set; }
        public abstract string GitPackRoot { get; protected set; }
        /// <summary>
        /// 仓库地址
        /// </summary>
        public string RepoUrl { get; }
        public bool FlushFileBuffersForPacks { get; }

        public string GitBinPath { get; }


        public virtual GitProcess CreateGitProcess()
        {
            return new GitProcess(this);
        }
    }
}
