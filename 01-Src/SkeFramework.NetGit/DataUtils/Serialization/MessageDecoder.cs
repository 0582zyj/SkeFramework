using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataUtils.Serialization
{
    /// <summary>
    /// 消息解码
    /// </summary>
   public class MessageDecoder
    {
        /// <summary>
        /// 路径解析
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ParseValue(string contents, string prefix)
        {
            int startIndex = contents.IndexOf(prefix) + prefix.Length;
            if (startIndex >= 0 && startIndex < contents.Length)
            {
                int endIndex = contents.IndexOf('\n', startIndex);
                if (endIndex >= 0 && endIndex < contents.Length)
                {
                    return
                        contents
                        .Substring(startIndex, endIndex - startIndex)
                        .Trim('\r');
                }
            }
            return null;
        }
    }
}
