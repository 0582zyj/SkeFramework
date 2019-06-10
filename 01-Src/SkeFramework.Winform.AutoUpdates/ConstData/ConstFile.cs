using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.AutoUpdates.ConstData
{
    public class ConstFile
    {
        /// <summary>
        /// 临时文件夹
        /// </summary>
        public const string TEMPFOLDERNAME = "TempFolder";
        /// <summary>
        /// 备份文件夹
        /// </summary>
        public const string BACKUPFOLDERNAME = "Backup";
        /// <summary>
        /// 下载文件后缀
        /// </summary>
        public const string FILENAME_NEW = ".new";
        /// <summary>
        /// 版本更新信息
        /// </summary>
        public const string FILENAME = "AutoupdateService.xml";


        public const string ROOLBACKFILE = "KnightsWarrior.exe";
        public const string MESSAGETITLE = "AutoUpdate Program";
        public const string CANCELORNOT = "AutoUpdate Program is in progress. Do you really want to cancel?";
        public const string APPLYTHEUPDATE = "Program need to restart to apply the update,Please click OK to restart the program!";
        public const string NOTNETWORK = "update is unsuccessful.Program will now restart. Please try to update again.";
    }
}
