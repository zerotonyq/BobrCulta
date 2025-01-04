using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Gameplay.Boss.Location.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(BossServiceConfig), fileName = nameof(BossServiceConfig))]
    public class BossServiceConfig : Utils.Initialize.Config
    {
        public override Type InitializableType { get; }

        public List<AssetReferenceGameObject> bossesReferences;
    }
}