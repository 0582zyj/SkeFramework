using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Protocols.Requests;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Connections
{
    /// <summary>
    /// 链接容器
    /// </summary>
    public class ConnectionDocker
    {
        private readonly ConcurrentStack<IConnection> caseList = new ConcurrentStack<IConnection>();
        internal IList<IConnection> BusinessCaseList
        {
            get { return caseList.ToList().AsReadOnly(); }
        }
        /// <summary>
        /// 添加Case对象到发送列表中。
        /// </summary>
        /// <param name="caseObj">业务对象。</param>
        internal void AddCase(IConnection caseObj)
        {
            try
            {
                if (caseObj != null && !BusinessCaseList.Contains(caseObj))
                {
                    lock (caseList)
                    {
                        caseList.Push(caseObj);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("AddCase：{0}", ex.ToString());
            }

        }
        /// <summary>
        /// 在收发列表中清除业务对象。
        /// 当业务对象被设置死亡时会调用此函数。
        /// </summary>
        internal void RemoveCase(IConnection caseObj)
        {
            try
            {
                lock (caseList)
                {
                   caseList.TryPop(out caseObj);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("RemoveCase：{0}", ex.ToString());
            }
        }
        /// <summary>
        /// 获取协议业务
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public IConnection GetCase(string cmd)
        {
            try
            {
                lock (caseList)
                {
                    return BusinessCaseList.OrderBy(o => o.Created).ToList()
                        .Find(o => o.ControlCode == cmd.ToString());
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("GetCase：{0}", ex.ToString());
            }
            return null;
        }
        /// <summary>
        /// 获取控制列表中最早的链接
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        public IConnection GetCase(List<string> cmdList)
        {
            try
            {
                lock (caseList)
                {
                    IList<IConnection> connections = this.BusinessCaseList.
                            Where(o => o.Receiving&& cmdList.Contains(o.ControlCode)).OrderByDescending(o => o.Created).ToList();
                    return connections.LastOrDefault();
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("GetCase：{0}", ex.ToString());
            }
            return null;
        }
        /// <summary>
        /// 获取协议业务
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public IConnection GetCase(INode node)
        {
            try
            {
                lock (caseList)
                {
                    return BusinessCaseList.ToList().Find(o => o.RemoteHost == node);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("GetCase：{0}", ex.ToString());
            }
            return null;
        }
        /// <summary>
        /// 任务过期
        /// </summary>
        /// <param name="task"></param>
        internal void SetCaseAsDead(ConnectionTask task)
        {
            IConnection csObj = null;
            lock (caseList)
            {
                foreach (IConnection cs in BusinessCaseList)
                {
                    if (cs == task.GetRelatedProtocol())
                    {
                        csObj = cs;
                        break;
                    }
                }
            }
            if (csObj != null&& csObj.WasDisposed)
            {
                csObj.Dead = true;
            }
        }
        /// <summary>
        /// 处理超时的协议
        /// </summary>
        internal void ProcessCaseOvertime()
        {
            try
            {
                lock (caseList)
                {
                    foreach (var cases in BusinessCaseList)
                    {
                        if (cases.Dead == true)
                        {
                            RemoveCase(cases);
                            string msg = string.Format("处理超时的协议：Name-{0};Time-{1}", cases.ControlCode, cases.Created.ToString());
                            Console.WriteLine(msg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("处理超时的协议：{0}", ex.ToString());
                Console.WriteLine(msg);
            }
        }
        /// <summary>
        /// 继续执行
        /// </summary>
        internal void ProcessCase()
        {
            try
            {
                lock (caseList)
                {
                    foreach (var cases in BusinessCaseList)
                    {
                        if (cases.Dead != true)
                        {
                            //if (cases.Reset)
                            //{
                            //    cases.Sender.FrameBeSent = cases.CreateFrame();
                            //    cases.Sender.FrameBeSent.SetCheckBytes();
                            //    cases.Reset = false;
                            //}
                            //cases.InternalPolling();
                            // 此行会导致业务对象cs被从列表中删除（在cs等待帧回复超时的情况下）。
                            if (cases is RefactorRequestChannel)
                            {
                                ((RefactorRequestChannel)cases).Sender.Send();
                            }
                            // cs还在列表中，说明它还活着。cs为死亡说明它已被从列表中删除（删除是上面的Send()函数导致的），所以i--。
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("发送业务列表对象的所有要发送的帧执行异常：{0}", ex.ToString());
            }
        }
    }
}
