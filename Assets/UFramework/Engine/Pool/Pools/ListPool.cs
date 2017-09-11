using System.Collections.Generic;
using UFramework.Engine.Pool.Interfaces;

namespace UFramework.Engine.Pool.Pools
{
    public class ListPool<T>:IPool
    {
        private static Stack<List<T>> _Cached = new Stack<List<T>>();

        public static List<T> Pick()
        {
            return _Cached.Count > 0 ? _Cached.Pop() : new List<T>();
        }

        public static void Drop(List<T> item)
        {
            if (item == null)
                return;
            item.Clear();
            _Cached.Push(item);
        }


        public int CachedCnt
        {
            get { return _Cached.Count; }
        }

        
        
        
    }
}