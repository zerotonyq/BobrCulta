using System;
using System.Collections.Generic;
using Gameplay.Character;
using UnityEngine;

namespace Gameplay.Player.Config
{
    [CreateAssetMenu(menuName = ConfigMenuName + nameof(CharacterConfig), fileName = nameof(CharacterConfig))]
    public class CharacterConfig : Utils.Initialize.Config
    {
        public override Type InitializableType { get; } = typeof(CharacterComponent);
        
        public List<Utils.Initialize.Config> configs = new();
    }
}