using MicrosServices.DAL.DataAccess.RealTimeSystem.RtMessageHandles;
using MicrosServices.Entities.Common.RealTimeSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
