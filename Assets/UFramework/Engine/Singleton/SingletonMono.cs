using UFramework.Engine.Helper;
using UnityEngine;

namespace UFramework.Engine.Singleton
{
    public class SingletonMono : MonoBehaviour
    {
        private static bool m_IsApplicationQuit = false;

        public static bool isApplicationQuit
        {
            get { return m_IsApplicationQuit; }
            set { m_IsApplicationQuit = value; }
        }

        public static T CreateMonoSingleton<T>() where T : MonoBehaviour, ISingleton
        {
            if (!m_IsApplicationQuit)
            {
                T instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    System.Reflection.MemberInfo info = typeof(T);
                    object[] attributes = info.GetCustomAttributes(true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        SingletonMonoTAttribute defineAttri = attributes[i] as SingletonMonoTAttribute;
                        if (defineAttri == null)
                        {
                            continue;
                        }
                        instance = CreateComponentOnGameObject<T>(defineAttri.AbsolutePath, true);
                        break;
                    }

                    if (instance == null)
                    {
                        GameObject obj = new GameObject("Singleton[" + typeof(T).Name + "]");
                        DontDestroyOnLoad(obj);
                        instance = obj.AddComponent<T>();
                    }

                    instance.OnInstanced();
                }
                return instance;
            }
            else
            {
                return null;
            }
        }

        protected static T CreateComponentOnGameObject<T>(string path, bool dontDestroy) where T : MonoBehaviour
        {
            GameObject obj = GameObjectHelper.FindGameObject(null, path, true, dontDestroy);
            if (obj == null)
            {
                obj = new GameObject("Singleton[" + typeof(T).Name+ "]");
                if (dontDestroy)
                {
                    DontDestroyOnLoad(obj);
                }
            }

            return obj.AddComponent<T>();
        }
    }
}