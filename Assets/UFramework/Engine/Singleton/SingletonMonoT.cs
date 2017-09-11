namespace UFramework.Engine.Singleton
{
    public class SingletonMonoT
    {
        
    }
    
    public abstract class SingletonMonoT<T> : SingletonMono, ISingleton where T : SingletonMonoT<T>
    {
        private static T        s_Instance = null;
        private static object   s_lock = new object();


        public static T S
        {
            get
            {
                if (s_Instance == null)
                {
                    lock (s_lock)
                    {
                        if (s_Instance == null)
                        {
                            s_Instance = CreateMonoSingleton<T>();
                        }
                    }
                }

                return s_Instance;
            }
        }

        public virtual void OnInstanced()
        {
            
        }

    }
}