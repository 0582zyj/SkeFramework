using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataConfig;
using SkeFramework.NetGit.DataHandle.ProcessHandle;
using SkeFramework.NetGit.DataService;
using SkeFramework.NetGit.DataService.CredentialServices;
using SkeFramework.NetGit.Interfaces;
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
            string enlistmentRoot = @"D:\JProject\GitRepository\Work";
            string workingDirectory = @"E:\JProject\GitRepository\Work\developmentdocs";
            string repoUrl = "https://gitee.com/SkeCloud/SkeFramework.git";
            string gitBinPath = @"C:\Program Files\Git\cmd\git.exe";

            GitConfig authConfig = new GitAuthConfig(enlistmentRoot, workingDirectory, repoUrl, gitBinPath);
            IGitCommand command = new GitCommandService(authConfig.CreateGitProcess());
            //Result result = gitProcess.InvokeGitOutsideEnlistment("version");
            string version = command.GitVersion();

            version = command.GitInit();
            string error = "";
            string originUrl = "";
             string username = "502525164@qq.com";
            string password = "jun502525164";
            ICredentialStore credentialStore = new CredentialService(authConfig.CreateGitProcess());
            credentialStore.TryStoreCredential(repoUrl,  username,  password, out error);
            credentialStore.TryGetCredential(repoUrl, out username, out password, out error);

        }


        

    }
}
