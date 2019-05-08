using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkeFramework.Winform.AutoUpdates.Entities.Common;
using SkeFramework.Winform.AutoUpdates.Helpers;

namespace SkeFramework.Winform.AutoUpdates.UserForm
{
    public partial class DownloadConfirm : Form
    {

        List<DownloadFileInfo> downloadFileList = null;

        public DownloadConfirm(List<DownloadFileInfo> downloadfileList)
        {
            InitializeComponent();

            downloadFileList = downloadfileList;
            this.labelProgramName.Text += CommonUnitity.GlobalConfig.ProgramName;
            this.labelServerUrl.Text += CommonUnitity.GlobalConfig.ServerUrl;
        }


        private void DownloadConfirm_Load(object sender, EventArgs e)
        {
            foreach (DownloadFileInfo file in this.downloadFileList)
            {
                ListViewItem item = new ListViewItem(new string[] { file.FileName, file.LastVer, file.Size.ToString() });
            }

            this.Activate();
            this.Focus();
        }

        #region 按钮事件
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
