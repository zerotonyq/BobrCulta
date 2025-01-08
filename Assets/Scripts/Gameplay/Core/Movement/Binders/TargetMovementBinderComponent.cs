using Gameplay.Core.Base;
using Gameplay.Core.TargetTracking;
using R3;
using UnityEngine;

namespace Gameplay.Core.Movement.Binders
{
    [RequireComponent(typeof(MovementComponent), typeof(TargetTrackingComponent))]
    public class TargetMovementBinderComponent : Binder
    {

        private DisposableBag _disposableBag;
        public override void Bind()
        {
            var movement = GetComponent<MovementComponent>();
            var targetTracker = GetComponent<TargetTrackingComponent>();
            
            targetTracker.Initialize();
            movement.Initialize();
            
            var sub = Observable.EveryUpdate(UnityFrameProvider.Update).Subscribe(_ =>
            {
                if (targetTracker.Target == null) return;   
                movement.AddAcceleration(targetTracker.Target.position - transform.position);
            });
            
            _disposableBag.Add(sub);
        }

        public void Unbind() => _disposableBag.Dispose();

        public void OnDestroy() => _disposableBag.Dispose();

        public void SetTarget(Transform target) => GetComponent<TargetTrackingComponent>().SetTarget(target);
    }
}