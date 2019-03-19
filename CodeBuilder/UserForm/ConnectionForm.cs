using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeBuilder.DataFactory;
using CodeBuilder.Global;
using CodeBuilder.Model;
using CodeBuilder.Model.Entities;
using DevExpress.XtraEditors;

namespace CodeBuilder.UserForm
{
    public partial class ConnectionForm : XtraForm
    {
        private List<ConnectionNode> ConnNoteList = null;

        public ConnectionForm()
        {
            InitializeComponent();
        }


        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            ConnNoteList = new List<ConnectionNode>();
            ConnNoteList.Add(new ConnectionNode("MySQL",ProviderType.MySQL, "Data Source={0};Initial Catalog ={1};Port=3306;User Id={2};Password={3};Charset=utf8;TreatTinyAsBoolean=false;"));
            ConnNoteList.Add(new ConnectionNode("SQL Server", "system.data.sqlclient", "Data Source = {0};Initial Catalog = {1};User Id = {2};Password = {3};"));
            ConnNoteList.Add(new ConnectionNode("SQLite",ProviderType.SQLite, "Data Source = {0};Initial Catalog = {1};User Id = {2};Password = {3};"));
            this.comboBoxEdit1.Properties.Items.AddRange(ConnNoteList);

            this.comboBoxEdit1.SelectedIndex = 0;
        }
        /// <summary>
        /// 键盘点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionState state = this.TryGetConnectState();
                if (state == ConnectionState.Open)
                {
                    StatusBarManager.DataBaseName = this.textEditDatabase.Text;
                    StatusBarManager.ConnectionState = state.ToString();
                    XtraMessageBox.Show(@"连接成功", @"信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #region 按钮事件
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonTestConnect_Click(object sender, EventArgs e)
        {
            ConnectionState state= this.TryGetConnectState();
            if (state == ConnectionState.Open)
            {
                XtraMessageBox.Show(@"连接成功！", @"信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonConnect_Click(object sender, EventArgs e)
        {
            ConnectionState state = this.TryGetConnectState();
            if (state == ConnectionState.Open)
            {
                StatusBarManager.DataBaseName = this.textEditDatabase.Text;
                StatusBarManager.ConnectionState = state.ToString();
                XtraMessageBox.Show(@"连接成功", @"信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /// <summary>
        /// 退出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 输入框事件
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.SelectedText != "SQLite")
            {
                this.textEditDatabase.Enabled = true;
                this.textEditDataSource.Enabled = true;
                this.textEditPassword.Enabled = true;
                this.textEditUserId.Enabled = true;
                this.richTextBoxConnectionString.Enabled = false;
                this.ConnectionStringFill();
            }
            else
            {
                this.textEditDatabase.Enabled = false;
                this.textEditDataSource.Enabled = false;
                this.textEditPassword.Enabled = false;
                this.textEditUserId.Enabled = false;
                this.richTextBoxConnectionString.Enabled = true;
                this.richTextBoxConnectionString.Text = @"Data Source=F:/ZProject/Software/SQLiteStudio/SmartHostDB";
            }
        }

        private void textEditDataSource_EditValueChanged(object sender, EventArgs e)
        {
            this.ConnectionStringFill();
        }

        private void textEditDatabase_EditValueChanged(object sender, EventArgs e)
        {
            this.ConnectionStringFill();
        }

        private void textEditUserId_EditValueChanged(object sender, EventArgs e)
        {
            this.ConnectionStringFill();
        }

        private void textEditPassword_EditValueChanged(object sender, EventArgs e)
        {
            this.ConnectionStringFill();
        }
        #endregion

        /// <summary>
        /// 连接字符串构造
        /// </summary>
        private void ConnectionStringFill()
        {
            ConnectionNode node = comboBoxEdit1.SelectedItem as ConnectionNode;
            string DataSource = this.textEditDataSource.Text;
            string Database = this.textEditDatabase.Text;
            string UserId = this.textEditUserId.Text;
            string Password = this.textEditPassword.Text;
            this.richTextBoxConnectionString.Text = string.Format(node.connectionString, DataSource, Database, UserId, Password);
        }
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns></returns>
        private ConnectionState TryGetConnectState()
        {
            try
            {
                string connectionString = this.richTextBoxConnectionString.Text;
                string database = this.textEditDatabase.Text;
                ConnectionNode node = comboBoxEdit1.SelectedItem as ConnectionNode;
                DbFactory.Instance().SetProperties(node.ProviderName, connectionString, database);
                IDbConnection conn = DbFactory.Instance().CreateDbConnection();
                conn.Open();
                return conn.State;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(@"连接失败！" + ex.Message, @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return ConnectionState.Closed;
        }
    }
}
