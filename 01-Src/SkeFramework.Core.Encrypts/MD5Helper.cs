using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.Encrypts
{
    public class MD5Helper
    {
        /// <summary>
        /// 字符串转换为MD5加密串
        /// </summary>
        /// <param name="originStr">未加密字符串</param>
        /// <returns>已加密字符串</returns>
        public static string GetMD5String(string originStr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.BigEndianUnicode.GetBytes(originStr);
            byte[] targetData = md5.ComputeHash(fromData);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < targetData.Length; i++)
            {
                sb.Append(targetData[i].ToString("x"));
            }
            return sb.ToString();
        }
    }
}
