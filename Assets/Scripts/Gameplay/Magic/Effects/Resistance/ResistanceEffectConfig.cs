using Gameplay.Magic.Effects.Base;
using UnityEngine;

namespace Gameplay.Magic.Effects.Resistance
{
    [CreateAssetMenu(menuName = "CreateEffectConfig/" + nameof(ResistanceEffectConfig),
        fileName = nameof(ResistanceEffectConfig))]
    public class ResistanceEffectConfig : EffectConfig
    {
        public override Effect GetEffect() => new ResistanceEffect();
    }
}