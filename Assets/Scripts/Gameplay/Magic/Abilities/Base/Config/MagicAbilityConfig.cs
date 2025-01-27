using System;
using System.Collections.Generic;
using Gameplay.Magic.Effects.Base;
using Gameplay.Services.UI.Gameplay.Magic.Enum;
using UnityEngine;

namespace Gameplay.Magic.Abilities.Base.Config
{
    public abstract class MagicAbilityConfig : ScriptableObject
    {
        public List<EffectConfig> effectConfigs = new();
        
        public List<MagicAbilityConfig> antagonistAbilities = new();

        public ApplicationType primaryApplicationType;

        public MagicAbility abilityPrefab;

        public abstract Type GetAbilityType();
    }
}