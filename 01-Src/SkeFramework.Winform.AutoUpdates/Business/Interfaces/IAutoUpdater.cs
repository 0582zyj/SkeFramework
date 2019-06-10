using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.AutoUpdates.DAL.Interfaces
{
    /// <summary>
    /// 更新业务接口
    /// </summary>
    public interface IAutoUpdater
    {
        /// <summary>
        /// 检查更新
        /// </summary>
        int Update();
        /// <summary>
        /// 回滚操作
        /// </summary>
        void RollBack();
    }
}
