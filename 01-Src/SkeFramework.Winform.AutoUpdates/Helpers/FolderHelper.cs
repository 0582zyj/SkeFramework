using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.AutoUpdates.Helpers
{
    /// <summary>
    /// 文件夹帮助类
    /// </summary>
    public class FolderHelper
    {

        /// <summary>
        /// 移动文件夹
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static void MoveFolderToOld(string oldPath, string newPath)
        {
            string path_old = oldPath + ".old";
            string path_new = newPath + ".old";
            if (File.Exists(path_old))
            {
                //删除旧文件 重命名新文件
                FileInfo fi_old = new FileInfo(path_old);
                if ((fi_old.Attributes & FileAttributes.ReadOnly) > 0)
                {
                    fi_old.Attributes ^= FileAttributes.ReadOnly;	// 必须去除只读属性才能进行设置
                }
                fi_old.Delete();
            }
            if (File.Exists(path_new))
            {
                //删除旧文件 重命名新文件
                FileInfo fi_new = new FileInfo(path_new);
                if ((fi_new.Attributes & FileAttributes.ReadOnly) > 0)
                {
                    fi_new.Attributes ^= FileAttributes.ReadOnly;	// 必须去除只读属性才能进行设置
                }
                fi_new.Delete();
            }
            File.Move(oldPath, path_new);
        }
    }
}
