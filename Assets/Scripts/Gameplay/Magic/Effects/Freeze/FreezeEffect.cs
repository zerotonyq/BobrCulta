using System;
using Gameplay.Core.Container;
using Gameplay.Magic.Abilities;
using Gameplay.Magic.Effects.Base;
using UnityEngine;

namespace Gameplay.Magic.Effects.Freeze
{
    public class FreezeEffect : Effect
    {
        public override Action<Effect> EffectElapsed { get; set; }
        public override Coroutine CurrentCoroutine { get; set; }

        private MagicAbilityComponent _magicAbilityComponent;

        protected override void Execute(ComponentContainer component)
        {
            _magicAbilityComponent =
                component.Components.Find(a => a.GetType() == typeof(MagicAbilityComponent)) as MagicAbilityComponent;

            if (!_magicAbilityComponent)
                return;

            _magicAbilityComponent.AllowEmit = false;
        }

        protected override void OnStopped()
        {
            _magicAbilityComponent.AllowEmit = true;
            _magicAbilityComponent = null;
        }
    }
}