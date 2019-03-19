using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DateBaseConnectionPool;
using SkeFramework.NetConnectionPool.ConnService;

namespace ConnectionPool
{
    public partial class MainForm : Form
    {
        private ConnPool c = null;
        private Thread checkPoolThread = null;
        private bool isRunning = true;

        public MainForm()
        {
            InitializeComponent();
            this.Text = string.Format("{0}-{1}", this.Text, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkPoolThread = new Thread(new ThreadStart(CheckConnPoolProcess));
            checkPoolThread.IsBackground = true;
            checkPoolThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isRunning = false;
                checkPoolThread.Abort();
                c.StopServices();
            }
            catch { }
        }

        #region 应用池操作
        private void btnStart_Click(object sender, EventArgs e)
        {
            int MinConnection = Convert.ToInt16(txtMinConn.Text);
            int MaxConnection = Convert.ToInt16(txtMaxConn.Text);
            int SeepConnection = Convert.ToInt16(txtNewConn.Text);
            int KeepRealConnection = Convert.ToInt16(txtKeepRealConnection.Text);
            string ConnectionString = txtConnectionString.Text;
            c = new ConnPool(ConnectionString, ConnTypeEnum.MySqlClient, MaxConnection, MinConnection, SeepConnection, KeepRealConnection);
            c.MaxRepeatDegree = Convert.ToInt16(txtMaxRepeatDegree.Text);
            c.Interval = 1;
            c.ExistMinute = Convert.ToInt16(txtExistTime.Text);
            c.StartServices();
            checkPoolThread.Interrupt();
            txtMinConn.ReadOnly = true;
            txtMaxConn.ReadOnly = true;
            txtNewConn.ReadOnly = true;
            txtKeepRealConnection.ReadOnly = true;
            txtConnectionString.ReadOnly = true;
            txtMaxRepeatDegree.ReadOnly = true;
            txtExistTime.ReadOnly = true;
            btnGetConn.Enabled = true;
            btnDisConn.Enabled = true;
            btnStart.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            c.StopServices(true);
            //try { checkPoolThread.Join(); } catch { }
            //try { checkOrderThread.Join(); } catch { }
            txtMinConn.ReadOnly = false;
            txtMaxConn.ReadOnly = false;
            txtNewConn.ReadOnly = false;
            txtKeepRealConnection.ReadOnly = false;
            txtConnectionString.ReadOnly = false;
        }
        #endregion

        #region 链接操作
        private void button2_Click(object sender, EventArgs e)
        {
            txtKey.Text = Guid.NewGuid().ToString();
        }

        private void btnGetConn_Click(object sender, EventArgs e)
        {
            string aaa = cbmConnType.SelectedItem.ToString();
            string Key = txtKey.Text;
            if (Key.Length == 0)
            {
                Key = Guid.NewGuid().ToString();
            }
            DbConnection conn = null;
            if (aaa == "start")
                c.StartServices();
            else if (aaa == "stop")
                c.StopServices(true);
            else if (aaa == "ReadOnly")
                conn = (DbConnection)c.GetConnectionFormPool(Key, ConnLevel.ReadOnly);
            else if (aaa == "High")
                conn = (DbConnection)c.GetConnectionFormPool(Key, ConnLevel.High);
            else if (aaa == "Bottom")
            {
                using (conn = (DbConnection)c.GetConnectionFormPool(Key, ConnLevel.Bottom))
                {
                }
            }
            else if (aaa == "None")
                conn = (DbConnection)c.GetConnectionFormPool(Key, ConnLevel.None);
            if (conn != null)
                AddMessageToRichTextBox(conn.ConnectionString, rtbMessage);
        }

        private void btnDisConn_Click(object sender, EventArgs e)
        {
            try
            {
                string Key = txtKey.Text;
                if (Key.Length > 0)
                {
                    c.DisposeConnection(Key);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion 

      
        private string print(ConnPool c)
        {
            StringBuilder sb = new StringBuilder();
            //Console.WriteLine("最大可以创建的连接数目：" + c.MaxConnection.ToString());
            //Console.WriteLine("每个连接的最大引用记数：" + c.MaxRepeatDegree.ToString());
            //Console.WriteLine("保留的实际空闲连接：" + c.KeepRealConnection.ToString());
            sb.AppendLine("实际连接(包含失效的)：" + c.RealFormPool.ToString());
            sb.AppendLine("实际连接(不包含失效的)：" + c.PotentRealFormPool.ToString());
            sb.AppendLine("目前可以提供的连接数：" + c.SpareFormPool.ToString());
            sb.AppendLine("空闲的实际连接：" + c.SpareRealFormPool.ToString());
            sb.AppendLine("已分配的实际连接：" + c.UseRealFormPool.ToString());
            sb.AppendLine("已分配连接数：" + c.UseFormPool.ToString());
            sb.AppendLine("已分配只读连接：" + c.ReadOnlyFormPool.ToString());
            sb.AppendLine("--------------------------");
            return sb.ToString();
        }

        #region  Thread
        /// <summary>
        /// Pool信息
        /// </summary>
        private void CheckConnPoolProcess()
        {
            do
            {
                try
                {
                    if (c != null)
                    {
                        if (c.State == PoolState.Run)
                        {
                            string LogMessage = print(c);
                            AddMessageToRichTextBox(LogMessage, rtbPoolMessage);
                        }
                        else if (c.State == PoolState.Initialize)
                        {
                            AddMessageToRichTextBox("连接池正在初始化连接！", rtbPoolMessage);
                        }
                        else if (c.State == PoolState.Stop)
                        {
                            AddMessageToRichTextBox("已经停止服务！", rtbPoolMessage);
                            try { checkPoolThread.Join(); }
                            catch { }
                        }
                    }
                    else
                    {
                        try { checkPoolThread.Join(); }
                        catch { }
                    }

                }
                catch (Exception ex)
                {
                    AddMessageToRichTextBox(ex.Message, rtbMessage);
                }
                Thread.Sleep(3000);
            } while (isRunning);
        }
        #endregion

        #region 委托

        private delegate void InvokeReceivedDataPoint(string message, RichTextBox rtb);
        private void AddMessageToRichTextBox(string message, RichTextBox rtb)
        {
            try {
                if (this.InvokeRequired)
                {
                    if (!this.IsHandleCreated) return;
                    var callback = new InvokeReceivedDataPoint(AddMessageToRichTextBox);
                    this.Invoke(callback, new object[] { message, rtb });
                }
                else
                {
                    rtb.AppendText("【" + System.DateTime.Now.ToString() + "】" + Environment.NewLine + message + Environment.NewLine);
                    rtb.Refresh();
                }
            }
            catch { }
        }

        #endregion
    }
}
