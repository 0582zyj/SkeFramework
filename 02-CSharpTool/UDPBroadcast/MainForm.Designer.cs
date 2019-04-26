namespace UDPBroadcast
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxMessageSend = new System.Windows.Forms.TextBox();
            this.chkbxBroadcast = new System.Windows.Forms.CheckBox();
            this.tbxSendToGroupIp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkbxJoinGtoup = new System.Windows.Forms.CheckBox();
            this.tbxGroupIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxlocalport = new System.Windows.Forms.TextBox();
            this.tbxlocalip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnReceive = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Size = new System.Drawing.Size(584, 442);
            this.splitContainer1.SplitterDistance = 107;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Controls.Add(this.tbxMessageSend);
            this.groupBox2.Controls.Add(this.chkbxBroadcast);
            this.groupBox2.Controls.Add(this.tbxSendToGroupIp);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(282, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(292, 87);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送到";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(233, 58);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(47, 21);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbxMessageSend
            // 
            this.tbxMessageSend.Location = new System.Drawing.Point(80, 58);
            this.tbxMessageSend.Name = "tbxMessageSend";
            this.tbxMessageSend.Size = new System.Drawing.Size(147, 21);
            this.tbxMessageSend.TabIndex = 7;
            // 
            // chkbxBroadcast
            // 
            this.chkbxBroadcast.AutoSize = true;
            this.chkbxBroadcast.Checked = true;
            this.chkbxBroadcast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbxBroadcast.Location = new System.Drawing.Point(25, 61);
            this.chkbxBroadcast.Name = "chkbxBroadcast";
            this.chkbxBroadcast.Size = new System.Drawing.Size(48, 16);
            this.chkbxBroadcast.TabIndex = 6;
            this.chkbxBroadcast.Text = "广播";
            this.chkbxBroadcast.UseVisualStyleBackColor = true;
            this.chkbxBroadcast.CheckedChanged += new System.EventHandler(this.chkbxBroadcast_CheckedChanged);
            // 
            // tbxSendToGroupIp
            // 
            this.tbxSendToGroupIp.Location = new System.Drawing.Point(61, 29);
            this.tbxSendToGroupIp.Name = "tbxSendToGroupIp";
            this.tbxSendToGroupIp.Size = new System.Drawing.Size(219, 21);
            this.tbxSendToGroupIp.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "组：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkbxJoinGtoup);
            this.groupBox1.Controls.Add(this.tbxGroupIp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxlocalport);
            this.groupBox1.Controls.Add(this.tbxlocalip);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "本地设置";
            // 
            // chkbxJoinGtoup
            // 
            this.chkbxJoinGtoup.AutoSize = true;
            this.chkbxJoinGtoup.Location = new System.Drawing.Point(187, 53);
            this.chkbxJoinGtoup.Name = "chkbxJoinGtoup";
            this.chkbxJoinGtoup.Size = new System.Drawing.Size(60, 16);
            this.chkbxJoinGtoup.TabIndex = 5;
            this.chkbxJoinGtoup.Text = "加入组";
            this.chkbxJoinGtoup.UseVisualStyleBackColor = true;
            this.chkbxJoinGtoup.CheckedChanged += new System.EventHandler(this.chkbxJoinGtoup_CheckedChanged);
            // 
            // tbxGroupIp
            // 
            this.tbxGroupIp.Location = new System.Drawing.Point(62, 48);
            this.tbxGroupIp.Name = "tbxGroupIp";
            this.tbxGroupIp.Size = new System.Drawing.Size(100, 21);
            this.tbxGroupIp.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "：";
            // 
            // tbxlocalport
            // 
            this.tbxlocalport.Location = new System.Drawing.Point(187, 18);
            this.tbxlocalport.Name = "tbxlocalport";
            this.tbxlocalport.Size = new System.Drawing.Size(60, 21);
            this.tbxlocalport.TabIndex = 2;
            this.tbxlocalport.Text = "52441";
            // 
            // tbxlocalip
            // 
            this.tbxlocalip.Location = new System.Drawing.Point(62, 18);
            this.tbxlocalip.Name = "tbxlocalip";
            this.tbxlocalip.Size = new System.Drawing.Size(100, 21);
            this.tbxlocalip.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "地址：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.Controls.Add(this.btnReceive);
            this.groupBox3.Controls.Add(this.btnStop);
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(10, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(564, 311);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "接收消息";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 54);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(550, 251);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // btnReceive
            // 
            this.btnReceive.Location = new System.Drawing.Point(505, 28);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(51, 21);
            this.btnReceive.TabIndex = 11;
            this.btnReceive.Text = "接收";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(63, 27);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(51, 21);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 27);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(51, 21);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 442);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UDP通信";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbxMessageSend;
        private System.Windows.Forms.CheckBox chkbxBroadcast;
        private System.Windows.Forms.TextBox tbxSendToGroupIp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkbxJoinGtoup;
        private System.Windows.Forms.TextBox tbxGroupIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxlocalport;
        private System.Windows.Forms.TextBox tbxlocalip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClear;
    }
}

