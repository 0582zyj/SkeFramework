using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Serialization
{
    public static class Encoders
    {
        /// <summary>
        ///     The default decoder option
        /// </summary>
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
