using Gameplay.Magic.Effects.Base;
using UnityEngine;
using Utils.Pooling;

namespace Gameplay.Magic.Effects.Damage
{
    [CreateAssetMenu(menuName = "CreateEffectConfig/" + nameof(DamageEffectConfig),
        fileName = nameof(DamageEffectConfig))]
    public class DamageEffectConfig : EffectConfig
    {
        [Range(1, 1000)]
        public int Damage = 1;
        public override Effect GetEffect() => new DamageEffect(Damage);
    }
}