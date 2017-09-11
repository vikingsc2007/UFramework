using System.Collections.Generic;
using UFramework.Engine.Debuger;
using UFramework.Engine.Singleton;
using UnityEngine;

namespace UFramework.Engine.Pool.Pools
{
    public class GameObjectPoolMgr: SingletonMonoT<GameObjectPoolMgr>
    {
        
        private Dictionary<string, GameObjectPool> _poolDic ;
        public override void OnInstanced()
        {
            _poolDic = new Dictionary<string, GameObjectPool>();
        }
        
        
        public bool IsCreated(string name)
        {
            return _poolDic.ContainsKey(name);
        }

        public void Create(string poolName, GameObject prefab, int maxCount, int initCount)
        {
            if (_poolDic.ContainsKey(poolName))
            {
                Log.w("Already Init GameObjectPool:" + poolName);
                return;
            }
            
            GameObjectPool pool = new GameObjectPool(poolName, transform, prefab, maxCount, initCount);
            _poolDic.Add(poolName, pool);
        }

        public void Destroy(string poolName, bool destroySource)
        {
            GameObjectPool pool = null;
            if(_poolDic.TryGetValue(poolName, out pool))
            {
                pool.DestoryInstances();
                if(destroySource)
                    pool.DestorySource();
                _poolDic.Remove(poolName);
            }
        }

        public void DestroyAll(bool destroySource)
        {
            foreach (var pool in _poolDic)
            {
                pool.Value.DestoryInstances();
                if(destroySource)
                    pool.Value.DestorySource();
            }
            _poolDic.Clear();
        }

        public GameObject Pick(string poolName)
        {
            GameObjectPool pool = null;
            if (!_poolDic.TryGetValue(poolName, out pool))
            {
                return null;
            }

            return pool.Pick();
        }

        public void Drop(string poolName, GameObject obj)
        {
            GameObjectPool pool = null;
            if (!_poolDic.TryGetValue(poolName, out pool))
            {
                return;
            }

            pool.Drop(obj);
        }

        public void Drop(GameObject obj)
        {
            Drop(obj.name, obj);
        }
    }
}