using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Core.Base;
using Gameplay.Core.Pickup;
using Gameplay.Core.Pickup.Base;
using Gameplay.Core.TargetTracking;
using Gameplay.Magic.Abilities.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Magic
{
    [RequireComponent(typeof(PickupComponent), typeof(TargetTrackingComponent))]
    public class MagicComponent : MonoComponent
    {
        [SerializeField] private Transform firePoint;
        
        private List<MagicAbility> projectilesPrefabs = new();

        public Action<MagicPickupable> MagicPickupableProvided;

        private TargetTrackingComponent _targetTrackingComponent;

        private IHoldableAbility _currentHoldableAbility;

        public override void Initialize()
        {
            GetComponent<PickupComponent>().PickedUp += OnPickupObtained;
            _targetTrackingComponent = GetComponent<TargetTrackingComponent>();
        }

        private void OnPickupObtained(IPickupable pickupable)
        {
            if (pickupable is not MagicPickupable magicPickupable) return;

            projectilesPrefabs.Add(magicPickupable.magicAbilityPrefab);
            MagicPickupableProvided?.Invoke(magicPickupable);
        }

        public void AddMagicAbilityPrefab(MagicAbility magicAbility) => projectilesPrefabs.Add(magicAbility);

        public void FireProjectile(Type projectileType)
        {
            var projectilePrefab = projectilesPrefabs.Find(a => a.GetType() == projectileType);

            if (!projectilePrefab)
            {
                Debug.LogError("there is no projectile prefab with such type TO FIRE! " + projectileType.Name);
                return;
            }

            var projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity, null);

            projectile.Activate();

            projectile.Emit(transform, _targetTrackingComponent.Target);
            
            TryReplaceHoldable(projectile);

            projectilesPrefabs.Remove(projectilePrefab);
        }

        private void TryReplaceHoldable(MagicAbility projectile)
        {
            _currentHoldableAbility?.Unhold();
            
            if (projectile is not IHoldableAbility holdableAbility)
                return;
            
            _currentHoldableAbility = holdableAbility;
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