using System;
using Modifiers.Base;
using UnityEngine;
using Utils.Initialize;

namespace Gameplay.Modifiers.Velocity.CircleMovement
{
    [CreateAssetMenu(menuName = "CreateModifierConfig/" + nameof(LookRotationModifierConfig), fileName = nameof(LookRotationModifierConfig))]
    public class LookRotationModifierConfig : ModifierConfig
    {
        public override Type InitializableType { get; } = typeof(LookRotationModifier);
    }
}