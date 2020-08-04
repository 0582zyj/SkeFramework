using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataUtils
{
    public static class ConsoleUtil
    {
        public enum ActionResult
        {
            Success,
            CompletedWithErrors,
            Failure,
        }



        public static bool ShowStatusWhileRunning(
            Func<bool> action,
            string message,
            TextWriter output,
            bool showSpinner,
            int initialDelayMs = 0)
        {
            Func<ActionResult> actionResultAction =
                () =>
                {
                    return action() ? ActionResult.Success : ActionResult.Failure;
                };

            ActionResult result = ShowStatusWhileRunning(
                actionResultAction,
                message,
                output,
                showSpinner,
                initialDelayMs: initialDelayMs);

            return result == ActionResult.Success;
        }

        public static ActionResult ShowStatusWhileRunning(
            Func<ActionResult> action,
            string message,
            TextWriter output,
            bool showSpinner,
            int initialDelayMs)
        {
            ActionResult result = ActionResult.Failure;
            bool initialMessageWritten = false;

            try
            {
                if (!showSpinner)
                {
                    if (output != null)
                    {
                        output.Write(message + "...");
                    }
                    initialMessageWritten = true;
                    result = action();
                }
                else
                {
                    ManualResetEvent actionIsDone = new ManualResetEvent(false);
                    bool isComplete = false;
                    Thread spinnerThread = new Thread(
                        () =>
                        {
                            int retries = 0;
                            char[] waiting = { '\u2014', '\\', '|', '/' };

                            while (!isComplete)
                            {
                                if (retries == 0)
                                {
                                    actionIsDone.WaitOne(initialDelayMs);
                                }
                                else
                                {
                                    if (output != null)
                                    {
                                        output.Write("\r{0}...{1}", message, waiting[(retries / 2) % waiting.Length]);
                                    }
                                    initialMessageWritten = true;
                                    actionIsDone.WaitOne(100);
                                }

                                retries++;
                            }

                            if (initialMessageWritten)
                            {
                                // Clear out any trailing waiting character
                                if (output != null)
                                {
                                    output.Write("\r{0}...", message);
                                }
                            }
                        });
                    spinnerThread.Start();

                    try
                    {
                        result = action();
                    }
                    finally
                    {
                        isComplete = true;

                        actionIsDone.Set();
                        spinnerThread.Join();
                    }
                }
            }
            finally
            {
                switch (result)
                {
                    case ActionResult.Success:
                        if (initialMessageWritten)
                        {
                            if (output != null)
                            {
                                output.WriteLine("Succeeded");
                            }
                            
                        }

                        break;

                    case ActionResult.CompletedWithErrors:
                        if (!initialMessageWritten)
                        {
                            if (output != null)
                            {
                                output.Write("\r{0}...", message);
                            }
                            
                        }
                        if (output != null)
                        {
                            output.WriteLine("Completed with errors.");
                        }
                       
                        break;

                    case ActionResult.Failure:
                        if (!initialMessageWritten)
                        {
                            if (output != null)
                            {
                                output.Write("\r{0}...", message);
                            }
                        }
                        if (output != null)
                        {
                            output.WriteLine("Failed");
                        }
                   
                        break;
                }
            }

            return result;
        }
    }
}

