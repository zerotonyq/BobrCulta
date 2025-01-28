using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Pooling
{
    public static class PoolManager
    {
        private static Dictionary<Type, Pool<GameObject>> _poolables = new();
        private static Dictionary<Type, GameObject> _prefabs = new();

        public static void RegisterPrefab(Type t, GameObject prefab)
        {
            ValidatePool(t);
            _prefabs.TryAdd(t, prefab);
        }

        public static void AddToPool(Type t, GameObject poolable)
        {
            if (!poolable.TryGetComponent(t, out _))
            {
                Debug.LogError("wrong type to pool!");
                return;
            }

            ValidatePool(t);

            _poolables[t].PoolQueue.Enqueue(poolable);
        }

        public static GameObject GetFromPool(Type t, GameObject prefab)
        {
            ValidatePool(t);

            if (_poolables[t].PoolQueue.Count != 0) 
                return _poolables[t].PoolQueue.Dequeue();
            
            var result = GameObject.Instantiate(prefab);
            result.SetActive(false);
            return result;
        }
        
        public static GameObject GetFromPool(Type t)
        {
            ValidatePool(t);

            if (_poolables[t].PoolQueue.Count != 0) 
                return _poolables[t].PoolQueue.Dequeue();
            
            if(!_prefabs.TryGetValue(t, out var prefab))
                Debug.LogError("There is no prefab to get from pool");
            
            var result = GameObject.Instantiate(prefab);
            
            result.SetActive(false);
            
            return result;
        }

        private static void ValidatePool(Type t)
        {
            if (!_poolables.ContainsKey(t))
                _poolables.Add(t, new Pool<GameObject>());
        }

        public static void ChangePoolCapacity(Type t, int capacity)
        {
            ValidatePool(t);
            _poolables[t].Capacity = capacity;
        }
    }

    public class Pool<T>
    {
        public readonly Queue<T> PoolQueue = new();
        public int Capacity = 3;
    }
}