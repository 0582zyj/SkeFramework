using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Protocols.Constants;
using SkeFramework.NetSerialPort.Protocols.Requests;

namespace SkeFramework.NetSerialPort.Protocols.Listenser
{
    public class SenderListenser
    {
        private NetworkData frame;
        private bool whetherSend = false;
        private int sentTimes = 0;
        private int totalSendTimes = 3;
        private readonly TimeCounter timeCounter;
        /// <summary>
        /// 只用于超时时设置它为Dead状态。
        /// </summary>
        private readonly IConnection caseObj;
        ///// <summary>
        ///// 此协议对象只用于发送数据。
        ///// </summary>
        //ProtocolBase protocol;

        /// <summary> 实例化帧发送器。</summary>
        /// <param name="protocol">要使用此协议对象发送数据。</param>
        internal SenderListenser(IConnection caseObj)
        {
            this.caseObj = caseObj;
            timeCounter = new TimeCounter();
        }

        /// <summary>
        /// 获取或设置要发送的帧。
        /// </summary>
        public NetworkData FrameBeSent
        {
            get { return frame; }
            set
            {
                frame = value;
                sentTimes = 0;
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
        /// 将当前的发送计时器设置为重新开始计时。
        /// </summary>
        public void ResetCalculagraph()
        {
            timeCounter.Reset();
            timeCounter.Start();
        }

        /// <summary>
        /// 获取或设置已经发送的次数。
        /// </summary>
        public int SentTimes
        {
            get { return sentTimes; }
            set { sentTimes = value; }
        }

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
        /// 开始发送帧数据。
        /// </summary>
        public bool BeginSend()
        {
            SentTimes = 0;
            whetherSend = true;
            return SendFrame();
        }
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
        public bool JustSendImmediately(NetworkData frame)
        {
            byte[] data = frame.Buffer;
            this.caseObj.Send(data, 0, data.Length, null);
            return true;
        }

      

        /// <summary>
        /// 在协议类的轮询中将执行此函数。
        /// </summary>
        internal void Send()
        {
            if (whetherSend && timeCounter.Over)
            {
                if (sentTimes < totalSendTimes || totalSendTimes == -1)
                {
                    SendFrame();
                }
                else
                {
                    caseObj.Dead=true;
                }
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <returns></returns>
        bool SendFrame()
        {
            bool ret = false;
            if (frame != null)
            {
                ++sentTimes;
                byte[] sendByte = frame.Buffer;
                this.caseObj.Send(sendByte, 0, sendByte.Length, null);
                timeCounter.Reset();
                timeCounter.Start();

            }
            return ret;
        }
      
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
            // this.Start();
        }
    }
}
