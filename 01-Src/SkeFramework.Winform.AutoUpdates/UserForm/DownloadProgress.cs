using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkeFramework.Winform.AutoUpdates.ConstData;
using SkeFramework.Winform.AutoUpdates.Entities.Common;
using SkeFramework.Winform.AutoUpdates.Helpers;

namespace SkeFramework.Winform.AutoUpdates.UserForm
{
    public partial class DownloadProgress : Form
    {
        private bool isFinished = false;
        private List<DownloadFileInfo> downloadFileList = null;
        private List<DownloadFileInfo> allFileList = null;
        private ManualResetEvent evtDownload = null;
        private ManualResetEvent evtPerDonwload = null;
        private WebClient clientDownload = null;

        private long total = 0;
        private long nDownloadedTotal = 0;
        /// <summary>
        /// 临时文件夹路径
        /// </summary>
        private string tempFolderPath
        {
            get
            {
                return Path.Combine(CommonUnitity.SystemBinUrl, ConstFile.TEMPFOLDERNAME);
            }
        }
        /// <summary>
        /// 备份文件夹路径
        /// </summary>
        private string backUpFolderPath
        {
            get
            {
                string path = Path.Combine(this.tempFolderPath, ConstFile.BACKUPFOLDERNAME);
                path += CommonUnitity.GetFolderUrl(null);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        #region 窗体事件
        public DownloadProgress(List<DownloadFileInfo> downloadFileListTemp)
        {
            InitializeComponent();
            this.labelProgramName.Text += CommonUnitity.GlobalConfig.ProgramName;
            this.labelServerUrl.Text += CommonUnitity.GlobalConfig.ServerUrl;

            this.downloadFileList = downloadFileListTemp;
            allFileList = new List<DownloadFileInfo>();
            foreach (DownloadFileInfo file in downloadFileListTemp)
            {
                allFileList.Add(file);
            }
        }
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadProgress_Load(object sender, EventArgs e)
        {
            evtDownload = new ManualResetEvent(true);
            evtDownload.Reset();
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcDownload));
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isFinished && DialogResult.No == MessageBox.Show(ConstFile.CANCELORNOT, ConstFile.MESSAGETITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                e.Cancel = true;
                return;
            }
            else
            {
                if (clientDownload != null)
                    clientDownload.CancelAsync();

                evtDownload.Set();
                evtPerDonwload.Set();
            }
        }
        #endregion

        #region 进度条
        delegate void ShowCurrentDownloadFileNameCallBack(string name);
        private void ShowCurrentDownloadFileName(string name)
        {
            if (this.labelCurrentItem.InvokeRequired)
            {
                ShowCurrentDownloadFileNameCallBack cb = new ShowCurrentDownloadFileNameCallBack(ShowCurrentDownloadFileName);
                this.Invoke(cb, new object[] { name });
            }
            else
            {
                this.labelCurrentItem.Text = name;
            }
        }

        delegate void SetProcessBarCallBack(int current, int total);
        private void SetProcessBar(int current, int total)
        {
            if (this.progressBarCurrent.InvokeRequired)
            {
                SetProcessBarCallBack cb = new SetProcessBarCallBack(SetProcessBar);
                this.Invoke(cb, new object[] { current, total });
            }
            else
            {
                this.progressBarCurrent.Value = current;
                this.progressBarTotal.Value = total;
            }
        }

        delegate void ExitCallBack(bool success);
        private void Exit(bool success)
        {
            if (this.InvokeRequired)
            {
                ExitCallBack cb = new ExitCallBack(Exit);
                this.Invoke(cb, new object[] { success });
            }
            else
            {
                this.isFinished = success;
                this.DialogResult = success ? DialogResult.OK : DialogResult.Cancel;
                this.Close();
            }
        }
        #endregion

