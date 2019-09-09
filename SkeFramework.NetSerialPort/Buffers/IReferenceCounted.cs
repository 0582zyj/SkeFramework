using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSerialPort.Buffers
{
    /// <summary>
    ///  引用计数接口
    /// </summary>
    public interface IReferenceCounted
    {
        /// <summary>
        ///  当前计数
        /// </summary>
        int ReferenceCount { get; }
        /// <summary>
        /// 计数自增1
        /// </summary>
        IReferenceCounted Retain();
        /// <summary>
        /// 计数增加n
        /// </summary>
        IReferenceCounted Retain(int increment);

        IReferenceCounted Touch();

        IReferenceCounted Touch(object hint);
        /// <summary>
        /// 将引用计数减少1，如果引用计数达到0，则释放该对象。
        /// </summary>
        /// <returns>当且仅当引用计数为0且该对象已被释放时，返回 true </returns>
        bool Release();
        /// <summary>
        /// 将引用计数减少n，如果引用计数达到0，则释放该对象。
        /// </summary>
        /// <returns>当且仅当引用计数为0且该对象已被释放时，返回 true </returns>
        bool Release(int decrement);
    }
}
