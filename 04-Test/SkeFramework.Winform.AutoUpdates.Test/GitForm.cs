using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataConfig;
using SkeFramework.NetGit.DataHandle.ProcessHandle;
using SkeFramework.NetGit.DataService;
using SkeFramework.NetGit.DataService.CloneServices;
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
            string enlistmentRoot = @"E:\JProject\GitRepository\Work";
            string workingDirectory = @"E:\JProject\GitRepository\Work\developmentdocs";
            string repoUrl = "https://gitee.com/SkeCloud/SkeFramework.git";
            string gitBinPath = @"D:\Program Files\Git\cmd\git.exe";

            GitBaseConfig authConfig = new GitAuthConfig(enlistmentRoot, workingDirectory, repoUrl, gitBinPath);
            IGitCommandService command = new GitCommandService(authConfig);
            //Result result = gitProcess.InvokeGitOutsideEnlistment("version");
            string version = command.GitVersion();

            version = command.GitInit();
            string error = "";
            string originUrl = "http://192.168.104.43/netProject/developmentdocs.git";
            string username = "zengyingjun@ut.cn";
            string password = "ut502525164";
            //ICredentialService credentialStore = new CredentialService(authConfig);
            //credentialStore.TryStoreCredential(repoUrl,  username,  password, out error);
            //username = "";
            //password = "";
            //credentialStore.TryGetCredential(repoUrl, out username, out password, out error);

            CloneService cloneService = new CloneService(authConfig);
            Result result= cloneService.GitClone(originUrl, "master");
            result = cloneService.GitPull();
 
        }


        

    }
}
