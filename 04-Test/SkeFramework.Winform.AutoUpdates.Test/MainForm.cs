using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using SkeFramework.Winform.AutoUpdates.DAL.Interfaces;
using SkeFramework.Winform.AutoUpdates.DAL.Services;

namespace SkeFramework.Winform.AutoUpdates.Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //InitCheckUpdate();
            //var utcNow = DateTime.Now;
            //AutoIDWorker snowFlake = new AutoIDWorker(2);
            //long start = DateTime.Now.Ticks;
            //for (int i = 0; i < 2*10000; i++)
            //{
            //    Console.WriteLine(snowFlake.GetAutoID());
            //}
            //Console.WriteLine(DateTime.Now.Subtract(utcNow).TotalSeconds);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            MessageBox.Show("检查更新");
            InitCheckUpdate();
        }

        void InitCheckUpdate()
        {
            #region check and download new version program
            bool bHasError = false;
            IAutoUpdater autoUpdater = new AutoUpdater();
            try
            {
              int result=  autoUpdater.Update();
              if (result==1)
              {
                  MessageBox.Show("更新成功，请重启程序");
              }
            }
            catch (WebException exp)
            {
                MessageBox.Show("服务器连接失败" + exp.Message);
                bHasError = true;
            }
            catch (XmlException exp)
            {
                bHasError = true;
                MessageBox.Show("下载更新文件错误" + exp.Message);
            }
            catch (NotSupportedException exp)
            {
                bHasError = true;
                MessageBox.Show("升级文件配置错误" + exp.Message);
            }
            catch (ArgumentException exp)
            {
                bHasError = true;
                MessageBox.Show("下载升级文件错误" + exp.Message);
            }
            catch (Exception exp)
            {
                bHasError = true;
                MessageBox.Show("更新过程中出现错误" + exp.Message);
            }
            finally
            {
                if (bHasError == true)
                {
                    try
                    {
                        autoUpdater.RollBack();
                    }
                    catch (Exception)
                    {
                        //Log the message to your file or database
                    }
                }
           
            }
            #endregion
        }

    }
}
