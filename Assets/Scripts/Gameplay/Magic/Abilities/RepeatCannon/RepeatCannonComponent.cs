using System.Collections.Generic;
using Gameplay.Core.Base;
using Gameplay.Magic.Abilities.Base.Pickupable;
using Gameplay.Services.Boss;
using Gameplay.Services.Boss.Config;
using UnityEngine;

namespace Gameplay.Magic.Abilities.RepeatCannon
{
    [RequireComponent(typeof(MagicAbilityComponent))]
    public class RepeatCannonComponent : MonoComponent
    {
        [SerializeField] private float defaultInterval = 3f;

        [SerializeField] private List<MagicPickupable> allowedPickupablesToRepeat = new();

        public override void Initialize()
        {
            var lastMagicTypeArgs = GetComponent<MagicAbilityComponent>().LastMagicTypeArgs;
            
            if (lastMagicTypeArgs.Pickupable == null)
                return;

            if (!allowedPickupablesToRepeat.Exists(a =>
                    a.GetType() == (lastMagicTypeArgs.Pickupable as MagicPickupable)?.GetType()))
                return;

            GetComponent<AutomationMagicComponentBinder>().SetAbilityIntervals(
                new List<BossActivityConfig.BossConfig.AbilityInterval>()
                {
                    new()
                    {
                        beforeInterval = defaultInterval, pickupable = lastMagicTypeArgs.Pickupable as MagicPickupable
                    }
                });
            
        }
    }
}