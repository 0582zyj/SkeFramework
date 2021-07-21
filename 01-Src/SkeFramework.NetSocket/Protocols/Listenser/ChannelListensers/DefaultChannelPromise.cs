using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Listenser.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSerialPort.Protocols.Listenser.ChannelListensers
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
        public void AddDataPointListener(IChannelListener listener)
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
        public void RemoveDataPointListener(IChannelListener listener)
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
        /// <param name="controlerId"></param>
        public void OnReceivedDataPoint(NetworkData datas, string controlerId)
        {
            this.notifyListeners0(datas, controlerId);
        }
        /// <summary>
        /// 通知全部监听者
        /// </summary>
        /// <param name="listener"></param>
        private void notifyListeners0(NetworkData datas, string controlerId)
        {
            try
            {
                IChannelListener[] a = this.BusinessList.ToArray();
                int size = a.Length;
                for (int i = 0; i < size; ++i)
                {
                    notifyListener0( a[i],  datas,  controlerId);
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
        /// <param name="controlerId"></param>
        private void notifyListener0(IChannelListener channelListener, NetworkData datas, string controlerId)
        {
            try
            {
                channelListener.OnReceivedDataPoint( datas,  controlerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
