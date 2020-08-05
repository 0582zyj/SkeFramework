using MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdProjectHandles;
using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Helper.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.PublishDeploy.PdProjectHandles
{
    public interface IPdProjectHandle : IPdProjectHandleCommon
    {
        PdProject GetProject(int ProjectId);
    }
}
