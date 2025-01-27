using System;
using Gameplay.Magic.Abilities.Base.Config;
using UnityEngine;

namespace Gameplay.Magic.Abilities.Bullet.Config
{
    [CreateAssetMenu(menuName = "CreateAbilityConfig/" + nameof(BulletMagicAbilityConfig), fileName = nameof(BulletMagicAbilityConfig))]
    public class BulletMagicAbilityConfig : MagicAbilityConfig
    {
        public override Type GetAbilityType() => typeof(BulletMagicAbility);
    }
}