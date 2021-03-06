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
                throw new Exception(String.Format("权限[{0}]不存在", Nos));
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
        /// <param name="management"></param>
        /// <returns></returns>
        public int RolesUpdate(PsRoles management)
        {
            PsRoles model = this.GetModelByKey(management.id.ToString());
            if (model != null)
            {
                model.UpdateTime = DateTime.Now;
                model.Name = management.Name;
                model.ParentNo = management.ParentNo;
                model.Description = management.Description;
                model.ManagementValue = management.ManagementValue;
                model.Enabled = management.Enabled;
                PsRoles ParentInfo = this.GetModelByKey(management.ParentNo.ToString());
                model.TreeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsRoles>(ParentInfo, management.ParentNo);
                return this.Update(model);
            }
            return 0;
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

    }
}
