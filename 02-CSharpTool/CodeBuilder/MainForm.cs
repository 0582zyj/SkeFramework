using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using SkeFramework.Core.CodeBuilder.Model;
using SkeFramework.Core.CodeBuilder.UserForm;
using SkeFramework.Core.CodeBuilder.Global;
using DevExpress.XtraEditors;
using SkeFramework.Core.CodeBuilder.DataServices;
using SkeFramework.Core.CodeBuilder.DataCommon;
using SkeFramework.Core.CodeBuilder.DataServices.Interfaces;
using SkeFramework.Core.CodeBuilder.DAL.Repositorys;
using SkeFramework.Core.CodeBuilder;
using SkeFramework.Core.CodeBuilder.DataEntities;
using SkeFramework.Core.CodeBuilder.DataFactory;

namespace SkeFramework.Core.CodeBuilder
{
    public partial class MainForm : RibbonForm
    {
        private IList<DataViewNode> RootDataViewList = new List<DataViewNode>();

        /// <summary>
        /// 数据视图
        /// </summary>
        private IList<DataViewNode> DataViewNodeList
        {
            get { return this.treeListDataView.DataSource as List<DataViewNode>; }
            set
            {
                LoadDataViewTreeList(value);
            }
        }
        /// <summary>
        /// 当前生成类
        /// </summary>
        private ICreate create = null;
        /// <summary>
        /// 当前表名
        /// </summary>
        private string CurrentTableName = string.Empty;

