using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Exceptions
{
    public class SocketConnectionException : SkeException
    {
        public SocketConnectionException()
            : this(ExceptionType.Unknown)
        {
        }

        public SocketConnectionException(ExceptionType type)
        {
            Type = type;
        }

        public SocketConnectionException(ExceptionType type, Exception innerException)
            : this(type, innerException == null ? string.Empty : innerException.Message, innerException)
        {
        }

        public SocketConnectionException(ExceptionType type, string message) : base(message)
        {
            Type = type;
        }

        public SocketConnectionException(ExceptionType type, string message, Exception innerException)
            : base(message, innerException)
        {
            Type = type;
        }

        public ExceptionType Type { get; }
    }
}
