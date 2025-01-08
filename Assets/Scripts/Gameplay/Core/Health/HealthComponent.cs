using Gameplay.Core.Base;
using UnityEngine;

namespace Gameplay.Core.Health
{
    public class HealthComponent : MonoComponent
    {
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int health;
        
        public override void Initialize()
        {
            health = maxHealth;
        }
        
        public void ChangeHealth(int count)
        {
            if (health + count > maxHealth)
            {
                health = maxHealth;
                return;
            }

            if (health + count < 0)
            {
                health = 0;
                return;
            }

            health += count;
        }
        
    }
}