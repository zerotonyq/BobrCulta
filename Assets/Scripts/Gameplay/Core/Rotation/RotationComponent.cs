using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gameplay.Core.Rotation.Config;
using Gameplay.Modifiers.Velocity.Base;
using Modifiers.Velocity.Base;
using UnityEngine;
using Utils.Initialize;

namespace Gameplay.Core.Rotation
{
    public class RotationComponent : IInitializableConcreteConfig<RotationConfig>
    {
     
        private List<RotationModifier> _modifiers = new();
        private Transform _transform;

        public Task Initialize(RotationConfig config)
        {
            foreach (var modifierConfig in config.modifiersConfigs)
            {
                var modifier = Activator.CreateInstance(modifierConfig.InitializableType) as RotationModifier;
                
                if(modifier == null)
                    Debug.LogError("Modifier is null");
                
                modifier?.Initialize(modifierConfig);
                
                _modifiers.Add(modifier);
            }

            return Task.CompletedTask;
        }

        public void SetTransform(Transform t) => _transform = t;

        public void RotateTowardsTarget()
        {
            var rotation = _transform.rotation;
            foreach (var modifier in _modifiers)
            {
                rotation = modifier.Apply(rotation, _transform.position);
            }

            _transform.rotation = rotation;
        }
    }
}