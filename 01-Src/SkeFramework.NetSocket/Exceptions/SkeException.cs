using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Exceptions
{
    public class SkeException : Exception
    {
        public SkeException()
        {
        }

        public SkeException(string message, Exception inner = null) : base(message, inner)
        {
        }
    }
}
