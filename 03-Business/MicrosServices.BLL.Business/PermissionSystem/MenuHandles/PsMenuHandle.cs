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
        /// <param name="management"></param>
        /// <returns></returns>
        public int MenuUpdate(PsMenu management)
        {
            PsMenu model = this.GetModelByKey(management.id.ToString());
            if (model != null)
            {
                model.UpdateTime = DateTime.Now;
                model.Name = management.Name;
                model.ParentNo = management.ParentNo;
                model.PlatformNo = management.PlatformNo;
                model.Sort = management.Sort;
                model.url = management.url;
                model.Value = management.Value;
                model.icon = management.icon;
                model.Enabled = management.Enabled;
                PsMenu ParentInfo = this.GetModelByKey(management.ParentNo.ToString());
                model.TreeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsMenu>(ParentInfo, management.ParentNo);
                return this.Update(model);
            }
            return 0;
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
            return this.GetOptionValues(PlatformNo);
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
                throw new ArgumentException(String.Format("下级节点不为空，暂不支持删除！", ManagementNo));
            }
            return true;
        }
    }
}
