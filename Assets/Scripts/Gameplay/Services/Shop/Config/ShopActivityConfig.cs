using System;
using System.Collections.Generic;
using Gameplay.Core.Pickup.Base;
using Gameplay.Services.Activity.Base;
using Signals.Activities;
using Signals.Activities.Base;
using UnityEngine;

namespace Gameplay.Services.Shop.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(ShopActivityConfig), fileName = nameof(ShopActivityConfig))]
    public class ShopActivityConfig : ActivityConfig
    {
        public List<GoodConfig> goodPrefabs = new();
        
        [Serializable]
        public struct GoodConfig
        {
            public Pickupable pickupablePrefab;
            public int price;
        }
        
        //TODO random generation
        protected override IActivityRequest ConstructSignal() => new ShopActivityRequest();
    }
}