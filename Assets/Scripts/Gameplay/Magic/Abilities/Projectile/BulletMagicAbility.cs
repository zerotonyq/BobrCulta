using Gameplay.Core.Health;
using Gameplay.Core.Movement.Binders;
using Gameplay.Magic.Abilities.Projectile.Base;
using Gameplay.Magic.Abilities.Shield;
using UnityEngine;

namespace Gameplay.Magic.Abilities.Projectile
{
    [RequireComponent(typeof(TargetMovementBinderComponent))]
    public class BulletMagicAbility : ProjectileMagicAbility
    {
        [SerializeField] private int damage = 1;

        public override void OnTriggerEnter(Collider other)
        {
            if (other == _emitterCollider)
            {
                Debug.Log("Same emitter");
                return;
            }

            if (other.transform.IsChildOf(_emitterCollider.transform))
                return;
            
            if (other.TryGetComponent(out ShieldMagicAbility shieldMagicAbility))
            {
                shieldMagicAbility.Unhold();
                Deactivate();
                return;
            }
            
            if (other.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.ChangeHealth(-damage);
                Deactivate();
                return;
            }
            
            Deactivate();
        }
    }
}