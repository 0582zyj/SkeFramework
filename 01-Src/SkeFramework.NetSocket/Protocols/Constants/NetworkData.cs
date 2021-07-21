﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkeFramework.NetSerialPort.Buffers;
using SkeFramework.NetSerialPort.Topology;

namespace SkeFramework.NetSerialPort.Protocols.Constants
{
    /// <summary>
    /// 远程主机数据接收处理
    /// </summary>
    public class NetworkData
    {
        /// <summary>
        /// 远程节点信息
        /// </summary>
        public INode RemoteHost { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime Recieved { get; set; }
        /// <summary>
        /// 接收数据
        /// </summary>
        public byte[] Buffer { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 结果数据
        /// </summary>
        public object ResultData { get; set; }

        public byte[] GetReadableBuffer()
        {
            return Buffer.Take(Length).ToArray();
        }

        #region 生成一个远程数据对象
        /// <summary>
        /// 空数据
        /// </summary>
        public static NetworkData Empty = new NetworkData() { Length = 0, RemoteHost = null };

        public static NetworkData Create(INode node, byte[] data, int bytes)
        {
            return new NetworkData()
            {
                Buffer = data,
                Length = bytes,
                RemoteHost = node
            };
        }

        public static NetworkData Create(INode node, IByteBuf buf)
        {
            var readableBytes = buf.ReadableBytes;
            return new NetworkData()
            {
                Buffer = buf.ToArray(),
                Length = readableBytes,
                RemoteHost = node,
                Recieved=DateTime.Now
            };
        }
        #endregion

    }
}
