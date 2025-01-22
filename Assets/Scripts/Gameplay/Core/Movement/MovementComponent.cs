using System;
using Gameplay.Core.Base;
using UnityEngine;
using Utils.Reset;

namespace Gameplay.Core.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementComponent : MonoComponent, IResetable
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
            
            _rigidbody.linearVelocity =
                Vector3.ClampMagnitude(new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z),
                    maxVelocity) + new Vector3(0, Mathf.Clamp(_rigidbody.linearVelocity.y, -maxVelocity, maxVelocity), 0);
        }

        public void AddAcceleration(Vector3 direction)
        {
            var resultVelocity = accelerationForce * direction.normalized;

            _rigidbody.AddForce(resultVelocity);

            _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity, maxVelocity);
        }

        public void Reset() => _rigidbody.linearVelocity = Vector3.zero;
    }
}