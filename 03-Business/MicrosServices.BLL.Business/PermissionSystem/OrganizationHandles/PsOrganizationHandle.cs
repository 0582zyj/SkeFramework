using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using SkeFramework.Core.SnowFlake;

namespace MicrosServices.BLL.SHBusiness.PsOrganizationHandles
{
    public class PsOrganizationHandle : PsOrganizationHandleCommon, IPsOrganizationHandle
  {
        public PsOrganizationHandle(IRepository<PsOrganization> dataSerialer)
            : base(dataSerialer)
        {
        }

        public int OrganizationInsert(PsOrganization model)
        {
            model.OrgNo = AutoIDWorker.Example.GetAutoID();
            model.InputTime = DateTime.Now;
            model.Enabled = 1;
            return this.Insert(model);
        }

        public int OrganizationUpdate(PsOrganization management)
        {
            PsOrganization model = this.GetModelByKey(management.id.ToString());
            if (model != null)
            {
                model.UpdateTime = DateTime.Now;
                model.Name = management.Name;
                model.ParentNo = management.ParentNo;
                model.Description = management.Description;
                model.Category = management.Category;
                model.Enabled = management.Enabled;
                return this.Update(model);
            }
            return -1;
        }
    }
}
