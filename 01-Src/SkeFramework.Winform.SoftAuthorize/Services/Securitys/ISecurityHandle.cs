﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.Services.Securitys
{
    /// <summary>
    /// 加密接口
    /// </summary>
    public interface ISecurityHandle
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        string Encrypt(string encryptStr);
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptStr"></param>
        /// <returns></returns>
        string Decrypt(string decryptStr);
    }
}
