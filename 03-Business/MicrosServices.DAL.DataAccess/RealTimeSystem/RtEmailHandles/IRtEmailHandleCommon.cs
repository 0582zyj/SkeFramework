﻿using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.RealTimeSystem.RtEmailHandles
{
    /// <summary>
    /// 邮件记录数据库访问接口
    /// </summary>
    public interface IRtEmailHandleCommon : IDataTableHandle<RtEmail>
    {
    }
}
