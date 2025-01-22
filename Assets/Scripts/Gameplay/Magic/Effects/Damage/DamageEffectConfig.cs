using Gameplay.Magic.Effects.Base;
using UnityEngine;
using Utils.Pooling;

namespace Gameplay.Magic.Effects.Damage
{
    [CreateAssetMenu(menuName = "CreateEffectConfig/" + nameof(DamageEffectConfig),
        fileName = nameof(DamageEffectConfig))]
    public class DamageEffectConfig : EffectConfig
    {
        public override Effect GetEffect() => new DamageEffect();
    }
}