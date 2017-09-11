namespace UFramework.Engine.Singleton
{
    public class SingletonT<T> : ISingleton where T : SingletonT<T>, new()
    {
        protected static T		s_Instance;
        protected static object s_lock = new object();

        public static T Inst
        {
            get
            {
                if(s_Instance == null)
                {
                    lock (s_lock)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = new T();
                            s_Instance.OnInstanced();
                        }
                    }
                }
                return s_Instance;
            }
        }

        public static T ReInstance()
        {
            s_Instance = new T();
            s_Instance.OnInstanced();
            return s_Instance;
        }

        public virtual void OnInstanced()
        {
        }
    }
}