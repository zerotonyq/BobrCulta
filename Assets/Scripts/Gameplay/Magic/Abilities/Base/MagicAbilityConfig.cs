using System;
using System.Collections.Generic;
using Gameplay.Magic.Effects.Base;
using UnityEngine;

namespace Gameplay.Magic.Abilities.Base
{
    public abstract class MagicAbilityConfig : ScriptableObject
    {
        public List<EffectConfig> effectConfigs = new();
        
        public List<MagicAbilityConfig> antagonistAbilities = new();

        public abstract Type GetAbilityType();
    }
}