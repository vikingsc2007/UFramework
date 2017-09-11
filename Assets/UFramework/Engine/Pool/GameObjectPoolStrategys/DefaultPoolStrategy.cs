using UFramework.Engine.Singleton;
using UnityEngine;

namespace UFramework.Engine.Pool.GameObjectPoolStrategys
{
    public class DefaultPoolStrategy: SingletonT<DefaultPoolStrategy>, IGameObjectPoolStrategy
    {
        public void ProcessContainer(GameObject container)
        {
            container.SetActive(false);
        }

        public void OnAllocate(GameObject result)
        {

        }

        public void OnRecycle(GameObject result)
        {

        }
    }
}