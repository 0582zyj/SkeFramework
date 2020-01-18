using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosServices.BLL.Business.UserCenter.UcUsersSettingHandles;
using MicrosServices.BLL.SHBusiness.PsManagementHandles;
using MicrosServices.BLL.SHBusiness.PsManagementRolesHandles;
using MicrosServices.BLL.SHBusiness.PsMenuHandles;
using MicrosServices.BLL.SHBusiness.PsMenuRolesHandles;
using MicrosServices.BLL.SHBusiness.PsOrganizationHandles;
using MicrosServices.BLL.SHBusiness.PsOrgRolesHandles;
using MicrosServices.BLL.SHBusiness.PsPlatformHandles;
using MicrosServices.BLL.SHBusiness.PsRolesHandles;
using MicrosServices.BLL.SHBusiness.PsUserOrgHandles;
using MicrosServices.BLL.SHBusiness.PsUserRolesHandles;
using MicrosServices.BLL.SHBusiness.UsersHandles;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.DataAccess.DataHandle.Achieve;
using SkeFramework.DataBase.Interfaces;

namespace ULCloudLockTool.BLL.Business.Achieve
{
    /// <summary>
    /// 具体工厂角色
    /// </summary>
    public class DALDataHandleFactory : DataHandleFactory
    {
        /// <summary>
        /// 对象生成器具体实现
        /// </summary>
        /// <typeparam name="IDataTableHandle"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public override IDataTableHandle GetDataHandleCommon<IDataTableHandle, TData>()
        {
            var dataType = typeof(TData);
            if (IsSubclassOf(typeof(PsManagement), dataType))
            {
                return new PsManagementHandle(GetConfigDataSerialer<PsManagement>(PsManagement.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PsManagementRoles), dataType))
            {
                return new PsManagementRolesHandle(GetConfigDataSerialer<PsManagementRoles>(PsManagementRoles.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PsMenu), dataType))
            {
                return new PsMenuHandle(GetConfigDataSerialer<PsMenu>(PsMenu.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PsMenuManagement), dataType))
            {
                return new PsMenuManagementHandle(GetConfigDataSerialer<PsMenuManagement>(PsMenuManagement.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PsOrganization), dataType))
            {
                return new PsOrganizationHandle(GetConfigDataSerialer<PsOrganization>(PsOrganization.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PsOrgRoles), dataType))
            {
                return new PsOrgRolesHandle(GetConfigDataSerialer<PsOrgRoles>(PsOrgRoles.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PsPlatform), dataType))
            {
                return new PsPlatformHandle(GetConfigDataSerialer<PsPlatform>(PsPlatform.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PsRoles), dataType))
            {
                return new PsRolesHandle(GetConfigDataSerialer<PsRoles>(PsRoles.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PsUserOrg), dataType))
            {
                return new PsUserOrgHandle(GetConfigDataSerialer<PsUserOrg>(PsUserOrg.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PsUserRoles), dataType))
            {
                return new PsUserRolesHandle(GetConfigDataSerialer<PsUserRoles>(PsUserRoles.TableName)) as IDataTableHandle;
            }
            //UserCenter
            else if (IsSubclassOf(typeof(UcUsers), dataType))
            {
                return new UcUsersHandle(GetConfigDataSerialer<UcUsers>(UcUsers.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(UcUsersSetting), dataType))
            {
                return new UcUsersSettingHandle(GetConfigDataSerialer<UcUsersSetting>(UcUsersSetting.TableName)) as IDataTableHandle;
            }
            return null;
        }


        /// <summary>
        /// 数据访问层具体实现
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public override IRepository<TData> GetConfigDataSerialer<TData>(string tableName)
        {
            switch (tableName)
            {
                //权限配置
                case PsManagement.TableName:
                    return new DBRepository<PsManagement, PsManagement>() as IRepository<TData>;
                case PsManagementRoles.TableName:
                    return new DBRepository<PsManagementRoles, PsManagementRoles>() as IRepository<TData>;
                case PsMenu.TableName:
                    return new DBRepository<PsMenu, PsMenu>() as IRepository<TData>;
                case PsMenuManagement.TableName:
                    return new DBRepository<PsMenuManagement, PsMenuManagement>() as IRepository<TData>;
                case PsOrganization.TableName:
                    return new DBRepository<PsOrganization, PsOrganization>() as IRepository<TData>;
                case PsOrgRoles.TableName:
                    return new DBRepository<PsOrgRoles, PsOrgRoles>() as IRepository<TData>;
                case PsPlatform.TableName:
                    return new DBRepository<PsPlatform, PsPlatform>() as IRepository<TData>;
                case PsRoles.TableName:
                    return new DBRepository<PsRoles, PsRoles>() as IRepository<TData>;
                case PsUserOrg.TableName:
                    return new DBRepository<PsUserOrg, PsUserOrg>() as IRepository<TData>;
                case PsUserRoles.TableName:
                    return new DBRepository<PsUserRoles, PsUserRoles>() as IRepository<TData>;

                //UserCenter
                case UcUsers.TableName:
                    return new DBRepository<UcUsers, UcUsers>() as IRepository<TData>;
                case UcUsersSetting.TableName:
                    return new DBRepository<UcUsersSetting, UcUsersSetting>() as IRepository<TData>;
            }
            return null;
        }
    }
}
