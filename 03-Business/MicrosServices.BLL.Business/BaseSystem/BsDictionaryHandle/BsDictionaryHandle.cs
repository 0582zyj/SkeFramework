using MicrosServices.DAL.DataAccess.Repository.BaseSystem.BsDictionaryHandle;
using MicrosServices.Entities.Common.BaseSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.BaseSystem.BsDictionaryHandle
{
    public class BsDictionaryHandle : BsDictionaryHandleCommon, IBsDictionaryHandle
    {
        public BsDictionaryHandle(IRepository<BsDictionary> dataSerialer)
            : base(dataSerialer)
        {
        }
    }
}

