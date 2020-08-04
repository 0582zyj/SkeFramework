using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataService.CredentialServices
{
    /// <summary>
    /// 认证存储
    /// </summary>
    public interface ICredentialService
    {
        /// <summary>
        /// 获取用户和密码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool TryGetCredential(string url, out string username, out string password, out string error);
        /// <summary>
        /// 设置用户和密码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool TryStoreCredential(string url, string username, string password, out string error);
        /// <summary>
        /// 删除用户和密码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool TryDeleteCredential(string url, string username, string password, out string error);
    }
}
