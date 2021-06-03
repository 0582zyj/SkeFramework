using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Common;

namespace MicrosServices.BLL.SHBusiness.PsPlatformHandles
{
    public interface IPsPlatformHandle : IPsPlatformHandleCommon
    {

        /// <summary>
        /// 获取平台信息
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <returns></returns>
        PsPlatform GetPlatformInfo(long PlatformNo);
          
    }
}
