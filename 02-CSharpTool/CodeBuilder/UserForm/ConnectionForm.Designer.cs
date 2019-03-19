namespace CodeBuilder.UserForm
{
    partial class ConnectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.textEditDatabase = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.richTextBoxConnectionString = new System.Windows.Forms.RichTextBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textEditUserId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEditDataSource = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonTestConnect = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonConnect = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatabase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDataSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textEditDatabase);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.richTextBoxConnectionString);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.textEditPassword);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.textEditUserId);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.textEditDataSource);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.comboBoxEdit1);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(14, 14);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(770, 582);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "连接参数";
            // 
            // textEditDatabase
            // 
            this.textEditDatabase.EditValue = "smartcloud";
            this.textEditDatabase.Location = new System.Drawing.Point(176, 169);
            this.textEditDatabase.Name = "textEditDatabase";
            this.textEditDatabase.Size = new System.Drawing.Size(537, 20);
            this.textEditDatabase.TabIndex = 11;
            this.textEditDatabase.EditValueChanged += new System.EventHandler(this.textEditDatabase_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(98, 173);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "数据库名：";
            // 
            // richTextBoxConnectionString
            // 
            this.richTextBoxConnectionString.Enabled = false;
            this.richTextBoxConnectionString.Location = new System.Drawing.Point(176, 336);
            this.richTextBoxConnectionString.Name = "richTextBoxConnectionString";
            this.richTextBoxConnectionString.Size = new System.Drawing.Size(536, 198);
            this.richTextBoxConnectionString.TabIndex = 9;
            this.richTextBoxConnectionString.Text = "";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(42, 339);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(108, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "数据库连接字符串：";
            // 
            // textEditPassword
            // 
            this.textEditPassword.EditValue = "ut123456";
            this.textEditPassword.Location = new System.Drawing.Point(176, 276);
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Size = new System.Drawing.Size(537, 20);
            this.textEditPassword.TabIndex = 7;
            this.textEditPassword.EditValueChanged += new System.EventHandler(this.textEditPassword_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(126, 280);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "密码：";
            // 
            // textEditUserId
            // 
            this.textEditUserId.EditValue = "root";
            this.textEditUserId.Location = new System.Drawing.Point(176, 224);
            this.textEditUserId.Name = "textEditUserId";
            this.textEditUserId.Size = new System.Drawing.Size(537, 20);
            this.textEditUserId.TabIndex = 5;
            this.textEditUserId.EditValueChanged += new System.EventHandler(this.textEditUserId_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(112, 227);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "用户名：";
            // 
            // textEditDataSource
            // 
            this.textEditDataSource.EditValue = "127.0.0.1";
            this.textEditDataSource.Location = new System.Drawing.Point(176, 117);
            this.textEditDataSource.Name = "textEditDataSource";
            this.textEditDataSource.Size = new System.Drawing.Size(537, 20);
            this.textEditDataSource.TabIndex = 3;
            this.textEditDataSource.EditValueChanged += new System.EventHandler(this.textEditDataSource_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(84, 120);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "服务器名称：";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(176, 54);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(537, 20);
            this.comboBoxEdit1.TabIndex = 1;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(84, 57);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "服务器类型：";
            // 
            // simpleButtonTestConnect
            // 
            this.simpleButtonTestConnect.Location = new System.Drawing.Point(215, 615);
            this.simpleButtonTestConnect.Name = "simpleButtonTestConnect";
            this.simpleButtonTestConnect.Size = new System.Drawing.Size(110, 27);
            this.simpleButtonTestConnect.TabIndex = 1;
            this.simpleButtonTestConnect.Text = "测试连接";
            this.simpleButtonTestConnect.Click += new System.EventHandler(this.simpleButtonTestConnect_Click);
            // 
            // simpleButtonConnect
            // 
            this.simpleButtonConnect.Location = new System.Drawing.Point(331, 615);
            this.simpleButtonConnect.Name = "simpleButtonConnect";
            this.simpleButtonConnect.Size = new System.Drawing.Size(110, 27);
            this.simpleButtonConnect.TabIndex = 2;
            this.simpleButtonConnect.Text = "开始连接";
            this.simpleButtonConnect.Click += new System.EventHandler(this.simpleButtonConnect_Click);
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Location = new System.Drawing.Point(448, 615);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(110, 27);
            this.simpleButtonClose.TabIndex = 3;
            this.simpleButtonClose.Text = "关闭";
            this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonClose_Click);
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 656);
            this.Controls.Add(this.simpleButtonClose);
            this.Controls.Add(this.simpleButtonConnect);
            this.Controls.Add(this.simpleButtonTestConnect);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库连接器";
            this.Load += new System.EventHandler(this.ConnectionForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConnectionForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDatabase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDataSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTestConnect;
        private DevExpress.XtraEditors.SimpleButton simpleButtonConnect;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        private System.Windows.Forms.RichTextBox richTextBoxConnectionString;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit textEditUserId;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEditDataSource;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditDatabase;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}