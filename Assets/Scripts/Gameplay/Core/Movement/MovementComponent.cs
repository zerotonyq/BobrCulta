using System;
using Gameplay.Core.Base;
using UnityEngine;
using Utils.Reset;

namespace Gameplay.Core.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementComponent : MonoComponent
    {
        private Rigidbody _rigidbody;

        [SerializeField] private float accelerationForce;
        [SerializeField] private float maxHorizontalVelocity;
        [SerializeField] private float maxVerticalVelocity;

        public override void Initialize() => _rigidbody = GetComponent<Rigidbody>();

        public void AddAcceleration(Vector2 direction)
        {
            var tr = _rigidbody.transform;

            var resultVelocity =
                (tr.forward * direction.y +
                 tr.right * direction.x) * accelerationForce;

            _rigidbody.AddForce(resultVelocity);

            ClampVelocity();
            
        }

        public void AddAcceleration(Vector3 direction)
        {
            var resultVelocity = accelerationForce * direction.normalized;

            _rigidbody.AddForce(resultVelocity);

            ClampVelocity();
        }
        
        private void ClampVelocity()
        {
            _rigidbody.linearVelocity =
                Vector3.ClampMagnitude(new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z),
                    maxHorizontalVelocity) + new Vector3(0,
                    Mathf.Clamp(_rigidbody.linearVelocity.y, -maxVerticalVelocity, maxVerticalVelocity), 0);
        }

        public override void Reset()
        {
            if (_rigidbody.isKinematic)
                return;

            _rigidbody.linearVelocity = Vector3.zero;
        }
    }
}