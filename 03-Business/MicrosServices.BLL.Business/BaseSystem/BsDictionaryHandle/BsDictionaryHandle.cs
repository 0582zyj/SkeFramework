using MicrosServices.DAL.DataAccess.Repository.BaseSystem.BsDictionaryHandle;
using MicrosServices.Entities.Common.BaseSystem;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.BaseSystem.BsDictionaryHandle
{
    public class BsDictionaryHandle : BsDictionaryHandleCommon, IBsDictionaryHandle
    {
        public BsDictionaryHandle(IRepository<BsDictionary> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 更新字典信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DictionaryUpdate(BsDictionary model)
        {
            BsDictionary UpdateModel = this.GetModelByKey(model.id.ToString());
            if (UpdateModel != null)
            {
                UpdateModel.UpdateTime = DateTime.Now;
                UpdateModel.DicKey = model.DicKey;
                UpdateModel.DicValue = model.DicValue;
                UpdateModel.DicType = model.DicType;
                UpdateModel.Descriptions = model.Descriptions;
                UpdateModel.PlatformNo = model.PlatformNo;
                UpdateModel.Enabled = model.Enabled;
                return this.Update(UpdateModel);
            }
            return 0;
        }
    }
}

