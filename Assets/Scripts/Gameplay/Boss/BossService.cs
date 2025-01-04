using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Boss.Location.Config;
using Gameplay.Character;
using Gameplay.Core.Movement.Physical;
using Gameplay.Modifiers.Velocity;
using Gameplay.Modifiers.Velocity.CircleMovement;
using Gameplay.Player.Base;
using Modifiers;
using R3;
using Signals;
using UnityEngine.AddressableAssets;
using Utils.Initialize;
using Zenject;

namespace Gameplay.Boss
{
    public class BossService : IInitializableConcreteConfig<BossServiceConfig>
    {
        //или icharacter????
        private CharacterComponent _currentBoss;
        private IList<AssetReferenceGameObject> _bossesReferences;
        private int _bossIndex;

        [Inject] private SignalBus _signalBus;

        [Inject]
        public Task Initialize(BossServiceConfig config)
        {
            _bossesReferences = config.bossesReferences;
            _signalBus.Subscribe<NextBossRequestSignal>(SpawnNext);
            return Task.CompletedTask;
        }

        private async void SpawnNext()
        {
            if(_currentBoss) Addressables.ReleaseInstance(_currentBoss.gameObject);

            _bossIndex = _bossIndex >= _bossesReferences.Count ? 0 : _bossIndex++;

            _currentBoss =
                (await Addressables.InstantiateAsync(_bossesReferences[_bossIndex])).GetComponent<CharacterComponent>();

            _signalBus.Fire(new NextBossSignal { Boss = _currentBoss });
            
            SetTargetForAllCharacters();
        }

        private void SetTargetForAllCharacters()
        {
            if (!ModifierProvider.Modifiers.TryGetValue(typeof(LookRotationModifier), out var modifiers))
                return;

            foreach (var modifier in modifiers)
                ((LookRotationModifier)modifier).SetTarget(_currentBoss.Transform);
        }
    }
}