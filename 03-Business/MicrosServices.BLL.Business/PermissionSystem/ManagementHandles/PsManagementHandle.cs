using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using System.Linq.Expressions;
using MicrosServices.BLL.Business;
using SkeFramework.Core.SnowFlake;
using System.Collections.Generic;
using MicrosServices.Entities.Constants;
using MicrosServices.Helper.DataUtil.Tree;
using MicrosServices.Helper.Core.Common;

namespace MicrosServices.BLL.SHBusiness.PsManagementHandles
{
    public class PsManagementHandle : PsManagementHandleCommon, IPsManagementHandle
    {
        public PsManagementHandle(IRepository<PsManagement> dataSerialer)
            : base(dataSerialer)
        {
        }

        /// <summary>
        /// 新增一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        public int ManagementInsert(PsManagement management)
        {
            management.ManagementNo = AutoIDWorker.Example.GetAutoSequence();
            PsManagement ParentInfo = this.GetManagementInfo(management.ParentNo);
            management.TreeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsManagement>(ParentInfo, management.ParentNo);
            management.InputTime = DateTime.Now;
            management.Enabled = 1;
            return this.Insert(management);
        }

        /// <summary>
        /// 新增一个权限
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        public int ManagementUpdate(PsManagement management)
        {
            PsManagement model = this.GetModelByKey(management.id.ToString());
            if (model != null)
            {
                PsManagement ParentInfo = this.GetManagementInfo(management.ParentNo);
                model.TreeLevelNo = TreeLevelUtil.GetTreeLevelNo<PsManagement>(ParentInfo, management.ParentNo );
                model.UpdateTime = DateTime.Now;
                model.Name = management.Name;
                model.ParentNo = management.ParentNo;
                model.Description = management.Description;
                model.Type = management.Type;
                model.Value = management.Value;
                model.PlatformNo = management.PlatformNo;
                model.Sort = management.Sort;
                model.Enabled = management.Enabled;
                return this.Update(model);
            }
            return 0;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="management"></param>
        /// <returns></returns>
        public int ManagementBeachDelete(List<long> ManagementNos)
        {
            return 0;
        }


        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool CheckManagementNameIsExist(string Name,long ManagementNo=ConstData.DefaultNo)
        {
            Expression<Func<PsManagement, bool>> where = (o => o.Name == Name&&(o.ManagementNo==ConstData.DefaultNo || o.ManagementNo!=ManagementNo));
            long count = this.Count(where);
            if (count > 0)
            {
                throw new Exception(String.Format("名称[{0}]已存在", Name));
            }
            return false;
        }
        /// <summary>
        /// 检查编码是否存在
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public bool CheckManagementNoIsExist(long ManagementNo)
        {
            if (ManagementNo !=ConstData.DefaultNo)
            {
                Expression<Func<PsManagement, bool>> where = (o => o.ManagementNo == ManagementNo);
                long count = this.Count(where);
                if (count == 0)
                {
                    throw new Exception(String.Format("权限编号[{0}]不存在", ManagementNo));
                }
            }
            return false;
        }

        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="ManagementNo"></param>
        /// <returns></returns>
        public PsManagement GetManagementInfo(long ManagementNo)
        {
            if (ManagementNo == ConstData.DefaultNo)
            {
                return null;
            }
            Expression<Func<PsManagement, bool>> where = (o => o.ManagementNo == ManagementNo);
            return this.Get(where);
        }

        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <returns></returns>
        public List<OptionValue> GetRolesOptionValues(long PlatformNo,long ManagementType= ConstData.DefaultNo)
        {
            return this.GetOptionValues(PlatformNo, ManagementType);
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <param name="platformNo"></param>
        /// <returns></returns>
        public List<TreeNodeInfo> GetPlatformManagementTree(long PlatformNo)
        {
            List<TreeNodeInfo> treeNodes = new List<TreeNodeInfo>();
            Expression<Func<PsManagement, bool>> where = (o => o.PlatformNo == PlatformNo);
            List<PsManagement> list = this.GetList(where).ToList();
            //if(Coll)
            foreach (var item in list)
            {
                treeNodes.Add(new TreeNodeInfo()
                {
                    TreeNo = item.ManagementNo.ToString(),
                    ParentNo = item.ParentNo.ToString(),
                    Name = item.Name
                });
            }
            return treeNodes;
        }
    }
}
