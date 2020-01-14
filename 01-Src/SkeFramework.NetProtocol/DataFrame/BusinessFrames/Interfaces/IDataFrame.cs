using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULCloudLockTool.BLL.SHProtocol.DataFrame.Interfaces
{
    /// <summary>
    /// 帧报文接口
    /// </summary>
    public interface IDataFrame
    {
        /// <summary>
        /// 报文解析
        /// </summary>
        /// <param name="dataBuffer"></param>
        /// <returns></returns>
        DataFrameBase ProcessDataFrame(byte[] dataBuffer);
    }
}
