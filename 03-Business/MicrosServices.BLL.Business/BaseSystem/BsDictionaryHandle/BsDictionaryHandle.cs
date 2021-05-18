using MicrosServices.DAL.DataAccess.Repository.BaseSystem.BsDictionaryHandle;
using MicrosServices.Entities.Common.BaseSystem;
using SkeFramework.Core.SnowFlake;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        /// 新增字典信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DictionaryInsert(BsDictionary model)
        {
            model.DicNo = AutoIDWorker.Example.GetAutoSequence();
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

        /// <summary>
        /// 检查字段类型是否已有字典
        /// </summary>
        /// <param name="dicType"></param>
        /// <returns></returns>
        public bool CheckDictionaryTypeIsExist(string dicType)
        {
            Expression<Func<BsDictionary, bool>> where = (o => o.DicType == dicType);
            long count = this.Count(where);
            if (count > 0)
            {
                throw new ArgumentException("已存在字典数据,不允许删除。");
            }
            return false;
        }
    }
}

