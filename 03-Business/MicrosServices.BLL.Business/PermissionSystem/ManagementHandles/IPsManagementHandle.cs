using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;

namespace MicrosServices.BLL.SHBusiness.PsManagementHandles
{
    public interface IPsManagementHandle : IPsManagementHandleCommon
  {
         int ManagementInser(PsManagement management);
  }
}
