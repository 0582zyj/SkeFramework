using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using System.Linq.Expressions;
using MicrosServices.BLL.Business;
using SkeFramework.Core.SnowFlake;
using System.Collections.Generic;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.DataUtil.Tree;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;
using MicrosServices.Helper.Core.Constants;
using SkeFramework.Core.Common.Collections;

namespace MicrosServices.BLL.SHBusiness.PsManagementHandles
{
    public class PsManagementHandle : PsManagementHandleCommon, IPsManagementHandle
    {
        public PsManagementHandle(IRepository<PsManagement> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 新增一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        public int ManagementInsert(PsManagement management)
        {
            management.ManagementNo = AutoIDWorker.Example.GetAutoSequence();
            PsManagement ParentInfo = this.GetManagementInfo(management.ParentNo);
            management.TreeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsManagement>(ParentInfo, management.ParentNo);
            management.InputTime = DateTime.Now;
            management.Enabled = 1;
            return this.Insert(management);
        }

        /// <summary>
        /// 新增一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        public int ManagementUpdate(PsManagement management)
        {
            PsManagement current = this.GetModelByKey(management.id.ToString());
            if (current == null)
                return 0;
            PsManagement ParentInfo = this.GetManagementInfo(management.ParentNo);
            if (ParentInfo.ParentNo != ConstantsData.DEFAULT_ID && ParentInfo == null)
            {
                throw new ArgumentException("父节点不存在");
            }
            string treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsManagement>(ParentInfo, management.ParentNo);
            bool isChange = current.TreeLevelNo != treeLevelNo;
            current.TreeLevelNo = treeLevelNo;
            current.UpdateTime = DateTime.Now;
            current.Name = management.Name;
            current.ParentNo = management.ParentNo;
            current.Description = management.Description;
            current.Type = management.Type;
            current.Value = management.Value;
            current.PlatformNo = management.PlatformNo;
            current.Sort = management.Sort;
            current.Enabled = management.Enabled;
            int result = this.Update(current);
            if (result > 0 && isChange)
            {
                this.RefreshChildTreeLevelNo(current);
            }
            return result;
        }

        /// <summary>
        /// 删除一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        public int ManagementDelete(int id)
        {
            PsManagement management = this.GetModelByKey(id.ToString());
            if (management == null)
                return 0;
            long managementNo = management.ManagementNo;
            //检查编号是否有子节点
            this.CheckNoHasChild(managementNo);
            //移除分组权限
            if (management.Type == (int)ManagementType.GROUP_TYPE)
            {
                DataHandleManager.Instance().PsMenuManagementHandle.DeleteMenuManagements(managementNo, new List<int>() {  });
            }
            //移除菜单、分组和权限
            DataHandleManager.Instance().PsMenuManagementHandle.DeleteManagementMenus(managementNo, new List<int>());
            //移除角色权限
            DataHandleManager.Instance().PsManagementRolesHandle.DeleteRoleManagementsByManagementNo(managementNo);
            return this.Delete(id);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        public int ManagementBeachDelete(List<long> ManagementNos)
        {
            return 0;
        }


        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool CheckManagementNameIsExist(string Name,long ManagementNo=ConstData.DefaultNo)
        {
            Expression<Func<PsManagement, bool>> where = (o => o.Name == Name&&(o.ManagementNo==ConstData.DefaultNo || o.ManagementNo!=ManagementNo));
            long count = this.Count(where);
            if (count > 0)
            {
                throw new ArgumentException(String.Format("名称[{0}]已存在", Name));
            }
            return false;
        }
        /// <summary>
        /// 检查编码是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public bool CheckManagementNoIsExist(long ManagementNo)
        {
            if (ManagementNo !=ConstData.DefaultNo)
            {
                Expression<Func<PsManagement, bool>> where = (o => o.ManagementNo == ManagementNo);
                long count = this.Count(where);
                if (count == 0)
                {
                    throw new ArgumentException(String.Format("权限编号[{0}]不存在", ManagementNo));
                }
            }
            return false;
        }

        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public PsManagement GetManagementInfo(long ManagementNo)
        {
            if (ManagementNo == ConstData.DefaultNo)
            {
                return null;
            }
            Expression<Func<PsManagement, bool>> where = (o => o.ManagementNo == ManagementNo);
            return this.Get(where);
        }

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetRolesOptionValues(long PlatformNo, int ManagementType = ConstData.DefaultInt)
        {
            return this.GetOptionValues(new List<int> { ManagementType }, new List<long> { PlatformNo });
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetPlatformManagementTree(long PlatformNo)
        {
            List<TreeNodeInfo> treeNodes = new List<TreeNodeInfo>();
            Expression<Func<PsManagement, bool>> where = (o => o.PlatformNo == PlatformNo);
            List<PsManagement> list = this.GetList(where).ToList();
            //if(Coll)
            foreach (var item in list)
            {
                treeNodes.Add(new TreeNodeInfo()
                {
                    TreeNo = item.ManagementNo.ToString(),
                    ParentNo = item.ParentNo.ToString(),
                    Name = item.Name
                });
            }
            return treeNodes;
        }

        /// <summary>
        /// 获取用户操作权限键值对[菜单权限+分组权限-去重]
        /// </summary>
        /// <returns></returns>      
        public List<ManagementOptionValue> GetUserManagementList(string UserNo)
        {
            List<PsManagement> managementGroups = this.GetUserManagementGroupList( UserNo);
            if (!CollectionUtils.IsEmpty(managementGroups))
            {
                List<long> MenuNos = managementGroups.Select(o => o.ManagementNo).ToList();
                return DataHandleManager.Instance().PsMenuManagementHandle.GetManagementOptionValues(MenuNos, (int)ManagementType.GROUP_TYPE);
            }
            return new List<ManagementOptionValue>();
        }

        /// <summary>
        /// 获取用户分组列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public List<PsManagement> GetUserManagementGroupList(string UserNo)
        {
            List<PsRoles> roles = DataHandleManager.Instance().PsRolesHandle.GetUserRoleList(UserNo);
            if (CollectionUtils.IsEmpty(roles))
                return new List<PsManagement>();
            List<long> RolesNos = roles.Select(o => o.RolesNo).ToList();
            return DataHandleManager.Instance().PsManagementHandle.GetRoleManagementList(RolesNos, (int)ManagementType.GROUP_TYPE);
        }

        /// <summary>
        /// 检查编号是否有子节点
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public bool CheckNoHasChild(long ManagementNo)
        {
            Expression<Func<PsManagement, bool>> where = (o => o.ParentNo == ManagementNo);
            long count = this.Count(where);
            if (count > 0)
            {
                throw new ArgumentException(String.Format("下级节点不为空，暂不支持删除！", ManagementNo));
            }
            return true;
        }

        /// <summary>
        /// 刷新子节点编号路径
        /// </summary>
        /// <param name="current"></param>
        public void RefreshChildTreeLevelNo(PsManagement current)
        {
            long ManagementNo = current.ManagementNo;
            List<PsManagement> childMenuList = this.GetChildManagementList(ManagementNo);
            if (CollectionUtils.IsEmpty(childMenuList))
                return;
            foreach (PsManagement item in childMenuList)
            {
                String treeLevelNo = "-1";
                if (item.ParentNo.Equals(ManagementNo))
                {
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsManagement>(current, item.ParentNo);
                }
                else
                {
                    PsManagement parent = childMenuList.Find(o => o.ManagementNo == item.ParentNo);
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsManagement>(parent, item.ParentNo);
                }
                item.TreeLevelNo = treeLevelNo;
                this.UpdateTreeLevelNo(ManagementNo, treeLevelNo);
            }
        }

        /// <summary>
        /// 统计平台的权限数
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <returns></returns>
        public long CountByPlatformNo(long PlatformNo)
        {
            Expression<Func<PsManagement, bool>> where = (o => o.PlatformNo == PlatformNo);
            return this.Count(where);
        }
    }
}
