using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.Core.SnowFlake;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.RealTimeSystem.RtPushConfigHandles
{
    public class RtPushconfigHandle : RtPushconfigHandleCommon, IRtPushconfigHandle
    {
        public RtPushconfigHandle(IRepository<RtPushconfig> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 新增推送服务端配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int PushconfigInsert(RtPushconfig model)
        {
            model.PushNo = AutoIDWorker.Example.GetAutoSequence();
            model.InputTime = DateTime.Now;
            model.UpdateTime = model.InputTime;
            model.UpdateUser = model.InputUser;
            return this.Insert(model);
        }
        /// <summary>
        /// 更新推送服务端配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public int PushconfigUpdate(RtPushconfig model)
        {
            RtPushconfig UpdateModel = this.GetModelByKey(model.id.ToString());
            if (UpdateModel != null)
            {
                UpdateModel.UpdateTime = DateTime.Now;
                UpdateModel.PushPort = model.PushPort;
                UpdateModel.Status = model.Status;
                UpdateModel.UpdateUser = model.UpdateUser;
                UpdateModel.Descriptions = model.Descriptions;
                UpdateModel.Enabled = model.Enabled;
                return this.Update(UpdateModel);
            }
            return 0;
        }
    }
}
