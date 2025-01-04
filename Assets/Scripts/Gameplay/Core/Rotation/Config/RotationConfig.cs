using System;
using System.Collections.Generic;
using Modifiers.Base;
using UnityEngine;

namespace Gameplay.Core.Rotation.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(RotationConfig), fileName = nameof(RotationConfig))]
    public class RotationConfig : Utils.Initialize.Config
    {
        public List<ModifierConfig> modifiersConfigs;
        public override Type InitializableType { get; } = typeof(RotationPresenter);
    }
}