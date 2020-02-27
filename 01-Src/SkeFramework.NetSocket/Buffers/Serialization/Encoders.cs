using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers.Serialization.Achieves;

namespace SkeFramework.NetSocket.Buffers.Serialization
{
   public static class Encoders
    {
        public static IMessageDecoder DefaultDecoder
        {
            get { return new NoOpDecoder(); }
        }


        /// <summary>
        ///     The default encoder option
        /// </summary>
        public static IMessageEncoder DefaultEncoder
        {
            get { return new NoOpEncoder(); }
        }
    }
}
