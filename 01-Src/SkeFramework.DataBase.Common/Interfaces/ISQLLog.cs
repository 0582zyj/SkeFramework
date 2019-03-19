using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.DataBase.Common.Interfaces
{
    /// <summary>
    /// SQL操作日志接口
    /// </summary>
    public interface ISQLLog
    {
        void OutputLog(string Msg);
    }

    public class ConsoleLog : ISQLLog
    {

        public void OutputLog(string Msg)
        {
            Console.WriteLine(Msg);
        }
    }
}
