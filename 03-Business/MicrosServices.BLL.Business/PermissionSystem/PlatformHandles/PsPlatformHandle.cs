using System;
using System.Data;
using System.Linq;
using System.Collections;
using MicrosServices.Entities.Common;
using SkeFramework.DataBase.Interfaces;
using MicrosServices.DAL.DataAccess.DataHandle.Repositorys;
using MicrosServices.Entities.Constants;
using System.Linq.Expressions;
using MicrosServices.Helper.Core.Constants;
using MicrosServices.Helper.DataUtil.Tree;
using SkeFramework.Core.Common.Collections;
using System.Collections.Generic;
using MicrosServices.BLL.Business;
using SkeFramework.Core.Network.Responses;
using MicrosServices.SDK.UserCenter;

namespace MicrosServices.BLL.SHBusiness.PsPlatformHandles
{
   public class PsPlatformHandle : PsPlatformHandleCommon, IPsPlatformHandle
  {
        private UserSDK userSDK = new UserSDK();

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

        /// <summary>
        /// 平台更新
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <returns></returns>
        public int PlatformUpdate(PsPlatform platform)
        {
            PsPlatform current = this.GetModelByKey(platform.id.ToString());
            if (current == null)
                return 0;
            PsPlatform ParentInfo = this.GetPlatformInfo(platform.ParentNo);
            if (ParentInfo.ParentNo != ConstantsData.DEFAULT_ID && ParentInfo == null)
            {
                throw new ArgumentException("父节点不存在");
            }
            string treeLevelNo = TreeLevelUtil.GetTreeLevelNo(ParentInfo, platform.ParentNo);
            bool isChange = current.TreeLevelNo != treeLevelNo;
            current.TreeLevelNo = treeLevelNo;
            current.UpdateTime = DateTime.Now;
            current.Name = platform.Name;
            current.Value = platform.Value;
            current.ParentNo = platform.ParentNo;
            current.DefaultUserName = platform.DefaultUserName;
            int result = this.Update(current);
            if (result > 0 && isChange)
            {
                this.RefreshChildTreeLevelNo(current);
            }
            return result;
        }

        /// <summary>
        /// 刷新子节点编号路径
        /// </summary>
        /// <param name="current"></param>
        public void RefreshChildTreeLevelNo(PsPlatform current)
        {
            long PlatformNo = current.PlatformNo;
            List<PsPlatform> childMenuList = this.GetChildPlatformList(PlatformNo);
            if (CollectionUtils.IsEmpty(childMenuList))
                return;
            foreach (PsPlatform item in childMenuList)
            {
                string treeLevelNo = "-1";
                if (item.ParentNo.Equals(PlatformNo))
                {
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo(current, item.ParentNo);
                }
                else
                {
                    PsPlatform parent = childMenuList.Find(o => o.PlatformNo == item.ParentNo);
                    treeLevelNo = TreeLevelUtil.GetTreeLevelNo(parent, item.ParentNo);
                }
                item.TreeLevelNo = treeLevelNo;
                this.UpdateTreeLevelNo(PlatformNo, treeLevelNo);
            }
        }

        /// <summary>
        /// 删除平台
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePlatform(int id)
        {
            PsPlatform platform = this.GetModelByKey(id.ToString());
            if (platform == null)
                throw new ArgumentException("平台不存在");
            JsonResponses jsonResponses = userSDK.CancelPlatform(platform.DefaultUserNo);
            if (!jsonResponses.ValidateResponses())
                 throw new ArgumentException(jsonResponses.msg);
            long PlatformNo = platform.PlatformNo;
            long count = DataHandleManager.Instance().PsManagementHandle.CountByPlatformNo(PlatformNo);
            if (count > 0)
                throw new ArgumentException("当前平台信息不为空，暂不支持删除");
            //检查编号是否有子节点
            this.CheckNoHasChild(PlatformNo);
            return this.Delete(id);
        }
        /// <summary>
        /// 检查编号是否有子节点
        /// </summary>
        /// <param name="PlatformNo"></param>
        /// <returns></returns>
        public bool CheckNoHasChild(long PlatformNo)
        {
            Expression<Func<PsPlatform, bool>> where = (o => o.ParentNo == PlatformNo);
            long count = this.Count(where);
            if (count > 0)
            {
                throw new ArgumentException(String.Format("下级节点不为空，暂不支持删除！", PlatformNo));
            }
            return true;
        }

    }
}
