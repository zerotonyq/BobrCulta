using System.Collections.Generic;
using Gameplay.Core.Pickup.Base;
using Gameplay.Services.Base;
using Gameplay.Services.Shop.Config;
using Signals;
using Signals.Activities;
using Signals.Activities.Base;
using Signals.Activities.Shop;
using Signals.Level;
using UnityEngine;
using Utils.Pooling;
using Zenject;

namespace Gameplay.Services.Shop
{
    public class ShopService : GameService, IInitializable
    {
        [Inject] private ShopActivityConfig _config;

        private List<ShopBoxComponent> _shopBoxes = new();

        private Vector3 _initPosition;

        public override void Initialize()
        {
            _signalBus.Subscribe<IActivityRequest>(OnShopActivityRequested);
            base.Initialize();
        }

        private void OnShopActivityRequested(IActivityRequest request)
        {
            if (request is not ShopActivityRequest shopActivityRequest)
                return;

            var exit = PoolManager.GetFromPool(_config.shopExitPickupable.GetType(),
                _config.shopExitPickupable.gameObject).GetComponent<Core.Pickup.Base.Pickupable>();

            exit.PickedUp += OnShopExit;
            
            exit.Activate(shopActivityRequest.TreeLevelChangedSignal.LevelPosition);

            SpawnBoxes(
                shopActivityRequest.TreeLevelChangedSignal.LevelPosition,
                shopActivityRequest.TreeLevelChangedSignal.Radius);
        }

        private void SpawnBoxes(Vector3 center, float spawnRadius)
        {
            foreach (var config in _config.shopBoxConfigs)
            {
                var box = PoolManager.GetFromPool(typeof(ShopBoxComponent)).GetComponent<ShopBoxComponent>();
                box.Initialize(config.pickupablePrefab, config.price);
                
                box.Activate(center + new Vector3(Random.Range(-spawnRadius, spawnRadius), 5, Random.Range(-spawnRadius, spawnRadius)));
            }
        }

        private void OnShopExit(Core.Pickup.Base.Pickupable pickupable)
        {
            pickupable.PickedUp -= OnShopExit;
            
            _signalBus.Fire(new LevelPassedSignal() { PassedType = LevelPassedSignal.LevelPassedType.Next });
        }
    }
}