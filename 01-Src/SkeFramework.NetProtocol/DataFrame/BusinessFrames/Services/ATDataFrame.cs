using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULCloudLockTool.BLL.SHProtocol.DataFrame.Interfaces;

namespace ULCloudLockTool.BLL.SHProtocol.DataFrame.Services
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
