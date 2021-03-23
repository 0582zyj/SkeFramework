using SkeFramework.NetSerialPort.Net.Reactor;
using SkeFramework.NetSerialPort.Protocols.Connections;
using SkeFramework.NetSerialPort.Protocols.Connections.Tasks;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Listenser.Interfaces;
using SkeFramework.NetSerialPort.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetProtocol.BusinessCase.Abstracts
{
    /// <summary>
    /// 组合规约请求
    /// </summary>
    internal class ComplexRequestChannel : SingleRequestChannel, IDisposable, IChannelListener
    {
        /// <summary>
        /// 监听协议列表
        /// </summary>
        protected List<string> ListenerTaskIdList = new List<string>();
  

        public ComplexRequestChannel(ReactorBase reactor, List<string> ListeningTaskIds)
         : this(reactor, null, ListeningTaskIds)
        {
        }

        public ComplexRequestChannel(ReactorBase reactor, INode endPoint, List<string> ListeningTaskIds)
            : base(reactor, endPoint, ListeningTaskIds.FirstOrDefault().ToString())
        {
            ListenerTaskIdList.AddRange(ListeningTaskIds);
            ProtocolProxyAgent.Instance().AddDataPointListener(this);
        }



        protected override void Dispose(bool disposing)
        {
            ProtocolProxyAgent.Instance().RemoveDataPointListener(this);
            base.Dispose(disposing);
        }
       
        /// <summary>
        /// 监听通知
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="taskId"></param>
        public virtual void OnReceivedDataPoint(NetworkData datas, string taskId)
        {
            if (!this.ListenerTaskIdList.Contains(taskId))
                return;
            this.BeginReceive();
            TaskResult taskResult = new TaskResult()
            {
                ResultCode = Convert.ToInt32(taskId),
                Param = datas.ResultData
            };
            datas.ResultData = taskResult;
            this.InvokeReceiveIfNotNull(datas);
        }
    }
}

