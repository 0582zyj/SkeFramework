using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.RealTimeSystem.RtShortMessageHandles
{
    /// <summary>
    /// 短信记录数据库访问接口
    /// </summary>
    public interface IRtShortMessageHandleCommon : IDataTableHandle<RtShortMessage>
    {
    }
}

