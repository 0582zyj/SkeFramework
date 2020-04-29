using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Protocols.Connections.Tasks
{
    /// <summary>
    /// 异步任务结果。
    /// </summary>
    public class AsyncTaskResult : IAsyncResult
    {
        /// <summary>
        /// 它表示正在异步操作中。
        /// </summary>
        private const int StatePending = 0;
        /// <summary>
        /// 它表示同步操作完成。
        /// </summary>
        private const int StateCompletedSynchronously = 1;
        /// <summary>
        /// 它表示异步操作完成。
        /// </summary>
        private const int StateCompletedAsynchronously = 2;
        /// <summary>
        /// 异步操作完成标识。
        /// </summary>
        private int completedState = StatePending;

        /// <summary>
        /// 异步操作完成时调用的回调方法。
        /// </summary>
        private readonly AsyncCallback asyncCallback;
        private readonly object asyncState;

        /// <summary>
        /// 异步等待事件。
        /// </summary>
        private ManualResetEvent asyncWaitHandle;

        public AsyncTaskResult(AsyncCallback asyncCallback, object state)
        {
            this.asyncCallback = asyncCallback;
            this.asyncState = state;
        }

        /// <summary>
        /// 返回回调函数对象是否相等。
        /// </summary>
        /// <param name="asyncCallback">要判断的回调函数对象。</param>
        internal bool AsyncCallbackObjEquals(AsyncCallback asyncCallback)
        {
            return this.asyncCallback == asyncCallback;
        }

        public void SetAsCompleted(bool completedSynchronously)
        {
            // The m_CompletedState field MUST be set prior calling the callback
            Interlocked.Exchange(ref completedState,
               completedSynchronously ? StateCompletedSynchronously : StateCompletedAsynchronously);

            // If the event exists, set it
            if (asyncWaitHandle != null)
                asyncWaitHandle.Set();

            // If a callback method was set, call it
            if (asyncCallback != null)
                asyncCallback(this);
        }

        /// <summary>
        /// 等待同步完成，它将阻塞当前线程，直至等待超过timeout时限，或等待事件被设置。
        /// </summary>
        /// <param name="timeout">等待的毫秒数。-1表示无限期等待。</param>
        public void EndInvoke(int timeout)
        {
            // This method assumes that only 1 thread calls EndInvoke for this object
            if (!IsCompleted)
            {
                // If the operation isn't done, wait for it
                //Translated from Java, -1 means wait to done
                if (-1 == timeout)
                {
                    timeout = 0;
                }
                AsyncWaitHandle.WaitOne(timeout, false);
                AsyncWaitHandle.Close();
                asyncWaitHandle = null;
            }
        }

        #region Implementation of IAsyncResult

        public object AsyncState
        {
            get { return asyncState; }
        }

        public Boolean CompletedSynchronously
        {
            get { return completedState == StateCompletedSynchronously; }
        }

        /// <summary>
        /// 获取用于等待异步操作完成的 WaitHandle。
        /// 返回值允许客户端等待异步操作完成，而不是轮询 IsCompleted 直到操作结束。返回值可用于执行 WaitOne、WaitAny 或 WaitAll 操作。
        /// </summary>
        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (asyncWaitHandle == null)
                {
                    bool done = IsCompleted;
                    ManualResetEvent mre = new ManualResetEvent(done);
                    if (Interlocked.CompareExchange(ref asyncWaitHandle, mre, null) != null)
                        mre.Close();    // Another thread created this object's event; dispose the event we just created
                    else
                    {
                        if (!done && IsCompleted)
                            asyncWaitHandle.Set();  // If the operation wasn't done when we created the event but now it is done, set the event
                    }
                }
                return asyncWaitHandle;
            }
        }

        public bool IsCompleted
        {
            get { return completedState != StatePending; }
        }

        #endregion
    }
}