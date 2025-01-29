using System;
using System.Linq;
using Gameplay.Core.Base;
using Gameplay.Core.Container;
using Gameplay.Core.Pickup.Base;
using UnityEngine;

namespace Gameplay.Core.Pickup
{
    public class PickupComponent : MonoComponent
    {

        [SerializeField] private float scanRadius = 2f;

        public Action<IPickupable> PickedUp;
        
        public override void Initialize()
        {
            
        }
        
        public void Pickup()
        {
            var colliders = Physics.OverlapSphere(transform.position, scanRadius);
            
            if (colliders.Length == 0)
                return;

            var closestDistance = Vector3.Distance(transform.position, colliders[0].transform.position);

            IPickupable pickupable = null;
            
            foreach (var coll in colliders)
            {
                if(!coll.TryGetComponent(out IPickupable p))
                    continue;
                
                if(Vector3.Distance(coll.transform.position, transform.position) > closestDistance)
                    continue;
                
                closestDistance = Vector3.Distance(coll.transform.position, transform.position);
                pickupable = p;
            }

            if (pickupable == null)
                return;
            
            pickupable.Pickup(GetComponent<ComponentContainer>());
            PickedUp?.Invoke(pickupable);
        }
    }
}