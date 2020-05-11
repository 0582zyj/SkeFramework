using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkeFramework.NetProtocol.DataFrame.Interfaces;

namespace SkeFramework.NetProtocol.DataFrame.Services
{
    /// <summary>
    /// AT帧报文
    /// </summary>
    public class ATDataFrame : IDataFrame
    {
        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="dataBuffer"></param>
        /// <returns></returns>
        public DataFrameBase ProcessDataFrame(byte[] dataBuffer)
        {
            return null;
        }
    }
}
