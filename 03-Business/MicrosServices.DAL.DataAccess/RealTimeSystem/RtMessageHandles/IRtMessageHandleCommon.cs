using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.RealTimeSystem.RtMessageHandles
{
    /// <summary>
    /// 推送记录数据库访问接口
    /// </summary>
    public interface IRtMessageHandleCommon : IDataTableHandle<RtMessage>
    {
        /// <summary>
        /// 更新处理信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Status"></param>
        /// <param name="HandleTime"></param>
        /// <param name="HandleResult"></param>
        /// <returns></returns>
        int UpdateHandleResult(int id, int Status, DateTime HandleTime, string HandleResult);

        /// <summary>
        /// 更新处理结果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Status"></param>
        /// <param name="AvailTime"></param>
        /// <param name="HandleResult"></param>
        /// <returns></returns>
        int UpdateAvailResult(int id, int Status, DateTime AvailTime, string HandleResult);
    }
}
