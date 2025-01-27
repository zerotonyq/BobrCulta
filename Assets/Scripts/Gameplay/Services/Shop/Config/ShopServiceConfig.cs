using System;
using System.Collections.Generic;
using Gameplay.Core.Pickup.Base;
using UnityEngine;

namespace Gameplay.Services.Shop.Config
{
    [CreateAssetMenu(menuName = "CreateConfig/" + nameof(ShopServiceConfig), fileName = nameof(ShopServiceConfig))]
    public class ShopServiceConfig : ScriptableObject
    {
        public List<GoodConfig> goodPrefabs = new();
        
        [Serializable]
        public struct GoodConfig
        {
            public Pickupable pickupablePrefab;
            public int price;
        }
        
        //TODO random generation
    }
}