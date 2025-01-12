using R3;
using UnityEngine;

namespace Gameplay.Core.Base
{
    public abstract class Binder : MonoBehaviour
    {
        protected DisposableBag DisposableBag;
        
        public abstract void Bind();

        private void OnDestroy() => DisposableBag.Dispose();
    }
}