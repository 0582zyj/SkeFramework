using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkeFramework.Core.NetLog;
using SkeFramework.Push.Core.Interfaces;
using SkeFramework.Push.Core.Services.Brokers;

namespace SkeFramework.Push.Core.Listenser
{
    /// <summary>
    /// 发送监听器
    /// </summary>
    public class SenderListenser<TNotification>
         where TNotification : INotification
    {
        private INotification frame;
        private bool whetherSend = false;
        private int totalSendTimes = 3;
        private readonly TimeCounter timeCounter;
        /// <summary>
        /// 只用于超时时设置它为Dead状态。
        /// </summary>
        private readonly IPushConnection<TNotification> caseObj;

        /// <summary> 实例化帧发送器。</summary>
        /// <param name="protocol">要使用此协议对象发送数据。</param>
        public SenderListenser(IPushConnection<TNotification> caseObj)
        {
            this.caseObj = caseObj;
            timeCounter = new TimeCounter();
            this.SentTimes = 0;
            this.CancelTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// 获取或设置要发送的帧。
        /// </summary>
        public INotification FrameBeSent
        {
            get { return frame; }
            set
            {
                frame = value;
                SentTimes = 0;
            }
        }
        /// <summary>
        /// 获取或设置发送间隔（默认值为3000毫秒）。
        /// </summary>
        /// <returns></returns>
        public int Interval
        {
            get { return timeCounter.Timeout; }
            set { timeCounter.Timeout = value > 0 ? value : 0; }
        }

        /// <summary>
        /// 获取或设置已经发送的次数。
        /// </summary>
        public int SentTimes { get; set; }
        /// <summary>
        /// 获取或设置要发送帧数据的次数。
        /// 若设置值为-1，表示不限制发送的次数。
        /// </summary>
        public int TotalSendTimes
        {
            get { return totalSendTimes; }
            set { totalSendTimes = value >= 0 ? value : -1; }
        }
        /// <summary>
        /// 超时时间[毫秒]
        /// </summary>
        public int TimeoutMS { get { return this.Interval * this.totalSendTimes; } }


        /// <summary>
        /// 取消发送【多线程】
        /// </summary>
        public CancellationTokenSource CancelTokenSource { get; private set; }
        /// <summary>
        /// 工作任务
        /// </summary>
        public Task WorkerTask { get; private set; }
        /// <summary>
        /// 开始任务
        /// </summary>
        public void Start()
        {
            SentTimes = 0;
            whetherSend = true;
            //发送任务列表
            WorkerTask = Task.Factory.StartNew(async delegate
            {
                var toSend = new List<Task>();

                while (!CancelTokenSource.IsCancellationRequested&& whetherSend)
                {
                    try
                    {
                        Thread.Sleep(100);
                        if (timeCounter.Over|| SentTimes==0)
                        {
                            if (SentTimes < totalSendTimes || totalSendTimes == -1)
                            {
                                //发送任务列表
                                toSend.Add(SendFrame());
                            }
                            else
                            {
                                try
                                {
                                    LogAgent.Info("Waiting on all tasks {0}", toSend.Count());
                                    await Task.WhenAll(toSend).ConfigureAwait(false);
                                    LogAgent.Info("All Tasks Finished");
                                }
                                catch (Exception ex)
                                {
                                    LogAgent.Error("Waiting on all tasks Failed: {0}", ex);
                                }
                                caseObj.StopReceive();
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogAgent.Error("Broker.Take: {0}", ex);
                    }
                    if (CancelTokenSource.IsCancellationRequested)
                        LogAgent.Info("Cancellation was requested");
                    if (SentTimes < totalSendTimes)
                        continue;
                  
                }
            }, CancelTokenSource.Token, TaskCreationOptions.LongRunning
            | TaskCreationOptions.DenyChildAttach, TaskScheduler.Default).Unwrap();

        }


        #region 发送数据
        /// <summary>
        /// 结束帧数据的发送。
        /// </summary>
        public void EndSend()
        {
            whetherSend = false;
        }
        /// <summary>
        /// 立即发送一个帧，而不计算发送次数。
        /// 它不会影响FrameBeSent帧的发送。
        /// 发送成功则返回true.</summary>
        /// <param name="frame">要发送的帧对象。</param>
        public bool JustSendImmediately(TNotification frame)
        {
            this.caseObj.Send(frame);
            return true;
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <returns></returns>
        private Task SendFrame()
        {
            if (frame == null)
                return null;
            ++SentTimes;
            Task task = this.caseObj.Send((TNotification)frame);
            this.ResetCalculagraph();
            return task;
        }
        /// <summary>
        /// 将当前的发送计时器设置为重新开始计时。
        /// </summary>
        public void ResetCalculagraph()
        {
            timeCounter.Reset();
            timeCounter.Start();
        }
        #endregion


    }

    /// <summary>时限计时器
    /// </summary>
    public class TimeCounter : System.Diagnostics.Stopwatch
    {
        /// <summary>
        ///  时限，单位毫秒
        /// </summary>
        int timeout = 3000;

        /// <summary>
        /// 获得或设置时限（单位为毫秒）
        /// </summary>
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        /// <summary>
        /// 获取或设置是否已经超时。
        /// </summary>
        public bool Over
        {
            get
            {
                return this.ElapsedMilliseconds >= timeout;
            }
        }

        public TimeCounter()
        {
            // this.Start();
        }

        /// <summary>
        /// 实例化一个时限计时器。
        /// 对象默认状态为超时状态（即计时器值为0）。
        /// </summary>
        /// <param name="timeout">时限（单位毫秒）</param>
        public TimeCounter(int timeout)
        {
            this.timeout = timeout;
        }
    }
}
