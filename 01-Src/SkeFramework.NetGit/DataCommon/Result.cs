using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataCommon
{
    /// <summary>
    /// 统一结果
    /// </summary>
    public class Result
    {
        public const int SuccessCode = 0;
        public const int FailureCode = 1;

        public Result(string stdout):this(stdout,"",FailureCode)
        {
          
        }

        public Result(string stdout, string stderr, int exitCode)
        {
            this.Output = stdout;
            this.Errors = stderr;
            this.ExitCode = exitCode;
        }

        public string Output { get; }
        public string Errors { get; }
        public int ExitCode { get; }

        public bool ExitCodeIsSuccess
        {
            get { return this.ExitCode == Result.SuccessCode; }
        }

        public bool ExitCodeIsFailure
        {
            get { return !this.ExitCodeIsSuccess; }
        }

        public bool StderrContainsErrors()
        {
            if (!string.IsNullOrWhiteSpace(this.Errors))
            {
                return !this.Errors
                    .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .All(line => line.TrimStart().StartsWith("warning:", StringComparison.OrdinalIgnoreCase));
            }
            return false;
        }
    }
}
