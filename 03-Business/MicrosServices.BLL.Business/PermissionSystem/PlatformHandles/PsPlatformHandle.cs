using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Constants;
using System.Linq.Expressions;

namespace MicrosServices.BLL.SHBusiness.PsPlatformHandles
{
    public class PsPlatformHandle : PsPlatformHandleCommon, IPsPlatformHandle
  {
        public PsPlatformHandle(IRepository<PsPlatform> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 获取平台信息
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <returns></returns>
        public PsPlatform GetPlatformInfo(long PlatformNo)
        {
            if (PlatformNo == ConstData.DefaultNo)
            {
                return null;
            }
            Expression<Func<PsPlatform, bool>> where = (o => o.PlatformNo == PlatformNo);
            return this.Get(where);
        }
    }
}
