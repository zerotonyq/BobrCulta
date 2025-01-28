using System;
using Gameplay.Core.Base;
using Gameplay.Core.Container;
using UnityEngine;

namespace Gameplay.Core.Health
{
    public class HealthComponent : MonoComponent
    {
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int health;

        public Action HealthDecreased;
        public Action Dead;

        public override void Initialize() => Reset();

        public void ChangeHealth(int count)
        {
            if (health + count > maxHealth)
            {
                health = maxHealth;
                return;
            }
            
            if(count < 0)
                HealthDecreased?.Invoke();
            
            if (health + count <= 0)
            {
                health = 0;
                Dead?.Invoke();
                return;
            }
            
            health += count;
        }

        public override void Reset() => health = maxHealth;
    }
}