using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Protocols.Constants
{
    /// <summary>
    /// 协议数据缓冲区
    /// </summary>
   public class NetworkDataDocker
    {
        private readonly List<NetworkData> networkDataList= new List<NetworkData>();
        internal IList<NetworkData> BusinessCaseList
        {
            get { return networkDataList.AsReadOnly(); }
        }

        /// <summary>
        /// 添加Case对象到发送列表中。
        /// </summary>
        /// <param name="caseObj">业务对象。</param>
        internal void AddNetworkData(NetworkData caseObj)
        {
            if (caseObj != null && !networkDataList.Contains(caseObj))
            {
                lock (networkDataList)
                {
                    networkDataList.Insert(0, caseObj);
                }
            }
        }
        /// <summary>
        /// 在收发列表中清除业务对象。
        /// 当业务对象被设置死亡时会调用此函数。
        /// </summary>
        internal void RemoveNetworkData(NetworkData caseObj)
        {
            lock (networkDataList)
            {
                networkDataList.Remove(caseObj);
            }
        }

        /// <summary>
        /// 获取协议业务
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public NetworkData GetNetworkData()
        {
            lock (networkDataList)
            {
                NetworkData networkData= networkDataList.FirstOrDefault();
                networkDataList.Remove(networkData);
                return networkData;
            }
        }
    }
}
