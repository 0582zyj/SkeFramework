using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.NetSocket.Buffers
{
    
    /// <summary>
    /// 缓冲区操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICircularBuffer<T> : IProducerConsumerCollection<T>
    {
        /// <summary>
        /// 获取缓冲区最大大小
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// 获取当前缓冲区大小
        /// </summary>
        int Size { get; }

        /// <summary>
        /// 查看缓冲区的下一条信息
        /// </summary>
        /// <returns>The object at the start position of the buffer</returns>
        T Peek();

        /// <summary>
        ///  添加一条信息到缓冲区
        /// </summary>
        /// <param name="obj">An object of type T</param>
        void Enqueue(T obj);

        /// <summary>
        ///  添加多条信息到缓冲区
        /// </summary>
        /// <param name="objs">An array of objects of type T</param>
        void Enqueue(T[] objs);

        /// <summary>
        ///  删除缓冲区对象
        /// </summary>
        /// <returns>An object of type T</returns>
        T Dequeue();

        /// <summary>
        ///  删除缓冲区多个对象
        /// </summary>
        /// <param name="count">The maximum number of items to dequeue</param>
        /// <returns>An enumerable list of items</returns>
        IEnumerable<T> Dequeue(int count);

        /// <summary>
        ///  移除所有
        /// </summary>
        /// <returns>All of the active contents of a circular buffer</returns>
        IEnumerable<T> DequeueAll();

        /// <summary>
        /// 移除所有
        /// </summary>
        void Clear();

        /// <summary>
        /// 将循环缓冲区的内容复制到一个新数组中
        /// </summary>
        /// <param name="array">The destination array for the copy</param>
        void CopyTo(T[] array);

        void CopyTo(T[] array, int index, int count);
    }
}
