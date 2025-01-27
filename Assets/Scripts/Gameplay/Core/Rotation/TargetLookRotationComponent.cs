using Gameplay.Core.Base;
using Gameplay.Core.TargetTracking;
using UnityEngine;

namespace Gameplay.Core.Rotation
{
    [RequireComponent(typeof(TargetTrackingComponent))]
    public class TargetLookRotationComponent : MonoComponent
    {
        private Transform _target;

        public override void Initialize()
        {
            GetComponent<TargetTrackingComponent>().TargetChanged += SetTarget;
            SetTarget(GetComponent<TargetTrackingComponent>().Target);
        }

        private void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Rotate()
        {
            var rotation = transform.rotation;

            if (_target)
                rotation = Quaternion.LookRotation(
                    new Vector3(_target.position.x - transform.position.x, 0,
                        _target.position.z - transform.position.z), Vector3.up);

            transform.rotation = rotation;
        }
    }
}