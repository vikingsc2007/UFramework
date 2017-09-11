namespace UFramework.Engine.RefCounter
{
    public interface IRefCount
    {
        int refCnt{get;}
        
        void AddRef();
        void SubRef();
    }

    public class RefCount : IRefCount
    {
        private int m_refCnt = 0;

        public int refCnt
        {
            get { return m_refCnt; }
        }

        public void AddRef() { ++m_refCnt; }
        public void SubRef()
        {
            --m_refCnt;
            if (m_refCnt == 0)
            {
                OnZeroRef();
            }
        }

        protected virtual void OnZeroRef()
        {

        }
    }
}
