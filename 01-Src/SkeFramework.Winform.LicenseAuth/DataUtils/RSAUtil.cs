using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.LicenseAuth.DataUtils
{
    public class RSAUtil
    {

        /// <summary>
        /// RSA算法对象，此处主要用于获取密钥对
        /// </summary>
        private RSACryptoServiceProvider RSA;

        /// <summary>
        /// 取得密钥
        /// </summary>
        /// <param name="includPrivateKey">true：包含私钥   false：不包含私钥</param>
        /// <returns></returns>
        public string ToXmlString(bool includPrivateKey)
        {
            if (includPrivateKey)
            {
                return RSA.ToXmlString(true);
            }
            else
            {
                return RSA.ToXmlString(false);
            }
        }

        /// <summary>
        /// 通过密钥初始化RSA对象
        /// </summary>
        /// <param name="xmlString">XML格式的密钥信息</param>
        public void FromXmlString(string xmlString)
        {
            RSA.FromXmlString(xmlString);
        }


       
     

        /// <summary>
        /// 通过公钥加密
        /// </summary>
        /// <param name="dataStr">待加密字符串</param>
        /// <returns>加密结果</returns>
        public byte[] EncryptByPublicKey(string dataStr)
        {
            //取得公钥参数
            RSAParameters rsaparameters = RSA.ExportParameters(false);
            byte[] keyN = rsaparameters.Modulus;
            byte[] keyE = rsaparameters.Exponent;
            //大整数N
            BigInteger biN = new BigInteger(keyN);
            //公钥大素数
            BigInteger biE = new BigInteger(keyE);
            //加密
            return EncryptString(dataStr, biE, biN);
        }

        /// <summary>
        /// 通过公钥加密
        /// </summary>
        /// <param name="dataStr">待加密字符串</param>
        /// <param name="n">大整数n</param>
        /// <param name="e">公钥</param>
        /// <returns>加密结果</returns>
        public byte[] EncryptByPublicKey(string dataStr, string n, string e)
        {
            //大整数N
            BigInteger biN = new BigInteger(n, 16);
            //公钥大素数
            BigInteger biE = new BigInteger(e, 16);
            //加密
            return EncryptString(dataStr, biE, biN);
        }


        /// <summary>
        /// 通过私钥解密
        /// </summary>
        /// <param name="dataBytes">待解密字符数组</param>
        /// <returns>解密结果</returns>
        public string DecryptByPrivateKey(byte[] dataBytes)
        {
            //取得私钥参数
            RSAParameters rsaparameters = RSA.ExportParameters(true);
            byte[] keyN = rsaparameters.Modulus;
            byte[] keyD = rsaparameters.D;
            //大整数N
            BigInteger biN = new BigInteger(keyN);
            //私钥大素数
            BigInteger biD = new BigInteger(keyD);
            //解密
            return DecryptBytes(dataBytes, biD, biN);
        }

        /// <summary>
        /// 通过私钥解密
        /// </summary>
        /// <param name="dataBytes">待解密字符数组</param>
        /// <param name="n">大整数n</param>
        /// <param name="d">私钥</param>
        /// <returns>解密结果</returns>
        public string DecryptByPrivateKey(byte[] dataBytes, string n, string d)
        {
            //大整数N
            BigInteger biN = new BigInteger(n, 16);
            //私钥大素数
            BigInteger biD = new BigInteger(d, 16);
            //解密
            return DecryptBytes(dataBytes, biD, biN);
        }


        /// <summary>
        /// 通过私钥加密
        /// </summary>
        /// <param name="dataStr">待加密字符串</param>
        /// <returns>加密结果</returns>
        public byte[] EncryptByPrivateKey(string dataStr)
        {
            //取得私钥参数
            RSAParameters rsaparameters = RSA.ExportParameters(true);
            byte[] keyN = rsaparameters.Modulus;
            byte[] keyD = rsaparameters.D;
            //大整数N
            BigInteger biN = new BigInteger(keyN);
            //私钥大素数
            BigInteger biD = new BigInteger(keyD);
            //加密
            return EncryptString(dataStr, biD, biN);
        }

        /// <summary>
        /// 通过私钥加密
        /// </summary>
        /// <param name="dataStr">待加密字符串</param>
        /// <param name="n">大整数n</param>
        /// <param name="d">私钥</param>
        /// <returns>加密结果</returns>
        public byte[] EncryptByPrivateKey(string dataStr, string n, string d)
        {
            //大整数N
            BigInteger biN = new BigInteger(n, 16);
            //私钥大素数
            BigInteger biD = new BigInteger(d, 16);
            //加密
            return EncryptString(dataStr, biD, biN);
        }

     


        /// <summary>
        /// 通过公钥解密
        /// </summary>
        /// <param name="dataBytes">待解密字符数组</param>
        /// <returns>解密结果</returns>
        public string DecryptByPublicKey(byte[] dataBytes)
        {
            //取得公钥参数
            RSAParameters rsaparameters = RSA.ExportParameters(false);
            byte[] keyN = rsaparameters.Modulus;
            byte[] keyE = rsaparameters.Exponent;
            //大整数N
            BigInteger biN = new BigInteger(keyN);
            //公钥大素数
            BigInteger biE = new BigInteger(keyE);
            //解密
            return DecryptBytes(dataBytes, biE, biN);
        }

        /// <summary>
        /// 通过公钥解密
        /// </summary>
        /// <param name="dataBytes">待加密字符串</param>
        /// <param name="n">大整数n</param>
        /// <param name="e">公钥</param>
        /// <returns>解密结果</returns>
        public string DecryptByPublicKey(byte[] dataBytes, string n, string e)
        {
            //大整数N
            BigInteger biN = new BigInteger(n, 16);
            //公钥大素数
            BigInteger biE = new BigInteger(e, 16);
            //解密
            return DecryptBytes(dataBytes, biE, biN);
        }

      

/// <summary>
/// 加密字符串
/// </summary>
/// <param name="dataStr">待加密字符串</param>
/// <param name="keyNmu">密钥大素数</param>
/// <param name="nNum">大整数N</param>
/// <returns>加密结果</returns>
        private byte[] EncryptString(string dataStr, BigInteger keyNum, BigInteger nNum)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(dataStr);
            int len = bytes.Length;
            int len1 = 0;
            int blockLen = 0;
            if ((len % 120) == 0)
                len1 = len / 120;
            else
                len1 = len / 120 + 1;
            List<byte> tempbytes = new List<byte>();
            for (int i = 0; i < len1; i++)
            {
                if (len >= 120)
                {
                    blockLen = 120;
                }
                else
                {
                    blockLen = len;
                }
                byte[] oText = new byte[blockLen];
                Array.Copy(bytes, i * 120, oText, 0, blockLen);
                string res = Encoding.UTF8.GetString(oText);
                BigInteger biText = new BigInteger(oText);
                BigInteger biEnText = biText.ModPow(keyNum, nNum);
                //补位
                byte[] testbyte = null;
                string resultStr = biEnText.ToString();
                if (resultStr.Length < 256)
                {
                    while (resultStr.Length != 256)
                    {
                        resultStr = "0" + resultStr;
                    }
                }
                byte[] returnBytes = new byte[128];
                for (int j = 0; j < returnBytes.Length; j++)
                    returnBytes[j] = Convert.ToByte(resultStr.Substring(j * 2, 2), 16);
                tempbytes.AddRange(returnBytes);
                len -= blockLen;
            }
            return tempbytes.ToArray();
        }

       

        /// <summary>
        /// 解密字符数组
        /// </summary>
        /// <param name="dataBytes">待解密字符数组</param>
        /// <param name="KeyNum">密钥大素数</param>
        /// <param name="nNum">大整数N</param>
        /// <returns>解密结果</returns>
        private string DecryptBytes(byte[] dataBytes, BigInteger KeyNum, BigInteger nNum)
        {
            int len = dataBytes.Length;
            int len1 = 0;
            int blockLen = 0;
            if (len % 128 == 0)
            {
                len1 = len / 128;
            }
            else
            {
                len1 = len / 128 + 1;
            }
            List<byte> tempbytes = new List<byte>();
            for (int i = 0; i < len1; i++)
            {
                if (len >= 128)
                {
                    blockLen = 128;
                }
                else
                {
                    blockLen = len;
                }
                byte[] oText = new byte[blockLen];
                Array.Copy(dataBytes, i * 128, oText, 0, blockLen);
                BigInteger biText = new BigInteger(oText);
                BigInteger biEnText = biText.ModPow(KeyNum, nNum);
                byte[] testbyte = biEnText.ToByteArray();
                string str = Encoding.UTF8.GetString(testbyte);
                tempbytes.AddRange(testbyte);
                len -= blockLen;
            }
            return System.Text.Encoding.UTF8.GetString(tempbytes.ToArray());
        }
    }
}
