using UnityEngine;

namespace UFramework.Engine.Pool.GameObjectPoolStrategys
{
    public interface IGameObjectPoolStrategy
    {
        void ProcessContainer(GameObject container);
        void OnAllocate(GameObject result);
        void OnRecycle(GameObject result);
    }
}