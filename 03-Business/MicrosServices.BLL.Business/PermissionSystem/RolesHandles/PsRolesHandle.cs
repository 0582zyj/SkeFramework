using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using System.Linq.Expressions;
using SkeFramework.Core.SnowFlake;

namespace MicrosServices.BLL.SHBusiness.PsRolesHandles
{
    public class PsRolesHandle : PsRolesHandleCommon, IPsRolesHandle
  {
        public PsRolesHandle(IRepository<PsRoles> dataSerialer)
            : base(dataSerialer)
        {
        }

      

        public int RolesInsert(PsRoles model)
        {
            model.RolesNo = AutoIDWorker.Example.GetAutoID();
            model.InputTime = DateTime.Now;
            model.Enabled = 1;
            return this.Insert(model);
        }

        public int RolesUpdate(PsRoles management)
        {
            PsRoles model = this.GetModelByKey(management.id.ToString());
            if (model != null)
            {
                model.UpdateTime = DateTime.Now;
                model.Name = management.Name;
                model.ParentNo = management.ParentNo;
                model.Description = management.Description;
                model.ManagementValue = management.ManagementValue ;
                model.Enabled = management.Enabled;
                return this.Update(model);
            }
            return -1;
        }

        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="Nos"></param>
        /// <returns></returns>
       public bool CheckRolesNoIsExist(long Nos)
        {
            Expression<Func<PsRoles, bool>> where = (o => o.RolesNo==Nos);
            long count = this.Count(where);
            if (count == 0)
            {
                throw new Exception(String.Format("权限[{0}]不存在", Nos));
            }
            return false;
        }
    }
}
