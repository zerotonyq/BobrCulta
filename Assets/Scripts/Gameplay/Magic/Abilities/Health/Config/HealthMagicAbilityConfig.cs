using System;
using Gameplay.Magic.Abilities.Base.Config;
using UnityEngine;

namespace Gameplay.Magic.Abilities.Health.Config
{
    [CreateAssetMenu(menuName = "CreateAbilityConfig/" + nameof(HealthMagicAbilityConfig), fileName = nameof(HealthMagicAbilityConfig))]
    public class HealthMagicAbilityConfig : MagicAbilityConfig
    {
        public override Type GetAbilityType() => typeof(HealthMagicAbility);
    }
}