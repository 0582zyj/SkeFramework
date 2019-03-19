using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeBuilder.BLL.Interfaces;
using CodeBuilder.DAL.DataFactory;
using CodeBuilder.DataFactory;
using CodeBuilder.Model;
using DevExpress.XtraEditors;

namespace CodeBuilder.UserForm
{
    public partial class CodeBuilderForm : XtraForm
    {
        /// <summary>
        /// 数据表
        /// </summary>
        public IList<DataViewNode> DataViewNodeList
        {
            get { return this.treeListDataView.DataSource as List<DataViewNode>; }
            set
            {
                LoadDataViewTreeList(value);
            }
        }

        public string ProjectName { get; private set; }

        public CodeBuilderForm(string projectName)
        {
            InitializeComponent();
            this.ProjectName = projectName;
        }

        #region 窗体事件
        private void CodeBuilderForm_Load(object sender, EventArgs e)
        {
            this.richTextBoxConnectionString.Text = DbFactory.Instance().GetConnectionString(); 
        }

        private void CodeBuilderForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                return;
            }
        }
        #endregion

        #region 初始化方法
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
        #endregion

        #region 按钮事件
        /// <summary>
        /// 开始生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonCreate_Click(object sender, EventArgs e)
        {
            if (this.listBoxDataViewDes.Items.Count==0)
            {
                XtraMessageBox.Show(@"请先选择要生成的数据表", @"提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.listBoxTemplateDes.Items.Count == 0)
            {
                XtraMessageBox.Show(@"请先选择要生成的模板", @"提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string OutputPath = this.textEditOutputPath.Text;
            if (string.IsNullOrEmpty(OutputPath))
            {
                XtraMessageBox.Show(@"请先选择要输出的目录", @"提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<DataViewNode> DvList = this.DataViewNodeList.Where(o => this.listBoxDataViewDes.Items.Contains(o.Name)).ToList();
            List<DataViewNode> TpList = this.ucTemplateTV1.TemplateNodeList.Where(o => this.listBoxTemplateDes.Items.Contains(o.Name)).ToList();

            foreach (var item in TpList)
            {
                ICreate create = CreateFactory.Instance().GetCreateHandle(item.ID);
                foreach (var itemDv in DvList)
                {
                    create.GenerationCode(OutputPath, itemDv.Name, this.ProjectName);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 选择目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonChooseFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)//判断对话框是否被选中
            {
                this.textEditOutputPath.Text = fbd.SelectedPath;
            }
        }
        #endregion

        #region 数据表选择事件

        private void simpleButtonDVAdds_Click(object sender, EventArgs e)
        {
            listBoxDataViewDes.Items.Clear();
            foreach (var item in this.DataViewNodeList)
            {
                listBoxDataViewDes.Items.Add(item.Name);
            }
        }

        private void simpleButtonDVAdd_Click(object sender, EventArgs e)
        {
            var select = this.treeListDataView.FocusedNode;
            if (select == null)
            {
                XtraMessageBox.Show(@"请先选择要添加的数据表", @"提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string name = select.GetValue("Name").ToString();
            if (!listBoxDataViewDes.Items.Contains(name))
            {
                listBoxDataViewDes.Items.Add(name);
            }
            else
            {
                XtraMessageBox.Show(@"该数据表已添加", @"提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void simpleButtonDVRemove_Click(object sender, EventArgs e)
        {
            if (this.listBoxDataViewDes.SelectedIndex == -1)
            {
                XtraMessageBox.Show(@"请先选择要移除的数据表", @"提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var name = this.listBoxDataViewDes.SelectedItem;
            this.listBoxDataViewDes.Items.Remove(name);
        }
        /// <summary>
        /// 移除全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDVRemoves_Click(object sender, EventArgs e)
        {
            this.listBoxDataViewDes.Items.Clear();
        }
        #endregion

        #region 模板选择事件
        private void simpleButtonTpAdds_Click(object sender, EventArgs e)
        {
            this.listBoxTemplateDes.Items.Clear();
            List<DataViewNode> list = this.ucTemplateTV1.TemplateNodeList.Where(o => o.ParentID != 0 && o.ParentID != 1).ToList();
            foreach (var item in list)
            {
                this.listBoxTemplateDes.Items.Add(item.Name);
            }
        }

        private void simpleButtonTpAdd_Click(object sender, EventArgs e)
        {
            var select = this.ucTemplateTV1.treeListTemplate.FocusedNode;
            if (select == null)
            {
                XtraMessageBox.Show(@"请先选择要添加的模板", @"提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string name = select.GetValue("Name").ToString();
            if (!listBoxTemplateDes.Items.Contains(name))
            {
                listBoxTemplateDes.Items.Add(name);
            }
            else
            {
                XtraMessageBox.Show(@"该模板已添加", @"提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void simpleButtonTpRemove_Click(object sender, EventArgs e)
        {
            if (this.listBoxTemplateDes.SelectedIndex == -1)
            {
                XtraMessageBox.Show(@"请先选择要移除的模板", @"提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var name = this.listBoxTemplateDes.SelectedItem;
            this.listBoxTemplateDes.Items.Remove(name);
        }

        private void simpleButtonTpRemoves_Click(object sender, EventArgs e)
        {
            this.listBoxTemplateDes.Items.Clear();
        }
        #endregion

  

       

    }
}
