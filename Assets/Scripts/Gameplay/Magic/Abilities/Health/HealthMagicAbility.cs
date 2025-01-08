using Gameplay.Core.Health;
using Gameplay.Magic.Abilities.Base;
using UnityEngine;

namespace Gameplay.Magic.Abilities.Health
{
    public class HealthMagicAbility : MagicAbility
    {
        [SerializeField] private int addition = 1;
        
        public override void Emit(Transform emitter, Transform target)
        {
            base.Emit(emitter, target);

            if (!emitter.TryGetComponent(out HealthComponent healthComponent))
            {
                Debug.LogWarning("there is no health component on emitter");
                return;
            }
            
            healthComponent.ChangeHealth(addition);
            
            Destroy(gameObject);
        }
    }
}