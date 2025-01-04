using System;
using System.Collections.Generic;
using Modifiers.Base;
using Modifiers.Velocity.Base;
using UnityEngine;

namespace Gameplay.Core.Movement.Physical.Config
{
    [CreateAssetMenu(menuName = ConfigMenuName + nameof(PhysicsMovementConfig), fileName = nameof(PhysicsMovementConfig))]
    public class PhysicsMovementConfig : Utils.Initialize.Config
    {
        public float accelerationForce;

        public List<ModifierConfig> modifiersConfigs;
        public override Type InitializableType { get; } = typeof(MovementPresenter);
    }
}