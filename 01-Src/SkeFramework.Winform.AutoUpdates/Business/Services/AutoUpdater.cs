using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using SkeFramework.Winform.AutoUpdates.ConstData;
using SkeFramework.Winform.AutoUpdates.DAL.Interfaces;
using SkeFramework.Winform.AutoUpdates.Entities.Common;
using SkeFramework.Winform.AutoUpdates.Helpers;
using SkeFramework.Winform.AutoUpdates.UserForm;

namespace SkeFramework.Winform.AutoUpdates.DAL.Services
{
    #region The delegate
    public delegate void ShowHandler();
    #endregion

    public class AutoUpdater : IAutoUpdater
    {
        #region 私有字段
        ///// <summary>
        ///// 更新XML类
        ///// </summary>
        //private Config config = null;
        /// <summary>
        /// 是否需要重启
        /// </summary>
        private bool bNeedRestart = false;
        /// <summary>
        /// 是否需要下载
        /// </summary>
        private bool bDownload = false;
        /// <summary>
        /// 下载文件列表
        /// </summary>
        List<DownloadFileInfo> downloadFileListTemp = null;
        #endregion

        #region 公共事件
        public event ShowHandler OnShow;
        #endregion

        #region 构造函数
        public AutoUpdater()
        {
            //config = Config.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstFile.FILENAME));
        }
        #endregion

        #region 公共方法
        public int Update()
        {
            if (!CommonUnitity.GlobalConfig.Enabled)
                return 0;
            List<DownloadFileInfo> downloadList = new List<DownloadFileInfo>();
            Dictionary<string, RemoteFile> RemotFileList = ParseRemoteXml(CommonUnitity.GlobalConfig.ServerUrl);
            foreach (LocalFile file in CommonUnitity.GlobalConfig.UpdateFileList)//本地与服务器文件比较
            {
                if (RemotFileList.ContainsKey(file.Path))
                {
                    RemoteFile rf = RemotFileList[file.Path];
                    string v1 = rf.Version;
                    string v2 = file.Version;
                    if (v1 != v2)
                    {
                        downloadList.Add(new DownloadFileInfo(rf.Url, rf.Path, rf.LastVer, rf.Size, rf.Version));
                        file.Path = rf.Path;
                        file.LastVer = rf.LastVer;
                        file.Size = rf.Size;
                        file.Version = rf.Version;
                        if (rf.NeedRestart)
                            bNeedRestart = true;
                        bDownload = true;
                    }

                    RemotFileList.Remove(file.Path);
                }
            }
            //服务器多余的文件
            foreach (RemoteFile file in RemotFileList.Values)
            {
                downloadList.Add(new DownloadFileInfo(file.Url, file.Path, file.LastVer, file.Size, file.Version));
                bDownload = true;
                CommonUnitity.GlobalConfig.UpdateFileList.Add(new LocalFile(file.Path, file.LastVer, file.Size, file.Version));
                if (file.NeedRestart)
                    bNeedRestart = true;
            }
            downloadFileListTemp = downloadList;

            if (bDownload)
            {
                //OperProcess op = new OperProcess();
                //op.InitUpdateEnvironment();
                DownloadConfirm dc = new DownloadConfirm(downloadList);
                if (dc.ShowDialog() == DialogResult.OK)
                {
                    if (this.OnShow != null)
                        this.OnShow();
                    StartDownload(downloadList);
                }
                return 1;
            }
            return -1;
        }
        /// <summary>
        /// 回滚操作
        /// </summary>
        public void RollBack()
        {
            foreach (DownloadFileInfo file in downloadFileListTemp)
            {
                string tempUrlPath = DateTime.Now.ToString("yyyyMMdd");
                string oldPath = string.Empty;
                try
                {
                    if (!string.IsNullOrEmpty(tempUrlPath))
                    {
                        oldPath = Path.Combine(CommonUnitity.SystemBinUrl + tempUrlPath.Substring(1), file.FileName);
                    }
                    else
                    {
                        oldPath = Path.Combine(CommonUnitity.SystemBinUrl, file.FileName);
                    }

                    if (oldPath.EndsWith("_"))
                        oldPath = oldPath.Substring(0, oldPath.Length - 1);

                    MoveFolderToOld(oldPath + ".old", oldPath);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        #endregion

        #region The private method
        string newfilepath = string.Empty;
        private void MoveFolderToOld(string oldPath, string newPath)
        {
            if (File.Exists(oldPath) && File.Exists(newPath))
            {
                System.IO.File.Copy(oldPath, newPath, true);
            }
        }

        private void StartDownload(List<DownloadFileInfo> downloadList)
        {
            DownloadProgress dp = new DownloadProgress(downloadList);
            if (dp.ShowDialog() == DialogResult.OK)
            {
                if (DialogResult.Cancel == dp.ShowDialog())
                {
                    return;
                }
                //Update successfully
                CommonUnitity.GlobalConfig.SaveConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstFile.FILENAME));

                if (bNeedRestart)
                {
                    //Delete the temp folder
                    Directory.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstFile.TEMPFOLDERNAME), true);

                    MessageBox.Show(ConstFile.APPLYTHEUPDATE, ConstFile.MESSAGETITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CommonUnitity.RestartApplication();
                }
            }
        }
        /// <summary>
        /// 解析XML文件
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private Dictionary<string, RemoteFile> ParseRemoteXml(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.Load(xml);
            XmlElement UpdateFileList = document.DocumentElement["UpdateFileList"];
            Dictionary<string, RemoteFile> list = new Dictionary<string, RemoteFile>();
            foreach (XmlNode node in UpdateFileList.ChildNodes)
            {
                list.Add(node.Attributes["path"].Value, new RemoteFile(node));
            }
            return list;
        }
        #endregion

    }

}