using UFramework.Engine.Singleton;
using UnityEngine;

namespace UFramework.Engine.Pool.GameObjectPoolStrategys
{
    public class UIPoolStrategy : SingletonT<UIPoolStrategy>, IGameObjectPoolStrategy
    {
        public void ProcessContainer(GameObject container)
        {
            //UITools.SetGameObjectLayer(container, LayerDefine.LAYER_HIDE_UI);
        }

        public void OnAllocate(GameObject result)
        {
            //UITools.SetGameObjectLayer(result, LayerDefine.LAYER_UI);
        }

        public void OnRecycle(GameObject result)
        {
            //UITools.SetGameObjectLayer(result, LayerDefine.LAYER_HIDE_UI);
        }
    }
}