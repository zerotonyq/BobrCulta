using UnityEngine;

namespace Gameplay.Magic.Effects.Base
{
    public abstract class EffectConfig : ScriptableObject
    {
        public EffectDurationType durationType = EffectDurationType.Instant;

        public float duration = 1f;

        public abstract Effect GetEffect();

        public enum EffectDurationType
        {
            None,
            Instant,
            Timed
        }
    }
}