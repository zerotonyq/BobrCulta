using UnityEngine;

namespace Gameplay.Magic.Projectiles.Base
{
    public abstract class MagicProjectile : MonoBehaviour
    {
        public virtual void Activate()
        {
            Debug.Log(GetType() + " activated");
        }

        public virtual void Deactivate()
        {
            Debug.Log(GetType() + " deactivated");
        }

        public virtual void Fire(Transform emitter, Transform target)
        {
            Debug.Log(GetType() + " fired");
        }
    }
}