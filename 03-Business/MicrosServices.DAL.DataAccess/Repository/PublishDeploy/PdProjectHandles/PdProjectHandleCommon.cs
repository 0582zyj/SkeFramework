using MicrosServices.Entities.Common.PublishDeploy;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdProjectHandles
{
    public class PdProjectHandleCommon : DataTableHandle<PdProject>, IPdProjectHandleCommon
    {
        public PdProjectHandleCommon(IRepository<PdProject> dataSerialer)
            : base(dataSerialer, PdProject.TableName, false)
        {
        }
    }
}
