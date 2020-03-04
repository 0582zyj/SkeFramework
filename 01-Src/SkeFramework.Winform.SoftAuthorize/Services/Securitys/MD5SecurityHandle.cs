using SkeFramework.Winform.SoftAuthorize.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.Services.Securitys
{
    /// <summary>
    /// MD5加密类
    /// </summary>
    public class MD5SecurityHandle : ISecurityHandle
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptStr"></param>
        /// <returns></returns>
        public string Decrypt(string decryptStr)
        {
            return SoftSecurity.MD5Decrypt(decryptStr);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        public string Encrypt(string encryptStr)
        {
            return SoftSecurity.MD5Encrypt(encryptStr);
        }
    }
}
