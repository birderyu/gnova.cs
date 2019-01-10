using System;

namespace GNova.Core
{
    /// <summary>
    /// 对象池的参数配置
    /// </summary>
    public class ObjectPoolConfig<T>
    {

        public ObjectPoolConfig()
        {
            ObjectBuilder = () => { return default(T); };
            ObjectValidater = (T obj) => { if (obj == null) throw new NullReferenceException(); };
            MaxActiveCount = 8;
            MaxIdleCount = 8;
            MinIdleCount = 0;
            ValidateOnBuild = false;
            ValidateOnBorrow = false;
            ValidateOnReturn = false;
            BlockWhenExhausted = false;
            MaxWaitMilliseconds = -1L;
        }

        /// <summary>
        /// 创建一个对象的方法
        /// </summary>
        public Func<T> ObjectBuilder { get; set; }

        /// <summary>
        /// 验证一个对象是否有效的方法
        /// 若对象是无效的，则抛出异常
        /// </summary>
        public Action<T> ObjectValidater { get; set; }

        /// <summary>
        /// 最大活动实例的数量
        /// </summary>
        public int MaxActiveCount { get; set; }

        /// <summary>
        /// 最大闲置实例的数量
        /// </summary>
        public int MaxIdleCount { get; set; }

        /// <summary>
        /// 最小闲置实例的数量
        /// </summary>
        public int MinIdleCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateOnBuild { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateOnBorrow { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool ValidateOnReturn { get; set; }

        /// <summary>
        /// 当池中的对象耗尽时，再获取一个对象时，是否阻塞，若不阻塞则直接抛出异常
        /// </summary>
        public bool BlockWhenExhausted { get; set; }

        /// <summary>
        /// 当池中的对象耗尽时，再获取一个对象时，若阻塞，等待的时间，单位毫秒
        /// 等待时间过后，若还没有获取到对象，则抛出异常
        /// 若该值设置为负数，则表示永远阻塞
        /// 该属性仅当WhenExhaustedBlock设置为true时有效
        /// </summary>
        public long MaxWaitMilliseconds { get; set; }

        /// <summary>
        /// 每次检查闲置可用性的间隔时间，单位毫秒
        /// 当该值设置为正数时，会在后台开启一个线程，该线程会在Redis对象闲置时，检查其可用性
        /// 当该值设置为负数时，则不会开启该线程
        /// </summary>
        public long TimeBetweenEvictionRunsMilliseconds { get; set; }

        /// <summary>
        /// 当Redis对象闲置时，是否测试其可用性
        /// 若TimeBetweenEvictionRunsMilliseconds设置为负数，则该值失效
        /// </summary>
        public bool ValidateWhileIdle { get; set; }
    }
}
