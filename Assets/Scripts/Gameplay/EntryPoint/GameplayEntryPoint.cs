using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Character;
using Gameplay.EntryPoint.Config;
using Gameplay.Player;
using Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Initialize;
using Zenject;

namespace Gameplay.EntryPoint
{
    public class GameplayEntryPoint : IInitializableConcreteConfig<GameplayEntryPointConfig>
    {
        [Inject] private SignalBus _signalBus;
        
        [Inject]
        public async Task Initialize(GameplayEntryPointConfig handler)
        {
            var player = await CreatePlayer(handler.playerAssetReference);
            
            await player.Initialize(handler.playerConfig);
            
            _signalBus.Fire<NextBossRequestSignal>();
            _signalBus.Fire(new PlayerInitializedSignal(){Player = player});
        }

        private async Task<CharacterComponent> CreatePlayer(AssetReferenceGameObject playerReference)
        {
            var obj = await Addressables.InstantiateAsync(playerReference);

            if (!obj.TryGetComponent(out CharacterComponent playerComponent))
            {
                Debug.LogError("No player component on player prefab");
                return null;
            }

            return playerComponent;
        }
    }
}