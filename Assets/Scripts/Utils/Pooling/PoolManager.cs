using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Pooling
{
    public static class PoolManager
    {
        private static Dictionary<Type, Pool<MonoBehaviour>> _poolables = new();

        public static void AddToPool(Type t, MonoBehaviour poolable)
        {
            Debug.Log(t.Name);
            if (poolable.GetType() != t)
            {
                Debug.LogError("wrong type to pool!" + poolable.GetType() + " " + t);
                return;
            }

            ValidatePool(t);

            _poolables[t].PoolQueue.Enqueue(poolable);
        }

        public static MonoBehaviour GetFromPool(Type t)
        {
            ValidatePool(t);
            
            if (_poolables[t].PoolQueue.Count == 0)
                return null;

            var result = _poolables[t].PoolQueue.Dequeue();

            return result;
        }

        private static void ValidatePool(Type t)
        {
            if (!_poolables.ContainsKey(t))
                _poolables.Add(t, new Pool<MonoBehaviour>());
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