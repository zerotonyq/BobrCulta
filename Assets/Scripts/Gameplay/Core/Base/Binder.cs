using R3;
using UnityEngine;

namespace Gameplay.Core.Base
{
    public abstract class Binder : MonoBehaviour
    {
        protected DisposableBag _disposableBag;
        
        public abstract void Bind();

        private void OnDestroy() => _disposableBag.Dispose();
    }
}