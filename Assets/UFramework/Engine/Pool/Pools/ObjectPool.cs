using UnityEngine;
using System.Collections.Generic;
using UFramework.Engine.Singleton;

namespace UFramework.Engine.Pool.Pools
{
    public class ObjectPool<T> : SingletonT<ObjectPool<T>>, IPool where T : IPoolItem, new()
    {

        //==================================实现IPool========================================================
        private Stack<T>    _cached;
        public int CachedCnt
        {
            get
            {
                return _cached == null ? 0 :_cached.Count;
            }
        }
        //======================================================================================================================
        
        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="maxcount">对象池最大容量</param>
        /// <param name="initcount">对象池初始容量</param>
        public void Initalize(int maxcount, int initcount)
        {
            InitCnt = MaxCnt > 0 ? Mathf.Min(maxcount, initcount):0;
            MaxCnt = maxcount;

            if (CachedCnt < InitCnt)
            {
                for (int i = CachedCnt; i < InitCnt; ++i)
                {
                    Drop(new T());
                }
            }
        }
        
        
        //======================================================================================================================
        
        public int MaxCnt { private set; get; }
        public int InitCnt { private set; get; }
        
        public int MaxCacheCount
        {
            get { return MaxCnt; }
            set
            {
                MaxCnt = value;

                if (_cached != null)
                {
                    for (int i = MaxCnt; i < _cached.Count; i++)
                    {
                        _cached.Pop();
                    }
                }
            }
        }
        
        
        //======================================================================================================================
        
        /// <summary>
        /// 从池中获取对象没有则新建
        /// </summary>
        /// <returns></returns>
        public T Pick()
        {
            T item;
            if (_cached == null || _cached.Count <= 0)
            {
                item = new T();
                return default(T);
            }
            else
            {
                item = _cached.Pop();
                item.OnReborn();
            }

            item.Cached = false;
            return item;
        }
        
        
        /// <summary>
        /// 丢弃一个对象进池中，池满则不进
        /// </summary>
        /// <param name="item"></param>
        /// <returns>是否进入池中</returns>
        public bool Drop(T item)
        {
            if (item == null || item.Cached)
                return false;

            if (_cached == null)
                _cached = new Stack<T>();

            if (_cached.Count < MaxCnt)
            {
                _cached.Push(item);
                item.Cached = true;
            }
            item.OnCache();
            return true;
        }
        
        //======================================================================================================================
        
    }
}
