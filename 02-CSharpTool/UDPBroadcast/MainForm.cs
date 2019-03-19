using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDPBroadcast.Services;

namespace UDPBroadcast
{
    public partial class MainForm : Form
    {
        private CommPortUdp server = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            server = new CommPortUdp();
            server.ReceiveMessageCallback += ServerHandleMessage;
            server.SendMessageCallback += ServerHandleMessage;

            this.tbxlocalip.Text = this.server.LocalIP;
            this.tbxlocalport.Text = this.server.LocalPort.ToString();
            this.tbxGroupIp.Text = this.server.LocalGroupIp;
            this.tbxSendToGroupIp.Text = this.server.LocalGroupIp;
        }

        #region 按钮事件
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tbxMessageSend.Text == "")
            {
                MessageBox.Show("消息内容不能为空！", "提示");
                return;
            }
            server.Send(tbxMessageSend.Text, chkbxBroadcast.Checked);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.chkbxJoinGtoup.Enabled = true;
            this.btnReceive.Enabled = true;
            this.server.receiveUdpClientDispose();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            chkbxJoinGtoup.Enabled = false;
            // 创建接收套接字
            string IP = this.tbxlocalip.Text;
            int Port = Convert.ToInt32( this.tbxlocalport.Text);
            this.server.Receive(chkbxJoinGtoup.Checked, IP, Port);

        }
        #endregion

        public void ServerHandleMessage(string message)
        {
            this.ShowMessage(this.richTextBox1, message);
        }

        // 通过委托回调机制显示消息内容
        delegate void ShowMessageCallBack(RichTextBox listbox, string text);
        private void ShowMessage(RichTextBox listbox, string text)
        {
            if (listbox.InvokeRequired)
            {
                ShowMessageCallBack showmessageCallback = ShowMessage;
                listbox.Invoke(showmessageCallback, new object[] { listbox, text });
            }
            else
            {
                if (String.IsNullOrEmpty(text))
                {
                    listbox.Clear();
                }
                else
                {
                    string msg = String.Format("{0}==>{1}", DateTime.Now.ToString("HH:mm:ss.fff"), text);
                    listbox.AppendText(msg + Environment.NewLine);
                }
                
            }
        }

        private void chkbxJoinGtoup_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkbxJoinGtoup.Checked == true)
            {
                this.tbxGroupIp.Enabled = false;
            }
            else
            {
                this.tbxGroupIp.Enabled = true;
                this.tbxGroupIp.Focus();
            }
        }

        private void chkbxBroadcast_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkbxBroadcast.Checked == true)
            {
                this.tbxSendToGroupIp.Enabled = false;
            }
            else
            {
                this.tbxSendToGroupIp.Enabled = true;
                this.tbxSendToGroupIp.Focus();
            }
        }
    }
}
