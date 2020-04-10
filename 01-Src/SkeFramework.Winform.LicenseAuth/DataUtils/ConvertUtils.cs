using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkeFramework.Winform.LicenseAuth.DataUtils
{
    /// <summary>
    /// 一个软件基础类，提供常用的一些静态方法 ->
    /// A software-based class that provides some common static methods
    /// </summary>
    public class ConvertUtils
    {
        #region Hex string and Byte[] transform


        /// <summary>
        /// 字节数据转化成16进制表示的字符串 ->
        /// Byte data into a string of 16 binary representations
        /// </summary>
        /// <param name="InBytes">字节数组</param>
        /// <returns>返回的字符串</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <example>
        /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample1" title="ByteToHexString示例" />
        /// </example>
        public static string ByteToHexString(byte[] InBytes)
        {
            return ByteToHexString(InBytes, (char)0);
        }

        /// <summary>
        /// 字节数据转化成16进制表示的字符串 ->
        /// Byte data into a string of 16 binary representations
        /// </summary>
        /// <param name="InBytes">字节数组</param>
        /// <param name="segment">分割符</param>
        /// <returns>返回的字符串</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <example>
        /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample2" title="ByteToHexString示例" />
        /// </example>
        public static string ByteToHexString(byte[] InBytes, char segment)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte InByte in InBytes)
            {
                if (segment == 0) sb.Append(string.Format("{0:X2}", InByte));
                else sb.Append(string.Format("{0:X2}{1}", InByte, segment));
            }

            if (segment != 0 && sb.Length > 1 && sb[sb.Length - 1] == segment)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }



        /// <summary>
        /// 字符串数据转化成16进制表示的字符串 ->
        /// String data into a string of 16 binary representations
        /// </summary>
        /// <param name="InString">输入的字符串数据</param>
        /// <returns>返回的字符串</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static string ByteToHexString(string InString)
        {
            return ByteToHexString(Encoding.Unicode.GetBytes(InString));
        }


        private static List<char> hexCharList = new List<char>()
            {
                '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'
            };

        /// <summary>
        /// 将16进制的字符串转化成Byte数据，将检测每2个字符转化，也就是说，中间可以是任意字符 ->
        /// Converts a 16-character string into byte data, which will detect every 2 characters converted, that is, the middle can be any character
        /// </summary>
        /// <param name="hex">十六进制的字符串，中间可以是任意的分隔符</param>
        /// <returns>转换后的字节数组</returns>
        /// <remarks>参数举例：AA 01 34 A8</remarks>
        /// <example>
        /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="HexStringToBytesExample" title="HexStringToBytes示例" />
        /// </example>
        public static byte[] HexStringToBytes(string hex)
        {
            hex = hex.ToUpper();

            MemoryStream ms = new MemoryStream();

            for (int i = 0; i < hex.Length; i++)
            {
                if ((i + 1) < hex.Length)
                {
                    if (hexCharList.Contains(hex[i]) && hexCharList.Contains(hex[i + 1]))
                    {
                        // 这是一个合格的字节数据
                        ms.WriteByte((byte)(hexCharList.IndexOf(hex[i]) * 16 + hexCharList.IndexOf(hex[i + 1])));
                        i++;
                    }
                }
            }

            byte[] result = ms.ToArray();
            ms.Dispose();
            return result;
        }

        #endregion
    }

}
