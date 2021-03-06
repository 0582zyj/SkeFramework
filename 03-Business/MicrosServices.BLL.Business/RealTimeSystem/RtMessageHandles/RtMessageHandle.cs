﻿using MicrosServices.DAL.DataAccess.RealTimeSystem.RtMessageHandles;
using MicrosServices.Entities.Common.RealTimeSystem;
using MicrosServices.Entities.Constants;
using SkeFramework.Core.Common.Collections;
using SkeFramework.Core.SqlExpression;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.RealTimeSystem.RtMessageHandles
{
    public class RtMessageHandle : RtMessageHandleCommon, IRtMessageHandle
    {
        public RtMessageHandle(IRepository<RtMessage> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public int RtMessageInsert(RtMessage model)
        {
            model.InputTime = DateTime.Now;
            model.HandleTime = model.InputTime;
            model.AvailTime = model.InputTime;
            return this.Insert(model);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int RtMessageUpdate(RtMessage model)
        {
            RtMessage UpdateModel = this.GetModelByKey(model.id.ToString());
            if (UpdateModel != null)
            {
                UpdateModel.HandleTime = DateTime.Now;
                UpdateModel.HandleResult = model.HandleResult;
                UpdateModel.AvailTime = model.AvailTime;
                return this.Update(UpdateModel);
            }
            return 0;
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="TimeOutSecond"></param>
        /// <returns></returns>
        public List<RtMessage> GetRtMessageList(int Status, int TimeOutSecond, List<string> AppIdList=null)
        {
            Expression<Func<RtMessage, bool>> where = null;
            where = (o => o.Status.Equals(Status));
            if (TimeOutSecond!=0)
            {
                where = where.And(o => o.InputTime.AddSeconds(TimeOutSecond) > DateTime.Now);
            }
            if (!CollectionUtils.IsEmpty(AppIdList))
            {
                where = where.And(o => AppIdList.Contains(o.AppId));
            }
            return this.GetList(where).ToList();
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="TimeOutSecond"></param>
        /// <returns></returns>
        public List<RtMessage> GetRtMessageList(int Status, List<string> ReceiveUserIdList)
        {
            Expression<Func<RtMessage, bool>> where = null;
            where = (o => o.Status.Equals(Status));
            if (!CollectionUtils.IsEmpty(ReceiveUserIdList))
            {
                where = where.And(o => ReceiveUserIdList.Contains(o.UserId));
            }
            return this.GetList(where).ToList();
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="TimeOutSecond"></param>
        /// <returns></returns>
        public List<RtMessage> GetRtMessageList(string ReceiveUserId)
        {
            return this.GetRtMessageList((int)MessageStatusEumns.Ready, new List<string>() { ReceiveUserId });
        }

        /// <summary>
        /// 统计某个应用要推送的消息个数
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="TimeOutSecond"></param>
        /// <param name="AppIdList"></param>
        /// <returns></returns>
        public long CountRtMessage(int Status, int TimeOutSecond, List<string> AppIdList = null)
        {
            Expression<Func<RtMessage, bool>> where = null;
            where = (o => o.Status.Equals(Status));
            if (TimeOutSecond != 0)
            {
                where = where.And(o => o.InputTime.AddSeconds(TimeOutSecond) > DateTime.Now);
            }
            if (!CollectionUtils.IsEmpty(AppIdList))
            {
                where = where.And(o => AppIdList.Contains(o.AppId));
            }
            return this.Count(where);
        }
    }
}
