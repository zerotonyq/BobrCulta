using Gameplay.Services.Base;
using Gameplay.Services.Shop.Config;
using Signals.Activities;
using Signals.Activities.Base;
using UnityEngine;
using Zenject;

namespace Gameplay.Services.Shop
{
    public class ShopService : GameService, IInitializable
    {
        [Inject] private ShopActivityConfig _config;

        public override void Initialize()
        {
            _signalBus.Subscribe<IActivityRequest>((a) => 
            {
                if (a is not ShopActivityRequest) return;
                Debug.Log("SHOP");
            });
            base.Initialize();
        }
    }
}