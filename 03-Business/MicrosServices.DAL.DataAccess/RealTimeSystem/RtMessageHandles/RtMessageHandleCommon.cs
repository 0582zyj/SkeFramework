using MicrosServices.Entities.Common.RealTimeSystem;
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

namespace MicrosServices.DAL.DataAccess.RealTimeSystem.RtMessageHandles
{
    /// <summary>
    /// 推送记录数据库访问实现
    /// </summary>
    public class RtMessageHandleCommon : DataTableHandle<RtMessage>, IRtMessageHandleCommon
    {
        public RtMessageHandleCommon(IRepository<RtMessage> dataSerialer)
            : base(dataSerialer, RtMessage.TableName, false)
        {
        }

        /// <summary>
        /// 更新处理信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Status"></param>
        /// <param name="HandleTime"></param>
        /// <param name="HandleResult"></param>
        /// <returns></returns>
        public int UpdateHandleResult(int id, int Status, DateTime HandleTime, string HandleResult)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            string sSQL = String.Format(@"UPDATE  {0} SET Status=@Status,HandleTime=@HandleTime,HandleResult=@HandleResult WHERE id=@id", _mTableName);
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@Status", Status));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@HandleTime", HandleTime));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@HandleResult", HandleResult));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@id", id));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, ParaList.ToArray());
        }

        /// <summary>
        /// 更新处理结果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Status"></param>
        /// <param name="AvailTime"></param>
        /// <param name="HandleResult"></param>
        /// <returns></returns>
        public int UpdateAvailResult(int id, int Status, DateTime AvailTime, string HandleResult)
        {
            List<DbParameter> ParaList = new List<DbParameter>();
            string sSQL = String.Format(@"UPDATE  {0} SET Status=@Status,AvailTime=@AvailTime,HandleResult=@HandleResult WHERE id=@id", _mTableName);
            ParaList.Add( DbFactory.Instance().CreateDataParameter("@Status", Status));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@AvailTime", AvailTime));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@HandleResult", HandleResult));
            ParaList.Add(DbFactory.Instance().CreateDataParameter("@id", id));
            return RepositoryHelper.ExecuteNonQuery(CommandType.Text, sSQL, ParaList.ToArray());
        }
    }
}
