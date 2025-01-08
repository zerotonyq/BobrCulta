using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Base;
using Gameplay.Core.Pickup;
using Gameplay.Core.Pickup.Base;
using Gameplay.Core.TargetTracking;
using Gameplay.Magic.Pickupables.Base;
using Gameplay.Magic.Projectiles.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Magic
{
    [RequireComponent(typeof(PickupComponent), typeof(TargetTrackingComponent))]
    public class MagicComponent : MonoComponent
    {
        [SerializeField] private List<MagicProjectile> projectilesPrefabs = new();

        public Action<MagicPickupable> MagicPickupableProvided;

        private TargetTrackingComponent _targetTrackingComponent;
        public override void Initialize()
        {
            GetComponent<PickupComponent>().PickedUp += OnPickupObtained;
            _targetTrackingComponent = GetComponent<TargetTrackingComponent>();
        }

        public void OnPickupObtained(IPickupable pickupable)
        {
            if (pickupable is not MagicPickupable magicPickupable) return;
            
            projectilesPrefabs.Add(magicPickupable.MagicProjectilePrefab);
            MagicPickupableProvided?.Invoke(magicPickupable);
        }

        public void FireProjectile(Type projectileType)
        {
            var projectilePrefab = projectilesPrefabs.Find(a => a.GetType() == projectileType);
            
            if (projectilePrefab == null)
            {
                Debug.LogError("there is no projectile prefab with such type TO FIRE! " + projectileType.Name);
                return;
            }

            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, null);
            
            projectile.Activate();
            
            projectile.Fire(transform, _targetTrackingComponent.Target);

            projectilesPrefabs.Remove(projectilePrefab);
        }

        public void RemoveProjectile(Type projectileType)
        {
            var projectilePrefab = projectilesPrefabs.Find(a => a.GetType() == projectileType);
            
            if (projectilePrefab == null)
            {
                Debug.LogError("there is no projectile prefab with such type TO REMOVE! " + projectileType.Name);
                return;
            }

            projectilesPrefabs.Remove(projectilePrefab);
        }
    }
}