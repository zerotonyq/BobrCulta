using System;
using Gameplay.Magic.Abilities.Base.Config;
using UnityEngine;

namespace Gameplay.Magic.Abilities.Shield.Config
{
    [CreateAssetMenu(menuName = "CreateAbilityConfig/" + nameof(ShieldMagicAbilityConfig), fileName = nameof(ShieldMagicAbilityConfig))]
    public class ShieldMagicAbilityConfig : MagicAbilityConfig
    {
        public override Type GetAbilityType() => typeof(ShieldMagicAbility);
    }
}