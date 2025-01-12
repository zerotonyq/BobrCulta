

using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Container;
using Gameplay.Core.TargetTracking.Provider;
using Gameplay.Services.Base;
using Gameplay.Services.Player.Config;
using Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Services.Player
{
    public class PlayerService : GameService, IInitializable 
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private PlayerConfig _playerConfig;

        private ComponentContainer _player;
        public override async void Boot()
        {
            await  CreatePlayer();
            base.Boot();
        }

        private async Task CreatePlayer()
        {
            if (_playerConfig.playerReference == null)
            {
                Debug.LogError("There is no player reference to create");
                return;
            }
            _player = (await Addressables.InstantiateAsync(_playerConfig.playerReference)).GetComponent<ComponentContainer>();

            await _player.Initialize();
            
            TargetProvider.SetPlayer(_player.transform);
            
            _signalBus.Fire(new PlayerInitializedSignal {Player = _player});
            
            _signalBus.Fire<ServiceBootEndRequest>();
        }
    }
}