        private string ProjectName {
            get
            {
                string Name = this.barEditItemProjectName.Edit.NullText;
                if (this.barEditItemProjectName.EditValue != null)
                {
                    Name = this.barEditItemProjectName.EditValue.ToString();
                }
                return Name;
            }
        }
        public MainForm()
        {
            InitializeComponent();
            StatusBarManager.ConnectionStateChanged += StatusBarManager_ConnectionStateChanged;
            StatusBarManager.OperationStateChanged += StatusBarManager_OperationStateChanged;
            this.barEditItemProjectName.EditValue = "SmartCloudIOT";
            this.Text += SoftwareVersion.GetVersion();
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //数据库管理: Alt+Q
            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.Q) { this.ShowDataBaseManager();  return; }
            //批量生成: Alt+Q
            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.B && this.toolStripButtonCreateAll.Enabled==true) { this.ShowCodeBuilderForm();return; }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult result= XtraMessageBox.Show(@"确认要退出系统吗？", @"提醒", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                return;
            }
         
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.RootDataViewList = new List<DataViewNode>();
            this.RootDataViewList.Add(new DataViewNode(1, "数据表", 0));
            this.RootDataViewList.Add(new DataViewNode(2, "视图", 0));
            this.DataViewNodeList = this.RootDataViewList;
            this.LoadTemplateTreeList();
        }

        #region 底部状态栏
        void StatusBarManager_OperationStateChanged(string obj)
        {
            this.barStaticItemOperationInfo.Caption = obj;
        }

        void StatusBarManager_ConnectionStateChanged(string DataBase, string State)
        {
            this.siStatusDataBase.Caption = string.Format("当前数据库：{0}", DataBase);
            this.siInfoConnectionState.Caption = string.Format("连接状态：{0}", State);
            if (State.Equals("Open"))
            {
                this.toolStripButtonCreateCode.Enabled = true;
                this.toolStripButtonCreateAll.Enabled = true;
                this.treeListDataView.Enabled = true;
                this.treeListTemplate.Enabled = true;
            }
        }
        #endregion

        #region 数据视图

        /// <summary>
        /// 初始化树
        /// </summary>
        public void LoadDataViewTreeList(IList<DataViewNode> NodeList)
        {
            if (NodeList != null && NodeList.Count > 0)
            {
                this.treeListDataView.Nodes.Clear();
                this.treeListDataView.ParentFieldName = "ParentID";
                this.treeListDataView.KeyFieldName = "ID";
                this.treeListDataView.OptionsBehavior.Editable = false;
                this.treeListDataView.DataSource = NodeList;
                this.treeListDataView.ExpandAll();
            }

        }

        private void treeListDataView_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var FocusedNode = this.treeListDataView.FocusedNode;
            if (FocusedNode != null)
            {
                int id = Convert.ToInt32(FocusedNode.GetValue("ParentID"));
                if (id != 0)
                {
                    var tableName = FocusedNode.GetValue("Name");
                    this.LoadGridView(tableName.ToString());
                }
            }
        }
        /// <summary>
        /// 加载列表
        /// </summary>
        /// <param name="tableName"></param>
        private void LoadGridView(string tableName)
        {
            var ChildList = new List<ColumnsEntities>();
            string database = DbFactory.Instance().GetDatabase();
            ChildList = DataHandleManager.repository.GetColumnsList(tableName, database);
            this.gridControl1.DataSource = ChildList.ToList();
        }

        #endregion

        #region 模板管理
        private void LoadTemplateTreeList()
        {
            List<DataViewNode> list = new List<DataViewNode>();
            list.Add(new DataViewNode(1, "SkeFramework", 0));

            list.Add(new DataViewNode(2, "00.Entities", 1));
            list.Add(new DataViewNode(CreateEntities.Type, CreateEntities.Name, 2));
       

            list.Add(new DataViewNode(3, "01.DAL", 1));
            list.Add(new DataViewNode(CreateHandleCommonInterface.Type, CreateHandleCommonInterface.Name, 3));
            list.Add(new DataViewNode(CreateHandleCommon.Type, CreateHandleCommon.Name, 3));

            list.Add(new DataViewNode(4, "02.BLL", 1));
            list.Add(new DataViewNode(CreateHandleInterface.Type, CreateHandleInterface.Name, 4));
            list.Add(new DataViewNode(CreateHandles.Type, CreateHandles.Name, 4));

            list.Add(new DataViewNode(5, "05.UI", 1));
            list.Add(new DataViewNode(CreateController.Type, CreateController.Name, 5));
            list.Add(new DataViewNode(CreateViewList.Type, CreateViewList.Name, 5));
            list.Add(new DataViewNode(CreateViewAdd.Type, CreateViewAdd.Name, 5));
            list.Add(new DataViewNode(CreateViewUpdate.Type, CreateViewUpdate.Name, 5));

            list.Add(new DataViewNode(6, "06.COMMON", 1));
            list.Add(new DataViewNode(CreateIEntity.Type, CreateIEntity.Name, 6));
            list.Add(new DataViewNode(CreateDataHandleManager.Type, CreateDataHandleManager.Name, 6));
            

            this.treeListTemplate.Nodes.Clear();
            this.treeListTemplate.ParentFieldName = "ParentID";
            this.treeListTemplate.KeyFieldName = "ID";
            this.treeListTemplate.OptionsBehavior.Editable = false;
            this.treeListTemplate.DataSource = list;
            this.treeListTemplate.ExpandAll();
        }
        #endregion

        #region 工具栏
        private void barButtonItemDBOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ShowDataBaseManager();
        }
        /// <summary>
        /// 打开数据库连接管理
        /// </summary>
        private void ShowDataBaseManager()
        {
            ConnectionForm fm = new ConnectionForm();
            if (fm.ShowDialog() == DialogResult.OK)
            {
                string database = DbFactory.Instance().GetDatabase();
                List<TableEntities> list = DataHandleManager.repository.GetTableList(database);
                int AutoId = this.DataViewNodeList.Max(o => o.ID) + 1;
                List<DataViewNode> viewlist = this.RootDataViewList.ToList();
                foreach (var item in list)
                {
                    viewlist.Add(new DataViewNode(AutoId++, item.TABLE_NAME, 1));
                }
                this.DataViewNodeList = viewlist;
            }
        }
        #endregion

        #region 底部状态栏
        private void barButtonItemDbManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ShowDataBaseManager();
        }
        #endregion

        #region 结果工具栏
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonCreateCode_Click(object sender, EventArgs e)
        {
            var FocusedNode = this.treeListDataView.FocusedNode;
            int id = Convert.ToInt32(FocusedNode.GetValue("ParentID"));
            if (id == 0)
            {
                XtraMessageBox.Show(@"请先选择要生成的表名！", @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tableName = FocusedNode.GetValue("Name").ToString();
            var TemplateFocusedNode = this.treeListTemplate.FocusedNode;
            int TemplateID = Convert.ToInt32(TemplateFocusedNode.GetValue("ID"));
            string TemplateName = TemplateFocusedNode.GetValue("Name").ToString();
            if (!CreateFactory.Instance().CheckCreateTemplate( TemplateName))
            {
                XtraMessageBox.Show(@"请先选择要生成的模板！", @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.CurrentTableName = tableName;
            create = CreateFactory.Instance().GetCreateHandle(TemplateID);
            if (create != null)
            {
                string Message = create.CreateMethod(this.CurrentTableName, this.ProjectName);
                this.richTextBoxMessage.Text = Message;
                this.toolStripButtonSave.Enabled = true;
            }
            else
            {
                XtraMessageBox.Show(@"该模板生成类暂未实现！", @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
         
            string FilePath = this.barEditItemOutputPath.Edit.NullText.ToString();
            if (string.IsNullOrEmpty(FilePath))
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)//判断对话框是否被选中
                {
                    FilePath = fbd.SelectedPath;
                    this.barEditItemOutputPath.Edit.NullText = FilePath;
                }
            }
            if (create == null)
            {
                XtraMessageBox.Show(@"请先生成代码！", @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string Content=this.richTextBoxMessage.Text;
            bool result = create.SaveFileString(FilePath, this.CurrentTableName, Content, this.ProjectName);
            XtraMessageBox.Show(result == true ?@"保存成功！":@"保存失败！", @"消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 批量生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonCreateAll_Click(object sender, EventArgs e)
        {
            this.ShowCodeBuilderForm();
        }

        private void ShowCodeBuilderForm()
        {
            CodeBuilderForm fm = new CodeBuilderForm(this.ProjectName);
            fm.DataViewNodeList = this.DataViewNodeList.Where(o => o.ParentID == 1).ToList();
            if (fm.ShowDialog() == DialogResult.OK)
            {
                XtraMessageBox.Show(@"生成成功！", @"消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion


    }
}