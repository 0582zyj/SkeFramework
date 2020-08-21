using MicrosServices.Entities.Common.BaseSystem;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.BaseSystem.BsDictionaryHandle
{
    public class BsDictionaryHandleCommon : DataTableHandle<BsDictionary>, IBsDictionaryHandleCommon
    {
        public BsDictionaryHandleCommon(IRepository<BsDictionary> dataSerialer)
            : base(dataSerialer, BsDictionary.TableName, false)
        {
        }
    }
}

