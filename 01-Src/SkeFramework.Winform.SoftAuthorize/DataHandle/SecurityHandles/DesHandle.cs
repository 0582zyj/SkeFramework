﻿using SkeFramework.Winform.SoftAuthorize.DataUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataHandle.Securitys
{
    /// <summary>
    /// Des加密类
    /// </summary>
    public class DesHandle : ISecurityHandle
    {
        /// <summary>
        /// 默认密钥向量
        /// </summary>
        public string Keys = "12345678";
        /// <summary>
        /// 密钥，长度为8，英文或数字
        /// </summary>
        public string Password { get; set; } = "ut123456";

        public DesHandle()
        {
        }
        public DesHandle(string password)
        {
            this.Password = password;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptStr"></param>
        /// <returns></returns>
        public string Decrypt(string decryptStr)
        {
            return DESDecrypt(decryptStr, Password, Keys);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        public string Encrypt(string encryptStr)
        {
            return DESEncrypt(encryptStr, Password, Keys);
        }



        #region DES加密和解密

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="Data">被加密的明文</param>
        /// <param name="Key">密钥</param>
        /// <param name="Vector">向量</param>
        /// <returns>密文</returns>
        public string DESEncrypt(string encryptString, String Key, String Vector)
        {
            byte[] Data = Encoding.UTF8.GetBytes(encryptString);
            //将密钥转为字节数组，取其前8位
            Byte[] bKey = new Byte[8];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);
            //将向量转为字节数组，取其前8位
            Byte[] bVector = new Byte[8];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(bVector.Length)), bVector, bVector.Length);
            Byte[] Cryptograph = null; // 加密后的密文
            DESCryptoServiceProvider EncryptProvider = new DESCryptoServiceProvider();
            EncryptProvider.Mode = CipherMode.CBC;
            EncryptProvider.Padding = PaddingMode.Zeros;
            try
            {
                // 开辟一块内存流
                MemoryStream Memory = new MemoryStream();
                // 把内存流对象包装成加密流对象
                using (CryptoStream Encryptor = new CryptoStream(Memory,
                 EncryptProvider.CreateEncryptor(bKey, bVector),
                  CryptoStreamMode.Write))
                {
                    // 明文数据写入加密流
                    Encryptor.Write(Data, 0, Data.Length);
                    Encryptor.FlushFinalBlock();
                    Cryptograph = Memory.ToArray();
                }
            }
            catch
            {
                Cryptograph = null;
            }

            return Convert.ToBase64String(Cryptograph.ToArray());
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="Data">被解密的密文</param>
        /// <param name="Key">密钥</param>
        /// <param name="Vector">向量</param>
        /// <returns>明文</returns>
        public string DESDecrypt(string dncryptString, String Key, String Vector)
        {
            using (DESCryptoServiceProvider CryptoProvider = new DESCryptoServiceProvider
            { Key = Encoding.UTF8.GetBytes(Key), IV = Encoding.UTF8.GetBytes(Vector) })
            {
                CryptoProvider.Mode = CipherMode.CBC;
                CryptoProvider.Padding = PaddingMode.Zeros;
                using (ICryptoTransform ct = CryptoProvider.CreateDecryptor())
                {
                    byte[] byt = Convert.FromBase64String(dncryptString);
                    var ms = new MemoryStream();
                    using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                    {
                        cs.Write(byt, 0, byt.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
        #endregion

    }
}
