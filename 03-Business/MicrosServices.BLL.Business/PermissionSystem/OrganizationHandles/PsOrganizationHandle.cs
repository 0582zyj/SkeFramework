using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using SkeFramework.Core.SnowFlake;
using MicrosServices.Helper.DataUtil.Tree;
using System.Linq.Expressions;
using MicrosServices.Entities.Constants;
using System.Collections.Generic;
using MicrosServices.BLL.Business;
using MicrosServices.Helper.Core.Constants;
using SkeFramework.Core.Common.Collections;

namespace MicrosServices.BLL.SHBusiness.PsOrganizationHandles
{
    public class PsOrganizationHandle : PsOrganizationHandleCommon, IPsOrganizationHandle
  {
        public PsOrganizationHandle(IRepository<PsOrganization> dataSerialer)
            : base(dataSerialer)
        {
        }
        /// <summary>
        /// 新增一个组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int OrganizationInsert(PsOrganization model)
        {
            PsOrganization ParentInfo = this.GetOrgInfo(model.ParentNo);
            model.TreeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsOrganization>(ParentInfo, model.ParentNo);
            model.OrgNo = AutoIDWorker.Example.GetAutoSequence();
            model.InputTime = DateTime.Now;
            model.Enabled = 1;
            return this.Insert(model);
        }
        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public int OrganizationUpdate(PsOrganization organization)
        {
            PsOrganization current = this.GetModelByKey(organization.id.ToString());
            if (current == null)
                return 0;
            PsOrganization ParentInfo = this.GetOrgInfo(organization.ParentNo);
            if (organization.ParentNo != ConstantsData.DEFAULT_ID && ParentInfo == null)
            {
                throw new ArgumentException("父节点不存在");
            }
            string treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsOrganization>(ParentInfo, organization.ParentNo);
            bool isChange = current.TreeLevelNo != treeLevelNo;
            current.TreeLevelNo = treeLevelNo;
            current.UpdateTime = DateTime.Now;
            current.Name = organization.Name;
            current.ParentNo = organization.ParentNo;
            current.Description = organization.Description;
            current.Category = organization.Category;
            current.Enabled = organization.Enabled;
            int result = this.Update(current);
            if (result > 0 && isChange)
            {
                this.RefreshChildTreeLevelNo(current);
            }
            return result;
        }

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="int"></param>
        public int OrganizationDelete(int id)
        {
            PsOrganization organization = this.GetModelByKey(id.ToString());
            if (organization == null)
                return 0;
            long orgNo = organization.OrgNo;
            //检查编号是否有子节点
            this.CheckNoHasChild(orgNo);
            //删除机构下角色列表
            DataHandleManager.Instance().PsOrgRolesHandle.DeleteOrgRoles(orgNo);
            //删除机构下用户列表
            DataHandleManager.Instance().PsUserOrgHandle.DeleteUserOrgsByOrgNo(orgNo);
            return this.Delete(id);
        }
        #region 检查
        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="Nos"></param>
        /// <returns></returns>
        public bool CheckOrgNoIsExist(long Nos)
        {
            Expression<Func<PsOrganization, bool>> where = (o => o.OrgNo == Nos);
            long count = this.Count(where);
            if (count == 0)
            {
                throw new ArgumentException(String.Format("编号[{0}]不存在", Nos));
            }
            return false;
        }

        #endregion

        /// <summary>
        /// 获取机构信息
        /// </summary>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        public PsOrganization GetOrgInfo(long OrgNo)
        {
            if (OrgNo == ConstData.DefaultNo)
            {
                return null;
            }
            Expression<Func<PsOrganization, bool>> where = (o => o.OrgNo == OrgNo);
            return this.Get(where);
        }

        /// <summary>
        /// 获取机构树列表
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetPlatformOrganizationTree(long PlatformNo)
        {
            List<TreeNodeInfo> treeNodes = new List<TreeNodeInfo>();
            Expression<Func<PsOrganization, bool>> where = (o => o.PlatformNo == PlatformNo);
            List<PsOrganization> list = this.GetList(where).ToList();
            //if(Coll)
            foreach (var item in list)
            {
                treeNodes.Add(new TreeNodeInfo()
                {
                    TreeNo = item.OrgNo.ToString(),
                    ParentNo = item.ParentNo.ToString(),
                    Name = item.Name
                });
            }
            return treeNodes;
        }
        /// <summary>
        /// 检查编号是否有子节点
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public bool CheckNoHasChild(long OrgNo)
        {
            Expression<Func<PsOrganization, bool>> where = (o => o.ParentNo == OrgNo);
            long count = this.Count(where);
            if (count > 0)
            {
                throw new ArgumentException("下级节点不为空，暂不支持删除！");
            }
            return true;
        }
        /// <summary>
        /// 刷新子节点编号路径
        /// </summary>
        /// <param name="current"></param>
        public void RefreshChildTreeLevelNo(PsOrganization current)
        {
            long OrgNo = current.OrgNo;
            List<PsOrganization> childMenuList = this.GetChildOrganizationList(OrgNo);
            if (CollectionUtils.IsEmpty(childMenuList))
                return;
            foreach (PsOrganization item in childMenuList)
            {
                string treeLevelNo = "-1";
                if (item.ParentNo.Equals(OrgNo))
                {
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsOrganization>(current, item.ParentNo);
                }
                else
                {
                    PsOrganization parent = childMenuList.Find(o => o.OrgNo == item.ParentNo);
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsOrganization>(parent, item.ParentNo);
                }
                item.TreeLevelNo = treeLevelNo;
                this.UpdateTreeLevelNo(OrgNo, treeLevelNo);
            }
        }
    }
}
