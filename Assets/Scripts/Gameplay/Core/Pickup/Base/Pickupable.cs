using System;
using UnityEngine;
using Utils.Activate;
using Utils.Pooling;
using Utils.Reset;

namespace Gameplay.Core.Pickup.Base
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Pickupable : MonoBehaviour, IPickupable, IActivateable, IResetable
    {
        public Action<GameObject> Deactivated { get; set; }

        public virtual void Activate(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            Reset();
            Deactivated?.Invoke(gameObject);
            gameObject.SetActive(false);
            PoolManager.AddToPool(GetType(), gameObject);
        }

        public virtual void Reset()
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        public virtual void Pickup() => Deactivate();
    }
}