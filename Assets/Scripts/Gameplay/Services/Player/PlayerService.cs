using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Core.Character;
using Gameplay.Core.Container;
using Gameplay.Core.TargetTracking.Provider;
using Gameplay.Services.Base;
using Gameplay.Services.Player.Config;
using Signals.GameStates;
using Signals.Level;
using Signals.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay.Services.Player
{
    public class PlayerService : GameService, IInitializable
    {
        [Inject] private PlayerConfig _playerConfig;

        private ComponentContainer _player;

        public override void Initialize()
        {
            _signalBus.Subscribe<NextLevelRequest>(ActivatePlayer);
            _signalBus.Subscribe<EndGameRequest>(DeactivatePlayer);
            base.Initialize();
        }

        private void ActivatePlayer()
        {
            if (_player.gameObject.activeSelf)
                return;

            _player.Activate(Vector3.up * 5);
            _player.Reset();
        }

        private void DeactivatePlayer()
        {
            if (!_player)
                return;

            _player.Deactivate();
        }

        public override async void Boot()
        {
            await CreatePlayer();
            base.Boot();
        }

        private async Task CreatePlayer()
        {
            _player = (await Addressables.InstantiateAsync(_playerConfig.playerReference))
                .GetComponent<ComponentContainer>();

            _player.GetComponent<CharacterComponent>().Dead += OnPlayerDead;

            await _player.Initialize();

            TargetProvider.SetPlayer(_player.transform);
            
            _signalBus.Fire(new PlayerInitializedSignal { Player = _player });

            DeactivatePlayer();
        }

        private void OnPlayerDead() =>
            _signalBus.Fire(new LevelPassedSignal { PassedType = LevelPassedSignal.LevelPassedType.Loose });
    }
}