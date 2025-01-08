using Gameplay.Core.Base;
using Gameplay.Core.TargetTracking;
using UnityEngine;

namespace Gameplay.Core.Rotation
{
    public class TargetLookRotationComponent : MonoComponent
    {
       private Transform _target;

       
       public override void Initialize()
       {
           if (!TryGetComponent(out TargetTrackingComponent component))
               return;


           component.TargetChanged += SetTarget;
       }
       
        public void SetTarget(Transform target)
        {
            Debug.Log(target);
            _target = target;
        }

        public void Rotate()
        {
            var rotation = transform.rotation;

            if (_target)
                rotation = Quaternion.LookRotation(
                    new Vector3(_target.position.x - transform.position.x, 0, _target.position.z - transform.position.z), Vector3.up);

            transform.rotation = rotation;
        }

       
    }
}