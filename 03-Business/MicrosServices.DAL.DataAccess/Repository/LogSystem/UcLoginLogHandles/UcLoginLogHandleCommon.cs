using MicrosServices.Entities.Common;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.LogSystem.UcLoginLogHandles
{
    public class UcLoginLogHandleCommon : DataTableHandle<UcLoginLog>, IUcLoginLogHandleCommon
    {
        public UcLoginLogHandleCommon(IRepository<UcLoginLog> dataSerialer)
            : base(dataSerialer, UcLoginLog.TableName, false)
        {
        }
    }
}
