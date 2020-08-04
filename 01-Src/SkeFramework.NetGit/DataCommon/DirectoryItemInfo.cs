using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataCommon
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class DirectoryItemInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public long Length { get; set; }
        public bool IsDirectory { get; set; }
    }
}
