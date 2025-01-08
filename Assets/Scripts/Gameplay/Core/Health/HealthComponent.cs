using Gameplay.Core.Base;
using UnityEngine;

namespace Gameplay.Core.Health
{
    public class HealthComponent : MonoComponent
    {
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int health;

        private bool _allowDecreaseHealth = true;
        
        public override void Initialize()
        {
            health = maxHealth;
        }

        public void AllowDecreaseHealth(bool i) => _allowDecreaseHealth = i;

        public void ChangeHealth(int count)
        {
            Debug.Log(count);
            if (!_allowDecreaseHealth && count < 0)
            {
                Debug.Log("disalowed to decrease" );
                return;
            }
            
            if (health + count > maxHealth)
            {
                health = maxHealth;
                return;
            }

            if (health + count < 0)
            {
                health = 0;
                Debug.Log("DEFEATED " + name);
                return;
            }

            health += count;
        }
    }
}