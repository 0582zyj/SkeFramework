namespace CodeBuilder.UserForm
{
    partial class CodeBuilderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeBuilderForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.richTextBoxConnectionString = new System.Windows.Forms.RichTextBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.treeListDataView = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn_View = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnDataViewID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnDataViewPID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.simpleButtonDVRemoves = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonDVRemove = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonDVAdd = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonDVAdds = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxDataViewDes = new System.Windows.Forms.ListBox();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.ucTemplateTV1 = new CodeBuilder.UserControls.UcTemplateTV();
            this.simpleButtonTpRemoves = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonTpRemove = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonTpAdd = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonTpAdds = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxTemplateDes = new System.Windows.Forms.ListBox();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButtonChooseFile = new DevExpress.XtraEditors.SimpleButton();
            this.textEditOutputPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCreate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListDataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOutputPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.richTextBoxConnectionString);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Location = new System.Drawing.Point(12, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(648, 83);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "选择数据源";
            // 
            // richTextBoxConnectionString
            // 
            this.richTextBoxConnectionString.Enabled = false;
            this.richTextBoxConnectionString.Location = new System.Drawing.Point(120, 36);
            this.richTextBoxConnectionString.Name = "richTextBoxConnectionString";
            this.richTextBoxConnectionString.Size = new System.Drawing.Size(500, 33);
            this.richTextBoxConnectionString.TabIndex = 9;
            this.richTextBoxConnectionString.Text = "";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(18, 39);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(96, 14);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "当前连接字符串：";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.treeListDataView);
            this.groupControl2.Controls.Add(this.simpleButtonDVRemoves);
            this.groupControl2.Controls.Add(this.simpleButtonDVRemove);
            this.groupControl2.Controls.Add(this.simpleButtonDVAdd);
            this.groupControl2.Controls.Add(this.simpleButtonDVAdds);
            this.groupControl2.Controls.Add(this.listBoxDataViewDes);
            this.groupControl2.Location = new System.Drawing.Point(12, 97);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(648, 197);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "选择数据表";
            // 
            // treeListDataView
            // 
            this.treeListDataView.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn_View,
            this.treeListColumnDataViewID,
            this.treeListColumnDataViewPID});
            this.treeListDataView.Location = new System.Drawing.Point(22, 32);
            this.treeListDataView.Name = "treeListDataView";
            this.treeListDataView.Size = new System.Drawing.Size(187, 144);
            this.treeListDataView.TabIndex = 16;
            // 
            // treeListColumn_View
            // 
            this.treeListColumn_View.Caption = "数据表";
            this.treeListColumn_View.FieldName = "Name";
            this.treeListColumn_View.Name = "treeListColumn_View";
            this.treeListColumn_View.Visible = true;
            this.treeListColumn_View.VisibleIndex = 0;
            // 
            // treeListColumnDataViewID
            // 
            this.treeListColumnDataViewID.Caption = "序号";
            this.treeListColumnDataViewID.FieldName = "ID";
            this.treeListColumnDataViewID.Name = "treeListColumnDataViewID";
            // 
            // treeListColumnDataViewPID
            // 
            this.treeListColumnDataViewPID.Caption = "所属序号";
            this.treeListColumnDataViewPID.FieldName = "ParentID";
            this.treeListColumnDataViewPID.Name = "treeListColumnDataViewPID";
            // 
            // simpleButtonDVRemoves
            // 
            this.simpleButtonDVRemoves.Location = new System.Drawing.Point(286, 138);
            this.simpleButtonDVRemoves.Name = "simpleButtonDVRemoves";
            this.simpleButtonDVRemoves.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonDVRemoves.TabIndex = 15;
            this.simpleButtonDVRemoves.Text = "<<";
            this.simpleButtonDVRemoves.Click += new System.EventHandler(this.simpleButtonDVRemoves_Click);
            // 
            // simpleButtonDVRemove
            // 
            this.simpleButtonDVRemove.Location = new System.Drawing.Point(286, 109);
            this.simpleButtonDVRemove.Name = "simpleButtonDVRemove";
            this.simpleButtonDVRemove.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonDVRemove.TabIndex = 14;
            this.simpleButtonDVRemove.Text = "<";
            this.simpleButtonDVRemove.Click += new System.EventHandler(this.simpleButtonDVRemove_Click);
            // 
            // simpleButtonDVAdd
            // 
            this.simpleButtonDVAdd.Location = new System.Drawing.Point(286, 80);
            this.simpleButtonDVAdd.Name = "simpleButtonDVAdd";
            this.simpleButtonDVAdd.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonDVAdd.TabIndex = 13;
            this.simpleButtonDVAdd.Text = ">";
            this.simpleButtonDVAdd.Click += new System.EventHandler(this.simpleButtonDVAdd_Click);
            // 
            // simpleButtonDVAdds
            // 
            this.simpleButtonDVAdds.Location = new System.Drawing.Point(286, 51);
            this.simpleButtonDVAdds.Name = "simpleButtonDVAdds";
            this.simpleButtonDVAdds.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonDVAdds.TabIndex = 12;
            this.simpleButtonDVAdds.Text = ">>";
            this.simpleButtonDVAdds.Click += new System.EventHandler(this.simpleButtonDVAdds_Click);
            // 
            // listBoxDataViewDes
            // 
            this.listBoxDataViewDes.FormattingEnabled = true;
            this.listBoxDataViewDes.ItemHeight = 14;
            this.listBoxDataViewDes.Location = new System.Drawing.Point(430, 32);
            this.listBoxDataViewDes.Name = "listBoxDataViewDes";
            this.listBoxDataViewDes.Size = new System.Drawing.Size(190, 144);
            this.listBoxDataViewDes.TabIndex = 1;
            // 
            // groupControl3
            // 
            this.groupControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl3.Controls.Add(this.ucTemplateTV1);
            this.groupControl3.Controls.Add(this.simpleButtonTpRemoves);
            this.groupControl3.Controls.Add(this.simpleButtonTpRemove);
            this.groupControl3.Controls.Add(this.simpleButtonTpAdd);
            this.groupControl3.Controls.Add(this.simpleButtonTpAdds);
            this.groupControl3.Controls.Add(this.listBoxTemplateDes);
            this.groupControl3.Location = new System.Drawing.Point(12, 304);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(648, 190);
            this.groupControl3.TabIndex = 3;
            this.groupControl3.Text = "选择模板";
            // 
            // ucTemplateTV1
            // 
            this.ucTemplateTV1.Location = new System.Drawing.Point(22, 32);
            this.ucTemplateTV1.Name = "ucTemplateTV1";
            this.ucTemplateTV1.Size = new System.Drawing.Size(183, 144);
            this.ucTemplateTV1.TabIndex = 20;
            // 
            // simpleButtonTpRemoves
            // 
            this.simpleButtonTpRemoves.Location = new System.Drawing.Point(286, 138);
            this.simpleButtonTpRemoves.Name = "simpleButtonTpRemoves";
            this.simpleButtonTpRemoves.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonTpRemoves.TabIndex = 19;
            this.simpleButtonTpRemoves.Text = "<<";
            this.simpleButtonTpRemoves.Click += new System.EventHandler(this.simpleButtonTpRemoves_Click);
            // 
            // simpleButtonTpRemove
            // 
            this.simpleButtonTpRemove.Location = new System.Drawing.Point(286, 107);
            this.simpleButtonTpRemove.Name = "simpleButtonTpRemove";
            this.simpleButtonTpRemove.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonTpRemove.TabIndex = 18;
            this.simpleButtonTpRemove.Text = "<";
            this.simpleButtonTpRemove.Click += new System.EventHandler(this.simpleButtonTpRemove_Click);
            // 
            // simpleButtonTpAdd
            // 
            this.simpleButtonTpAdd.Location = new System.Drawing.Point(286, 75);
            this.simpleButtonTpAdd.Name = "simpleButtonTpAdd";
            this.simpleButtonTpAdd.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonTpAdd.TabIndex = 17;
            this.simpleButtonTpAdd.Text = ">";
            this.simpleButtonTpAdd.Click += new System.EventHandler(this.simpleButtonTpAdd_Click);
            // 
            // simpleButtonTpAdds
            // 
            this.simpleButtonTpAdds.Location = new System.Drawing.Point(286, 43);
            this.simpleButtonTpAdds.Name = "simpleButtonTpAdds";
            this.simpleButtonTpAdds.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonTpAdds.TabIndex = 16;
            this.simpleButtonTpAdds.Text = ">>";
            this.simpleButtonTpAdds.Click += new System.EventHandler(this.simpleButtonTpAdds_Click);
            // 
            // listBoxTemplateDes
            // 
            this.listBoxTemplateDes.FormattingEnabled = true;
            this.listBoxTemplateDes.ItemHeight = 14;
            this.listBoxTemplateDes.Location = new System.Drawing.Point(430, 32);
            this.listBoxTemplateDes.Name = "listBoxTemplateDes";
            this.listBoxTemplateDes.Size = new System.Drawing.Size(190, 144);
            this.listBoxTemplateDes.TabIndex = 1;
            // 
            // groupControl4
            // 
            this.groupControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl4.Controls.Add(this.simpleButtonChooseFile);
            this.groupControl4.Controls.Add(this.textEditOutputPath);
            this.groupControl4.Controls.Add(this.labelControl1);
            this.groupControl4.Location = new System.Drawing.Point(12, 510);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(648, 83);
            this.groupControl4.TabIndex = 4;
            this.groupControl4.Text = "选择目录";
            // 
            // simpleButtonChooseFile
            // 
            this.simpleButtonChooseFile.Location = new System.Drawing.Point(545, 35);
            this.simpleButtonChooseFile.Name = "simpleButtonChooseFile";
            this.simpleButtonChooseFile.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonChooseFile.TabIndex = 11;
            this.simpleButtonChooseFile.Text = "选择文件";
            this.simpleButtonChooseFile.Click += new System.EventHandler(this.simpleButtonChooseFile_Click);
            // 
            // textEditOutputPath
            // 
            this.textEditOutputPath.Enabled = false;
            this.textEditOutputPath.Location = new System.Drawing.Point(84, 36);
            this.textEditOutputPath.Name = "textEditOutputPath";
            this.textEditOutputPath.Size = new System.Drawing.Size(455, 20);
            this.textEditOutputPath.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "输出目录：";
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Location = new System.Drawing.Point(334, 612);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(110, 27);
            this.simpleButtonClose.TabIndex = 6;
            this.simpleButtonClose.Text = "关闭";
            this.simpleButtonClose.Click += new System.EventHandler(this.simpleButtonClose_Click);
            // 
            // simpleButtonCreate
            // 
            this.simpleButtonCreate.Location = new System.Drawing.Point(218, 612);
            this.simpleButtonCreate.Name = "simpleButtonCreate";
            this.simpleButtonCreate.Size = new System.Drawing.Size(110, 27);
            this.simpleButtonCreate.TabIndex = 5;
            this.simpleButtonCreate.Text = "开始生成";
            this.simpleButtonCreate.Click += new System.EventHandler(this.simpleButtonCreate_Click);
            // 
            // CodeBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 662);
            this.Controls.Add(this.simpleButtonClose);
            this.Controls.Add(this.simpleButtonCreate);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "CodeBuilderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量生成代码";
            this.Load += new System.EventHandler(this.CodeBuilderForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodeBuilderForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListDataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOutputPath.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.RichTextBox richTextBoxConnectionString;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.ListBox listBoxDataViewDes;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.ListBox listBoxTemplateDes;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEditOutputPath;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCreate;
        private DevExpress.XtraEditors.SimpleButton simpleButtonChooseFile;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDVAdds;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDVRemoves;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDVRemove;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDVAdd;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTpRemoves;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTpRemove;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTpAdd;
        private DevExpress.XtraEditors.SimpleButton simpleButtonTpAdds;
        private UserControls.UcTemplateTV ucTemplateTV1;
        private DevExpress.XtraTreeList.TreeList treeListDataView;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn_View;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnDataViewID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnDataViewPID;
    }
}