using System.Threading.Tasks;
using Gameplay.Core.Movement;
using Gameplay.Modifiers.Velocity.ClampVelocity;
using Modifiers.Base;
using Modifiers.Velocity.Base;
using UnityEngine;

namespace Modifiers.Velocity
{
    public class ClampVelocityModifier : VelocityModifier
    {
        public float maxVelocity;
        public override Vector3 Apply(Vector3 velocity)
        {
            return Vector3.ClampMagnitude(velocity, maxVelocity);
        }

        public override Task Initialize(ModifierConfig config)
        {
            base.Initialize(config);
            maxVelocity = (config as ClampVelocityModifierConfig).maxVelocity;
            return Task.CompletedTask;
        }
    }
}