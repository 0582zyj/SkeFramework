using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;

namespace MicrosServices.BLL.SHBusiness.PsPlatformHandles
{
    public class PsPlatformHandle : PsPlatformHandleCommon, IPsPlatformHandle
  {
        public PsPlatformHandle(IRepository<PsPlatform> dataSerialer)
            : base(dataSerialer)
        {
        }
  }
}
