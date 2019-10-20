using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;

namespace MicrosServices.BLL.SHBusiness.PsOrganizationHandles
{
    public interface IPsOrganizationHandle : IPsOrganizationHandleCommon
    {
        int OrganizationInsert(PsOrganization model);
        int OrganizationUpdate(PsOrganization model);
       
    }
}
