using System.Collections.Generic;
using UFramework.Engine.Debuger;
using UFramework.Engine.Pool.Interfaces;
using UnityEngine;

namespace UFramework.Engine.Pool.Pools
{
    public class GameObjectPool:IPool
    {
        public GameObjectPool(string name, Transform parent, GameObject prefab, int maxcount, int initcount)
        {
            Initalize(name,parent,prefab,maxcount,initcount);
        }

        //============================================================================================================
        public GameObject Prefab { get;private set; }
        public Transform Root { get;private set; }
        public string Name { get;private set; }
        
        //==================================实现IPool========================================================
        private Stack<GameObject>    _cached =new Stack<GameObject>();
        public int CachedCnt
        {
            get
            {
                return _cached == null ? 0 :_cached.Count;
            }
        }
        //======================================================================================================================
        
        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="maxcount">对象池最大容量</param>
        /// <param name="initcount">对象池初始容量</param>
        public void Initalize(string name, Transform parent, GameObject prefab, int maxcount, int initcount)
        {
            if (prefab == null || Prefab != null)
                return;
            
            
            Prefab = prefab;
            Root = parent;
            Name = name;
            
            
            GameObject container = new GameObject(Name);
            Root = container.transform;
            Root.SetParent(parent);
            container.SetActive(false);
            
            
            
            InitCnt = MaxCnt > 0 ? Mathf.Min(maxcount, initcount):0;
            MaxCnt = maxcount;

            
            if (CachedCnt < InitCnt)
            {
                for (int i = CachedCnt; i < InitCnt; ++i)
                {
                    Drop(GenerateGameObject());
                }
            }
        }
        
        
        //======================================================================================================================
        
        public int MaxCnt { private set; get; }
        public int InitCnt { private set; get; }
        
        public int MaxCacheCount
        {
            get { return MaxCnt; }
            set
            {
                MaxCnt = value;

                if (_cached != null)
                {
                    for (int i = MaxCnt; i < _cached.Count; i++)
                    {
                        _cached.Pop();
                    }
                }
            }
        }
        
        
        //======================================================================================================================
        
        /// <summary>
        /// 从池中获取对象没有则新建
        /// </summary>
        /// <returns></returns>
        public GameObject Pick()
        {
            return (_cached == null || _cached.Count <= 0)? null : _cached.Pop();
        }
        
        
        /// <summary>
        /// 丢弃一个对象进池中，池满则不进
        /// </summary>
        /// <param name="item"></param>
        /// <returns>是否进入池中</returns>
        public bool Drop(GameObject item)
        {
            _cached =_cached ==null ?  new Stack<GameObject>():_cached;

            if (_cached.Count < MaxCnt)
            {
                _cached.Push(item);
            }
            return true;
        }


        public void DestoryInstances()
        {
            if (Root != null)
            {
                GameObject.Destroy(Root.gameObject);
                Root = null;
            }

            if (_cached != null)
            {
                _cached.Clear();
            }
        }


        public void DestorySource()
        {
            if (Prefab != null)
            {
                GameObject.Destroy(Prefab);
                Prefab = null;
            }
        }


        public void ClearCache()
        {
            if (_cached != null && _cached.Count > 0)
            {
                while (_cached.Count > 0)
                {
                    GameObject.Destroy(_cached.Pop());
                }
            }
        }

        private GameObject GenerateGameObject()
        {
            if (Prefab == null)
                return null;
            GameObject obj = GameObject.Instantiate(Prefab, Root, true) as GameObject;
            obj.name = Name;
            return obj;
        }
    }
    
    
    
}