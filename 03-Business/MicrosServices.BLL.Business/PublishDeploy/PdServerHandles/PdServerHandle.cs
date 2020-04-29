using MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdServerHandles;
using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Helper.Core.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.PublishDeploy.PdServerHandles
{
    public class PdServerHandle : PdServerHandleCommon, IPdServerHandle
    {
        public PdServerHandle(IRepository<PdServer> dataSerialer)
            : base(dataSerialer)
        {
        }


      
    }
}
