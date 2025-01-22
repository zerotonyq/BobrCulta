using Gameplay.Core.Base;
using Gameplay.Core.TargetTracking;
using UnityEngine;

namespace Gameplay.Core.Movement.Binders
{
    [RequireComponent(typeof(MovementComponent), typeof(TargetTrackingComponent))]
    public class TargetMovementBinderComponent : Binder
    {
        private TargetTrackingComponent _targetTracker;
        private MovementComponent _movement;
        
        public override void Bind()
        {
            _movement = GetComponent<MovementComponent>();
             _targetTracker = GetComponent<TargetTrackingComponent>();
            
            _targetTracker.Initialize();
            _movement.Initialize();
        }
        
        private void Update()
        {
            if (!_targetTracker.Target) return;   
            _movement.AddAcceleration(_targetTracker.Target.position - transform.position);
        }

        public void SetTarget(Transform target) => GetComponent<TargetTrackingComponent>().SetTarget(target);

    }
}