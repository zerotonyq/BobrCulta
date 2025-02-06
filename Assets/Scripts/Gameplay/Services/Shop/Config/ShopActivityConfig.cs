using System;
using System.Collections.Generic;
using Gameplay.Services.Activity.Base;
using Signals;
using Signals.Activities;
using Signals.Activities.Base;
using Signals.Activities.Shop;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Services.Shop.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(ShopActivityConfig), fileName = nameof(ShopActivityConfig))]
    public class ShopActivityConfig : ActivityConfig
    {
        public Core.Pickup.Base.Pickupable shopExitPickupable;
        [FormerlySerializedAs("shopBoxPrefabs")] public List<GoodConfig> shopBoxConfigs = new();
        
        [Serializable]
        public struct GoodConfig
        {
            public Core.Pickup.Base.Pickupable pickupablePrefab;
            public int price;
        }
        
        public override IActivityRequest ConstructRequest(TreeLevelChangedSignal signal) => new ShopActivityRequest { TreeLevelChangedSignal = signal };
    }
}