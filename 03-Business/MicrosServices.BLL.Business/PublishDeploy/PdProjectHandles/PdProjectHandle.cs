using MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdProjectHandles;
using MicrosServices.Entities.Common.PublishDeploy;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.PublishDeploy.PdProjectHandles
{
    public class PdProjectHandle : PdProjectHandleCommon, IPdProjectHandle
    {
        public PdProjectHandle(IRepository<PdProject> dataSerialer)
            : base(dataSerialer)
        {
        }


         
    }
}
