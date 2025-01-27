using System;
using Gameplay.Core.Base;
using Gameplay.Core.Health;
using UnityEngine;

namespace Gameplay.Core.Character
{
    [RequireComponent(typeof(HealthComponent))]
    public class CharacterComponent : MonoComponent
    {
        public override void Initialize() => GetComponent<HealthComponent>().Dead += Die;

        public Action Dead { get; set; }
        public void Die() => Dead?.Invoke();
    }
}