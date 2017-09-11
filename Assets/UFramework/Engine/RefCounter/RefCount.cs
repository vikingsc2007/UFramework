namespace UFramework.Engine.RefCounter
{
    public interface IRef
    {
        int refCnt{get;}
        
        
        void retain();//增加引用计数
        void release();//减少引用计数

    }

    public class RefCount : IRef
    {
        private int _refCnt = 0;

        public int refCnt
        {
            get { return _refCnt; }
        }

        public void retain() { ++_refCnt; }
        public void release()
        {
            --_refCnt;
            if (_refCnt == 0)
            {
                OnDispose();
            }
        }

        protected virtual void OnDispose()
        {

        }
    }
}
