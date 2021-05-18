using MicrosServices.DAL.DataAccess.Repository.BaseSystem.BsDictionaryTypeHandle;
using MicrosServices.Entities.Common.BaseSystem;
using MicrosServices.Helper.Core.Constants;
using SkeFramework.Core.SnowFlake;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.BLL.Business.BaseSystem.BsDictionaryTypeHandle
{
    public class BsDictionaryTypeHandle : BsDictionaryTypeHandleCommon, IBsDictionaryTypeHandle
    {
        public BsDictionaryTypeHandle(IRepository<BsDictionaryType> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 新增字典信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DictionaryTypeInsert(BsDictionaryType model)
        {
            model.InputTime = DateTime.Now;
            model.UpdateTime = model.InputTime;
            model.UpdateUser = model.InputUser;
            return this.Insert(model);
        }
        /// <summary>
        /// 更新字典信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DictionaryTypeUpdate(BsDictionaryType model)
        {
            BsDictionaryType UpdateModel = this.GetModelByKey(model.id.ToString());
            if (UpdateModel != null)
            {
                if(model.Enabled==(int) EnabledType.INACTIVE)
                {
                    DataHandleManager.Instance().BsDictionaryHandle.UpdateDictionaryEnabled(UpdateModel.DicType, (int)EnabledType.INACTIVE);
                }
                if (!UpdateModel.DicType.Equals(model.DicType))
                {
                    DataHandleManager.Instance().BsDictionaryHandle.UpdateDictionartType(model.DicType, UpdateModel.DicType);
                }
                UpdateModel.UpdateTime = DateTime.Now;
                UpdateModel.DicType = model.DicType;
                UpdateModel.Descriptions = model.Descriptions;
                UpdateModel.PlatformNo = model.PlatformNo;
                UpdateModel.Enabled = model.Enabled;
                return this.Update(UpdateModel);
            }
            return 0;
        }

        /// <summary>
        /// 检查字典类型是否可删除
        /// </summary>
        /// <param name="dicType"></param>
        public bool CheckDictionaryTypeCanDelete(int id)
        {
            BsDictionaryType current = this.GetModelByKey(id.ToString());
            if (current == null)
                return false;
            return DataHandleManager.Instance().BsDictionaryHandle.CheckDictionaryTypeIsExist(current.DicType);
        }
    }
}

