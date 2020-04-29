using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Helper.Core.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdProjectHandles
{
    public interface IPdProjectHandleCommon : IDataTableHandle<PdProject>
    {

        List<OptionValue> GetOptionValues();
    }
}
