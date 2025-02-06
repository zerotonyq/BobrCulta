using Gameplay.Magic.Effects.Base;
using UnityEngine;

namespace Gameplay.Magic.Effects.Freeze
{
    [CreateAssetMenu(menuName = "CreateEffectConfig/" + nameof(FreezeEffectConfig),
        fileName = nameof(FreezeEffectConfig))]
    public class FreezeEffectConfig : EffectConfig
    {
        public override Effect GetEffect() => new FreezeEffect();
    }
}