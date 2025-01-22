using System;
using System.Collections.Generic;
using Gameplay.Magic.Pickupables.Base;
using UnityEngine;
using Utils.Initialize;

namespace Utils.Pooling
{
    public static class PoolManager
    {
        private static Dictionary<Type, Pool<GameObject>> _poolables = new();

        public static void AddToPool(Type t, GameObject poolable)
        {
            if (!poolable.TryGetComponent(t, out _))
            {
                Debug.LogError("wrong type to pool!" );
                return;
            }

            if (!poolable.TryGetComponent(out IInitializableMono initializableMono))
            {
                Debug.LogError("type to pool must be derived from " + nameof(IInitializableMono));
                return;
            }

            ValidatePool(t);

            _poolables[t].PoolQueue.Enqueue(poolable);
        }

        public static GameObject GetFromPool(Type t, GameObject prefab)
        {
            ValidatePool(t);

            GameObject result;
            if (_poolables[t].PoolQueue.Count == 0)
            {
                result = GameObject.Instantiate(prefab);
                result.GetComponent<IInitializableMono>().Initialize();
            }
            else
                result = _poolables[t].PoolQueue.Dequeue();


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