﻿using Gameplay.Core.Base;
using Gameplay.Core.Pickup;
using Gameplay.Core.TargetTracking;
using Gameplay.Magic.Abilities.Base;
using Gameplay.Magic.Abilities.Base.Pickupable;
using Gameplay.Magic.Barrel;
using UnityEngine;
using Utils.Pooling;

namespace Gameplay.Magic.Abilities
{
    [RequireComponent(typeof(TargetTrackingComponent))]
    public class MagicAbilityComponent : MonoComponent
    {
        [SerializeField] private Transform firePoint;

        private TargetTrackingComponent _targetTrackingComponent;
        
        public MagicPickupableBarrelComponent.MagicTypeArgs LastMagicTypeArgs { get; private set; }

        public bool AllowEmit { get; set; } = true;

        public override void Initialize() => _targetTrackingComponent = GetComponent<TargetTrackingComponent>();

        public void EmitMagicAbility(MagicPickupableBarrelComponent.MagicTypeArgs args)
        {
            if (!AllowEmit)
                return;

            LastMagicTypeArgs = args;
            
            if (args.Pickupable is not MagicPickupable magicPickupable) return;
            
            var ability = PoolManager.GetFromPool(magicPickupable.magicAbilityPrefab.GetType(), magicPickupable.magicAbilityPrefab.gameObject)
                .GetComponent<MagicAbility>();

            ability.Initialize();

            ability.Activate(firePoint.position);

            ability.Use(transform, _targetTrackingComponent.Target, args.ApplicationType);
        }
    }
}