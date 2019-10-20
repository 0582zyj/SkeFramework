using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Protocols;

namespace SkeFramework.NetSerialPort.Buffers
{
    /// <summary>
    ///  字节编码接口
    /// </summary>
    public interface IMessageEncoder
    {
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="connection">链接</param>
        /// <param name="buffer"></param>
        /// <param name="encoded"></param>
        void Encode(IConnection connection, IByteBuf buffer, out List<IByteBuf> encoded);
        /// <summary>
        /// 深度克隆
        /// </summary>
        IMessageEncoder Clone();
    }
}
