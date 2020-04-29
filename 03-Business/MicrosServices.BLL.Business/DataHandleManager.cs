using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosServices.BLL.Business.PublishDeploy.PdProjectHandles;
using MicrosServices.BLL.Business.PublishDeploy.PdPublishHandles;
using MicrosServices.BLL.Business.PublishDeploy.PdServerHandles;
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
using ULCloudLockTool.BLL.Business.Achieve;

namespace MicrosServices.BLL.Business
{
    public class DataHandleManager
    {
        private static DataHandleManager _manager;

        public static DataHandleManager Instance()
        {
            if (_manager == null)
            {
                DataHandleFactory.SetDataHandleFactory(new DALDataHandleFactory());
                _manager = new DataHandleManager();
            }
            return _manager ?? (_manager = new DataHandleManager());
        }


        #region PermissionSystem

        public IPsManagementHandle PsManagementHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsManagementHandle, PsManagement>(); }
        }

        public IPsManagementRolesHandle PsManagementRolesHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsManagementRolesHandle, PsManagementRoles>(); }
        }

        public IPsMenuHandle PsMenuHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsMenuHandle, PsMenu>(); }
        }

        public IPsMenuManagementHandle PsMenuManagementHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsMenuManagementHandle, PsMenuManagement>(); }
        }

        public IPsOrganizationHandle PsOrganizationHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsOrganizationHandle, PsOrganization>(); }
        }

        public IPsOrgRolesHandle PsOrgRolesHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsOrgRolesHandle, PsOrgRoles>(); }
        }

        public IPsPlatformHandle PsPlatformHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsPlatformHandle, PsPlatform>(); }
        }

        public IPsRolesHandle PsRolesHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsRolesHandle, PsRoles>(); }
        }

        public IPsUserOrgHandle PsUserOrgHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsUserOrgHandle, PsUserOrg>(); }
        }

        public IPsUserRolesHandle PsUserRolesHandle
        {
            get { return DataHandleFactory.GetDataHandle<PsUserRolesHandle, PsUserRoles>(); }
        }
        #endregion

        #region UserCenter

        public IUcUsersHandle UcUsersHandle
        {
            get { return DataHandleFactory.GetDataHandle<UcUsersHandle, UcUsers>(); }
        }
        public IUcUsersSettingHandle UcUsersSettingHandle
        {
            get { return DataHandleFactory.GetDataHandle<UcUsersSettingHandle, UcUsersSetting>(); }
        }

        #endregion

        #region PublishDeploy
        public IPdServerHandle PdServerHandle
        {
            get { return DataHandleFactory.GetDataHandle<PdServerHandle, PdServer>(); }
        }
        public IPdProjectHandle PdProjectHandle
        {
            get { return DataHandleFactory.GetDataHandle<IPdProjectHandle, PdProject>(); }
        }
        public IPdPublishHandle PdPublishHandle
        {
            get { return DataHandleFactory.GetDataHandle<IPdPublishHandle, PdPublish>(); }
        }

        

        #endregion
    }
}
