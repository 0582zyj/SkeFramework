using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.Interfaces
{
    /// <summary>
    /// 认证存储
    /// </summary>
    public interface ICredentialStore
    {
        bool TryGetCredential(string url, out string username, out string password, out string error);

        bool TryStoreCredential(string url, string username, string password, out string error);

        bool TryDeleteCredential(string url, string username, string password, out string error);
    }
}
