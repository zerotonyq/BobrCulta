using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Initialize;

namespace Utils.Disposing
{
    public class DisposeManager : MonoBehaviour
    {
        private List<IDisposable> _disposables = new();
        
        public void Initialize(List<IDisposable> objects) => _disposables = objects;

        private void OnDestroy()
        {
            foreach (var disposable in _disposables) disposable.Dispose();
        }

        public void Dispose() => OnDestroy();
    }
}