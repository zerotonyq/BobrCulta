using System;
using System.Collections.Generic;
using Gameplay.Core.Base;
using Gameplay.Core.Pickup;
using Gameplay.Core.Pickup.Base;
using Gameplay.Core.TargetTracking;
using Gameplay.Magic.Abilities.Base;
using Gameplay.Magic.Abilities.Base.Pickupable;
using Gameplay.Services.UI.Gameplay.Magic.Views;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.Pooling;

namespace Gameplay.Magic.Abilities
{
    [RequireComponent(typeof(PickupComponent), typeof(TargetTrackingComponent))]
    public class MagicAbilityComponent : MonoComponent
    {
        [SerializeField] private Transform firePoint;

        [SerializeField] private List<MagicAbility> projectilesPrefabs = new();

        public Action<MagicPickupable> MagicPickupableProvided;

        private TargetTrackingComponent _targetTrackingComponent;
        private PickupComponent _pickupComponent;

        public override void Initialize()
        {
            _targetTrackingComponent = GetComponent<TargetTrackingComponent>();
            _pickupComponent = GetComponent<PickupComponent>();

            _pickupComponent.PickedUp += OnPickupObtained;
        }

        private void OnPickupObtained(IPickupable pickupable)
        {
            if (pickupable is not MagicPickupable magicPickupable) return;

            projectilesPrefabs.Add(magicPickupable.magicAbilityPrefab);
            MagicPickupableProvided?.Invoke(magicPickupable);
        }

        public void AddMagicAbilityPrefab(MagicAbility magicAbility) => projectilesPrefabs.Add(magicAbility);

        public void EmitMagicAbility(MagicProjectilesBarrel.MagicTypeArgs args)
        {
            var abilityPrefab = projectilesPrefabs.Find(a => a.GetType() == args.MagicType);

            var ability = PoolManager.GetFromPool(abilityPrefab.GetType(), abilityPrefab.gameObject)
                .GetComponent<MagicAbility>();

            ability.Initialize();

            ability.Activate(firePoint.position);

            ability.Use(transform, _targetTrackingComponent.Target, args.ApplicationType);

            projectilesPrefabs.Remove(abilityPrefab);
        }

        public void RemoveProjectile(MagicProjectilesBarrel.MagicTypeArgs args)
        {
            var projectilePrefab = projectilesPrefabs.Find(a => a.GetType() == args.MagicType);

            if (projectilePrefab == null)
            {
                Debug.LogError("there is no projectile prefab with such type TO REMOVE! " + args.MagicType.Name);
                return;
            }

            projectilesPrefabs.Remove(projectilePrefab);
        }

        public override void Reset()
        {
            projectilesPrefabs.Clear();
        }
    }
}