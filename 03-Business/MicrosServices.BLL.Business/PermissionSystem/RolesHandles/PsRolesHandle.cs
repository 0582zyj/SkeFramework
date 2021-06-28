using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using System.Linq.Expressions;
using SkeFramework.Core.SnowFlake;
using MicrosServices.Helper.DataUtil.Tree;
using MicrosServices.BLL.Business;
using SkeFramework.Core.Common.Collections;
using System.Collections.Generic;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Entities.Constants.Enums;
using MicrosServices.Helper.Core.Common;

namespace MicrosServices.BLL.SHBusiness.PsRolesHandles
{
    public class PsRolesHandle : PsRolesHandleCommon, IPsRolesHandle
    {
        public PsRolesHandle(IRepository<PsRoles> dataSerialer)
            : base(dataSerialer)
        {
        }
        #region 检查
        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="Nos"></param>
        /// <returns></returns>
        public bool CheckRolesNoIsExist(long Nos)
        {
            Expression<Func<PsRoles, bool>> where = (o => o.RolesNo == Nos);
            long count = this.Count(where);
            if (count == 0)
            {
                throw new ArgumentException(String.Format("权限[{0}]不存在", Nos));
            }
            return false;
        }

        #endregion
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int RolesInsert(PsRoles model)
        {
            model.RolesNo = AutoIDWorker.Example.GetAutoSequence();
            model.InputTime = DateTime.Now;
            model.Enabled = 1;
            PsRoles ParentInfo = this.GetModelByKey(model.ParentNo.ToString());
            model.TreeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsRoles>(ParentInfo, model.ParentNo);
            return this.Insert(model);
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public int RolesUpdate(PsRoles roles)
        {
            PsRoles current = this.GetModelByKey(roles.id.ToString());
            if (current == null)
                return 0;
            PsRoles ParentInfo = this.GetRolesInfo(roles.ParentNo);
            if (roles.ParentNo != ConstantsData.DEFAULT_ID && ParentInfo == null)
            {
                throw new ArgumentException("父节点不存在");
            }
            string treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsRoles>(ParentInfo, roles.ParentNo);
            bool isChange = current.TreeLevelNo != treeLevelNo;
            current.TreeLevelNo = treeLevelNo;
            current.UpdateTime = DateTime.Now;
            current.Name = roles.Name;
            current.ParentNo = roles.ParentNo;
            current.RolesType = roles.RolesType;
            current.Description = roles.Description;
            current.ManagementValue = roles.ManagementValue;
            current.Enabled = roles.Enabled;
            int result = this.Update(current);
            if (result > 0 && isChange)
            {
                this.RefreshChildTreeLevelNo(current);
            }
            return result;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int RolesDelete(int id)
        {
            PsRoles roles = this.GetModelByKey(id.ToString());
            if (roles == null)
                return 0;
            long rolesNo = roles.RolesNo;
            //检查编号是否有子节点
            this.CheckNoHasChild(rolesNo);
            //删除相关关系表
            DataHandleManager.Instance().PsManagementRolesHandle.DeleteManagementRoles(rolesNo);
            DataHandleManager.Instance().PsOrgRolesHandle.DeleteOrgRolesByRolesNo(rolesNo);
            DataHandleManager.Instance().PsUserRolesHandle.DeleteUserRolesByRolesNo(rolesNo);
            return this.Delete(id);
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int BatchRolesDelete(long[] rolesNos)
        {
            if (CollectionUtils.IsEmpty(rolesNos))
                return 0;
            int DeleteResult = 0;
            foreach(var item in rolesNos)
            {
                PsRoles roles = this.GetRolesInfo(item);
                if (roles == null)
                    return 0;
                long rolesNo = roles.RolesNo;
                DataHandleManager.Instance().PsManagementRolesHandle.DeleteManagementRoles(rolesNo);
                DataHandleManager.Instance().PsOrgRolesHandle.DeleteOrgRolesByRolesNo(rolesNo);
                DataHandleManager.Instance().PsUserRolesHandle.DeleteUserRolesByRolesNo(rolesNo);
                DeleteResult+= this.Delete((int)roles.id);
            }
            return DeleteResult;
        }
        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="Nos"></param>
        /// <returns></returns>
        public PsRoles GetRolesInfo(long RolesNo)
        {
            Expression<Func<PsRoles, bool>> where = (o => o.RolesNo == RolesNo);
            return this.Get(where);
        }

        /// <summary>
        /// 获取角色树信息
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetPlatformRolesTree(long platformNo)
        {
            List<TreeNodeInfo> treeNodes = new List<TreeNodeInfo>();
            Expression<Func<PsRoles, bool>> where = (o => o.PlatformNo == platformNo);
            List<PsRoles> list = this.GetList(where).ToList();
            //if(Coll)
            foreach (var item in list)
            {
                treeNodes.Add(new TreeNodeInfo()
                {
                    TreeNo = item.RolesNo.ToString(),
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
        public bool CheckNoHasChild(long RolesNo)
        {
            Expression<Func<PsRoles, bool>> where = (o => o.ParentNo == RolesNo);
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
        public void RefreshChildTreeLevelNo(PsRoles current)
        {
            long RolesNo = current.RolesNo;
            List<PsRoles> childMenuList = this.GetChildManagementList(RolesNo);
            if (CollectionUtils.IsEmpty(childMenuList))
                return;
            foreach (PsRoles item in childMenuList)
            {
                string treeLevelNo = "-1";
                if (item.ParentNo.Equals(RolesNo))
                {
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsRoles>(current, item.ParentNo);
                }
                else
                {
                    PsRoles parent = childMenuList.Find(o => o.RolesNo == item.ParentNo);
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsRoles>(parent, item.ParentNo);
                }
                item.TreeLevelNo = treeLevelNo;
                this.UpdateTreeLevelNo(RolesNo, treeLevelNo);
            }
        }

        /// <summary>
        /// 获取当前平台可分配的角色列表
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetPlatformRoleOptionValues(long PlatfromNo)
        {
            long UserPlatfromNo = PlatfromNo;
            List<long> ParentPlatformNos = DataHandleManager.Instance().PsPlatformHandle.GetParentPlatformNos(PlatfromNo);
            if (CollectionUtils.IsEmpty(ParentPlatformNos))
            {
                UserPlatfromNo = -1;
            }
            ParentPlatformNos.Add(PlatfromNo);
            return this.GetOptionValues(ParentPlatformNos, UserPlatfromNo, (int)RolesType.AUTH_TYPE);
        }

    }
}
