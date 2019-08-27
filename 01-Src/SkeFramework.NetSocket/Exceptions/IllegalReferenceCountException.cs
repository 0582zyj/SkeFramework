using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Exceptions
{
    public class IllegalReferenceCountException : SkeException
    {
        public IllegalReferenceCountException(int count)
            : base(string.Format("Illegal reference count of {0} for this object", count))
        {
        }

        public IllegalReferenceCountException(int count, int increment)
            : base(
                string.Format("Illegal reference count of {0} for this object; was attempting to increment by {1}",
                    count, increment))
        {
        }
    }
}
