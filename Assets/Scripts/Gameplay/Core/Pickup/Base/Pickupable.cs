using UnityEngine;
using Utils.Activate;
using Utils.Pooling;
using Utils.Reset;

namespace Gameplay.Core.Pickup.Base
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Pickupable : MonoBehaviour, IPickupable, IActivateable, IResetable
    {
        
        public void Activate(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            Reset();
            gameObject.SetActive(false);
            PoolManager.AddToPool(GetType(), gameObject);
        }

        public abstract void Reset();

        public virtual void Pickup() => Deactivate();
    }
}