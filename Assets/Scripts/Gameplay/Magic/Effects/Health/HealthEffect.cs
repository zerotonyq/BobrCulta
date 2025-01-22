using System;
using Gameplay.Core.Container;
using Gameplay.Core.Health;
using Gameplay.Magic.Effects.Base;
using UnityEngine;

namespace Gameplay.Magic.Effects.Health
{
    public class HealthEffect : Effect
    {
        public override Action<Effect> EffectElapsed { get; set; }
        public override Coroutine CurrentCoroutine { get; set; }
        protected override void Execute(ComponentContainer component)
        {
            component.GetComponent<HealthComponent>().ChangeHealth(1);
        }
    }
}