        #region
        /// <summary>
        /// 取消下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            ShowErrorAndRestartApplication();
        }
        private void ShowErrorAndRestartApplication()
        {
            MessageBox.Show(ConstFile.NOTNETWORK, ConstFile.MESSAGETITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        /// <summary>
        /// 下载方法
        /// </summary>
        /// <param name="o"></param>
        private void ProcDownload(object o)
        {

            evtPerDonwload = new ManualResetEvent(false);

            foreach (DownloadFileInfo file in this.downloadFileList)
            {
                total += file.Size;
            }
            try
            {
                while (!evtDownload.WaitOne(0, false))
                {
                    if (this.downloadFileList.Count == 0)
                        break;
                    DownloadFileInfo file = this.downloadFileList[0];
                    Debug.WriteLine(String.Format("Start Download:{0}", file.FileName));
                    this.ShowCurrentDownloadFileName(file.FileName);

                    //Download
                    clientDownload = new WebClient();
                    //Added the function to support proxy
                    //clientDownload.Proxy = System.Net.WebProxy.GetDefaultProxy();
                    clientDownload.Proxy = WebRequest.GetSystemWebProxy();
                    clientDownload.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    clientDownload.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    //End added
                    clientDownload.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) =>
                    {
                        try
                        {
                            this.SetProcessBar(e.ProgressPercentage, (int)((nDownloadedTotal + e.BytesReceived) * 100 / total));
                        }
                        catch
                        {
                            //log the error message,you can use the application's log code
                        }

                    };
                    clientDownload.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) =>
                    {
                        try
                        {
                            DealWithDownloadErrors();
                            DownloadFileInfo dfile = e.UserState as DownloadFileInfo;
                            nDownloadedTotal += dfile.Size;
                            this.SetProcessBar(0, (int)(nDownloadedTotal * 100 / total));
                            evtPerDonwload.Set();
                        }
                        catch (Exception)
                        {
                            //log the error message,you can use the application's log code
                        }
                    };
                    evtPerDonwload.Reset();

                    clientDownload.DownloadFileAsync(new Uri(file.DownloadUrl), Path.Combine(this.tempFolderPath, file.FileName + ConstFile.FILENAME_NEW), file);
                    //Wait for the download complete
                    evtPerDonwload.WaitOne();
                    clientDownload.Dispose();
                    clientDownload = null;
                    //Remove the downloaded files
                    this.downloadFileList.Remove(file);
                }
            }
            catch (Exception)
            {
                ShowErrorAndRestartApplication();
            }
            //When the files have not downloaded,return.
            if (downloadFileList.Count > 0)
            {
                return;
            }
            //Test network and deal with errors if there have 
            DealWithDownloadErrors();
            Debug.WriteLine("All Downloaded Complete");
            Debug.WriteLine("Move Folder Start");
            MoveAllFileList();
            Debug.WriteLine("Move Folder Complete");
            //After dealed with all files, clear the data
            this.allFileList.Clear();
            if (this.downloadFileList.Count == 0)
                Exit(true);
            else
                Exit(false);
            evtDownload.Set();
        }
        /// <summary>
        /// 更新下载文件
        /// </summary>
        private void MoveAllFileList()
        {
            foreach (DownloadFileInfo file in this.allFileList)
            {
                string oldPath = string.Empty;
                string backUpPath = string.Empty;
                string downLoadPath = string.Empty;
                try
                {
                    oldPath = Path.Combine(CommonUnitity.SystemBinUrl, file.FileName);
                    backUpPath = Path.Combine(this.backUpFolderPath, file.FileName);
                    downLoadPath = Path.Combine(this.tempFolderPath, file.FileName + ConstFile.FILENAME_NEW);
                    //先备份
                    string newfilepath = string.Empty;
                    if (System.IO.File.Exists(oldPath))
                    {
                        FolderHelper.MoveFolderToOld(oldPath, backUpPath);
                    }
                    //再覆盖
                    if (File.Exists(downLoadPath))
                    {
                        File.Move(downLoadPath, oldPath);
                    }
                    else
                    {
                        FolderHelper.MoveFolderToOld(backUpPath, oldPath);
                    }
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                }
            }
        }
        private void DealWithDownloadErrors()
        {
            try
            {
                //Config config = Config.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstFile.FILENAME));
                WebClient client = new WebClient();
                client.DownloadString(CommonUnitity.GlobalConfig.ServerUrl);
            }
            catch (Exception)
            {
                ShowErrorAndRestartApplication();
            }
        }

    

       
    }
}
