using UFramework.Engine.Pool.Interfaces;
using UFramework.Engine.RefCounter;

namespace UFramework.Framework.ResSystem.Interfaces
{
    public class IRes : RefCount, IPoolItem
    {
        public bool Cached {
            get;
            set;
        }

        public virtual void OnCache()
        {
        }

        public virtual void OnReborn()
        {
        }
    }
}