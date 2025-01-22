using System;
using System.Collections.Generic;
using Gameplay.Core.Base;
using Gameplay.Core.Pickup;
using Gameplay.Core.Pickup.Base;
using Gameplay.Core.TargetTracking;
using Gameplay.Magic.Abilities.Base;
using Gameplay.Services.UI.Magic.Views;
using UnityEngine;
using Utils.Pooling;

namespace Gameplay.Magic.Abilities
{
    [RequireComponent(typeof(PickupComponent), typeof(TargetTrackingComponent))]
    public class AbilityEmitter : MonoComponent
    {
        [SerializeField] private Transform firePoint;

        private readonly List<MagicAbility> _projectilesPrefabs = new();

        public Action<MagicPickupable> MagicPickupableProvided;

        private TargetTrackingComponent _targetTrackingComponent;
        private PickupComponent _pickupComponent;

        public override void Initialize()
        {
            _pickupComponent = GetComponent<PickupComponent>();
            _targetTrackingComponent = GetComponent<TargetTrackingComponent>();
            
            _pickupComponent.PickedUp += OnPickupObtained;
        }

        private void OnPickupObtained(IPickupable pickupable)
        {
            if (pickupable is not MagicPickupable magicPickupable) return;

            _projectilesPrefabs.Add(magicPickupable.magicAbilityPrefab);
            MagicPickupableProvided?.Invoke(magicPickupable);
        }

        public void AddMagicAbilityPrefab(MagicAbility magicAbility) => _projectilesPrefabs.Add(magicAbility);

        public void EmitMagicAbility(MagicProjectilesUIView.MagicTypeArgs args)
        {
            var abilityPrefab = _projectilesPrefabs.Find(a => a.GetType() == args.MagicType);

            var ability = MagicAbility.Get(abilityPrefab);

            ability.transform.position = firePoint.position;
            
            ability.Activate(transform, _targetTrackingComponent.Target, args.ApplicationType);

            _projectilesPrefabs.Remove(abilityPrefab);
        }

        public void RemoveProjectile(MagicProjectilesUIView.MagicTypeArgs args)
        {
            var projectilePrefab = _projectilesPrefabs.Find(a => a.GetType() == args.MagicType);

            if (projectilePrefab == null)
            {
                Debug.LogError("there is no projectile prefab with such type TO REMOVE! " + args.MagicType.Name);
                return;
            }

            _projectilesPrefabs.Remove(projectilePrefab);
        }
    }
}