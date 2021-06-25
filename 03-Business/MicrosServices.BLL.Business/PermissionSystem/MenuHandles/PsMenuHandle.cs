using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using System.Linq.Expressions;
using MicrosServices.Entities.Constants;
using SkeFramework.Core.SnowFlake;
using MicrosServices.Helper.DataUtil.Tree;
using System.Collections.Generic;
using MicrosServices.Helper.Core.Common;
using MicrosServices.BLL.Business;
using MicrosServices.Helper.Core.Constants;
using SkeFramework.Core.Common.Collections;

namespace MicrosServices.BLL.SHBusiness.PsMenuHandles
{
    public class PsMenuHandle : PsMenuHandleCommon, IPsMenuHandle
    {
        public PsMenuHandle(IRepository<PsMenu> dataSerialer)
            : base(dataSerialer)
        {
        }

        #region Check
        /// <summary>
        /// 检查名称是否重复
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool CheckNameIsExist(long MenuNo, string Name)
        {
            Expression<Func<PsMenu, bool>> where = (n => n.MenuNo != MenuNo && n.Name == Name);
            return this.Count(where) > 0 ? true : false;
        }

        /// <summary>
        /// 检查菜单编号是否存在
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        public bool CheckMenuNoIsExist(long MenuNo)
        {
            if (MenuNo != ConstData.DefaultNo)
            {
                Expression<Func<PsMenu, bool>> where = (n => n.MenuNo == MenuNo);
                return this.Count(where) > 0 ? true : false;
            }
            return true;
        }

        #endregion

        #region Base 
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int MenuInsert(PsMenu model)
        {
            model.MenuNo = AutoIDWorker.Example.GetAutoSequence();
            model.InputTime = DateTime.Now;
            model.Enabled = 1;
            PsMenu ParentInfo = this.GetModelByKey(model.ParentNo.ToString());
            model.TreeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsMenu>(ParentInfo, model.ParentNo);
            return this.Insert(model);
        }
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public int MenuUpdate(PsMenu menu)
        {
            PsMenu current = this.GetModelByKey(menu.id.ToString());
            if (current == null)
                return 0;
            PsMenu ParentInfo = this.GetMenuInfo(menu.ParentNo);
            if (menu.ParentNo != ConstantsData.DEFAULT_ID && ParentInfo == null)
            {
                throw new ArgumentException("父节点不存在");
            }
            string treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsMenu>(ParentInfo, menu.ParentNo);
            bool isChange = current.TreeLevelNo != treeLevelNo;
            current.UpdateTime = DateTime.Now;
            current.Name = menu.Name;
            current.ParentNo = menu.ParentNo;
            current.PlatformNo = menu.PlatformNo;
            current.Sort = menu.Sort;
            current.url = menu.url;
            current.Value = menu.Value;
            current.icon = menu.icon;
            current.Enabled = menu.Enabled;
            current.TreeLevelNo = treeLevelNo;
            int result = this.Update(current);
            if (result > 0 && isChange)
            {
                this.RefreshChildTreeLevelNo(current);
            }
            return result ;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int MenuDelete(int id)
        {
            PsMenu menu = this.GetModelByKey(id.ToString());
            if (menu == null)
                return 0;
            long MenuNo=menu.MenuNo;
            //检查编号是否有子节点
            this.CheckNoHasChild(MenuNo);
            //删除菜单权限和权限菜单关系
            DataHandleManager.Instance().PsMenuManagementHandle.DeleteMenuManagements(menu.MenuNo, new List<int>());
            return this.Delete(id);
        }

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetMenusOptionValues(long PlatformNo)
        {
            return this.GetOptionValues(new List<long>() { PlatformNo });
        }
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="MenuNo"></param>
        /// <returns></returns>
        public PsMenu GetMenuInfo(long MenuNo)
        {
            Expression<Func<PsMenu, bool>> where = (n => n.MenuNo == MenuNo);
            return this.Get(where) ;
        }
        #endregion

        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="UserNo"></param>
        /// <returns></returns>
        public List<PsMenu> GetUserMenusList(string UserNo)
        {
            List<PsRoles> roles = DataHandleManager.Instance().PsRolesHandle.GetUserRoleList(UserNo);
            if (CollectionUtils.IsEmpty(roles))
                return new List<PsMenu>();
            List<long> RolesNos = roles.Select(o => o.RolesNo).ToList();
            List<PsManagement> menus = DataHandleManager.Instance().PsManagementHandle.GetRoleManagementList(RolesNos,(int)ManagementType.MENU_TYPE);
            if (CollectionUtils.IsEmpty(menus))
                return new List<PsMenu>();
            List<long> ManagenmentNos = menus.Select(o => o.ManagementNo).ToList();
            List<PsMenu> list = DataHandleManager.Instance().PsMenuHandle.GetManagementMenusList(ManagenmentNos).ToList();
            return list;
        }

        /// <summary>
        /// 获取平台菜单树
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetPlatformMenuTree(long PlatformNo)
        {
            List<TreeNodeInfo> treeNodes = new List<TreeNodeInfo>();
            Expression<Func<PsMenu, bool>> where =  (o => o.PlatformNo == PlatformNo);
            List<PsMenu> list =this.GetList(where).ToList();
            //if(Coll)
            foreach(var item in list)
            {
                treeNodes.Add(new TreeNodeInfo()
                {
                    TreeNo = item.MenuNo.ToString(),
                    ParentNo=item.ParentNo.ToString(),
                    Name=item.Name
                });
            }
            return treeNodes;
        }
        /// <summary>
        /// 检查编号是否有子节点
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public bool CheckNoHasChild(long MenuNo)
        {
            Expression<Func<PsMenu, bool>> where = (o => o.ParentNo == MenuNo);
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
        public void RefreshChildTreeLevelNo(PsMenu current)
        {
            List<PsMenu> childMenuList = this.GetChildMenuList(current.MenuNo);
            if (CollectionUtils.IsEmpty(childMenuList))
                return;
            foreach (PsMenu menu in childMenuList)
            {
                String treeLevelNo = "-1";
                if (menu.ParentNo.Equals(current.MenuNo))
                {
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsMenu>(current, menu.ParentNo);
                }
                else
                {
                    PsMenu parent = childMenuList.Find(o=>o.MenuNo==menu.ParentNo);
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsMenu>(parent, menu.ParentNo);
                }
                menu.TreeLevelNo=treeLevelNo;
                this.UpdateTreeLevelNo(menu.MenuNo, treeLevelNo);
            }
        }
    }
}
