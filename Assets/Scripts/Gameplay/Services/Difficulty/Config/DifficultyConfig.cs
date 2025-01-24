using System;
using System.Collections.Generic;
using Gameplay.Magic.Abilities.Base.Pickupable;
using UnityEngine;

namespace Gameplay.Services.Difficulty.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(DifficultyConfig), fileName = nameof(DifficultyConfig))]
    public class DifficultyConfig : ScriptableObject
    {
        public List<DifficultySection> sections = new();
        
        [Serializable]
        public struct DifficultySection
        {
            public const int MaxDifficulty = 10;
            
            [Range(0, MaxDifficulty)] public int difficulty;
            
            public List<MagicPickupable> allowedPickupables;
        }
    }
}