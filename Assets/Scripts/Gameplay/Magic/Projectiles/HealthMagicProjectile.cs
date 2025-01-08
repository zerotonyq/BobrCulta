using Gameplay.Core.ComponentContainer;
using Gameplay.Core.Health;
using Gameplay.Magic.Projectiles.Base;
using UnityEngine;

namespace Gameplay.Magic.Projectiles
{
    public class HealthMagicProjectile : MagicProjectile
    {
        public override void Fire(Transform emitter, Transform target)
        {
            base.Fire(emitter, target);

            if (!emitter.TryGetComponent(out HealthComponent healthComponent))
            {
                Debug.LogError("There is no health component on emmiter");
                return;
            }
            
            healthComponent.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}