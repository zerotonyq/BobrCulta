using System;
using Modifiers.Base;
using Modifiers.Velocity;
using UnityEngine;
using Utils.Initialize;

namespace Gameplay.Modifiers.Velocity.ClampVelocity
{
    [CreateAssetMenu(menuName = "CreateModifierConfig/" + nameof(ClampVelocityModifierConfig), fileName = nameof(ClampVelocityModifierConfig))]
    public class ClampVelocityModifierConfig : ModifierConfig
    {
        public float maxVelocity;
        public override Type InitializableType { get; } = typeof(ClampVelocityModifier);
    }
}