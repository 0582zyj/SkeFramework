using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSocket.Buffers;
using SkeFramework.NetSocket.Topology;

namespace SkeFramework.NetSocket.Channels
{
    /// <summary>
    /// 连接参数配置
    /// </summary>
    public interface IConnectionConfig
    {
        IList<KeyValuePair<string, object>> Options { get; }

        /// <summary>
        /// 设置参数和值
        /// </summary>
        /// <param name="optionKey">名称</param>
        /// <param name="optionValue">值</param>
        /// <returns></returns>
        IConnectionConfig SetOption(string optionKey, object optionValue);

        /// <summary>
        /// 判断某个Key是否存在
        /// </summary>
        /// <param name="optionKey">名称</param>
        /// <returns>true if found, false otherwise</returns>
        bool HasOption(string optionKey);

        /// <summary>
        ///     Checks to see if we have a set option of ths key in the dictionary AND
        ///     that the value of this option is of type
        ///     <typeparam name="T"></typeparam>
        /// </summary>
        /// <param name="optionKey">The name of the value to check</param>
        /// <returns>true if found and of type T, false otherwise</returns>
        bool HasOption<T>(string optionKey);

        /// <summary>
        /// 获取Key对应的值
        /// </summary>
        /// <param name="optionKey">The name of the value to get</param>
        /// <returns>the object if found, null otherwise</returns>
        object GetOption(string optionKey);

        /// <summary>
        ///  获取值泛型方法
        ///  <typeparam name="T"></typeparam>
        /// </summary>
        /// <param name="optionKey">The name of the value to get</param>
        /// <returns>the object as instance of type T if found, default(T) otherwise</returns>
        T GetOption<T>(string optionKey);
    }

    /// <summary>
    /// 用于处理连接实例上的数据的状态对象
    /// </summary>
    public class NetworkState
    {
        public NetworkState(Socket socket, INode remoteHost, IByteBuf buffer, int rawBufferLength)
        {
            Buffer = buffer;
            RemoteHost = remoteHost;
            Socket = socket;
            RawBuffer = new byte[rawBufferLength];
        }

        /// <summary>
        /// The low-level socket object
        /// </summary>
        public Socket Socket { get; private set; }

        /// <summary>
        /// The remote host on the other end of the connection
        /// </summary>
        public INode RemoteHost { get; set; }

        /// <summary>
        /// The receive buffer used for processing data from this connection
        /// </summary>
        public IByteBuf Buffer { get; private set; }

        /// <summary>
        /// Raw buffer used for receiving data directly from the network
        /// </summary>
        public byte[] RawBuffer { get; private set; }
    }
}
