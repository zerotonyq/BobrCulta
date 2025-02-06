using System;
using Gameplay.Core.Container;
using Gameplay.Core.Health;
using Gameplay.Magic.Effects.Base;
using UnityEngine;

namespace Gameplay.Magic.Effects.Damage
{
    public class DamageEffect : Effect
    {
        private readonly int _damage;

        public DamageEffect(int damage) => _damage = damage;

        public override Coroutine CurrentCoroutine { get; set; }
        
        public override Action<Effect> EffectElapsed { get; set; }
        
        protected override void Execute(ComponentContainer component)
        {
            var c = component.Components.Find(a => a.GetType() == typeof(HealthComponent));
            
            (c as HealthComponent)?.ChangeHealth(-_damage);
        }

    }
}