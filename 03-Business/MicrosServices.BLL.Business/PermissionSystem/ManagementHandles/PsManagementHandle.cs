using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using System.Linq.Expressions;
using MicrosServices.BLL.Business;
using SkeFramework.Core.SnowFlake;

namespace MicrosServices.BLL.SHBusiness.PsManagementHandles
{
    public class PsManagementHandle : PsManagementHandleCommon, IPsManagementHandle
  {
        public PsManagementHandle(IRepository<PsManagement> dataSerialer)
            : base(dataSerialer)
        {
        }


        public int ManagementInser(PsManagement management)
        {
            Expression<Func<PsManagement, bool>> where = (o=>o.Name==management.Name);
            long count = this.Count(where);
            if (count > 0)
            {
                return -1;
            }
            management.ManagementNo = AutoIDWorker.Example.GetAutoID();
            management.InputTime = DateTime.Now;
            management.Enabled = 1;
            return this.Insert(management);
        }
  }
}
