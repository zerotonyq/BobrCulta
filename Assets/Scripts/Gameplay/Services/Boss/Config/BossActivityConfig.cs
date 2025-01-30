using System;
using System.Collections.Generic;
using Gameplay.Magic.Abilities.Base.Pickupable;
using Gameplay.Services.Activity.Base;
using Signals;
using Signals.Activities.Base;
using Signals.Activities.Boss;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace Gameplay.Services.Boss.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(BossActivityConfig),
        fileName = nameof(BossActivityConfig))]
    public class BossActivityConfig : ActivityConfig
    {
        public List<BossSection> sections = new();

        public override IActivityRequest ConstructRequest(TreeLevelChangedSignal signal)
        {
            return new BossActivityRequest { TreeLevelChangedSignal = signal };
        }

        [Serializable]
        public struct BossSection
        {
            public List<BossConfig> Configs { get; private set; }
            
            public List<MagicPickupable> magicPickupables;
            public List<AssetReferenceGameObject> bossReferences;

            public int additionallyGeneratedCount;

            public float minInterval;
            public float maxInterval;

            public int minIntervalCount;
            public int maxIntervalCount;

            public BossSection(BossSection bossSection)
            {
                Configs = new List<BossConfig>(bossSection.Configs);
                magicPickupables = bossSection.magicPickupables;
                bossReferences = bossSection.bossReferences;
                additionallyGeneratedCount = bossSection.additionallyGeneratedCount;
                minInterval = bossSection.minInterval;
                maxInterval = bossSection.maxInterval;
                minIntervalCount = bossSection.minIntervalCount;
                maxIntervalCount = bossSection.maxIntervalCount;
            }


            public void Generate()
            {
                Configs = new();
                
                for (int i = 0; i < additionallyGeneratedCount; i++)
                {
                    var intervalCount = Random.Range(minIntervalCount, maxIntervalCount + 1);

                    var intervals = new List<BossConfig.AbilityInterval>();

                    for (int j = 0; j < intervalCount; j++)
                    {
                        var abilityConfig = magicPickupables[Random.Range(0, magicPickupables.Count)];

                        intervals.Add(new BossConfig.AbilityInterval()
                        {
                            beforeInterval = Random.Range(minInterval, maxInterval + 1),
                            pickupable = abilityConfig
                        });
                    }

                    var config = new BossConfig(bossReferences[Random.Range(0, bossReferences.Count)], intervals);

                    Configs.Add(config);
                }
            }
        }

        [Serializable]
        public struct BossConfig
        {
            public  AssetReferenceGameObject BossReference { get; private set; }
            public List<AbilityInterval> AbilityIntervals { get; private set; }

            public BossConfig(AssetReferenceGameObject bossReference, List<AbilityInterval> intervals)
            {
                BossReference = bossReference;
                AbilityIntervals = intervals;
            }

            [Serializable]
            public struct AbilityInterval
            {
                public MagicPickupable pickupable;
                public float beforeInterval;
            }
        }
    }
}