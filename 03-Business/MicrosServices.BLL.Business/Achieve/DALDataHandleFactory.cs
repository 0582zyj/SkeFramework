using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.DataAccess.DataHandle.Achieve;
using SkeFramework.DataBase.Interfaces;
using ULCloudLockTool.BLL.SHBusiness.PowerBatteryHandles;
using ULCloudLockTool.BLL.SHBusiness.ProjectDeviceHandles;
using ULCloudLockTool.BLL.SHBusiness.ProjectLockHandles;
using ULCloudLockTool.BLL.SHBusiness.SystemLockCoreHandles;
using ULCloudLockTool.BLL.SHBusiness.SystemLockFinalHandles;
using ULCloudLockTool.BLL.SHBusiness.SystemLockKeyHandles;
using ULCloudLockTool.BLL.SHBusiness.SystemProductTypeHandles;
using ULCloudLockTool.BLL.SHBusiness.SystemVendorHandles;
using ULCloudLockTool.Entities.Common;
using ULCSharp.DAL.DataAccess.DataHandle.Achieve;
using ULCSharp.DAL.Interfaces;

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
            else if (IsSubclassOf(typeof(PsMenuRoles), dataType))
            {
                return new PsMenuRolesHandle(GetConfigDataSerialer<PsMenuRoles>(PsMenuRoles.TableName)) as IDataTableHandle;
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
                //工程配置
                case PowerBattery.TableName:
                    return new DBRepository<PowerBattery, PowerBattery>() as IRepository<TData>;

            }
            return null;
        }
    }
}
