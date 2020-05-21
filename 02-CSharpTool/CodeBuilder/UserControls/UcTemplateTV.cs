using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkeFramework.Core.CodeBuilder.Model;
using SkeFramework.Core.CodeBuilder.DataServices;

namespace SkeFramework.Core.CodeBuilder.UserControls
{
    public partial class UcTemplateTV : UserControl
    {

        public IList<DataViewNode> TemplateNodeList
        {
            get { return this.treeListTemplate.DataSource as List<DataViewNode>; }
            set
            {
                LoadTemplateTreeList(value);
            }
        }


        public UcTemplateTV()
        {
            InitializeComponent();
        }

        private void UcTemplateTV_Load(object sender, EventArgs e)
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
            this.TemplateNodeList = list;
        }

        private void LoadTemplateTreeList(IList<DataViewNode> list)
        {
            this.treeListTemplate.Nodes.Clear();
            this.treeListTemplate.ParentFieldName = "ParentID";
            this.treeListTemplate.KeyFieldName = "ID";
            this.treeListTemplate.OptionsBehavior.Editable = false;
            this.treeListTemplate.DataSource = list;
            this.treeListTemplate.ExpandAll();
        }
    }
}
