using UnityEngine;

namespace Gameplay.Magic.Abilities.Base
{
    public abstract class MagicAbility : MonoBehaviour
    {
        protected bool _isActivated;
        public virtual void Activate()
        {
            _isActivated = true;
            Debug.Log(GetType() + " activated");
        }

        public virtual void Deactivate()
        {
            _isActivated = false;
            Debug.Log(GetType() + " deactivated");
        }

        public virtual void Emit(Transform emitter, Transform target)
        {
            Debug.Log(GetType() + " fired");
        }
    }
}