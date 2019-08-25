using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Ops
{
    public class TryCatchExecutor : BasicExecutor
    {
        private readonly Action<Exception> _exceptionCallback;

        public TryCatchExecutor() : this(exception => { })
        {
        }

        public TryCatchExecutor(Action<Exception> callback)
        {
            _exceptionCallback = callback;
        }


        public override void Execute(Action op)
        {
            try
            {
                if (!AcceptingJobs) return;
                op();
            }
            catch (Exception ex)
            {
                _exceptionCallback(ex);
            }
        }

        public override void Execute(IList<Action> ops, Action<IEnumerable<Action>> remainingOps)
        {
            var i = 0;
            try
            {
                for (; i < ops.Count; i++)
                {
                    if (!AcceptingJobs)
                    {
                        // ReSharper disable once AccessToModifiedClosure
                        //remainingOps.NotNull(obj => remainingOps(ops.Skip(i + 1)));
                        break;
                    }

                    ops[i]();
                }
            }
            catch (Exception ex)
            {
                //remainingOps.NotNull(obj => remainingOps(ops.Skip(i + 1)));
                _exceptionCallback(ex);
            }
        }
    }
}