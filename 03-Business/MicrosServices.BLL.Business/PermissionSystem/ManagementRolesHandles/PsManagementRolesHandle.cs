using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Helper.Core.Form;
using MicrosServices.BLL.Business;
using System.Linq.Expressions;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core;
using System.Collections.Generic;
using MicrosServices.Helper.Core.Common;
using MicrosServices.Helper.Core.Extends;

namespace MicrosServices.BLL.SHBusiness.PsManagementRolesHandles
{
    public class PsManagementRolesHandle : PsManagementRolesHandleCommon, IPsManagementRolesHandle
    {
        public PsManagementRolesHandle(IRepository<PsManagementRoles> dataSerialer)
            : base(dataSerialer)
        {
        }
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int ManagementRolesInsert(ManagementRolesForm model)
        {
            int result = 0;
            //删除原有权限
            DataHandleManager.Instance().PsManagementRolesHandle.DeleteManagementRoles(model.RolesNo);
            PsManagementRoles managementRoles = null;
            if (model.ManagementNos != null)
            {
                foreach (var nos in model.ManagementNos)
                {
                    PsManagement management = DataHandleManager.Instance().PsManagementHandle.GetManagementInfo(nos);
                    if (management != null)
                    {
                        managementRoles = new PsManagementRoles()
                        {
                            RolesNo = model.RolesNo,
                            ManagementNo = nos,
                            ManagementValue = management.Value,
                            InputUser = model.InputUser,
                            InputTime = DateTime.Now,
                            UpdateTime = DateTime.Now
                        };
                        result += DataHandleManager.Instance().PsManagementRolesHandle.Insert(managementRoles);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 检查角色权限是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public bool CheckManagementRolesNoIsExist(long ManagementNo, long RolesNo)
        {
            if (ManagementNo != ConstData.DefaultNo)
            {
                Expression<Func<PsManagementRoles, bool>> where = (o => o.ManagementNo == ManagementNo && o.RolesNo == RolesNo);
                long count = this.Count(where);
                if (count == 0)
                {
                    throw new Exception(String.Format("角色权限[{0}-{1}]已存在", RolesNo, ManagementNo));
                }
            }
            return false;
        }

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="RolesNo"></param>
        /// <returns></returns>
        public ManagmentAssignVo GetManagementAssign(long RolesNo, long ManagementType)
        {
            ManagmentAssignVo assignVo = new ManagmentAssignVo();
            List<PsManagementRoles> psManagementRoles = this.GetManagementRoles(RolesNo);
            assignVo.RolesInfo = DataHandleManager.Instance().PsRolesHandle.GetRolesInfo(RolesNo);
            List<OptionValue> optionValues = DataHandleManager.Instance().PsManagementHandle.GetRolesOptionValues(assignVo.RolesInfo.PlatformNo, ManagementType);
            assignVo.optionValues = new List<CheckOptionValue>();
            foreach (var item in optionValues)
            {
                bool isCheck = psManagementRoles.Where(o => o.ManagementNo == item.Value).FirstOrDefault() != null;
                assignVo.optionValues.Add(new CheckOptionValue()
                {
                    isCheck = isCheck,
                    Name = item.Name,
                    Value = item.Value
                });
            }
            return assignVo;
        }

        /// <summary>
        /// 获取权限关系列表
        /// </summary>
        /// <param name="RolesNo"></param>
        /// <returns></returns>
        public List<PsManagementRoles> GetManagementRoles(long RolesNo)
        {
            Expression<Func<PsManagementRoles, bool>> where = (o =>  o.RolesNo == RolesNo);
            return this.GetList(where).ToList();
        }
    }
}
