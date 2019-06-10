using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.Winform.AutoUpdates.Entities.Bases;

namespace SkeFramework.Winform.AutoUpdates.Entities.Common
{
    /// <summary>
    /// 本地文件信息
    /// </summary>
    public class LocalFile : BaseFile
    {
        public LocalFile(string path, string ver, int size, string versionid)
            : base(path, ver, size, versionid)
        {
        }

        public LocalFile()
        {
        }

    }
}
