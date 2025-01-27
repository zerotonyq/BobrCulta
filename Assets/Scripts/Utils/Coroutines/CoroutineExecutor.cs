using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Coroutines
{
    public class CoroutineExecutor : MonoBehaviour
    {
        private List<Coroutine> _coroutines = new();

        public Coroutine Add(IEnumerator routine)
        {
            var coroutine = StartCoroutine(routine);

            _coroutines.Add(coroutine);

            return coroutine;
        }

        public void Remove(Coroutine coroutine)
        {
            if (_coroutines.Contains(coroutine))
                StopCoroutine(coroutine);

            _coroutines.Remove(coroutine);
        }
    }
}