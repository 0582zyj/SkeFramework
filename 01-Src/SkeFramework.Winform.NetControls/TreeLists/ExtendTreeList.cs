using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.NetControls.TreeLists
{
    /// <summary>
    /// 树节点扩展类
    /// </summary>
    public class ExtendTreeList: TreeList
    {
        //勾选|取消勾选 父节点，递归处理子节点
        private bool m_SetClick = true;
        private void SetSubNodeCheck(TreeListNode p_TreeNode, bool p_SelectCheck)
        {
            m_SetClick = false;
            foreach (TreeListNode _SubNode in p_TreeNode.Nodes)
            {
                _SubNode.Checked = p_SelectCheck;
                SetSubNodeCheck(_SubNode, p_SelectCheck);
            }
        }
        //勾选子节点，父节点同样勾上
        private void SetParentCheck(TreeListNode p_TreeNode)
        {
            if (p_TreeNode.Checked && p_TreeNode.ParentNode != null)
            {
                p_TreeNode.ParentNode.Checked = true;
                SetParentCheck(p_TreeNode.ParentNode);
            }
        }
        //如果当前分支下最后一个勾选的子节点取消勾选，父节点勾选状态取消
        private void SetParentNotCheck(TreeListNode p_TreeNode)
        {
            if (!p_TreeNode.Checked && p_TreeNode.ParentNode != null)
            {
                foreach (TreeListNode _Node in p_TreeNode.ParentNode.Nodes)
                {
                    if (_Node.Checked) return;
                }
                p_TreeNode.ParentNode.Checked = false;
                SetParentNotCheck(p_TreeNode.ParentNode);
            }
        }
        private void tlBoi_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            // 禁用TreeView视图重绘的功能。
            //tlDetail.BeginUpdate();            
            if (m_SetClick)
            {
                SetSubNodeCheck(e.Node, e.Node.Checked);
                SetParentCheck(e.Node);
                SetParentNotCheck(e.Node);
                m_SetClick = true;
            }
            // 启用TreeView视图重绘的功能。
            //tlDetail.EndUpdate();
        }
    }
}
