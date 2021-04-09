using MicrosServices.BLL.Business.PublishDeploy.PdProjectHandles;
using MicrosServices.BLL.Business.PublishDeploy.PdPublishHandles;
using MicrosServices.BLL.Business.PublishDeploy.PdServerHandles;
using MicrosServices.BLL.Business.LogSystem.UcLoginLogHandles;
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
using MicrosServices.Entities.Common.PublishDeploy;
using SkeFramework.DataBase.DataAccess.DataHandle.Achieve;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.Entities.Common.BaseSystem;
using MicrosServices.BLL.Business.BaseSystem.BsDictionaryHandle;
using MicrosServices.Entities.Common.RealTimeSystem;
using MicrosServices.BLL.Business.RealTimeSystem.RtPushConfigHandles;
using MicrosServices.BLL.Business.RealTimeSystem.RtMessageHandles;
using MicrosServices.BLL.Business.RealTimeSystem.RtEmailHandles;
using MicrosServices.BLL.Business.RealTimeSystem.RtShortMessageHandles;

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
            #region PermissionSystem
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
            #endregion
            #region UserCenter
            else if (IsSubclassOf(typeof(UcUsers), dataType))
            {
                return new UcUsersHandle(GetConfigDataSerialer<UcUsers>(UcUsers.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(UcLoginLog), dataType))
            {
                return new UcLoginLogHandle(GetConfigDataSerialer<UcLoginLog>(UcLoginLog.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(UcUsersSetting), dataType))
            {
                return new UcUsersSettingHandle(GetConfigDataSerialer<UcUsersSetting>(UcUsersSetting.TableName)) as IDataTableHandle;
            }
            #endregion
            #region PublishDeploy
            else if (IsSubclassOf(typeof(PdServer), dataType))
            {
                return new PdServerHandle(GetConfigDataSerialer<PdServer>(PdServer.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PdPublish), dataType))
            {
                return new PdPublishHandle(GetConfigDataSerialer<PdPublish>(PdPublish.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(PdProject), dataType))
            {
                return new PdProjectHandle(GetConfigDataSerialer<PdProject>(PdProject.TableName)) as IDataTableHandle;
            }
            #endregion
            #region BaseSystem
            else if (IsSubclassOf(typeof(BsDictionary), dataType))
            {
                return new BsDictionaryHandle(GetConfigDataSerialer<BsDictionary>(BsDictionary.TableName)) as IDataTableHandle;
            }
            #endregion
            #region RealTimeSystem
            else if (IsSubclassOf(typeof(RtPushconfig), dataType))
            {
                return new RtPushconfigHandle(GetConfigDataSerialer<RtPushconfig>(RtPushconfig.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(RtMessage), dataType))
            {
                return new RtMessageHandle(GetConfigDataSerialer<RtMessage>(RtMessage.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(RtEmail), dataType))
            {
                return new RtEmailHandle(GetConfigDataSerialer<RtEmail>(RtEmail.TableName)) as IDataTableHandle;
            }
            else if (IsSubclassOf(typeof(RtShortMessage), dataType))
            {
                return new RtShortMessageHandle(GetConfigDataSerialer<RtShortMessage>(RtShortMessage.TableName)) as IDataTableHandle;
            }
            #endregion
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
                #region 权限配置
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
                #endregion
                #region UserCenter
                case UcUsers.TableName:
                    return new DBRepository<UcUsers, UcUsers>() as IRepository<TData>;
                case UcUsersSetting.TableName:
                    return new DBRepository<UcUsersSetting, UcUsersSetting>() as IRepository<TData>;
                case UcLoginLog.TableName:
                    return new DBRepository<UcLoginLog, UcLoginLog>() as IRepository<TData>;
                #endregion
                #region PublishDeploy
                case PdServer.TableName:
                    return new DBRepository<PdServer, PdServer>() as IRepository<TData>;
                case PdPublish.TableName:
                    return new DBRepository<PdPublish, PdPublish>() as IRepository<TData>;
                case PdProject.TableName:
                    return new DBRepository<PdProject, PdProject>() as IRepository<TData>;
                #endregion
                #region BaseSystem
                case BsDictionary.TableName:
                    return new DBRepository<BsDictionary, BsDictionary>() as IRepository<TData>;
                #endregion
                #region RealTimeSystem
                case RtPushconfig.TableName:
                    return new DBRepository<RtPushconfig, RtPushconfig>() as IRepository<TData>;
                case RtMessage.TableName:
                    return new DBRepository<RtMessage, RtMessage>() as IRepository<TData>;
                case RtEmail.TableName:
                    return new DBRepository<RtEmail, RtEmail>() as IRepository<TData>;
                case RtShortMessage.TableName:
                    return new DBRepository<RtShortMessage, RtShortMessage>() as IRepository<TData>;
                    #endregion
            }
            return null;
        }
    }
}
