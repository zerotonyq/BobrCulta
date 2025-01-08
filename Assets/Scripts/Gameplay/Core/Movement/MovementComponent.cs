using Gameplay.Core.Base;
using UnityEngine;

namespace Gameplay.Core.Movement
{
    public class MovementComponent : MonoComponent
    {
        private Rigidbody _rigidbody;

        [SerializeField] private float accelerationForce;
        [SerializeField] private float maxVelocity;

        public override void Initialize() => _rigidbody = GetComponent<Rigidbody>();

        public void AddAcceleration(Vector2 direction)
        {
            var tr = _rigidbody.transform;

            var resultVelocity =
                (tr.forward * direction.y +
                 tr.right * direction.x) * accelerationForce;

            _rigidbody.AddForce(resultVelocity);

            _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity, maxVelocity);
        }
    }
}