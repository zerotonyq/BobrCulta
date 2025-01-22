using Gameplay.Magic.Effects.Base;
using UnityEngine;

namespace Gameplay.Magic.Effects.Health
{
    [CreateAssetMenu(menuName = "CreateEffectConfig/" + nameof(HealthEffectConfig), fileName = nameof(HealthEffectConfig))]
    public class HealthEffectConfig : EffectConfig
    {
        public override Effect GetEffect() => new HealthEffect();
    }
}