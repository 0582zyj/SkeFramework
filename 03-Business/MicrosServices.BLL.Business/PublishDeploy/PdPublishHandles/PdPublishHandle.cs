using MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdPublishHandles;
using MicrosServices.Entities.Common.PublishDeploy;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.PublishDeploy.PdPublishHandles
{
    public class PdPublishHandle : PdPublishHandleCommon, IPdPublishHandle
    {
        public PdPublishHandle(IRepository<PdPublish> dataSerialer)
            : base(dataSerialer)
        {
        }
    }
}
