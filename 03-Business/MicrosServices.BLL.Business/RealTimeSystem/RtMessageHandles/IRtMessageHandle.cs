using MicrosServices.DAL.DataAccess.RealTimeSystem.RtMessageHandles;
using MicrosServices.Entities.Common.RealTimeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.RealTimeSystem.RtMessageHandles
{
    public interface IRtMessageHandle : IRtMessageHandleCommon
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        
        int RtMessageInsert(RtMessage model);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int RtMessageUpdate(RtMessage model);

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="TimeOutSecond"></param>
        /// <returns></returns>
        List<RtMessage> GetRtMessageList(int Status, int TimeOutSecond,List<string> AppIdList = null);
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="TimeOutSecond"></param>
        /// <returns></returns>
        List<RtMessage> GetRtMessageList(int Status, List<string> ReceiveUserIdList);
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="TimeOutSecond"></param>
        /// <returns></returns>
        List<RtMessage> GetRtMessageList(string ReceiveUserId);
        /// <summary>
        /// 统计某个应用要推送的消息个数
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="TimeOutSecond"></param>
        /// <param name="AppIdList"></param>
        /// <returns></returns>
        long CountRtMessage(int Status, int TimeOutSecond, List<string> AppIdList = null);
    }
}
