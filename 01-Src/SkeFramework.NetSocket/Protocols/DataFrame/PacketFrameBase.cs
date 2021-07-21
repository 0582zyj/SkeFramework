using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SkeFramework.NetSerialPort.Protocols.DataFrame
{
    /// <summary>
    /// 文件数据包
    /// </summary>
    public class PacketFrameBase
    {
        /// <summary>
        /// 是否补齐字节
        /// </summary>
        private bool isFill;
        /// <summary>
        /// 分帧大小
        /// </summary>
        protected readonly int frameSize;
        /// <summary>
        /// 文件实际大小
        /// </summary>
        protected long fileLength;
        /// <summary>
        /// 分帧数据正文列表
        /// </summary>
        protected Dictionary<int, FrameBase> frameList = new Dictionary<int, FrameBase>();
        public PacketFrameBase(int singleFrameSize,bool isFillFrame=true)
        {
            this.frameSize = singleFrameSize;
            this.isFill = isFillFrame;
        }
        /// <summary>
        /// 总帧数
        /// </summary>
        public int FrameCount { get { return frameList.Count; } }
        /// <summary>
        /// 新增一个数据帧
        /// </summary>
        /// <param name="frameNum"></param>
        /// <param name="data"></param>
        public void AddOrUpdateFrameBase(int frameNum, byte[] data)
        {
            if (this.frameList.ContainsKey(frameNum))
            {
                this.frameList[frameNum] = new FrameBase(data, null);
                return;
            }
            this.frameList.Add(frameNum, new FrameBase(data, null));
        }
        /// <summary>
        /// 获取文件全部内容
        /// </summary>
        /// <returns></returns>
        public virtual List<byte> GetFileTextData()
        {
            List<byte> data = new List<byte>();
            if (this.frameList != null && this.frameList.Count > 0)
            {
                foreach (var item in this.frameList.Values)
                {
                    data.AddRange(item.FrameBytes);
                }
            }
            return data;
        }
        /// <summary>
        /// 获得下一个帧。若帧对象f为空则返回第一个帧，若帧对象f为最后一个帧则返回null。 
        /// </summary>
        /// <param name="f">帧对象</param>
        public virtual FrameBase GetFrameBase(int frameNum)
        {
            if (this.frameList.ContainsKey(frameNum))
            {
                return this.frameList[frameNum];
            }
            return null;
        }
        /// <summary>
        /// 创建一个文件发送包
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="frameSize"></param>
        /// <returns></returns>
        public static PacketFrameBase CreateSerialPackage<T>(T packet, string fileName)
            where T : PacketFrameBase
        {
            if (!File.Exists(fileName))
                return default(T);
            FileInfo file = new FileInfo(fileName);
            using (FileStream stream = file.OpenRead())
            {
                int frameNum = 0;
                packet.fileLength = file.Length;
                while (true)
                {
                    ++frameNum;
                    byte[] filedata = new byte[packet.frameSize];
                    int r = stream.Read(filedata, 0, packet.frameSize);
                    //如果读取到的字节数为0，说明已到达文件结尾，则退出while循
                    if (r == 0)
                    {
                        break;
                    }
                    if (r == packet.frameSize || packet.isFill)
                    {
                        packet.AddOrUpdateFrameBase(frameNum, filedata);
                    }
                    else
                    {
                        byte[] realdata = new byte[r];
                        Array.Copy(filedata, realdata, r);
                        packet.AddOrUpdateFrameBase(frameNum, realdata);
                    }
                }
            }
            return packet;
        }
    }
}