using System.Collections.Generic;
namespace UFramework.Engine.Pool
{
    public class ListPool<T>
    {
        private static Stack<List<T>> m_Cached;

        public static List<T> Allocate()
        {
            List<T> result;
            if (m_Cached == null || m_Cached.Count == 0)
            {
                result = new List<T>();
            }
            else
            {
                result = m_Cached.Pop();
            }

            return result;
        }

        public static void Recycle(List<T> t)
        {
            if (t == null)
            {
                return;
            }

            t.Clear();

            if (m_Cached == null)
            {
                m_Cached = new Stack<List<T>>();
            }

            m_Cached.Push(t);
        }
    }
}