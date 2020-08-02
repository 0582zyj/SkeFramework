using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataConfig;
using SkeFramework.NetGit.DataHandle.ProcessHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataService.CredentialServices
{
    /// <summary>
    /// 认证
    /// </summary>
    public class CredentialService : ICredentialStore
    {
        private GitProcess Process;

        public CredentialService(GitProcess gitProcess)
        {
            Process = gitProcess;
        }
        public bool TryDeleteCredential(string url, string username, string password, out string error)
        {
            throw new NotImplementedException();
        }

        public bool TryGetCredential(string url, out string username, out string password, out string error)
        {
            username = null;
            password = null;
            error = null;
            
            Result gitCredentialOutput = this.Process.InvokeGitAgainstDotGitFolder(
                     GenerateCredentialVerbCommand("fill"),
                     stdin => stdin.Write($"url={url}\n\n"),
                     parseStdOutLine: null);

            if (gitCredentialOutput.ExitCodeIsFailure)
            {
                EventMetadata errorData = new EventMetadata();
                error = gitCredentialOutput.Errors;
                return false;
            }

            //username = ParseValue(gitCredentialOutput.Output, "username=");
            //password = ParseValue(gitCredentialOutput.Output, "password=");

            bool success = username != null && password != null;

            EventMetadata metadata = new EventMetadata();
            metadata.Add("Success", success);
            if (!success)
            {
                metadata.Add("Output", gitCredentialOutput.Output);
            }

            return success;

        }

        private string GenerateCredentialVerbCommand(string verb)
        {
            return $"-c {GitConfigSetting.CredentialUseHttpPath}=true credential {verb}";
        }

        public bool TryStoreCredential(string url, string username, string password, out string error)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("url={0}\n", url);
            sb.AppendFormat("username={0}\n", username);
            sb.AppendFormat("password={0}\n", password);
            sb.Append("\n");

            string stdinConfig = sb.ToString();

            Result result = this.Process.InvokeGitOutsideEnlistment(
                GenerateCredentialVerbCommand("approve"),
                stdin => stdin.Write(stdinConfig),
                null);

            if (result.ExitCodeIsFailure)
            {
                error = result.Errors;
                return false;
            }

            error = null;
            return true;
        }
    }
}
