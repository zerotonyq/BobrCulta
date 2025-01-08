using System.Threading.Tasks;
using Binders;
using Cysharp.Threading.Tasks;
using Gameplay.Core.ComponentContainer;
using Gameplay.Core.Pickup;
using Gameplay.Core.TargetTracking.Provider;
using Gameplay.EntryPoint.Config;
using Gameplay.Magic;
using Signals;
using UI.Magic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.EntryPoint
{
    public class GameplayEntryPoint
    {
        [Inject] private SignalBus _signalBus;

        [Inject]
        public async Task Initialize(GameplayEntryPointConfig handler, SignalBus signalBus, MagicAndUIBinder binder)
        {
            var player = await InstantiateType<ComponentContainer>(handler.playerAssetReference);

            var magicProjectilesUIManager =
                await InstantiateType<MagicProjectilesUIManager>(handler.magicProjectilesUIManager);

            magicProjectilesUIManager.Initialize();
            await player.Initialize();
            
            TargetProvider.SetPlayer(player.transform);

            binder.Bind(player.GetComponent<MagicComponent>(), magicProjectilesUIManager);

            _signalBus.Fire<NextBossRequestSignal>();
            _signalBus.Fire(new PlayerInitializedSignal() { Player = player });
        }

        private async Task<T> InstantiateType<T>(AssetReferenceGameObject assetReference)
        {
            var obj = await Addressables.InstantiateAsync(assetReference);

            return obj.GetComponent<T>();
        }
    }
}