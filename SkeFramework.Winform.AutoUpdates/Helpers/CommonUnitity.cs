using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkeFramework.Winform.AutoUpdates.Entities.Common;

namespace SkeFramework.Winform.AutoUpdates.Helpers
{
    public class CommonUnitity
    {
        public static string SystemBinUrl = AppDomain.CurrentDomain.BaseDirectory;

        public static void RestartApplication()
        {
            Process.Start(Application.ExecutablePath);
            Environment.Exit(0);
        }


        public static string GetFolderUrl(DownloadFileInfo file)
        {
            return "/" + DateTime.Now.ToString("yyyyMMdd");
            //string folderPathUrl = string.Empty;
            //int folderPathPoint = file.DownloadUrl.IndexOf("/", 15) + 1;
            //string filepathstring = file.DownloadUrl.Substring(folderPathPoint);
            //if (filepathstring.IndexOf("/") != -1)
            //{
            //    //string[] ExeGroup = filepathstring1.Split('/');
            //    string[] ExeGroup = filepathstring.Split('/');
            //    for (int i = 0; i < ExeGroup.Length - 1; i++)
            //    {
            //        folderPathUrl += "\\" + ExeGroup[i];
            //    }
            //    if (!Directory.Exists(SystemBinUrl + ConstFile.TEMPFOLDERNAME + folderPathUrl))
            //    {
            //        Directory.CreateDirectory(SystemBinUrl + ConstFile.TEMPFOLDERNAME + folderPathUrl);
            //    }
            //}
            //return folderPathUrl;
        }
    }
}
