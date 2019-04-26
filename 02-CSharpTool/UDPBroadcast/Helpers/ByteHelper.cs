using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPBroadcast.Helpers
{
    public class ByteHelper
    {
        /// <summary>
        /// 将字节数组转变成字符串形式返回。如bytes值为0xA5、0x5A时返回A5 5A.
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public static string GetBytesText(byte[] bytes, int offset, int size)
        {
            StringBuilder ret = new StringBuilder();
            string tmp = "";
            for (int i = offset; i < size; ++i)
            {
                tmp = "0" + bytes[i].ToString("X");
                ret.Append(tmp.Substring(tmp.Length - 2, 2));
                ret.Append(" ");
            }
            return ret.ToString();
        }
    }
}
