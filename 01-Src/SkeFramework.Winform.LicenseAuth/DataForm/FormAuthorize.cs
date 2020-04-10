using SkeFramework.Winform.SoftAuthorize.BusinessServices;
using SkeFramework.Winform.SoftAuthorize.DataHandle;
using SkeFramework.Winform.SoftAuthorize.DataHandle.SecurityHandles;
using SkeFramework.Winform.SoftAuthorize.DataHandle.Securitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkeFramework.Winform.SoftAuthorize.DataForm
{
    /// <summary>
    /// 注册窗体
    /// </summary>
    public partial class FormAuthorize : Form
    {
        /// <summary>
        /// 机器码
        /// </summary>
        private string machineCode = "";

        private IAuthorize Authorize;

        #region 窗体事件
        public FormAuthorize()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 实例化授权注册窗口
        /// </summary>
        /// <param name="aboutCode">提示关于怎么获取注册码的信息</param>
        /// <param name="encrypt">加密的方法</param>
        public FormAuthorize(string aboutCode, IAuthorize authorize)
        {
            InitializeComponent();
            Authorize = authorize;
            machineCode = Authorize.GetMachineCodeString();
            this.lblMachineCode.Tag = machineCode;
        }
        private void FormAuthorize_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 注册按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (Authorize.CheckAuthorize(rtbFinalCode.Text.Trim()))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("注册码不正确");
            }
        }
        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

    }
}
