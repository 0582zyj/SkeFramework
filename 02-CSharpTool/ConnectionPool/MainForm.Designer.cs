namespace ConnectionPool
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtExistTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaxRepeatDegree = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbmConnType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.rtbPoolMessage = new System.Windows.Forms.RichTextBox();
            this.txtKeepRealConnection = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewConn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMaxConn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDisConn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetConn = new System.Windows.Forms.Button();
            this.txtMinConn = new System.Windows.Forms.TextBox();
            this.lblMinConn = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(239, 42);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(225, 21);
            this.txtKey.TabIndex = 3;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(613, 136);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "停止应用池";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(613, 102);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "启动应用池";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(90, 96);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(494, 73);
            this.txtConnectionString.TabIndex = 13;
            this.txtConnectionString.Text = "Data Source=127.0.0.1;port=3306;Initial Catalog=smartcloud;user id=root;password=" +
    "ut123456;Charset=utf8";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "连接字符串:";
            // 
            // txtExistTime
            // 
            this.txtExistTime.Location = new System.Drawing.Point(484, 61);
            this.txtExistTime.Name = "txtExistTime";
            this.txtExistTime.Size = new System.Drawing.Size(100, 21);
            this.txtExistTime.TabIndex = 11;
            this.txtExistTime.Text = "3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(398, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "生存期限(M):";
            // 
            // txtMaxRepeatDegree
            // 
            this.txtMaxRepeatDegree.Location = new System.Drawing.Point(484, 32);
            this.txtMaxRepeatDegree.Name = "txtMaxRepeatDegree";
            this.txtMaxRepeatDegree.Size = new System.Drawing.Size(100, 21);
            this.txtMaxRepeatDegree.TabIndex = 9;
            this.txtMaxRepeatDegree.Text = "3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "最大引用记数:";
            // 
            // cbmConnType
            // 
            this.cbmConnType.FormattingEnabled = true;
            this.cbmConnType.Items.AddRange(new object[] {
            "ReadOnly",
            "High",
            "None",
            "Bottom"});
            this.cbmConnType.Location = new System.Drawing.Point(84, 42);
            this.cbmConnType.Name = "cbmConnType";
            this.cbmConnType.Size = new System.Drawing.Size(100, 20);
            this.cbmConnType.TabIndex = 1;
            this.cbmConnType.Text = "ReadOnly";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "连接类型：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.rtbPoolMessage);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 308);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(784, 253);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "连接池信息";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rtbMessage);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(200, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(581, 233);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "其他消息";
            // 
            // rtbMessage
            // 
            this.rtbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMessage.Location = new System.Drawing.Point(3, 17);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(575, 213);
            this.rtbMessage.TabIndex = 2;
            this.rtbMessage.Text = "";
            // 
            // rtbPoolMessage
            // 
            this.rtbPoolMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.rtbPoolMessage.Location = new System.Drawing.Point(3, 17);
            this.rtbPoolMessage.Name = "rtbPoolMessage";
            this.rtbPoolMessage.Size = new System.Drawing.Size(197, 233);
            this.rtbPoolMessage.TabIndex = 4;
            this.rtbPoolMessage.Text = "";
            // 
            // txtKeepRealConnection
            // 
            this.txtKeepRealConnection.Location = new System.Drawing.Point(292, 60);
            this.txtKeepRealConnection.Name = "txtKeepRealConnection";
            this.txtKeepRealConnection.Size = new System.Drawing.Size(100, 21);
            this.txtKeepRealConnection.TabIndex = 7;
            this.txtKeepRealConnection.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "保留连接数:";
            // 
            // txtNewConn
            // 
            this.txtNewConn.Location = new System.Drawing.Point(90, 61);
            this.txtNewConn.Name = "txtNewConn";
            this.txtNewConn.Size = new System.Drawing.Size(100, 21);
            this.txtNewConn.TabIndex = 5;
            this.txtNewConn.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "新建连接数:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(198, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "Key：";
            // 
            // txtMaxConn
            // 
            this.txtMaxConn.Location = new System.Drawing.Point(292, 32);
            this.txtMaxConn.Name = "txtMaxConn";
            this.txtMaxConn.Size = new System.Drawing.Size(100, 21);
            this.txtMaxConn.TabIndex = 3;
            this.txtMaxConn.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "最大连接数:";
            // 
            // btnDisConn
            // 
            this.btnDisConn.Enabled = false;
            this.btnDisConn.Location = new System.Drawing.Point(627, 41);
            this.btnDisConn.Name = "btnDisConn";
            this.btnDisConn.Size = new System.Drawing.Size(75, 23);
            this.btnDisConn.TabIndex = 18;
            this.btnDisConn.Text = "释放连接";
            this.btnDisConn.UseVisualStyleBackColor = true;
            this.btnDisConn.Click += new System.EventHandler(this.btnDisConn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(470, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "随机Key";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDisConn);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btnGetConn);
            this.groupBox2.Controls.Add(this.txtKey);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cbmConnType);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(784, 109);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "连接操作";
            // 
            // btnGetConn
            // 
            this.btnGetConn.Enabled = false;
            this.btnGetConn.Location = new System.Drawing.Point(551, 41);
            this.btnGetConn.Name = "btnGetConn";
            this.btnGetConn.Size = new System.Drawing.Size(75, 23);
            this.btnGetConn.TabIndex = 15;
            this.btnGetConn.Text = "获取连接";
            this.btnGetConn.UseVisualStyleBackColor = true;
            this.btnGetConn.Click += new System.EventHandler(this.btnGetConn_Click);
            // 
            // txtMinConn
            // 
            this.txtMinConn.Location = new System.Drawing.Point(90, 32);
            this.txtMinConn.Name = "txtMinConn";
            this.txtMinConn.Size = new System.Drawing.Size(100, 21);
            this.txtMinConn.TabIndex = 1;
            this.txtMinConn.Text = "2";
            // 
            // lblMinConn
            // 
            this.lblMinConn.AutoSize = true;
            this.lblMinConn.Location = new System.Drawing.Point(13, 36);
            this.lblMinConn.Name = "lblMinConn";
            this.lblMinConn.Size = new System.Drawing.Size(71, 12);
            this.lblMinConn.TabIndex = 0;
            this.lblMinConn.Text = "最小连接数:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.txtConnectionString);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtExistTime);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMaxRepeatDegree);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtKeepRealConnection);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNewConn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMaxConn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMinConn);
            this.groupBox1.Controls.Add(this.lblMinConn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 199);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "应用池参数设置";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库连接池模拟实现";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtExistTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMaxRepeatDegree;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbmConnType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private System.Windows.Forms.RichTextBox rtbPoolMessage;
        private System.Windows.Forms.TextBox txtKeepRealConnection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNewConn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMaxConn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDisConn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetConn;
        private System.Windows.Forms.TextBox txtMinConn;
        private System.Windows.Forms.Label lblMinConn;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

