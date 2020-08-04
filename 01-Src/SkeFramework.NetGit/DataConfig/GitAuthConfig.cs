using SkeFramework.NetGit.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataConfig
{
    public class GitAuthConfig : GitBaseConfig
    {

        public GitAuthConfig(string enlistmentRoot, string workingDirectory, string repoUrl, string gitBinPath )
           : base(
                 enlistmentRoot,
                 workingDirectory,
                 repoUrl,
                 gitBinPath,
                 flushFileBuffersForPacks: true)
        {
            this.LocalObjectsRoot = Path.Combine(this.WorkingDirectoryRoot, GitConstant.DotGit.Objects.Root);
        }

        public override string GitObjectsRoot { get; protected set; }
        public override string LocalObjectsRoot { get; protected set; }
        public override string GitPackRoot { get; protected set; }
    }
}
