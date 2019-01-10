using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNova.Core
{
    /// <summary>
    /// 对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectPool<T> : IDisposable
    {

        /// <summary>
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsClosed { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        T BorrowObject();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="Exception"></exception>
        void ReturnObject(T obj);

    }
}
