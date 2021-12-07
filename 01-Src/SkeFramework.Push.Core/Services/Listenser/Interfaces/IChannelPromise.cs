using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeFramework.Push.Core.Listenser.Interfaces
{
    /// <summary>
    /// 通道监听处理接口
    /// </summary>
    public interface IChannelPromise: IChannelListener
    {
        /// <summary>
        /// 添加一个监听者
        /// </summary>
        /// <param name="listener"></param>
        void AddDataPointListener(IChannelListener listener);
        /// <summary>
        /// 移除一个监听者
        /// </summary>
        /// <param name="listener"></param>
        void RemoveDataPointListener(IChannelListener listener);
    }
}
