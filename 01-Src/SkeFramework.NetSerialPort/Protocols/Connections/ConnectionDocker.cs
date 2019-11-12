using System;
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
       private readonly List<IConnection> caseList = new List<IConnection>();
       internal IList<IConnection> BusinessCaseList
        {
            get { return caseList.AsReadOnly(); }
        }

        /// <summary>
        /// 添加Case对象到发送列表中。
        /// </summary>
        /// <param name="caseObj">业务对象。</param>
       internal void AddCase(IConnection caseObj)
        {
            if (caseObj != null && !caseList.Contains(caseObj))
            {
                lock (caseList)
                {
                    caseList.Insert(0, caseObj);
                }
            }
        }
        /// <summary>
        /// 在收发列表中清除业务对象。
        /// 当业务对象被设置死亡时会调用此函数。
        /// </summary>
       internal void RemoveCase(IConnection caseObj)
        {
            lock (caseList)
            {
                caseList.Remove(caseObj);
            }
        }
        /// <summary>
        /// 获取协议业务
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
       public IConnection GetCase(byte cmd)
        {
            lock (caseList)
            {
                return caseList.Find(o => o.Local.TaskTag == cmd.ToString());
            }
        }
        /// <summary>
        /// 获取协议业务
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public IConnection GetCase(INode node)
        {
            lock (caseList)
            {
                return caseList.Find(o => o.Local == node);
            }
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
               foreach (IConnection cs in caseList)
               {
                    if (cs == task.GetRelatedProtocol())
                    {
                        csObj = cs;
                        break;
                    }
                }
           }
           if (csObj != null)
           {
               csObj.Dead = true;
           }
       }
      

        internal void ProcessCaseOvertime()
        {
            try
            {
                lock (caseList)
                {
                    foreach (var cases in caseList)
                    {
                        if (cases.Dead == true)
                        {
                            RemoveCase(cases);
                            Console.WriteLine("处理超时的协议:" + cases.Created.ToString() + " " );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("处理超时的协议：{0}", ex.ToString());
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
                    foreach (var cases in caseList)
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
                            if(cases is RefactorRequestChannel)
                            {
                               ((RefactorRequestChannel) cases).Sender.Send();
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
