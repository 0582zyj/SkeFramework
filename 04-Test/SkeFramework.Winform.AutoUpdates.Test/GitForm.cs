using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataConfig;
using SkeFramework.NetGit.DataHandle.ProcessHandle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkeFramework.Winform.AutoUpdates.Test
{
    public partial class GitForm : Form
    {
        public GitForm()
        {
            InitializeComponent();
        }

        private void GitForm_Load(object sender, EventArgs e)
        {
            string enlistmentRoot = @"E:\JProject\GitRepository\Work";
            string workingDirectory = @"E:\JProject\GitRepository\Work\developmentdocs";
            string repoUrl = "http://192.168.104.43/netProject/developmentdocs.git";
            string gitBinPath = @"D:\Program Files\Git\cmd\git.exe";
            GitConfig authConfig = new GitAuthConfig(enlistmentRoot, workingDirectory, repoUrl, gitBinPath);
            GitProcess gitProcess = authConfig.CreateGitProcess();
            //Result result = gitProcess.InvokeGitOutsideEnlistment("version");
            //string version = result.Output;

            Result result = gitProcess.InvokeGitInWorkingDirectoryRoot("init", false);
            string version = result.Output;
        }
    }
}
