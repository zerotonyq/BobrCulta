﻿using System;
using System.Collections.Generic;
using Gameplay.Magic;
using Gameplay.Magic.Abilities.Base.Pickupable;
using Gameplay.Services.LevelActivity.Base;
using Gameplay.Services.UI.Magic.Enum;
using Signals.Activities;
using Signals.Activities.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Gameplay.Services.Boss.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(BossDifficultyConfig),
        fileName = nameof(BossDifficultyConfig))]
    public class BossDifficultyConfig : ActivityConfig
    {
        public List<BossSection> sections = new();

        public override ActivitySignal ConstructSignal() => new BossActivitySignal();

        [Serializable]
        public struct BossSection
        {
            public List<BossConfig> configs;
            public List<AssetReferenceGameObject> bossReferences;

            [Space, Header("AUTO GENERATION")] 
            public bool autoGenerated;

            public int additionallyGeneratedCount;

            public float minInterval;
            public float maxInterval;

            public int minIntervalCount;
            public int maxIntervalCount;

            public void Generate(List<MagicPickupable> allowed)
            {
                if (!autoGenerated)
                    return;

                for (int i = 0; i < additionallyGeneratedCount; i++)
                {
                    var intervalCount = Random.Range(minIntervalCount, maxIntervalCount + 1);

                    var intervals = new List<BossConfig.AbilityInterval>();

                    for (int j = 0; j < intervalCount; j++)
                    {
                        var pickupable = allowed[Random.Range(0, allowed.Count)];
                        intervals.Add(new BossConfig.AbilityInterval()
                        {
                            applicationType = pickupable.primaryApplicationType,
                            beforeInterval = Random.Range(minInterval, maxInterval + 1),
                            pickupablePrefab = pickupable
                        });
                    }

                    var config = new BossConfig
                    {
                        abilityIntervals = intervals,
                        bossReference = bossReferences[Random.Range(0, bossReferences.Count)]
                    };

                    configs.Add(config);
                }
            }
        }

        [Serializable]
        public struct BossConfig
        {
            public AssetReferenceGameObject bossReference;
            public List<AbilityInterval> abilityIntervals;

            [Serializable]
            public struct AbilityInterval
            {
                public MagicPickupable pickupablePrefab;
                public ApplicationType applicationType;
                public float beforeInterval;
            }
        }
    }
}