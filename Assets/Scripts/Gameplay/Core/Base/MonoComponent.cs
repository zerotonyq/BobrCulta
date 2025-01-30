using UnityEngine;
using Utils.Reset;

namespace Gameplay.Core.Base
{
    public abstract class MonoComponent : MonoBehaviour, IResetable
    {
        public virtual void Initialize(){}

        public virtual void Reset() {}
    }
}