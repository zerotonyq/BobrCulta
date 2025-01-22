using System;
using System.Collections;
using Gameplay.Core.Container;
using UnityEngine;
using Utils.Pooling;

namespace Gameplay.Magic.Effects.Base
{
    public abstract class Effect
    {
        public abstract Action<Effect> EffectElapsed { get; set; }

        public abstract Coroutine CurrentCoroutine { get; set; }

        private float _currentDuration;
        private float _duration;

        public void SetupDuration(EffectConfig.EffectDurationType durationType, float duration)
        {
            switch (durationType)
            {
                case EffectConfig.EffectDurationType.None:
                    Debug.LogError("duration type is none for effect " + GetType());
                    break;
                case EffectConfig.EffectDurationType.Instant:
                    _duration = 0;
                    break;
                case EffectConfig.EffectDurationType.Timed:
                    _duration = duration;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Reset() => _currentDuration = 0;

        public IEnumerator Apply(ComponentContainer component)
        {
            if (_duration <= 0f)
            {
                Execute(component);
                EffectElapsed?.Invoke(this);
                yield break;
            }

            var delay = 1;
            var instr = new WaitForSeconds(delay);
            while (_duration > _currentDuration)
            {
                Execute(component);

                float delta = Time.fixedDeltaTime;

                _currentDuration += delay;

                yield return instr;
            }

            EffectElapsed?.Invoke(this);
        }

        protected abstract void Execute(ComponentContainer component);
    }
}