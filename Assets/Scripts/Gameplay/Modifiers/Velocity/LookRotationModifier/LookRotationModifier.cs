using Gameplay.Modifiers.Velocity.Base;
using Modifiers.Velocity.Base;
using UnityEngine;

namespace Gameplay.Modifiers.Velocity.CircleMovement
{
    
    public class LookRotationModifier : RotationModifier
    {
        private Transform _target;
        
        public override Quaternion Apply(Quaternion rotation, Vector3 position)
        {
            return !_target ? rotation : Quaternion.LookRotation(_target.position - position, Vector3.up);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

    }
}