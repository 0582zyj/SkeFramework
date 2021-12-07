using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Listenser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkeFramework.Push.Core.Listenser.ChannelListensers
{
    /// <summary>
    /// 默认通道监听容器
    /// </summary>
    public class DefaultChannelPromise:IChannelPromise
    {
        private readonly List<IChannelListener> listenerList = new List<IChannelListener>();
        internal IList<IChannelListener> BusinessList
        {
            get { return listenerList.ToList().AsReadOnly(); }
        }
        /// <summary>
        /// 添加一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public virtual void AddDataPointListener(IChannelListener listener)
        {
            try
            {
                if (listener != null && !BusinessList.Contains(listener))
                {
                    lock (listenerList)
                    {
                        listenerList.Add(listener);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("AddDataPointListener：{0}", ex.ToString());
            }
        }
        /// <summary>
        /// 移除一个监听者
        /// </summary>
        /// <param name="listener"></param>
        public virtual void RemoveDataPointListener(IChannelListener listener)
        {
            try
            {
                lock (listenerList)
                {
                    listenerList.Remove(listener);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("RemoveDataPointListener：{0}", ex.ToString());
            }
        }
        /// <summary>
        /// 监听方法
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="taskId"></param>
        public virtual void OnReceivedDataPoint(INotification datas, string taskId)
        {
            this.notifyListeners0(datas, taskId);
        }
        /// <summary>
        /// 通知全部监听者
        /// </summary>
        /// <param name="listener"></param>
        protected void notifyListeners0(INotification datas, string taskId)
        {
            try
            {
                IChannelListener[] a = this.BusinessList.ToArray();
                int size = a.Length;
                for (int i = 0; i < size; ++i)
                {
                    notifyListener0( a[i],  datas,  taskId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 通知某个监听者
        /// </summary>
        /// <param name="l"></param>
        /// <param name="datas"></param>
        /// <param name="taskId"></param>
        protected void notifyListener0(IChannelListener channelListener, INotification datas, string taskId)
        {
            try
            {
                channelListener.OnReceivedDataPoint( datas,  taskId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
