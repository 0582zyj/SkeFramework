﻿using MicrosServices.Entities.Common.BaseSystem;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.Core.Extends;
using Newtonsoft.Json;
using SkeFramework.DataBase.Common.DataCommon;
using SkeFramework.DataBase.Common.DataFactory;
using SkeFramework.DataBase.DataAccess.DataHandle.Common;
using SkeFramework.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.DAL.DataAccess.Repository.BaseSystem.BsDictionaryHandle
{
    public class BsDictionaryHandleCommon : DataTableHandle<BsDictionary>, IBsDictionaryHandleCommon
    {
        public BsDictionaryHandleCommon(IRepository<BsDictionary> dataSerialer)
            : base(dataSerialer, BsDictionary.TableName, false)
        {
        }

        /// <summary>
        /// 根据字典类型获取键值对
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public List<DictionaryOptionValue> GetOptionValues( string DicType, long PlatformNo=ConstData.DefaultNo)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            string sSQL = String.Format("SELECT DicNo as Value,DicValue as Name,DicKey,DicType  FROM {0} ", _mTableName);
            sSQL += " WHERE DicType=@DicType ";
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@DicType", DicType));
            if (PlatformNo != ConstData.DefaultNo)
            {
                sSQL += " AND PlatformNo=@PlatformNo ";
                ParaList.Add(DbFactory.Instance().CreateDataParameter("@PlatformNo", PlatformNo));
            }
           
          
            DataTable dataTable = RepositoryHelper.GetDataTable(CommandType.Text, sSQL, ParaList.ToArray());
            if (dataTable != null || dataTable.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<DictionaryOptionValue>>(JsonConvert.SerializeObject(dataTable));
            }
            return new List<DictionaryOptionValue>();
        }
    }
}

