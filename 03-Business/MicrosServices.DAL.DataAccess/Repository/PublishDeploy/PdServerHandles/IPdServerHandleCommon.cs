using MicrosServices.Entities.Common.PublishDeploy;
using MicrosServices.Helper.Core.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.PublishDeploy.PdServerHandles
{
    public interface IPdServerHandleCommon : IDataTableHandle<PdServer>
    {
        List<OptionValue> GetOptionValues();
    }
}
