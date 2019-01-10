using System;
using System.Collections.Concurrent;

namespace GNova.Core
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private IProducerConsumerCollection<T> _buffer;
        private ObjectPoolConfig<T> _config;

        /// <summary>
        /// 
        /// </summary>
        public ObjectPool()
            : this(new ObjectPoolConfig<T>())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectBuilder"></param>
        /// <param name="capacity"></param>
        /// <param name="timeout">等待时间，单位为毫秒，若为0，则表示不等待</param>
        public ObjectPool(ObjectPoolConfig<T> config)
        {
            _config = config;
            _buffer = new ConcurrentBag<T>();
            IsClosed = false;
        }

        public int Count
        {
            get
            {
                return _buffer.Count;
            }
        }

        public int Capacity
        {
            get
            {
                return _config.MaxActiveCount;
            }
        }

        public bool IsClosed { get; private set; }

        public T BorrowObject()
        {
            AssertOpen();

            T obj;
            if (_buffer.TryTake(out obj))
            {
                if (_config.ValidateOnBorrow)
                {
                    _config.ObjectValidater(obj);
                }
                return obj;
            }
            else
            {
                return Build();
            }
        }

        public void ReturnObject(T obj)
        {
            AssertOpen();

            if (_config.ValidateOnReturn)
            {
                _config.ObjectValidater(obj);
            }
            Add(obj);
        }

        public void Dispose()
        {
            IsClosed = true;
        }

        private void AssertOpen()
        {
            if (IsClosed)
            {
                throw new Exception("pool has been closed");
            }
        }

        private void Add(T obj)
        {
            long startMilliseconds = DateTime.Now.Ticks / 1000;
            while (_buffer.Count >= _config.MaxActiveCount)
            {
                if (!_config.BlockWhenExhausted)
                {
                    throw new Exception("pool is full");
                }
                if (_config.MaxWaitMilliseconds < 0)
                {
                    continue;
                }
                long currentMilliseconds = DateTime.Now.Ticks / 1000;
                if (currentMilliseconds - startMilliseconds > _config.MaxWaitMilliseconds)
                {
                    throw new Exception("timeout, pool is full");
                }
            }

            if (!_buffer.TryAdd(obj))
            {
                throw new Exception("error on return");
            }
        }

        private T Build()
        {
            T obj = _config.ObjectBuilder();
            if (_config.ValidateOnBuild)
            {
                _config.ObjectValidater(obj);
            }
            return obj;
        }

    }
}
