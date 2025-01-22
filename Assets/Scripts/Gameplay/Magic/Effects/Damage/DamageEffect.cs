using System;
using System.Collections;
using Gameplay.Core.Container;
using Gameplay.Core.Health;
using Gameplay.Magic.Effects.Base;
using UnityEngine;

namespace Gameplay.Magic.Effects.Damage
{
    public class DamageEffect : Effect
    {
        public override Coroutine CurrentCoroutine { get; set; }
        
        public override Action<Effect> EffectElapsed { get; set; }
        
        protected override void Execute(ComponentContainer component)
        {
            var c = component.Components.Find(a => a.GetType() == typeof(HealthComponent));
            
            (c as HealthComponent)?.ChangeHealth(-1);
            
            Debug.Log("DAMAGE");
        }

    }
}