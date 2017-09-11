using System;

namespace UFramework.Engine.Singleton
{
    
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonMonoTAttribute : System.Attribute
    {
        private string m_AbsolutePath;

        public SingletonMonoTAttribute(string relativePath)
        {
            m_AbsolutePath = relativePath;
        }

        public string AbsolutePath
        {
            get { return m_AbsolutePath; }
        }
    }
}