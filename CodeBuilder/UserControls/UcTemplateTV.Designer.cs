namespace CodeBuilder.UserControls
{
    partial class UcTemplateTV
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.treeListTemplate = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnTempID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnTempPID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeListTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListTemplate
            // 
            this.treeListTemplate.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn_Name,
            this.treeListColumnTempID,
            this.treeListColumnTempPID});
            this.treeListTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListTemplate.Location = new System.Drawing.Point(0, 0);
            this.treeListTemplate.Name = "treeListTemplate";
            this.treeListTemplate.Size = new System.Drawing.Size(190, 577);
            this.treeListTemplate.TabIndex = 2;
            // 
            // treeListColumn_Name
            // 
            this.treeListColumn_Name.Caption = "模板管理";
            this.treeListColumn_Name.FieldName = "Name";
            this.treeListColumn_Name.Name = "treeListColumn_Name";
            this.treeListColumn_Name.Visible = true;
            this.treeListColumn_Name.VisibleIndex = 0;
            // 
            // treeListColumnTempID
            // 
            this.treeListColumnTempID.Caption = "序号";
            this.treeListColumnTempID.FieldName = "ID";
            this.treeListColumnTempID.Name = "treeListColumnTempID";
            // 
            // treeListColumnTempPID
            // 
            this.treeListColumnTempPID.Caption = "所属序号";
            this.treeListColumnTempPID.FieldName = "ParentID";
            this.treeListColumnTempPID.Name = "treeListColumnTempPID";
            // 
            // UcTemplateTV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListTemplate);
            this.Name = "UcTemplateTV";
            this.Size = new System.Drawing.Size(190, 577);
            this.Load += new System.EventHandler(this.UcTemplateTV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListTemplate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn_Name;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnTempID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnTempPID;
        internal DevExpress.XtraTreeList.TreeList treeListTemplate;
    }
}
