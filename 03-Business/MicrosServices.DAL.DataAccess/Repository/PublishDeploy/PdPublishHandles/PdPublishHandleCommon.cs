using MicrosServices.Entities.Common.PublishDeploy;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdPublishHandles
{
    public class PdPublishHandleCommon : DataTableHandle<PdPublish>, IPdPublishHandleCommon
    {
        public PdPublishHandleCommon(IRepository<PdPublish> dataSerialer)
            : base(dataSerialer, PdPublish.TableName, false)
        {
        }
    }
}
