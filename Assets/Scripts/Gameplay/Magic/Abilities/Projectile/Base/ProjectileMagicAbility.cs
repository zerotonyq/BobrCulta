using Gameplay.Core.Movement.Binders;
using Gameplay.Magic.Abilities.Base;
using UnityEngine;

namespace Gameplay.Magic.Abilities.Projectile.Base
{
    public abstract class ProjectileMagicAbility : MagicAbility
    {
        private TargetMovementBinderComponent _targetMovementBinder;

        protected Collider _emitterCollider;

        public override void Activate()
        {
            base.Activate();
            
            if (_targetMovementBinder != null)
                return;

            _targetMovementBinder = GetComponent<TargetMovementBinderComponent>();
        }

        public override void Emit(Transform emitter, Transform target)
        {
            base.Emit(emitter, target);

            _targetMovementBinder.Bind();

            _targetMovementBinder.SetTarget(target);
            
            _emitterCollider = emitter.GetComponent<Collider>();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _targetMovementBinder.Unbind();
            Destroy(gameObject);
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other == _emitterCollider)
            {
                Debug.Log("Same emitter");
                return;
            }

            if (other.transform.IsChildOf(_emitterCollider.transform))
                return;
        }
        
    }
}