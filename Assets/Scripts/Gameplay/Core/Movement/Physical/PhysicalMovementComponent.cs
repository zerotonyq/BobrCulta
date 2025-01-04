using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Core.Movement.Physical.Config;
using Modifiers.Velocity.Base;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Initialize;

namespace Gameplay.Core.Movement.Physical
{
    public class PhysicalMovementComponent : IInitializableConcreteConfig<PhysicsMovementConfig>
    {
        private Rigidbody _rigidbody;
        private float _accelerationForce;

        private List<VelocityModifier> _modifiers = new();

        public Task Initialize(PhysicsMovementConfig config)
        {
            _accelerationForce = config.accelerationForce;

            foreach (var modifierConfig in config.modifiersConfigs)
            {
                var modifier = Activator.CreateInstance(modifierConfig.InitializableType) as VelocityModifier;

                if (modifier == null)
                    Debug.LogError("Modifier is null");

                modifier?.Initialize(modifierConfig);

                _modifiers.Add(modifier);
            }

            return Task.CompletedTask;
        }

        public void SetRigidbody(Rigidbody rb) => _rigidbody = rb;

        public void AddAcceleration(Vector2 direction)
        {
            var transform = _rigidbody.transform;
            
            var resultVelocity =
                (transform.forward * direction.y +
                 transform.right * direction.x) * _accelerationForce;

            _rigidbody.AddForce(resultVelocity);
            
            foreach (var modifier in _modifiers)
            {
                _rigidbody.linearVelocity = modifier.Apply(_rigidbody.linearVelocity);
            }

        }

        public void Dispose()
        {
        }
    }
}